// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewPlugIn
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Contracts
{
  /// <summary>Base interface for all contentview plugins</summary>
  public interface IContentViewPlugIn
  {
    /// <summary>Gets or sets the plug in definition.</summary>
    /// <value>The plug in definition.</value>
    IContentViewPlugInDefinition Definition { get; set; }

    /// <summary>Instaniates the plugin with specified display mode.</summary>
    /// <param name="displayMode">The display mode</param>
    void GeneratePlugIn(FieldDisplayMode displayMode);

    /// <summary>
    /// Instaniates the plug in with specified display mode and content.
    /// </summary>
    /// <param name="displayMode">The display mode.</param>
    /// <param name="content">The content.</param>
    void GeneratePlugIn(FieldDisplayMode displayMode, Content content);
  }
}
