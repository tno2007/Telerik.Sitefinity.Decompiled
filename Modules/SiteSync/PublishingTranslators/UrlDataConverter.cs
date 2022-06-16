// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.UrlDataConverter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Xml.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.SiteSync.Serialization;

namespace Telerik.Sitefinity.SiteSync
{
  internal class UrlDataConverter : ISiteSyncConverter
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
      UrlData urlData = (UrlData) obj;
      root.Add((object) new XElement((XName) "Culture", (object) urlData.Culture));
      root.Add((object) new XElement((XName) "Disabled", (object) urlData.Disabled));
      root.Add((object) new XElement((XName) "Id", (object) urlData.Id));
      root.Add((object) new XElement((XName) "IsDefault", (object) urlData.IsDefault));
      root.Add((object) new XElement((XName) "LastModified", (object) urlData.LastModified));
      root.Add((object) new XElement((XName) "QueryString", (object) urlData.QueryString));
      root.Add((object) new XElement((XName) "RedirectToDefault", (object) urlData.RedirectToDefault));
      root.Add((object) new XElement((XName) "Url", (object) HttpUtility.UrlEncode(urlData.Url)));
    }

    public void ImportProperty(
      object instance,
      PropertyDescriptor prop,
      object value,
      Type type,
      object args)
    {
      if (instance is MediaContent)
      {
        // ISSUE: reference to a compiler-generated field
        if (UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, PropertyInfo>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (PropertyInfo), typeof (UrlDataConverter)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, PropertyInfo> target1 = UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__2.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, PropertyInfo>> p2 = UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__2;
        // ISSUE: reference to a compiler-generated field
        if (UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetProperty", (IEnumerable<Type>) null, typeof (UrlDataConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string, object> target2 = UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string, object>> p1 = UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__1;
        // ISSUE: reference to a compiler-generated field
        if (UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetType", (IEnumerable<Type>) null, typeof (UrlDataConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__0, args);
        object obj2 = target2((CallSite) p1, obj1, "ImportTransaction");
        PropertyInfo propertyInfo = target1((CallSite) p2, obj2);
        ISiteSyncImportTransaction importTransaction = (ISiteSyncImportTransaction) null;
        if (propertyInfo != (PropertyInfo) null)
          importTransaction = propertyInfo.GetValue(args, (object[]) null) as ISiteSyncImportTransaction;
        if (args is IDictionary<string, object> dictionary)
          importTransaction = dictionary["ImportTransaction"] as ISiteSyncImportTransaction;
        if (importTransaction != null && importTransaction.Headers.ContainsKey("MultisiteMigrationTarget"))
          return;
      }
      // ISSUE: reference to a compiler-generated field
      if (UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, IManager>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (IManager), typeof (UrlDataConverter)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, IManager> target = UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__4.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, IManager>> p4 = UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__4;
      // ISSUE: reference to a compiler-generated field
      if (UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Manager", typeof (UrlDataConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__3.Target((CallSite) UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__3, args);
      IManager manager = target((CallSite) p4, obj3);
      ILocatable locatable = (ILocatable) instance;
      if (locatable.Provider is UrlDataProviderBase provider)
        provider.ClearItemUrls<ILocatable>(locatable);
      else
        locatable.ClearUrls();
      if (value == null)
        return;
      IEnumerable<string> strings = value as IEnumerable<string>;
      string str1 = value as string;
      if (strings == null && str1 != null)
        strings = (IEnumerable<string>) new string[1]
        {
          str1
        };
      if (strings == null)
        return;
      IList list = (IList) prop.GetValue(instance);
      Type genericArgument = type.GetGenericArguments()[0];
      foreach (string str2 in strings)
      {
        // ISSUE: reference to a compiler-generated field
        if (UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__5 = CallSite<Func<CallSite, UrlDataConverter, string, Type, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Deserialize", (IEnumerable<Type>) null, typeof (UrlDataConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__5.Target((CallSite) UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__5, this, str2, genericArgument, args);
        // ISSUE: reference to a compiler-generated field
        if (UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, Guid, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Id", typeof (UrlDataConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__6.Target((CallSite) UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__6, obj4, manager.Provider.GetNewGuid());
        // ISSUE: reference to a compiler-generated field
        if (UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, IDataItem, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Parent", typeof (UrlDataConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__7.Target((CallSite) UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__7, obj4, (IDataItem) instance);
        // ISSUE: reference to a compiler-generated field
        if (UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__8 = CallSite<Action<CallSite, IList, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (UrlDataConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__8.Target((CallSite) UrlDataConverter.\u003C\u003Eo__2.\u003C\u003Ep__8, list, obj4);
      }
    }

    public virtual object Deserialize(string str, Type type, object settings)
    {
      XElement root = XDocument.Parse(HttpUtility.HtmlDecode(str)).Root;
      UrlData instance = (UrlData) Activator.CreateInstance(type);
      UrlData urlData = instance;
      // ISSUE: reference to a compiler-generated field
      if (UrlDataConverter.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        UrlDataConverter.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, IManager>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (IManager), typeof (UrlDataConverter)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, IManager> target = UrlDataConverter.\u003C\u003Eo__3.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, IManager>> p1 = UrlDataConverter.\u003C\u003Eo__3.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (UrlDataConverter.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        UrlDataConverter.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Manager", typeof (UrlDataConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = UrlDataConverter.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) UrlDataConverter.\u003C\u003Eo__3.\u003C\u003Ep__0, settings);
      string applicationName = target((CallSite) p1, obj).Provider.ApplicationName;
      urlData.ApplicationName = applicationName;
      instance.Culture = int.Parse(root.Element((XName) "Culture").Value);
      instance.Disabled = bool.Parse(root.Element((XName) "Disabled").Value);
      instance.Id = new Guid(root.Element((XName) "Id").Value);
      instance.IsDefault = bool.Parse(root.Element((XName) "IsDefault").Value);
      instance.LastModified = DateTime.Parse(root.Element((XName) "LastModified").Value, (IFormatProvider) CultureInfo.InvariantCulture);
      instance.QueryString = root.Element((XName) "QueryString").Value;
      instance.RedirectToDefault = bool.Parse(root.Element((XName) "RedirectToDefault").Value);
      instance.Url = HttpUtility.UrlDecode(root.Element((XName) "Url").Value);
      return (object) instance;
    }
  }
}
