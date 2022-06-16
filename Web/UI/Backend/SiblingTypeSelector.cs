// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.SiblingTypeSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>
  /// Control for populating a section of a menu, with <ul><li></li></ul> rendered elements
  /// </summary>
  public class SiblingTypeSelector : SimpleScriptView
  {
    private const string ClickMenuScript = "Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.SiblingTypeSelector" /> class
    /// </summary>
    public SiblingTypeSelector() => this.LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.SiblingTypeSelector.ascx");

    /// <summary>Gets or sets the current dynamic module type</summary>
    public Type CurrentType { get; set; }

    /// <summary>Gets or sets the provider name</summary>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the language</summary>
    public string Language { get; set; }

    /// <summary>Gets or sets the system parent url</summary>
    public string SystemParenUrl { get; set; }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(typeof (SiblingTypeSelector).FullName, this.ClientID);
      behaviorDescriptor.AddElementProperty("siblingTypeSelector", this.SiblingTypeSelectorMenu.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>((IEnumerable<ScriptReference>) PageManager.GetScriptReferences(ScriptRef.JQueryUI));
      string str = typeof (SiblingTypeSelector).Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name));
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Scripts.SiblingTypeSelector.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container of the control</param>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      if (this.CurrentType != (Type) null)
      {
        DynamicModuleType dynamicModuleType1 = manager.GetDynamicModuleType(this.CurrentType);
        IEnumerable<DynamicModuleType> childTypes = manager.GetChildTypes(dynamicModuleType1.ParentModuleType);
        List<object> objectList = new List<object>();
        if (childTypes != null && childTypes.Count<DynamicModuleType>() > 0)
        {
          foreach (DynamicModuleType dynamicModuleType2 in childTypes)
          {
            if (BackendSiteMap.FindSiteMapNode(dynamicModuleType2.PageId, true) != null)
            {
              string nodeReference = RouteHelper.CreateNodeReference(dynamicModuleType2.PageId) + this.SystemParenUrl + "/";
              objectList.Add((object) new
              {
                Name = PluralsResolver.Instance.ToPlural(dynamicModuleType2.DisplayName),
                NavigateUrl = (dynamicModuleType2.Id == dynamicModuleType1.Id ? "#" : this.ResolveNavigateUrl(nodeReference)),
                Class = (dynamicModuleType2.Id == dynamicModuleType1.Id ? "sfDisabled" : string.Empty)
              });
            }
          }
          this.Visible = objectList.Count > 0;
        }
        else
          this.Visible = false;
        this.CurrentTypeNameLiteral.Text = PluralsResolver.Instance.ToPlural(dynamicModuleType1.DisplayName);
        this.ItemsRepeater.DataSource = (object) objectList;
        this.ItemsRepeater.DataBind();
      }
      base.OnPreRender(e);
    }

    /// <summary>
    /// Gets the embedded path of the template used for this control
    /// </summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets a repeater listing the menu items</summary>
    protected virtual Repeater ItemsRepeater => this.Container.GetControl<Repeater>("repeaterAllChildTypes", true);

    /// <summary>
    /// Gets a literal control holding the title of the current item
    /// </summary>
    protected virtual Literal CurrentTypeNameLiteral => this.Container.GetControl<Literal>("currentTypeNameLiteral", true);

    /// <summary>Gets the html control that displays the sites menu.</summary>
    protected virtual HtmlGenericControl SiblingTypeSelectorMenu => this.Container.GetControl<HtmlGenericControl>("siblingTypeSelector", true);

    private string ResolveNavigateUrl(string nodeReference)
    {
      string str1 = "?";
      string str2 = RouteHelper.ResolveUrl(nodeReference, UrlResolveOptions.Rooted);
      if (!this.ProviderName.IsNullOrEmpty())
      {
        str2 = str2 + str1 + "provider=" + this.ProviderName;
        str1 = "&";
      }
      if (!this.Language.IsNullOrEmpty())
        str2 = str2 + str1 + "lang=" + this.Language;
      return str2;
    }
  }
}
