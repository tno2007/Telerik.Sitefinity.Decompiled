// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.LightNavigationDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.ControlDesign.Selectors;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  /// <summary>Represents LightNavigationControl designer</summary>
  public class LightNavigationDesigner : ControlDesignerBase
  {
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.NavigationControls.LightNavigationDesigner.ascx");
    internal const string designerScript = "Telerik.Sitefinity.Web.UI.NavigationControls.Scripts.LightNavigationDesigner.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.NavigationControls.LightNavigationDesigner" /> class.
    /// </summary>
    public LightNavigationDesigner() => this.LayoutTemplatePath = LightNavigationDesigner.layoutTemplatePath;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// The script control type name passed to the <see cref="T:System.Web.UI.ScriptControlDescriptor" />. It defaults to the full name
    /// of the current object class. E.g. can be overriden to reuse the script of some of the base classes and just customize
    /// some controls server-side.
    /// </summary>
    /// <value></value>
    protected override string ScriptDescriptorTypeName => typeof (LightNavigationDesigner).FullName;

    /// <summary>Gets the page selector control.</summary>
    /// <value>The page selector control.</value>
    protected virtual PagesSelector PageSelectorControl => this.Container.GetControl<PagesSelector>("pageSelector", true);

    /// <summary>Gets the pages selector control.</summary>
    /// <value>The pages selector control.</value>
    protected virtual PagesSelector PagesSelectorControl => this.Container.GetControl<PagesSelector>("pagesSelector", true);

    /// <summary>Gets the control that shows the selected pages.</summary>
    /// <value>The control that shows the selected pages.</value>
    protected virtual PageItemsBuilder ItemsBuilder => this.Container.GetControl<PageItemsBuilder>("customSelectedPages", true);

    /// <summary>
    /// Gets the jquery UI dialog which shows "Select page" dialog
    /// </summary>
    protected virtual HtmlGenericControl SelectorTag => this.Container.GetControl<HtmlGenericControl>("selectorTag", true);

    /// <summary>
    /// Gets the jquery UI dialog which shows "Select pages" dialog
    /// </summary>
    protected virtual HtmlGenericControl PagesSelectorTag => this.Container.GetControl<HtmlGenericControl>("pagesSelectorTag", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the NavigationTemplatesSelector control.</summary>
    protected virtual TemplateSelector NavigationTemplatesSelector => this.Container.GetControl<TemplateSelector>("navigationTemplatesSelector", true);

    /// <summary>Gets the templates legend tooltip link.</summary>
    protected virtual LinkButton TemplatesLegendTooltipLink => this.Container.GetControl<LinkButton>("templatesLegendTooltipLink", true);

    /// <summary>Gets the templates legend tooltip.</summary>
    protected virtual HtmlGenericControl TemplatesLegendTooltip => this.Container.GetControl<HtmlGenericControl>("templatesLegendTooltip", true);

    /// <summary>Gets the levels to include text field.</summary>
    protected virtual DropDownList LevelsToIncludeSelect => this.Container.GetControl<DropDownList>("levelsToIncludeSelect", true);

    /// <summary>
    /// Gets or sets the CssClass for the navigation template top element.
    /// </summary>
    protected virtual TextField CssClassTextField => this.Container.GetControl<TextField>("cssClassTextField", true);

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.JQueryUI;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <exception cref="T:System.NotImplementedException"></exception>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.PropertyEditor != null)
      {
        string propertyValuesCulture = this.PropertyEditor.PropertyValuesCulture;
        this.PageSelectorControl.UICulture = this.PagesSelectorControl.UICulture = propertyValuesCulture;
        string specificCultureFilter = CommonMethods.GenerateSpecificCultureFilter(propertyValuesCulture, "Title");
        if (!string.IsNullOrEmpty(specificCultureFilter))
        {
          this.PageSelectorControl.ConstantFilter = this.PagesSelectorControl.ConstantFilter = specificCultureFilter;
          this.PageSelectorControl.AppendConstantFilter = this.PagesSelectorControl.AppendConstantFilter = true;
        }
      }
      this.NavigationTemplatesSelector.DesignedControlType = this.PropertyEditor.Control.GetType().FullName;
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
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("pageSelector", this.PageSelectorControl.ClientID);
      controlDescriptor.AddComponentProperty("pagesSelector", this.PagesSelectorControl.ClientID);
      controlDescriptor.AddComponentProperty("customSelectedPagesControl", this.ItemsBuilder.ClientID);
      controlDescriptor.AddProperty("_topLevelPageNames", (object) this.GetTopLevelPageNames());
      controlDescriptor.AddElementProperty("selectorTag", this.SelectorTag.ClientID);
      controlDescriptor.AddElementProperty("pagesSelectorTag", this.PagesSelectorTag.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddElementProperty("levelsToIncludeSelect", this.LevelsToIncludeSelect.ClientID);
      controlDescriptor.AddComponentProperty("cssClassTextField", this.CssClassTextField.ClientID);
      controlDescriptor.AddComponentProperty("navigationTemplatesSelector", this.NavigationTemplatesSelector.ClientID);
      controlDescriptor.AddElementProperty("templatesLegendTooltipLink", this.TemplatesLegendTooltipLink.ClientID);
      controlDescriptor.AddElementProperty("templatesLegendTooltip", this.TemplatesLegendTooltip.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.NavigationControls.Scripts.LightNavigationDesigner.js", typeof (LightNavigationDesigner).Assembly.FullName)
    };
  }
}
