// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewControlDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Contracts
{
  /// <summary>
  /// Defines the mandated members that need to be implemented by every type that
  /// represents a definition of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentView" /> control.
  /// </summary>
  public interface IContentViewControlDefinition : IDefinition
  {
    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    string ControlDefinitionName { get; set; }

    /// <summary>Gets or sets the type of the content view.</summary>
    /// `
    ///             <value>The type of the content.</value>
    Type ContentType { get; set; }

    /// <summary>Gets or sets the type of the manager.</summary>
    /// `
    ///             <value>The type of the manager.</value>
    Type ManagerType { get; set; }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather this control should use workflow.
    /// </summary>
    bool? UseWorkflow { get; set; }

    /// <summary>Gets the views.</summary>
    /// <value>The views.</value>
    [TypeConverter(typeof (GenericCollectionConverter))]
    ViewDefinitionCollection Views { get; }

    /// <summary>Gets the collection of dialog definitions.</summary>
    IEnumerable<IDialogDefinition> Dialogs { get; }

    /// <summary>Gets the default master view.</summary>
    /// <returns></returns>
    IContentViewDefinition GetDefaultMasterView();

    /// <summary>Gets the default detail view.</summary>
    /// <returns></returns>
    IContentViewDefinition GetDefaultDetailView();

    /// <summary>Tries the get view.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="view">The view.</param>
    /// <returns></returns>
    bool TryGetView(string viewName, out IContentViewDefinition view);

    /// <summary>
    /// Determines whether the views collection contains view with the specified name.
    /// </summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns>
    /// 	<c>true</c> if the view exists; otherwise, <c>false</c>.
    /// </returns>
    bool ContainsView(string viewName);
  }
}
