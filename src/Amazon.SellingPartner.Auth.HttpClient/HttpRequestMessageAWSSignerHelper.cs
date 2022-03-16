﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Amazon.SellingPartner.Auth.Core;

namespace Amazon.SellingPartner.Auth.HttpClient
{
    public class HttpRequestMessageAWSSignerHelper
    {
        public const string ISO8601BasicDateTimeFormat = "yyyyMMddTHHmmssZ";
        public const string ISO8601BasicDateFormat = "yyyyMMdd";

        public const string XAmzDateHeaderName = "X-Amz-Date";
        public const string AuthorizationHeaderName = "Authorization";
        public const string CredentialSubHeaderName = "Credential";
        public const string SignatureSubHeaderName = "Signature";
        public const string SignedHeadersSubHeaderName = "SignedHeaders";
        public const string HostHeaderName = "host";

        public const string Scheme = "AWS4";
        public const string Algorithm = "HMAC-SHA256";
        public const string TerminationString = "aws4_request";
        public const string ServiceName = "execute-api";
        public const string Slash = "/";

        private readonly static Regex CompressWhitespaceRegex = new Regex("\\s+");

        public HttpRequestMessageAWSSignerHelper()
        {
            DateHelper = new SigningDateHelper();
        }

        public virtual IDateHelper DateHelper { get; set; }

        /// <summary>
        /// Returns URI encoded version of absolute path
        /// </summary>
        /// <param name="resource">Resource path(absolute path) from the request</param>
        /// <returns>URI encoded version of absolute path</returns>
        public virtual string ExtractCanonicalURIParameters(string resource)
        {
            string canonicalUri = string.Empty;

            if (string.IsNullOrEmpty(resource))
            {
                canonicalUri = Slash;
            }
            else
            {
                if (!resource.StartsWith(Slash))
                {
                    canonicalUri = Slash;
                }

                //Split path at / into segments
                IEnumerable<string> encodedSegments = resource.Split(new char[] { '/' }, StringSplitOptions.None);

                // Encode twice
                encodedSegments = encodedSegments.Select(segment => Utils.UrlEncode(segment));
                encodedSegments = encodedSegments.Select(segment => Utils.UrlEncode(segment));

                canonicalUri += string.Join(Slash, encodedSegments.ToArray());
            }

            return canonicalUri;
        }

        private static Dictionary<string, string> CovertNameValueCollectionToDictionary(NameValueCollection nvc)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var k in nvc.AllKeys)
            {
                if (k == null)
                    continue;
                dict.Add(k.Trim().ToString(), nvc[k]);
            }

            return dict;
        }

        /// <summary>
        /// Returns query parameters in canonical order with URL encoding
        /// </summary>
        /// <param name="request">RestRequest</param>
        /// <returns>Query parameters in canonical order with URL encoding</returns>
        public virtual string ExtractCanonicalQueryString(HttpRequestMessage request)
        {
            IDictionary<string, string> queryParameters = CovertNameValueCollectionToDictionary(HttpUtility.ParseQueryString(request.RequestUri.Query.ToString()));

            SortedDictionary<string, string> sortedqueryParameters = new SortedDictionary<string, string>(queryParameters);

            StringBuilder canonicalQueryString = new StringBuilder();
            foreach (var key in sortedqueryParameters.Keys)
            {
                if (canonicalQueryString.Length > 0)
                {
                    canonicalQueryString.Append("&");
                }

                canonicalQueryString.AppendFormat("{0}={1}",
                    Utils.UrlEncode(key),
                    Utils.UrlEncode(sortedqueryParameters[key]));
            }

            return canonicalQueryString.ToString();
        }

        /// <summary>
        /// Returns Http headers in canonical order with all header names to lowercase
        /// </summary>
        /// <param name="request">RestRequest</param>
        /// <returns>Returns Http headers in canonical order</returns>
        public virtual string ExtractCanonicalHeaders(HttpRequestMessage request)
        {
            IDictionary<string, string> headers = request.Headers
                .ToDictionary(header => header.Key.Trim().ToLowerInvariant(), header => header.Value.FirstOrDefault())!;

            SortedDictionary<string, string> sortedHeaders = new SortedDictionary<string, string>(headers);

            StringBuilder headerString = new StringBuilder();

            foreach (string headerName in sortedHeaders.Keys)
            {
                headerString.AppendFormat("{0}:{1}\n",
                    headerName,
                    CompressWhitespaceRegex.Replace(sortedHeaders[headerName].Trim(), " "));
            }

            return headerString.ToString();
        }

        /// <summary>
        /// Returns list(as string) of Http headers in canonical order
        /// </summary>
        /// <param name="request">RestRequest</param>
        /// <returns>List of Http headers in canonical order</returns>
        public virtual string ExtractSignedHeaders(HttpRequestMessage request)
        {
            List<string> rawHeaders = request.Headers
                .Select(header => header.Key.Trim().ToLowerInvariant())
                .ToList();
            rawHeaders.Sort(StringComparer.OrdinalIgnoreCase);

            return string.Join(";", rawHeaders);
        }

        /// <summary>
        /// Returns hexadecimal hashed value(using SHA256) of payload in the body of request
        /// </summary>
        /// <param name="request">RestRequest</param>
        /// <returns>Hexadecimal hashed value of payload in the body of request</returns>
        public virtual string HashRequestBody(HttpRequestMessage request)
        {
            string body = null;
            if (request.Content != null)
            {
                body = request.Content.ReadAsStringAsync()
                    .GetAwaiter()
                    .GetResult();
            }

            string value = body != null || !string.IsNullOrWhiteSpace(body) ? body.ToString() : string.Empty;
            return Utils.ToHex(Utils.Hash(value));
        }

        /// <summary>
        /// Builds the string for signing using signing date, hashed canonical request and region
        /// </summary>
        /// <param name="signingDate">Signing Date</param>
        /// <param name="hashedCanonicalRequest">Hashed Canonical Request</param>
        /// <param name="region">Region</param>
        /// <returns>String to be used for signing</returns>
        public virtual string BuildStringToSign(DateTime signingDate, string hashedCanonicalRequest, string region)
        {
            string scope = BuildScope(signingDate, region);
            string stringToSign = string.Format(CultureInfo.InvariantCulture, "{0}-{1}\n{2}\n{3}\n{4}",
                Scheme,
                Algorithm,
                signingDate.ToString(ISO8601BasicDateTimeFormat, CultureInfo.InvariantCulture),
                scope,
                hashedCanonicalRequest);

            return stringToSign;
        }

        /// <summary>
        /// Sets AWS4 mandated 'x-amz-date' header, returning the date/time that will
        /// be used throughout the signing process.
        /// </summary>
        /// <param name="request">RestRequest</param>
        /// <param name="host">Request endpoint</param>
        /// <returns>Date and time used for x-amz-date, in UTC</returns>
        public virtual DateTime InitializeHeaders(HttpRequestMessage request, string host)
        {
            request.Headers.Remove(XAmzDateHeaderName);
            request.Headers.Remove(HostHeaderName);

            DateTime signingDate = DateHelper.GetUtcNow();

            request.Headers.Add(XAmzDateHeaderName, signingDate.ToString(ISO8601BasicDateTimeFormat, CultureInfo.InvariantCulture));
            request.Headers.Add(HostHeaderName, host);

            return signingDate;
        }

        /// <summary>
        /// Calculates AWS4 signature for the string, prepared for signing
        /// </summary>
        /// <param name="stringToSign">String to be signed</param>
        /// <param name="signingDate">Signing Date</param>
        /// <param name="secretKey">Secret Key</param>
        /// <param name="region">Region</param>
        /// <returns>AWS4 Signature</returns>
        public virtual string CalculateSignature(string stringToSign,
            DateTime signingDate,
            string secretKey,
            string region)
        {
            string date = signingDate.ToString(ISO8601BasicDateFormat, CultureInfo.InvariantCulture);
            byte[] kSecret = Encoding.UTF8.GetBytes(Scheme + secretKey);
            byte[] kDate = Utils.GetKeyedHash(kSecret, date);
            byte[] kRegion = Utils.GetKeyedHash(kDate, region);
            byte[] kService = Utils.GetKeyedHash(kRegion, ServiceName);
            byte[] kSigning = Utils.GetKeyedHash(kService, TerminationString);

            // Calculate the signature
            return Utils.ToHex(Utils.GetKeyedHash(kSigning, stringToSign));
        }

        /// <summary>
        /// Add a signature to a request in the form of an 'Authorization' header
        /// </summary>
        /// <param name="request">Request to be signed</param>
        /// <param name="accessKeyId">Access Key Id</param>
        /// <param name="signedHeaders">Signed Headers</param>
        /// <param name="signature">The signature to add</param>
        /// <param name="region">AWS region for the request</param>
        /// <param name="signingDate">Signature date</param>
        public virtual void AddSignature(HttpRequestMessage request,
            string accessKeyId,
            string signedHeaders,
            string signature,
            string region,
            DateTime signingDate)
        {
            string scope = BuildScope(signingDate, region);
            StringBuilder authorizationHeaderValueBuilder = new StringBuilder();
            authorizationHeaderValueBuilder.AppendFormat("{0}-{1}", Scheme, Algorithm);
            authorizationHeaderValueBuilder.AppendFormat(" {0}={1}/{2},", CredentialSubHeaderName, accessKeyId, scope);
            authorizationHeaderValueBuilder.AppendFormat(" {0}={1},", SignedHeadersSubHeaderName, signedHeaders);
            authorizationHeaderValueBuilder.AppendFormat(" {0}={1}", SignatureSubHeaderName, signature);

            // will throw an InvalidFormatException if we just use Headers.Add so skip validation
            request.Headers.TryAddWithoutValidation(AuthorizationHeaderName, authorizationHeaderValueBuilder.ToString());
        }

        private static string BuildScope(DateTime signingDate, string region)
        {
            return string.Format("{0}/{1}/{2}/{3}",
                signingDate.ToString(ISO8601BasicDateFormat, CultureInfo.InvariantCulture),
                region,
                ServiceName,
                TerminationString);
        }
    }
}