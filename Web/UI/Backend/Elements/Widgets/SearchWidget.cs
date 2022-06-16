// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SearchWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>
  /// /// Type that constructs search widget user interface element. All widgets should inherit <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.IWidget" /> interface.
  /// </summary>
  public class SearchWidget : CommandWidget
  {
    private string closeSearchCommandName;
    private string templatePath;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Search.SearchWidget.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(this.templatePath) ? SearchWidget.layoutTemplatePath : this.templatePath;
      set => this.templatePath = value;
    }

    /// <summary>
    /// Gets or sets the name of the command that widget fires.
    /// </summary>
    /// <value></value>
    public override string CommandName
    {
      get => string.IsNullOrEmpty(base.CommandName) ? "search" : base.CommandName;
      set => base.CommandName = value;
    }

    /// <summary>
    /// Gets or sets the name of the command that widget fires when closing search.
    /// </summary>
    /// <value></value>
    public virtual string CloseSearchCommandName
    {
      get => string.IsNullOrEmpty(this.closeSearchCommandName) ? "closeSearch" : this.closeSearchCommandName;
      set => this.closeSearchCommandName = value;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      foreach (ScriptReference scriptReference in base.GetScriptReferences())
        yield return scriptReference;
      string fullName = typeof (SearchWidget).Assembly.FullName;
      yield return new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.SearchWidget.js"
      };
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      SearchWidget searchWidget = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated method
      ScriptBehaviorDescriptor behaviorDescriptor = searchWidget.\u003C\u003En__1().First<ScriptDescriptor>() as ScriptBehaviorDescriptor;
      behaviorDescriptor.AddComponentProperty("searchBox", searchWidget.SimpleSearchBox.ClientID);
      behaviorDescriptor.AddElementProperty("hideSearchBoxLink", searchWidget.HideSearchBoxLink.ClientID);
      behaviorDescriptor.AddElementProperty("showSearchBoxLink", searchWidget.ShowSearchBoxLink.ClientID);
      behaviorDescriptor.AddElementProperty("searchPlaceHolder", searchWidget.SearchPlaceHolder.ClientID);
      behaviorDescriptor.AddProperty("_searchCommandName", (object) searchWidget.CommandName);
      behaviorDescriptor.AddProperty("_closeSearchCommandName", (object) searchWidget.CloseSearchCommandName);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (ScriptDescriptor) behaviorDescriptor;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    /// <summary>Configures the specified definition.</summary>
    /// <param name="definition">The definition.</param>
    public override void Configure(IWidgetDefinition definition)
    {
      base.Configure(definition);
      this.Definition = definition;
      if (!(definition is ISearchWidgetDefinition))
        return;
      this.CloseSearchCommandName = ((ISearchWidgetDefinition) definition).CloseSearchCommandName;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the link button showing the search box.</summary>
    protected virtual LinkButton ShowSearchBoxLink => this.Container.GetControl<LinkButton>("showSearchBoxLink", true);

    /// <summary>Gets the link button hiding the search box.</summary>
    protected virtual LinkButton HideSearchBoxLink => this.Container.GetControl<LinkButton>("hideSearchBoxLink", true);

    /// <summary>Gets the link button hiding the search box.</summary>
    protected virtual HtmlGenericControl SearchPlaceHolder => this.Container.GetControl<HtmlGenericControl>("searchPlaceHolder", true);

    /// <summary>Gets the simple search box.</summary>
    /// <value>The simple search box.</value>
    protected virtual BackendSearchBox SimpleSearchBox => this.Container.GetControl<BackendSearchBox>("searchBox", true);

    /// <summary>
    /// Gets or sets the definition for constuction the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget" /> control.
    /// </summary>
    /// <value>The definition.</value>
    public new IWidgetDefinition Definition { get; set; }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Scripts
    {
      public const string SearchWidget = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.SearchWidget.js";
    }
  }
}
