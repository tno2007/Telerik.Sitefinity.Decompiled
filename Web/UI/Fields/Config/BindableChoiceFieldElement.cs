// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.BindableChoiceFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions.CodeQuality;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// The configuration element for bindable selector fields.
  /// </summary>
  [ApprovedBy("Boyan Rabchev", "2012/02/03")]
  public class BindableChoiceFieldElement : 
    FieldControlDefinitionElement,
    IBindableChoiceFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    internal const string WebServiceUrlPropertyName = "webServiceUrl";
    internal const string ProviderNamePropertyName = "providerName";
    internal const string BindOnLoadPropertyName = "bindOnLoad";
    internal const string CreateHyperLinkTitlePropertyName = "createHyperLinkTitle";
    internal const string ExampleHyperLinkTitlePropertyName = "exampleHyperLinkTitle";
    internal const string FilterExpressionPropertyName = "filterExpression";
    internal const string SortExpressionPropertyName = "sortExpression";
    internal const string CreatePromptTitlePropertyName = "CreatePromptTitle";
    internal const string CreatePromptTextFieldTitlePropertyName = "CreatePromptTextFieldTitle";
    internal const string CreatePromptExampleTextPropertyName = "CreatePromptExampleText";
    internal const string CreatePromptCreateButtonTitlePropertyName = "CreatePromptCreateButtonTitle";
    internal const string CreateNewItemServiceUrlPropertyName = "CreateNewItemServiceUrl";

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public BindableChoiceFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new BindableChoiceFieldDefinition((ConfigElement) this);

    /// <summary>Gets or sets the web service URL.</summary>
    [ConfigurationProperty("webServiceUrl")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WebServiceUrlDescription", Title = "WebServiceUrlCaption")]
    public string WebServiceUrl
    {
      get => (string) this["webServiceUrl"];
      set => this["webServiceUrl"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the provider from which the corresponding object ought to be selected.
    /// </summary>
    [ConfigurationProperty("providerName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProviderNameDescription", Title = "ProviderNameCaption")]
    public string ProviderName
    {
      get => (string) this["providerName"];
      set => this["providerName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to automatically bind the drop down on load.
    /// </summary>
    /// <value><c>true</c> if to bind on load; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("bindOnLoad")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BindOnLoadDescription", Title = "BindOnLoadCaption")]
    public bool? BindOnLoad
    {
      get => (bool?) this["bindOnLoad"];
      set => this["bindOnLoad"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value to prepopulate the title of the create hyperlink.
    /// </summary>
    [ConfigurationProperty("createHyperLinkTitle")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CreateHyperLinkTitleDescription", Title = "CreateHyperLinkTitleCaption")]
    public string CreateHyperLinkTitle
    {
      get => (string) this["createHyperLinkTitle"];
      set => this["createHyperLinkTitle"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value to prepopulate the title of the example hyperlink.
    /// </summary>
    [ConfigurationProperty("exampleHyperLinkTitle")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ExampleHyperLinkTitleDescription", Title = "ExampleHyperLinkTitleCaption")]
    public string ExampleHyperLinkTitle
    {
      get => (string) this["exampleHyperLinkTitle"];
      set => this["exampleHyperLinkTitle"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("filterExpression")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FilterExpressionDescription", Title = "FilterExpressionCaption")]
    public string FilterExpression
    {
      get => (string) this["filterExpression"];
      set => this["filterExpression"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("sortExpression")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SortExpressionDescription", Title = "SortExpressionCaption")]
    public string SortExpression
    {
      get => (string) this["sortExpression"];
      set => this["sortExpression"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("CreatePromptTitle")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CreatePromptTitleDescription", Title = "CreatePromptTitleCaption")]
    public string CreatePromptTitle
    {
      get => (string) this[nameof (CreatePromptTitle)];
      set => this[nameof (CreatePromptTitle)] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("CreatePromptTextFieldTitle")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CreatePromptTextFieldTitleDescription", Title = "CreatePromptTextFieldTitleCaption")]
    public string CreatePromptTextFieldTitle
    {
      get => (string) this[nameof (CreatePromptTextFieldTitle)];
      set => this[nameof (CreatePromptTextFieldTitle)] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("CreatePromptExampleText")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CreatePromptExampleTextDescription", Title = "CreatePromptExampleTextCaption")]
    public string CreatePromptExampleText
    {
      get => (string) this[nameof (CreatePromptExampleText)];
      set => this[nameof (CreatePromptExampleText)] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("CreatePromptCreateButtonTitle")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CreatePromptCreateButtonTitleDescription", Title = "CreatePromptCreateButtonTitleCaption")]
    public string CreatePromptCreateButtonTitle
    {
      get => (string) this[nameof (CreatePromptCreateButtonTitle)];
      set => this[nameof (CreatePromptCreateButtonTitle)] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("CreateNewItemServiceUrl")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CreateNewItemServiceUrlDescription", Title = "CreateNewItemServiceUrlCaption")]
    public string CreateNewItemServiceUrl
    {
      get => (string) this[nameof (CreateNewItemServiceUrl)];
      set => this[nameof (CreateNewItemServiceUrl)] = (object) value;
    }
  }
}
