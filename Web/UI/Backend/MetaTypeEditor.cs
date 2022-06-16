// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.MetaTypeEditor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  public class MetaTypeEditor : SimpleScriptView
  {
    private List<MetaTypeEditor.DynamicFieldTypeDefinition> fieldDefinitions = new List<MetaTypeEditor.DynamicFieldTypeDefinition>()
    {
      new MetaTypeEditor.DynamicFieldTypeDefinition()
      {
        Id = "DynamicFieldTypeDefinitionGuid",
        Title = Res.Get<Labels>().MetaFieldTypeGuid,
        ClrType = typeof (Guid).FullName,
        DBTypes = new List<string>() { "GUID" }.ToArray(),
        DefaultDBType = "GUID",
        DBSQLTypes = new List<string>()
        {
          "UNIQUEIDENTIFIER"
        }.ToArray(),
        DefaultDBSQLType = "UNIQUEIDENTIFIER",
        SupportsDefaultValueText = false,
        SupportsDefaultValueBool = false,
        SupportsMaxLength = true,
        SupportsTaxonomyFields = false,
        SupportsPrecision = false,
        DefaultValueRegularExpression = "^(\\{){0,1}[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}(\\}){0,1}$"
      },
      new MetaTypeEditor.DynamicFieldTypeDefinition()
      {
        Id = "DynamicFieldTypeDefinitionText",
        Title = Res.Get<Labels>().MetaFieldTypeString,
        ClrType = typeof (string).FullName,
        DBTypes = new List<string>()
        {
          "VARCHAR",
          "LONGVARCHAR"
        }.ToArray(),
        DefaultDBType = "VARCHAR",
        DBSQLTypes = new List<string>()
        {
          "VARCHAR",
          "NVARCHAR",
          "NVARCHAR(MAX)"
        }.ToArray(),
        DefaultDBSQLType = "NVARCHAR",
        SupportsDefaultValueText = true,
        SupportsDefaultValueBool = false,
        SupportsMaxLength = true,
        SupportsTaxonomyFields = false,
        SupportsPrecision = false,
        DefaultValueRegularExpression = "^.*$"
      },
      new MetaTypeEditor.DynamicFieldTypeDefinition()
      {
        Id = "DynamicFieldTypeDefinitionInteger",
        Title = Res.Get<Labels>().MetaFieldTypeInt,
        ClrType = typeof (int).FullName,
        DBTypes = new List<string>() { "INTEGER" }.ToArray(),
        DefaultDBType = "INTEGER",
        DBSQLTypes = new List<string>() { "INTEGER" }.ToArray(),
        DefaultDBSQLType = "INTEGER",
        SupportsDefaultValueText = true,
        SupportsDefaultValueBool = false,
        SupportsMaxLength = false,
        SupportsTaxonomyFields = false,
        SupportsPrecision = false,
        DefaultValueRegularExpression = "^-{0,1}\\d*$"
      },
      new MetaTypeEditor.DynamicFieldTypeDefinition()
      {
        Id = "DynamicFieldTypeDefinitionDecimal",
        Title = Res.Get<Labels>().MetaFieldTypeDecimal,
        ClrType = typeof (Decimal).FullName,
        DBTypes = new List<string>()
        {
          "NUMERIC",
          "DECIMAL",
          "CLOB",
          "LONGVARCHAR",
          "LONGVARBINARY"
        }.ToArray(),
        DefaultDBType = "DECIMAL",
        DBSQLTypes = new List<string>()
        {
          "NUMERIC",
          "DECIMAL",
          "TEXT",
          "IMAGE"
        }.ToArray(),
        DefaultDBSQLType = "DECIMAL",
        SupportsDefaultValueText = true,
        SupportsDefaultValueBool = false,
        SupportsMaxLength = false,
        SupportsTaxonomyFields = false,
        SupportsPrecision = true,
        DefaultValueRegularExpression = "^(-){0,1}[\\d]+([\\.]\\d+){0,1}$"
      },
      new MetaTypeEditor.DynamicFieldTypeDefinition()
      {
        Id = "DynamicFieldTypeDefinitionDate",
        Title = Res.Get<Labels>().MetaFieldTypeDate,
        ClrType = typeof (DateTime).FullName,
        DBTypes = new List<string>()
        {
          "TIMESTAMP",
          "DATE",
          "TIME"
        }.ToArray(),
        DefaultDBType = "DATE",
        DBSQLTypes = new List<string>() { "DATETIME" }.ToArray(),
        DefaultDBSQLType = "DATETIME",
        SupportsDefaultValueText = false,
        SupportsDefaultValueBool = false,
        SupportsMaxLength = false,
        SupportsTaxonomyFields = false,
        SupportsPrecision = false,
        DefaultValueRegularExpression = ""
      },
      new MetaTypeEditor.DynamicFieldTypeDefinition()
      {
        Id = "DynamicFieldTypeDefinitionYesNo",
        Title = Res.Get<Labels>().MetaFieldTypeBoolean,
        ClrType = typeof (bool).FullName,
        DBTypes = new List<string>() { "BIT", "TINYINT" }.ToArray(),
        DefaultDBType = "BIT",
        DBSQLTypes = new List<string>() { "TINYINT" }.ToArray(),
        DefaultDBSQLType = "TINYINT",
        SupportsDefaultValueText = false,
        SupportsDefaultValueBool = true,
        SupportsMaxLength = false,
        SupportsTaxonomyFields = false,
        SupportsPrecision = false,
        DefaultValueRegularExpression = ""
      }
    };
    private const string ClientJsCodePath = "Telerik.Sitefinity.Web.UI.Backend.Scripts.MetaTypeEditor.js";
    public static readonly string LayoutPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.MetaTypeEditor.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.MetaTypeEditor" /> class.
    /// </summary>
    public MetaTypeEditor() => this.LayoutTemplatePath = MetaTypeEditor.LayoutPath;

    /// <summary>Gets the "Add new data field" button.</summary>
    /// <value>The "Add new data field" button.</value>
    /// k
    private LinkButton AddNewDataFieldButton => this.Container.GetControl<LinkButton>("btnAddNewDataField", true);

    /// <summary>Gets the "Delete data field" button.</summary>
    /// <value>The "Delete data field" button.</value>
    private LinkButton DeleteDataFieldButton => this.Container.GetControl<LinkButton>("btnDeleteDataField", true);

    /// <summary>Gets the fields list.</summary>
    /// <value>The fields list.</value>
    private ListBox FieldsList => this.Container.GetControl<ListBox>("lstFields", true);

    /// <summary>Gets the field type list box.</summary>
    /// <value>The field type list box.</value>
    private RadComboBox FieldTypeListBox => this.Container.GetControl<RadComboBox>("lstFieldType", true);

    /// <summary>Gets the field db type list box.</summary>
    /// <value>The field db type list box.</value>
    private RadComboBox FieldDbTypeListBox => this.Container.GetControl<RadComboBox>("lstDbType", true);

    /// <summary>Gets the field db type list box.</summary>
    /// <value>The field db type list box.</value>
    private RadComboBox FieldSQLDbTypeListBox => this.Container.GetControl<RadComboBox>("lstSQLDbType", true);

    /// <summary>Gets the default value check box.</summary>
    /// <value>The default value check box.</value>
    private CheckBox DefaultValueCheckBox => this.Container.GetControl<CheckBox>("chkDefaultValue", true);

    /// <summary>Gets or sets the taxonomy provider list.</summary>
    /// <value>The taxonomy provider list.</value>
    private RadComboBox TaxonomyProviderList => this.Container.GetControl<RadComboBox>("lstTaxonomyProvider", true);

    /// <summary>Gets the taxonomy list.</summary>
    /// <value>The taxonomy list.</value>
    private RadComboBox TaxonomyList => this.Container.GetControl<RadComboBox>("lstTaxonomySelector", true);

    /// <summary>Gets the "Allow multiple taxons" check box.</summary>
    /// <value>The "Allow multiple taxons" check box.</value>
    private CheckBox AllowMultipleTaxonsCheckBox => this.Container.GetControl<CheckBox>("chkAllowMultipleTaxons", true);

    /// <summary>Gets the precision text box.</summary>
    /// <value>The precision text box.</value>
    private TextField PrecisionTextBox => this.Container.GetControl<TextField>("txtPrecision", true);

    /// <summary>Gets the max length panel.</summary>
    /// <value>The max length panel.</value>
    private Panel MaxLengthPanel => this.Container.GetControl<Panel>("pnlMaxLen", true);

    /// <summary>Gets the default values panel.</summary>
    /// <value>The default values panel.</value>
    private Panel DefaultValuesPanel => this.Container.GetControl<Panel>("pnlDefaultValues", true);

    /// <summary>Gets the precision panel.</summary>
    /// <value>The precision panel.</value>
    private Panel PrecisionPanel => this.Container.GetControl<Panel>("pnlPrecision", true);

    /// <summary>Gets the taxonomies panel.</summary>
    /// <value>The taxonomies panel.</value>
    private Panel TaxonomiesPanel => this.Container.GetControl<Panel>("pnlTaxonomies", true);

    /// <summary>Gets the DB type panel.</summary>
    /// <value>The DB type panel.</value>
    private Panel DBTypePanel => this.Container.GetControl<Panel>("pnlDBType", true);

    /// <summary>Gets the taxonomie providers panel.</summary>
    /// <value>The taxonomie providers panel.</value>
    private Panel TaxonomieProvidersPanel => this.Container.GetControl<Panel>("pnlTaxonomyProvider", true);

    /// <summary>Gets the default text field panel.</summary>
    /// <value>The default text field panel.</value>
    private Panel DefaultTextFieldPanel => this.Container.GetControl<Panel>("pnlDefaultTextField", true);

    /// <summary>Gets the message control - for client notifications.</summary>
    /// <value>The message control.</value>
    private Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the taxonomy selecttor binder.</summary>
    /// <value>The taxonomy selecttor binder.</value>
    private RadComboBinder TaxonomySelectorBinder => this.Container.GetControl<RadComboBinder>("taxonomySelectorBinder", true);

    /// <summary>Gets the field name text box.</summary>
    /// <value>The field name text box.</value>
    private TextField FieldNameTextBox => this.Container.GetControl<TextField>("txtFieldName", true);

    /// <summary>Gets the field title text box.</summary>
    /// <value>The field title text box.</value>
    private TextField FieldTitleTextBox => this.Container.GetControl<TextField>("txtFieldTitle", true);

    /// <summary>Gets the max length text box.</summary>
    /// <value>The max length text box.</value>
    private TextField MaxLengthTextBox => this.Container.GetControl<TextField>("txtMaxLength", true);

    /// <summary>Gets the default value text box.</summary>
    /// <value>The default value text box.</value>
    private TextField DefaultValueTextBox => this.Container.GetControl<TextField>("txtDefaultValue", true);

    private ClientLabelManager ClientLabelManagerControl => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets the Boolean default value panel (with the checkbox in it).
    /// </summary>
    /// <value>The Boolean default value panel (with the checkbox in it).</value>
    private Panel ChkDefaultValuePanel => this.Container.GetControl<Panel>("pnlChkDefaultValue", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.TaxonomyProviderList.DataSource = (object) TaxonomyManager.GetManager().StaticProviders;
      this.TaxonomyProviderList.ItemDataBound += new RadComboBoxItemEventHandler(this.TaxonomyProviderList_ItemDataBound);
      this.TaxonomyProviderList.DataBind();
      this.FieldTypeListBox.DataSource = (object) this.fieldDefinitions;
      this.FieldTypeListBox.ItemDataBound += new RadComboBoxItemEventHandler(this.FieldTypeListBox_ItemDataBound);
      this.FieldTypeListBox.DataBind();
    }

    /// <summary>
    /// Handles the ItemDataBound event of the FieldTypeListBox control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Web.UI.RadComboBoxItemEventArgs" /> instance containing the event data.</param>
    private void FieldTypeListBox_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
    {
      MetaTypeEditor.DynamicFieldTypeDefinition dataItem = (MetaTypeEditor.DynamicFieldTypeDefinition) e.Item.DataItem;
      e.Item.Text = dataItem.Title;
      e.Item.Value = dataItem.ClrType;
    }

    /// <summary>
    /// Handles the ItemDataBound event of the TaxonomyProviderList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Web.UI.RadComboBoxItemEventArgs" /> instance containing the event data.</param>
    private void TaxonomyProviderList_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
    {
      DataProviderBase dataItem = (DataProviderBase) e.Item.DataItem;
      e.Item.Text = dataItem.Title;
      e.Item.Value = dataItem.Name;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor("Telerik.Sitefinity.Web.UI.Backend.MetaTypeEditor", this.ClientID);
      behaviorDescriptor.AddProperty("_addNewDataFieldButtonId", (object) this.AddNewDataFieldButton.ClientID);
      behaviorDescriptor.AddProperty("_deleteDataFieldButtonId", (object) this.DeleteDataFieldButton.ClientID);
      behaviorDescriptor.AddProperty("_fieldsListId", (object) this.FieldsList.ClientID);
      behaviorDescriptor.AddProperty("_fieldTypeListBoxId", (object) this.FieldTypeListBox.ClientID);
      behaviorDescriptor.AddProperty("_fieldDbTypeListBoxId", (object) this.FieldDbTypeListBox.ClientID);
      behaviorDescriptor.AddProperty("_defaultValueCheckBoxId", (object) this.DefaultValueCheckBox.ClientID);
      behaviorDescriptor.AddProperty("_taxonomyProviderListId", (object) this.TaxonomyProviderList.ClientID);
      behaviorDescriptor.AddProperty("_taxonomyListId", (object) this.TaxonomyList.ClientID);
      behaviorDescriptor.AddProperty("_allowMultipleTaxonsCheckBoxId", (object) this.AllowMultipleTaxonsCheckBox.ClientID);
      behaviorDescriptor.AddProperty("_precisionTextBoxId", (object) this.PrecisionTextBox.ClientID);
      behaviorDescriptor.AddProperty("_precisionTextBoxId", (object) this.PrecisionTextBox.ClientID);
      behaviorDescriptor.AddProperty("_fieldDefinitions", (object) new JavaScriptSerializer().Serialize((object) this.fieldDefinitions));
      behaviorDescriptor.AddProperty("_messageControlId", (object) this.MessageControl.ClientID);
      behaviorDescriptor.AddProperty("_taxonomySelectorBinderId", (object) this.TaxonomySelectorBinder.ClientID);
      behaviorDescriptor.AddProperty("_fieldSQLDbTypeListBoxId", (object) this.FieldSQLDbTypeListBox.ClientID);
      behaviorDescriptor.AddProperty("_fieldTitleTextBoxId", (object) this.FieldTitleTextBox.ClientID);
      behaviorDescriptor.AddProperty("_fieldNameTextBoxId", (object) this.FieldNameTextBox.ClientID);
      behaviorDescriptor.AddProperty("_maxLengthTextBoxId", (object) this.MaxLengthTextBox.ClientID);
      behaviorDescriptor.AddProperty("_defaultValueTextBoxId", (object) this.DefaultValueTextBox.ClientID);
      behaviorDescriptor.AddProperty("_clientLabelManagerId", (object) this.ClientLabelManagerControl.ClientID);
      behaviorDescriptor.AddProperty("_maxLengthPanelId", (object) this.MaxLengthPanel.ClientID);
      behaviorDescriptor.AddProperty("_defaultValuesPanelId", (object) this.DefaultValuesPanel.ClientID);
      behaviorDescriptor.AddProperty("_precisionPanelId", (object) this.PrecisionPanel.ClientID);
      behaviorDescriptor.AddProperty("_taxonomiesPanelId", (object) this.TaxonomiesPanel.ClientID);
      behaviorDescriptor.AddProperty("_dBTypePanelId", (object) this.DBTypePanel.ClientID);
      behaviorDescriptor.AddProperty("_taxonomieProvidersPanelId", (object) this.TaxonomieProvidersPanel.ClientID);
      behaviorDescriptor.AddProperty("_defaultTextFieldPanelId", (object) this.DefaultTextFieldPanel.ClientID);
      behaviorDescriptor.AddProperty("_chkDefaultValuePanelId", (object) this.ChkDefaultValuePanel.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Backend.Scripts.MetaTypeEditor.js", typeof (CommentsList).Assembly.GetName().Name)
    };

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// This structure is used for client-side representation of definition of various field types
    /// </summary>
    private struct DynamicFieldTypeDefinition
    {
      /// <summary>Gets or sets the id of the field type.</summary>
      /// <value>The id of the field type.</value>
      public string Id { get; set; }

      /// <summary>Gets or sets the UI title of the field type.</summary>
      /// <value>The UI title of the field type.</value>
      public string Title { get; set; }

      /// <summary>
      /// Gets or sets the CLR type corresponding with this field type.
      /// </summary>
      /// <value>The CLR type corresponding with this field type.</value>
      public string ClrType { get; set; }

      /// <summary>
      /// Gets or sets the available OpenAccess types which match this field type.
      /// </summary>
      /// <value>The available OpenAccess types which match this field type.</value>
      public string[] DBTypes { get; set; }

      /// <summary>
      /// Gets or sets the default OpenAccess type associated with this field type.
      /// </summary>
      /// <value>The default OpenAccess type associated with this field type.</value>
      public string DefaultDBType { get; set; }

      /// <summary>
      /// Gets or sets the available MS-SQL types which match this field type.
      /// </summary>
      /// <value>The available MS-SQL types which match this field type.</value>
      public string[] DBSQLTypes { get; set; }

      /// <summary>
      /// Gets or sets the default MS-SQL type associated with this field type.
      /// </summary>
      /// <value>The default MS-SQL type associated with this field type.</value>
      public string DefaultDBSQLType { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this field supports the a maximum length restriction.
      /// </summary>
      /// <value><c>true</c> if this field supports the a maximum length restriction; otherwise, <c>false</c>.</value>
      public bool SupportsMaxLength { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this field supports decimal precision.
      /// </summary>
      /// <value><c>true</c> if this field supports decimal precision; otherwise, <c>false</c>.</value>
      public bool SupportsPrecision { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this field supports a textual default value.
      /// </summary>
      /// <value>
      /// 	<c>true</c> if this field supports a textual default value; otherwise, <c>false</c>.
      /// </value>
      public bool SupportsDefaultValueText { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this field supports a boolena default value.
      /// </summary>
      /// <value>
      /// 	<c>true</c> if this field supports a boolena default value; otherwise, <c>false</c>.
      /// </value>
      public bool SupportsDefaultValueBool { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this field supports taxonomy fields.
      /// </summary>
      /// <value>
      /// 	<c>true</c> if this field supports taxonomy fields; otherwise, <c>false</c>.
      /// </value>
      public bool SupportsTaxonomyFields { get; set; }

      /// <summary>Gets or sets the default value regular expression.</summary>
      /// <value>The default value regular expression.</value>
      public string DefaultValueRegularExpression { get; set; }
    }
  }
}
