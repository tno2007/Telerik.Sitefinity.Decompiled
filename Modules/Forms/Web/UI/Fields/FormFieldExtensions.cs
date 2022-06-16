// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormFieldExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  public static class FormFieldExtensions
  {
    /// <summary>
    /// This is an extension method that will load default values to MetaField, based on the attribute, from the configuration mapping.
    /// </summary>
    /// <param name="metaField">The meta field.</param>
    /// <param name="configurationKey">The configuration key.</param>
    public static void LoadDefaults(
      this IFormFieldControl formFieldControl,
      string configurationKey)
    {
      if (formFieldControl.MetaField != null)
        return;
      formFieldControl.MetaField = (IMetaField) FormFieldExtensions.LoadMetaFieldFromConfiguration(configurationKey);
    }

    /// <summary>
    /// This is an extension method that will load default values to MetaField, based on the attribute, from the configuration mapping.
    /// </summary>
    /// <param name="metaField">The meta field.</param>
    public static void LoadDefaults(this IFormFieldControl formFieldControl)
    {
      if (!(TypeDescriptor.GetAttributes((object) formFieldControl)[typeof (DatabaseMappingAttribute)] is DatabaseMappingAttribute attribute))
        return;
      if (attribute.DbType.HasValue)
        throw new NotImplementedException("Mapping directly to DbType not implemented.");
      formFieldControl.LoadDefaults(attribute.DatabaseMappingKey);
    }

    /// <summary>
    /// This is an extension method that will load default values to MetaField, based on the attribute, from the configuration mapping.
    /// </summary>
    /// <param name="metaField">The meta field.</param>
    public static MetaFieldProxy LoadDefaultMetaField(
      this IFormFieldControl formFieldControl)
    {
      if (!(TypeDescriptor.GetAttributes((object) formFieldControl)[typeof (DatabaseMappingAttribute)] is DatabaseMappingAttribute attribute))
        return (MetaFieldProxy) null;
      if (attribute.DbType.HasValue)
        throw new NotImplementedException("Mapping directly to DbType not implemented.");
      return FormFieldExtensions.LoadMetaFieldFromConfiguration(attribute.DatabaseMappingKey);
    }

    /// <summary>Adds the specified CSS class to the control</summary>
    /// <param name="control">web control</param>
    /// <param name="cssClass">CSS class name</param>
    public static void AddCssClass(this WebControl control, string cssClass)
    {
      if (string.IsNullOrEmpty(control.CssClass))
      {
        control.CssClass = cssClass;
      }
      else
      {
        WebControl webControl = control;
        webControl.CssClass = webControl.CssClass + " " + cssClass;
      }
    }

    /// <summary>Sorts the ChoiceField items in alphabetical order</summary>
    /// <param name="choiceField"></param>
    public static void SortChoiceItemsAlphabetically(this ChoiceField choiceField)
    {
      List<ChoiceItem> list = choiceField.Choices.OrderBy<ChoiceItem, string>((Func<ChoiceItem, string>) (c => c.Text)).ToList<ChoiceItem>();
      choiceField.Choices.Clear();
      Action<ChoiceItem> action = (Action<ChoiceItem>) (c => choiceField.Choices.Add(c));
      list.ForEach(action);
    }

    private static MetaFieldProxy LoadMetaFieldFromConfiguration(
      string configurationKey)
    {
      DatabaseMappingsElement mapping = (DatabaseMappingsElement) null;
      Config.Get<MetadataConfig>().DatabaseMappings.TryGetValue(configurationKey, out mapping);
      return mapping != null ? mapping.ConvertToMetaField() : throw new ArgumentOutOfRangeException("No database mapping found for the specified key: " + configurationKey);
    }

    private static MetaFieldProxy ConvertToMetaField(
      this DatabaseMappingsElement mapping)
    {
      MetaFieldProxy metaField = new MetaFieldProxy();
      metaField.ClrType = mapping.ClrType;
      metaField.DBLength = mapping.DbLength;
      metaField.DBScale = mapping.DbScale;
      metaField.DBSqlType = mapping.DbSqlType;
      metaField.DBType = mapping.DbType;
      metaField.Required = !mapping.Nullable;
      return metaField;
    }
  }
}
