// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.ConnectorDataMappingExtender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>
  /// Base class defines field mapping between Sitefinity and external connectors' fields.
  /// </summary>
  public abstract class ConnectorDataMappingExtender
  {
    /// <summary>
    /// Gets the ordinal number (descending) for this extender in the data mapping dialog.
    /// </summary>
    /// <value>
    /// The ordinal number for this extender in the data mapping dialog.
    /// </value>
    public abstract int Ordinal { get; }

    /// <summary>
    /// Gets the name of the key for the collection of connectors' fields.
    /// </summary>
    /// <value>The fields key.</value>
    public abstract string Key { get; }

    /// <summary>
    /// Gets or sets the dependent controls selector css class.
    /// The dependent controls are used to determine the logic of write mode(on|off) for data mapping selectors for the specified extender.
    /// If the dependent controls do not have values, the fields in the data mappings grid will not be available for edit.
    /// </summary>
    public virtual string DependentControlsCssClass { get; set; }

    /// <summary>
    /// Gets or sets the required controls selector css class.
    /// The required controls are used for constructing the parameters of the auto-complete service for for the specified extender.
    /// </summary>
    public virtual string AutocompleteRequiredControlsCssClass { get; set; }

    /// <summary>
    /// Gets a value indicating whether this instance has auto-complete functionality.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance has auto-complete; otherwise, <c>false</c>.
    /// </value>
    public virtual bool HasAutocomplete => false;

    /// <summary>
    /// Gets a value indicating whether this instance has connectivity issues.
    /// <see cref="P:Telerik.Sitefinity.Modules.Forms.ConnectorDataMappingExtender.ConnectivityIssuesMessage" /> must be specified as well."/&gt;
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance has connectivity issues; otherwise, <c>false</c>.
    /// </value>
    public virtual bool HasConnectivityIssues => false;

    /// <summary>
    /// Gets the auto complete data.
    /// The purpose of this property is to hold the data for the extender before it is requested by autocomplete fields, e.g. on form properties initialization.
    /// Used with <see cref="P:Telerik.Sitefinity.Modules.Forms.ConnectorDataMappingExtender.TrackAndShowWarningsForInvalidFieldsMappings" /> to show warnings when custom values are entered or no longer valid.
    /// </summary>
    /// <value>The automatic complete data.</value>
    public virtual IEnumerable<string> AutoCompleteData => (IEnumerable<string>) null;

    /// <summary>
    /// Gets a value indicating whether to track and show warnings for invalid fields mappings.
    /// For warnings on extender initialization, set also <see cref="P:Telerik.Sitefinity.Modules.Forms.ConnectorDataMappingExtender.AutoCompleteData" />.
    /// <see cref="P:Telerik.Sitefinity.Modules.Forms.ConnectorDataMappingExtender.InvalidFieldValueMessageFormat" /> must be specified as well."/&gt;
    /// </summary>
    /// <value>
    ///   <c>true</c> if enabled; otherwise, <c>false</c>.
    /// </value>
    public virtual bool TrackAndShowWarningsForInvalidFieldsMappings => false;

    /// <summary>
    /// Gets the connectivity issues message.
    /// Shown when <see cref="P:Telerik.Sitefinity.Modules.Forms.ConnectorDataMappingExtender.HasConnectivityIssues" /> is <c>true</c>.
    /// </summary>
    /// <value>The connectivity issues message.</value>
    public virtual string ConnectivityIssuesMessage => string.Empty;

    /// <summary>
    /// Gets the invalid field value message format.
    /// <see cref="P:Telerik.Sitefinity.Modules.Forms.ConnectorDataMappingExtender.TrackAndShowWarningsForInvalidFieldsMappings" /> must be enabled.
    /// </summary>
    /// <value>The invalid field value message format.</value>
    public virtual string InvalidFieldValueMessageFormat => string.Empty;

    /// <summary>
    /// Gets the data mapping field input validation regex.
    /// <see cref="P:Telerik.Sitefinity.Modules.Forms.ConnectorDataMappingExtender.DataMappingFieldValidationErrorMessageFormat" /> must be specified as well."/&gt;
    /// </summary>
    /// <value>The data mapping field input validation regex.</value>
    public virtual string DataMappingFieldInputValidationRegex => string.Empty;

    /// <summary>
    /// Gets the data mapping field validation error message format.
    /// <see cref="P:Telerik.Sitefinity.Modules.Forms.ConnectorDataMappingExtender.DataMappingFieldInputValidationRegex" /> must be set.
    /// </summary>
    /// <value>The data mapping field validation error message format.</value>
    public virtual string DataMappingFieldValidationErrorMessageFormat => string.Empty;

    /// <summary>
    /// Gets the auto-complete data for a specified search string and number of items to return.
    /// </summary>
    /// <param name="term">The search string.</param>
    /// <param name="paramValues">The values for filtering the auto-complete data.
    /// These are the field values for all fields marked with the <see cref="P:Telerik.Sitefinity.Modules.Forms.ConnectorDataMappingExtender.AutocompleteRequiredControlsCssClass" /> class.</param>
    /// <returns>The auto-complete data.</returns>
    public virtual IEnumerable<string> GetAutocompleteData(
      string term,
      params string[] paramValues)
    {
      return Enumerable.Empty<string>();
    }
  }
}
