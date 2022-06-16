// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Pages.Model.ObjectDataExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.PropertyLoaders;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model.PropertyLoaders;

namespace Telerik.Sitefinity.Pages.Model
{
  /// <summary>
  /// Extension methods for the <see cref="T:Telerik.Sitefinity.Pages.Model.ObjectData" /> object.
  /// </summary>
  public static class ObjectDataExtensions
  {
    /// <summary>
    /// Returns the properties for the given language. If null is specified for the language, the current context language is used.
    /// If a fallback is specified, then the properties will be loaded from the first found language (Valid for backward compatibility before 7.3).
    /// </summary>
    /// <param name="data">The object data from which to retrieve the properties.</param>
    /// <param name="language">The language by which to filter.</param>
    /// <param name="fallbackToAnyLanguage">Determines weather to use fallback logic when retrieving the properties.</param>
    /// <returns>The filtered properties.</returns>
    public static IEnumerable<ControlProperty> GetProperties(
      this ObjectData data,
      CultureInfo language = null,
      bool fallbackToAnyLanguage = false)
    {
      return PropertyLoader.GetLoader(data).GetProperties(language, fallbackToAnyLanguage);
    }

    /// <summary>
    /// Returns the properties for the given language. If null is specified for the language, the current context language is used.
    /// If a fallback is specified, then the properties will be loaded from the first found language (Valid for backward compatibility before 7.3).
    /// </summary>
    /// <param name="data">The object data from which to retrieve the properties.</param>
    /// <param name="fallbackToAnyLanguage">Determines weather to use fallback logic when retrieving the properties.</param>
    /// <returns>The filtered properties.</returns>
    public static IEnumerable<ControlProperty> GetProperties(
      this ObjectData data,
      bool fallbackToAnyLanguage = true)
    {
      return data.GetProperties((CultureInfo) null, fallbackToAnyLanguage);
    }

    /// <summary>
    /// Sets the property persistence strategy of the <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> object.
    /// </summary>
    /// <param name="data">The <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> instance.</param>
    public static void SetPersistanceStrategy(this ControlData data)
    {
      if (!data.IsTranslatable)
      {
        data.Strategy = PropertyPersistenceStrategy.NotTranslatable;
      }
      else
      {
        if (data.PersonalizationMasterId != Guid.Empty)
        {
          SitefinityOAContext context = OpenAccessContextBase.GetContext((object) data) as SitefinityOAContext;
          if (data != null)
          {
            ControlData controlData = context.GetAll<ControlData>().FirstOrDefault<ControlData>((Expression<Func<ControlData, bool>>) (x => x.Id == data.PersonalizationMasterId)) ?? context.GetDirtyItems().OfType<ControlData>().FirstOrDefault<ControlData>((Func<ControlData, bool>) (x => x.Id == data.PersonalizationMasterId));
            if (controlData != null)
            {
              data.Strategy = controlData.Strategy;
              return;
            }
          }
        }
        if (data.TrySetPageControlStrategy() || data.TrySetTemplateControlStrategy() || data.TrySetFormControlStrategy())
          return;
        data.Strategy = PropertyPersistenceStrategy.NotTranslatable;
      }
    }

    /// <summary>Increments the version of the control.</summary>
    /// <param name="data">The control.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void IncrementVersion(this ObjectData data) => data.IncrementVersion((CultureInfo) null);

    /// <summary>Increments the multilingual version of the control.</summary>
    /// <param name="data">The control.</param>
    /// <param name="culture">Increment version for specific culture.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void IncrementVersion(this ObjectData data, CultureInfo culture) => data.IncreaseMultilingualVersion(culture);

    internal static void OverrideFrom(this ControlData overridenData, ControlData baseData)
    {
      overridenData.OverridedProperties = (IList<ControlProperty>) baseData.GetProperties(true).ToList<ControlProperty>();
      overridenData.OverridenStrategy = new PropertyPersistenceStrategy?(baseData.Strategy);
      overridenData.OverridenIsPersonalized = new bool?(baseData.IsPersonalized);
    }

    internal static void ClearOverrides(this ControlData overridenData)
    {
      overridenData.OverridedProperties = (IList<ControlProperty>) null;
      overridenData.OverridenStrategy = new PropertyPersistenceStrategy?();
      overridenData.OverridenIsPersonalized = new bool?();
    }

    private static bool TrySetPageControlStrategy(this ControlData data)
    {
      if (data is PageDraftControl pageDraftControl && pageDraftControl.Page != null && pageDraftControl.Page.ParentPage != null && pageDraftControl.Page.ParentPage.NavigationNode != null && pageDraftControl.Page.ParentPage.NavigationNode.LocalizationStrategy == LocalizationStrategy.Synced)
      {
        using (SiteRegion.FromSiteMapRoot(pageDraftControl.Page.ParentPage.NavigationNode.RootNodeId))
        {
          if (((IEnumerable<CultureInfo>) pageDraftControl.Page.ParentPage.NavigationNode.AvailableCultures).Count<CultureInfo>() > 1)
          {
            pageDraftControl.Strategy = PropertyPersistenceStrategy.Translatable;
            return true;
          }
        }
      }
      if (data is PageControl pageControl && pageControl.Page != null && pageControl.Page.NavigationNode != null && pageControl.Page.NavigationNode.LocalizationStrategy == LocalizationStrategy.Synced)
      {
        using (SiteRegion.FromSiteMapRoot(pageControl.Page.NavigationNode.RootNodeId))
        {
          if (((IEnumerable<CultureInfo>) pageControl.Page.NavigationNode.AvailableCultures).Count<CultureInfo>() > 1)
          {
            pageControl.Strategy = PropertyPersistenceStrategy.Translatable;
            return true;
          }
        }
      }
      return false;
    }

    private static bool TrySetTemplateControlStrategy(this ControlData data)
    {
      switch (data)
      {
        case TemplateControl templateControl when templateControl.Page != null && templateControl.Page.GetAvailableLanguagesIgnoringContext().Count<CultureInfo>() > 1:
          data.Strategy = PropertyPersistenceStrategy.Translatable;
          return true;
        case TemplateDraftControl templateDraftControl when templateDraftControl.Page != null && templateDraftControl.Page.ParentTemplate != null && templateDraftControl.Page.ParentTemplate.GetAvailableLanguagesIgnoringContext().Count<CultureInfo>() > 1:
          data.Strategy = PropertyPersistenceStrategy.Translatable;
          return true;
        default:
          return false;
      }
    }

    private static bool TrySetFormControlStrategy(this ControlData data)
    {
      switch (data)
      {
        case FormControl formControl when formControl.Form != null && formControl.Form.GetAvailableLanguagesIgnoringContext().Count<CultureInfo>() > 1:
          data.Strategy = PropertyPersistenceStrategy.Translatable;
          return true;
        case FormDraftControl formDraftControl when formDraftControl.Form != null && formDraftControl.Form.ParentForm != null && formDraftControl.Form.ParentForm.GetAvailableLanguagesIgnoringContext().Count<CultureInfo>() > 1:
          data.Strategy = PropertyPersistenceStrategy.Translatable;
          return true;
        default:
          return false;
      }
    }
  }
}
