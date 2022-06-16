// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.ValidationMessages
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// validation messages used in content fields entry validation
  /// </summary>
  [ObjectInfo("ValidationMessages", ResourceClassId = "ValidationMessages")]
  public sealed class ValidationMessages : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.ValidationMessages" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public ValidationMessages()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.ValidationMessages" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public ValidationMessages(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Validation Messages</summary>
    [ResourceEntry("ValidationMessagesTitle", Description = "The title of this class.", LastModified = "2009/09/10", Value = "Validation Messages")]
    public string ValidationMessagesTitle => this[nameof (ValidationMessagesTitle)];

    /// <summary>Validation Messages title plural</summary>
    [ResourceEntry("ValidationMessagesTitlePlural", Description = "The title plural of this class.", LastModified = "2009/09/10", Value = "Validation Messages")]
    public string ValidationMessagesTitlePlural => this[nameof (ValidationMessagesTitlePlural)];

    /// <summary>Contains localizable validation messages.</summary>
    [ResourceEntry("ValidationMessagesDescription", Description = "The description of this class.", LastModified = "2009/09/10", Value = "Contains localizable validation messages.")]
    public string ValidationMessagesDescription => this[nameof (ValidationMessagesDescription)];

    /// <summary>
    /// Looks up a localized string similar to Value is not valid.
    /// </summary>
    [ResourceEntry("InvalidValue", Description = "label", LastModified = "2009/09/02", Value = "Value is not valid")]
    public string InvalidValue => this[nameof (InvalidValue)];

    /// <summary>
    ///   Looks up a localized string similar to Field is required.
    /// </summary>
    [ResourceEntry("RequiredValue", Description = "label", LastModified = "2009/09/02", Value = "Field is required")]
    public string RequiredValue => this[nameof (RequiredValue)];

    /// <summary>Field cannot be empty</summary>
    [ResourceEntry("CannotBeEmptyValue", Description = "label", LastModified = "2009/09/02", Value = "cannot be empty")]
    public string CannotBeEmptyValue => this[nameof (CannotBeEmptyValue)];

    /// <summary>
    ///   Looks up a localized string similar to Value must be between {0} and {1}.
    /// </summary>
    [ResourceEntry("ValueBetween", Description = "label", LastModified = "2009/09/02", Value = "Value must be between {0} and {1}")]
    public string ValueBetween => this[nameof (ValueBetween)];

    /// <summary>
    ///   Looks up a localized string similar to Value must be bigger than {0}.
    /// </summary>
    [ResourceEntry("ValueGreaterThan", Description = "label", LastModified = "2009/09/02", Value = "Value must be greater than {0}")]
    public string ValueGreaterThan => this[nameof (ValueGreaterThan)];

    /// <summary>
    ///   Looks up a localized string similar to Value must be less than {0}.
    /// </summary>
    [ResourceEntry("ValueLessThan", Description = "label", LastModified = "2009/09/02", Value = "Value must be less than {0}")]
    public string ValueLessThan => this[nameof (ValueLessThan)];
  }
}
