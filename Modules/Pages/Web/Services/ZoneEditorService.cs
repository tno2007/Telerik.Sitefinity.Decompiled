// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.ZoneEditorService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.Services.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UrlEvaluation;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>Represents web service for ZoneEditor.</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class ZoneEditorService : IZoneEditorService
  {
    public ZoneEditorWebServiceArgs SetOverride(
      ZoneEditorWebServiceArgs state,
      string editable)
    {
      Guid controlIdGuid = state.Id;
      bool flag = bool.Parse(editable);
      PageManager manager = PageManager.GetManager();
      TemplateDraftControl controlData = manager.GetControls<TemplateDraftControl>().Where<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (ctrlIter => ctrlIter.Id == controlIdGuid)).FirstOrDefault<TemplateDraftControl>();
      if (controlData.PersonalizationMasterId != Guid.Empty)
        controlData = manager.GetControl<TemplateDraftControl>(controlData.PersonalizationMasterId);
      controlData.Editable = flag;
      controlData.IsOverridedControl = flag;
      PageManager.GetManager().SaveChanges();
      state.Attributes["perm_widgetDisableOverride"] = flag.ToString();
      state.Attributes["perm_displayOverridenControls"] = flag.ToString();
      state.Attributes["perm_widgetOverride"] = (!flag).ToString();
      if (ControlUtilities.BehaviorResolver.GetBehaviorObject(manager.LoadControl((ObjectData) controlData, (CultureInfo) null)) is ICustomWidgetTitlebar behaviorObject)
      {
        string str = string.Join("", behaviorObject.CustomMessages.ToArray<string>());
        state.CustomTitleHtml = str;
      }
      if (flag)
        state.CustomTitleHtml += ZoneEditor.GetOverridedControlTitle((Telerik.Sitefinity.Pages.Model.ControlData) controlData, state.DockId);
      return state;
    }

    public ZoneEditorWebServiceArgs Rollback(ZoneEditorWebServiceArgs state)
    {
      Guid controlIdGuid = state.Id;
      Guid pageIdGuid = state.PageId;
      PageManager manager = PageManager.GetManager();
      IQueryable<PageDraftControl> controls1 = manager.GetControls<PageDraftControl>();
      IQueryable<TemplateDraftControl> controls2 = manager.GetControls<TemplateDraftControl>();
      Telerik.Sitefinity.Pages.Model.ControlData controlData = (Telerik.Sitefinity.Pages.Model.ControlData) controls1.Where<PageDraftControl>((Expression<Func<PageDraftControl, bool>>) (control => control.BaseControlId == controlIdGuid && control.Page.Id == pageIdGuid)).FirstOrDefault<PageDraftControl>();
      if (controlData == null)
        controlData = (Telerik.Sitefinity.Pages.Model.ControlData) controls2.Where<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (control => control.BaseControlId == controlIdGuid && control.Page.Id == pageIdGuid)).FirstOrDefault<TemplateDraftControl>();
      manager.Delete(controlData);
      manager.SaveChanges();
      return state;
    }

    /// <summary>Updates the state of the layout.</summary>
    /// <param name="state">The state.</param>
    /// <returns></returns>
    public ZoneEditorWebServiceArgs UpdateLayoutState(
      ZoneEditorWebServiceArgs state)
    {
      return this.UpdateState(state, true);
    }

    /// <summary>Updates the state of the control.</summary>
    /// <param name="state">The state.</param>
    /// <returns></returns>
    public ZoneEditorWebServiceArgs UpdateControlState(
      ZoneEditorWebServiceArgs state)
    {
      return this.UpdateState(state, false);
    }

    /// <summary>
    /// Updates the styles of the columns in the layout control.
    /// </summary>
    /// <param name="styles">The array of styles.</param>
    /// <returns></returns>
    public ZoneEditorWebServiceArgs UpdateLayoutControlStyles(
      ZoneEditorWebServiceArgs args,
      string layoutControlId,
      string pageId,
      string isTemplate)
    {
      if (!ControlUtilities.IsGuid(layoutControlId))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
      IControlManager manager;
      Telerik.Sitefinity.Pages.Model.ControlData control;
      switch (args.MediaType)
      {
        case DesignMediaType.Page:
        case DesignMediaType.NewsletterCampaign:
        case DesignMediaType.NewsletterTemplate:
          manager = (IControlManager) PageManager.GetManager();
          control = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControl<PageDraftControl>(new Guid(layoutControlId));
          break;
        case DesignMediaType.Template:
          manager = (IControlManager) PageManager.GetManager();
          control = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControl<TemplateDraftControl>(new Guid(layoutControlId));
          break;
        case DesignMediaType.Form:
          manager = (IControlManager) FormsManager.GetManager();
          control = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControl<FormDraftControl>(new Guid(layoutControlId));
          break;
        default:
          throw new ArgumentException();
      }
      if (args.Title != null)
        control.Caption = args.Title;
      ControlProperty controlProperty1 = control.GetProperties(true).Single<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Layout"));
      string str1 = controlProperty1.Value;
      string str2;
      if (str1.StartsWith("~/"))
      {
        using (Stream stream = SitefinityFile.Open(str1))
        {
          using (StreamReader streamReader = new StreamReader(stream))
            str2 = streamReader.ReadToEnd();
        }
      }
      else if (str1.EndsWith(".ascx", StringComparison.OrdinalIgnoreCase))
      {
        ControlProperty controlProperty2 = control.GetProperties(true).SingleOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "AssemblyInfo"));
        Type assemblyInfo = controlProperty2 == null || string.IsNullOrEmpty(controlProperty2.Value) ? Config.Get<ControlsConfig>().ResourcesAssemblyInfo : TypeResolutionService.ResolveType(controlProperty2.Value, true);
        str2 = ControlUtilities.GetTextResource(str1, assemblyInfo);
      }
      controlProperty1.Value = args.LayoutHtml;
      ++control.Version;
      manager.SaveChanges();
      return (ZoneEditorWebServiceArgs) null;
    }

    protected virtual void SetPermissions(Telerik.Sitefinity.Pages.Model.ControlData ctrl, ZoneEditorWebServiceArgs args)
    {
      if (args.Permissions == null)
        args.Permissions = new Dictionary<string, string>();
      else
        args.Permissions.Clear();
      foreach (KeyValuePair<string, string> controlPermission in ZoneEditor.GetControlPermissions(ctrl, args.PageId, new DesignMediaType?(args.MediaType)))
        args.Permissions.Add(controlPermission.Key, controlPermission.Value);
    }

    private void DemandPlaceholderPermissions(
      ZoneEditorWebServiceArgs state,
      IControlManager manager)
    {
      Guid layoutControldataID;
      if (string.IsNullOrEmpty(state.LayoutControlDataID) || !Guid.TryParse(state.LayoutControlDataID, out layoutControldataID))
        return;
      Telerik.Sitefinity.Pages.Model.ControlData securedObject = manager.GetControls<Telerik.Sitefinity.Pages.Model.ControlData>().Where<Telerik.Sitefinity.Pages.Model.ControlData>((Expression<Func<Telerik.Sitefinity.Pages.Model.ControlData, bool>>) (c => c.Id == layoutControldataID)).SingleOrDefault<Telerik.Sitefinity.Pages.Model.ControlData>();
      if (securedObject != null)
      {
        string permissionSetName = "LayoutElement";
        if (!(state.CommandName == "duplicate") && !(state.CommandName == "new") && !(state.CommandName == "indexchanged"))
          return;
        securedObject.Demand(permissionSetName, "DropOnLayout");
      }
      else
        state.LayoutControlDataID = (string) null;
    }

    private ZoneEditorWebServiceArgs UpdateState(
      ZoneEditorWebServiceArgs state,
      bool isLayoutControl)
    {
      IControlManager manager1;
      switch (state.MediaType)
      {
        case DesignMediaType.Page:
        case DesignMediaType.NewsletterCampaign:
        case DesignMediaType.NewsletterTemplate:
          manager1 = (IControlManager) PageManager.GetManager();
          break;
        case DesignMediaType.Template:
          manager1 = (IControlManager) PageManager.GetManager();
          break;
        case DesignMediaType.Form:
          manager1 = (IControlManager) FormsManager.GetManager();
          break;
        default:
          throw new ArgumentException();
      }
      this.DemandPlaceholderPermissions(state, manager1);
      string commandName = state.CommandName;
      Telerik.Sitefinity.Pages.Model.ControlData controlData1;
      if (!(commandName == "reloadDependant") && !(commandName == "reload"))
      {
        if (!(commandName == "new"))
        {
          if (!(commandName == "duplicate"))
          {
            if (!(commandName == "indexchanged"))
            {
              if (commandName == "delete")
                this.DeleteControl(state, manager1);
              controlData1 = (Telerik.Sitefinity.Pages.Model.ControlData) null;
            }
            else
              controlData1 = this.ChangeIndex(state, manager1);
          }
          else
          {
            controlData1 = this.DuplicateControl(state, manager1, isLayoutControl);
            this.SetPermissions(controlData1, state);
          }
        }
        else
        {
          controlData1 = this.CreateNewControl(state, manager1, isLayoutControl);
          this.SetPermissions(controlData1, state);
          controlData1.Editable = true;
        }
      }
      else
      {
        controlData1 = this.ReloadControl(state, manager1);
        this.SetPermissions(controlData1, state);
      }
      if (!state.ModuleName.IsNullOrEmpty())
        state.Attributes.Add("moduleName", state.ModuleName);
      SiteRegion siteRegion = (SiteRegion) null;
      if (controlData1 != null)
      {
        Guid guid;
        if (state.MediaType == DesignMediaType.Page)
        {
          HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
          guid = state.PageNodeId;
          string key = guid.ToString();
          string url = state.Url;
          int startIndex = url.IndexOf("?");
          if (startIndex != -1)
          {
            QueryStringBuilder queryStringBuilder = new QueryStringBuilder(url.Substring(startIndex));
            if (queryStringBuilder.Contains("sf_site"))
            {
              IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
              Guid result;
              if (multisiteContext != null && Guid.TryParse(queryStringBuilder["sf_site"], out result))
                siteRegion = new SiteRegion(multisiteContext.GetSiteById(result));
            }
          }
          SiteMapProvider mapProviderForUrl = SiteMapBase.GetSiteMapProviderForUrl(url);
          SiteMapNode siteMapNodeFromKey = mapProviderForUrl.FindSiteMapNodeFromKey(key);
          if (siteMapNodeFromKey == null)
            throw new InvalidDataException("A page with the specified ID ({0}) cannot be found.".Arrange((object) key));
          IDictionary items = currentHttpContext.Items;
          items[(object) "SF_SiteMap"] = (object) mapProviderForUrl;
          items[(object) "ServedPageNode"] = (object) siteMapNodeFromKey;
        }
        Control control1 = manager1.LoadControl((ObjectData) controlData1);
        if (control1 is LayoutControl)
        {
          LayoutControl layoutControl = (LayoutControl) control1;
          layoutControl.PlaceHolder = controlData1.PlaceHolder;
          state.Attributes.Add("clientIds", string.Join(",", layoutControl.Placeholders.Select<Control, string>((Func<Control, string>) (c => c.ClientID))));
        }
        if (controlData1.IsLayoutControl)
        {
          state.ModifiedLayoutPermissios = new JsonDictionary<LayoutControlDataPermissions>();
          LayoutControlDataPermissions controlDataPermissions = LayoutControlDataPermissions.Create(controlData1);
          foreach (string placeHolder in controlData1.PlaceHolders)
            state.ModifiedLayoutPermissios[placeHolder] = controlDataPermissions;
          if (state.CommandName == "indexchanged" && !string.IsNullOrEmpty(state.PreviousPlaceholderID))
            this.RestorePreviousPlaceholderPermissionsOnIndexChange(state, manager1);
        }
        else
          state.ModifiedLayoutPermissios = (JsonDictionary<LayoutControlDataPermissions>) null;
        if (!SystemManager.HttpContextItems.Contains((object) "sfContentFilters"))
          SystemManager.HttpContextItems.Add((object) "sfContentFilters", (object) new string[1]
          {
            "LinksParser"
          });
        SystemManager.HttpContextItems[(object) "RadControlRandomNumber"] = (object) 0;
        ControlLiteralRepresentation literalRepresentation;
        if (control1 is MvcProxyBase originalControl)
        {
          MvcProxyBase mvcProxyBase = originalControl;
          guid = controlData1.Id;
          string str = guid.ToString();
          mvcProxyBase.ControlDataId = str;
          literalRepresentation = new ControlLiteralRepresentation((Control) originalControl, state);
        }
        else
          literalRepresentation = new ControlLiteralRepresentation(control1, state);
        literalRepresentation.CheckAndInsertEmptyControl = true;
        if (state.CommandName == "reload")
          SystemManager.HttpContextItems[(object) "sf_use_embedded_backend_theme"] = (object) true;
        if (state.CommandName != "indexchanged")
        {
          state.Html = literalRepresentation.GetControlHtml();
          state.CssLinkUrls = literalRepresentation.CssLinkControls.Select<HtmlLink, string>((Func<HtmlLink, string>) (ctrl => ctrl.Href)).ToArray<string>();
          if (literalRepresentation.RenderedPage != null)
          {
            SitefinityScriptManager current = SitefinityScriptManager.GetCurrent(literalRepresentation.RenderedPage);
            if (current != null)
              state.Scripts = current.ScriptDefinitions.Select<ScriptDefinition, string>((Func<ScriptDefinition, string>) (s => s.Url)).ToArray<string>();
          }
          Control control2 = (Control) null;
          if (control1 != null && (!(controlData1 is Telerik.Sitefinity.Pages.Model.TemplateControl) || controlData1.IsOverridedControl))
            control2 = ZoneEditor.TryCreateEmptyControl(control1);
          if (control2 != null)
          {
            Control control3 = control2;
            control3.ID = control3.ID + "_" + DateTime.UtcNow.ToString("MMddyyyyHHmmssfff");
            StringBuilder sb = new StringBuilder();
            HtmlTextWriter writer = new HtmlTextWriter((TextWriter) new StringWriter(sb));
            control2.RenderControl(writer);
            state.Html = sb.ToString();
          }
        }
        JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
        if (control1 != null)
        {
          if (ControlUtilities.BehaviorResolver.GetBehaviorObject(control1) is IHasEditCommands behaviorObject1)
          {
            if (!LicenseState.Current.LicenseInfo.IsPageControlsPermissionsEnabled)
            {
              foreach (WidgetMenuItem widgetMenuItem in behaviorObject1.Commands.Where<WidgetMenuItem>((Func<WidgetMenuItem, bool>) (c => c.CommandName == "permissions")).ToList<WidgetMenuItem>())
                behaviorObject1.Commands.Remove(widgetMenuItem);
            }
            string str = scriptSerializer.Serialize((object) behaviorObject1.Commands);
            state.Attributes.Add("widgetCommands", str);
          }
          if (ControlUtilities.BehaviorResolver.GetBehaviorObject(control1) is ICustomWidgetTitlebar behaviorObject2)
          {
            string str = string.Join("", behaviorObject2.CustomMessages.ToArray<string>());
            state.CustomTitleHtml = str;
          }
          else
            state.CustomTitleHtml = (string) null;
          object behaviorObject3 = ControlUtilities.BehaviorResolver.GetBehaviorObject(control1);
          if (behaviorObject3 is IZoneEditorReloader zoneEditorReloader)
          {
            state.Attributes["reloadKey"] = zoneEditorReloader.Key;
            if (string.IsNullOrEmpty(state.ControlType))
              state.ControlType = control1.GetType().AssemblyQualifiedName;
            state.ReloadControlsWithSameKey = state.CommandName != "reloadDependant" && zoneEditorReloader.ShouldReloadControlsWithSameKey();
          }
          else
          {
            if (state.Attributes.ContainsKey("reloadKey"))
              state.Attributes.Remove("reloadKey");
            state.ReloadControlsWithSameKey = false;
          }
          if (behaviorObject3 is IHasDependentControls dependentControls)
          {
            if (dependentControls.CreateDependentKeys.Count<string>() > 0)
              state.Attributes["createDependentKeys"] = scriptSerializer.Serialize((object) dependentControls.CreateDependentKeys);
            if (dependentControls.DeleteDependentKeys.Count<string>() > 0)
              state.Attributes["deleteDependentKeys"] = scriptSerializer.Serialize((object) dependentControls.DeleteDependentKeys);
            if (dependentControls.IndexChangeDependentKeys.Count<string>() > 0)
              state.Attributes["indexChangedDependentKeys"] = scriptSerializer.Serialize((object) dependentControls.IndexChangeDependentKeys);
            if (dependentControls.ReloadDependentKeys.Count<string>() > 0)
              state.Attributes["reloadDependentKeys"] = scriptSerializer.Serialize((object) dependentControls.ReloadDependentKeys);
          }
          string controlDesignerUrl = ZoneEditor.GetControlDesignerUrl(controlData1);
          if (!controlDesignerUrl.IsNullOrEmpty())
            state.Attributes.Add("designerUrl", VirtualPathUtility.ToAbsolute(controlDesignerUrl));
          bool flag = !controlData1.IsBackendObject && state.MediaType == DesignMediaType.Page && ZoneEditor.OpenNewWidgetEditor(controlData1);
          state.Attributes.Add("openAdminAppEditor", flag.ToString().ToLowerInvariant());
          if (SystemManager.IsModuleAccessible("Personalization") && ZoneEditor.ImplementsInterface<IPersonalizable>(controlData1))
          {
            Telerik.Sitefinity.Pages.Model.ControlData controlData2 = controlData1;
            if (controlData1.PersonalizationMasterId != Guid.Empty)
              controlData2 = manager1.GetControl<Telerik.Sitefinity.Pages.Model.ControlData>(controlData1.PersonalizationMasterId);
            List<WidgetMenuItem> widgetSegments = ZoneEditor.GetWidgetSegments(controlData2);
            string str = scriptSerializer.Serialize((object) widgetSegments);
            state.Attributes.Add("widgetSegments", str);
          }
        }
      }
      siteRegion?.Dispose();
      if (state.Attributes != null)
      {
        foreach (string key in state.Attributes.Keys.ToList<string>())
        {
          if (state.Attributes[key].ToLower().Contains("perm_"))
            state.Attributes.Remove(key);
        }
      }
      if (controlData1 != null)
      {
        foreach (KeyValuePair<string, string> controlPermission in ZoneEditor.GetControlPermissions(controlData1, state.PageId, new DesignMediaType?(state.MediaType)))
          state.Attributes.Add(controlPermission.Key, controlPermission.Value);
        if (!controlData1.IsLayoutControl)
        {
          PageManager manager2 = PageManager.GetManager();
          if (controlData1 is TemplateDraftControl && controlData1.PersonalizationMasterId != Guid.Empty)
            controlData1 = (Telerik.Sitefinity.Pages.Model.ControlData) manager2.GetControl<TemplateDraftControl>(controlData1.PersonalizationMasterId);
          if (state.CommandName == "new")
          {
            Dictionary<string, string> attributes1 = state.Attributes;
            bool flag = false;
            string str1 = flag.ToString();
            attributes1["perm_widgetDisableOverride"] = str1;
            Dictionary<string, string> attributes2 = state.Attributes;
            flag = false;
            string str2 = flag.ToString();
            attributes2["perm_displayOverridenControls"] = str2;
            Dictionary<string, string> attributes3 = state.Attributes;
            flag = controlData1 is TemplateDraftControl;
            string str3 = flag.ToString();
            attributes3["perm_widgetOverride"] = str3;
          }
          if (controlData1.Editable && state.CommandName != "new" && controlData1 is TemplateDraftControl && ((TemplateDraftControl) controlData1).Page.Id == state.PageId)
            state.CustomTitleHtml += ZoneEditor.GetOverridedControlTitle(controlData1, state.DockId);
          if (controlData1 is FormDraftControl formControl && formControl.IsFieldHidden())
            state.Attributes.Add("sf-hidden-field-label", Res.Get<FormsResources>().Hidden);
        }
        state.Attributes.Add("mastercontrolid", controlData1.PersonalizationMasterId != Guid.Empty ? controlData1.PersonalizationMasterId.ToString() : controlData1.Id.ToString());
      }
      return state;
    }

    private void RestorePreviousPlaceholderPermissionsOnIndexChange(
      ZoneEditorWebServiceArgs state,
      IControlManager man)
    {
      Telerik.Sitefinity.Pages.Model.ControlData layoutControlData;
      switch (state.MediaType)
      {
        case DesignMediaType.Page:
        case DesignMediaType.NewsletterCampaign:
        case DesignMediaType.NewsletterTemplate:
          PageDraft draft1 = ((PageManager) man).GetDraft<PageDraft>(state.PageId);
          layoutControlData = this.GetPagePlaceholder((PageManager) man, draft1, state.PreviousPlaceholderID);
          break;
        case DesignMediaType.Template:
          TemplateDraft draft2 = ((PageManager) man).GetDraft<TemplateDraft>(state.PageId);
          layoutControlData = this.GetTemplatePlaceholder((PageManager) man, draft2, state.PreviousPlaceholderID);
          break;
        case DesignMediaType.Form:
          FormDraft draft3 = ((FormsManager) man).GetDraft(state.PageId);
          layoutControlData = this.GetFormPlaceholder((FormsManager) man, draft3, state.PreviousPlaceholderID);
          break;
        default:
          throw new NotImplementedException();
      }
      if (layoutControlData == null)
        return;
      LayoutControlDataPermissions controlDataPermissions = LayoutControlDataPermissions.Create(layoutControlData);
      foreach (string placeHolder in layoutControlData.PlaceHolders)
        state.ModifiedLayoutPermissios[placeHolder] = controlDataPermissions;
    }

    protected CultureInfo GetCurrentLanguage()
    {
      CultureInfo currentLanguage = SystemManager.CurrentContext.Culture;
      if (!SystemManager.CurrentContext.AppSettings.Multilingual)
        currentLanguage = (CultureInfo) null;
      return currentLanguage;
    }

    /// <summary>Changes the index.</summary>
    /// <param name="state">The state.</param>
    /// <param name="manager">The manager.</param>
    protected virtual Telerik.Sitefinity.Pages.Model.ControlData ChangeIndex(
      ZoneEditorWebServiceArgs state,
      IControlManager manager)
    {
      IControlsContainer draft;
      Telerik.Sitefinity.Pages.Model.ControlData ctrl;
      switch (state.MediaType)
      {
        case DesignMediaType.Page:
        case DesignMediaType.NewsletterCampaign:
        case DesignMediaType.NewsletterTemplate:
          PageManager pageManager1 = (PageManager) manager;
          ctrl = (Telerik.Sitefinity.Pages.Model.ControlData) pageManager1.GetControl<PageDraftControl>(state.Id);
          draft = (IControlsContainer) pageManager1.GetDraft<PageDraft>(state.PageId);
          break;
        case DesignMediaType.Template:
          PageManager pageManager2 = (PageManager) manager;
          ctrl = (Telerik.Sitefinity.Pages.Model.ControlData) pageManager2.GetControl<TemplateDraftControl>(state.Id);
          draft = (IControlsContainer) pageManager2.GetDraft<TemplateDraft>(state.PageId);
          break;
        case DesignMediaType.Form:
          FormsManager formsManager = (FormsManager) manager;
          ctrl = (Telerik.Sitefinity.Pages.Model.ControlData) formsManager.GetControl<FormDraftControl>(state.Id);
          draft = (IControlsContainer) formsManager.GetDraft(state.PageId);
          break;
        default:
          throw new ArgumentException();
      }
      if (draft is DraftData)
        ++((DraftData) draft).Version;
      Guid id = ctrl.Id;
      Guid newSiblingId = state.SiblingId;
      string newPlaceHolder = state.PlaceholderId;
      if (newSiblingId != ctrl.SiblingId || newPlaceHolder != ctrl.PlaceHolder)
      {
        Telerik.Sitefinity.Pages.Model.ControlData controlData1 = draft.Controls.SingleOrDefault<Telerik.Sitefinity.Pages.Model.ControlData>((Func<Telerik.Sitefinity.Pages.Model.ControlData, bool>) (c => c.SiblingId == id && c.PlaceHolder == ctrl.PlaceHolder));
        if (controlData1 != null)
          controlData1.SiblingId = ctrl.SiblingId;
        Telerik.Sitefinity.Pages.Model.ControlData controlData2 = draft.Controls.SingleOrDefault<Telerik.Sitefinity.Pages.Model.ControlData>((Func<Telerik.Sitefinity.Pages.Model.ControlData, bool>) (c => c.SiblingId == newSiblingId && c.PlaceHolder == newPlaceHolder));
        if (controlData2 != null)
          controlData2.SiblingId = ctrl.Id;
        ctrl.PlaceHolder = newPlaceHolder;
        ctrl.SiblingId = newSiblingId;
        ++ctrl.Version;
      }
      manager.SaveChanges();
      return ctrl;
    }

    /// <summary>Reloads the control.</summary>
    /// <param name="state">The state.</param>
    /// <param name="manager">The manager.</param>
    /// <returns></returns>
    protected virtual Telerik.Sitefinity.Pages.Model.ControlData ReloadControl(
      ZoneEditorWebServiceArgs state,
      IControlManager manager)
    {
      Telerik.Sitefinity.Pages.Model.ControlData controlData = (Telerik.Sitefinity.Pages.Model.ControlData) null;
      switch (state.MediaType)
      {
        case DesignMediaType.Page:
        case DesignMediaType.NewsletterCampaign:
        case DesignMediaType.NewsletterTemplate:
          Telerik.Sitefinity.Pages.Model.ControlData control = manager.GetControl<Telerik.Sitefinity.Pages.Model.ControlData>(state.Id);
          if (control is Telerik.Sitefinity.Pages.Model.TemplateControl)
          {
            if (control.PersonalizationMasterId != Guid.Empty)
            {
              Telerik.Sitefinity.Pages.Model.ControlData control1 = manager.GetControl<Telerik.Sitefinity.Pages.Model.ControlData>(control.PersonalizationMasterId);
              Telerik.Sitefinity.Pages.Model.ControlData overridingControlForPage = this.GetOverridingControlForPage(control1.BaseControlId != Guid.Empty ? control1.BaseControlId : control1.Id, state.PageId);
              if (overridingControlForPage != null)
                controlData = overridingControlForPage.PersonalizedControls.FirstOrDefault<Telerik.Sitefinity.Pages.Model.ControlData>((Func<Telerik.Sitefinity.Pages.Model.ControlData, bool>) (c => c.PersonalizationSegmentId == control.PersonalizationSegmentId));
            }
            else
              controlData = this.GetOverridingControlForPage(state.Id, state.PageId);
            if (controlData != null)
              return controlData;
          }
          return control;
        case DesignMediaType.Template:
          Telerik.Sitefinity.Pages.Model.ControlData templateControl = manager.GetControl<Telerik.Sitefinity.Pages.Model.ControlData>(state.Id);
          if (templateControl is Telerik.Sitefinity.Pages.Model.TemplateControl)
          {
            if (templateControl.PersonalizationMasterId != Guid.Empty)
            {
              Telerik.Sitefinity.Pages.Model.ControlData control2 = manager.GetControl<Telerik.Sitefinity.Pages.Model.ControlData>(templateControl.PersonalizationMasterId);
              Telerik.Sitefinity.Pages.Model.ControlData controlForTemplate = this.GetOverridingControlForTemplate(control2.BaseControlId != Guid.Empty ? control2.BaseControlId : control2.Id, state.PageId);
              if (controlForTemplate != null)
                controlData = controlForTemplate.PersonalizedControls.FirstOrDefault<Telerik.Sitefinity.Pages.Model.ControlData>((Func<Telerik.Sitefinity.Pages.Model.ControlData, bool>) (c => c.PersonalizationSegmentId == templateControl.PersonalizationSegmentId));
            }
            else
              controlData = this.GetOverridingControlForTemplate(state.Id, state.PageId);
            if (controlData != null)
              return controlData;
          }
          return templateControl;
        case DesignMediaType.Form:
          return (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControl<FormDraftControl>(state.Id);
        default:
          throw new ArgumentException();
      }
    }

    private Telerik.Sitefinity.Pages.Model.ControlData GetOverridingControlForPage(
      Guid controlId,
      Guid pageId)
    {
      PageManager manager = PageManager.GetManager();
      Telerik.Sitefinity.Pages.Model.ControlData overridingControlForPage = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControls<PageDraftControl>().Where<PageDraftControl>((Expression<Func<PageDraftControl, bool>>) (c => c.Page.Id == pageId && (c.Id == controlId || c.BaseControlId == controlId))).FirstOrDefault<PageDraftControl>();
      if (overridingControlForPage == null)
      {
        PageDraft pageDraft = manager.GetDrafts<PageDraft>().Where<PageDraft>((Expression<Func<PageDraft, bool>>) (a => a.Id == pageId)).FirstOrDefault<PageDraft>();
        if (pageDraft != null)
        {
          PageTemplate pageTemplate = manager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (c => c.Id == pageDraft.TemplateId)).FirstOrDefault<PageTemplate>();
          for (PageTemplate iter = pageTemplate; iter != null; iter = iter.ParentTemplate)
          {
            overridingControlForPage = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControls<Telerik.Sitefinity.Pages.Model.TemplateControl>().Where<Telerik.Sitefinity.Pages.Model.TemplateControl>((Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>>) (c => c.Page.Id == iter.Id && (c.Id == controlId || c.BaseControlId == controlId))).FirstOrDefault<Telerik.Sitefinity.Pages.Model.TemplateControl>();
            if (overridingControlForPage != null)
              break;
          }
        }
      }
      return overridingControlForPage;
    }

    private Telerik.Sitefinity.Pages.Model.ControlData GetOverridingControlForTemplate(
      Guid controlId,
      Guid pageId)
    {
      PageManager manager = PageManager.GetManager();
      Telerik.Sitefinity.Pages.Model.ControlData controlForTemplate = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControls<TemplateDraftControl>().Where<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (c => c.Page.Id == pageId && (c.Id == controlId || c.BaseControlId == controlId))).FirstOrDefault<TemplateDraftControl>();
      if (controlForTemplate == null)
      {
        TemplateDraft templateDraft = manager.GetDrafts<TemplateDraft>().Where<TemplateDraft>((Expression<Func<TemplateDraft, bool>>) (a => a.Id == pageId)).FirstOrDefault<TemplateDraft>();
        if (templateDraft == null)
          return (Telerik.Sitefinity.Pages.Model.ControlData) null;
        PageTemplate iter = templateDraft.ParentTemplate;
        if (iter != null)
          iter = iter.ParentTemplate;
        for (; iter != null; iter = iter.ParentTemplate)
        {
          controlForTemplate = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControls<Telerik.Sitefinity.Pages.Model.TemplateControl>().Where<Telerik.Sitefinity.Pages.Model.TemplateControl>((Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>>) (c => c.Page.Id == iter.Id && (c.Id == controlId || c.BaseControlId == controlId))).FirstOrDefault<Telerik.Sitefinity.Pages.Model.TemplateControl>();
          if (controlForTemplate != null)
            break;
        }
      }
      return controlForTemplate;
    }

    /// <summary>Makes a duplicate copy of the provided control.</summary>
    /// <param name="state">The state.</param>
    /// <param name="manager">The manager.</param>
    protected virtual Telerik.Sitefinity.Pages.Model.ControlData DuplicateControl(
      ZoneEditorWebServiceArgs state,
      IControlManager manager,
      bool isLayoutControl)
    {
      Telerik.Sitefinity.Pages.Model.ControlData orgCtrl = (Telerik.Sitefinity.Pages.Model.ControlData) null;
      IControlsContainer draft;
      switch (state.MediaType)
      {
        case DesignMediaType.Page:
        case DesignMediaType.NewsletterCampaign:
        case DesignMediaType.NewsletterTemplate:
          orgCtrl = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControls<PageDraftControl>().Where<PageDraftControl>((Expression<Func<PageDraftControl, bool>>) (c => c.Id == state.Id)).FirstOrDefault<PageDraftControl>();
          if (orgCtrl == null)
            orgCtrl = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControls<PageDraftControl>().FirstOrDefault<PageDraftControl>((Expression<Func<PageDraftControl, bool>>) (x => x.BaseControlId == state.Id && x.Page.Id == state.PageId));
          if (orgCtrl == null)
            orgCtrl = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControls<TemplateDraftControl>().Where<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (c => c.Id == state.Id)).FirstOrDefault<TemplateDraftControl>();
          if (orgCtrl == null)
            orgCtrl = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControls<TemplateDraftControl>().FirstOrDefault<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (x => x.BaseControlId == state.Id && x.Page.Id == state.PageId));
          if (orgCtrl == null)
            orgCtrl = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControls<Telerik.Sitefinity.Pages.Model.TemplateControl>().Where<Telerik.Sitefinity.Pages.Model.TemplateControl>((Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>>) (c => c.Id == state.Id)).FirstOrDefault<Telerik.Sitefinity.Pages.Model.TemplateControl>();
          draft = (IControlsContainer) ((PageManager) manager).GetDraft<PageDraft>(state.PageId);
          break;
        case DesignMediaType.Template:
          orgCtrl = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControls<TemplateDraftControl>().FirstOrDefault<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (c => c.Id == state.Id));
          if (orgCtrl == null)
            orgCtrl = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControls<TemplateDraftControl>().FirstOrDefault<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (x => x.BaseControlId == state.Id && x.Page.Id == state.PageId));
          if (orgCtrl == null)
            orgCtrl = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControls<PageDraftControl>().Where<PageDraftControl>((Expression<Func<PageDraftControl, bool>>) (c => c.Id == state.Id)).FirstOrDefault<PageDraftControl>();
          if (orgCtrl == null)
            orgCtrl = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControls<Telerik.Sitefinity.Pages.Model.TemplateControl>().Where<Telerik.Sitefinity.Pages.Model.TemplateControl>((Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>>) (c => c.Id == state.Id)).FirstOrDefault<Telerik.Sitefinity.Pages.Model.TemplateControl>();
          draft = (IControlsContainer) ((PageManager) manager).GetDraft<TemplateDraft>(state.PageId);
          break;
        case DesignMediaType.Form:
          orgCtrl = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControl<FormDraftControl>(state.Id);
          draft = (IControlsContainer) ((FormsManager) manager).GetDraft(state.PageId);
          break;
        default:
          throw new ArgumentException();
      }
      if (orgCtrl == null)
        throw new ArgumentException("Can not find the control", nameof (state));
      if (orgCtrl.PersonalizationMasterId != Guid.Empty)
        orgCtrl = manager.GetControl<Telerik.Sitefinity.Pages.Model.ControlData>(orgCtrl.PersonalizationMasterId);
      if (draft is DraftData)
        ++((DraftData) draft).Version;
      if (string.IsNullOrEmpty(state.ControlType))
        state.ControlType = orgCtrl.ObjectType;
      state.SiblingId = orgCtrl.Id;
      Telerik.Sitefinity.Pages.Model.ControlData newCtrl = this.CreateNewControl(state, manager, isLayoutControl, new Guid?(orgCtrl.BaseControlId));
      string str = newCtrl.GetProperties(true).First<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ID")).Value;
      manager.CopyControl(orgCtrl, newCtrl);
      if (orgCtrl.BaseControlId != Guid.Empty)
      {
        Telerik.Sitefinity.Pages.Model.TemplateControl templateControl = manager.GetControls<Telerik.Sitefinity.Pages.Model.TemplateControl>().Where<Telerik.Sitefinity.Pages.Model.TemplateControl>((Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>>) (c => c.Id == orgCtrl.BaseControlId)).FirstOrDefault<Telerik.Sitefinity.Pages.Model.TemplateControl>();
        newCtrl.PlaceHolder = templateControl.PlaceHolder;
      }
      newCtrl.OriginalControlId = Guid.Empty;
      newCtrl.Editable = false;
      newCtrl.IsOverridedControl = false;
      newCtrl.BaseControlId = Guid.Empty;
      ControlProperty controlProperty = newCtrl.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (x => x.Name == "ID"));
      if (controlProperty != null)
        controlProperty.Value = str;
      IEnumerable<Telerik.Sitefinity.Pages.Model.ControlData> personalizedControls = (IEnumerable<Telerik.Sitefinity.Pages.Model.ControlData>) orgCtrl.PersonalizedControls;
      if (personalizedControls.Count<Telerik.Sitefinity.Pages.Model.ControlData>() > 0)
      {
        PageManager pageManager = (PageManager) manager;
        if (newCtrl is PageDraftControl)
        {
          List<PageDraftControl> list = newCtrl.PersonalizedControls.Cast<PageDraftControl>().ToList<PageDraftControl>();
          pageManager.CopyControls<Telerik.Sitefinity.Pages.Model.ControlData, PageDraftControl>(personalizedControls, (IList<PageDraftControl>) list, (CultureInfo) null, (CultureInfo) null, CopyDirection.Unspecified);
          list.ForEach((Action<PageDraftControl>) (t => t.PersonalizationMasterId = newCtrl.Id));
        }
        else if (newCtrl is TemplateDraftControl)
        {
          List<TemplateDraftControl> list = newCtrl.PersonalizedControls.Cast<TemplateDraftControl>().ToList<TemplateDraftControl>();
          pageManager.CopyControls<Telerik.Sitefinity.Pages.Model.ControlData, TemplateDraftControl>(personalizedControls, (IList<TemplateDraftControl>) list, (CultureInfo) null, (CultureInfo) null, CopyDirection.Unspecified);
          list.ForEach((Action<TemplateDraftControl>) (t => t.PersonalizationMasterId = newCtrl.Id));
        }
      }
      manager.SaveChanges();
      state.Id = newCtrl.Id;
      return newCtrl;
    }

    protected Telerik.Sitefinity.Pages.Model.ControlData GetPagePlaceholder(
      PageManager manager,
      PageDraft page,
      string placeholderName)
    {
      Telerik.Sitefinity.Pages.Model.ControlData pagePlaceholder = (Telerik.Sitefinity.Pages.Model.ControlData) null;
      foreach (PageDraftControl control in (IEnumerable<PageDraftControl>) page.Controls)
      {
        if (control.IsLayoutControl && ((IEnumerable<string>) control.PlaceHolders).Contains<string>(placeholderName, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
        {
          pagePlaceholder = (Telerik.Sitefinity.Pages.Model.ControlData) control;
          break;
        }
      }
      if (pagePlaceholder == null && page.TemplateId != Guid.Empty && page.TemplateId != Guid.Empty)
      {
        for (PageTemplate pageTemplate = manager.GetTemplate(page.TemplateId); pageTemplate != null; pageTemplate = pagePlaceholder == null ? pageTemplate.ParentTemplate : (PageTemplate) null)
        {
          foreach (Telerik.Sitefinity.Pages.Model.TemplateControl control in (IEnumerable<Telerik.Sitefinity.Pages.Model.TemplateControl>) pageTemplate.Controls)
          {
            if (control.IsLayoutControl && ((IEnumerable<string>) control.PlaceHolders).Contains<string>(placeholderName, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
            {
              pagePlaceholder = (Telerik.Sitefinity.Pages.Model.ControlData) control;
              break;
            }
          }
        }
      }
      return pagePlaceholder;
    }

    protected Telerik.Sitefinity.Pages.Model.ControlData GetTemplatePlaceholder(
      PageManager manager,
      TemplateDraft template,
      string placeholderName)
    {
      Telerik.Sitefinity.Pages.Model.ControlData templatePlaceholder = (Telerik.Sitefinity.Pages.Model.ControlData) null;
      foreach (TemplateDraftControl control in (IEnumerable<TemplateDraftControl>) template.Controls)
      {
        if (control.IsLayoutControl && ((IEnumerable<string>) control.PlaceHolders).Contains<string>(placeholderName, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
        {
          templatePlaceholder = (Telerik.Sitefinity.Pages.Model.ControlData) control;
          break;
        }
      }
      if (templatePlaceholder == null)
      {
        for (PageTemplate pageTemplate = template.ParentTemplate; pageTemplate != null; pageTemplate = templatePlaceholder == null ? pageTemplate.ParentTemplate : (PageTemplate) null)
        {
          foreach (Telerik.Sitefinity.Pages.Model.TemplateControl control in (IEnumerable<Telerik.Sitefinity.Pages.Model.TemplateControl>) pageTemplate.Controls)
          {
            if (control.IsLayoutControl && ((IEnumerable<string>) control.PlaceHolders).Contains<string>(placeholderName, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
            {
              templatePlaceholder = (Telerik.Sitefinity.Pages.Model.ControlData) control;
              break;
            }
          }
        }
      }
      return templatePlaceholder;
    }

    protected Telerik.Sitefinity.Pages.Model.ControlData GetFormPlaceholder(
      FormsManager manager,
      FormDraft form,
      string placeholderName)
    {
      Telerik.Sitefinity.Pages.Model.ControlData formPlaceholder = (Telerik.Sitefinity.Pages.Model.ControlData) null;
      foreach (FormDraftControl control in (IEnumerable<FormDraftControl>) form.Controls)
      {
        if (control.IsLayoutControl && ((IEnumerable<string>) control.PlaceHolders).Contains<string>(placeholderName, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
        {
          formPlaceholder = (Telerik.Sitefinity.Pages.Model.ControlData) control;
          break;
        }
      }
      return formPlaceholder;
    }

    /// <summary>Creates the new control.</summary>
    /// <param name="state">The state.</param>
    /// <param name="manager">The manager.</param>
    protected virtual Telerik.Sitefinity.Pages.Model.ControlData CreateNewControl(
      ZoneEditorWebServiceArgs state,
      IControlManager manager,
      bool isLayoutControl)
    {
      return this.CreateNewControl(state, manager, isLayoutControl, new Guid?());
    }

    private Telerik.Sitefinity.Pages.Model.ControlData CreateNewControl(
      ZoneEditorWebServiceArgs state,
      IControlManager manager,
      bool isLayoutControl,
      Guid? baseControlId = null)
    {
      bool isBackendObject = false;
      IControlsContainer draft;
      Telerik.Sitefinity.Pages.Model.ControlData control1;
      switch (state.MediaType)
      {
        case DesignMediaType.Page:
        case DesignMediaType.NewsletterCampaign:
        case DesignMediaType.NewsletterTemplate:
          draft = (IControlsContainer) ((PageManager) manager).GetDraft<PageDraft>(state.PageId);
          bool isBackend1 = ((PageDraft) draft).ParentPage.NavigationNode.IsBackend;
          control1 = (Telerik.Sitefinity.Pages.Model.ControlData) manager.CreateControl<PageDraftControl>(isBackend1);
          break;
        case DesignMediaType.Template:
          draft = (IControlsContainer) ((PageManager) manager).GetDraft<TemplateDraft>(state.PageId);
          bool isBackend2 = ((TemplateDraft) draft).ParentTemplate.IsBackend;
          control1 = (Telerik.Sitefinity.Pages.Model.ControlData) manager.CreateControl<TemplateDraftControl>(isBackend2);
          break;
        case DesignMediaType.Form:
          draft = (IControlsContainer) ((FormsManager) manager).GetDraft(state.PageId);
          control1 = (Telerik.Sitefinity.Pages.Model.ControlData) manager.CreateControl<FormDraftControl>(isBackendObject);
          break;
        default:
          throw new ArgumentException();
      }
      if (draft is DraftData)
        ++((DraftData) draft).Version;
      control1.ObjectType = state.ControlType;
      control1.PlaceHolder = state.PlaceholderId;
      control1.SiblingId = state.SiblingId;
      control1.IsLayoutControl = isLayoutControl;
      control1.SupportedPermissionSets = isLayoutControl ? Telerik.Sitefinity.Pages.Model.ControlData.LayoutPermissionSets : Telerik.Sitefinity.Pages.Model.ControlData.ControlPermissionSets;
      control1.Editable = false;
      control1.IsOverridedControl = false;
      control1.BaseControlId = Guid.Empty;
      control1.SetDefaultPermissions(manager);
      control1.Caption = !string.IsNullOrEmpty(state.ClassId) ? Res.Get(state.ClassId, state.Title) : state.Title;
      manager.SetControlId(draft, (ObjectData) control1);
      if (isLayoutControl)
      {
        ControlProperty property = manager.CreateProperty();
        property.Name = "Layout";
        property.Value = state.LayoutTemlpate;
        control1.Properties.Add(property);
      }
      if (control1.SiblingId != Guid.Empty)
      {
        foreach (Telerik.Sitefinity.Pages.Model.ControlData control2 in draft.Controls)
        {
          if (!(control2.SiblingId == control1.SiblingId))
          {
            if (baseControlId.HasValue)
            {
              Guid? nullable = baseControlId;
              Guid empty = Guid.Empty;
              if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != empty ? 1 : 0) : 0) : 1) != 0)
              {
                nullable = baseControlId;
                Guid siblingId = control2.SiblingId;
                if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == siblingId ? 1 : 0) : 1) : 0) == 0)
                  continue;
              }
              else
                continue;
            }
            else
              continue;
          }
          control2.SiblingId = control1.Id;
          break;
        }
      }
      else
      {
        Guid id = Guid.Empty;
        string plcHldr = control1.PlaceHolder;
        Telerik.Sitefinity.Pages.Model.ControlData controlData = draft.Controls.Where<Telerik.Sitefinity.Pages.Model.ControlData>((Func<Telerik.Sitefinity.Pages.Model.ControlData, bool>) (c => c.PlaceHolder != null && c.PlaceHolder.Equals(plcHldr, StringComparison.OrdinalIgnoreCase) && c.SiblingId == id)).SingleOrDefault<Telerik.Sitefinity.Pages.Model.ControlData>();
        if (controlData != null)
          controlData.SiblingId = control1.Id;
      }
      switch (state.MediaType)
      {
        case DesignMediaType.Page:
        case DesignMediaType.NewsletterCampaign:
        case DesignMediaType.NewsletterTemplate:
          ((PageDraft) draft).Controls.Add((PageDraftControl) control1);
          break;
        case DesignMediaType.Template:
          ((TemplateDraft) draft).Controls.Add((TemplateDraftControl) control1);
          break;
        case DesignMediaType.Form:
          FormDraft formDraft = (FormDraft) draft;
          if (!control1.IsLayoutControl)
          {
            string submitButtonType = typeof (FormSubmitButton).ToString();
            bool flag = formDraft.Controls.Where<FormDraftControl>((Func<FormDraftControl, bool>) (c => c.ObjectType.Contains(submitButtonType))).Count<FormDraftControl>() == 0 && !control1.ObjectType.Contains(submitButtonType);
            state.Attributes.Add("addSubmit", flag.ToString());
            if (flag)
              state.Attributes.Add("formSubmitType", submitButtonType);
          }
          FormDraftControl formControl = (FormDraftControl) control1;
          ControlProperty controlProperty = formControl.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ID"));
          if (controlProperty != null)
            state.Attributes.Add("controlKey", controlProperty.Value);
          if (formControl.IsFieldHidden())
            state.Attributes.Add("sf-hidden-field-label", Res.Get<FormsResources>().Hidden);
          formDraft.Controls.Add(formControl);
          break;
        default:
          throw new ArgumentException();
      }
      this.PresetControlProperties(state, control1, manager);
      Type type = TypeResolutionService.ResolveType(ObjectFactory.Resolve<IControlBehaviorResolver>().GetBehaviorObjectType(control1), false);
      if (type != (Type) null)
        state.Attributes.Add("behaviourobjecttype", type.FullName);
      Type controlType = ControlManager<PageDataProvider>.GetControlType((ObjectData) control1, false);
      if (controlType != (Type) null)
      {
        RequireScriptManagerAttribute attribute = (RequireScriptManagerAttribute) TypeDescriptor.GetAttributes(controlType)[typeof (RequireScriptManagerAttribute)];
        if (typeof (IScriptControl).IsAssignableFrom(controlType) || attribute != null && attribute.Require)
        {
          switch (state.MediaType)
          {
            case DesignMediaType.Page:
              ((PageDraft) draft).IncludeScriptManager = true;
              break;
            case DesignMediaType.Template:
              ((TemplateDraft) draft).IncludeScriptManager = true;
              break;
            case DesignMediaType.Form:
            case DesignMediaType.NewsletterCampaign:
            case DesignMediaType.NewsletterTemplate:
              break;
            default:
              throw new ArgumentException();
          }
        }
        control1.IsDataSource = typeof (IDataSource).IsAssignableFrom(controlType);
      }
      manager.SaveChanges();
      state.Id = control1.Id;
      return control1;
    }

    private void PresetControlProperties(
      ZoneEditorWebServiceArgs state,
      Telerik.Sitefinity.Pages.Model.ControlData control,
      IControlManager manager)
    {
      if (state.Parameters == null || state.Parameters.Count <= 0)
        return;
      object obj1 = (object) null;
      switch (state.MediaType)
      {
        case DesignMediaType.Page:
        case DesignMediaType.Template:
        case DesignMediaType.NewsletterCampaign:
        case DesignMediaType.NewsletterTemplate:
          obj1 = (object) (manager as PageManager);
          break;
        case DesignMediaType.Form:
          obj1 = (object) (manager as FormsManager);
          break;
      }
      // ISSUE: reference to a compiler-generated field
      if (ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "CreateDefaultInstance", (IEnumerable<Type>) null, typeof (ZoneEditorService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, Type, object> target = ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, Type, object>> p1 = ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ObjectDataUtility", typeof (ZoneEditorService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__0.Target((CallSite) ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__0, obj1);
      Type type = TypeResolutionService.ResolveType(control.ObjectType);
      object obj3 = target((CallSite) p1, obj2, type);
      // ISSUE: reference to a compiler-generated field
      if (ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ID", typeof (ZoneEditorService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__2.Target((CallSite) ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__2, obj3, control.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ID")).Value);
      // ISSUE: reference to a compiler-generated field
      if (ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__3 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetProperties", (IEnumerable<Type>) null, typeof (ZoneEditorService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      PropertyDescriptorCollection descriptorCollection = ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__3.Target((CallSite) ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__3, typeof (TypeDescriptor), obj3) as PropertyDescriptorCollection;
      control.SetPersistanceStrategy();
      if (!state.ModuleName.IsNullOrWhitespace())
      {
        // ISSUE: reference to a compiler-generated field
        if (ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__4 = CallSite<Action<CallSite, ZoneEditorService, string, string, Telerik.Sitefinity.Pages.Model.ControlData, PropertyDescriptorCollection, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetControlValue", (IEnumerable<Type>) null, typeof (ZoneEditorService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[7]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__4.Target((CallSite) ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__4, this, "ModuleName", state.ModuleName, control, descriptorCollection, obj3, obj1);
      }
      foreach (KeyValuePair<string, string> parameter in state.Parameters)
      {
        // ISSUE: reference to a compiler-generated field
        if (ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__5 = CallSite<Action<CallSite, ZoneEditorService, string, string, Telerik.Sitefinity.Pages.Model.ControlData, PropertyDescriptorCollection, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetControlValue", (IEnumerable<Type>) null, typeof (ZoneEditorService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[7]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__5.Target((CallSite) ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__5, this, parameter.Key, parameter.Value, control, descriptorCollection, obj3, obj1);
      }
      if ((state.Parameters.Count <= 0 || !(obj1 is PageManager)) && !(obj1 is FormsManager))
        return;
      // ISSUE: reference to a compiler-generated field
      if (ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__6 = CallSite<Action<CallSite, object, object, Telerik.Sitefinity.Pages.Model.ControlData, CultureInfo>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ReadProperties", (IEnumerable<Type>) null, typeof (ZoneEditorService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__6.Target((CallSite) ZoneEditorService.\u003C\u003Eo__20.\u003C\u003Ep__6, obj1, obj3, control, SystemManager.CurrentContext.Culture);
    }

    private void SetControlValue(
      string propName,
      string propValue,
      Telerik.Sitefinity.Pages.Model.ControlData control,
      PropertyDescriptorCollection clrProperties,
      object controlInstance,
      object controlManager)
    {
      PropertyDescriptor propertyDescriptor = clrProperties.Find(propName, true);
      if (propertyDescriptor != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__0 = CallSite<Action<CallSite, Type, object, PropertyDescriptor, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetValue", (IEnumerable<Type>) null, typeof (ZoneEditorService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__0.Target((CallSite) ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__0, typeof (WcfPropertyManager), controlInstance, propertyDescriptor, propValue);
        // ISSUE: reference to a compiler-generated field
        if (ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__1 = CallSite<Action<CallSite, object, object, Telerik.Sitefinity.Pages.Model.ControlData, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ReadProperty", (IEnumerable<Type>) null, typeof (ZoneEditorService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[5]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__1.Target((CallSite) ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__1, controlManager, controlInstance, control, propName, (object) null);
      }
      else
      {
        if (!(propName != "ModuleName"))
          return;
        try
        {
          WcfPropertyManager wcfPropertyManager = new WcfPropertyManager();
          WcfControlProperty wcfControlProperty = new WcfControlProperty();
          wcfControlProperty.PropertyPath = propName.Replace('.', '/');
          wcfControlProperty.PropertyValue = propValue;
          if (control.ObjectType == "Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy" || control.ObjectType.StartsWith("Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.MvcWidgetProxy"))
            wcfControlProperty.PropertyPath = "Settings/" + wcfControlProperty.PropertyPath;
          // ISSUE: reference to a compiler-generated field
          if (ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__2 = CallSite<Action<CallSite, WcfPropertyManager, WcfControlProperty, object, Telerik.Sitefinity.Pages.Model.ControlData>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetProperty", (IEnumerable<Type>) null, typeof (ZoneEditorService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__2.Target((CallSite) ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__2, wcfPropertyManager, wcfControlProperty, controlInstance, control);
        }
        catch (System.InvalidOperationException ex)
        {
          // ISSUE: reference to a compiler-generated field
          if (ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__3 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__3 = CallSite<Func<CallSite, IControlBehaviorResolver, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetBehaviorObject", (IEnumerable<Type>) null, typeof (ZoneEditorService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__3.Target((CallSite) ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__3, ControlUtilities.BehaviorResolver, controlInstance);
          // ISSUE: reference to a compiler-generated field
          if (ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__5 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__5 = CallSite<Action<CallSite, Type, object, ConfigurationPolicy>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Write", (IEnumerable<Type>) null, typeof (ZoneEditorService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Action<CallSite, Type, object, ConfigurationPolicy> target = ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__5.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Action<CallSite, Type, object, ConfigurationPolicy>> p5 = ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__5;
          Type type = typeof (Log);
          // ISSUE: reference to a compiler-generated field
          if (ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__4 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__4 = CallSite<Func<CallSite, Type, string, string, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Format", (IEnumerable<Type>) null, typeof (ZoneEditorService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj2 = ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__4.Target((CallSite) ZoneEditorService.\u003C\u003Eo__21.\u003C\u003Ep__4, typeof (string), "Can't find property {0}. Check the parameters collection for the widget registration of {1} from Advanced settings->Toolbox.", propName, obj1);
          target((CallSite) p5, type, obj2, ConfigurationPolicy.ErrorLog);
        }
      }
    }

    /// <summary>Deletes the control.</summary>
    /// <param name="state">The state.</param>
    /// <param name="manager">The manager.</param>
    protected virtual void DeleteControl(ZoneEditorWebServiceArgs state, IControlManager manager)
    {
      Telerik.Sitefinity.Pages.Model.ControlData control;
      IControlsContainer draft;
      switch (state.MediaType)
      {
        case DesignMediaType.Page:
        case DesignMediaType.NewsletterCampaign:
        case DesignMediaType.NewsletterTemplate:
          PageManager pageManager = (PageManager) manager;
          control = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControl<PageDraftControl>(state.Id);
          if (control.PersonalizationMasterId != Guid.Empty)
            control = (Telerik.Sitefinity.Pages.Model.ControlData) pageManager.GetControl<PageDraftControl>(control.PersonalizationMasterId);
          Guid id1 = control.Id;
          IQueryable<PageDraftControl> controls1 = manager.GetControls<PageDraftControl>();
          Expression<Func<PageDraftControl, bool>> predicate1 = (Expression<Func<PageDraftControl, bool>>) (c => c.SiblingId == id1);
          foreach (Telerik.Sitefinity.Pages.Model.ControlData controlData in (IEnumerable<PageDraftControl>) controls1.Where<PageDraftControl>(predicate1))
            controlData.SiblingId = control.SiblingId;
          draft = (IControlsContainer) pageManager.GetDraft<PageDraft>(state.PageId);
          break;
        case DesignMediaType.Template:
          control = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControl<TemplateDraftControl>(state.Id);
          if (control.PersonalizationMasterId != Guid.Empty)
            control = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControl<TemplateDraftControl>(control.PersonalizationMasterId);
          Guid id2 = control.Id;
          IQueryable<TemplateDraftControl> controls2 = manager.GetControls<TemplateDraftControl>();
          Expression<Func<TemplateDraftControl, bool>> predicate2 = (Expression<Func<TemplateDraftControl, bool>>) (c => c.SiblingId == id2);
          foreach (Telerik.Sitefinity.Pages.Model.ControlData controlData in (IEnumerable<TemplateDraftControl>) controls2.Where<TemplateDraftControl>(predicate2))
            controlData.SiblingId = control.SiblingId;
          draft = (IControlsContainer) ((PageManager) manager).GetDraft<TemplateDraft>(state.PageId);
          break;
        case DesignMediaType.Form:
          control = (Telerik.Sitefinity.Pages.Model.ControlData) manager.GetControl<FormDraftControl>(state.Id);
          Guid id3 = control.Id;
          IQueryable<FormDraftControl> controls3 = manager.GetControls<FormDraftControl>();
          Expression<Func<FormDraftControl, bool>> predicate3 = (Expression<Func<FormDraftControl, bool>>) (c => c.SiblingId == id3);
          foreach (Telerik.Sitefinity.Pages.Model.ControlData controlData in (IEnumerable<FormDraftControl>) controls3.Where<FormDraftControl>(predicate3))
            controlData.SiblingId = control.SiblingId;
          draft = (IControlsContainer) ((FormsManager) manager).GetDraft(state.PageId);
          break;
        default:
          throw new ArgumentException();
      }
      List<Telerik.Sitefinity.Pages.Model.ControlData> list = new List<Telerik.Sitefinity.Pages.Model.ControlData>();
      if (control.IsLayoutControl)
        this.DeleteChildControlsRecursive(draft, control, (IList<Telerik.Sitefinity.Pages.Model.ControlData>) list);
      foreach (Telerik.Sitefinity.Pages.Model.ControlData controlData in list)
        manager.Delete(controlData);
      if (draft is DraftData)
        ++((DraftData) draft).Version;
      manager.Delete(control);
      manager.SaveChanges();
    }

    /// <summary>Deletes all child controls recursively.</summary>
    protected virtual void DeleteChildControlsRecursive(
      IControlsContainer pageDraft,
      Telerik.Sitefinity.Pages.Model.ControlData parentControl,
      IList<Telerik.Sitefinity.Pages.Model.ControlData> list)
    {
      string[] cols = parentControl.PlaceHolders;
      foreach (Telerik.Sitefinity.Pages.Model.ControlData parentControl1 in pageDraft.Controls.Where<Telerik.Sitefinity.Pages.Model.ControlData>((Func<Telerik.Sitefinity.Pages.Model.ControlData, bool>) (c => ((IEnumerable<string>) cols).Contains<string>(c.PlaceHolder, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))))
      {
        if (parentControl1.IsLayoutControl)
          this.DeleteChildControlsRecursive(pageDraft, parentControl1, list);
        list.Add(parentControl1);
      }
    }

    /// <summary>
    /// Publishes the page draft and returns true if the draft has been published; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft ID.</param>
    /// <returns></returns>
    public bool PublishPageDraft(string draftId)
    {
      if (!ControlUtilities.IsGuid(draftId))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
      try
      {
        PageManager manager1 = PageManager.GetManager();
        PageDraft draft = manager1.GetDraft<PageDraft>(new Guid(draftId));
        Guid parentId = draft.ParentId;
        manager1.PublishPageDraft(draft, SystemManager.CurrentContext.Culture);
        VersionManager manager2 = VersionManager.GetManager();
        manager2.CreateVersion((object) draft, parentId, true);
        manager1.SaveChanges();
        manager2.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
    }

    /// <summary>
    /// Saves the page draft and returns true if the draft has been saved; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="pageId">page id </param>
    /// <param name="workflowOperation">workflow operation name</param>
    /// <param name="publicationDate"> publication date</param>
    /// <param name="expirationDate"> expiration date</param>
    /// <returns></returns>
    public bool SavePageDraft(WcfPageData pageData, string workflowOperation)
    {
      try
      {
        Dictionary<string, string> contextBag = new Dictionary<string, string>();
        if (workflowOperation == "Schedule")
        {
          contextBag.Add("PublicationDate", pageData.PublicationDate.ToIsoFormat());
          if (pageData.ExpirationDate.HasValue)
            contextBag.Add("ExpirationDate", pageData.ExpirationDate.Value.ToIsoFormat());
          PageManager manager = PageManager.GetManager();
          if (CommonMethods.TryUpdateItemBeforeWorkflowScheduleOperation((IDataItem) manager.GetPageNode(pageData.PageNodeId), (IDictionary<string, string>) contextBag))
            manager.SaveChanges();
        }
        if (!pageData.Note.IsNullOrWhitespace())
          contextBag.Add("Note", pageData.Note);
        WorkflowManager.MessageWorkflow(pageData.PageNodeId, typeof (PageNode), ManagerBase<PageDataProvider>.GetDefaultProviderName(), workflowOperation, false, contextBag);
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
      return true;
    }

    /// <summary>
    /// Discards the specified page draft and returns true if the draft has been discarded; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft ID.</param>
    /// <returns></returns>
    public bool DiscardPageDraft(string draftId)
    {
      try
      {
        Guid parsedDraftId;
        if (!Guid.TryParse(draftId, out parsedDraftId))
          throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
        SystemManager.CurrentHttpContext.Items[(object) "OptimizedCopy"] = (object) true;
        PageManager manager = PageManager.GetManager();
        PageData live = manager.GetDrafts<PageDraft>().Where<PageDraft>((Expression<Func<PageDraft, bool>>) (x => x.Id == parsedDraftId)).Select<PageDraft, PageData>((Expression<Func<PageDraft, PageData>>) (x => x.ParentPage)).FirstOrDefault<PageData>();
        if (live.PersonalizationMasterId != Guid.Empty)
          live = manager.GetPageData(live.PersonalizationMasterId);
        List<PageData> pageDataList = new List<PageData>();
        pageDataList.Add(live);
        if (live.IsPersonalized)
        {
          List<PageData> list = manager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == live.Id)).ToList<PageData>();
          pageDataList.AddRange((IEnumerable<PageData>) list);
        }
        foreach (PageData pageData in pageDataList)
        {
          PageDraft pageDraft = pageData.Drafts.FirstOrDefault<PageDraft>((Func<PageDraft, bool>) (x => !x.IsTempDraft));
          PageDraft targetDraft = pageData.Drafts.FirstOrDefault<PageDraft>((Func<PageDraft, bool>) (x => x.IsTempDraft));
          if (pageDraft != null)
          {
            if (targetDraft != null && targetDraft.Version != pageDraft.Version)
              manager.PagesLifecycle.Copy(pageDraft, targetDraft, ((CultureInfo) null).GetSitefinityCulture());
            else
              manager.SetMasterSynced(pageDraft);
          }
          pageData.LockedBy = Guid.Empty;
          pageData.SkipNotifyObjectChanged = true;
        }
        manager.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
    }

    public void SetPageLocalizationStrategy(
      string pageNodeId,
      string strategyString,
      bool copyData)
    {
      LocalizationStrategy result;
      if (!System.Enum.TryParse<LocalizationStrategy>(strategyString, out result))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid localization strategy: " + strategyString, (Exception) null);
      PageManager manager = PageManager.GetManager();
      PageNode pageNode = manager.GetPageNode(new Guid(pageNodeId));
      PageData pageData = pageNode.GetPageData();
      manager.InitializePageLocalizationStrategy(pageNode, result, copyData);
      if (pageData != null && result == LocalizationStrategy.Split)
        manager.UnlockPage(pageData.Id, false, true);
      manager.SaveChanges();
    }

    public void InitializeSplitPage(
      string targetNodeId,
      string sourceLanguageString,
      string targetLanguageString)
    {
      PageManager manager = PageManager.GetManager();
      PageNode pageNode = manager.GetPageNode(new Guid(targetNodeId));
      CultureInfo sourceCulture = !string.IsNullOrEmpty(sourceLanguageString) ? (!(sourceLanguageString == "none") ? new CultureInfo(sourceLanguageString) : (CultureInfo) null) : CultureInfo.InvariantCulture;
      CultureInfo targetCulture = (CultureInfo) null;
      if (string.IsNullOrEmpty(targetLanguageString))
      {
        PageData pageData = pageNode.GetPageData();
        if (pageData.Culture != null)
          targetCulture = new CultureInfo(pageData.Culture);
      }
      else
        targetCulture = !(targetLanguageString == "none") ? new CultureInfo(targetLanguageString) : (CultureInfo) null;
      manager.InitializeSplitPage(pageNode, sourceCulture, targetCulture);
      manager.SaveChanges();
    }

    public void SplitPage(string pageNodeId)
    {
      string transactionName = Guid.NewGuid().ToString();
      VersionManager manager1 = VersionManager.GetManager((string) null, transactionName);
      PageManager manager2 = PageManager.GetManager((string) null, transactionName);
      PageNode pageNode = manager2.GetPageNode(new Guid(pageNodeId));
      PageData pageData = pageNode.GetPageData();
      manager2.SplitSynchronizedPageData(pageNode, true);
      manager1.Provider.GetItem(pageData.Id).IsSyncedItem = false;
      TransactionManager.CommitTransaction(transactionName);
    }

    /// <summary>
    /// Publishes the page draft and returns true if the draft has been published; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft ID.</param>
    /// <returns></returns>
    public bool PublishTemplateDraft(string draftId)
    {
      if (!ControlUtilities.IsGuid(draftId))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
      PageManager manager = PageManager.GetManager();
      return this.PublishTemplateDraft(manager.GetDraft<TemplateDraft>(new Guid(draftId)), manager);
    }

    internal bool PublishTemplateDraft(TemplateDraft draft, PageManager pageMan)
    {
      try
      {
        Guid parentId = draft.ParentId;
        pageMan.PublishTemplateDraft(draft, SystemManager.CurrentContext.Culture);
        VersionManager manager = VersionManager.GetManager();
        manager.CreateVersion((object) draft, parentId, true);
        if (pageMan.TransactionName.IsNullOrEmpty())
          pageMan.SaveChanges();
        manager.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
    }

    /// <summary>
    /// Saves the template draft and returns true if the draft has been saved; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft pageId.</param>
    /// <returns></returns>
    public bool SaveTemplateDraft(string draftId)
    {
      Guid id = ControlUtilities.IsGuid(draftId) ? new Guid(draftId) : throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
      PageManager manager = PageManager.GetManager();
      return this.SaveTemplateDraft(manager.GetDraft<TemplateDraft>(id), manager);
    }

    internal bool SaveTemplateDraft(TemplateDraft draft, PageManager pageMan)
    {
      try
      {
        Guid parentId = draft.ParentId;
        pageMan.SaveTemplateDraft(draft.Id);
        VersionManager manager = VersionManager.GetManager();
        manager.CreateVersion((object) draft, parentId, false);
        if (pageMan.TransactionName.IsNullOrEmpty())
          pageMan.SaveChanges();
        manager.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
    }

    /// <summary>
    /// Discards the specified template draft and returns true if the draft has been discarded; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft ID.</param>
    /// <returns></returns>
    public bool DiscardTemplateDraft(string draftId)
    {
      if (!ControlUtilities.IsGuid(draftId))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
      try
      {
        Guid draftId1 = new Guid(draftId);
        PageManager manager = PageManager.GetManager();
        manager.DiscardTemplateDraft(draftId1);
        manager.SaveChanges();
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
      return true;
    }

    public bool ChangeTheme(string draftId, string themeName, bool isTemplate)
    {
      if (!ControlUtilities.IsGuid(draftId))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
      try
      {
        Guid guid = new Guid(draftId);
        PageManager manager = PageManager.GetManager();
        if (isTemplate)
        {
          ZoneEditorValidationExtensions.ValidateChange(guid, DesignMediaType.Template, nameof (ChangeTheme));
          manager.GetDraft<TemplateDraft>(guid).Themes.SetString(SystemManager.CurrentContext.Culture, themeName);
        }
        else
        {
          ZoneEditorValidationExtensions.ValidateChange(guid, DesignMediaType.Page, nameof (ChangeTheme));
          manager.GetDraft<PageDraft>(guid).Themes.SetString(SystemManager.CurrentContext.Culture, themeName);
        }
        manager.SaveChanges();
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
      return true;
    }

    /// <summary>
    /// Publishes the form draft and returns true if the draft has been published; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="formViewModel">The form ViewModel object.</param>
    /// <param name="draftId">The draft id.</param>
    /// <returns></returns>
    public bool PublishFormDraft(FormDescriptionViewModel formViewModel, string draftId)
    {
      if (formViewModel == null)
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid request format.", (Exception) null);
      if (!ControlUtilities.IsGuid(draftId))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
      try
      {
        FormsManager manager = FormsManager.GetManager();
        FormDraft draft = manager.GetDraft(new Guid(draftId));
        manager.CopyFormCommonData<FormDraftControl>(formViewModel, (IFormData<FormDraftControl>) draft);
        manager.PublishFormDraft(draft, SystemManager.CurrentContext.Culture);
        manager.SaveChanges(true);
        this.UpdateFormEmailSubscription(formViewModel);
        return true;
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
    }

    private void UpdateFormEmailSubscription(FormDescriptionViewModel formViewModel)
    {
      FormsManager manager = FormsManager.GetManager();
      string name = manager.Provider.Name;
      FormDescription form = manager.GetForm(formViewModel.Id);
      User currentUser = ZoneEditorService.GetCurrentUser();
      bool flag1 = FormsManager.CheckUserIsSubscribed(currentUser, form.Id, name);
      if (formViewModel.HasSubscription && !flag1)
        FormsManager.SubscribeUser(currentUser, form.Id, name);
      else if (!formViewModel.HasSubscription & flag1)
        FormsManager.UnsubscribeUser(currentUser, form.Id, name);
      bool flag2 = FormsManager.CheckUserIsSubscribed(currentUser, (Func<Guid>) (() => form.SubscriptionListIdAfterFormUpdate));
      if (formViewModel.HasSubscriptionAfterFormUpdate && !flag2)
        FormsManager.ManageUserSubscriptionAfterFormUpdate(true, currentUser, form.Id, name);
      else if (!formViewModel.HasSubscriptionAfterFormUpdate & flag2)
        FormsManager.ManageUserSubscriptionAfterFormUpdate(false, currentUser, form.Id, name);
      FormsExtensions.CreateSubscriptionList(form, form.SubscriptionListId, (IList<string>) formViewModel.SubscribedEmails, (Action<Guid>) (id =>
      {
        form.SubscriptionListId = id;
        manager.SaveChanges();
      }));
      FormsExtensions.CreateSubscriptionList(form, form.SubscriptionListIdAfterFormUpdate, (IList<string>) formViewModel.SubscribedEmailsAfterFormUpdate, (Action<Guid>) (id =>
      {
        form.SubscriptionListIdAfterFormUpdate = id;
        manager.SaveChanges();
      }));
    }

    private static User GetCurrentUser()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      return UserManager.GetManager(currentIdentity.MembershipProvider).GetUser(currentIdentity.Name);
    }

    /// <summary>
    /// Discards the specified form draft and returns true if the draft has been discarded; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft ID.</param>
    /// <returns></returns>
    public bool DiscardFormDraft(string draftId)
    {
      try
      {
        Guid draftId1 = ControlUtilities.IsGuid(draftId) ? new Guid(draftId) : throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
        FormsManager manager = FormsManager.GetManager();
        manager.DiscardFormDraft(draftId1);
        manager.SaveChanges();
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
      return true;
    }

    public bool SaveFormDraft(FormDescriptionViewModel formViewModel, string draftId)
    {
      if (formViewModel == null)
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid request format.", (Exception) null);
      if (!ControlUtilities.IsGuid(draftId))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
      bool flag = false;
      try
      {
        Guid id = new Guid(draftId);
        FormsManager manager = FormsManager.GetManager();
        FormDraft draft = manager.GetDraft(id);
        manager.CopyFormCommonData<FormDraftControl>(formViewModel, (IFormData<FormDraftControl>) draft);
        manager.SaveFormDraft(draft);
        manager.SaveChanges();
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
      return flag;
    }

    /// <summary>
    /// Take ownership of Page draft. If current user is not administrator throws InvalidOperationException
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft pageId.</param>
    public void TakePageOwnership(string draftId)
    {
      Guid pageDraftId = ControlUtilities.IsGuid(draftId) ? new Guid(draftId) : throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
      PageManager.GetManager().TakeDraftOwnership<PageDraft>(pageDraftId);
    }

    /// <summary>
    /// Take ownership of Template draft. If current user is not administrator throws InvalidOperationException
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft pageId.</param>
    public void TemplateTakeOwnership(string draftId)
    {
      Guid pageDraftId = ControlUtilities.IsGuid(draftId) ? new Guid(draftId) : throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
      PageManager.GetManager().TakeDraftOwnership<TemplateDraft>(pageDraftId);
    }

    /// <summary>
    /// Take ownership of Page data. If current user is not administrator throws InvalidOperationException
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="pageDataId">The page data Id.</param>
    public void UnlockPage(string pageDataId)
    {
      Guid pageDataId1 = ControlUtilities.IsGuid(pageDataId) ? new Guid(pageDataId) : throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
      PageManager manager = PageManager.GetManager();
      manager.UnlockPage(pageDataId1, false);
      manager.SaveChanges();
    }

    public void UnlockTemplate(string templateId)
    {
      Guid templateId1 = ControlUtilities.IsGuid(templateId) ? new Guid(templateId) : throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
      PageManager manager = PageManager.GetManager();
      manager.UnlockTemplate(templateId1, false);
      manager.SaveChanges();
    }

    public void UnlockForm(string formId)
    {
      Guid formId1 = ControlUtilities.IsGuid(formId) ? new Guid(formId) : throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) null);
      FormsManager manager = FormsManager.GetManager();
      manager.UnlockForm(formId1, false);
      manager.SaveChanges();
    }

    /// <summary>Changes the template of the page</summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="newTemplateId">The new template id.</param>
    public void ChangeTemplate(string draftId, string newTemplateId)
    {
      PageManager manager = PageManager.GetManager();
      PageDraft draft = manager.GetDraft<PageDraft>(new Guid(draftId));
      draft.TemplateId = new Guid(newTemplateId);
      PageTemplateHelper.CheckCreateOrMockAndReturnBasicEmptyTemplate(draft.TemplateId, true);
      manager.SaveChanges();
    }

    /// <summary>Changes the parent template of a template draft.</summary>
    /// <param name="draftId">The template draft id.</param>
    /// <param name="newTemplateId">The new template id.</param>
    /// <returns></returns>
    public bool ChangeParentTemplate(string draftId, string newTemplateId) => this.ChangeParentTemlateInternal(draftId, newTemplateId);

    private bool ChangeParentTemlateInternal(string draftId, string newTemplateId)
    {
      bool flag = false;
      PageManager manager = PageManager.GetManager();
      Guid id1 = new Guid(draftId);
      TemplateDraft draft = manager.GetDraft<TemplateDraft>(id1);
      if (draft != null && draft.ParentTemplate != null)
      {
        Guid id2 = new Guid(newTemplateId);
        if (id2 != Guid.Empty)
        {
          PageTemplate newTemplate;
          try
          {
            newTemplate = manager.GetTemplate(id2);
          }
          catch (Exception ex)
          {
            if (PageTemplateHelper.IsOnDemandTempalteId(id2))
            {
              switch (ex)
              {
                case ItemNotFoundException _:
                case NoSuchObjectException _:
                  newTemplate = PageTemplateHelper.CheckCreateOrMockAndReturnBasicEmptyTemplate(id2, true);
                  goto label_7;
              }
            }
            throw;
          }
label_7:
          if (newTemplate == null)
            throw new ArgumentException("There is no template with ID \"{0}\".".Arrange((object) id2));
          draft.SetParentBaseTemplate(newTemplate);
        }
        else
          draft.SetParentBaseTemplate((PageTemplate) null);
        manager.SaveChanges();
        flag = true;
      }
      return flag;
    }
  }
}
