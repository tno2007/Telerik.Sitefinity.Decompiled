// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.PublishingTranslators.UserLinkConverter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Web;
using System.Xml.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.SiteSync.Serialization;
using Telerik.Sitefinity.Utilities.Json;

namespace Telerik.Sitefinity.SiteSync.PublishingTranslators
{
  internal class UserLinkConverter : ISiteSyncConverter
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
      UserLink userLink = (UserLink) obj;
      root.Add((object) new XElement((XName) "Role", (object) userLink.Role.Id));
      root.Add((object) new XElement((XName) "UserId", (object) userLink.UserId));
      root.Add((object) new XElement((XName) "Id", (object) userLink.Id));
      root.Add((object) new XElement((XName) "ApplicationName", (object) userLink.ApplicationName));
      root.Add((object) new XElement((XName) "LastModified", (object) userLink.LastModified));
      root.Add((object) new XElement((XName) "MembershipManagerInfo", (object) userLink.MembershipManagerInfo.ToJson<ManagerInfo>()));
    }

    public void ImportProperty(
      object instance,
      PropertyDescriptor prop,
      object value,
      Type type,
      object args)
    {
      string str1 = value as string;
      IEnumerable<string> strings = value as IEnumerable<string>;
      List<Guid> source = new List<Guid>();
      if (str1 != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, UserLink>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (UserLink), typeof (UserLinkConverter)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, UserLink> target = UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, UserLink>> p1 = UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__1;
        // ISSUE: reference to a compiler-generated field
        if (UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, UserLinkConverter, string, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Deserialize", (IEnumerable<Type>) null, typeof (UserLinkConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__0, this, str1, type, args);
        UserLink userLink = target((CallSite) p1, obj);
        prop.SetValue(instance, (object) userLink);
      }
      else if (strings != null)
      {
        foreach (string str2 in strings)
        {
          // ISSUE: reference to a compiler-generated field
          if (UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__2 = CallSite<Action<CallSite, UserLinkConverter, string, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Deserialize", (IEnumerable<Type>) null, typeof (UserLinkConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__2.Target((CallSite) UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__2, this, str2, typeof (UserLink), args);
          Guid guid = new Guid(this.GetElement(str2)((XName) "Id").Value);
          source.Add(guid);
        }
      }
      // ISSUE: reference to a compiler-generated field
      if (UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, RoleManager>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (RoleManager), typeof (UserLinkConverter)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, RoleManager> target1 = UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__4.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, RoleManager>> p4 = UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__4;
      // ISSUE: reference to a compiler-generated field
      if (UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Manager", typeof (UserLinkConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__3.Target((CallSite) UserLinkConverter.\u003C\u003Eo__2.\u003C\u003Ep__3, args);
      RoleManager roleManager = target1((CallSite) p4, obj1);
      Guid roleId = ((Role) instance).Id;
      IQueryable<UserLink> userLinks = roleManager.Provider.GetUserLinks();
      Expression<Func<UserLink, bool>> predicate = (Expression<Func<UserLink, bool>>) (ul => ul.Role.Id == roleId);
      foreach (UserLink userLink in userLinks.Where<UserLink>(predicate).ToList<UserLink>())
      {
        UserLink currrentUserLink = userLink;
        if (!source.Any<Guid>((Func<Guid, bool>) (id => id == currrentUserLink.Id)))
          roleManager.Provider.Delete(currrentUserLink);
      }
    }

    public object Deserialize(string str, Type type, object settings)
    {
      // ISSUE: reference to a compiler-generated field
      if (UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, RoleManager>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (RoleManager), typeof (UserLinkConverter)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, RoleManager> target1 = UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, RoleManager>> p1 = UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Manager", typeof (UserLinkConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__0, settings);
      RoleManager roleManager = target1((CallSite) p1, obj1);
      Func<XName, XElement> element = this.GetElement(str);
      Guid elementId = new Guid(element((XName) "Id").Value);
      UserLink userLink1 = roleManager.Provider.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (p => p.Id == elementId)).SingleOrDefault<UserLink>() ?? roleManager.Provider.CreateUserLink(elementId);
      userLink1.ApplicationName = element((XName) "ApplicationName").Value;
      userLink1.UserId = new Guid(element((XName) "UserId").Value);
      ManagerInfo membInfo = JsonUtility.FromJson<ManagerInfo>(element((XName) "MembershipManagerInfo").Value);
      ManagerInfo managerInfo1 = roleManager.Provider.GetManagerInfos().Where<ManagerInfo>((Expression<Func<ManagerInfo, bool>>) (p => p.ManagerType == membInfo.ManagerType && p.ProviderName == membInfo.ProviderName)).FirstOrDefault<ManagerInfo>();
      if (managerInfo1 == null)
      {
        ManagerInfo managerInfo2 = roleManager.Provider.CreateManagerInfo(membInfo.Id);
        managerInfo2.ProviderName = membInfo.ProviderName;
        managerInfo2.ManagerType = membInfo.ManagerType;
        userLink1.MembershipManagerInfo = managerInfo2;
      }
      else
        userLink1.MembershipManagerInfo = managerInfo1;
      userLink1.LastModified = DateTime.UtcNow;
      UserLink userLink2 = userLink1;
      // ISSUE: reference to a compiler-generated field
      if (UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, Role>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (Role), typeof (UserLinkConverter)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, Role> target2 = UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, Role>> p3 = UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Item", typeof (UserLinkConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__2.Target((CallSite) UserLinkConverter.\u003C\u003Eo__3.\u003C\u003Ep__2, settings);
      Role role = target2((CallSite) p3, obj2);
      userLink2.Role = role;
      return (object) userLink1;
    }

    private Func<XName, XElement> GetElement(string str) => new Func<XName, XElement>(((XContainer) XDocument.Parse(HttpUtility.HtmlDecode(str)).Root).Element);
  }
}
