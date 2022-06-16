// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.ContentViewPlugInDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// A class that has all needed information to construct the content view plugIn
  /// </summary>
  public class ContentViewPlugInDefinition : 
    DefinitionBase,
    IContentViewPlugInDefinition,
    IDefinition
  {
    private string name;
    private int? ordinal;
    private string placeHolderId;
    private string virtualPath;
    private Type plugInType;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    public ContentViewPlugInDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ContentViewPlugInDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ContentViewPlugInDefinition GetDefinition() => this;

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    public string ControlDefinitionName { get; private set; }

    /// <summary>
    /// Gets or sets the name of the view. Used for resolving property values.
    /// </summary>
    /// <value>The name of the view.</value>
    public string ViewName { get; private set; }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    public string Name
    {
      get => this.ResolveProperty<string>(nameof (Name), this.name);
      set => this.name = value;
    }

    /// <summary>Gets or sets the ordinal.</summary>
    /// <value>The ordinal.</value>
    public int? Ordinal
    {
      get => this.ResolveProperty<int?>(nameof (Ordinal), this.ordinal);
      set => this.ordinal = value;
    }

    /// <summary>Gets or sets the place holder pageId.</summary>
    /// <value>The place holder pageId.</value>
    public string PlaceHolderId
    {
      get => this.ResolveProperty<string>(nameof (PlaceHolderId), this.placeHolderId);
      set => this.placeHolderId = value;
    }

    /// <summary>Gets or sets the virtual path.</summary>
    /// <value>The virtual path.</value>
    public string VirtualPath
    {
      get => this.ResolveProperty<string>(nameof (VirtualPath), this.virtualPath);
      set => this.virtualPath = value;
    }

    /// <summary>Gets or sets the type of the plugIn type.</summary>
    /// <value>The type of the plug in.</value>
    public Type PlugInType
    {
      get => this.ResolveProperty<Type>(nameof (PlugInType), this.plugInType);
      set => this.plugInType = value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ContentViewPlugInProps
    {
      public const string Name = "Name";
      public const string Ordinal = "Ordinal";
      public const string PlaceHolderId = "PlaceHolderId";
      public const string VirtualPath = "VirtualPath";
      public const string PlugInType = "PlugInType";
    }
  }
}
