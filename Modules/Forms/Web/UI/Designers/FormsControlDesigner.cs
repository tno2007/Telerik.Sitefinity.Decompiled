// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormsControlDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Designers
{
  /// <summary>A designer for the Forms control</summary>
  public class FormsControlDesigner : ControlDesignerBase
  {
    private bool? addCultureToFilter;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Forms.FormsControlDesigner.ascx");
    private const string designerScriptName = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormsControlDesigner.js";

    /// <summary>
    /// Gets or sets a value indicating whether to add the culture of the property editor to the filter.
    /// </summary>
    /// <value><c>true</c> if the culture will be added to the filter; otherwise, <c>false</c>.</value>
    /// <remarks>
    /// If not set a value, the property will return the value of the <see cref="M:Telerik.Sitefinity.Model.IAppSettings.Multilingual" />.;
    /// </remarks>
    public bool AddCultureToFilter
    {
      get
      {
        if (!this.addCultureToFilter.HasValue)
          this.addCultureToFilter = new bool?(SystemManager.CurrentContext.AppSettings.Multilingual);
        return this.addCultureToFilter.Value;
      }
      set => this.addCultureToFilter = new bool?(value);
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FormsControlDesigner.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the single item content selector control. example - news selector or events selector
    /// </summary>
    /// <value>The content selector.</value>
    public ContentSelector ContentSelector => this.Container.GetControl<ContentSelector>("selector", true, TraverseMethod.DepthFirst);

    /// <summary>Represents the text field for the confirmation box</summary>
    protected TextField SuccessMessageTextField => this.Container.GetControl<TextField>("successMessageTextField", true);

    /// <summary>Represents the text field for the redirect url</summary>
    protected TextField RedirectUrlTextField => this.Container.GetControl<TextField>("redirectUrlTextField", true);

    /// <summary>
    /// List with the radiobuttons for the confirmation options
    /// </summary>
    protected IEnumerable<Control> ConfirmationRadioButtons => this.Container.GetControls<RadioButton>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (rb => rb.Value.ID.StartsWith("confirmation"))).Select<KeyValuePair<string, Control>, Control>((Func<KeyValuePair<string, Control>, Control>) (c => c.Value));

    /// <summary>
    /// Represents the user's choice to use custom confirmation options.
    /// </summary>
    protected CheckBox ChkPerWidgetConfirmation => this.Container.GetControl<CheckBox>("chkPerWidgetConfirmation", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.RedirectUrlTextField.Attributes.CssStyle.Add("display", "none");
      this.ContentSelector.TitleText = Res.Get<FormsResources>().FormsTitle;
      this.ContentSelector.ShowButtonArea = false;
      this.ContentSelector.ShowHeader = false;
      string str1 = string.Format("Framework == {0}", (object) FormFramework.WebForms.ToString());
      this.PropertyEditor.SaveButtonTitle = Res.Get<Labels>().DoneWithSelecting;
      if (this.AddCultureToFilter)
      {
        string str2 = (string) null;
        if (this.PropertyEditor != null)
          str2 = this.PropertyEditor.PropertyValuesCulture;
        if (!string.IsNullOrEmpty(str2))
        {
          str1 += string.Format("AND Culture == {0}", (object) str2);
          this.ContentSelector.UICulture = str2;
        }
      }
      this.ContentSelector.ItemsFilter = str1;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("contentSelector", this.ContentSelector.ClientID);
      controlDescriptor.AddComponentProperty("redirectUrlTextField", this.RedirectUrlTextField.ClientID);
      controlDescriptor.AddComponentProperty("successMessageTextField", this.SuccessMessageTextField.ClientID);
      controlDescriptor.AddProperty("_defaultSuccessMessage", (object) Res.Get<FormsResources>().SuccessThanksForFillingOutOurForm);
      controlDescriptor.AddProperty("confirmationRadioButtons", (object) this.ConfirmationRadioButtons.Select<Control, string>((Func<Control, string>) (b => b.ClientID)));
      controlDescriptor.AddElementProperty("chkPerWidgetConfirmation", this.ChkPerWidgetConfirmation.ClientID);
      controlDescriptor.AddProperty("_submitActionsMap", (object) new FormsControlDesigner.EnumMap(typeof (SubmitAction)));
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
      new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormsControlDesigner.js", typeof (FormsControlDesigner).Assembly.FullName)
    }.ToArray();

    private class EnumMap
    {
      public EnumMap(Type enumType)
      {
        this.ValuesToNamesMap = this.CreateEnumValueToNameMap(enumType);
        this.NamesToValuesMap = this.CreateEnumNameToValueMap(enumType);
      }

      public Dictionary<string, string> ValuesToNamesMap { get; private set; }

      public Dictionary<string, string> NamesToValuesMap { get; private set; }

      /// <summary>
      /// Creates a map string - int with each of the enumeration members
      /// </summary>
      private Dictionary<string, string> CreateEnumNameToValueMap(Type enumType) => ((IEnumerable<string>) Enum.GetNames(enumType)).ToDictionary<string, string, string>((Func<string, string>) (k => k), (Func<string, string>) (v => ((int) Enum.Parse(enumType, v)).ToString()));

      private Dictionary<string, string> CreateEnumValueToNameMap(Type enumType) => ((IEnumerable<string>) Enum.GetNames(enumType)).ToDictionary<string, string, string>((Func<string, string>) (v => ((int) Enum.Parse(enumType, v)).ToString()), (Func<string, string>) (k => k));
    }
  }
}
