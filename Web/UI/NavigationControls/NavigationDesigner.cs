// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.NavigationDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  /// <summary>The designer for Navigation Control.</summary>
  public class NavigationDesigner : ControlDesignerBase
  {
    private const string JqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.NavigationControls.NavigationDesigner.ascx");

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? NavigationDesigner.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// The script control type name passed to the <see cref="T:System.Web.UI.ScriptControlDescriptor" />. It defaults to the full name
    /// of the current object class. E.g. can be overriden to reuse the script of some of the base classes and just customize
    /// some controls server-side.
    /// </summary>
    /// <value></value>
    protected override string ScriptDescriptorTypeName => this.GetType().FullName;

    /// <summary>Gets the page selector control.</summary>
    /// <value>The page selector control.</value>
    protected internal virtual PagesSelector PageSelectorControl => this.Container.GetControl<PagesSelector>("pageSelector", true);

    /// <summary>Gets the pages selector control.</summary>
    /// <value>The pages selector control.</value>
    protected internal virtual PagesSelector PagesSelectorControl => this.Container.GetControl<PagesSelector>("pagesSelector", true);

    /// <summary>Gets the control that shows the selected pages.</summary>
    /// <value>The control that shows the selected pages.</value>
    protected virtual PageItemsBuilder ItemsBuilder => this.Container.GetControl<PageItemsBuilder>("customSelectedPages", true);

    /// <summary>
    /// Gets the jquery UI dialog which shows "Select page" dialog
    /// </summary>
    public HtmlGenericControl SelectorTag => this.Container.GetControl<HtmlGenericControl>("selectorTag", true);

    /// <summary>
    /// Gets the jquery UI dialog which shows "Select pages" dialog
    /// </summary>
    public HtmlGenericControl PagesSelectorTag => this.Container.GetControl<HtmlGenericControl>("pagesSelectorTag", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.DesignerMode = ControlDesignerModes.Simple;
      this.AdvancedModeIsDefault = false;
      if (this.PropertyEditor == null)
        return;
      string propertyValuesCulture = this.PropertyEditor.PropertyValuesCulture;
      this.PageSelectorControl.UICulture = this.PagesSelectorControl.UICulture = propertyValuesCulture;
      string specificCultureFilter = CommonMethods.GenerateSpecificCultureFilter(propertyValuesCulture, "Title");
      if (string.IsNullOrEmpty(specificCultureFilter))
        return;
      this.PageSelectorControl.ConstantFilter = this.PagesSelectorControl.ConstantFilter = specificCultureFilter;
      this.PageSelectorControl.AppendConstantFilter = this.PagesSelectorControl.AppendConstantFilter = true;
    }

    /// <summary>Gets the top level page names.</summary>
    /// <returns></returns>
    protected internal virtual string GetTopLevelPageNames()
    {
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      StringBuilder stringBuilder = new StringBuilder(50);
      try
      {
        SystemManager.CurrentContext.Culture = new CultureInfo(this.PropertyEditor.PropertyValuesCulture);
        SiteMapProvider currentProvider = SitefinitySiteMap.GetCurrentProvider();
        bool flag = true;
        foreach (SiteMapNode siteMapNode in currentProvider.RootNode.ChildNodes.OfType<SiteMapNode>())
        {
          if (!flag)
            stringBuilder.Append(",");
          stringBuilder.Append(siteMapNode.Title);
          flag = false;
        }
      }
      finally
      {
        SystemManager.CurrentContext.Culture = culture;
      }
      return stringBuilder.Length > 45 ? stringBuilder.ToString(0, 42) + "..." : stringBuilder.ToString();
    }

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
      controlDescriptor.AddComponentProperty("pageSelector", this.PageSelectorControl.ClientID);
      controlDescriptor.AddComponentProperty("pagesSelector", this.PagesSelectorControl.ClientID);
      controlDescriptor.AddComponentProperty("customSelectedPagesControl", this.ItemsBuilder.ClientID);
      controlDescriptor.AddProperty("_topLevelPageNames", (object) this.GetTopLevelPageNames());
      controlDescriptor.AddElementProperty("selectorTag", this.SelectorTag.ClientID);
      controlDescriptor.AddElementProperty("pagesSelectorTag", this.PagesSelectorTag.ClientID);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      Labels labels = Res.Get<Labels>();
      foreach (string name in Enum.GetNames(typeof (NavigationModes)))
        dictionary.Add(name, labels.Get("NavigationSelectionHeading" + name));
      controlDescriptor.AddProperty("selectionHeadingLabels", (object) dictionary);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) source.ToArray();
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.NavigationControls.Scripts.NavigationDesigner.js", this.GetType().Assembly.GetName().ToString()),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources")
    }.ToArray();
  }
}
