// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.DateFormatElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Data.Configuration
{
  /// <summary>Represents date format configuration.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "DateFormatElementDescription", Title = "DateFormatElementCaption")]
  public class DateFormatElement : ConfigElement
  {
    /// <summary>
    /// Initializes new isntance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public DateFormatElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the date format.</summary>
    /// <value>The date format.</value>
    [ConfigurationProperty("dateFormat", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DateFormatValueDescription", Title = "DateFormatValueTitle")]
    public string DateFormat
    {
      get => (string) this["dateFormat"];
      set => this["dateFormat"] = (object) value;
    }

    /// <summary>Gets or sets the content type of the sort expression</summary>
    /// <value>The license service URL.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnabledDescription", Title = "EnabledTitle")]
    [ConfigurationProperty("enabled", DefaultValue = false, IsKey = false, IsRequired = false)]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Props
    {
      public const string DateFormat = "dateFormat";
      public const string Enabled = "enabled";
    }
  }
}
