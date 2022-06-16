// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.FolderField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields
{
  /// <summary>A field control for selecting a specific library</summary>
  public class FolderField : PageField
  {
    private string treeViewCssClass = "sfTreeView";
    private const string sfTreeViewSingleSelectCss = "sfTreeViewSingle";
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.Fields.FolderField.ascx");
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Scripts.FolderField.js";
    private const string iRequiresProviderScript = "Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js";
    private string webServiceUrl;
    private string providerName;
    private bool bindOnLoad;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.ParentLibraryField" /> class.
    /// </summary>
    public FolderField() => this.LayoutTemplatePath = FolderField.layoutTemplatePath;

    /// <summary>
    /// Gets the localizable string that represents the name of the item in singular.
    /// </summary>
    public string ItemName { get; set; }

    /// <summary>Gets or sets the name of the library type.</summary>
    public string LibraryTypeName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the button for creating new library.
    /// </summary>
    public bool ShowCreateNewLibraryButton { get; set; }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    public override string WebServiceUrl
    {
      get => this.webServiceUrl;
      set
      {
        this.webServiceUrl = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets or sets the web create folder Service URL.</summary>
    /// <value>The web service URL.</value>
    public string CreateLibraryServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider from which the page node ought to be selected.
    /// </summary>
    public override string ProviderName
    {
      get => this.providerName;
      set
      {
        this.providerName = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets the option to automatically bind the selector when the control loads
    /// </summary>
    public override bool BindOnLoad
    {
      get => this.bindOnLoad;
      set
      {
        this.bindOnLoad = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets the configured value of how many items should be displayed on the first load. This configuration enables the control to load items only when required.
    /// </summary>
    public int ItemsCount => Config.Get<LibrariesConfig>().ItemsCount;

    /// <summary>
    /// Gets or sets the name of the CSS class applied to the tree view.
    /// </summary>
    /// <value>The CSS class.</value>
    protected virtual string TreeViewCssClass
    {
      get => this.treeViewCssClass;
      set => this.treeViewCssClass = value;
    }

    /// <summary>
    /// Gets the reference to the folder selector title control.
    /// </summary>
    protected virtual Literal FolderSelectorTitle => this.Container.GetControl<Literal>(nameof (FolderSelectorTitle), true);

    /// <summary>
    /// Gets a reference to the link for creating a new library
    /// </summary>
    protected virtual LinkButton CreateNewLibraryButton => this.Container.GetControl<LinkButton>("createNewLibraryButton", true);

    /// <summary>
    /// Gets the jquery UI dialog which shows "Create library" dialog
    /// </summary>
    protected virtual HtmlGenericControl CreateLibraryContainer => this.Container.GetControl<HtmlGenericControl>("createLibraryContainer", true);

    /// <summary>Gets the create library control.</summary>
    protected virtual CreateLibraryControl CreateLibraryControl => this.Container.GetControl<CreateLibraryControl>("createLibraryControl", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.FolderSelectorTitle.Text = string.IsNullOrEmpty(this.ItemName) ? Res.Get<LibrariesResources>().SelectALibrary : string.Format(Res.Get<LibrariesResources>().SelectLibraryCommon, (object) this.ItemName.ToLower());
      if (this.PageSelector != null)
      {
        string str = VirtualPathUtility.AppendTrailingSlash(this.WebServiceUrl);
        this.PageSelector.WebServiceUrl = str;
        this.PageSelector.OrginalServiceBaseUrl = str;
        this.PageSelector.ServiceChildItemsBaseUrl = str;
        this.PageSelector.ServicePredecessorBaseUrl = str + "predecessors/";
        this.PageSelector.ServiceTreeUrl = str + "tree/";
        this.PageSelector.ConstantFilter = (string) null;
      }
      if (this.ShowCreateNewLibraryButton)
      {
        this.CreateNewLibraryButton.Visible = true;
        this.CreateLibraryContainer.Visible = true;
        this.CreateLibraryControl.LibraryTypeName = this.LibraryTypeName;
        this.CreateLibraryControl.CreateLibraryServicePath = this.CreateLibraryServiceUrl;
        this.CreateLibraryControl.FoldersSelectorServiceUrl = this.WebServiceUrl;
      }
      else
      {
        this.CreateNewLibraryButton.Visible = false;
        this.CreateLibraryContainer.Visible = false;
      }
      if (this.TreeViewCssClass != null)
        this.PageSelector.TreeViewCssClass = this.TreeViewCssClass;
      if (this.PageSelector.AllowMultipleSelection)
        return;
      this.PageSelector.TreeViewCssClass += " sfTreeViewSingle";
    }

    /// <inheritdoc />
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is FolderFieldDefinition folderFieldDefinition))
        return;
      this.ItemName = Res.Get(folderFieldDefinition.ResourceClassId, folderFieldDefinition.ItemName);
      this.ShowCreateNewLibraryButton = folderFieldDefinition.ShowCreateNewLibraryButton;
      this.CreateLibraryServiceUrl = folderFieldDefinition.CreateLibraryServiceUrl;
      this.LibraryTypeName = folderFieldDefinition.LibraryTypeName;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().LastOrDefault<ScriptDescriptor>() as ScriptControlDescriptor;
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
      {
        {
          "Change",
          Res.Get<Labels>().Change
        },
        {
          "SelectPage",
          Res.Get<LibrariesResources>().SelectALibrary
        }
      };
      controlDescriptor.AddProperty("resources", (object) dictionary);
      controlDescriptor.AddProperty("_itemsCount", (object) this.ItemsCount);
      if (this.ShowCreateNewLibraryButton)
      {
        controlDescriptor.AddElementProperty("createNewLibraryButton", this.CreateNewLibraryButton.ClientID);
        controlDescriptor.AddElementProperty("createLibraryContainer", this.CreateLibraryContainer.ClientID);
        controlDescriptor.AddComponentProperty("createLibraryControl", this.CreateLibraryControl.ClientID);
      }
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferenceList = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = typeof (FolderField).Assembly.FullName;
      scriptReferenceList.Add(new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js"
      });
      scriptReferenceList.Add(new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Scripts.FolderField.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferenceList.ToArray();
    }
  }
}
