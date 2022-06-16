// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Web.UI.Views.VersionHistoryDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Versioning.Web.UI.Views
{
  public class VersionHistoryDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Versioning.VersionHistoryDialog.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? VersionHistoryDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the top tool bar.</summary>
    /// <value>The top tool bar.</value>
    protected WidgetBar TopToolBar => this.Container.GetControl<WidgetBar>();

    internal RadWindowManager WindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    private HiddenField HfCompareVersionDialogUrl => this.Container.GetControl<HiddenField>("hfCompareVersionDialogUrl", true);

    private Literal BackToAllItemsLiteral => this.Container.GetControl<Literal>("backToAllItemsLiteral", true);

    private CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", true);

    private HiddenField HfItem => this.Container.GetControl<HiddenField>("hfItem", true);

    private HiddenField HfItemType => this.Container.GetControl<HiddenField>("hfItemType", true);

    private HiddenField HfIsEditable => this.Container.GetControl<HiddenField>("hfIsEditable", true);

    private HiddenField HfIsDeletable => this.Container.GetControl<HiddenField>("hfIsDeletable", true);

    private HiddenField HfCulture => this.Container.GetControl<HiddenField>("hfCulture", true);

    private HiddenField HfBackLabelLink => this.Container.GetControl<HiddenField>("hfBackLabelLink", true);

    private HiddenField HfRevertUrl => this.Container.GetControl<HiddenField>("hfRevertUrl", true);

    private HiddenField HfProvider => this.Container.GetControl<HiddenField>("hfProvider", true);

    protected HiddenField HfServiceUrl => this.Container.GetControl<HiddenField>("hfServiceUrl", true);

    protected HiddenField HfIsPages => this.Container.GetControl<HiddenField>("hfIsPages", true);

    private HiddenField IsAutoBind => this.Container.GetControl<HiddenField>("isAutoBind", true);

    private HtmlContainerControl HistoryKendoGrid => this.Container.GetControl<HtmlContainerControl>("historyKendoGrid", true);

    protected override void InitializeControls(GenericContainer container)
    {
      string str = RouteHelper.ResolveUrl("~/Sitefinity/Dialog/ContentViewEditDialog", UrlResolveOptions.Rooted);
      if (this.DisableComparisonForVersions)
      {
        for (int index = 0; index < this.CommandBar.Commands.Count; ++index)
        {
          if (this.CommandBar.Commands[index] is CommandToolboxItem && ((CommandToolboxItem) this.CommandBar.Commands[index]).CommandName == "compareversions")
          {
            this.CommandBar.Commands.RemoveAt(index);
            break;
          }
        }
      }
      string backLabelText = this.BackLabelText;
      if (!string.IsNullOrEmpty(backLabelText))
        this.BackToAllItemsLiteral.Text = backLabelText;
      this.HfCompareVersionDialogUrl.Value = str + "?ControlDefinitionName=" + this.ControlDefinitionName + "&ViewName=" + this.VersionComparisonViewName;
      this.HfServiceUrl.Value = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Versioning/HistoryService.svc");
      this.IsAutoBind.Value = this.AutoBindParam.ToString();
      if (!(this.ItemId != Guid.Empty))
        return;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        if (!string.IsNullOrEmpty(this.Culture))
        {
          try
          {
            SystemManager.CurrentContext.Culture = CultureInfo.GetCultureInfo(this.Culture);
            CultureInfo.CurrentCulture = AppSettings.CurrentSettings.DefaultBackendLanguage;
          }
          catch
          {
          }
        }
      }
      IManager mappedManager = ManagerBase.GetMappedManager(this.ItemType, this.ProviderName);
      Type type = TypeResolutionService.ResolveType(this.ItemType);
      Type itemType = type;
      Guid itemId = this.ItemId;
      object obj = mappedManager.GetItem(itemType, itemId);
      using (MemoryStream memoryStream = new MemoryStream())
      {
        if (obj is DynamicContent)
        {
          object objectToSerialize = new DynamicFieldsDataContractSurrogate().GetObjectToSerialize(obj, obj.GetType());
          new DataContractJsonSerializer(type, (IEnumerable<Type>) new Type[1]
          {
            objectToSerialize.GetType()
          }).WriteObject((Stream) memoryStream, objectToSerialize);
        }
        else
          new DataContractJsonSerializer(type).WriteObject((Stream) memoryStream, obj);
        this.HfItem.Value = Encoding.UTF8.GetString(memoryStream.ToArray());
      }
      this.HfItemType.Value = this.ItemType;
      this.HfIsEditable.Value = ContentViewModelBase.IsItemEditable(obj as ISecuredObject).ToString().ToLower();
      this.HfIsDeletable.Value = ContentViewModelBase.IsItemDeletable(obj as ISecuredObject).ToString().ToLower();
      this.HfCulture.Value = this.Culture;
      this.HfBackLabelLink.Value = this.BackLabelLink;
      this.HfRevertUrl.Value = this.RevertURL;
      this.HfProvider.Value = this.ProviderName;
      this.InitializeVersionPreviewDialog(obj);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.DialogManager).RegisterScriptControl<VersionHistoryDialog>(this);
    }

    protected virtual void InitializeVersionPreviewDialog(object item)
    {
      if (!Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>().ContentViewControls.ContainsKey(this.ControlDefinitionName) || !(ContentViewControlDefinition.Initialize(this.ControlDefinitionName).Views.First<IContentViewDefinition>((Func<IContentViewDefinition, bool>) (v => v.IsMasterView)) is MasterGridViewDefinition gridViewDefinition))
        return;
      IDialogDefinition dialogDefinition = gridViewDefinition.Dialogs.First<IDialogDefinition>((Func<IDialogDefinition, bool>) (d => d.OpenOnCommandName == "versionPreview"));
      RadWindow control = new RadWindow();
      control.ID = dialogDefinition.OpenOnCommandName;
      control.Behaviors = dialogDefinition.Behaviors;
      control.InitialBehaviors = dialogDefinition.InitialBehaviors;
      control.AutoSizeBehaviors = dialogDefinition.AutoSizeBehaviors;
      control.Width = dialogDefinition.Width;
      control.Height = dialogDefinition.Height;
      control.VisibleTitlebar = dialogDefinition.VisibleTitleBar;
      control.VisibleStatusbar = dialogDefinition.VisibleStatusBar;
      control.NavigateUrl = dialogDefinition.NavigateUrl + ("&provider=" + HttpUtility.UrlEncode(this.ProviderName)) + "&revertURL=" + HttpUtility.UrlEncode(this.RevertURL);
      control.Modal = dialogDefinition.IsModal;
      RadWindow radWindow1 = control;
      bool? nullable = dialogDefinition.ReloadOnShow;
      int num1;
      if (!nullable.HasValue)
      {
        num1 = 0;
      }
      else
      {
        nullable = dialogDefinition.ReloadOnShow;
        num1 = nullable.Value ? 1 : 0;
      }
      radWindow1.ReloadOnShow = num1 != 0;
      RadWindow radWindow2 = control;
      nullable = dialogDefinition.DestroyOnClose;
      int num2;
      if (!nullable.HasValue)
      {
        num2 = 0;
      }
      else
      {
        nullable = dialogDefinition.DestroyOnClose;
        num2 = nullable.Value ? 1 : 0;
      }
      radWindow2.DestroyOnClose = num2 != 0;
      if (!string.IsNullOrEmpty(dialogDefinition.Skin))
        control.Skin = dialogDefinition.Skin;
      if (!string.IsNullOrEmpty(dialogDefinition.CssClass))
        control.CssClass = dialogDefinition.CssClass;
      if (item is DynamicContent)
      {
        Guid systemParentId = ((DynamicContent) item).SystemParentId;
        if (systemParentId != Guid.Empty)
          control.NavigateUrl = control.NavigateUrl + "&parentId=" + (object) systemParentId;
      }
      this.WindowManager.Windows.Add(control);
      this.HostedInRadWindow = false;
    }

    protected Guid ItemId => this.Page.Request.QueryString["dataItemId"] != null ? Guid.Parse(this.Page.Request.QueryString["dataItemId"]) : Guid.Empty;

    private string ItemType => this.Page.Request.QueryString["typeName"] != null ? this.Page.Request.QueryString["typeName"].ToString() : string.Empty;

    private bool AutoBindParam => this.Page.Request.QueryString["autoBind"] != null && bool.Parse(this.Page.Request.QueryString["autoBind"]);

    private string ProviderName => this.Page.Request.QueryString["provider"] != null ? this.Page.Request.QueryString["provider"].ToString() : string.Empty;

    private bool DisableComparisonForVersions => this.Page.Request.QueryString[nameof (DisableComparisonForVersions)] != null && this.Page.Request.QueryString[nameof (DisableComparisonForVersions)].ToString().ToLower() == "true";

    private string VersionComparisonViewName => this.Page.Request.QueryString["VersionComparisonView"] != null ? this.Page.Request.QueryString["VersionComparisonView"].ToString() : string.Empty;

    private string ControlDefinitionName => this.Page.Request.QueryString[nameof (ControlDefinitionName)] != null ? this.Page.Request.QueryString[nameof (ControlDefinitionName)].ToString() : string.Empty;

    private string BackLabelText => this.Page.Request.QueryString["backLabelText"] != null ? this.Page.Request.QueryString["backLabelText"].ToString() : string.Empty;

    private string Culture => this.Page.Request.QueryString["culture"] != null ? this.Page.Request.QueryString["culture"].ToString() : string.Empty;

    private string RevertURL => this.Page.Request.QueryString["revertURL"] != null ? this.Page.Request.QueryString["revertURL"].ToString() : string.Empty;

    private string BackLabelLink
    {
      get
      {
        string empty = string.Empty;
        if (this.Page.Request.QueryString["backLabelLink"] != null)
        {
          empty = this.Page.Request.QueryString["backLabelLink"].ToString();
          if (!ObjectFactory.Resolve<IRedirectUriValidator>().IsValid(empty))
            empty = string.Empty;
        }
        return empty;
      }
    }
  }
}
