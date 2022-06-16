// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.ResponsiveDesignSettingsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration;
using Telerik.Sitefinity.ResponsiveDesign.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class ResponsiveDesignSettingsService : IResponsiveDesignSettingsService
  {
    private const string ResponsiveDesignRestartReason = "ResponsiveDesignSettingsChange";

    /// <summary>
    /// Gets the navigation transformations and returns it in JSON format.
    /// </summary>
    public IEnumerable<NavTransformationElementViewModel> GetNavigationTransformationElements()
    {
      ServiceUtility.RequestBackendUserAuthentication();
      ConfigElementDictionary<string, NavigationTransformationElement> navigationTransformations = Config.Get<ResponsiveDesignConfig>().NavigationTransformations;
      ServiceUtility.DisableCache();
      return navigationTransformations.Values.Select<NavigationTransformationElement, NavTransformationElementViewModel>((Func<NavigationTransformationElement, NavTransformationElementViewModel>) (t => new NavTransformationElementViewModel(t)));
    }

    /// <summary>Gets the navigation transformation element.</summary>
    /// <param name="name">The name.</param>
    public NavTransformationElementViewModel GetNavigationTransformationElement(
      string name)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      ServiceUtility.DisableCache();
      return new NavTransformationElementViewModel(Config.Get<ResponsiveDesignConfig>().NavigationTransformations[name]);
    }

    /// <summary>Saves the navigation transformation element.</summary>
    /// <param name="name">The name.</param>
    /// <param name="transformation">The transformation.</param>
    public NavTransformationElementViewModel SaveNavigationTransformationElement(
      string name,
      NavTransformationElementViewModel transformation,
      bool isNew)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      ConfigManager manager1 = ConfigManager.GetManager();
      ResponsiveDesignConfig section = manager1.GetSection<ResponsiveDesignConfig>();
      ConfigElementDictionary<string, NavigationTransformationElement> navigationTransformations1 = section.NavigationTransformations;
      int num = navigationTransformations1.Elements.Where<NavigationTransformationElement>((Func<NavigationTransformationElement, bool>) (t => t.Name == transformation.Name)).Count<NavigationTransformationElement>();
      if (num > 0 & isNew)
        throw new Exception(Res.Get<Labels>().DuplicateSetting);
      if (num > 0 && !isNew && name != transformation.Name)
        throw new Exception(Res.Get<Labels>().DuplicateSetting);
      NavigationTransformationElement element = section.NavigationTransformations[name];
      string str = (string) null;
      bool flag = false;
      if (element == null)
      {
        element = new NavigationTransformationElement((ConfigElement) navigationTransformations1);
        flag = true;
      }
      else
        str = element.ResourceClassId;
      element.Name = transformation.Name;
      element.ResourceClassId = str;
      element.Title = transformation.Title;
      element.TransformationCss = transformation.Css;
      element.IsActive = transformation.IsActive;
      if (flag)
        navigationTransformations1.Add(element);
      manager1.SaveSection((ConfigSection) section, true);
      if (name != element.Name)
      {
        foreach (ConfigElement provider in (ConfigElementCollection) section.Providers)
        {
          ResponsiveDesignManager manager2 = ResponsiveDesignManager.GetManager(provider[nameof (name)].ToString());
          IQueryable<NavigationTransformation> navigationTransformations2 = manager2.GetNavigationTransformations();
          Expression<Func<NavigationTransformation, bool>> predicate = (Expression<Func<NavigationTransformation, bool>>) (t => t.TransformationName == name);
          foreach (NavigationTransformation navigationTransformation in navigationTransformations2.Where<NavigationTransformation>(predicate).ToArray<NavigationTransformation>())
            navigationTransformation.TransformationName = element.Name;
          using (new FileSystemModeRegion())
            manager2.SaveChanges();
        }
      }
      SystemManager.RestartApplication(OperationReason.FromKey("ResponsiveDesignSettingsChange"));
      ServiceUtility.DisableCache();
      return transformation;
    }

    /// <summary>Deletes the navigation transformation element.</summary>
    /// <param name="name">The name.</param>
    public bool DeleteNavigationTransformationElement(string name)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      ConfigManager manager = ConfigManager.GetManager();
      ResponsiveDesignConfig section = manager.GetSection<ResponsiveDesignConfig>();
      section.NavigationTransformations.Remove(name);
      manager.SaveSection((ConfigSection) section, true);
      SystemManager.RestartApplication(OperationReason.FromKey("ResponsiveDesignSettingsChange"));
      ServiceUtility.DisableCache();
      return true;
    }

    /// <summary>Change navigation transformation status.</summary>
    /// <param name="name">The name.</param>
    public bool ChangeStatus(string name)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      ConfigManager manager = ConfigManager.GetManager();
      ResponsiveDesignConfig section = manager.GetSection<ResponsiveDesignConfig>();
      NavigationTransformationElement navigationTransformation = section.NavigationTransformations[name];
      navigationTransformation.IsActive = !navigationTransformation.IsActive;
      manager.SaveSection((ConfigSection) section, true);
      SystemManager.RestartApplication(OperationReason.FromKey("ResponsiveDesignSettingsChange"));
      ServiceUtility.DisableCache();
      return true;
    }
  }
}
