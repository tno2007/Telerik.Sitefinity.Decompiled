// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.ExternalPageFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// Providers the configuration element for ExternalPageField
  /// </summary>
  public class ExternalPageFieldElement : 
    FieldControlDefinitionElement,
    IExternalPageFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.ExternalPageFieldElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ExternalPageFieldElement generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ExternalPageFieldElement, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ExternalPageFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ExternalPageFieldDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the definition for the child ChoiceField control indicating if the page is external.
    /// </summary>
    /// <value>The choice field with value if the page is external.</value>
    [ConfigurationProperty("isExternalPageChoiceFieldDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsExternalPageChoiceFieldDefinitionDescription", Title = "IsExternalPageChoiceFieldDefinitionCaption")]
    public ChoiceFieldElement IsExternalPageChoiceFieldDefinition
    {
      get => (ChoiceFieldElement) this["isExternalPageChoiceFieldDefinition"];
      set => this["isExternalPageChoiceFieldDefinition"] = (object) value;
    }

    /// <summary>The guid of the site page to be redirected to.</summary>
    [ConfigurationProperty("internalPageId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "", Title = "")]
    public Guid InternalPageId
    {
      get => (Guid) this["internalPageId"];
      set => this["internalPageId"] = (object) value;
    }

    /// <summary>The url of the external page to be redirected to.</summary>
    [ConfigurationProperty("externalPageUrlFieldDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "", Title = "")]
    public TextFieldDefinitionElement ExternalPageUrlFieldDefinition
    {
      get => (TextFieldDefinitionElement) this["externalPageUrlFieldDefinition"];
      set => this["externalPageUrlFieldDefinition"] = (object) value;
    }

    /// <summary>The url of the external page to be redirected to.</summary>
    [ConfigurationProperty("openInNewWindowChoiceFieldDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "", Title = "")]
    public ChoiceFieldElement OpenInNewWindowChoiceFieldDefinition
    {
      get => (ChoiceFieldElement) this["openInNewWindowChoiceFieldDefinition"];
      set => this["openInNewWindowChoiceFieldDefinition"] = (object) value;
    }

    IChoiceFieldDefinition IExternalPageFieldDefinition.IsExternalPageChoiceFieldDefinition => (IChoiceFieldDefinition) this.IsExternalPageChoiceFieldDefinition;

    Guid IExternalPageFieldDefinition.InternalPageId => this.InternalPageId;

    ITextFieldDefinition IExternalPageFieldDefinition.ExternalPageUrlFieldDefinition => (ITextFieldDefinition) this.ExternalPageUrlFieldDefinition;

    IChoiceFieldDefinition IExternalPageFieldDefinition.OpenInNewWindowChoiceFieldDefinition => (IChoiceFieldDefinition) this.OpenInNewWindowChoiceFieldDefinition;

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (ExternalPageField);

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropertyNames
    {
      public const string isExternalPageChoiceFieldDefinition = "isExternalPageChoiceFieldDefinition";
      public const string internalPageId = "internalPageId";
      public const string externalPageUrlFieldDefinition = "externalPageUrlFieldDefinition";
      public const string openInNewWindowChoiceFieldDefinition = "openInNewWindowChoiceFieldDefinition";
    }
  }
}
