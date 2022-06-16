// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.UI.Designers.RssAtomPipeImportDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
  public class RssAtomPipeImportDesignerView : ControlDesignerBase
  {
    /// <summary>The path to the of the layout template.</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Publishing.RssAtomPipeImportDesignerView.ascx");
    /// <summary>The minimum value for the MaxItems setting</summary>
    protected const int minMaxItemsCount = 1;
    /// <summary>The minimum value for the ContentSize setting</summary>
    protected const int minContentSize = 1;
    private string providerName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Web.UI.Designers.RssAtomPipeImportDesignerView" /> class.
    /// </summary>
    public RssAtomPipeImportDesignerView() => this.LayoutTemplatePath = RssAtomPipeImportDesignerView.layoutTemplatePath;

    public ITextControl UINameLabel => this.Container.GetControl<ITextControl>("uiNameLabel", true);

    public FieldControl UrlName => this.Container.GetControl<FieldControl>("urlName", true);

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected virtual HtmlControl ErrorMessageHolder => this.Container.GetControl<HtmlControl>("errorMessageHolder", false);

    /// <summary>Gets the language selector.</summary>
    /// <value>The language selector.</value>
    protected virtual LanguageChoiceField LanguageSelector => this.Container.GetControl<LanguageChoiceField>("languageChoiceField", true);

    /// <summary>Gets the open mapping settings dialog.</summary>
    /// <value>The open mapping settings dialog.</value>
    protected virtual Control OpenMappingSettingsDialog => this.Container.GetControl<Control>("openMappingSettings", true);

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
      controlDescriptor.AddProperty("dataFieldNameControlIdMap", (object) dictionary1);
      controlDescriptor.AddElementProperty("uiNameLabel", ((Control) this.UINameLabel).ClientID);
      controlDescriptor.AddProperty("_shortDescriptionBase", (object) Res.Get<PublishingMessages>().PipeSettingsImporShortDescriptionBase);
      controlDescriptor.AddProperty("_urlNameNotSet", (object) Res.Get<PublishingMessages>().PipeSettingsUrlNameNotSet);
      controlDescriptor.AddElementProperty("openMappingSettingsButton", this.OpenMappingSettingsDialog.ClientID);
      controlDescriptor.AddElementProperty("selectLanguage", this.LanguageSelector.ClientID);
      controlDescriptor.AddComponentProperty("labelManager", this.Container.GetControl<ClientLabelManager>("labelManager", true).ClientID);
      controlDescriptor.AddProperty("_errorMessageHolderId", (object) this.ErrorMessageHolder.ClientID);
      Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
      PipeSettings defaultSettings = PublishingSystemFactory.GetPipe("RSSInboundPipe").GetDefaultSettings();
      dictionary2.Add("settings", (object) defaultSettings);
      dictionary2.Add("pipe", (object) new WcfPipeSettings("RSSInboundPipe", this.providerName));
      controlDescriptor.AddElementProperty("scheduleTypeElement", this.Container.GetControl<DropDownList>("ddlScheduleType", true).ClientID);
      controlDescriptor.AddElementProperty("scheduleDaysElement", this.Container.GetControl<DropDownList>("ddlDays", true).ClientID);
      controlDescriptor.AddComponentProperty("scheduleTime", this.Container.GetControl<DateField>("dtpScheduleTime", true).ClientID);
      controlDescriptor.AddProperty("_settingsData", (object) dictionary2);
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
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Publishing.Web.UI.Scripts.RssAtomPipeImportDesignerView.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      this.UrlName.Description = string.Format(Res.Get<PublishingMessages>().FeedsBaseUrlPattern, (object) PublishingManager.GetFeedsBaseURl());
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
