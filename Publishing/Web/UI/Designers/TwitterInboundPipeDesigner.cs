// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterInboundPipeDesigner
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

namespace Telerik.Sitefinity.Publishing.Web.UI.Designers
{
  /// <summary>Twitter Inbound Pipe Designer</summary>
  public class TwitterInboundPipeDesigner : ControlDesignerBase
  {
    /// <summary>Control Template</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Publishing.TwitterInboundPipeDesigner.ascx");
    private string providerName;

    /// <summary>Twitter Tag Key</summary>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Open Mapping Settings Dialog</summary>
    protected virtual Control OpenMappingSettingsDialog => this.Container.GetControl<Control>("openMappingSettings", true);

    /// <summary>Applications Binder</summary>
    protected virtual GenericCollectionBinder AppsBinder => this.Container.GetControl<GenericCollectionBinder>(nameof (AppsBinder), true);

    /// <summary>Search Pattern Control</summary>
    protected virtual TextField SearchPatternControl => this.Container.GetControl<TextField>("SearchPattern", true);

    /// <summary>Applications Selector</summary>
    protected virtual Control AppsSelector => this.Container.GetControl<Control>("AppsSelect", true);

    /// <summary>Language Choice Field</summary>
    protected virtual LanguageChoiceField LanguageChoiceField => this.Container.GetControl<LanguageChoiceField>("languageChoiceField", true);

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      ScriptReferenceCollection scriptReferences = PageManager.GetScriptReferences(ScriptRef.JQuery);
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", typeof (PublishingModule).Assembly.GetName().ToString()));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Publishing.Web.UI.Scripts.TwitterInboundPipeDesigner.js", typeof (TwitterInboundPipeDesigner).Assembly.GetName().ToString()));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>Get Script Descriptors</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddProperty("_shortDescriptionBase", (object) Res.Get<PublishingMessages>().TwitterPipeSettingsShortDescription);
      controlDescriptor.AddComponentProperty("SearchPattern", this.SearchPatternControl.ClientID);
      controlDescriptor.AddProperty("_appsSelectorId", (object) this.AppsSelector.ClientID);
      controlDescriptor.AddProperty("_urlNameNotSet", (object) Res.Get<PublishingMessages>().TwitterPipeSettingsUrlNameNotSet);
      controlDescriptor.AddElementProperty("openMappingSettingsButton", this.OpenMappingSettingsDialog.ClientID);
      controlDescriptor.AddElementProperty("languageChoiceField", this.LanguageChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("appsBinder", this.AppsBinder.ClientID);
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      PipeSettings defaultSettings = PublishingSystemFactory.GetPipe("TwitterInboundPipe").GetDefaultSettings();
      dictionary.Add("settings", (object) defaultSettings);
      dictionary.Add("pipe", (object) new WcfPipeSettings("TwitterInboundPipe", this.providerName));
      controlDescriptor.AddElementProperty("scheduleTypeElement", this.Container.GetControl<DropDownList>("ddlScheduleType", true).ClientID);
      controlDescriptor.AddElementProperty("scheduleDaysElement", this.Container.GetControl<DropDownList>("ddlDays", true).ClientID);
      controlDescriptor.AddComponentProperty("scheduleTime", this.Container.GetControl<DateField>("dtpScheduleTime", true).ClientID);
      controlDescriptor.AddComponentProperty("MaxItems", this.Container.GetControl<TextField>("maxItems", true).ClientID);
      controlDescriptor.AddProperty("_settingsData", (object) dictionary);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? TwitterInboundPipeDesigner.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Initialize Controls</summary>
    /// <param name="container"> The Container</param>
    protected override void InitializeControls(GenericContainer container)
    {
      this.providerName = this.Context.Request.QueryString["provider"];
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
