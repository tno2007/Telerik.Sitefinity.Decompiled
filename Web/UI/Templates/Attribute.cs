// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.Attribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.DataResolving;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  [DebuggerDisplay("Attribute {Name} = {Value}")]
  internal class Attribute
  {
    private string name;
    private object value;
    private string changedValue;
    private bool isAttribute;
    private bool isSimpleFormat;
    private bool hasBindings;
    private bool hasDataItem;
    private ObjectBuilder builder;
    private SubstitutionItem[] substitutions;
    private PropertyDescriptor[] propertyDescriptors;
    private static Type stringType = typeof (string);
    private static Regex evalRegex = new Regex("\\s*Eval\\s*\\(\\s*\"\\s*(?<key>[\\w\\d\\.]+)\\s*\"\\s*\\)|\\s*Eval\\s*\\(\\s*\"\\s*(?<key>[\\w\\d\\.]+)\\s*\"\\s*,\\s*\"(?<format>.*)\"\\s*\\)", RegexOptions.Compiled);
    private static Regex resolveRegex = new Regex("\\s*DataResolver\\s*.\\s*Resolve\\s*\\(\\s*Container\\s*.\\s*DataItem\\s*,\\s*\"\\s*(?<key>[\\w\\d\\.]+)\\s*\"\\s*\\)|\\s*DataResolver\\s*.\\s*Resolve\\s*\\(\\s*Container\\s*.\\s*DataItem\\s*,\\s*\"\\s*(?<key>[\\w\\d\\.]+)\\s*\"\\s*,\\s*\"(?<format>.*)\"\\s*,\\s*\"(?<args>.*)\"\\s*\\)|\\s*DataResolver\\s*.\\s*Resolve\\s*\\(\\s*Container\\s*.\\s*DataItem\\s*,\\s*\"\\s*(?<key>[\\w\\d\\.]+)\\s*\"\\s*,\\s*\"(?<format>.*)\"\\s*\\)", RegexOptions.Compiled);
    private static Regex localResourcesRegex = new Regex("\\s*GetLocalResourceObject\\(\"(\\S*)\"\\)\\s*", RegexOptions.Compiled);

    public Attribute(string name, string value, ObjectBuilder builder)
      : this(name, value, builder, (PropertyDescriptorCollection) null)
    {
    }

    public Attribute(
      string name,
      string value,
      ObjectBuilder builder,
      PropertyDescriptorCollection properties)
    {
      this.name = name;
      this.builder = builder;
      this.ParseValue(value);
      if (properties == null)
        properties = TypeDescriptor.GetProperties(builder.ObjectType);
      string[] strArray = name.Split('-');
      PropertyDescriptor propertyDescriptor = properties.Find(strArray[0], true);
      if (propertyDescriptor == null)
      {
        this.propertyDescriptors = new PropertyDescriptor[0];
        if (!typeof (IAttributeAccessor).IsAssignableFrom(builder.ObjectType))
          this.ThrowInvalidAttribute(builder.TagName, builder.ObjectType, strArray[0], value);
        this.isAttribute = true;
      }
      else
      {
        this.propertyDescriptors = new PropertyDescriptor[strArray.Length];
        this.propertyDescriptors[0] = propertyDescriptor;
        for (int index = 1; index < strArray.Length; ++index)
        {
          propertyDescriptor = TypeDescriptor.GetProperties(propertyDescriptor.PropertyType).Find(strArray[index], true);
          if (propertyDescriptor == null)
            this.ThrowInvalidAttribute(builder.TagName, builder.ObjectType, strArray[index], value);
          this.propertyDescriptors[index] = propertyDescriptor;
        }
        if (propertyDescriptor.IsReadOnly)
        {
          if (!typeof (IAttributeAccessor).IsAssignableFrom(builder.ObjectType))
            throw new InvalidOperationException("Read only property.");
          this.isAttribute = true;
        }
        else if (!typeof (string).IsAssignableFrom(propertyDescriptor.PropertyType))
        {
          bool flag = false;
          object obj = (object) null;
          try
          {
            flag = propertyDescriptor.Converter.CanConvertFrom(typeof (string));
          }
          catch (MethodAccessException ex1)
          {
            try
            {
              obj = Convert.ChangeType(this.value, propertyDescriptor.PropertyType);
              flag = obj.GetType().Equals(propertyDescriptor.PropertyType);
            }
            catch (InvalidCastException ex2)
            {
            }
          }
          if (!flag)
            throw new InvalidOperationException("Cannot convert from string.");
          if (this.substitutions == null)
            this.value = obj == null ? propertyDescriptor.Converter.ConvertFrom(this.value) : (object) value;
        }
      }
      builder.Attributes.Add(this);
    }

    internal string Name => this.name;

    internal object Value => this.value;

    internal string ChangedValue => this.changedValue;

    internal bool HasBindings => this.hasBindings;

    internal SubstitutionItem[] Substitutions => this.substitutions;

    private void ThrowInvalidAttribute(
      string tagName,
      Type objectType,
      string attributeName,
      string value)
    {
      throw new FormatException(string.Format(Res.Get<ErrorMessages>().TemplateParserCannotSetAttributeValue, (object) tagName, (object) objectType, (object) attributeName, (object) value));
    }

    internal void SetAttribute(object component, Control bindingContainer) => this.SetAttribute(component, bindingContainer, false);

    private void SetAttribute(object component, Control bindingContainer, bool bound)
    {
      PropertyDescriptor descriptor;
      if (this.propertyDescriptors.Length != 0)
      {
        descriptor = this.propertyDescriptors[0];
        for (int index = 1; index < this.propertyDescriptors.Length; ++index)
        {
          component = descriptor.GetValue(component);
          descriptor = this.propertyDescriptors[index];
        }
      }
      else
        descriptor = (PropertyDescriptor) null;
      object obj;
      if (this.substitutions != null)
      {
        if (!bound && this.hasBindings)
        {
          Attribute.BoundAttribute boundAttribute = new Attribute.BoundAttribute(this, component, bindingContainer);
          return;
        }
        obj = this.GetValue(descriptor, bindingContainer);
      }
      else
        obj = this.value;
      if (this.isAttribute)
        ((IAttributeAccessor) component).SetAttribute(this.name, (string) obj);
      else if (obj == null && !descriptor.PropertyType.IsValueType || obj != null && descriptor.PropertyType.IsAssignableFrom(obj.GetType()))
      {
        if (component is Content && descriptor.Name == "ContentPlaceHolderID")
        {
          Content content = (Content) component;
          bool flag = false;
          if (content.Site == null)
          {
            content.Site = (ISite) new DesignTimeSite((IComponent) content);
            flag = true;
          }
          content.ContentPlaceHolderID = (string) obj;
          if (!flag)
            return;
          content.Site = (ISite) null;
        }
        else
          descriptor.SetValue(component, obj);
      }
      else
      {
        if (!descriptor.PropertyType.IsAssignableFrom(Attribute.stringType))
          return;
        descriptor.SetValue(component, (object) Convert.ToString(obj));
      }
    }

    private object GetValue(PropertyDescriptor descriptor, Control bindingContainer)
    {
      object obj;
      if (this.isSimpleFormat)
      {
        obj = this.GetValue(this.substitutions[0], bindingContainer);
      }
      else
      {
        int num = 0;
        string str1 = this.changedValue;
        foreach (SubstitutionItem substitution in this.substitutions)
        {
          if (descriptor != null && !descriptor.PropertyType.IsAssignableFrom(Attribute.stringType))
            throw new NotSupportedException("Only string values are supported for complex formats.");
          string str2 = Convert.ToString(this.GetValue(substitution, bindingContainer));
          str1 = str1.Insert(substitution.Index + num, str2);
          num += str2.Length;
        }
        obj = (object) str1;
      }
      return obj;
    }

    private void ParseValue(string attVal)
    {
      if (attVal != null && attVal.Length > 8)
      {
        List<SubstitutionItem> substs = (List<SubstitutionItem>) null;
        int element = this.ParseElement(ref attVal, 0, ref substs);
        if (element != -1)
        {
          while (element > -1 && element < attVal.Length)
            element = this.ParseElement(ref attVal, element, ref substs);
        }
        if (substs != null)
        {
          this.substitutions = substs.ToArray();
          this.changedValue = attVal;
        }
      }
      this.value = (object) attVal;
    }

    protected int ParseElement(ref string str, int startIndex, ref List<SubstitutionItem> substs)
    {
      int element = str.IndexOf("<%", startIndex, StringComparison.Ordinal);
      string input;
      int end;
      if (element != -1 && Attribute.GetDeclaration(str, element, out input, out end))
      {
        int num1 = end + 2;
        string name = (string) null;
        string format = (string) null;
        string args = (string) null;
        bool isDataResolver = false;
        DataType type;
        switch (str[element + 2])
        {
          case '#':
            Match match1 = Attribute.evalRegex.Match(input);
            if (match1.Success)
            {
              name = match1.Groups["key"].Value;
              format = match1.Groups["format"].Value;
            }
            else
            {
              Match match2 = Attribute.resolveRegex.Match(input);
              if (match2.Success)
              {
                name = match2.Groups["key"].Value;
                format = match2.Groups["format"].Value;
                args = match2.Groups["args"].Value;
                isDataResolver = true;
              }
            }
            type = DataType.DataItem;
            this.hasBindings = true;
            this.hasDataItem = true;
            break;
          case '$':
            int num2 = input.IndexOf(':');
            name = input.Substring(num2 + 1);
            type = DataType.Resource;
            break;
          case '-':
          case '@':
            type = DataType.Comment;
            break;
          case '=':
            Match match3 = Attribute.localResourcesRegex.Match(input);
            if (match3.Success)
            {
              name = match3.Groups[1].Value;
              type = DataType.Resource;
              break;
            }
            name = input;
            type = DataType.Control;
            this.hasBindings = true;
            break;
          default:
            throw new FormatException(Res.Get<ErrorMessages>("TemplateParser_UnknownCharacterSequence", (object) str[element + 2]));
        }
        if (type != DataType.Comment)
        {
          if (substs == null)
            substs = new List<SubstitutionItem>();
          substs.Add(new SubstitutionItem(element, name, type, format, args, isDataResolver));
        }
        if (element > 0 || num1 < str.Length - 1)
        {
          str = str.Remove(element, num1 - element);
          this.isSimpleFormat = false;
        }
        else
        {
          str = string.Empty;
          this.isSimpleFormat = true;
        }
      }
      return element;
    }

    protected static bool GetDeclaration(
      string str,
      int startIndex,
      out string value,
      out int end)
    {
      startIndex += 3;
      end = str.IndexOf("%>", startIndex, StringComparison.Ordinal);
      if (end != -1)
      {
        int length = str.Length - (str.Length - end + startIndex);
        value = str.Substring(startIndex, length).Trim();
        return true;
      }
      value = (string) null;
      return false;
    }

    private object GetValue(SubstitutionItem subst, Control bindingContainer)
    {
      switch (subst.Type)
      {
        case DataType.Resource:
          try
          {
            return (object) Res.Get(subst.ClassId, subst.Name);
          }
          catch (ArgumentException ex)
          {
            TemplateException exceptionToHandle = new TemplateException(Res.Get<ErrorMessages>("InvalidClassIdInTemplate", (object) subst.ClassId, (object) this.builder.TemplatePath), (Exception) ex);
            if (Exceptions.HandleException((Exception) exceptionToHandle, ExceptionPolicyName.Global))
              throw exceptionToHandle;
            return (object) null;
          }
          catch (KeyNotFoundException ex)
          {
            TemplateException exceptionToHandle = new TemplateException(Res.Get<ErrorMessages>("InvalidResourceKey", (object) subst.Name, (object) subst.ClassId, (object) this.builder.TemplatePath), (Exception) ex);
            if (Exceptions.HandleException((Exception) exceptionToHandle, ExceptionPolicyName.Global))
              throw exceptionToHandle;
            return (object) null;
          }
        case DataType.Control:
          return subst.GetValue((object) bindingContainer);
        case DataType.DataItem:
          object obj = bindingContainer.Page != null ? bindingContainer.Page.GetDataItem() : throw new InvalidOperationException("The container must be added to the Page controls collection prior to data binding.");
          return subst.IsDataResolver ? (object) DataResolver.Resolve(obj, subst.Name, subst.Format, subst.Args) : (object) DataBinder.Eval(obj, subst.Name, subst.Format);
        default:
          throw new InvalidOperationException();
      }
    }

    private class BoundAttribute
    {
      private Attribute parent;
      private object component;
      private Control bindingContainer;

      public BoundAttribute(Attribute parent, object component, Control bindingContainer)
      {
        this.parent = parent;
        this.component = component;
        this.bindingContainer = bindingContainer;
        if (parent.hasDataItem)
          ((Control) this.component).DataBinding += new EventHandler(this.bindingContainer_DataBinding);
        else
          this.bindingContainer.PreRender += new EventHandler(this.bindingContainer_DataBinding);
      }

      private void bindingContainer_DataBinding(object sender, EventArgs e) => this.parent.SetAttribute(this.component, this.bindingContainer, true);
    }
  }
}
