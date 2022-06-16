// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.Web.UI.ManageContentLocations
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.ContentLocations.Web.UI
{
  /// <summary>Represents the content locations management UI</summary>
  public class ManageContentLocations : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.ContentLocations.Web.UI.Scripts.ManageContentLocations.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ContentLocations.ManageContentLocations.ascx");
    /// <summary>
    /// Represents the name of the request parameter that will be used to set the provider name.
    /// </summary>
    internal const string providerParamKey = "provider";
    /// <summary>
    /// Represents the name of the request parameter that will be used to set the item type.
    /// </summary>
    internal const string itemTypeParamKey = "item_type";
    private static readonly string webServiceUrl = VirtualPathUtility.ToAbsolute(string.Format("~/{0}/", (object) "Sitefinity/Services/LocationService"));

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = ManageContentLocations.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets a reference to the pageHeaderLiteral</summary>
    protected virtual Literal PageHeaderLiteral => this.Container.GetControl<Literal>("pageHeaderLiteral", true);

    /// <summary>Gets the content languages drop down.</summary>
    /// <value>The content languages drop down.</value>
    protected virtual LanguagesDropDownList ContentLanguagesDropDown => this.Container.GetControl<LanguagesDropDownList>("contentLanguagesDropDown", true);

    /// <summary>Gets the content provider selector.</summary>
    /// <value>The content provider selector.</value>
    protected virtual FlatProviderSelector ContentProviderSelector => this.Container.GetControl<FlatProviderSelector>("contentProviderSelector", true);

    /// <summary>
    /// Gets a reference to the div element that will be transformed into grid.
    /// </summary>
    protected virtual HtmlContainerControl Grid => this.Container.GetControl<HtmlContainerControl>("contentLocationsGrid", true);

    /// <summary>Gets a reference to the SiteTableHeader.</summary>
    protected virtual HtmlTableCell SiteTableHeader => this.Container.GetControl<HtmlTableCell>("siteTableHeader", false);

    /// <summary>Gets a reference to the SiteTableData.</summary>
    protected virtual HtmlTableCell SiteTableData => this.Container.GetControl<HtmlTableCell>(nameof (SiteTableData), false);

    /// <summary>Gets a reference to the TableBodyData.</summary>
    protected virtual HtmlTableCell TableBodyData => this.Container.GetControl<HtmlTableCell>("tableBodyData", false);

    /// <summary>Gets a reference to the back button.</summary>
    protected virtual HyperLink BackButton => this.Container.GetControl<HyperLink>("backButton", true);

    /// <summary>Gets a reference to the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets a reference to the label manager.</summary>
    /// <value>The label manager.</value>
    protected virtual ClientLabelManager LabelManager => this.Container.GetControl<ClientLabelManager>("labelManager", true);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected override void InitializeControls(GenericContainer container)
    {
      string itemTypeName = this.GetItemTypeName();
      Type clrType = TypeResolutionService.ResolveType(itemTypeName, false, true);
      IManager mappedManager = ManagerBase.GetMappedManager(itemTypeName);
      if (mappedManager is DynamicModuleManager)
      {
        DynamicModuleType dynamicModuleType = ModuleBuilderManager.GetManager().GetDynamicModuleType(clrType);
        string moduleName = dynamicModuleType.ModuleName;
        string displayName = dynamicModuleType.DisplayName;
        this.ContentProviderSelector.DynamicModuleName = moduleName;
        this.PageHeaderLiteral.Text = Res.Get<ContentLocationsResources>("ContentLocationsManagementTitleTemplate", (object) displayName);
      }
      else
        this.PageHeaderLiteral.Text = Res.Get<ContentLocationsResources>("ContentLocationsManagementTitleTemplate", (object) clrType.Name);
      this.SetTableColumns();
      this.ContentLanguagesDropDown.LanguageSource = LanguageSource.Frontend;
      this.ContentProviderSelector.Manager = mappedManager;
    }

    private void SetTableColumns()
    {
      bool flag = SystemManager.CurrentContext.GetSites().Count<ISite>() > 1;
      if (this.SiteTableHeader != null)
        this.SiteTableHeader.Visible = flag;
      if (this.SiteTableData != null)
        this.SiteTableData.Visible = flag;
      if (this.TableBodyData == null)
        return;
      this.TableBodyData.Attributes.Add("colspan", "4");
    }

    private string GetItemTypeName()
    {
      string empty = string.Empty;
      NameValueCollection nameValueCollection = SystemManager.CurrentHttpContext.Request.Params;
      if (nameValueCollection.Keys.Contains("item_type"))
        empty = nameValueCollection["item_type"];
      return !string.IsNullOrWhiteSpace(empty) ? empty : throw new ArgumentException(string.Format("Missing {0} parameter.", (object) "item_type"));
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddProperty("_isMultiSite", (object) true);
      controlDescriptor.AddProperty("_isMultilingualSite", (object) SystemManager.CurrentContext.AppSettings.Multilingual);
      controlDescriptor.AddProperty("_itemTypeKey", (object) "item_type");
      controlDescriptor.AddProperty("_providerNameKey", (object) "provider");
      controlDescriptor.AddProperty("webServiceUrl", (object) ManageContentLocations.webServiceUrl);
      controlDescriptor.AddElementProperty("grid", this.Grid.ClientID);
      controlDescriptor.AddElementProperty("backButton", this.BackButton.ClientID);
      controlDescriptor.AddComponentProperty("contentLanguagesDropDown", this.ContentLanguagesDropDown.ClientID);
      controlDescriptor.AddComponentProperty("contentProviderSelector", this.ContentProviderSelector.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddComponentProperty("labelManager", this.LabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.ContentLocations.Web.UI.Scripts.ManageContentLocations.js", this.GetType().Assembly.FullName)
    };
  }
}
