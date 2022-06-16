// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.Model.ContentLinksFieldBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.ModuleEditor.Web.Services.Model
{
  public class ContentLinksFieldBuilder : ICustomFieldBuilder
  {
    /// <summary>
    /// Implement custom logic for createion of a meta field and add it to the specified type
    /// </summary>
    /// <param name="metaType">Type to which to add the metafield</param>
    /// <param name="field">WCF field from which a meta field is created</param>
    /// <param name="manager">MetaData manger</param>
    /// <param name="contentType">Conetnt type</param>
    /// <returns></returns>
    public MetaField CreateCustomMetaField(
      MetaType metaType,
      WcfField field,
      MetadataManager manager,
      Type contentType)
    {
      if (field.FieldTypeKey == "Image")
      {
        field.Definition.FieldType = typeof (ImageField).FullName;
        return ContentLinksExtensions.CreateContentLinkField(field.Name, (string) null, manager);
      }
      throw new NotImplementedException("Support of {0} is not implemented.".Arrange((object) field.FieldTypeKey));
    }

    /// <inheritdoc />
    public FieldDefinitionElement CreateOrUpdateDynamicDefinitionElement(
      WcfField field,
      Type fieldControlType,
      ConfigElement parent,
      ConfigElement instance)
    {
      FieldDefinitionElement definitionElement = DefinitionBuilder.CreateOrUpdateDefinitionElement(fieldControlType, (object) field.Definition, parent, true, instance) as FieldDefinitionElement;
      object obj1 = (object) new DynamicBindingProxy((object) definitionElement);
      if (fieldControlType != (Type) null)
      {
        // ISSUE: reference to a compiler-generated field
        if (ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Type, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "FieldType", typeof (ContentLinksFieldBuilder), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__0, obj1, fieldControlType);
      }
      if (fieldControlType == typeof (ImageField))
      {
        // ISSUE: reference to a compiler-generated field
        if (ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "MaxWidth", typeof (ContentLinksFieldBuilder), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__1.Target((CallSite) ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__1, obj1, field.Definition.MaxWidth);
        // ISSUE: reference to a compiler-generated field
        if (ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "MaxHeight", typeof (ContentLinksFieldBuilder), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__2.Target((CallSite) ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__2, obj1, field.Definition.MaxHeight);
        // ISSUE: reference to a compiler-generated field
        if (ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ProviderNameForDefaultImage", typeof (ContentLinksFieldBuilder), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__3.Target((CallSite) ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__3, obj1, field.Definition.ProviderNameForDefaultImage);
        // ISSUE: reference to a compiler-generated field
        if (ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, Guid, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "DefaultImageId", typeof (ContentLinksFieldBuilder), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__4.Target((CallSite) ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__4, obj1, field.Definition.DefaultImageId);
        // ISSUE: reference to a compiler-generated field
        if (ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, Type, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "DataFieldType", typeof (ContentLinksFieldBuilder), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj7 = ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__5.Target((CallSite) ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__5, obj1, typeof (ContentLink));
      }
      // ISSUE: reference to a compiler-generated field
      if (ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "DataFieldName", typeof (ContentLinksFieldBuilder), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj8 = ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__6.Target((CallSite) ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__6, obj1, field.Definition.FieldName);
      // ISSUE: reference to a compiler-generated field
      if (ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "MessageCssClass", typeof (ContentLinksFieldBuilder), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string, object> target = ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__8.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string, object>> p8 = ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__8;
      // ISSUE: reference to a compiler-generated field
      if (ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ValidatorConfig", typeof (ContentLinksFieldBuilder), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj9 = ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__7.Target((CallSite) ContentLinksFieldBuilder.\u003C\u003Eo__1.\u003C\u003Ep__7, obj1);
      object obj10 = target((CallSite) p8, obj9, "sfError");
      return definitionElement;
    }

    public void PopulateDefinitionTo()
    {
    }
  }
}
