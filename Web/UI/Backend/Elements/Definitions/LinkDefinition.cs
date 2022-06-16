// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.LinkDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>
  /// A definition class containing all information needed to construct Link item
  /// </summary>
  public class LinkDefinition : DefinitionBase, ILinkDefinition, IDefinition
  {
    private string navigateUrl;
    private string commandName;
    private string name;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    public LinkDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public LinkDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public LinkDefinition GetDefinition() => this;

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    public string ControlDefinitionName { get; private set; }

    /// <summary>
    /// Gets or sets the name of the view. Used for resolving property values.
    /// </summary>
    /// <value>The name of the view.</value>
    public string ViewName { get; private set; }

    /// <summary>Gets or sets the name of the link.</summary>
    /// <value>The name of the link.</value>
    public string LinkName { get; private set; }

    /// <summary>
    /// Gets or sets the URL to link to when the Link item is clicked.
    /// </summary>
    /// <value></value>
    public string NavigateUrl
    {
      get => this.ResolveProperty<string>(nameof (NavigateUrl), this.navigateUrl);
      set => this.navigateUrl = value;
    }

    /// <summary>Gets or sets the command name of the Link item</summary>
    /// <value></value>
    public string CommandName
    {
      get => this.ResolveProperty<string>(nameof (CommandName), this.commandName);
      set => this.commandName = value;
    }

    /// <summary>Gets or sets the name for the Link Item</summary>
    /// <value></value>
    public string Name
    {
      get => this.ResolveProperty<string>(nameof (Name), this.name);
      set => this.name = value;
    }

    /// <summary>Refreshes the internal state</summary>
    public void Refresh()
    {
      if (string.IsNullOrEmpty(this.NavigateUrl))
        return;
      string virtualPath = this.NavigateUrl;
      string str = string.Empty;
      int num = virtualPath.IndexOf("?");
      if (num != -1)
      {
        str = virtualPath.Substring(num);
        virtualPath = virtualPath.Substring(0, num);
      }
      if (!VirtualPathUtility.IsAppRelative(virtualPath))
        return;
      this.NavigateUrl = VirtualPathUtility.ToAbsolute(virtualPath) + str;
    }
  }
}
