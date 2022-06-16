// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.BindableChoiceFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions.CodeQuality;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  ///  A definition that provides the common members of a bindable choice field element.
  /// </summary>
  [ApprovedBy("Boyan Rabchev", "2012/02/03")]
  public class BindableChoiceFieldDefinition : 
    FieldControlDefinition,
    IBindableChoiceFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private string webServiceUrl;
    private string providerName;
    private bool? bindOnLoad;
    private string createHyperLinkTitle;
    private string exampleHyperLinkTitle;
    private string filterExpression;
    private string sortExpression;
    private string createPromptTitle;
    private string createPromptTextFieldTitle;
    private string createPromptExampleText;
    private string createPromptCreateButtonTitle;
    private string createPromptCancelButtonTitle;
    private string createNewItemServiceUrl;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.BindableChoiceFieldDefinition" /> class.
    /// </summary>
    public BindableChoiceFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.BindableChoiceFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public BindableChoiceFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (BindableChoiceField);

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    public string WebServiceUrl
    {
      get => this.ResolveProperty<string>(nameof (WebServiceUrl), this.webServiceUrl);
      set => this.webServiceUrl = value;
    }

    /// <summary>
    /// Gets or sets the name of the provider from which the corresponding object ought to be selected.
    /// </summary>
    public string ProviderName
    {
      get => this.ResolveProperty<string>(nameof (ProviderName), this.providerName);
      set => this.providerName = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to automatically bind the selector on load.
    /// </summary>
    /// <value><c>true</c> if to bind on load; otherwise, <c>false</c>.</value>
    public bool? BindOnLoad
    {
      get => this.ResolveProperty<bool?>(nameof (BindOnLoad), this.bindOnLoad);
      set => this.bindOnLoad = value;
    }

    /// <summary>
    /// Gets or sets the HyperLink title from which the dialog for create should be opened.
    /// </summary>
    public string CreateHyperLinkTitle
    {
      get => this.ResolveProperty<string>(nameof (CreateHyperLinkTitle), this.createHyperLinkTitle);
      set => this.createHyperLinkTitle = value;
    }

    /// <summary>
    /// Gets or sets the ExampleLink title from which the example dialog should be opened.
    /// </summary>
    public string ExampleHyperLinkTitle
    {
      get => this.ResolveProperty<string>(nameof (ExampleHyperLinkTitle), this.exampleHyperLinkTitle);
      set => this.exampleHyperLinkTitle = value;
    }

    /// <inheritdoc />
    public string FilterExpression
    {
      get => this.ResolveProperty<string>(nameof (FilterExpression), this.filterExpression);
      set => this.filterExpression = value;
    }

    /// <inheritdoc />
    public string SortExpression
    {
      get => this.ResolveProperty<string>(nameof (SortExpression), this.sortExpression);
      set => this.sortExpression = value;
    }

    /// <inheritdoc />
    public string CreatePromptTitle
    {
      get => this.ResolveProperty<string>(nameof (CreatePromptTitle), this.createPromptTitle);
      set => this.createPromptTitle = value;
    }

    /// <inheritdoc />
    public string CreatePromptTextFieldTitle
    {
      get => this.ResolveProperty<string>(nameof (CreatePromptTextFieldTitle), this.createPromptTextFieldTitle);
      set => this.createPromptTextFieldTitle = value;
    }

    /// <inheritdoc />
    public string CreatePromptExampleText
    {
      get => this.ResolveProperty<string>(nameof (CreatePromptExampleText), this.createPromptExampleText);
      set => this.createPromptExampleText = value;
    }

    /// <inheritdoc />
    public string CreatePromptCreateButtonTitle
    {
      get => this.ResolveProperty<string>(nameof (CreatePromptCreateButtonTitle), this.createPromptCreateButtonTitle);
      set => this.createPromptCreateButtonTitle = value;
    }

    /// <inheritdoc />
    public string CreatePromptCancelButtonTitle
    {
      get => this.ResolveProperty<string>(nameof (CreatePromptCancelButtonTitle), this.createPromptCancelButtonTitle);
      set => this.createPromptCancelButtonTitle = value;
    }

    /// <inheritdoc />
    public string CreateNewItemServiceUrl
    {
      get => this.ResolveProperty<string>(nameof (CreateNewItemServiceUrl), this.createNewItemServiceUrl);
      set => this.createNewItemServiceUrl = value;
    }
  }
}
