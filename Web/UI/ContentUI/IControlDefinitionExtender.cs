// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.IControlDefinitionExtender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// Defines a common interface for components that can extend <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" /> instances.
  /// Useful to plug in additional UI components in existing screens, rendered based on definitions.
  /// </summary>
  public interface IControlDefinitionExtender
  {
    /// <summary>
    /// This method will be executed when a <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition" /> is being initialized
    /// and its instance will be passed for modifications as the <paramref name="contentViewControlDefinition" /> parameter.
    /// </summary>
    /// <param name="contentViewControlDefinition">The content view control definition to modify.</param>
    void ExtendDefinition(
      IContentViewControlDefinition contentViewControlDefinition);
  }
}
