// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentPropertyResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity
{
  /// <summary>
  /// Helper class for resolving properties that have default value specified via configuration and can have different value specified at UI.
  /// The approach to set the property value when property is resolved is if property do not have value return default value.
  /// Default values are determined with following priority:
  ///     1) parent content type
  ///     2) module level
  ///     3) configuration
  /// </summary>
  public class ContentPropertyResolver : ConfigPropertyResolver
  {
    private Content contentItem;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ContentPropertyResolver" /> class.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="virtualPath">The virtual path to resolve configuration property.</param>
    public ContentPropertyResolver(Content contentItem, string configPath)
      : base(configPath)
    {
      if (contentItem == null)
        return;
      this.contentItem = contentItem;
      this.RegisterSettingsResolverMethod(new SettingsResolverDelegate(this.GetSettingsObject));
    }

    private object GetSettingsObject()
    {
      if (this.contentItem is IHasParent)
      {
        Content parent = ((IHasParent) this.contentItem).Parent;
        if (parent != null)
          return (object) parent;
      }
      return (object) null;
    }
  }
}
