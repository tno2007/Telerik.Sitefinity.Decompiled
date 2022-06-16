// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.RequiresEmbeddedWebResourceAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// This class serves the purpose of registering custom resource to be included in the page.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
  public class RequiresEmbeddedWebResourceAttribute : Attribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.RequiresEmbeddedWebResourceAttribute" /> class.
    /// </summary>
    /// <param name="name">The embedded resource name.</param>
    /// <param name="typeFullName">The type used to resolve the assembly in which the embedded resource is in.</param>
    public RequiresEmbeddedWebResourceAttribute(string name, string typeFullName)
    {
      this.Validate(name);
      this.ParseResourceType(name);
      this.Name = name;
      this.TypeFullName = typeFullName;
    }

    /// <summary>Gets or sets the name of the embedded resource.</summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the full type name used to resolve the assembly in which the embedded resource is contained.
    /// </summary>
    public string TypeFullName { get; set; }

    /// <inheritdoc />
    public override int GetHashCode() => this.Name.GetHashCode() ^ this.TypeFullName.GetHashCode();

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is RequiresEmbeddedWebResourceAttribute resourceAttribute && string.Equals(this.Name, resourceAttribute.Name) && object.Equals((object) this.TypeFullName, (object) resourceAttribute.TypeFullName);

    private void Validate(string name)
    {
      if (!name.EndsWith("css", StringComparison.OrdinalIgnoreCase) && !name.EndsWith("js", StringComparison.OrdinalIgnoreCase))
        throw new ArgumentException("Only css and js files are supported");
    }

    private void ParseResourceType(string name)
    {
      if (name.EndsWith("js"))
      {
        this.ResType = RequiresEmbeddedWebResourceAttribute.ResourceType.Javascript;
      }
      else
      {
        if (!name.EndsWith("css"))
          return;
        this.ResType = RequiresEmbeddedWebResourceAttribute.ResourceType.StyleSheet;
      }
    }

    /// <summary>Gets the type of the embedded resource.</summary>
    public RequiresEmbeddedWebResourceAttribute.ResourceType ResType { get; private set; }

    /// <summary>Defines the embedded resource type.</summary>
    public enum ResourceType
    {
      /// <summary>Represents JavaScript resources.</summary>
      Javascript,
      /// <summary>Represents StyleSheet resources.</summary>
      StyleSheet,
    }
  }
}
