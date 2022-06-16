// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Configuration.ElmahSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services.Configuration
{
  [ObjectInfo(typeof (ConfigDescriptions), Description = "UIElmahConfigDescriptions", Title = "UIElmahConfigDescriptions")]
  public class ElmahSettings : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Configuration.ElmahSettings" /> class.
    /// </summary>
    /// <param name="parent">The parent.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ElmahSettings(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether to turn the elmah logging on or off.
    /// </summary>
    /// <value>
    ///   If the value is <c>true</c> all the exceptions in Sitefinity will be logged into Elmah,
    ///   otherwise if <c>false</c> the default Ent Lib logging is used.
    /// </value>
    [ConfigurationProperty("isElmahLoggingTurnedOn", DefaultValue = false)]
    [DescriptionResource(typeof (ConfigDescriptions), "TurnElmahLogging")]
    public virtual bool IsElmahLoggingTurnedOn
    {
      get => (bool) this["isElmahLoggingTurnedOn"];
      set => this["isElmahLoggingTurnedOn"] = (object) value;
    }
  }
}
