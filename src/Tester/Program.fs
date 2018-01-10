// Learn more about F# at http://fsharp.org

open System

open RestSharp
open RestSharp.Deserializers
open RestSharp.Serializers

open FsCheck

// let urlIsNotLost (uri:string) = RestClient(uri).BaseUrl = new Uri(uri)

type RequestModel() =
    interface IRestRequest with
        /// <summary>
        /// Always send a multipart/form-data request - even when no Files are present.
        /// </summary>
        member val AlwaysMultipartFormData : bool = true with get, set

        /// <summary>
        /// Serializer to use when writing JSON request bodies. Used if RequestFormat is Json.
        /// By default the included JsonSerializer is used (currently using JSON.NET default serialization).
        /// </summary>
        member val JsonSerializer : ISerializer = null with get, set

        /// <summary>
        /// Serializer to use when writing XML request bodies. Used if RequestFormat is Xml.
        /// By default the included XmlSerializer is used.
        /// </summary>
        member val XmlSerializer : ISerializer = null with get, set

        /// <summary>
        /// Set this to write response to Stream rather than reading into memory.
        /// </summary>
        member val ResponseWriter : Action<IO.Stream> = null with get, set

        /// <summary>
        /// Container of all HTTP parameters to be passed with the request.
        /// See AddParameter() for explanation of the types of parameters that can be passed
        /// </summary>
        member val Parameters : Collections.Generic.List<Parameter> = Collections.Generic.List() with get

        /// <summary>
        /// Container of all the files to be uploaded with the request.
        /// </summary>
        member val Files : Collections.Generic.List<FileParameter> = Collections.Generic.List() with get

        /// <summary>
        /// Determines what HTTP method to use for this request. Supported methods: GET, POST, PUT, DELETE, HEAD, OPTIONS
        /// Default is GET
        /// </summary>
        member val Method : Method = Method.GET with get, set

        /// <summary>
        /// The Resource URL to make the request against.
        /// Tokens are substituted with UrlSegment parameters and match by name.
        /// Should not include the scheme or domain. Do not include leading slash.
        /// Combined with RestClient.BaseUrl to assemble final URL:
        /// {BaseUrl}/{Resource} (BaseUrl is scheme + domain, e.g. http://example.com)
        /// </summary>
        /// <example>
        /// // example for url token replacement
        /// request.Resource = "Products/{ProductId}";
        /// request.AddParameter("ProductId", 123, ParameterType.UrlSegment);
        /// </example>
        member val Resource : string = "notInitializedResource" with get, set

        /// <summary>
        /// Serializer to use when writing XML request bodies. Used if RequestFormat is Xml.
        /// By default XmlSerializer is used.
        /// </summary>
        member val RequestFormat : DataFormat = DataFormat.Xml with get, set

        /// <summary>
        /// Used by the default deserializers to determine where to start deserializing from.
        /// Can be used to skip container or root elements that do not have corresponding deserialzation targets.
        /// </summary>
        member val RootElement : string = "notInitializedRootElement" with get, set

        /// <summary>
        /// Used by the default deserializers to explicitly set which date format string to use when parsing dates.
        /// </summary>
        member val DateFormat : string = "notInitializedDateFormat" with get, set

        /// <summary>
        /// Used by XmlDeserializer. If not specified, XmlDeserializer will flatten response by removing namespaces from element names.
        /// </summary>
        member val XmlNamespace : string = "notInitializedXmlNamespace" with get, set

        /// <summary>
        /// In general you would not need to set this directly. Used by the NtlmAuthenticator.
        /// </summary>
        member val Credentials : Net.ICredentials = null with get, set

        /// <summary>
        /// Timeout in milliseconds to be used for the request. This timeout value overrides a timeout set on the RestClient.
        /// </summary>
        member val Timeout : int = -1 with get, set

        /// <summary>
        /// The number of milliseconds before the writing or reading times out.  This timeout value overrides a timeout set on the RestClient.
        /// </summary>
        member val ReadWriteTimeout : int = -2 with get, set

        /// <summary>
        /// How many attempts were made to send this Request?
        /// </summary>
        /// <remarks>
        /// This Number is incremented each time the RestClient sends the request.
        /// Useful when using Asynchronous Execution with Callbacks
        /// </remarks>
        member val Attempts : int = -4 with get

        /// <summary>
        /// Determine whether or not the "default credentials" (e.g. the user account under which the current process is running)
        /// will be sent along to the server. The default is false.
        /// </summary>
        member val UseDefaultCredentials : bool = false with get, set

        /// <summary>
        /// List of Allowed Decompression Methods
        /// </summary>
        member val AllowedDecompressionMethods : Collections.Generic.IList<Net.DecompressionMethods> = Collections.Generic.List() :> Collections.Generic.IList<Net.DecompressionMethods> with get

        member val OnBeforeDeserialization : Action<IRestResponse> = null with get, set

        member this.IncreaseNumAttempts() : unit = failwith "Not implemented: IRestRequest.IncreaseNumAttempts() : unit"

        member this.AddDecompressionMethod(decompressionMethod: Net.DecompressionMethods) : IRestRequest = failwith "Not implemented: IRestRequest.AddDecompressionMethod(decompressionMethod: Net.DecompressionMethods) : IRestRequest"

        member this.AddQueryParameter(name: string, value: string) : IRestRequest = failwith "Not implemented: IRestRequest.AddQueryParameter(name: string, value: string) : IRestRequest"

        member this.AddUrlSegment(name: string, value: string) : IRestRequest = failwith "Not implemented: IRestRequest.AddUrlSegment(name: string, value: string) : IRestRequest"

        member this.AddCookie(name: string, value: string) : IRestRequest = failwith "Not implemented: IRestRequest.AddCookie(name: string, value: string) : IRestRequest"

        member this.AddHeader(name: string, value: string) : IRestRequest = failwith "Not implemented: IRestRequest.AddHeader(name: string, value: string) : IRestRequest"

        member this.AddOrUpdateParameter(name: string, value: obj, contentType: string, type_: ParameterType) : IRestRequest = failwith "Not implemented: IRestRequest.AddOrUpdateParameter(name: string, value: obj, contentType: string, type: ParameterType) : IRestRequest"

        member this.AddOrUpdateParameter(name: string, value: obj, type_: ParameterType) : IRestRequest = failwith "Not implemented: IRestRequest.AddOrUpdateParameter(name: string, value: obj, type: ParameterType) : IRestRequest"

        member this.AddOrUpdateParameter(name: string, value: obj) : IRestRequest = failwith "Not implemented: IRestRequest.AddOrUpdateParameter(name: string, value: obj) : IRestRequest"

        member this.AddOrUpdateParameter(p: Parameter) : IRestRequest = failwith "Not implemented: IRestRequest.AddOrUpdateParameter(p: Parameter) : IRestRequest"

        member this.AddParameter(name: string, value: obj, contentType: string, type_: ParameterType) : IRestRequest = failwith "Not implemented: IRestRequest.AddParameter(name: string, value: obj, contentType: string, type: ParameterType) : IRestRequest"

        member this.AddParameter(name: string, value: obj, type_: ParameterType) : IRestRequest = failwith "Not implemented: IRestRequest.AddParameter(name: string, value: obj, type: ParameterType) : IRestRequest"

        member this.AddParameter(name: string, value: obj) : IRestRequest = failwith "Not implemented: IRestRequest.AddParameter(name: string, value: obj) : IRestRequest"

        member this.AddParameter(p: Parameter) : IRestRequest = failwith "Not implemented: IRestRequest.AddParameter(p: Parameter) : IRestRequest"

        member this.AddObject(obj: obj) : IRestRequest = failwith "Not implemented: IRestRequest.AddObject(obj: obj) : IRestRequest"

        member this.AddObject(obj: obj, [<ParamArray>] includedProperties: string []) : IRestRequest = failwith "Not implemented: IRestRequest.AddObject(obj: obj, [<ParamArray>] includedProperties: string []) : IRestRequest"

        member this.AddXmlBody(obj: obj, xmlNamespace: string) : IRestRequest = failwith "Not implemented: IRestRequest.AddXmlBody(obj: obj, xmlNamespace: string) : IRestRequest"

        member this.AddXmlBody(obj: obj) : IRestRequest = failwith "Not implemented: IRestRequest.AddXmlBody(obj: obj) : IRestRequest"

        member this.AddJsonBody(obj: obj) : IRestRequest = failwith "Not implemented: IRestRequest.AddJsonBody(obj: obj) : IRestRequest"

        member this.AddBody(obj: obj) : IRestRequest = failwith "Not implemented: IRestRequest.AddBody(obj: obj) : IRestRequest"

        member this.AddBody(obj: obj, xmlNamespace: string) : IRestRequest = failwith "Not implemented: IRestRequest.AddBody(obj: obj, xmlNamespace: string) : IRestRequest"

        member this.AddFileBytes(name: string, bytes: byte [], filename: string, contentType: string) : IRestRequest = failwith "Not implemented: IRestRequest.AddFileBytes(name: string, bytes: byte [], filename: string,?contentType: string) : IRestRequest"

        member this.AddFile(name: string, writer: Action<IO.Stream>, fileName: string, contentLength: int64, contentType: string) : IRestRequest = failwith "Not implemented: IRestRequest.AddFile(name: string, writer: Action<IO.Stream>, fileName: string, contentLength: int64,?contentType: string) : IRestRequest"

        member this.AddFile(name: string, bytes: byte [], fileName: string, contentType: string) : IRestRequest = failwith "Not implemented: IRestRequest.AddFile(name: string, bytes: byte [], fileName: string,?contentType: string) : IRestRequest"

        member this.AddFile(name: string, path: string, contentType: string) : IRestRequest = failwith "Not implemented: IRestRequest.AddFile(name: string, path: string,?contentType: string) : IRestRequest"


let accept2types((t1, t2):string * string) =
    if t1 = null || t2 = null || t1 = "" || t2 = "" || t1 = t2
    then
       true
    else
        let mutable add2ThenRemoveSecond = RestClient();
        let mutable addOnlyFirst = RestClient();
        add2ThenRemoveSecond.ClearHandlers();
        addOnlyFirst.ClearHandlers();
        add2ThenRemoveSecond.AddHandler(t1, JsonDeserializer());
        addOnlyFirst.AddHandler(t1, JsonDeserializer());
        add2ThenRemoveSecond.AddHandler(t2, XmlDeserializer());
        add2ThenRemoveSecond.RemoveHandler(t2);
        add2ThenRemoveSecond.DefaultParameters = addOnlyFirst.DefaultParameters

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!";
    Check.One({ Config.Quick with MaxTest = 10; QuietOnSuccess=false }, accept2types);
    0 // return an integer exit code
