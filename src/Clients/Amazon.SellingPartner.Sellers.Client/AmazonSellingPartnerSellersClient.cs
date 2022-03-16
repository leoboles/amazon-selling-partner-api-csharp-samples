//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."
#pragma warning disable 8073 // Disable "CS8073 The result of the expression is always 'false' since a value of type 'T' is never equal to 'null' of type 'T?'"
#pragma warning disable 3016 // Disable "CS3016 Arrays as attribute arguments is not CLS-compliant"
#pragma warning disable 8603 // Disable "CS8603 Possible null reference return"

namespace Amazon.SellingPartner.Sellers.Client
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial interface IAmazonSellingPartnerSellersClient
    {
        /// <returns>Marketplace participations successfully retrieved.</returns>
        /// <exception cref="AmazonSellingPartnerSellersApiException">A server side error occurred.</exception>
        GetMarketplaceParticipationsResponse GetMarketplaceParticipations();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Marketplace participations successfully retrieved.</returns>
        /// <exception cref="AmazonSellingPartnerSellersApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<GetMarketplaceParticipationsResponse> GetMarketplaceParticipationsAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class AmazonSellingPartnerSellersClient : IAmazonSellingPartnerSellersClient
    {
        private System.Net.Http.HttpClient _httpClient;
        private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

        public AmazonSellingPartnerSellersClient(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
        }

        private Newtonsoft.Json.JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            UpdateJsonSerializerSettings(settings);
            return settings;
        }

        public Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

        /// <returns>Marketplace participations successfully retrieved.</returns>
        /// <exception cref="AmazonSellingPartnerSellersApiException">A server side error occurred.</exception>
        public virtual GetMarketplaceParticipationsResponse GetMarketplaceParticipations()
        {
            return System.Threading.Tasks.Task.Run(async () => await GetMarketplaceParticipationsAsync(System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Marketplace participations successfully retrieved.</returns>
        /// <exception cref="AmazonSellingPartnerSellersApiException">A server side error occurred.</exception>
        public virtual async System.Threading.Tasks.Task<GetMarketplaceParticipationsResponse> GetMarketplaceParticipationsAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("sellers/v1/marketplaceParticipations");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<GetMarketplaceParticipationsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new AmazonSellingPartnerSellersApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ == 400)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<GetMarketplaceParticipationsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new AmazonSellingPartnerSellersApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new AmazonSellingPartnerSellersApiException<GetMarketplaceParticipationsResponse>("Request has missing or invalid parameters and cannot be parsed.", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                        else
                        if (status_ == 403)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<GetMarketplaceParticipationsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new AmazonSellingPartnerSellersApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new AmazonSellingPartnerSellersApiException<GetMarketplaceParticipationsResponse>("403 can be caused for reasons like Access Denied, Unauthorized, Expired Token, Invalid Signature or Resource Not Found.", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                        else
                        if (status_ == 404)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<GetMarketplaceParticipationsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new AmazonSellingPartnerSellersApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new AmazonSellingPartnerSellersApiException<GetMarketplaceParticipationsResponse>("The resource specified does not exist.", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                        else
                        if (status_ == 413)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<GetMarketplaceParticipationsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new AmazonSellingPartnerSellersApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new AmazonSellingPartnerSellersApiException<GetMarketplaceParticipationsResponse>("The request size exceeded the maximum accepted size.", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                        else
                        if (status_ == 415)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<GetMarketplaceParticipationsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new AmazonSellingPartnerSellersApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new AmazonSellingPartnerSellersApiException<GetMarketplaceParticipationsResponse>("The entity of the request is in a format not supported by the requested resource.", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                        else
                        if (status_ == 429)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<GetMarketplaceParticipationsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new AmazonSellingPartnerSellersApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new AmazonSellingPartnerSellersApiException<GetMarketplaceParticipationsResponse>("The frequency of requests was greater than allowed.", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                        else
                        if (status_ == 500)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<GetMarketplaceParticipationsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new AmazonSellingPartnerSellersApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new AmazonSellingPartnerSellersApiException<GetMarketplaceParticipationsResponse>("Encountered an unexpected condition which prevented the server from fulfilling the request.", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                        else
                        if (status_ == 503)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<GetMarketplaceParticipationsResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new AmazonSellingPartnerSellersApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new AmazonSellingPartnerSellersApiException<GetMarketplaceParticipationsResponse>("Temporary overloading or maintenance of the server.", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new AmazonSellingPartnerSellersApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        protected struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                this.Object = responseObject;
                this.Text = responseText;
            }

            public T Object { get; }

            public string Text { get; }
        }

        public bool ReadResponseAsString { get; set; }

        protected virtual async System.Threading.Tasks.Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Threading.CancellationToken cancellationToken)
        {
            if (response == null || response.Content == null)
            {
                return new ObjectResponseResult<T>(default(T), string.Empty);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw new AmazonSellingPartnerSellersApiException(message, (int)response.StatusCode, responseText, headers, exception);
                }
            }
            else
            {
                try
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var streamReader = new System.IO.StreamReader(responseStream))
                    using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                    {
                        var serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
                        var typedBody = serializer.Deserialize<T>(jsonTextReader);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                    throw new AmazonSellingPartnerSellersApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
                }
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return "";
            }

            if (value is System.Enum)
            {
                var name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute)) 
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value != null ? attribute.Value : name;
                        }
                    }

                    var converted = System.Convert.ToString(System.Convert.ChangeType(value, System.Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                    return converted == null ? string.Empty : converted;
                }
            }
            else if (value is bool) 
            {
                return System.Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[]) value);
            }
            else if (value.GetType().IsArray)
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Array) value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            var result = System.Convert.ToString(value, cultureInfo);
            return result == null ? "" : result;
        }
    }

    /// <summary>
    /// Error response returned when the request is unsuccessful.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class Error
    {
        /// <summary>
        /// An error code that identifies the type of error that occured.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("code", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string Code { get; set; }

        /// <summary>
        /// A message that describes the error condition in a human-readable form.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("message", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string Message { get; set; }

        /// <summary>
        /// Additional details that can help the caller understand or fix the issue.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("details", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Details { get; set; }

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static Error FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Error>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }

    /// <summary>
    /// A list of error responses returned when a request is unsuccessful.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ErrorList : System.Collections.ObjectModel.Collection<Error>
    {

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static ErrorList FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorList>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class MarketplaceParticipation
    {
        [Newtonsoft.Json.JsonProperty("marketplace", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public Marketplace Marketplace { get; set; } = new Marketplace();

        [Newtonsoft.Json.JsonProperty("participation", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public Participation Participation { get; set; } = new Participation();

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static MarketplaceParticipation FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<MarketplaceParticipation>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }

    /// <summary>
    /// List of marketplace participations.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class MarketplaceParticipationList : System.Collections.ObjectModel.Collection<MarketplaceParticipation>
    {

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static MarketplaceParticipationList FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<MarketplaceParticipationList>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }

    /// <summary>
    /// The response schema for the getMarketplaceParticipations operation.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class GetMarketplaceParticipationsResponse
    {
        /// <summary>
        /// The payload for the getMarketplaceParticipations operation.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("payload", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public MarketplaceParticipationList Payload { get; set; }

        /// <summary>
        /// Encountered errors for the getMarketplaceParticipations operation.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("errors", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ErrorList Errors { get; set; }

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static GetMarketplaceParticipationsResponse FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<GetMarketplaceParticipationsResponse>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }

    /// <summary>
    /// Detailed information about an Amazon market where a seller can list items for sale and customers can view and purchase items.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class Marketplace
    {
        /// <summary>
        /// The encrypted marketplace value.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string Id { get; set; }

        /// <summary>
        /// Marketplace name.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string Name { get; set; }

        /// <summary>
        /// The ISO 3166-1 alpha-2 format country code of the marketplace.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("countryCode", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^([A-Z]{2})$")]
        public string CountryCode { get; set; }

        /// <summary>
        /// The ISO 4217 format currency code of the marketplace.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("defaultCurrencyCode", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string DefaultCurrencyCode { get; set; }

        /// <summary>
        /// The ISO 639-1 format language code of the marketplace.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("defaultLanguageCode", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string DefaultLanguageCode { get; set; }

        /// <summary>
        /// The domain name of the marketplace.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("domainName", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string DomainName { get; set; }

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static Marketplace FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Marketplace>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }

    /// <summary>
    /// Detailed information that is specific to a seller in a Marketplace.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class Participation
    {
        [Newtonsoft.Json.JsonProperty("isParticipating", Required = Newtonsoft.Json.Required.Always)]
        public bool IsParticipating { get; set; }

        /// <summary>
        /// Specifies if the seller has suspended listings. True if the seller Listing Status is set to Inactive, otherwise False.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("hasSuspendedListings", Required = Newtonsoft.Json.Required.Always)]
        public bool HasSuspendedListings { get; set; }

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static Participation FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Participation>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }



    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class AmazonSellingPartnerSellersApiException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public AmazonSellingPartnerSellersApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class AmazonSellingPartnerSellersApiException<TResult> : AmazonSellingPartnerSellersApiException
    {
        public TResult Result { get; private set; }

        public AmazonSellingPartnerSellersApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }

}

#pragma warning restore 1591
#pragma warning restore 1573
#pragma warning restore  472
#pragma warning restore  114
#pragma warning restore  108
#pragma warning restore 3016
#pragma warning restore 8603