// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IBindableChoiceFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Abstractions.CodeQuality;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the definition of a bindable choice field element.
  /// </summary>
  [ApprovedBy("Boyan Rabchev", "2012/02/03")]
  public interface IBindableChoiceFieldDefinition : 
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>Gets or sets the web service URL.</summary>
    string WebServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider from which the corresponding object ought to be selected.
    /// </summary>
    string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to automatically bind the drop down on load.
    /// </summary>
    /// <value><c>true</c> if to bind on load; otherwise, <c>false</c>.</value>
    bool? BindOnLoad { get; set; }

    /// <summary>
    /// Gets or sets the name of the create hyper link title property.
    /// </summary>
    string CreateHyperLinkTitle { get; set; }

    /// <summary>
    /// Gets or sets the name of the example hyper link title property.
    /// </summary>
    string ExampleHyperLinkTitle { get; set; }

    /// <summary>Gets or sets the binder default filter expression.</summary>
    string FilterExpression { get; set; }

    /// <summary>Gets or sets the binder default sort expression.</summary>
    string SortExpression { get; set; }

    string CreatePromptTitle { get; set; }

    string CreatePromptTextFieldTitle { get; set; }

    string CreatePromptExampleText { get; set; }

    string CreatePromptCreateButtonTitle { get; set; }

    string CreateNewItemServiceUrl { get; set; }
  }
}
