// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ResourceFile
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents the single resource to be be linked with ResourceLink controls Links collection.
  /// </summary>
  [DebuggerDisplay("ResourceFile - Name:{Name}")]
  public class ResourceFile
  {
    private bool isFromCustomTheme;
    private bool? staticResource;

    /// <summary>
    /// Gets or sets the library to be embedded by this resource. The default is none.
    /// </summary>
    public JavaScriptLibrary JavaScriptLibrary { get; set; }

    /// <summary>Gets or sets the name of the resource.</summary>
    /// <value>The name of the resource.</value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the assembly info. Set this value to the full name of any type in the
    /// assembly where the resource is embedded.
    /// </summary>
    /// <value>The assembly info.</value>
    public string AssemblyInfo { get; set; }

    /// <summary>
    /// Gets or sets whether the current resource is defined in the theme. This is used to change the order of rendering of CSS links.
    /// CSS links from themes are rendered after RadControls links and before CSS widget links/styles.
    /// </summary>
    /// <value>Whether the current resource is from custom theme.</value>
    public bool IsFromTheme
    {
      get => this.isFromCustomTheme;
      set => this.isFromCustomTheme = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="!:Resource" /> is static.
    /// Static resources will not be pulled from the theme folder, but rather a full url
    /// for external or full resource name for embedded resources should be used.
    /// </summary>
    /// <remarks>
    /// For external templates, tilde "~" can be used to mark the root of the application.
    /// </remarks>
    /// <value><c>true</c> if static; otherwise, <c>false</c>.</value>
    public bool Static
    {
      get
      {
        if (!this.staticResource.HasValue)
          this.staticResource = new bool?(!this.Name.EndsWith(".css") && !this.Name.EndsWith(".less"));
        return this.staticResource.Value;
      }
      set => this.staticResource = new bool?(value);
    }

    internal string GetPath() => string.IsNullOrEmpty(this.Version) ? this.Name : this.Name + "?v=" + this.Version;

    internal string Version { get; set; }

    /// <summary>Gets or sets the type of the resource.</summary>
    public WebResourceType? Type { get; set; }
  }
}
