// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.TimeZoneUISettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services
{
  [ObjectInfo(typeof (ConfigDescriptions), Description = "UITimeZoneConfigDescriptions", Title = "UITimeZoneConfigDescriptions")]
  public class TimeZoneUISettings : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.TimeZoneUISettings" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public TimeZoneUISettings(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name of the time zone.</summary>
    /// <value>The name of the time zone.</value>
    [ConfigurationProperty("timeZoneId")]
    [DescriptionResource(typeof (ConfigDescriptions), "TimeZoneId")]
    [TypeConverter(typeof (TimeZoneInfoTypeConverter))]
    [DataMember]
    public virtual TimeZoneInfo CurrentTimeZoneInfo
    {
      get => (TimeZoneInfo) this["timeZoneId"];
      set => this["timeZoneId"] = (object) value;
    }

    /// <summary>
    /// Indicates whether to user user browser settings for calculating dates.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "UserBrowserSettingsForCalculatingDates")]
    [ConfigurationProperty("userBrowserSettingsForCalculatingDates", DefaultValue = "true", IsRequired = true)]
    [DataMember]
    public virtual bool UserBrowserSettingsForCalculatingDates
    {
      get => (bool) this["userBrowserSettingsForCalculatingDates"];
      set => this["userBrowserSettingsForCalculatingDates"] = (object) value;
    }
  }
}
