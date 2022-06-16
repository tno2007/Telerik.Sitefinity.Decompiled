// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadFilterMain
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadFilterMainDescription", Name = "RadFilter.Main", ResourceClassId = "RadFilter.Main", Title = "RadFilterMainTitle", TitlePlural = "RadFilterMainTitlePlural")]
  public sealed class RadFilterMain : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadFilterMain" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadFilterMain()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadFilterMain" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadFilterMain(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>RadFilter Main</summary>
    [ResourceEntry("RadFilterMainTitle", Description = "The title of this class.", LastModified = "2010/09/28", Value = "RadFilter Main")]
    public string RadFilterMainTitle => this[nameof (RadFilterMainTitle)];

    /// <summary>RadFilter Main</summary>
    [ResourceEntry("RadFilterMainTitlePlural", Description = "The title plural of this class.", LastModified = "2010/09/28", Value = "RadFilter Main")]
    public string RadFilterMainTitlePlural => this[nameof (RadFilterMainTitlePlural)];

    /// <summary>Resource strings for RadFilter.</summary>
    [ResourceEntry("RadFilterMainDescription", Description = "The description of this class.", LastModified = "2010/09/28", Value = "Resource strings for RadFilter.")]
    public string RadFilterMainDescription => this[nameof (RadFilterMainDescription)];

    [ResourceEntry("ReservedResource", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("AddExpressionToolTip", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Add Expression")]
    public string AddExpressionToolTip => this[nameof (AddExpressionToolTip)];

    [ResourceEntry("AddGroupToolTip", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Add Group")]
    public string AddGroupToolTip => this[nameof (AddGroupToolTip)];

    [ResourceEntry("ApplyButtonText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Apply")]
    public string ApplyButtonText => this[nameof (ApplyButtonText)];

    [ResourceEntry("BetweenDelimeterText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "And")]
    public string BetweenDelimeterText => this[nameof (BetweenDelimeterText)];

    [ResourceEntry("FilterFunctionBetween", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Between")]
    public string FilterFunctionBetween => this[nameof (FilterFunctionBetween)];

    [ResourceEntry("FilterFunctionContains", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Contains")]
    public string FilterFunctionContains => this[nameof (FilterFunctionContains)];

    [ResourceEntry("FilterFunctionDoesNotContain", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "DoesNotContain")]
    public string FilterFunctionDoesNotContain => this[nameof (FilterFunctionDoesNotContain)];

    [ResourceEntry("FilterFunctionEndsWith", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "EndsWith")]
    public string FilterFunctionEndsWith => this[nameof (FilterFunctionEndsWith)];

    [ResourceEntry("FilterFunctionEqualTo", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "EqualTo")]
    public string FilterFunctionEqualTo => this[nameof (FilterFunctionEqualTo)];

    [ResourceEntry("FilterFunctionGreaterThan", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "GreaterThan")]
    public string FilterFunctionGreaterThan => this[nameof (FilterFunctionGreaterThan)];

    [ResourceEntry("FilterFunctionGreaterThanOrEqualTo", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "GreaterThanOrEqualTo")]
    public string FilterFunctionGreaterThanOrEqualTo => this[nameof (FilterFunctionGreaterThanOrEqualTo)];

    [ResourceEntry("FilterFunctionIsEmpty", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "IsEmpty")]
    public string FilterFunctionIsEmpty => this[nameof (FilterFunctionIsEmpty)];

    [ResourceEntry("FilterFunctionIsNull", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "IsNull")]
    public string FilterFunctionIsNull => this[nameof (FilterFunctionIsNull)];

    [ResourceEntry("FilterFunctionLessThan", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "LessThan")]
    public string FilterFunctionLessThan => this[nameof (FilterFunctionLessThan)];

    [ResourceEntry("FilterFunctionLessThanOrEqualTo", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "LessThanOrEqualTo")]
    public string FilterFunctionLessThanOrEqualTo => this[nameof (FilterFunctionLessThanOrEqualTo)];

    [ResourceEntry("FilterFunctionNotBetween", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "NotBetween")]
    public string FilterFunctionNotBetween => this[nameof (FilterFunctionNotBetween)];

    [ResourceEntry("FilterFunctionNotEqualTo", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "NotEqualTo")]
    public string FilterFunctionNotEqualTo => this[nameof (FilterFunctionNotEqualTo)];

    [ResourceEntry("FilterFunctionNotIsEmpty", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "NotIsEmpty")]
    public string FilterFunctionNotIsEmpty => this[nameof (FilterFunctionNotIsEmpty)];

    [ResourceEntry("FilterFunctionNotIsNull", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "NotIsNull")]
    public string FilterFunctionNotIsNull => this[nameof (FilterFunctionNotIsNull)];

    [ResourceEntry("FilterFunctionStartsWith", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "StartsWith")]
    public string FilterFunctionStartsWith => this[nameof (FilterFunctionStartsWith)];

    [ResourceEntry("GroupOperationAnd", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "And")]
    public string GroupOperationAnd => this[nameof (GroupOperationAnd)];

    [ResourceEntry("GroupOperationNotAnd", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Not And")]
    public string GroupOperationNotAnd => this[nameof (GroupOperationNotAnd)];

    [ResourceEntry("GroupOperationNotOr", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Not Or")]
    public string GroupOperationNotOr => this[nameof (GroupOperationNotOr)];

    [ResourceEntry("GroupOperationOr", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Or")]
    public string GroupOperationOr => this[nameof (GroupOperationOr)];

    [ResourceEntry("RemoveToolTip", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Remove Item")]
    public string RemoveToolTip => this[nameof (RemoveToolTip)];

    [ResourceEntry("PreviewProviderBetweenText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Between")]
    public string PreviewProviderBetweenText => this[nameof (PreviewProviderBetweenText)];

    [ResourceEntry("PreviewProviderContainsText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Contains")]
    public string PreviewProviderContainsText => this[nameof (PreviewProviderContainsText)];

    [ResourceEntry("PreviewProviderDoesNotContainText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Does Not Contain")]
    public string PreviewProviderDoesNotContainText => this[nameof (PreviewProviderDoesNotContainText)];

    [ResourceEntry("PreviewProviderEndsWithText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Ends With")]
    public string PreviewProviderEndsWithText => this[nameof (PreviewProviderEndsWithText)];

    [ResourceEntry("PreviewProviderEqualToText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "=")]
    public string PreviewProviderEqualToText => this[nameof (PreviewProviderEqualToText)];

    [ResourceEntry("PreviewProviderGreaterThanOrEqualToText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = ">=")]
    public string PreviewProviderGreaterThanOrEqualToText => this[nameof (PreviewProviderGreaterThanOrEqualToText)];

    [ResourceEntry("PreviewProviderGreaterThanText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = ">")]
    public string PreviewProviderGreaterThanText => this[nameof (PreviewProviderGreaterThanText)];

    [ResourceEntry("PreviewProviderIsEmptyText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Is Empty")]
    public string PreviewProviderIsEmptyText => this[nameof (PreviewProviderIsEmptyText)];

    [ResourceEntry("PreviewProviderIsNullText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Is Null")]
    public string PreviewProviderIsNullText => this[nameof (PreviewProviderIsNullText)];

    [ResourceEntry("PreviewProviderLessThanOrEqualToText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "<=")]
    public string PreviewProviderLessThanOrEqualToText => this[nameof (PreviewProviderLessThanOrEqualToText)];

    [ResourceEntry("PreviewProviderLessThanText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "<")]
    public string PreviewProviderLessThanText => this[nameof (PreviewProviderLessThanText)];

    [ResourceEntry("PreviewProviderNotBetweenText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "NotBetween")]
    public string PreviewProviderNotBetweenText => this[nameof (PreviewProviderNotBetweenText)];

    [ResourceEntry("PreviewProviderNotEqualToText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "<>")]
    public string PreviewProviderNotEqualToText => this[nameof (PreviewProviderNotEqualToText)];

    [ResourceEntry("PreviewProviderNotIsEmptyText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Is Not Empty")]
    public string PreviewProviderNotIsEmptyText => this[nameof (PreviewProviderNotIsEmptyText)];

    [ResourceEntry("PreviewProviderNotIsNullText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Is Not Null")]
    public string PreviewProviderNotIsNullText => this[nameof (PreviewProviderNotIsNullText)];

    [ResourceEntry("PreviewProviderStartsWithText", Description = "RadFilterMain resource strings.", LastModified = "2014/06/19", Value = "Starts With")]
    public string PreviewProviderStartsWithText => this[nameof (PreviewProviderStartsWithText)];
  }
}
