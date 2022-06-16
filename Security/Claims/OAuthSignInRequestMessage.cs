// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.OAuthSignInRequestMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.IdentityModel.Services;
using System.IO;
using System.Text;

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>
  /// Sign in request message that uses the OAuth 2.0 protocol
  /// </summary>
  public class OAuthSignInRequestMessage : WSFederationMessage
  {
    public OAuthSignInRequestMessage(Uri baseUrl, string realm, string reply, bool deflate)
      : base(baseUrl, "oauth2.0")
    {
      if (string.IsNullOrEmpty(realm) && string.IsNullOrEmpty(reply))
        throw new WSFederationMessageException("ID3204");
      if (!string.IsNullOrEmpty(realm))
        this.SetUriParameter(nameof (realm), realm);
      if (!string.IsNullOrEmpty(reply))
        this.SetParameter("redirect_uri", reply);
      this.Deflate = deflate;
      this.RemoveParameter("wa");
    }

    public string Realm
    {
      get => this.GetParameter("realm");
      set
      {
        if (string.IsNullOrEmpty(value))
          this.RemoveParameter("realm");
        else
          this.SetUriParameter("realm", value);
      }
    }

    public string Reply
    {
      get => this.GetParameter("redirect_uri");
      set
      {
        if (string.IsNullOrEmpty(value))
          this.RemoveParameter("redirect_uri");
        else
          this.SetParameter("redirect_uri", value);
      }
    }

    public bool Deflate
    {
      get => "true".Equals(this.GetParameter("deflate"), StringComparison.OrdinalIgnoreCase);
      set
      {
        if (value)
          this.SetParameter("deflate", "true");
        else
          this.RemoveParameter("deflate");
      }
    }

    public string RequestUrl
    {
      get
      {
        StringBuilder sb = new StringBuilder(128);
        using (StringWriter writer = new StringWriter(sb, (IFormatProvider) CultureInfo.InvariantCulture))
        {
          this.Write((TextWriter) writer);
          return sb.ToString();
        }
      }
    }

    public override void Write(TextWriter writer)
    {
      if (writer == null)
        throw new ArgumentNullException(nameof (writer));
      this.Validate();
      writer.Write(this.WriteQueryString());
    }
  }
}
