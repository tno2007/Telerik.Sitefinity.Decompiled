// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.UI.FieldWizardDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.ModuleEditor.Configuration;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.ModuleEditor.Web.UI
{
  /// <summary>The dialog that is used to add meta fields.</summary>
  public class FieldWizardDialog : AjaxDialogBase
  {
    private ICollection<DatabaseMappingsElement> databaseMappings;
    private ICollection<FieldTypeElement> customFieldTypes;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleEditor.FieldWizardDialog.ascx");
    internal const string fieldWizardDialogScript = "Telerik.Sitefinity.ModuleEditor.Web.Scripts.FieldWizardDialog.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ModuleEditor.Web.UI.FieldWizardDialog" /> class.
    /// </summary>
    public FieldWizardDialog() => this.LayoutTemplatePath = FieldWizardDialog.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (FieldWizardDialog).FullName;

    private ICollection<DatabaseMappingsElement> DatabaseMappings
    {
      get
      {
        if (this.databaseMappings == null)
          this.databaseMappings = Config.Get<MetadataConfig>().DatabaseMappings.Values;
        return this.databaseMappings;
      }
    }

    private ICollection<FieldTypeElement> CustomFieldTypes
    {
      get
      {
        if (this.customFieldTypes == null)
          this.customFieldTypes = Config.Get<CustomFieldsConfig>().FieldTypes.Values;
        return this.customFieldTypes;
      }
    }

    protected bool AllowContentLinks { get; set; }

    /// <summary>Gets the reference to the field types control.</summary>
    public ChoiceField FieldTypes => this.Container.GetControl<ChoiceField>("fieldTypes", true);

    /// <summary>Gets the reference to the field name control.</summary>
    public TextField FieldName => this.Container.GetControl<TextField>("fieldName", true);

    /// <summary>Gets the reference to the hidden field control.</summary>
    public ChoiceField HiddenField => this.Container.GetControl<ChoiceField>("hiddenField", true);

    /// <summary>
    /// Gets the reference to the multiple choice field control.
    /// </summary>
    public ChoiceField MultipleChoiceField => this.Container.GetControl<ChoiceField>("multipleChoiceField", true);

    /// <summary>Gets the interface widgets.</summary>
    /// <value>The interface widgets.</value>
    public ChoiceField InterfaceWidgets => this.Container.GetControl<ChoiceField>("interfaceWidgets", true);

    /// <summary>Gets the reference to the continue link.</summary>
    public LinkButton ContinueLink => this.Container.GetControl<LinkButton>("continueLink", true);

    /// <summary>Gets the reference to the cancel link.</summary>
    public LinkButton CancelLink => this.Container.GetControl<LinkButton>("cancelLink", true);

    /// <summary>Represents the container for more options elements</summary>
    protected virtual HtmlGenericControl MoreOptionsSection => this.Container.GetControl<HtmlGenericControl>("moreOptionsSection", true);

    /// <summary>
    /// Represents the button that expands/collapses the more options group
    /// </summary>
    protected virtual HtmlAnchor MoreOptionsExpander => this.Container.GetControl<HtmlAnchor>("moreOptionsExpander", true);

    /// <summary>Gets the reference to the decimalPlacesField control.</summary>
    public ChoiceField DecimalPlacesField => this.Container.GetControl<ChoiceField>("decimalPlacesField", true);

    /// <summary>Gets the taxonomy selector.</summary>
    public RadComboBox TaxonomySelector => this.Container.GetControl<RadComboBox>("taxonomySelector", true);

    /// <summary>Gets the taxonomy selector binder.</summary>
    public RadComboBinder TaxonomySelectorBinder => this.Container.GetControl<RadComboBinder>("taxonomySelectorBinder", true);

    /// <summary>
    /// Gets the reference to the control wrapping taxonomy selector.
    /// </summary>
    public HtmlGenericControl ClassificationsWrapper => this.Container.GetControl<HtmlGenericControl>("classificationsWrapper", true);

    /// <summary>Gets the reference to the DB types control.</summary>
    public ChoiceField DbTypes => this.Container.GetControl<ChoiceField>("dbTypes", true);

    /// <summary>Gets the db length field.</summary>
    public TextField DbLengthField => this.Container.GetControl<TextField>("dbLengthField", true);

    /// <summary>Gets the db precision field.</summary>
    public TextField DbPrecisionField => this.Container.GetControl<TextField>("dbPrecisionField", true);

    /// <summary>Gets the db scale field.</summary>
    public TextField DbScaleField => this.Container.GetControl<TextField>("dbScaleField", true);

    /// <summary>Gets the empty values field.</summary>
    public ChoiceField EmptyValuesField => this.Container.GetControl<ChoiceField>("emptyValuesField", true);

    /// <summary>Gets the include in indexes field.</summary>
    public ChoiceField IncludeInIndexesField => this.Container.GetControl<ChoiceField>("includeInIndexesField", true);

    /// <summary>Gets the column name field.</summary>
    public TextField ColumnNameField => this.Container.GetControl<TextField>("columnNameField", true);

    /// <summary>Gets the client label manager.</summary>
    public ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the custom widget field.</summary>
    /// <value>The custom widget field.</value>
    public TextField CustomWidgetField => this.Container.GetControl<TextField>("customWidgetField", true);

    private System.Web.UI.WebControls.HiddenField MultisiteVal => this.Container.GetControl<System.Web.UI.WebControls.HiddenField>("multisiteVal", true);

    private System.Web.UI.WebControls.HiddenField DbType => this.Container.GetControl<System.Web.UI.WebControls.HiddenField>("dbtype", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      string str = this.Page.Request.QueryString.Get("AllowContentLinks");
      this.AllowContentLinks = !string.IsNullOrEmpty(str) && str.Equals("true", StringComparison.InvariantCultureIgnoreCase);
      this.MultisiteVal.Value = true.ToString();
      this.DbType.Value = ((IOpenAccessDataProvider) ModuleBuilderManager.GetManager().Provider).GetContext().OpenAccessConnection.DbType.ToString();
      this.BindFieldTypes();
      this.BindDbTypes();
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor descriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      descriptor.AddComponentProperty("fieldTypes", this.FieldTypes.ClientID);
      descriptor.AddComponentProperty("fieldName", this.FieldName.ClientID);
      descriptor.AddComponentProperty("hiddenField", this.HiddenField.ClientID);
      descriptor.AddComponentProperty("multipleChoiceField", this.MultipleChoiceField.ClientID);
      descriptor.AddComponentProperty("dbTypes", this.DbTypes.ClientID);
      descriptor.AddComponentProperty("dbLengthField", this.DbLengthField.ClientID);
      descriptor.AddComponentProperty("dbPrecisionField", this.DbPrecisionField.ClientID);
      descriptor.AddComponentProperty("dbScaleField", this.DbScaleField.ClientID);
      descriptor.AddComponentProperty("emptyValuesField", this.EmptyValuesField.ClientID);
      descriptor.AddComponentProperty("includeInIndexesField", this.IncludeInIndexesField.ClientID);
      descriptor.AddComponentProperty("columnNameField", this.ColumnNameField.ClientID);
      descriptor.AddComponentProperty("decimalPlacesField", this.DecimalPlacesField.ClientID);
      descriptor.AddComponentProperty("taxonomySelector", this.TaxonomySelector.ClientID);
      descriptor.AddComponentProperty("taxonomySelectorBinder", this.TaxonomySelectorBinder.ClientID);
      descriptor.AddComponentProperty("interfaceWidgets", this.InterfaceWidgets.ClientID);
      descriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      descriptor.AddComponentProperty("customWidgetField", this.CustomWidgetField.ClientID);
      descriptor.AddElementProperty("continueLink", this.ContinueLink.ClientID);
      descriptor.AddElementProperty("cancelLink", this.CancelLink.ClientID);
      descriptor.AddElementProperty("moreOptionsSection", this.MoreOptionsSection.ClientID);
      descriptor.AddElementProperty("moreOptionsExpander", this.MoreOptionsExpander.ClientID);
      descriptor.AddElementProperty("classificationsWrapper", this.ClassificationsWrapper.ClientID);
      descriptor.AddProperty("_siteBaseUrl", (object) (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') + "/"));
      descriptor.AddProperty("_forbiddenFieldNames", (object) CustomFieldsContext.ReservedFieldNames);
      this.SerializeObject((object) this.DatabaseMappings.ToDictionary<DatabaseMappingsElement, string, DatabaseMappingsElement>((Func<DatabaseMappingsElement, string>) (dm => dm.Name), (Func<DatabaseMappingsElement, DatabaseMappingsElement>) (dm => dm)), descriptor, "databaseMappings");
      this.SerializeObject((object) this.CustomFieldTypes.ToDictionary<FieldTypeElement, string, ConfigElementList<FieldControlElement>>((Func<FieldTypeElement, string>) (ft => ft.Name), (Func<FieldTypeElement, ConfigElementList<FieldControlElement>>) (ft => ft.Controls)), descriptor, "customFieldTypes");
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        descriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.ModuleEditor.Web.Scripts.FieldWizardDialog.js", typeof (FieldWizardDialog).Assembly.FullName)
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void BindFieldTypes()
    {
      this.FieldTypes.Choices.Clear();
      List<FieldTypeElement> list = this.CustomFieldTypes.ToList<FieldTypeElement>();
      string name = HttpContext.Current.Request.ParamsGet("componentType");
      Type c = TypeResolutionService.ResolveType(name, false);
      if (c != (Type) null && typeof (UserProfile).IsAssignableFrom(c))
        list = this.CustomFieldTypes.Where<FieldTypeElement>((Func<FieldTypeElement, bool>) (cf => cf.Title != "RelatedData" && cf.Title != "RelatedMedia")).ToList<FieldTypeElement>();
      foreach (FieldTypeElement fieldTypeElement in list)
      {
        if ((this.AllowContentLinks || !(fieldTypeElement.Name == "Image")) && !(fieldTypeElement.Name == "MultipleChoice") && !(fieldTypeElement.Name == "Currency"))
        {
          string title = fieldTypeElement.Title;
          if (!string.IsNullOrEmpty(fieldTypeElement.ResourceClassId))
            title = Res.Get(fieldTypeElement.ResourceClassId, title);
          this.FieldTypes.Choices.Add(new ChoiceItem()
          {
            Text = title,
            Value = fieldTypeElement.Name
          });
        }
      }
      if (name != typeof (PageNode).FullName)
        this.FieldTypes.Choices.Add(new ChoiceItem()
        {
          Text = "Search engine optimization",
          Value = "Seo"
        });
      this.FieldTypes.Choices.Add(new ChoiceItem()
      {
        Text = "Social media (OpenGraph)",
        Value = "OpenGraph"
      });
    }

    private void BindDbTypes()
    {
      this.DbTypes.Choices.Clear();
      foreach (DatabaseMappingsElement databaseMapping in (IEnumerable<DatabaseMappingsElement>) this.DatabaseMappings)
        this.DbTypes.Choices.Add(new ChoiceItem()
        {
          Text = databaseMapping.DbType,
          Value = databaseMapping.Name
        });
    }

    private void SerializeObject(
      object graph,
      ScriptControlDescriptor descriptor,
      string propertyName)
    {
      Type type = graph.GetType();
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(type, (IEnumerable<Type>) new Type[1]
        {
          type
        }).WriteObject((Stream) memoryStream, graph);
        descriptor.AddProperty(propertyName, (object) Encoding.Default.GetString(memoryStream.ToArray()));
      }
    }
  }
}
