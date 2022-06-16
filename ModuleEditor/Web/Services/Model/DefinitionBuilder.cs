// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.Model.DefinitionBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.ModuleEditor.Web.Services.Model
{
  /// <summary>
  /// Helper class for creating or updating definition elements based on specified field type and definition properties.
  /// </summary>
  public static class DefinitionBuilder
  {
    /// <summary>
    /// Creates or updates the definition element depending on value of input parameter 'fieldDefinitionElement'.
    /// If parameter 'fieldDefinitionElement' is 'null' definition instance is created, otherwise definition element properties are updated.
    /// </summary>
    /// <param name="fieldControlType">Type of the field control.</param>
    /// <param name="definition">The definition.</param>
    /// <param name="parent">The parent configuraiton element.</param>
    /// <param name="resolveElementType">if set to <c>true</c> [resolve element type].</param>
    /// <param name="fieldDefinitionElement">An instance of field definition to be updated.</param>
    /// <returns>An instance of definition element.</returns>
    public static ConfigElement CreateOrUpdateDefinitionElement(
      Type fieldControlType,
      object definition,
      ConfigElement parent,
      bool resolveElementType,
      ConfigElement fieldDefinitionElement)
    {
      if (fieldDefinitionElement == null)
        fieldDefinitionElement = Activator.CreateInstance(resolveElementType ? DefinitionBuilder.GetDefinitionElementType(fieldControlType) : fieldControlType, (object) parent) as ConfigElement;
      return DefinitionBuilder.BuildDefinitionElementProperties(fieldDefinitionElement, definition, parent);
    }

    /// <summary>Builds the properties of definition element</summary>
    /// <param name="fieldDefinitionElement">The field definition element.</param>
    /// <param name="definition">The definition.</param>
    /// <param name="parent">The parent.</param>
    /// <returns></returns>
    public static ConfigElement BuildDefinitionElementProperties(
      ConfigElement fieldDefinitionElement,
      object definition,
      ConfigElement parent)
    {
      fieldDefinitionElement.CopyPropertiesFrom(definition);
      DefinitionBuilder.CopyComplexProperties(fieldDefinitionElement, definition, parent);
      DefinitionBuilder.CopyCollectionProperties(fieldDefinitionElement, definition, parent);
      return fieldDefinitionElement;
    }

    /// <summary>
    /// Tries to get the type of definition element for a specified field type.
    /// </summary>
    /// <param name="fieldControlType">Type of the field.</param>
    /// <param name="defaultDefinitionElementType">Default type to be returned, if definition element type cannot be resolved.</param>
    internal static Type GetDefinitionElementType(
      Type fieldControlType,
      Type defaultDefinitionElementType = null)
    {
      Type type1 = (Type) null;
      if (fieldControlType != (Type) null)
      {
        if (fieldControlType.IsArray)
          fieldControlType = fieldControlType.GetElementType();
        object[] customAttributes = fieldControlType.GetCustomAttributes(typeof (FieldDefinitionElementAttribute), false);
        if (customAttributes != null && customAttributes.Length != 0 && customAttributes[0] is FieldDefinitionElementAttribute elementAttribute)
          type1 = elementAttribute.FieldDefinitionType;
      }
      Type definitionElementType = type1;
      if ((object) definitionElementType != null)
        return definitionElementType;
      Type type2 = defaultDefinitionElementType;
      return (object) type2 != null ? type2 : typeof (DefaultFieldDefinitionElement);
    }

    private static void CopyComplexProperties(
      ConfigElement fieldDefinitionElement,
      object definition,
      ConfigElement parent)
    {
      IEnumerable<PropertyInfo> source = ((IEnumerable<PropertyInfo>) definition.GetType().GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.PropertyType.IsDefinition()));
      if (source.Count<PropertyInfo>() <= 0)
        return;
      parent = fieldDefinitionElement;
      foreach (PropertyInfo propertyInfo in source)
      {
        object definition1 = propertyInfo.GetValue(definition, (object[]) null);
        if (definition1 != null)
        {
          ConfigElement definitionElement = DefinitionBuilder.CreateOrUpdateDefinitionElement(propertyInfo.PropertyType, definition1, parent, true, (ConfigElement) null);
          PropertyInfo configProperty = DefinitionBuilder.GetConfigProperty(fieldDefinitionElement, definitionElement);
          if (configProperty != (PropertyInfo) null)
            configProperty.SetValue((object) fieldDefinitionElement, (object) definitionElement, (object[]) null);
        }
      }
    }

    private static void CopyCollectionProperties(
      ConfigElement fieldDefinitionElement,
      object definition,
      ConfigElement parent)
    {
      IEnumerable<PropertyInfo> source = ((IEnumerable<PropertyInfo>) definition.GetType().GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.PropertyType.IsArrayOfDefinition()));
      if (source.Count<PropertyInfo>() <= 0)
        return;
      parent = fieldDefinitionElement;
      foreach (PropertyInfo propertyInfo in source)
      {
        if (propertyInfo.GetValue(definition, (object[]) null) is ICollection collection && collection.Count > 0)
        {
          string elementPropertyName = DefinitionBuilder.GetConfigElementPropertyName(propertyInfo.PropertyType.GetElementType().Name);
          PropertyInfo property = fieldDefinitionElement.GetType().GetProperty(elementPropertyName);
          Type elementType = (Type) null;
          if (DefinitionBuilder.TryGetConfigItemType(fieldDefinitionElement, elementPropertyName, out elementType))
          {
            object obj = property.GetValue((object) fieldDefinitionElement, (object[]) null);
            property.PropertyType.GetMethod("Clear").Invoke(obj, (object[]) null);
            MethodInfo method = property.PropertyType.GetMethod("Add", new Type[1]
            {
              elementType
            });
            foreach (object definition1 in (IEnumerable) collection)
            {
              ConfigElement definitionElement = DefinitionBuilder.CreateOrUpdateDefinitionElement(elementType, definition1, parent, false, (ConfigElement) null);
              if (property != (PropertyInfo) null)
              {
                definitionElement.Parent = (ConfigElement) obj;
                method.Invoke(obj, new object[1]
                {
                  (object) definitionElement
                });
              }
            }
          }
        }
      }
    }

    private static PropertyInfo GetConfigProperty(
      ConfigElement fieldDefinitionElement,
      ConfigElement configElement)
    {
      string elementPropertyName = DefinitionBuilder.GetConfigElementPropertyName(configElement.GetType().Name);
      return fieldDefinitionElement.GetType().GetProperty(elementPropertyName);
    }

    private static string GetConfigElementPropertyName(string type)
    {
      if (type == "ValidatorDefinitionElement")
        return "ValidatorConfig";
      if (type == "ChoiceDefinition")
        return "ChoicesConfig";
      return type == "ExpandableDefinitionElement" ? "ExpandableDefinitionConfig" : string.Empty;
    }

    private static bool TryGetConfigItemType(
      ConfigElement fieldDefinitionElement,
      string configPropertyName,
      out Type elementType)
    {
      elementType = (Type) null;
      PropertyInfo property = fieldDefinitionElement.GetType().GetProperty(configPropertyName);
      if (property != (PropertyInfo) null)
      {
        Type[] genericArguments = property.PropertyType.GetGenericArguments();
        if (genericArguments.Length != 0)
        {
          elementType = genericArguments[0];
          return true;
        }
      }
      return false;
    }
  }
}
