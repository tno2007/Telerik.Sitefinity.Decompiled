// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Providers.FileLicenseProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Licensing.Providers
{
  internal class FileLicenseProvider : ILicenseProvider
  {
    private const string PathKeyName = "licenseFilePath";

    public void Initialize(NameValueCollection parameters) => this.Parameters = parameters;

    public string LoadLicenseData()
    {
      if (TestContext.Testing && TestContext.Attributes["LicenseState.LoadLicenseDataFromFile"] == "true")
        return TestContext.GetMock<string>("LicenseState.LoadLicenseDataFromFileResult");
      string licenseFilePath = this.LicenseFilePath;
      return !File.Exists(licenseFilePath) ? (string) null : File.ReadAllText(licenseFilePath, (Encoding) new UTF8Encoding(false, true));
    }

    public void SaveLicense(string licenseData) => File.WriteAllText(this.LicenseFilePath, licenseData, (Encoding) new UTF8Encoding(false, true));

    private string LicenseFilePath => this.Parameters != null && ((IEnumerable<string>) this.Parameters.AllKeys).Any<string>((Func<string, bool>) (x => x == "licenseFilePath")) ? HostingEnvironment.MapPath(this.Parameters["licenseFilePath"]) : this.DefaultLicenseFilePath;

    private string DefaultLicenseFilePath => HostingEnvironment.MapPath("~/App_Data/Sitefinity/Sitefinity.lic");

    private NameValueCollection Parameters { get; set; }
  }
}
