// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SWT.SimpleWebToken
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;
using System.Security.Claims;

namespace Telerik.Sitefinity.Security.Claims.SWT
{
  /// <summary>An Access Token format that can be used within WRAP</summary>
  /// <see cref="!:http://wiki.oauth.net/w/page/12238537/OAuth%20WRAP" />
  public class SimpleWebToken : SecurityToken
  {
    private string tokenId;
    private string issuer;
    private string audience;
    private DateTime validFrom;
    private DateTime validTo;
    private IList<Claim> claims;

    /// <summary>
    /// Creates a new instance of SimpleWebToken and optionally parses it
    /// </summary>
    /// <param name="rawToken">URL decoded SWT</param>
    /// <param name="autoParse">true if parsing is required, otherwise false.</param>
    public SimpleWebToken(string rawToken)
    {
      this.RawToken = rawToken;
      this.EnsureProperties();
    }

    public string RawToken { get; private set; }

    public override string Id => this.tokenId;

    public override ReadOnlyCollection<SecurityKey> SecurityKeys => new List<SecurityKey>().AsReadOnly();

    public override DateTime ValidFrom => this.validFrom;

    public override DateTime ValidTo => this.validTo;

    public string Issuer => this.issuer;

    public string Audience => this.audience;

    public IList<Claim> Claims => this.claims;

    private void EnsureProperties()
    {
      SWTParser swtParser = new SWTParser(this.RawToken);
      this.issuer = swtParser.Issuer;
      this.audience = swtParser.Audience;
      this.validFrom = swtParser.ValidFrom;
      this.validTo = swtParser.ExpiresOn;
      this.claims = (IList<Claim>) swtParser.Claims;
      this.tokenId = swtParser.TokenId;
    }
  }
}
