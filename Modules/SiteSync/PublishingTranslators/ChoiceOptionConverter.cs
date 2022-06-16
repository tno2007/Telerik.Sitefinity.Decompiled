// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.ChoiceOptionConverter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Web;
using System.Xml.Linq;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.SiteSync.Serialization;

namespace Telerik.Sitefinity.SiteSync
{
  internal class ChoiceOptionConverter : ISiteSyncConverter
  {
    public string Serialize(object obj, Type type, object settings)
    {
      XDocument xdocument = new XDocument(new object[1]
      {
        (object) new XElement((XName) type.FullName)
      });
      this.AddElements(xdocument.Root, obj);
      return xdocument.ToString();
    }

    protected virtual void AddElements(XElement root, object obj)
    {
      ChoiceOption choiceOption = (ChoiceOption) obj;
      root.Add((object) new XElement((XName) "PersistedValue", (object) choiceOption.PersistedValue));
    }

    public void ImportProperty(
      object instance,
      PropertyDescriptor prop,
      object value,
      Type type,
      object args)
    {
      if (value == null)
      {
        prop.SetValue(instance, (object) null);
      }
      else
      {
        string str1 = value as string;
        IEnumerable<string> strings = value as IEnumerable<string>;
        if (str1 != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, ChoiceOption>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (ChoiceOption), typeof (ChoiceOptionConverter)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, ChoiceOption> target = ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, ChoiceOption>> p1 = ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__1;
          // ISSUE: reference to a compiler-generated field
          if (ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, ChoiceOptionConverter, string, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Deserialize", (IEnumerable<Type>) null, typeof (ChoiceOptionConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__0, this, str1, type, args);
          ChoiceOption choiceOption = target((CallSite) p1, obj);
          prop.SetValue(instance, (object) choiceOption);
        }
        else
        {
          List<ChoiceOption> choiceOptionList1 = new List<ChoiceOption>();
          if (strings != null)
          {
            foreach (string str2 in strings)
            {
              List<ChoiceOption> choiceOptionList2 = choiceOptionList1;
              // ISSUE: reference to a compiler-generated field
              if (ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__3 == null)
              {
                // ISSUE: reference to a compiler-generated field
                ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, ChoiceOption>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (ChoiceOption), typeof (ChoiceOptionConverter)));
              }
              // ISSUE: reference to a compiler-generated field
              Func<CallSite, object, ChoiceOption> target = ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__3.Target;
              // ISSUE: reference to a compiler-generated field
              CallSite<Func<CallSite, object, ChoiceOption>> p3 = ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__3;
              // ISSUE: reference to a compiler-generated field
              if (ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__2 == null)
              {
                // ISSUE: reference to a compiler-generated field
                ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__2 = CallSite<Func<CallSite, ChoiceOptionConverter, string, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Deserialize", (IEnumerable<Type>) null, typeof (ChoiceOptionConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              object obj = ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__2.Target((CallSite) ChoiceOptionConverter.\u003C\u003Eo__2.\u003C\u003Ep__2, this, str2, typeof (ChoiceOption), args);
              ChoiceOption choiceOption = target((CallSite) p3, obj);
              choiceOptionList2.Add(choiceOption);
            }
          }
          prop.SetValue(instance, (object) choiceOptionList1.ToArray());
        }
      }
    }

    public object Deserialize(string str, Type type, object settings)
    {
      XElement root = XDocument.Parse(HttpUtility.HtmlDecode(str)).Root;
      ChoiceOption instance = (ChoiceOption) Activator.CreateInstance(type);
      instance.PersistedValue = root.Element((XName) "PersistedValue").Value;
      return (object) instance;
    }
  }
}
