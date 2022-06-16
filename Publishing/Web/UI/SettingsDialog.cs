// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.UI.SettingsDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model.Publishing.Model.Twitter;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Publishing.Web.UI.Designers;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Publishing.Web.UI
{
  /// <summary>
  /// This dialog is used to show dynamically Settings designers and pass javascript settings data objects to the settings designer
  /// The settings designer to show is resolved through the Url SettingsName parameter
  /// </summary>
  public class SettingsDialog : AjaxDialogBase
  {
    private Dictionary<string, Control> containedControls = new Dictionary<string, Control>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private Dictionary<string, Control> designers;
    private bool editMode;
    private string providerName;
    public static readonly string templatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Publishing.SettingsDialog.ascx");
    public const string dialogScript = "Telerik.Sitefinity.Publishing.Web.UI.Scripts.SettingsDialog.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Web.UI.SettingsDialog" /> class.
    /// </summary>
    public SettingsDialog() => this.LayoutTemplatePath = SettingsDialog.templatePath;

    /// <summary>
    /// Text to display in the mapping dialog when publishing point title is empty string (e.g. during creation)
    /// </summary>
    public string NewPublishingPointDefaultTitle => Res.Get<PublishingMessages>().NewPublishingPointDefaultTitle;

    /// <summary>
    /// Used to register the loaded designers and pass their client id's for the javascript component
    /// </summary>
    /// <value>The loaded designers.</value>
    protected Dictionary<string, Control> LoadedDesigners
    {
      get
      {
        if (this.designers == null)
          this.designers = new Dictionary<string, Control>();
        return this.designers;
      }
    }

    /// <summary>Gets the command bar.</summary>
    /// <value>The command bar.</value>
    protected internal virtual CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", true);

    /// <summary>Gets a reference to the designere switcher</summary>
    protected internal virtual RadMultiPage DesignersMultiPage => this.Container.GetControl<RadMultiPage>("designerMultiPage", true);

    /// <summary>
    /// Get a reference to the pipe selector in the selector area
    /// </summary>
    protected virtual ChoiceField PipeSelector => this.GetControlFromContainer<ChoiceField>(this.SelectorArea, "pipeSelector");

    /// <summary>
    /// Gets a reference to the pipe chooser text element in the selector area
    /// </summary>
    protected virtual ITextControl PipeChooserTitle => this.GetControlFromContainer<ITextControl>(this.SelectorArea, "pipeChooserTitle");

    /// <summary>
    /// Gets a reference to the selectors html container element in the template
    /// </summary>
    protected virtual Control SelectorArea => this.Container.GetControl<Control>("selectorArea", true);

    /// <summary>
    /// Gets a reference to the mapping area container html element in the template
    /// </summary>
    protected virtual Control MappingArea => this.Container.GetControl<Control>("mappingArea", true);

    /// <summary>
    /// Gets a reference to the accept changes html element in the template
    /// </summary>
    protected virtual Control AcceptChangesButton => this.GetControlFromContainer<Control>(this.MappingArea, "btnAcceptChanges");

    /// <summary>
    /// Gets a reference to the html element that serves as a cancel button
    /// </summary>
    protected virtual Control CancelChangesButton => this.GetControlFromContainer<Control>(this.MappingArea, "btnCancelChanges");

    /// <summary>
    /// Gets a reference to the mapping control in the template
    /// </summary>
    protected virtual Mapping MappingControl => this.GetControlFromContainer<Mapping>(this.MappingArea, "mapping");

    /// <summary>Name of the publishing provider to use</summary>
    public string ProviderName
    {
      get => this.providerName;
      set => this.providerName = value;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(this.GetBaseScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = source.Last<ScriptDescriptor>() as ScriptControlDescriptor;
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (KeyValuePair<string, Control> loadedDesigner in this.LoadedDesigners)
        dictionary.Add(loadedDesigner.Key, loadedDesigner.Value.ClientID);
      controlDescriptor.AddProperty("designersMap", (object) dictionary);
      controlDescriptor.AddComponentProperty("multiPage", this.DesignersMultiPage.ClientID);
      controlDescriptor.AddComponentProperty("pipeSelector", this.PipeSelector.ClientID);
      controlDescriptor.AddComponentProperty("commandBar", this.CommandBar.ClientID);
      controlDescriptor.AddProperty("_editMode", (object) this.editMode);
      controlDescriptor.AddElementProperty("selectorArea", this.SelectorArea.ClientID);
      controlDescriptor.AddElementProperty("mappingArea", this.MappingArea.ClientID);
      controlDescriptor.AddComponentProperty("mappingControl", this.MappingControl.ClientID);
      controlDescriptor.AddElementProperty("btnCancelMappingSettings", this.CancelChangesButton.ClientID);
      controlDescriptor.AddElementProperty("btnAcceptMappingSettings", this.AcceptChangesButton.ClientID);
      controlDescriptor.AddProperty("_newPublishingPointDefaultTitle", (object) this.NewPublishingPointDefaultTitle);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Publishing.Web.UI.Scripts.SettingsDialog.js", typeof (SettingsDialog).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.FilterSelectorHelper.js", typeof (SettingsDialog).Assembly.FullName),
      new ScriptReference()
      {
        Assembly = "Telerik.Web.UI",
        Name = "Telerik.Web.UI.Common.Core.js"
      }
    }.ToArray();

    public override string ClientComponentType => this.GetType().FullName;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      this.ProviderName = this.Context.Request.QueryString["provider"];
      string settingsName = this.Context.Request.QueryString["SettingsName"];
      string str = this.Context.Request.QueryString["Mode"];
      bool flag = str == null || str == "Inbound";
      SettingsDialog.PipeMode mode = flag ? SettingsDialog.PipeMode.Import : SettingsDialog.PipeMode.Export;
      if (!string.IsNullOrEmpty(settingsName))
        this.editMode = true;
      else
        this.DesignersMultiPage.PageViews.Add(new RadPageView());
      this.PipeChooserTitle.Text = flag ? Res.Get<PublishingMessages>().SelectInboundPipes : Res.Get<PublishingMessages>().SelectOutboundPipes;
      this.LoadPipeDesigners(settingsName, mode);
    }

    private void LoadPipeDesigners(string settingsName, SettingsDialog.PipeMode mode)
    {
      RadMultiPage designersMultiPage = this.DesignersMultiPage;
      if (!this.editMode)
      {
        foreach (IPipe publishingPipe in PublishingSystemFactory.GetPublishingPipes())
          this.LoadSinglePipeDesigner(publishingPipe, mode);
      }
      else
      {
        IPipe pipe = PublishingSystemFactory.GetPipe(settingsName);
        if (pipe is IDynamicPipe dynamicPipe)
          dynamicPipe.SetPipeName(settingsName);
        this.LoadSinglePipeDesigner(pipe, mode);
      }
    }

    private void LoadSinglePipeDesigner(IPipe pipe, SettingsDialog.PipeMode pipeMode)
    {
      Control control1 = (Control) null;
      Control control2 = (Control) null;
      Type inboundDesigner;
      Type outboundDesigner;
      this.GetDesignerTypes(pipe, out inboundDesigner, out outboundDesigner);
      if (pipeMode == SettingsDialog.PipeMode.Import && inboundDesigner != (Type) null && pipe is IInboundPipe)
      {
        control1 = (Control) Activator.CreateInstance(inboundDesigner);
        if (control1 is IPipeDesigner pipeDesigner)
          pipeDesigner.Pipe = pipe;
      }
      if (outboundDesigner != (Type) null && pipe is IOutboundPipe)
      {
        control2 = (Control) Activator.CreateInstance(outboundDesigner);
        if (control2 is IPipeDesigner pipeDesigner)
          pipeDesigner.Pipe = pipe;
      }
      if (pipeMode == SettingsDialog.PipeMode.Export && control2 != null && this.CheckIfDesignerIsActive(control2))
      {
        string key = pipe.Name + "_" + (object) pipeMode;
        RadPageView pageView = new RadPageView();
        pageView.Controls.Add(control2);
        this.DesignersMultiPage.PageViews.Add(pageView);
        this.LoadedDesigners.Add(key, control2);
        PipeSettings defaultSettings = pipe.GetDefaultSettings();
        defaultSettings.IsInbound = pipeMode == SettingsDialog.PipeMode.Import;
        this.PipeSelector.Choices.Add(new ChoiceItem()
        {
          Value = key,
          Text = defaultSettings.GetLocalizedUIName()
        });
      }
      if (pipeMode != SettingsDialog.PipeMode.Import || control1 == null || !this.CheckIfDesignerIsActive(control1))
        return;
      string key1 = pipe.Name + "_" + (object) pipeMode;
      RadPageView pageView1 = new RadPageView();
      pageView1.Controls.Add(control1);
      this.DesignersMultiPage.PageViews.Add(pageView1);
      this.LoadedDesigners.Add(key1, control1);
      PipeSettings defaultSettings1 = pipe.GetDefaultSettings();
      defaultSettings1.IsInbound = pipeMode == SettingsDialog.PipeMode.Import;
      this.PipeSelector.Choices.Add(new ChoiceItem()
      {
        Value = key1,
        Text = defaultSettings1.GetLocalizedUIName()
      });
    }

    private void GetDesignerTypes(IPipe pipe, out Type inboundDesigner, out Type outboundDesigner)
    {
      inboundDesigner = (Type) null;
      outboundDesigner = (Type) null;
      string name = pipe.Name;
      bool isInbound = pipe is IInboundPipe;
      bool flag = pipe is IOutboundPipe;
      if (PublishingSystemFactory.PipeDesignerRegistered(name, isInbound))
        inboundDesigner = PublishingSystemFactory.GetPipeDesigner(name, isInbound);
      if (PublishingSystemFactory.PipeDesignerRegistered(name, !flag))
        outboundDesigner = PublishingSystemFactory.GetPipeDesigner(name, !flag);
      PipeDesignerAttribute designerAttribute = this.GetDesignerAttribute(pipe.GetType());
      if (designerAttribute == null)
        return;
      if (inboundDesigner == (Type) null)
        inboundDesigner = designerAttribute.InboundDesignerType;
      if (!(outboundDesigner == (Type) null))
        return;
      outboundDesigner = designerAttribute.OutboundDesignerType;
    }

    private bool CheckIfDesignerIsActive(Control designer) => !(designer is IToggleable toggleable) || toggleable.IsActive();

    protected virtual PipeDesignerAttribute GetDesignerAttribute(Type pipeType)
    {
      object[] customAttributes = pipeType.GetCustomAttributes(typeof (PipeDesignerAttribute), true);
      return customAttributes.Length != 0 ? ((IEnumerable<object>) customAttributes).Last<object>() as PipeDesignerAttribute : (PipeDesignerAttribute) null;
    }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.Page.ClientScript.RegisterStartupScript(this.GetType(), "designerId", string.Format("window.designerId = '{0}';", (object) this.LoadedDesigners.Values.First<Control>().ClientID), true);
    }

    [Obsolete("This method is not used anymore.")]
    protected virtual Type GetSettingsTypeByName(Type settingsType)
    {
      if (settingsType == typeof (RssPipeSettings))
        return typeof (RssAtomPipeDesignerView);
      return settingsType == typeof (TwitterPipeSettings) ? typeof (TwitterPipeDesignerView) : (Type) null;
    }

    protected virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new List<ScriptDescriptor>(base.GetScriptDescriptors());

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected T GetControlFromContainer<T>(Control container, string id) where T : class
    {
      string key = id + "@" + container.ID;
      Control control;
      if (!this.containedControls.TryGetValue(key, out control))
      {
        control = container.NamingContainer.FindControl(id);
        this.containedControls.Add(key, control);
      }
      return control as T;
    }

    /// <summary>
    /// Use instead of bool (ex. IsInbound) so that the intent in the code is more clear
    /// </summary>
    private enum PipeMode
    {
      /// <summary>Inbound</summary>
      Import,
      /// <summary>Outbound</summary>
      Export,
    }
  }
}
