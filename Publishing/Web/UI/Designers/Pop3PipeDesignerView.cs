// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.UI.Designers.Pop3PipeDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Web.Services.Data;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Publishing.Web.UI.Designers
{
  public class Pop3PipeDesignerView : ControlDesignerBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Publishing.Pop3PipeDesignerView.ascx");
    private string providerName;

    public ITextControl UINameLabel => this.Container.GetControl<ITextControl>("uiNameLabel", true);

    public TextField ServerName => this.Container.GetControl<TextField>("serverName", true);

    public TextField ServerPort => this.Container.GetControl<TextField>("serverPort", true);

    public ChoiceField IsUseSsl => this.Container.GetControl<ChoiceField>("isUseSsl", true);

    public TextField Account => this.Container.GetControl<TextField>("account", true);

    public RadTextBox Password => this.Container.GetControl<RadTextBox>("RadTextBoxPassword", true);

    /// <summary>Gets the language selector.</summary>
    protected virtual LanguageChoiceField LanguageSelector => this.Container.GetControl<LanguageChoiceField>("languageChoiceField", true);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected virtual Control OpenMappingSettingsDialog => this.Container.GetControl<Control>("openMappingSettings", true);

    protected virtual DropDownList ScheduleTypeDropDownList => this.Container.GetControl<DropDownList>("ddlScheduleType", true);

    protected virtual DateField ScheduleTimeDateField => this.Container.GetControl<DateField>("dtpScheduleTime", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      foreach (FieldControl fieldControl in this.Container.GetControls<FieldControl>().Values)
      {
        if (!string.IsNullOrEmpty(fieldControl.DataFieldName))
          dictionary1.Add(fieldControl.DataFieldName, fieldControl.ClientID);
      }
      dictionary1.Add("Pop3Password", this.Password.ClientID);
      controlDescriptor.AddProperty("dataControlIdMap", (object) dictionary1);
      controlDescriptor.AddElementProperty("uiNameLabel", ((Control) this.UINameLabel).ClientID);
      controlDescriptor.AddElementProperty("selectLanguage", this.LanguageSelector.ClientID);
      controlDescriptor.AddProperty("_shortDescription", (object) Res.Get<PublishingMessages>().Pop3PipeSettingsImportShortDescription);
      string str = string.Empty;
      if (this.IsUseSsl != null)
        str = this.IsUseSsl.DataFieldName;
      controlDescriptor.AddProperty("isUseSslDataFieldName", (object) str);
      controlDescriptor.AddProperty("passwordRadTextBoxDataFieldName", (object) "Pop3Password");
      controlDescriptor.AddElementProperty("openMappingSettingsButton", this.OpenMappingSettingsDialog.ClientID);
      Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
      PipeSettings defaultSettings = PublishingSystemFactory.GetPipe("Pop3InboundPipe").GetDefaultSettings();
      dictionary2.Add("settings", (object) defaultSettings);
      dictionary2.Add("pipe", (object) new WcfPipeSettings("Pop3InboundPipe", this.providerName));
      controlDescriptor.AddProperty("_settingsData", (object) dictionary2);
      controlDescriptor.AddElementProperty("scheduleTypeElement", this.Container.GetControl<DropDownList>("ddlScheduleType", true).ClientID);
      controlDescriptor.AddElementProperty("scheduleDaysElement", this.Container.GetControl<DropDownList>("ddlDays", true).ClientID);
      controlDescriptor.AddComponentProperty("scheduleTime", this.Container.GetControl<DateField>("dtpScheduleTime", true).ClientID);
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
      string assembly = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Publishing.Web.UI.Scripts.Pop3PipeDesignerView.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? Pop3PipeDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container.</param>
    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.providerName = PublishingManager.GetProviderNameFromQueryString();
      this.FillDaysDropDown();
    }

    protected virtual void FillDaysDropDown()
    {
      DropDownList control = this.Container.GetControl<DropDownList>("ddlDays", true);
      for (int index = 0; index < 7; ++index)
      {
        DayOfWeek dayOfWeek = (DayOfWeek) index;
        control.Items.Add(new ListItem()
        {
          Text = dayOfWeek.ToString(),
          Value = index.ToString()
        });
      }
    }
  }
}
