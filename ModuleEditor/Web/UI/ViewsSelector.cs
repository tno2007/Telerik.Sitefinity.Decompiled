// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.UI.ViewsSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.ModuleEditor.Web.UI
{
  /// <summary>Represents selector for views</summary>
  public class ViewsSelector : SimpleScriptView
  {
    private string itemType;
    private string itemName;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleEditor.ViewsSelector.ascx");
    internal const string viewsSelectorScript = "Telerik.Sitefinity.ModuleEditor.Web.Scripts.ViewsSelector.js";
    private const string serviceUrl = "~/Sitefinity/Services/MetaData/ModuleEditor.svc/views/";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ModuleEditor.Web.UI.ViewsSelector" /> class.
    /// </summary>
    public ViewsSelector() => this.LayoutTemplatePath = ViewsSelector.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the type of the item.</summary>
    public virtual string ItemType
    {
      get => this.itemType;
      set => this.itemType = value;
    }

    /// <summary>
    /// Gets or sets the localizable string that represents the name of the item in singular.
    /// </summary>
    public virtual string ItemName
    {
      get => this.itemName;
      set => this.itemName = value;
    }

    /// <summary>Gets the content selector control.</summary>
    public FlatSelector ItemsSelector => this.Container.GetControl<FlatSelector>("itemsSelector", true);

    /// <summary>Gets all screens radio.</summary>
    public RadioButton AllScreensRadio => this.Container.GetControl<RadioButton>("allScreensRadio", true);

    /// <summary>Gets some screens radio.</summary>
    public RadioButton SomeScreensRadio => this.Container.GetControl<RadioButton>("someScreensRadio", true);

    /// <summary>Gets the nowhere radio.</summary>
    public RadioButton NowhereRadio => this.Container.GetControl<RadioButton>("nowhereRadio", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ItemsSelector.ServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/MetaData/ModuleEditor.svc/views/");
      this.ItemType = this.Page.Request.QueryString["componentType"];
      this.ItemsSelector.ItemType = this.ItemType;
      this.ItemName = this.Page.Request.QueryString["itemsName"];
      this.AllScreensRadio.Text = string.Format(this.AllScreensRadio.Text, (object) this.ItemName);
      this.SomeScreensRadio.Text = string.Format(this.SomeScreensRadio.Text, (object) this.ItemName);
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
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (ViewsSelector).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("itemsSelector", this.ItemsSelector.ClientID);
      controlDescriptor.AddElementProperty("allScreensRadio", this.AllScreensRadio.ClientID);
      controlDescriptor.AddElementProperty("someScreensRadio", this.SomeScreensRadio.ClientID);
      controlDescriptor.AddElementProperty("nowhereRadio", this.NowhereRadio.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.ModuleEditor.Web.Scripts.ViewsSelector.js", typeof (ViewsSelector).Assembly.FullName)
    };
  }
}
