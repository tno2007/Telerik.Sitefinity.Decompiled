// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SortWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  public class SortWidget : DynamicCommandWidget
  {
    private string layoutTemplatePath;
    private const string sortWidgetScriptName = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.SortWidget.js";
    public static readonly string sortWidgetTemplate = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.SortWidget.ascx");
    public static readonly string SortCommandName = "sort";
    public static readonly string ShowHierarchicalCommandName = "showHierarchical";
    public static readonly string CustomOptionName = "Custom";

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      foreach (ScriptReference scriptReference in base.GetScriptReferences())
        scriptReferences.Add(scriptReference);
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = this.GetType().Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.SortWidget.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      ScriptBehaviorDescriptor scriptDescriptor = this.GetDynamicWidgetScriptDescriptor();
      scriptDescriptor.AddProperty("_radWindowClientID", (object) this.RadWindow.ClientID);
      scriptDescriptor.AddProperty("_customOption", (object) SortWidget.CustomOptionName);
      scriptDescriptors.Add((ScriptDescriptor) scriptDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
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
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(this.layoutTemplatePath) ? SortWidget.sortWidgetTemplate : this.layoutTemplatePath;
      set => this.layoutTemplatePath = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.ConstructDialog();
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the RAD window manager.</summary>
    /// <value>The RAD window manager.</value>
    protected virtual RadWindow RadWindow => this.Container.GetControl<RadWindow>("windowSortExpr", this.BindTo == BindCommandListTo.ComboBox);

    /// <summary>
    /// Constructs the dialog for specifying a custom sorting expression
    /// </summary>
    protected void ConstructDialog()
    {
      this.RadWindow.ID = "CustomSortingDialog";
      this.RadWindow.Modal = true;
      this.RadWindow.InitialBehaviors = WindowBehaviors.None;
      this.RadWindow.Behaviors = WindowBehaviors.Close;
      this.RadWindow.Width = Unit.Pixel(435);
      this.RadWindow.Height = Unit.Pixel(275);
      this.RadWindow.VisibleTitlebar = true;
      this.RadWindow.VisibleStatusbar = false;
      this.RadWindow.Skin = "Default";
      if (this.DynamicModuleTypeId == Guid.Empty)
        this.RadWindow.NavigateUrl = this.GetNavigateUrl(string.Format("?contentType={0}&sortExpression={1}", (object) this.ContentType.FullName, (object) this.SelectedValue));
      else
        this.RadWindow.NavigateUrl = this.GetNavigateUrl(string.Format("?dynamicModuleTypeId={0}&sortExpression={1}", (object) this.DynamicModuleTypeId, (object) this.SelectedValue));
    }

    /// <summary>Gets the navigate URL for the radWindow</summary>
    /// <param name="parameters">The parameters.</param>
    private string GetNavigateUrl(string parameters)
    {
      string str1 = VirtualPathUtility.Combine(VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/"), typeof (CustomSortingDialog).Name);
      if (parameters != null && parameters.IndexOf("?") != 0)
        parameters += "?";
      string str2 = parameters;
      return str1 + str2;
    }
  }
}
