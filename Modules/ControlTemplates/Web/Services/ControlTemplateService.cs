// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ControlTemplates.Web.Services.ControlTemplateService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentViewAttributes;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Multisite.Web.Services.ViewModel;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.NewImplementation;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search.Data;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;
using Telerik.Sitefinity.Versioning.Web.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.ControlTemplates.Web.Services
{
  /// <summary>
  /// The WCF web service that is used to work with control templates.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class ControlTemplateService : IControlTemplateService
  {
    private Dictionary<UserFriendlyDataType, string> defaultTemplates = new Dictionary<UserFriendlyDataType, string>()
    {
      {
        UserFriendlyDataType.ShortText,
        "<%#: Eval(\"{0}\")%>"
      },
      {
        UserFriendlyDataType.LongText,
        "<%# ControlUtilities.Sanitize(Eval(\"{0}\"))%>"
      },
      {
        UserFriendlyDataType.Choices,
        "<%# Eval(\"{0}\")%>"
      },
      {
        UserFriendlyDataType.MultipleChoice,
        "<%# Eval(\"{0}\")%>"
      }
    };
    private Dictionary<UserFriendlyDataType, string> userProfileTemplates = new Dictionary<UserFriendlyDataType, string>()
    {
      {
        UserFriendlyDataType.ShortText,
        "<sf:TextField ID=\"{0}\" runat=\"server\" DisplayMode=\"Read\" DataFieldName=\"{0}\" Value='<%#: Eval(\"{0}\")%>' />"
      },
      {
        UserFriendlyDataType.LongText,
        "<sf:HtmlField ID=\"{0}\" runat=\"server\" DisplayMode=\"Read\" DataFieldName=\"{0}\" Value='<%# ControlUtilities.Sanitize(Eval(\"{0}\"))%>' />"
      },
      {
        UserFriendlyDataType.Image,
        "<sf:ImageField ID=\"{0}\" runat=\"server\" DataFieldType=\"Telerik.Sitefinity.Model.ContentLinks.ContentLink\" DisplayMode=\"Read\" ShowDeleteImageButton=\"false\" DataFieldName=\"{0}\" />"
      }
    };

    /// <summary>
    /// Gets the single control template and returns it in JSON format.
    /// </summary>
    /// <param name="id">Id of the control template that ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider that is to be used to retrieve the control template.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the control template to be retrieved.
    /// </returns>
    public ItemContext<ControlPresentation> GetControlTemplate(
      string id,
      string providerName,
      string VersionId)
    {
      return this.GetControlTemplateInternal(id, providerName, VersionId);
    }

    /// <summary>
    /// Gets the single control template and returns it in XML format.
    /// </summary>
    /// <param name="id">Id of the control template that ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider that is to be used to retrieve the control template.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the control template to be retrieved.
    /// </returns>
    public ItemContext<ControlPresentation> GetControlTemplateInXml(
      string id,
      string providerName,
      string VersionId)
    {
      return this.GetControlTemplateInternal(id, providerName, VersionId);
    }

    /// <summary>
    /// Gets the collection of all control templates and returns the result in JSON format.
    /// </summary>
    /// <param name="providerName">Name of the provider to be used to get the content items.</param>
    /// <param name="sortExpression">Sort expression used to order the control templates in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="templateFilter">Specific filter for widget templates.</param>
    /// <returns></returns>
    public CollectionContext<ControlTemplateViewModel> GetControlTemplates(
      string providerName,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string templateFilter)
    {
      return this.GetControlTemplatesInternal(providerName, sortExpression, skip, take, filter, templateFilter);
    }

    /// <summary>
    /// Gets the collection of all control templates and returns the result in XML format.
    /// </summary>
    /// <param name="providerName">Name of the provider to be used to get the content items.</param>
    /// <param name="sortExpression">Sort expression used to order the control templates in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="templateFilter">Specific filter for widget templates.</param>
    /// <returns></returns>
    public CollectionContext<ControlTemplateViewModel> GetControlTemplatesInXml(
      string providerName,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string templateFilter)
    {
      return this.GetControlTemplatesInternal(providerName, sortExpression, skip, take, filter, templateFilter);
    }

    /// <summary>
    /// Saves the control template and returns the saved control template in JSON format. If a control template
    /// with the specified id exists the it will be updated; otherwise new control template will
    /// be created.
    /// </summary>
    /// <param name="template">The control template to be saved.</param>
    /// <param name="id">The id of the control template to be saved.</param>
    /// <param name="providerName">Name of the provider that is to be used to save the control template.</param>
    /// <returns>
    /// An instance of ItemContext that contains the control template that was saved.
    /// </returns>
    public ItemContext<ControlPresentation> SaveControlTemplate(
      ItemContext<ControlPresentation> context,
      string id,
      string providerName)
    {
      return this.SaveControlTemplateInternal(context, id, providerName);
    }

    /// <summary>
    /// Saves the control template and returns the saved control template in XML format. If a control template
    /// with the specified id exists the it will be updated; otherwise new control template will
    /// be created.
    /// </summary>
    /// <param name="template">The control template to be saved.</param>
    /// <param name="id">The id of the control template to be saved.</param>
    /// <param name="providerName">Name of the provider that is to be used to save the control template.</param>
    /// <returns>
    /// An instance of ItemContext that contains the control template that was saved.
    /// </returns>
    public ItemContext<ControlPresentation> SaveControlTemplateInXml(
      ItemContext<ControlPresentation> context,
      string id,
      string providerName)
    {
      return this.SaveControlTemplateInternal(context, id, providerName);
    }

    /// <summary>
    /// Deletes the control template and returns true if the control template has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="id">Id of the control template to be deleted.</param>
    /// <param name="providerName">Name of the provider to be used when deleting the control template.</param>
    /// <returns></returns>
    public bool DeleteControlTemplate(string id, string providerName) => this.DeleteControlTemplateInternal(id, providerName);

    /// <summary>
    /// Deletes the control template and returns true if the control template has been deleted; otherwise false.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="id">Id of the control template to be deleted.</param>
    /// <param name="providerName">Name of the provider to be used when deleting the control template.</param>
    /// <returns></returns>
    public bool DeleteControlTemplateInXml(string id, string providerName) => this.DeleteControlTemplateInternal(id, providerName);

    /// <summary>
    /// Restores the control template to its default markup from the embedded resource.
    /// </summary>
    /// <param name="context">The control template to be restored.</param>
    /// <param name="id">The id of the control template.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns>
    /// An instance of the control template that was restored to the default one
    /// </returns>
    public ItemContext<ControlPresentation> RestoreControlTemplate(
      ItemContext<ControlPresentation> context,
      string id,
      string providerName)
    {
      return this.RestoreControlTemplateInternal(context, id, providerName);
    }

    /// <summary>
    /// Restores the control template to its default markup from the embedded resource.
    /// </summary>
    /// <param name="context">The control template to be restored.</param>
    /// <param name="id">The id of the control template.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns>
    /// An instance of the control template that was restored to the default one
    /// </returns>
    public ItemContext<ControlPresentation> RestoreControlTemplateInXml(
      ItemContext<ControlPresentation> context,
      string id,
      string providerName)
    {
      return this.RestoreControlTemplateInternal(context, id, providerName);
    }

    /// <summary>
    /// Deletes an array of control templates.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="ids">An array containing the ids of the control templates to be deleted.</param>
    /// <param name="providerName">Name of the provider to be used when deleting the control templates.</param>
    /// <returns></returns>
    public bool BatchDeleteControlTemplates(string[] ids, string providerName) => this.BatchDeleteControlTemplateInternal(ids, providerName);

    /// <summary>
    /// Deletes an array of control templates.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="ids">An array containing the ids of the control templates to be deleted.</param>
    /// <param name="providerName">Name of the provider to be used when deleting the control templates.</param>
    /// <returns></returns>
    public bool BatchDeleteControlTemplatesInXml(string[] ids, string providerName) => this.BatchDeleteControlTemplateInternal(ids, providerName);

    /// <summary>
    /// Gets common properties of the data item in JSON format.
    /// </summary>
    /// <param name="controlType">Type of the control.</param>
    /// <returns></returns>
    public CollectionContext<DataItemPropertyViewModel> GetCommonProperties(
      string controlType)
    {
      return this.GetPropertiesInternal(controlType, true);
    }

    /// <summary>
    /// Gets common properties of the data item in Xml format.
    /// </summary>
    /// <param name="controlType">Type of the control.</param>
    /// <returns></returns>
    public CollectionContext<DataItemPropertyViewModel> GetCommonPropertiesInXml(
      string controlType)
    {
      return this.GetPropertiesInternal(controlType, true);
    }

    /// <summary>
    /// Gets non-common properties of the data item in JSON format.
    /// </summary>
    /// <param name="controlType">Type of the control.</param>
    /// <returns></returns>
    public CollectionContext<DataItemPropertyViewModel> GetOtherProperties(
      string controlType)
    {
      return this.GetPropertiesInternal(controlType, false);
    }

    /// <summary>
    /// Gets non-common properties of the data item in Xml format.
    /// </summary>
    /// <param name="controlType">Type of the control.</param>
    /// <returns></returns>
    public CollectionContext<DataItemPropertyViewModel> GetOtherPropertiesInXml(
      string controlType)
    {
      return this.GetPropertiesInternal(controlType, false);
    }

    /// <inheritdoc />
    public CollectionContext<SiteItemLinkViewModel> GetSharedSites(
      string templateId,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetSharedSitesInternal(templateId, sortExpression, skip, take, filter);
    }

    /// <inheritdoc />
    public CollectionContext<SiteItemLinkViewModel> GetSharedSitesInXml(
      string templateId,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetSharedSitesInternal(templateId, sortExpression, skip, take, filter);
    }

    /// <inheritdoc />
    public bool SaveSharedSites(string templateId, string[] sharedSiteIDs) => this.SaveSharedSitesInternal(templateId, sharedSiteIDs);

    /// <inheritdoc />
    public bool SaveSharedSitesInXml(string templateId, string[] sharedSiteIDs) => this.SaveSharedSitesInternal(templateId, sharedSiteIDs);

    public ItemContext<ControlTemplateVersionInfo> GetTemplateVersionInfo(
      string id,
      string providerName,
      string VersionId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid guid = new Guid(id);
      Guid version = new Guid(VersionId);
      Change change = VersionManager.GetManager().GetChanges().Where<Change>((Expression<Func<Change, bool>>) (u => u.Id == version)).First<Change>();
      Change previousChange = VersionManager.GetManager().GetPreviousChange(change);
      Change nextChange = VersionManager.GetManager().GetNextChange(change);
      ControlTemplateVersionInfo templateVersionInfo = new ControlTemplateVersionInfo()
      {
        VersionInfo = new WcfChange(change)
      };
      templateVersionInfo.VersionInfo.PreviousId = previousChange != null ? previousChange.Id.ToString() : "";
      templateVersionInfo.VersionInfo.NextId = nextChange != null ? nextChange.Id.ToString() : "";
      templateVersionInfo.VersionInfo.NextVersionNumber = nextChange != null ? nextChange.Version : -1;
      templateVersionInfo.VersionInfo.PrevVersionNumber = previousChange != null ? previousChange.Version : -1;
      ServiceUtility.DisableCache();
      return new ItemContext<ControlTemplateVersionInfo>()
      {
        Item = templateVersionInfo
      };
    }

    private ItemContext<ControlPresentation> GetControlTemplateInternal(
      string id,
      string providerName,
      string VersionId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      ControlPresentation presentationItem = PageManager.GetManager(providerName).GetPresentationItem<ControlPresentation>(new Guid(id));
      Guid result = Guid.Empty;
      if (!string.IsNullOrWhiteSpace(VersionId) && Guid.TryParse(VersionId, out result))
        VersionManager.GetManager().GetSpecificVersionByChangeId((object) presentationItem, result);
      presentationItem.ControlType = Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.GetControlKey(presentationItem.ControlType, presentationItem.AreaName);
      ServiceUtility.DisableCache();
      return new ItemContext<ControlPresentation>()
      {
        Item = presentationItem
      };
    }

    private CollectionContext<ControlTemplateViewModel> GetControlTemplatesInternal(
      string providerName,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string templateFilter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PageManager manager = PageManager.GetManager(providerName);
      int? totalCount = new int?(0);
      string dataType = "ASP_NET_TEMPLATE";
      IQueryable<ControlPresentation> source;
      if (!(templateFilter == "AllTemplates"))
      {
        if (!(templateFilter == "MyTemplates"))
        {
          if (!(templateFilter == "NotSharedTemplates"))
          {
            if (templateFilter == "ThisSiteTemplates")
              ;
            IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
            source = multisiteContext == null ? manager.GetPresentationItems<ControlPresentation>() : manager.GetPresentationItems<ControlPresentation>(multisiteContext.CurrentSite.Id);
          }
          else
          {
            IQueryable<ControlPresentation> source2 = manager.GetPresentationItems<ControlPresentation>().Join((IEnumerable<SiteItemLink>) manager.GetSitePresentationItemLinks<ControlPresentation>(), (Expression<Func<ControlPresentation, Guid>>) (c => c.Id), (Expression<Func<SiteItemLink, Guid>>) (l => l.ItemId), (c, l) => new
            {
              c = c,
              l = l
            }).Where(data => data.l.SiteId != Guid.Empty).Select(data => data.c);
            source = manager.GetPresentationItems<ControlPresentation>().Except<ControlPresentation>((IEnumerable<ControlPresentation>) source2);
          }
        }
        else
        {
          ParameterExpression parameterExpression;
          // ISSUE: method reference
          // ISSUE: method reference
          source = manager.GetPresentationItems<ControlPresentation>().Where<ControlPresentation>(Expression.Lambda<Func<ControlPresentation, bool>>((Expression) Expression.Equal(pt.Owner, (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClaimsManager.GetCurrentUserId)), Array.Empty<Expression>()), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Equality))), parameterExpression));
        }
      }
      else
        source = manager.GetPresentationItems<ControlPresentation>();
      IQueryable<ControlPresentation> queryable1 = source.Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (p => p.DataType == dataType && p.AreaName != default (string) && p.AreaName != "Users"));
      if (Config.Get<PagesConfig>().PageTemplatesFrameworks == PageTemplatesAvailability.MvcOnly)
        queryable1 = queryable1.Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (t => t.FriendlyControlName.Contains("MVC")));
      IQueryable<ControlPresentation> queryable2 = DataProviderBase.SetExpressions<ControlPresentation>(queryable1, filter, sortExpression, new int?(skip), new int?(take), ref totalCount);
      List<ControlTemplateViewModel> items = new List<ControlTemplateViewModel>();
      foreach (ControlPresentation controlPresentation in (IEnumerable<ControlPresentation>) queryable2)
      {
        ControlPresentation controlTemplate = controlPresentation;
        int num = manager.GetSitePresentationItemLinks<ControlPresentation>().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == controlTemplate.Id && l.SiteId != Guid.Empty)).GroupBy<SiteItemLink, Guid>((Expression<Func<SiteItemLink, Guid>>) (l => l.SiteId)).Count<IGrouping<Guid, SiteItemLink>>();
        items.Add(new ControlTemplateViewModel(controlTemplate)
        {
          SiteLinksString = num.ToString() + " " + (num != 1 ? Res.Get<MultisiteResources>().SitesLower : Res.Get<MultisiteResources>().SiteLower)
        });
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<ControlTemplateViewModel>((IEnumerable<ControlTemplateViewModel>) items)
      {
        TotalCount = totalCount.Value
      };
    }

    private ItemContext<ControlPresentation> SaveControlTemplateInternal(
      ItemContext<ControlPresentation> context,
      string id,
      string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PageManager manager1 = PageManager.GetManager(providerName);
      ControlPresentation controlPresentation = context.Item;
      Guid id1 = controlPresentation.Id;
      ControlPresentation presentationItem;
      if (id1 == Guid.Empty)
      {
        presentationItem = manager1.CreatePresentationItem<ControlPresentation>();
        if (string.IsNullOrEmpty(controlPresentation.DataType))
          presentationItem.DataType = "ASP_NET_TEMPLATE";
        else
          presentationItem.DataType = controlPresentation.DataType;
        presentationItem.EmbeddedTemplateName = (string) null;
      }
      else
      {
        presentationItem = manager1.GetPresentationItem<ControlPresentation>(id1);
        if (!string.IsNullOrEmpty(presentationItem.EmbeddedTemplateName))
          presentationItem.IsDifferentFromEmbedded = true;
      }
      if (presentationItem != null)
      {
        Type controlTypeFromKey = Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.GetControlTypeFromKey(controlPresentation.ControlType);
        presentationItem.ControlType = controlTypeFromKey.FullName;
        presentationItem.Name = controlPresentation.Name;
        presentationItem.NameForDevelopers = controlPresentation.NameForDevelopers;
        string str = this.PreventServerScriptInjection(controlPresentation.Data);
        presentationItem.Data = str;
        presentationItem.Condition = WcfHelper.DecodeWcfString(controlPresentation.Condition);
        Type attributeType = typeof (ControlTemplateInfoAttribute);
        if (TypeDescriptor.GetAttributes(controlTypeFromKey)[attributeType] is ControlTemplateInfoAttribute attribute)
        {
          if (string.IsNullOrEmpty(attribute.ResourceClassId))
          {
            presentationItem.AreaName = attribute.AreaName;
            presentationItem.FriendlyControlName = attribute.ControlDisplayName;
          }
          else
          {
            presentationItem.AreaName = Res.Get(attribute.ResourceClassId, attribute.AreaName);
            presentationItem.FriendlyControlName = Res.Get(attribute.ResourceClassId, attribute.ControlDisplayName);
          }
        }
        else
        {
          presentationItem.AreaName = controlPresentation.AreaName;
          presentationItem.FriendlyControlName = controlPresentation.FriendlyControlName;
        }
        VersionManager manager2 = VersionManager.GetManager();
        manager2.CreateVersion((IDataItem) presentationItem, true);
        manager2.SaveChanges();
      }
      this.SaveChanges(manager1);
      ControlUtilities.RemoveTemplateFromCache(presentationItem.Id.ToString());
      return new ItemContext<ControlPresentation>()
      {
        Item = presentationItem
      };
    }

    private string PreventServerScriptInjection(string data) => Regex.Replace(data, "<script runat=\"server\".*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

    private bool DeleteControlTemplateInternal(string id, string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PageManager manager = PageManager.GetManager(providerName);
      Guid id1 = new Guid(id);
      PresentationData presentationItem = manager.GetPresentationItem<PresentationData>(id1);
      manager.Delete(presentationItem);
      this.SaveChanges(manager);
      ControlUtilities.RemoveTemplateFromCache(id);
      return true;
    }

    private ItemContext<ControlPresentation> RestoreControlTemplateInternal(
      ItemContext<ControlPresentation> context,
      string id,
      string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PageManager manager1 = PageManager.GetManager(providerName);
      Guid id1 = context.Item.Id;
      ControlPresentation controlPresentation = !(id1 == Guid.Empty) ? manager1.GetPresentationItem<ControlPresentation>(id1) : throw new InvalidOperationException("Could not find a template with the given ID.");
      if (controlPresentation != null)
      {
        Type moduleType = TypeResolutionService.ResolveType(controlPresentation.Condition, false);
        Type controlType = TypeResolutionService.ResolveType(controlPresentation.ControlType, false);
        if (moduleType != (Type) null && controlType != (Type) null && typeof (DynamicContent).IsAssignableFrom(moduleType.BaseType))
        {
          ModuleBuilderManager manager2 = ModuleBuilderManager.GetManager();
          WidgetTemplateInstaller templateInstaller = new WidgetTemplateInstaller(manager1, manager2);
          controlPresentation.Data = templateInstaller.GetDefaultTemplate(moduleType, controlType);
        }
        else
          controlPresentation.Data = (string) null;
      }
      this.SaveChanges(manager1);
      ControlUtilities.RemoveTemplateFromCache(controlPresentation.Id.ToString());
      return new ItemContext<ControlPresentation>()
      {
        Item = controlPresentation
      };
    }

    private bool BatchDeleteControlTemplateInternal(string[] ids, string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PageManager manager = PageManager.GetManager(providerName);
      foreach (string id1 in ids)
      {
        Guid id2 = new Guid(id1);
        PresentationData presentationItem = manager.GetPresentationItem<PresentationData>(id2);
        manager.Delete(presentationItem);
      }
      this.SaveChanges(manager);
      foreach (string id in ids)
        ControlUtilities.RemoveTemplateFromCache(id);
      return true;
    }

    private CollectionContext<DataItemPropertyViewModel> GetPropertiesInternal(
      string controlType,
      bool returnCommonProperties)
    {
      Type componentType = TypeResolutionService.ResolveType(WcfHelper.DecodeWcfString(controlType), false);
      List<DataItemPropertyViewModel> items = new List<DataItemPropertyViewModel>();
      if (componentType != (Type) null)
      {
        PropertyDescriptorCollection descriptorCollection;
        if (componentType == typeof (PageSiteNode))
        {
          descriptorCollection = PageSiteNode.PropertyCache.Sort();
        }
        else
        {
          if (componentType == typeof (IDocument))
            return this.GetSearchDocumentProperties(returnCommonProperties);
          descriptorCollection = TypeDescriptor.GetProperties(componentType).Sort();
        }
        foreach (PropertyDescriptor propertyDescriptor in descriptorCollection)
        {
          PropertyDescriptor prop = propertyDescriptor;
          if (prop.IsBrowsable && prop.Converter.CanConvertTo(typeof (string)))
          {
            CommonPropertyAttribute attribute1 = prop.Attributes[typeof (CommonPropertyAttribute)] as CommonPropertyAttribute;
            MetaFieldAttributeAttribute attribute2 = prop.Attributes[typeof (MetaFieldAttributeAttribute)] as MetaFieldAttributeAttribute;
            ScaffoldInfoAttribute attribute3 = prop.Attributes[typeof (ScaffoldInfoAttribute)] as ScaffoldInfoAttribute;
            string str1 = string.Empty;
            bool flag = attribute1 > null;
            if (attribute2 != null && attribute3 != null)
              throw new InvalidOperationException("Only one attribute can be used to specify the control tag");
            if (attribute2 != null)
            {
              attribute2.Attributes.TryGetValue(DynamicAttributeNames.ControlTag, out str1);
              string str2;
              if (attribute2.Attributes.TryGetValue(DynamicAttributeNames.IsCommonProperty, out str2))
                flag |= bool.Parse(str2);
            }
            if (attribute3 != null)
              str1 = attribute3.ControlTag;
            if (!str1.IsNullOrEmpty() || !prop.Converter.GetPropertiesSupported())
            {
              UserFriendlyDataType? fieldType = prop.GetFieldType();
              if (componentType == typeof (SitefinityProfile))
              {
                if (fieldType.HasValue && this.userProfileTemplates.ContainsKey(fieldType.Value))
                  str1 = string.Format(this.userProfileTemplates[fieldType.Value], (object) prop.Name);
                else if (fieldType.HasValue && fieldType.Value == UserFriendlyDataType.Classification)
                {
                  Taxonomy taxonomy = (Taxonomy) TaxonomyManager.GetManager().GetTaxonomies<FlatTaxonomy>().Where<FlatTaxonomy>((Expression<Func<FlatTaxonomy, bool>>) (t => t.Name == prop.Name)).SingleOrDefault<FlatTaxonomy>();
                  if (taxonomy == null)
                    taxonomy = (Taxonomy) TaxonomyManager.GetManager().GetTaxonomies<HierarchicalTaxonomy>().Where<HierarchicalTaxonomy>((Expression<Func<HierarchicalTaxonomy, bool>>) (t => t.TaxonName == (Lstring) prop.Name)).SingleOrDefault<HierarchicalTaxonomy>();
                  if (taxonomy != null)
                    str1 = TaxonomyManager.GetTaxonomyFieldControlTemplate(prop.Name, (ITaxonomy) taxonomy);
                }
                else
                  str1 = string.Format(this.userProfileTemplates[UserFriendlyDataType.ShortText], (object) prop.Name);
              }
              if (str1.IsNullOrEmpty() && fieldType.HasValue && this.defaultTemplates.ContainsKey(fieldType.Value))
                str1 = string.Format(this.defaultTemplates[fieldType.Value], (object) prop.Name);
              if (returnCommonProperties == flag)
                items.Add(new DataItemPropertyViewModel(prop)
                {
                  ControlTag = str1
                });
            }
          }
        }
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<DataItemPropertyViewModel>((IEnumerable<DataItemPropertyViewModel>) items);
    }

    private CollectionContext<DataItemPropertyViewModel> GetSearchDocumentProperties(
      bool returnCommonProperties)
    {
      List<DataItemPropertyViewModel> items = new List<DataItemPropertyViewModel>();
      IQueryable<PublishingPoint> source1 = PublishingManager.GetManager("SearchPublishingProvider").GetPublishingPoints().Where<PublishingPoint>((Expression<Func<PublishingPoint, bool>>) (p => p.ApplicationName == "/Search"));
      IQueryable<string> queryable;
      if (returnCommonProperties)
      {
        queryable = source1.SelectMany<PublishingPoint, string>((Expression<Func<PublishingPoint, IEnumerable<string>>>) (p => p.PipeSettings.Where<PipeSettings>((Func<PipeSettings, bool>) (ps => !ps.IsInbound)).SelectMany<PipeSettings, Mapping>((Func<PipeSettings, IEnumerable<Mapping>>) (ps => ps.Mappings.Mappings)).Select<Mapping, string>((Func<Mapping, string>) (m => m.DestinationPropertyName))));
      }
      else
      {
        List<string> source2 = new List<string>();
        IQueryable<PublishingPoint> source3 = source1;
        Expression<Func<PublishingPoint, IEnumerable<PipeSettings>>> selector = (Expression<Func<PublishingPoint, IEnumerable<PipeSettings>>>) (p => p.PipeSettings.Where<PipeSettings>((Func<PipeSettings, bool>) (ps => !ps.IsInbound)));
        foreach (PipeSettings settings in (IEnumerable<PipeSettings>) source3.SelectMany<PublishingPoint, PipeSettings>(selector))
          source2.AddRange(PublishingUtilities.SearchIndexAdditionalFields(settings));
        queryable = source2.AsQueryable<string>();
      }
      foreach (string str in (IEnumerable<string>) queryable)
      {
        DataItemPropertyViewModel propertyViewModel = new DataItemPropertyViewModel()
        {
          DisplayName = str,
          Name = str
        };
        items.Add(propertyViewModel);
      }
      return new CollectionContext<DataItemPropertyViewModel>((IEnumerable<DataItemPropertyViewModel>) items);
    }

    private void SaveChanges(PageManager manager)
    {
      if (manager.Provider.TransactionName.IsNullOrWhitespace())
        manager.SaveChanges();
      else
        TransactionManager.CommitTransaction(manager.Provider.TransactionName);
    }

    private CollectionContext<SiteItemLinkViewModel> GetSharedSitesInternal(
      string templateId,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ControlTemplateService.\u003C\u003Ec__DisplayClass31_0 cDisplayClass310 = new ControlTemplateService.\u003C\u003Ec__DisplayClass31_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass310.templateGuid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(templateId);
      PageManager manager = PageManager.GetManager();
      IQueryable<Telerik.Sitefinity.Multisite.ISite> source = SystemManager.CurrentContext.MultisiteContext.GetSites().AsQueryable<Telerik.Sitefinity.Multisite.ISite>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass310.siteLinks = (IEnumerable<SiteItemLink>) null;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass310.templateGuid != Guid.Empty)
      {
        // ISSUE: reference to a compiler-generated field
        SiteItemLink[] array = manager.GetSitePresentationItemLinks<ControlPresentation>().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == cDisplayClass310.templateGuid)).ToArray<SiteItemLink>();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass310.siteLinks = (IEnumerable<SiteItemLink>) array;
      }
      if (!string.IsNullOrEmpty(sortExpression))
        source = source.OrderBy<Telerik.Sitefinity.Multisite.ISite>(sortExpression);
      if (!string.IsNullOrEmpty(filter))
        source = source.Where<Telerik.Sitefinity.Multisite.ISite>(filter);
      int num = source.Count<Telerik.Sitefinity.Multisite.ISite>();
      if (skip > 0)
        source = source.Skip<Telerik.Sitefinity.Multisite.ISite>(skip);
      if (take > 0)
        source = source.Take<Telerik.Sitefinity.Multisite.ISite>(take);
      ServiceUtility.DisableCache();
      ParameterExpression parameterExpression1;
      ParameterExpression parameterExpression2;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: method reference
      return ; //unable to render the statement
    }

    private bool SaveSharedSitesInternal(string templateId, string[] sharedSiteIDs)
    {
      Guid templateGuid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(templateId);
      IEnumerable<Guid> source = ((IEnumerable<string>) sharedSiteIDs).ToList<string>().Select<string, Guid>((Func<string, Guid>) (id => Telerik.Sitefinity.Utilities.Utility.StringToGuid(id)));
      PageManager manager = PageManager.GetManager();
      if (!(templateGuid != Guid.Empty))
        return false;
      SiteItemLink[] array = manager.GetSitePresentationItemLinks<ControlPresentation>().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (link => link.ItemId == templateGuid)).ToArray<SiteItemLink>();
      for (int index = array.Length - 1; index >= 0; --index)
      {
        if (!source.Contains<Guid>(array[index].SiteId))
          manager.Delete(array[index]);
      }
      ControlPresentation controlPresentation1 = new ControlPresentation();
      controlPresentation1.Id = templateGuid;
      ControlPresentation controlPresentation2 = controlPresentation1;
      foreach (Guid guid in source)
      {
        Guid siteId = guid;
        if (siteId != Guid.Empty && !((IEnumerable<SiteItemLink>) array).Any<SiteItemLink>((Func<SiteItemLink, bool>) (l => l.SiteId == siteId)))
          manager.LinkPresentationItemToSite((PresentationData) controlPresentation2, siteId);
      }
      manager.SaveChanges();
      return true;
    }
  }
}
