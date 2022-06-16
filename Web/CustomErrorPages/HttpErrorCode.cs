// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.CustomErrorPages.HttpErrorCode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.CustomErrorPages
{
  /// <summary>Represents Http Status Error Code</summary>
  internal enum HttpErrorCode
  {
    /// <summary>
    /// Equivalent to HTTP status 400. HttpErrorCode.BadRequest indicates
    /// that the request could not be understood by the server. HttpErrorCode.BadRequest
    /// is sent when no other error is applicable, or if the exact error is unknown or
    /// does not have its own error code.
    /// </summary>
    BadRequest = 400, // 0x00000190
    /// <summary>
    ///     Equivalent to HTTP status 402. HttpErrorCode.PaymentRequired is reserved
    ///     for future use.
    /// </summary>
    PaymentRequired = 402, // 0x00000192
    /// <summary>
    ///     Equivalent to HTTP status 403. HttpErrorCode.Forbidden indicates
    ///     that the server refuses to fulfill the request.
    /// </summary>
    Forbidden = 403, // 0x00000193
    /// <summary>
    ///     Equivalent to HTTP status 404. HttpErrorCode.NotFound indicates that
    ///     the requested resource does not exist on the server.
    /// </summary>
    NotFound = 404, // 0x00000194
    /// <summary>
    ///     Equivalent to HTTP status 405. HttpErrorCode.MethodNotAllowed indicates
    ///     that the request method (POST or GET) is not allowed on the requested resource.
    /// </summary>
    MethodNotAllowed = 405, // 0x00000195
    /// <summary>
    ///     Equivalent to HTTP status 406. HttpErrorCode.NotAcceptable indicates
    ///     that the client has indicated with Accept headers that it will not accept any
    ///     of the available representations of the resource.
    /// </summary>
    NotAcceptable = 406, // 0x00000196
    /// <summary>
    ///     Equivalent to HTTP status 407. HttpErrorCode.ProxyAuthenticationRequired
    ///     indicates that the requested proxy requires authentication. The Proxy-authenticate
    ///     header contains the details of how to perform the authentication.
    /// </summary>
    ProxyAuthenticationRequired = 407, // 0x00000197
    /// <summary>
    ///     Equivalent to HTTP status 408. HttpErrorCode.RequestTimeout indicates
    ///     that the client did not send a request within the time the server was expecting
    ///     the request.
    /// </summary>
    RequestTimeout = 408, // 0x00000198
    /// <summary>
    ///     Equivalent to HTTP status 409. HttpErrorCode.Conflict indicates that
    ///     the request could not be carried out because of a conflict on the server.
    /// </summary>
    Conflict = 409, // 0x00000199
    /// <summary>
    ///     Equivalent to HTTP status 410. HttpErrorCode.Gone indicates that
    ///     the requested resource is no longer available.
    /// </summary>
    Gone = 410, // 0x0000019A
    /// <summary>
    ///     Equivalent to HTTP status 411. HttpErrorCode.LengthRequired indicates
    ///     that the required Content-length header is missing.
    /// </summary>
    LengthRequired = 411, // 0x0000019B
    /// <summary>
    ///     Equivalent to HTTP status 412. HttpErrorCode.PreconditionFailed indicates
    ///     that a condition set for this request failed, and the request cannot be carried
    ///     out. Conditions are set with conditional request headers like If-Match, If-None-Match,
    ///     or If-Unmodified-Since.
    /// </summary>
    PreconditionFailed = 412, // 0x0000019C
    /// <summary>
    ///     Equivalent to HTTP status 413. HttpErrorCode.RequestEntityTooLarge
    ///     indicates that the request is too large for the server to process.
    /// </summary>
    RequestEntityTooLarge = 413, // 0x0000019D
    /// <summary>
    ///     Equivalent to HTTP status 414. HttpErrorCode.RequestUriTooLong indicates
    ///     that the URI is too long.
    /// </summary>
    RequestUriTooLong = 414, // 0x0000019E
    /// <summary>
    ///     Equivalent to HTTP status 415. HttpErrorCode.UnsupportedMediaType
    ///     indicates that the request is an unsupported type.
    /// </summary>
    UnsupportedMediaType = 415, // 0x0000019F
    /// <summary>
    ///     Equivalent to HTTP status 416. HttpErrorCode.RequestedRangeNotSatisfiable
    ///     indicates that the range of data requested from the resource cannot be returned,
    ///     either because the beginning of the range is before the beginning of the resource,
    ///     or the end of the range is after the end of the resource.
    /// </summary>
    RequestedRangeNotSatisfiable = 416, // 0x000001A0
    /// <summary>
    ///     Equivalent to HTTP status 417. HttpErrorCode.ExpectationFailed indicates
    ///     that an expectation given in an Expect header could not be met by the server.
    /// </summary>
    ExpectationFailed = 417, // 0x000001A1
    /// <summary>
    ///     Equivalent to HTTP status 426. HttpErrorCode.UpgradeRequired indicates
    ///     that the client should switch to a different protocol such as TLS/1.0.
    /// </summary>
    UpgradeRequired = 426, // 0x000001AA
    /// <summary>
    ///     Equivalent to HTTP status 500. HttpErrorCode.InternalServerError
    ///     indicates that a generic error has occurred on the server.
    /// </summary>
    InternalServerError = 500, // 0x000001F4
    /// <summary>
    ///     Equivalent to HTTP status 501. HttpErrorCode.NotImplemented indicates
    ///     that the server does not support the requested function.
    /// </summary>
    NotImplemented = 501, // 0x000001F5
    /// <summary>
    ///     Equivalent to HTTP status 502. HttpErrorCode.BadGateway indicates
    ///     that an intermediate proxy server received a bad response from another proxy
    ///     or the origin server.
    /// </summary>
    BadGateway = 502, // 0x000001F6
    /// <summary>
    ///     Equivalent to HTTP status 503. HttpErrorCode.ServiceUnavailable indicates
    ///     that the server is temporarily unavailable, usually due to high load or maintenance.
    /// </summary>
    ServiceUnavailable = 503, // 0x000001F7
    /// <summary>
    ///     Equivalent to HTTP status 504. HttpErrorCode.GatewayTimeout indicates
    ///     that an intermediate proxy server timed out while waiting for a response from
    ///     another proxy or the origin server.
    /// </summary>
    GatewayTimeout = 504, // 0x000001F8
    /// <summary>
    ///     Equivalent to HTTP status 505. HttpErrorCode.HttpVersionNotSupported
    ///     indicates that the requested HTTP version is not supported by the server.
    /// </summary>
    HttpVersionNotSupported = 505, // 0x000001F9
  }
}
