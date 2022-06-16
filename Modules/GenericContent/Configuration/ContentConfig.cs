// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Configuration.ContentConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Modules.GenericContent.Configuration
{
  /// <summary>Defines Generic Content configuration settings.</summary>
  [DescriptionResource(typeof (ConfigDescriptions), "ContentConfig")]
  public class ContentConfig : ContentModuleConfigBase
  {
    /// <summary>Initializes the default providers.</summary>
    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "OpenAccessDataProvider",
        Title = "Default Content",
        Description = "A provider that stores content data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessContentProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "/Generic_Content"
          }
        }
      });
    }

    /// <summary>Initializes the default views.</summary>
    protected override void InitializeDefaultViews(
      ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
    {
      contentViewControls.Add(ContentDefinitions.DefineContentBackendContentView((ConfigElement) contentViewControls));
      contentViewControls.Add(CommentsDefinitions.DefineCommentsBackendContentView((ConfigElement) contentViewControls, ContentDefinitions.BackendCommentsDefinitionName, this.DefaultProvider, typeof (ContentManager), typeof (ContentResources).Name));
      contentViewControls.Add(CommentsDefinitions.DefineCommentsFrontendView((ConfigElement) contentViewControls, ContentDefinitions.FrontendCommentsDefinitionName, this.DefaultProvider, typeof (ContentManager)));
      contentViewControls.Add(ContentDefinitions.DefineGenericContentFrontendView((ConfigElement) contentViewControls));
    }
  }
}
