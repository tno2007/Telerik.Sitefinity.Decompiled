// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.FieldListView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Lists property values</summary>
  public class FieldListView : Repeater, IHasTextMode
  {
    private Regex templateExpression = new Regex("\\{[^\\{\\}]+\\}", RegexOptions.Compiled);
    private bool errorOccurred;
    private object dataItem;
    private string formattedFields;
    private string formattedText;
    private string[] propertyNames = new string[0];
    private List<string> propertyValues = new List<string>();
    private FieldListView.RenderMode renderMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.FieldListView" /> class.
    /// </summary>
    public FieldListView() => this.Separator = ", ";

    /// <inheritdoc />
    public TextMode TextMode { get; set; }

    [Browsable(false)]
    internal string FormattedText
    {
      get => this.formattedText;
      set => this.formattedText = this.ProcessText(value);
    }

    /// <summary>
    /// Text that wraps the properties. Should include a placeholder ({0}) for the properties.
    /// </summary>
    public string Text { get; set; }

    /// <summary>Text that is a mix of properties and values</summary>
    /// <example>Hello, Mr. {FirstName}! How was your day?</example>
    public string Format { get; set; }

    /// <summary>
    /// Comma-separated list of properties whose values are needed
    /// </summary>
    public string Properties { get; set; }

    /// <summary>Separator</summary>
    [DefaultValue(typeof (string), ", ")]
    public string Separator { get; set; }

    /// <summary>
    /// If not in data-bound context, this property would be the pageId of the object
    /// </summary>
    public string Identity { get; set; }

    /// <summary>
    /// If not in data bound context, this would be the type of the data-bound object
    /// </summary>
    public new string ItemType { get; set; }

    /// <summary>
    /// If not in data-bound context, this would be the name of the provider to use
    /// </summary>
    public string ProviderName { get; set; }

    /// <summary>Optional tag name to insert around the fields list</summary>
    public string WrapperTagName { get; set; }

    /// <summary>
    /// Optional css class name to put to the wrapper tag name. Used only if WrapperTagName is set.
    /// </summary>
    public string WrapperTagCssClass { get; set; }

    /// <summary>
    /// Optional editable field type. Used only for inline editing.
    /// </summary>
    public string EditableFieldType { get; set; }

    /// <summary>Raises the DataBinding event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnDataBinding(EventArgs e)
    {
      base.OnDataBinding(e);
      this.renderMode = FieldListView.RenderMode.Unspecified;
      this.dataItem = this.GetDataItem();
      if (string.IsNullOrEmpty(this.Text))
        this.Text = "{0}";
      if (this.dataItem == null || this.Text.IndexOf("{0}") == -1)
        return;
      if (!string.IsNullOrEmpty(this.Properties))
      {
        this.formattedFields = this.ReplaceProperties(this.dataItem);
        this.renderMode = FieldListView.RenderMode.CombinedProperties;
        if (string.IsNullOrEmpty(this.formattedFields) || this.propertyNames.Length == 0 || this.propertyValues.Count == 0)
          return;
        this.FormattedText = string.Format(this.Text, (object) this.formattedFields);
      }
      else
      {
        if (string.IsNullOrEmpty(this.Format))
          return;
        string formattedText = this.GetFormattedText();
        this.renderMode = FieldListView.RenderMode.FormattedText;
        if (string.IsNullOrEmpty(formattedText))
          return;
        this.FormattedText = string.Format(this.Text, (object) formattedText);
      }
    }

    /// <summary>
    /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.
    /// </summary>
    /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      bool flag1 = this.propertyNames.Length != 0 && (uint) this.propertyValues.Count > 0U;
      bool flag2 = !string.IsNullOrEmpty(this.formattedFields);
      bool flag3 = this.renderMode == FieldListView.RenderMode.CombinedProperties && (!flag1 || !flag2);
      bool flag4 = this.renderMode == FieldListView.RenderMode.FormattedText && this.errorOccurred;
      int num = (this.renderMode == FieldListView.RenderMode.Unspecified || flag3 ? 0 : (!flag4 ? 1 : 0)) & (!this.WrapperTagName.IsNullOrWhitespace() ? 1 : 0);
      if (num != 0)
      {
        if (!string.IsNullOrEmpty(this.WrapperTagCssClass))
          writer.AddAttribute(HtmlTextWriterAttribute.Class, this.WrapperTagCssClass);
        if (!string.IsNullOrEmpty(this.EditableFieldType))
        {
          writer.AddAttribute("data-sf-field", this.Properties);
          writer.AddAttribute("data-sf-ftype", this.EditableFieldType);
        }
        writer.RenderBeginTag(this.WrapperTagName);
      }
      if (!string.IsNullOrEmpty(this.FormattedText))
        writer.Write(this.FormattedText);
      else
        base.Render(writer);
      if (num == 0)
        return;
      writer.RenderEndTag();
    }

    /// <summary>
    /// Helper method for getting properties in the for of [propname, propnam2] and transforming
    /// to [propvalue, propvalue2] -&gt; using the custom separator
    /// </summary>
    /// <param name="dataItem">Data item to get property values from</param>
    /// <returns>Formatted string</returns>
    protected virtual string ReplaceProperties(object dataItem)
    {
      if (string.IsNullOrEmpty(this.Properties))
        return string.Empty;
      if (!string.IsNullOrEmpty(this.formattedFields))
        return this.formattedFields;
      this.propertyNames = this.Properties.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      this.propertyValues = new List<string>();
      foreach (string propertyName in this.propertyNames)
      {
        string name = propertyName.Trim();
        PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(dataItem).Find(name, true);
        if (propertyDescriptor != null)
        {
          object obj1 = propertyDescriptor.GetValue(dataItem);
          object obj2 = propertyDescriptor.PropertyType.IsValueType ? Activator.CreateInstance(propertyDescriptor.PropertyType) : (object) null;
          if (obj1 != null && !obj1.Equals(obj2))
          {
            if (propertyDescriptor.PropertyType == typeof (string) && !((string) obj1).IsNullOrWhitespace())
            {
              this.propertyValues.Add((string) obj1);
            }
            else
            {
              string empty = string.Empty;
              TypeConverter converter = TypeDescriptor.GetConverter(propertyDescriptor.PropertyType);
              if (converter != null && converter.CanConvertTo(typeof (string)))
                empty = converter.ConvertToString(obj1);
              else if (obj1 != obj2)
                empty = obj1.ToString();
              if (!empty.IsNullOrWhitespace())
                this.propertyValues.Add(empty);
            }
          }
        }
      }
      return string.Join(this.Separator, this.propertyValues.ToArray());
    }

    protected virtual object GetDataItem()
    {
      IDataItemContainer dataItemContainer = this.GetDataItemContainer();
      if (dataItemContainer != null && dataItemContainer.DataItem != null)
        return dataItemContainer.DataItem;
      if (string.IsNullOrEmpty(this.Identity) || string.IsNullOrEmpty(this.ItemType))
        return (object) null;
      Type itemType = TypeResolutionService.ResolveType(this.ItemType, false);
      if (itemType == (Type) null)
        return (object) null;
      IManager mappedManager = ManagerBase.GetMappedManager(itemType, this.ProviderName);
      if (mappedManager == null)
        return (object) null;
      if (!Utility.IsGuid(this.Identity))
        return (object) null;
      Guid id = new Guid(this.Identity);
      return mappedManager.GetItem(itemType, id);
    }

    /// <summary>
    /// Process <see cref="P:Telerik.Sitefinity.Web.UI.FieldListView.Format" />
    /// </summary>
    /// <returns>result of procession</returns>
    protected virtual string GetFormattedText()
    {
      if (string.IsNullOrEmpty(this.Format))
        return (string) null;
      string str = this.templateExpression.Replace(this.Format, new MatchEvaluator(this.ReplacePropertyValues));
      return !this.errorOccurred ? str : (string) null;
    }

    /// <summary>Replaces property name placeholders</summary>
    /// <param name="current">current match</param>
    /// <returns>replacement</returns>
    protected virtual string ReplacePropertyValues(Match current)
    {
      if (this.dataItem == null)
        this.dataItem = this.GetDataItem();
      string name = current.Value.Trim("{}".ToCharArray());
      string format = string.Empty;
      int num = name.IndexOf(':');
      if (num != -1)
      {
        format = name.Substring(num).TrimStart(':');
        name = name.Substring(0, num);
      }
      PropertyDescriptor propertyDescriptor = !name.TrimEnd().EndsWith(".ToLocal()") ? TypeDescriptor.GetProperties(this.dataItem).Find(name, true) : TypeDescriptor.GetProperties(this.dataItem).Find(name.TrimEnd().Replace(".ToLocal()", ""), true);
      if (propertyDescriptor == null)
        return current.Value;
      object sitefinityUiTime = propertyDescriptor.GetValue(this.dataItem);
      object obj = propertyDescriptor.PropertyType.IsValueType ? Activator.CreateInstance(propertyDescriptor.PropertyType) : (object) null;
      if (name.TrimEnd().EndsWith(".ToLocal()") && sitefinityUiTime != null)
        sitefinityUiTime = (object) ((DateTime) sitefinityUiTime).ToSitefinityUITime();
      if (sitefinityUiTime == null || sitefinityUiTime.Equals(obj))
        return string.Empty;
      return sitefinityUiTime is IFormattable ? ((IFormattable) sitefinityUiTime).ToString(format, (IFormatProvider) SystemManager.CurrentContext.Culture) : sitefinityUiTime.ToString();
    }

    private enum RenderMode
    {
      Unspecified,
      FormattedText,
      CombinedProperties,
    }
  }
}
