// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services.ResponsiveDesignSynchronizer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.ResponsiveDesign.Model;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services
{
  /// <summary>
  /// Provides functionality for synchronizing view model and model types of the responsive
  /// design module.
  /// </summary>
  public static class ResponsiveDesignSynchronizer
  {
    /// <summary>Synchronizes the persistent type to the view model.</summary>
    /// <param name="source">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> persistent type.
    /// </param>
    /// <param name="target">
    /// The instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services.MediaQueryViewModel" /> view model type.
    /// </param>
    public static void Synchronize(
      MediaQuery source,
      MediaQueryRule[] sourceRules,
      int pagesCount,
      int templatesCount,
      MediaQueryViewModel target)
    {
      ResponsiveDesignSynchronizer.Synchronize(source, sourceRules, new NavigationTransformation[0], pagesCount, templatesCount, target);
    }

    /// <summary>Synchronizes the persistent type to the view model.</summary>
    /// <param name="source">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> persistent type.
    /// </param>
    /// <param name="target">
    /// The instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services.MediaQueryViewModel" /> view model type.
    /// </param>
    internal static void Synchronize(
      MediaQuery source,
      MediaQueryRule[] sourceRules,
      NavigationTransformation[] sourceNavigationTransformations,
      int pagesCount,
      int templatesCount,
      MediaQueryViewModel target)
    {
      target.Id = source.Id;
      target.Name = source.Name;
      target.PagesCount = pagesCount;
      target.PageTemplatesCount = templatesCount;
      target.Behavior = source.Behavior.ToString();
      target.AdditionalCss = new AdditionalCssViewModel()
      {
        CssFilePath = source.AdditionalCssFilePath
      };
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      target.LayoutTransformations = (LayoutTransformationViewModel[]) scriptSerializer.Deserialize(source.LayoutTransformationsJSON, typeof (LayoutTransformationViewModel[]));
      target.MiniSitePageId = source.MiniSitePageId;
      target.IsActive = source.IsActive;
      List<MediaQueryRuleViewModel> queryRuleViewModelList = new List<MediaQueryRuleViewModel>();
      foreach (MediaQueryRule sourceRule in sourceRules)
      {
        MediaQueryRuleViewModel queryRuleViewModel = new MediaQueryRuleViewModel()
        {
          DeviceTypeName = sourceRule.DeviceTypeName,
          ParentMediaQueryId = source.Id
        };
        queryRuleViewModel.DeviceTypeName = sourceRule.DeviceTypeName;
        queryRuleViewModel.Description = sourceRule.Description;
        queryRuleViewModel.WidthType = sourceRule.WidthType;
        queryRuleViewModel.ExactWidth = sourceRule.ExactWidth;
        queryRuleViewModel.MinWidth = sourceRule.MinWidth;
        queryRuleViewModel.MaxWidth = sourceRule.MaxWidth;
        queryRuleViewModel.HeightType = sourceRule.HeightType;
        queryRuleViewModel.ExactHeight = sourceRule.ExactHeight;
        queryRuleViewModel.MinHeight = sourceRule.MinHeight;
        queryRuleViewModel.MaxHeight = sourceRule.MaxHeight;
        queryRuleViewModel.AspectRatio = sourceRule.AspectRatio;
        queryRuleViewModel.Resolution = sourceRule.Resolution;
        queryRuleViewModel.Orientation = sourceRule.Orientation;
        queryRuleViewModel.IsGrid = sourceRule.IsGrid;
        queryRuleViewModel.IsMonochrome = sourceRule.IsMonochrome;
        queryRuleViewModel.IsManualMediaQuery = sourceRule.IsManualMediaQuery;
        queryRuleViewModel.MediaQueryRule = sourceRule.ResultingRule;
        queryRuleViewModelList.Add(queryRuleViewModel);
      }
      target.Rules = queryRuleViewModelList.ToArray();
      List<NavigationTransformationViewModel> transformationViewModelList = new List<NavigationTransformationViewModel>();
      foreach (NavigationTransformation navigationTransformation in sourceNavigationTransformations)
        transformationViewModelList.Add(new NavigationTransformationViewModel()
        {
          CssClasses = navigationTransformation.CssClasses,
          TransformationName = navigationTransformation.TransformationName
        });
      target.NavigationTransformations = transformationViewModelList;
    }

    /// <summary>
    /// Synchronizes the view model type to the persistent type.
    /// </summary>
    /// <param name="source">
    /// The instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services.MediaQueryViewModel" /> view model type.
    /// </param>
    /// <param name="target">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> persistent type.
    /// </param>
    public static void Synchronize(MediaQueryViewModel source, MediaQuery target)
    {
      target.Name = source.Name;
      target.Behavior = (ResponsiveDesignBehavior) Enum.Parse(typeof (ResponsiveDesignBehavior), source.Behavior);
      if (source.AdditionalCss != null)
        target.AdditionalCssFilePath = source.AdditionalCss.CssFilePath;
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      target.LayoutTransformationsJSON = scriptSerializer.Serialize((object) source.LayoutTransformations);
      target.MiniSitePageId = source.MiniSitePageId;
      target.IsActive = source.IsActive;
    }

    /// <summary>
    /// Synchronizes the view model type to the persistent type.
    /// </summary>
    /// <param name="mediaQueryId">Id of the parent media query.</param>
    /// <param name="source">
    /// The instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services.MediaQueryRuleViewModel" /> view model type.
    /// </param>
    /// <param name="target">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> persistent type.
    /// </param>
    public static void Synchronize(
      Guid mediaQueryId,
      MediaQueryRuleViewModel source,
      MediaQueryRule target)
    {
      target.ParentMediaQueryId = mediaQueryId;
      target.DeviceTypeName = source.DeviceTypeName;
      target.Description = source.Description;
      target.WidthType = source.WidthType;
      target.ExactWidth = source.ExactWidth;
      target.MinWidth = source.MinWidth;
      target.MaxWidth = source.MaxWidth;
      target.HeightType = source.HeightType;
      target.ExactHeight = source.ExactHeight;
      target.MinHeight = source.MinHeight;
      target.MaxHeight = source.MaxHeight;
      target.AspectRatio = source.AspectRatio;
      target.Resolution = source.Resolution;
      target.Orientation = source.Orientation;
      target.IsGrid = source.IsGrid;
      target.IsMonochrome = source.IsMonochrome;
      target.IsManualMediaQuery = source.IsManualMediaQuery;
      target.ResultingRule = source.MediaQueryRule;
    }

    public static void Synchronize(
      MediaQueryLinkViewModel source,
      MediaQueryLink target,
      MediaQueryLinkType linkType)
    {
      target.ItemId = source.ItemId;
      target.ItemType = source.ItemType;
      target.LinkType = linkType;
      if (linkType == MediaQueryLinkType.Selected)
      {
        target.MediaQueries.Clear();
        foreach (MediaQueryViewModel mediaQuery1 in source.MediaQueries)
        {
          MediaQuery mediaQuery2 = ResponsiveDesignManager.GetManager().GetMediaQuery(mediaQuery1.Id);
          target.MediaQueries.Add(mediaQuery2);
        }
      }
      else
        target.MediaQueries.Clear();
    }
  }
}
