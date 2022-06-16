// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.CertificateUtil
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Security.Cryptography.X509Certificates;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>
  /// A utility class which helps to retrieve an x509 certificate
  /// </summary>
  [Obsolete("This class will be removed in a future Sitefinity version. Use System.Security.Criptography.X509Store instead")]
  public class CertificateUtil
  {
    [Obsolete("This method will be removed in a future Sitefinity version. Use System.Security.Criptography.X509Store.Certificates.Find(X509FindType findType, object findValue, bool validOnly) instead")]
    public static X509Certificate2 GetCertificate(
      StoreName name,
      StoreLocation location,
      string subjectName)
    {
      X509Certificate2 certificate = (X509Certificate2) null;
      if (!string.IsNullOrWhiteSpace(subjectName))
      {
        X509Store x509Store = new X509Store(name, location);
        x509Store.Open(OpenFlags.ReadOnly);
        X509Certificate2Collection certificate2Collection = x509Store.Certificates.Find(X509FindType.FindBySubjectName, (object) subjectName, false);
        if (certificate2Collection.Count > 0)
        {
          certificate = certificate2Collection[0];
          if (certificate2Collection.Count > 1)
            Log.Trace("More than one IdentityServer signing certificate with subject name {0} found. System will be using certificate with thumbprint {1}", (object) subjectName, (object) certificate2Collection[0].Thumbprint);
        }
        else
          Log.Error("Unable to find the custom signing certificate with subject name {0}.", (object) subjectName);
        x509Store.Close();
      }
      return certificate;
    }
  }
}
