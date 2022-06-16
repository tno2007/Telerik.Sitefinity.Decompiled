// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.ExternalPageFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// Contains all properties needed to construct an instance of the ExternalPageField control
  /// </summary>
  public class ExternalPageFieldDefinition : 
    FieldControlDefinition,
    IExternalPageFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    private IChoiceFieldDefinition isExternalPageChoiceFieldDefinition;
    private Guid internalPageId;
    private ITextFieldDefinition externalPageUrlFieldDefinition;
    private IChoiceFieldDefinition openInNewWindowChoiceFieldDefinition;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.ExternalPageFieldDefinition" /> class.
    /// </summary>
    public ExternalPageFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ExternalPageFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>
    /// Gets or sets the definition for the child ChoiceField control indicating if the page is external.
    /// </summary>
    /// <value>The choice field with value if the page is external.</value>
    public IChoiceFieldDefinition IsExternalPageChoiceFieldDefinition
    {
      get => this.ResolveProperty<IChoiceFieldDefinition>(nameof (IsExternalPageChoiceFieldDefinition), this.isExternalPageChoiceFieldDefinition);
      set => this.isExternalPageChoiceFieldDefinition = value;
    }

    /// <summary>The guid of the site page to be redirected to.</summary>
    public Guid InternalPageId
    {
      get => this.ResolveProperty<Guid>(nameof (InternalPageId), this.internalPageId);
      set => this.internalPageId = value;
    }

    /// <summary>The url of the external page to be redirected to.</summary>
    /// <value>The text field with the external page url to redirect to.</value>
    public ITextFieldDefinition ExternalPageUrlFieldDefinition
    {
      get => this.ResolveProperty<ITextFieldDefinition>(nameof (ExternalPageUrlFieldDefinition), this.externalPageUrlFieldDefinition);
      set => this.externalPageUrlFieldDefinition = value;
    }

    /// <summary>
    /// Gets or sets the definition for the child ChoiceField control indicating if the page is to be open in a new window.
    /// </summary>
    /// <value>The choice field with value if the page will be open in a new window.</value>
    public IChoiceFieldDefinition OpenInNewWindowChoiceFieldDefinition
    {
      get => this.ResolveProperty<IChoiceFieldDefinition>(nameof (OpenInNewWindowChoiceFieldDefinition), this.openInNewWindowChoiceFieldDefinition);
      set => this.openInNewWindowChoiceFieldDefinition = value;
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (ExternalPageField);
  }
}
