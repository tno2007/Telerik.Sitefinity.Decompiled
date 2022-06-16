// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContextualHelp.ContextualHelpResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.ContextualHelp
{
  [ObjectInfo("ContextualHelpResources", ResourceClassId = "ContextualHelpResources", TitlePlural = "ContextualHelpTitle")]
  internal class ContextualHelpResources : Resource
  {
    /// <summary>Gets the Service API Title</summary>
    /// <value>API services</value>
    [ResourceEntry("ContextualHelpTitle", Description = "Gets the Contextual Help Title", LastModified = "2019/06/03", Value = "Contextual help")]
    public string ContextualHelpTitle => this[nameof (ContextualHelpTitle)];

    [ResourceEntry("EnabledTitle", Description = "Description of the enabled title.", LastModified = "2019/06/03", Value = "Enabled")]
    public string EnabledTitle => this[nameof (EnabledTitle)];

    [ResourceEntry("EnabledDescription", Description = "Description about the contextual help enabled description.", LastModified = "2019/06/03", Value = "Enables or disables the contextual help")]
    public string EnabledDescription => this[nameof (EnabledDescription)];

    [ResourceEntry("ContextualHelpResourcesTitle", Description = "Phrase: Contextual Help Resources", LastModified = "2019/06/24", Value = "Contextual Help Resources")]
    public string ContextualHelpResourcesTitle => this[nameof (ContextualHelpResourcesTitle)];

    [ResourceEntry("ContextualHelpResourcesDescription", Description = "Phrase: Contains localizable resources for the Contextual Help user interface.", LastModified = "2019/06/24", Value = "Contains localizable resources for the Contextual Help user interface.")]
    public string ContextualHelpResourcesDescription => this[nameof (ContextualHelpResourcesDescription)];
  }
}
