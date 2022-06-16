// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.Implementation.AnyPropertyEditorFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Fluent.AnyContent.Implementation
{
  public class AnyPropertyEditorFacade<TParentFacade> : IAnyPropertyEditorFacade<TParentFacade>
    where TParentFacade : class
  {
    private TParentFacade parentFacade;
    private Content item;
    private IManager manager;

    public void SetInitialState(TParentFacade parentFacade, Content item, IManager manager)
    {
      FacadeHelper.AssertArgumentNotNull<TParentFacade>(parentFacade, nameof (parentFacade));
      FacadeHelper.AssertArgumentNotNull<Content>(item, nameof (item));
      FacadeHelper.AssertArgumentNotNull<IManager>(manager, nameof (manager));
      this.parentFacade = parentFacade;
      this.item = item;
      this.manager = manager;
    }

    public IAnyPropertyEditorFacade<TParentFacade> SetValue(
      string propertyName,
      object value)
    {
      return this.SetValue(propertyName, value, false);
    }

    public IAnyPropertyEditorFacade<TParentFacade> SetValue(
      string propertyName,
      object value,
      bool caseInsensitive)
    {
      FacadeHelper.Assert<ArgumentException>(!string.IsNullOrWhiteSpace(propertyName), "Argument propertyName must not be null or whitespace");
      PropertyDescriptor desc = TypeDescriptor.GetProperties((object) this.item).Find(propertyName, caseInsensitive);
      if (desc != null)
      {
        object obj = value;
        if (value != null)
        {
          Type type = value.GetType();
          if (type != desc.PropertyType)
          {
            bool flag = false;
            if (desc.Converter.CanConvertFrom(type))
            {
              obj = desc.Converter.ConvertFrom(value);
              flag = true;
            }
            if (!flag)
            {
              MethodInfo methodInfo = ((IEnumerable<MethodInfo>) type.GetMethods(BindingFlags.Static | BindingFlags.Public)).Where<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "op_Implicit" && m.GetParameters()[0].ParameterType == desc.PropertyType)).FirstOrDefault<MethodInfo>();
              if (methodInfo != (MethodInfo) null)
              {
                obj = methodInfo.Invoke((object) null, new object[1]
                {
                  value
                });
                flag = true;
              }
            }
            if (!flag)
            {
              if (typeof (IConvertible).IsAssignableFrom(type))
              {
                try
                {
                  obj = ((IConvertible) value).ToType(desc.PropertyType, (IFormatProvider) SystemManager.CurrentContext.Culture);
                  flag = true;
                }
                catch
                {
                }
              }
            }
            if (!flag)
            {
              ConstructorInfo constructor = desc.PropertyType.GetConstructor(new Type[1]
              {
                type
              });
              if (constructor != (ConstructorInfo) null)
                obj = constructor.Invoke(new object[1]
                {
                  value
                });
            }
          }
        }
        desc.SetValue((object) this.item, obj);
      }
      return (IAnyPropertyEditorFacade<TParentFacade>) this;
    }

    public IAnyPropertyEditorFacade<TParentFacade> GetValueAndContinue(
      string propertyName,
      out object value)
    {
      value = this.GetValue(propertyName);
      return (IAnyPropertyEditorFacade<TParentFacade>) this;
    }

    public IAnyPropertyEditorFacade<TParentFacade> GetValueAndContinue(
      string propertyName,
      out object value,
      bool caseInsensitive)
    {
      value = this.GetValue(propertyName, caseInsensitive);
      return (IAnyPropertyEditorFacade<TParentFacade>) this;
    }

    public object GetValue(string propertyName) => this.GetValue(propertyName, false);

    public object GetValue(string propertyName, bool caseInsensitive)
    {
      FacadeHelper.Assert<ArgumentException>(!string.IsNullOrWhiteSpace(propertyName), "Argument propertyName must not be null or whitespace");
      PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties((object) this.item).Find(propertyName, caseInsensitive);
      FacadeHelper.AssertNotNull<PropertyDescriptor>(propertyDescriptor, "No property getter with that name");
      return propertyDescriptor.GetValue((object) this.item);
    }

    public TParentFacade Done() => this.Done(true);

    public TParentFacade Done(bool recompileUrls)
    {
      if (this.manager != null & recompileUrls)
        CommonMethods.RecompileItemUrls(this.item, this.manager);
      return this.parentFacade;
    }
  }
}
