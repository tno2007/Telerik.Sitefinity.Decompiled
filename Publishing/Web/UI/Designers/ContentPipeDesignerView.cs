// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Web.Services.Data;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Publishing.Web.UI.Designers
{
  public class ContentPipeDesignerView : ControlDesignerBase, IToggleable
  {
    /// <summary>The name of the layout template.</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Publishing.ContentPipeDesignerView.ascx");
    internal const string controlScript = "Telerik.Sitefinity.Publishing.Web.UI.Scripts.ContentPipeDesignerView.js";
    private string providerName;
    private IEnumerable<Type> contentTypes;
    private IList<Control> designerViewsControls = (IList<Control>) new List<Control>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeDesignerView" /> class.
    /// </summary>
    public ContentPipeDesignerView() => this.LayoutTemplatePath = ContentPipeDesignerView.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the back links page picker control.</summary>
    /// <value>The back links page picker.</value>
    protected PageField BackLinksPagePicker => this.Container.GetControl<PageField>("backLinksPagePicker", true);

    /// <summary>Gets the RadMultiPage for the designer.</summary>
    /// <value>The RadMultiPage control.</value>
    protected RadMultiPage MultiPage => this.Container.GetControl<RadMultiPage>();

    /// <summary>Gets the list control containing the content types.</summary>
    /// <value>The list control.</value>
    protected virtual ListControl SelectContent => this.Container.GetControl<ListControl>("selectContent", true);

    /// <summary>Gets the language selector.</summary>
    /// <value>The language selector.</value>
    protected virtual LanguageChoiceField LanguageSelector => this.Container.GetControl<LanguageChoiceField>("languageChoiceField", true);

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the open mapping settings dialog.</summary>
    /// <value>The open mapping settings dialog.</value>
    protected virtual Control OpenMappingSettingsDialog => this.Container.GetControl<Control>("openMappingSettings", true);

    protected virtual string ClientScriptType => this.GetType().FullName;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.providerName = PublishingManager.GetProviderNameFromQueryString();
      this.SelectContent.Items.Clear();
      foreach (Type contentType in this.ContentTypes)
        this.SelectContent.Items.Add(new ListItem()
        {
          Text = contentType.GetTypeUIPluralName(),
          Value = contentType.FullName
        });
      this.BackLinksPagePicker.WebServiceUrl = "~/Sitefinity/Services/Pages/PagesService.svc/";
      this.BackLinksPagePicker.RootNodeID = SiteInitializer.CurrentFrontendRootNodeId;
      this.BackLinksPagePicker.DisplayMode = FieldDisplayMode.Write;
      this.BuildDesignerViews();
    }

    /// <summary>
    /// Gets the content types to be displayed by the designer.
    /// </summary>
    /// <value>The content types.</value>
    protected IEnumerable<Type> ContentTypes
    {
      get
      {
        if (this.contentTypes == null)
          this.contentTypes = Config.Get<PublishingConfig>().ContentPipeTypes.Select<TypeConfigElement, Type>((Func<TypeConfigElement, Type>) (pt => TypeResolutionService.ResolveType(!string.IsNullOrEmpty(pt.AssemblyQualifiedName) ? pt.AssemblyQualifiedName : pt.FullName)));
        return this.contentTypes;
      }
    }

    private object CreateContentPipeSettingsCollection()
    {
      Dictionary<string, object> settingsCollection = new Dictionary<string, object>();
      foreach (Type contentType in this.ContentTypes)
      {
        SitefinityContentPipeSettings defaultSettings = (SitefinityContentPipeSettings) PublishingSystemFactory.GetPipe("ContentInboundPipe").GetDefaultSettings();
        defaultSettings.ContentTypeName = contentType.FullName;
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary.Add("settings", (object) defaultSettings);
        WcfPipeSettings wcfPipeSettings = new WcfPipeSettings();
        wcfPipeSettings.InitializeFromModel((PipeSettings) defaultSettings, this.providerName);
        dictionary.Add("pipe", (object) wcfPipeSettings);
        settingsCollection.Add(contentType.FullName, (object) dictionary);
      }
      return (object) settingsCollection;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ClientScriptType, this.ClientID);
      controlDescriptor.AddComponentProperty("multiPage", this.MultiPage.ClientID);
      controlDescriptor.AddProperty("designerViewIds", (object) this.designerViewsControls.Select<Control, string>((Func<Control, string>) (c => c.ClientID)));
      controlDescriptor.AddComponentProperty("defaultDesignerView", this.designerViewsControls[this.designerViewsControls.Count - 1].ClientID);
      controlDescriptor.AddElementProperty("selectContent", this.SelectContent.ClientID);
      controlDescriptor.AddElementProperty("selectLanguage", this.LanguageSelector.ClientID);
      controlDescriptor.AddElementProperty("openMappingSettingsButton", this.OpenMappingSettingsDialog.ClientID);
      controlDescriptor.AddComponentProperty("backLinksPagePicker", this.BackLinksPagePicker.ClientID);
      controlDescriptor.AddProperty("_settingsDatas", this.CreateContentPipeSettingsCollection());
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      ScriptReferenceCollection scriptReferences = PageManager.GetScriptReferences(ScriptRef.JQuery);
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", typeof (Bootstrapper).Assembly.FullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Publishing.Web.UI.Scripts.ContentPipeDesignerView.js", typeof (ContentPipeDesignerView).Assembly.FullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    public bool IsActive() => this.ContentTypes.Any<Type>();

    private void BuildDesignerViews()
    {
      RadPageViewCollection pageViews = this.MultiPage.PageViews;
      pageViews.Clear();
      IEnumerable<IContentPipeDesignerView> designerViews = ObjectFactory.Container.ResolveAll(typeof (IContentPipeDesignerView)).Cast<IContentPipeDesignerView>();
      if (designerViews != null)
      {
        foreach (IContentPipeDesignerView pipeDesignerView in designerViews)
        {
          if (!pipeDesignerView.UseDefaultDesigner)
          {
            Control designerView = pipeDesignerView.BuildControl((Control) this);
            if (designerView is IControlDesignerView controlDesignerView)
            {
              this.PropertyEditor = new PropertyEditor();
              ContentViewControlDefinition controlDefinition = new ContentViewControlDefinition()
              {
                ContentType = pipeDesignerView.ContentType
              };
              this.PropertyEditor.Control = (object) new ContentView()
              {
                ControlDefinition = controlDefinition
              };
              controlDesignerView.InitView((ControlDesignerBase) this);
            }
            RadPageView pageView = this.BuildDesignerPageView(pipeDesignerView.ContentType, designerView);
            pageViews.Add(pageView);
          }
        }
      }
      ContentSelectorsPipeDesignerView designerView1 = (ContentSelectorsPipeDesignerView) this.Page.LoadControl(typeof (ContentSelectorsPipeDesignerView), (object[]) null);
      RadPageView pageView1 = this.BuildDesignerPageView((Type) null, (Control) designerView1, true);
      this.InitializeDefaultDesignerView(designerView1, designerViews);
      pageViews.Add(pageView1);
    }

    private RadPageView BuildDesignerPageView(
      Type contentType,
      Control designerView,
      bool isDefault = false)
    {
      RadPageView radPageView = new RadPageView();
      string str = !isDefault ? contentType.FullName.Replace('.', '_') : "default";
      radPageView.ID = str;
      designerView.ID = str + "_designer";
      radPageView.Controls.Add(designerView);
      this.designerViewsControls.Add(designerView);
      return radPageView;
    }

    private void InitializeDefaultDesignerView(
      ContentSelectorsPipeDesignerView designerView,
      IEnumerable<IContentPipeDesignerView> designerViews)
    {
      if (designerViews == null)
        return;
      foreach (IContentPipeDesignerView designerView1 in designerViews)
      {
        if (designerView1.UseDefaultDesigner)
          designerView.AddResource(designerView1);
      }
    }
  }
}
