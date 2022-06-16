// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Proxy.DynamicModuleFieldProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Model.Proxy;

namespace Telerik.Sitefinity.DynamicModules.Builder.Proxy
{
  internal class DynamicModuleFieldProxy : IDynamicModuleField, ISecuredObject, IDynamicSecuredObject
  {
    private readonly SecuredProxy securedProxy;
    private string permissionSetName;

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Those are better to be in one place.")]
    public DynamicModuleFieldProxy(DynamicModuleTypeProxy type, DynamicModuleField source)
    {
      this.Type = type;
      this.Id = source.Id;
      this.ModuleName = source.ModuleName;
      this.Name = source.Name;
      this.AddressFieldMode = source.AddressFieldMode;
      this.AllowImageLibrary = source.AllowImageLibrary;
      this.AllowMultipleFiles = source.AllowMultipleFiles;
      this.AllowMultipleImages = source.AllowMultipleImages;
      this.AllowMultipleVideos = source.AllowMultipleVideos;
      this.AllowNulls = source.AllowNulls;
      this.CanCreateItemsWhileSelecting = source.CanCreateItemsWhileSelecting;
      this.CanSelectMultipleItems = source.CanSelectMultipleItems;
      this.CheckedByDefault = source.CheckedByDefault;
      this.Choices = source.Choices;
      this.ClassificationId = source.ClassificationId;
      this.ColumnName = source.ColumnName;
      this.DBLength = source.DBLength;
      this.DBType = source.DBType;
      this.DdlChoiceDefaultValueIndex = source.DdlChoiceDefaultValueIndex;
      this.DecimalPlacesCount = source.DecimalPlacesCount;
      this.DefaultValue = source.DefaultValue;
      this.DisableLinkParser = source.DisableLinkParser;
      this.FieldNamespace = source.FieldNamespace;
      this.FieldStatus = source.FieldStatus;
      this.FieldType = source.FieldType;
      this.FieldTypeDisplayName = source.FieldTypeDisplayName;
      this.FileExtensions = source.FileExtensions;
      this.FileMaxSize = source.FileMaxSize;
      this.FrontendWidgetLabel = source.FrontendWidgetLabel;
      this.FrontendWidgetTypeName = source.FrontendWidgetTypeName;
      this.GridColumnOrdinal = source.GridColumnOrdinal;
      this.ImageExtensions = source.ImageExtensions;
      this.ImageMaxSize = source.ImageMaxSize;
      this.IncludeInIndexes = source.IncludeInIndexes;
      this.InstructionalChoice = source.InstructionalChoice;
      this.InstructionalText = source.InstructionalText;
      this.IsHiddenField = source.IsHiddenField;
      this.IsLocalizable = source.IsLocalizable;
      this.IsRequired = source.IsRequired;
      this.IsRequiredToSelectCheckbox = source.IsRequiredToSelectCheckbox;
      this.IsRequiredToSelectDdlValue = source.IsRequiredToSelectDdlValue;
      this.IsTransient = source.IsTransient;
      this.LastModified = source.LastModified;
      this.LengthValidationMessage = source.LengthValidationMessage;
      this.MaxLength = source.MaxLength;
      this.MaxNumberRange = source.MaxNumberRange;
      this.MediaType = source.MediaType;
      this.MinLength = source.MinLength;
      this.MinNumberRange = source.MinNumberRange;
      this.NumberUnit = source.NumberUnit;
      this.Ordinal = source.Ordinal;
      this.Owner = source.Owner;
      this.ParentSectionId = source.ParentSectionId;
      this.ParentTypeId = source.ParentTypeId;
      this.RegularExpression = source.RegularExpression;
      this.RelatedDataProvider = source.RelatedDataProvider;
      this.RelatedDataType = source.RelatedDataType;
      this.ShowInGrid = source.ShowInGrid;
      this.SpecialType = source.SpecialType;
      this.Title = source.Title;
      this.TypeUIName = source.TypeUIName;
      this.VideoExtensions = source.VideoExtensions;
      this.VideoMaxSize = source.VideoMaxSize;
      this.WidgetTypeName = source.WidgetTypeName;
      this.permissionSetName = source.GetPermissionSetName();
      this.securedProxy = new SecuredProxy((ISecuredObject) source);
      this.ChoiceRenderType = source.ChoiceRenderType;
      this.RecommendedCharactersCount = source.RecommendedCharactersCount;
    }

    public DynamicModuleTypeProxy Type { get; private set; }

    public AddressFieldMode AddressFieldMode { get; private set; }

    public bool AllowImageLibrary { get; private set; }

    public bool AllowMultipleFiles { get; private set; }

    public bool AllowMultipleImages { get; private set; }

    public bool AllowMultipleVideos { get; private set; }

    public bool AllowNulls { get; private set; }

    public bool CanCreateItemsWhileSelecting { get; private set; }

    public bool CanSelectMultipleItems { get; private set; }

    public bool CheckedByDefault { get; private set; }

    public string ChoiceRenderType { get; set; }

    public string Choices { get; private set; }

    public Guid ClassificationId { get; private set; }

    public string ColumnName { get; private set; }

    public string DBLength { get; private set; }

    public string DBType { get; private set; }

    public int DdlChoiceDefaultValueIndex { get; private set; }

    public int DecimalPlacesCount { get; private set; }

    public string DefaultValue { get; private set; }

    public bool DisableLinkParser { get; private set; }

    public string FieldNamespace { get; private set; }

    public DynamicModuleFieldStatus FieldStatus { get; private set; }

    public FieldType FieldType { get; private set; }

    public string FieldTypeDisplayName { get; private set; }

    public string FileExtensions { get; private set; }

    public int FileMaxSize { get; private set; }

    public string FrontendWidgetLabel { get; private set; }

    public string FrontendWidgetTypeName { get; private set; }

    public int GridColumnOrdinal { get; private set; }

    public Guid Id { get; private set; }

    public string ImageExtensions { get; private set; }

    public int ImageMaxSize { get; private set; }

    public bool IncludeInIndexes { get; private set; }

    public string InstructionalChoice { get; private set; }

    public string InstructionalText { get; private set; }

    public bool IsHiddenField { get; private set; }

    public bool IsLocalizable { get; private set; }

    public bool IsRequired { get; private set; }

    public bool IsRequiredToSelectCheckbox { get; private set; }

    public bool IsRequiredToSelectDdlValue { get; private set; }

    public bool IsTransient { get; private set; }

    public DateTime LastModified { get; private set; }

    public string LengthValidationMessage { get; private set; }

    public int MaxLength { get; private set; }

    public string MaxNumberRange { get; private set; }

    public string MediaType { get; private set; }

    public int MinLength { get; private set; }

    public string MinNumberRange { get; private set; }

    public string ModuleName { get; private set; }

    public string Name { get; private set; }

    public string NumberUnit { get; private set; }

    public int Ordinal { get; private set; }

    public Guid Owner { get; private set; }

    public Guid ParentSectionId { get; private set; }

    public Guid ParentTypeId { get; private set; }

    public string RegularExpression { get; private set; }

    public string RelatedDataProvider { get; private set; }

    public string RelatedDataType { get; private set; }

    public bool ShowInGrid { get; private set; }

    public FieldSpecialType SpecialType { get; private set; }

    public string Title { get; private set; }

    public string TypeUIName { get; private set; }

    public string VideoExtensions { get; private set; }

    public int VideoMaxSize { get; private set; }

    public string WidgetTypeName { get; private set; }

    public int? RecommendedCharactersCount { get; private set; }

    public string GetPermissionSetName() => this.permissionSetName;

    public bool InheritsPermissions
    {
      get => this.securedProxy.InheritsPermissions;
      set => this.securedProxy.InheritsPermissions = value;
    }

    public bool CanInheritPermissions
    {
      get => this.securedProxy.CanInheritPermissions;
      set => this.securedProxy.CanInheritPermissions = value;
    }

    public IList<Permission> Permissions => this.securedProxy.Permissions;

    public string[] SupportedPermissionSets
    {
      get => this.securedProxy.SupportedPermissionSets;
      set => this.securedProxy.SupportedPermissionSets = value;
    }

    public IDictionary<string, string> PermissionsetObjectTitleResKeys
    {
      get => this.securedProxy.PermissionsetObjectTitleResKeys;
      set => this.securedProxy.PermissionsetObjectTitleResKeys = value;
    }

    string IDynamicSecuredObject.GetSecurityActionTitle(string actionTitle) => string.Format(actionTitle, (object) this.Title.ToLower());

    string IDynamicSecuredObject.GetTitle() => this.Title.ToLower();

    /// <inheritdoc />
    string IDynamicSecuredObject.GetModuleName() => this.ModuleName;
  }
}
