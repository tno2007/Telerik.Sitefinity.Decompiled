// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Help
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// Represents string resources for descrptions, tooltips, FAQs and etc.
  /// </summary>
  [ObjectInfo("Help", ResourceClassId = "Help")]
  public sealed class Help : Resource
  {
    /// <summary>Help</summary>
    [ResourceEntry("HelpTitle", Description = "The title of this class.", LastModified = "2009/04/30", Value = "Help")]
    public string HelpTitle => this[nameof (HelpTitle)];

    /// <summary>Help</summary>
    [ResourceEntry("HelpTitlePlural", Description = "The title plural of this class.", LastModified = "2009/04/30", Value = "Help")]
    public string HelpTitlePlural => this[nameof (HelpTitlePlural)];

    /// <summary>
    /// Contains localizable resources for help information such as UI elements descriptions, usage explanations, FAQ and etc.
    /// </summary>
    [ResourceEntry("HelpDescription", Description = "The description of this class.", LastModified = "2009/04/30", Value = "Contains localizable resources for help information such as UI elements descriptions, usage explanations, FAQ and etc.")]
    public string HelpDescription => this[nameof (HelpDescription)];

    /// <summary>
    /// Description: Resource data provider that uses an XML file to store and retrieve resource data.
    /// </summary>
    [ResourceEntry("ResourcesXmlDataProviderDescription", Description = "Description of XML data provider for localizable resources.", LastModified = "2012/01/05", Value = "Resource data provider that uses an XML file to store and retrieve resource data.")]
    public string ResourcesXmlDataProviderDescription => this[nameof (ResourcesXmlDataProviderDescription)];

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Help" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public Help()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Help" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public Help(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }
  }
}
