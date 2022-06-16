// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SWT.SR
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security.Claims.SWT
{
  internal class SR
  {
    /// <summary>
    ///   Looks up a localized string similar to Token doesn't meet audience restrictions..
    /// </summary>
    internal static string AudienceMismatch => "Audience Mismatch";

    /// <summary>
    ///   Looks up a localized string similar to HTTP authorization header: {0}.
    /// </summary>
    internal static string AuthorizationHeaderFormat => "Authorization Header Format";

    /// <summary>
    ///   Looks up a localized string similar to Authentication tokens other than UserName/Password are currently not supported..
    /// </summary>
    internal static string AuthTokenNotSupported => "Auth Token Not Supported";

    /// <summary>
    ///   Looks up a localized string similar to Creating claims from SimpleWebToken....
    /// </summary>
    internal static string CreatingClaimsInfo => nameof (CreatingClaimsInfo);

    /// <summary>
    ///   Looks up a localized string similar to Creating SimpleWebToken....
    /// </summary>
    internal static string CreatingSWT => nameof (CreatingSWT);

    /// <summary>
    ///   Looks up a localized string similar to Incomming token issuer is resolved as {0}.
    /// </summary>
    internal static string IncomingTokenIssuerResolvedAs => nameof (IncomingTokenIssuerResolvedAs);

    /// <summary>
    ///   Looks up a localized string similar to Input authentication token {0} created.
    /// </summary>
    internal static string InputTokenCreated => nameof (InputTokenCreated);

    /// <summary>
    ///   Looks up a localized string similar to Internal server error. Error id = {0}.
    /// </summary>
    internal static string InternalServerError => nameof (InternalServerError);

    /// <summary>
    ///   Looks up a localized string similar to Token must be of type SimpleWebToken.
    /// </summary>
    internal static string MustBeSWT => nameof (MustBeSWT);

    /// <summary>
    ///   Looks up a localized string similar to Processing token issuance request....
    /// </summary>
    internal static string ProcessingTokenIssuanceRequest => nameof (ProcessingTokenIssuanceRequest);

    /// <summary>
    ///   Looks up a localized string similar to Raw Form POST: {0}.
    /// </summary>
    internal static string RawFormPostFormat => nameof (RawFormPostFormat);

    /// <summary>
    ///   Looks up a localized string similar to RAW SWT = {0}.
    /// </summary>
    internal static string RawSTSFormat => nameof (RawSTSFormat);

    /// <summary>
    ///   Looks up a localized string similar to Unable to resovle key required for signature varification..
    /// </summary>
    internal static string ResolveIssuerKeyFailed => nameof (ResolveIssuerKeyFailed);

    /// <summary>
    ///   Looks up a localized string similar to Resolved issuer security key..
    /// </summary>
    internal static string ResolvingIssuerKey => nameof (ResolvingIssuerKey);

    /// <summary>
    ///   Looks up a localized string similar to Returning issued token.
    /// </summary>
    internal static string ReturningTokenInfo => nameof (ReturningTokenInfo);

    /// <summary>
    ///   Looks up a localized string similar to Saving bootstrap token....
    /// </summary>
    internal static string SavingBootstrapTokenInfo => nameof (SavingBootstrapTokenInfo);

    /// <summary>
    ///   Looks up a localized string similar to Failed to verify the signature of the token..
    /// </summary>
    internal static string SigVerificationFailed => nameof (SigVerificationFailed);

    /// <summary>
    ///   Looks up a localized string similar to Starting WIF's token issuance pipeline.
    /// </summary>
    internal static string StartingTokenIssuancePipeline => nameof (StartingTokenIssuancePipeline);

    /// <summary>
    ///   Looks up a localized string similar to No suitable security token handler found. Please add SWTSecurityTokenHandler in the WIF configuration..
    /// </summary>
    internal static string SuitableTokenHandlerMissing => nameof (SuitableTokenHandlerMissing);

    /// <summary>
    ///   Looks up a localized string similar to Token is expired..
    /// </summary>
    internal static string TokenExpired => nameof (TokenExpired);

    /// <summary>
    ///   Looks up a localized string similar to Token issuance request failed. Error id = {0}.
    /// </summary>
    internal static string TokenIssuanceRequestFailedWithErrorId => nameof (TokenIssuanceRequestFailedWithErrorId);

    /// <summary>
    ///   Looks up a localized string similar to Failed to validate incoming token.
    /// </summary>
    internal static string TokenValidationFailed => nameof (TokenValidationFailed);

    /// <summary>
    ///   Looks up a localized string similar to Unable to extract token from authorization header..
    /// </summary>
    internal static string UnableToExtractToken => nameof (UnableToExtractToken);

    /// <summary>
    ///   Looks up a localized string similar to This issuer is not trusted..
    /// </summary>
    internal static string UntrustedIssuer => nameof (UntrustedIssuer);

    /// <summary>
    ///   Looks up a localized string similar to {0} validated....
    /// </summary>
    internal static string Validated => nameof (Validated);

    /// <summary>
    ///   Looks up a localized string similar to Validating audience restriction....
    /// </summary>
    internal static string ValidatingAudienceInfo => nameof (ValidatingAudienceInfo);

    /// <summary>
    ///   Looks up a localized string similar to Validating issuer signature....
    /// </summary>
    internal static string ValidatingIssuerSignature => nameof (ValidatingIssuerSignature);

    /// <summary>
    ///   Looks up a localized string similar to Validating token lifetime....
    /// </summary>
    internal static string ValidatingTokenLifetime => nameof (ValidatingTokenLifetime);

    /// <summary>
    ///   Looks up a localized string similar to Started SimpleWebToken validation....
    /// </summary>
    internal static string VerificationStarted => nameof (VerificationStarted);

    /// <summary>
    ///   Looks up a localized string similar to WIF is not configured on this service..
    /// </summary>
    internal static string WIFNotConfigured => nameof (WIFNotConfigured);

    /// <summary>
    ///   Looks up a localized string similar to Starting writing SWT....
    /// </summary>
    internal static string WritingToken => nameof (WritingToken);
  }
}
