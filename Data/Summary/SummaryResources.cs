// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Summary.SummaryResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Data.Summary
{
  /// <summary>Localization messages for the summary parser</summary>
  [ObjectInfo("SummaryResources", ResourceClassId = "SummaryResources")]
  public class SummaryResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Data.Summary.SummaryResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public SummaryResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Data.Summary.SummaryResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public SummaryResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Summary</summary>
    [ResourceEntry("SummaryResourcesTitle", Description = "The title of this class.", LastModified = "2009/12/03", Value = "Summary")]
    public string SummaryResourcesTitle => this[nameof (SummaryResourcesTitle)];

    /// <summary>Summary</summary>
    [ResourceEntry("SummaryResourcesTitlePlaural", Description = "The title of this class.", LastModified = "2010/10/18", Value = "Summaries")]
    public string SummaryResourcesTitlePlaural => this[nameof (SummaryResourcesTitlePlaural)];

    /// <summary>
    /// Contains localizable resources for help information such as UI elements descriptions, usage explanations, FAQ and etc.
    /// </summary>
    [ResourceEntry("SummaryResourcesDescription", Description = "The description of this class.", LastModified = "2009/12/03", Value = "Contains localizable resources for summary parser.")]
    public string SummaryResourcesDescription => this[nameof (SummaryResourcesDescription)];

    /// <summary>Message: Invalid String Format</summary>
    [ResourceEntry("InvalidStringFormat", Description = "", LastModified = "2009/12/03", Value = "Invalid String Format")]
    public string InvalidStringFormat => this[nameof (InvalidStringFormat)];
  }
}
