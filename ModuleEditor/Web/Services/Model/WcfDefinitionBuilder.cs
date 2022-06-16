// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.Model.WcfDefinitionBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.ModuleEditor.Web.Services.Model
{
  /// <summary>
  /// Helper class for building instance of type <see cref="T:Telerik.Sitefinity.ModuleEditor.Web.Services.Model.WcfFieldDefinition" />,
  /// used when definition is needed on UI for rendering information for custom fields.
  /// </summary>
  public static class WcfDefinitionBuilder
  {
    /// <summary>Gets the base WCF definition.</summary>
    /// <param name="fieldType">Type of the field.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    public static WcfFieldDefinition GetBaseWcfDefinition(
      Type fieldType,
      Type contentType,
      string fieldName,
      string command)
    {
      WcfFieldDefinition definition = new WcfFieldDefinition();
      definition.FieldType = fieldType.FullName;
      definition.FieldName = fieldType.FullName;
      if (definition.FieldType.StartsWith("~"))
      {
        definition.FieldType = string.Empty;
        string fieldType1 = definition.FieldType;
        definition.FieldVirtualPath = fieldType1;
      }
      WcfDefinitionBuilder.BuildVisibleViewsInfo(fieldType, contentType, fieldName, definition, command);
      return definition;
    }

    /// <summary>
    /// Builds data transfer definition object needed for custom field objects.
    ///  </summary>
    /// <param name="fieldType">Type of the field.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="localizableByDefault">Specify whether the field should be localizable by default.</param>
    /// <returns></returns>
    public static WcfFieldDefinition GetWcfDefinition(
      Type fieldType,
      Type contentType,
      string fieldName,
      string command,
      bool localizableByDefault = false)
    {
      WcfFieldDefinition baseWcfDefinition = WcfDefinitionBuilder.GetBaseWcfDefinition(fieldType, contentType, fieldName, command);
      DetailFormViewElement detailFormViewElement = CustomFieldsContext.GetViews(contentType.FullName).FirstOrDefault<DetailFormViewElement>();
      if (localizableByDefault)
        baseWcfDefinition.IsLocalizable = true;
      if (detailFormViewElement != null)
      {
        foreach (ContentViewSectionElement section in (IEnumerable<ContentViewSectionElement>) detailFormViewElement.Sections.Values)
        {
          FieldDefinitionElement from = (FieldDefinitionElement) null;
          if (section != null)
            from = WcfDefinitionBuilder.GetFieldDefinitionElement(fieldName, section);
          if (from != null)
          {
            baseWcfDefinition.CopyPropertiesFrom((object) from);
            if (from.FieldType != (Type) null)
              baseWcfDefinition.FieldType = from.FieldType.FullName;
            if (from is TaxonFieldDefinitionElement && !baseWcfDefinition.ResourceClassId.IsNullOrEmpty())
            {
              if (!baseWcfDefinition.Title.IsNullOrEmpty())
                baseWcfDefinition.Title = Res.Get(baseWcfDefinition.ResourceClassId, baseWcfDefinition.Title, SystemManager.CurrentContext.Culture, true, false);
              if (!baseWcfDefinition.Example.IsNullOrEmpty())
                baseWcfDefinition.Example = Res.Get(baseWcfDefinition.ResourceClassId, baseWcfDefinition.Example, SystemManager.CurrentContext.Culture, true, false);
            }
            if (typeof (RelatedMediaFieldDefinitionElement).IsAssignableFrom(from.GetType()))
            {
              RelatedMediaFieldDefinitionElement definitionElement = (RelatedMediaFieldDefinitionElement) from;
              AssetsWorkMode? workMode = definitionElement.WorkMode;
              if (workMode.HasValue)
              {
                switch (workMode.Value)
                {
                  case AssetsWorkMode.SingleImage:
                    baseWcfDefinition.MediaType = "image";
                    baseWcfDefinition.AllowMultipleImages = false;
                    break;
                  case AssetsWorkMode.MultipleImages:
                    baseWcfDefinition.MediaType = "image";
                    baseWcfDefinition.AllowMultipleImages = true;
                    break;
                  case AssetsWorkMode.SingleDocument:
                    baseWcfDefinition.MediaType = "file";
                    baseWcfDefinition.AllowMultipleFiles = false;
                    break;
                  case AssetsWorkMode.MultipleDocuments:
                    baseWcfDefinition.MediaType = "file";
                    baseWcfDefinition.AllowMultipleFiles = true;
                    break;
                  case AssetsWorkMode.SingleVideo:
                    baseWcfDefinition.MediaType = "video";
                    baseWcfDefinition.AllowMultipleVideos = false;
                    break;
                  case AssetsWorkMode.MultipleVideos:
                    baseWcfDefinition.MediaType = "video";
                    baseWcfDefinition.AllowMultipleVideos = true;
                    break;
                  default:
                    baseWcfDefinition.MediaType = "image";
                    break;
                }
              }
              baseWcfDefinition.MaxFileSize = new int?(definitionElement.MaxFileSize / 1048576);
              baseWcfDefinition.FileExtensions = definitionElement.AllowedExtensions;
              baseWcfDefinition.FrontendWidgetTypeName = definitionElement.FrontendWidgetType != (Type) null ? definitionElement.FrontendWidgetType.FullName : definitionElement.FrontendWidgetVirtualPath;
              baseWcfDefinition.FrontendWidgetLabel = definitionElement.FrontendWidgetLabel;
              if (definitionElement.FieldType != (Type) null)
              {
                baseWcfDefinition.FieldType = definitionElement.FieldType.FullName;
                baseWcfDefinition.FieldVirtualPath = definitionElement.FieldVirtualPath;
              }
            }
            if (typeof (RelatedDataFieldDefinitionElement).IsAssignableFrom(from.GetType()))
            {
              RelatedDataFieldDefinitionElement definitionElement = (RelatedDataFieldDefinitionElement) from;
              baseWcfDefinition.FrontendWidgetTypeName = definitionElement.FrontendWidgetType != (Type) null ? definitionElement.FrontendWidgetType.FullName : definitionElement.FrontendWidgetVirtualPath;
              baseWcfDefinition.FrontendWidgetLabel = definitionElement.FrontendWidgetLabel;
              if (definitionElement.FieldType != (Type) null)
              {
                baseWcfDefinition.FieldType = definitionElement.FieldType.FullName;
                baseWcfDefinition.FieldVirtualPath = definitionElement.FieldVirtualPath;
              }
              baseWcfDefinition.RelatedDataProvider = definitionElement.RelatedDataProvider;
              baseWcfDefinition.RelatedDataType = definitionElement.RelatedDataType;
              baseWcfDefinition.AllowMultipleSelection = definitionElement.AllowMultipleSelection;
            }
            object obj1 = (object) new DynamicBindingProxy((object) baseWcfDefinition);
            if (from is TaxonFieldDefinitionElement definitionElement1)
            {
              // ISSUE: reference to a compiler-generated field
              if (WcfDefinitionBuilder.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
              {
                // ISSUE: reference to a compiler-generated field
                WcfDefinitionBuilder.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TaxonomyId", typeof (WcfDefinitionBuilder), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              object obj2 = WcfDefinitionBuilder.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) WcfDefinitionBuilder.\u003C\u003Eo__1.\u003C\u003Ep__0, obj1, definitionElement1.TaxonomyId.ToString());
            }
            if (from is FieldControlDefinitionElement definitionElement2)
            {
              if (definitionElement2.Value != null)
              {
                // ISSUE: reference to a compiler-generated field
                if (WcfDefinitionBuilder.\u003C\u003Eo__1.\u003C\u003Ep__1 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  WcfDefinitionBuilder.\u003C\u003Eo__1.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "DefaultValue", typeof (WcfDefinitionBuilder), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                object obj3 = WcfDefinitionBuilder.\u003C\u003Eo__1.\u003C\u003Ep__1.Target((CallSite) WcfDefinitionBuilder.\u003C\u003Eo__1.\u003C\u003Ep__1, obj1, definitionElement2.Value.ToString());
                // ISSUE: reference to a compiler-generated field
                if (WcfDefinitionBuilder.\u003C\u003Eo__1.\u003C\u003Ep__2 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  WcfDefinitionBuilder.\u003C\u003Eo__1.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "DefaultStringValue", typeof (WcfDefinitionBuilder), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                object obj4 = WcfDefinitionBuilder.\u003C\u003Eo__1.\u003C\u003Ep__2.Target((CallSite) WcfDefinitionBuilder.\u003C\u003Eo__1.\u003C\u003Ep__2, obj1, definitionElement2.Value.ToString());
              }
              ValidatorDefinition to = new ValidatorDefinition();
              baseWcfDefinition.ValidatorDefinition = to;
              if (definitionElement2.Validation != null)
                to.CopyPropertiesFrom((object) definitionElement2.Validation);
            }
            if (from is ChoiceFieldElement choiceFieldElement && choiceFieldElement.Choices != null && choiceFieldElement.Choices.Count > 0)
            {
              List<ChoiceDefinition> choiceDefinitionList = new List<ChoiceDefinition>();
              foreach (ChoiceElement configDefinition in choiceFieldElement.ChoicesConfig)
                choiceDefinitionList.Add(new ChoiceDefinition((ConfigElement) configDefinition));
              baseWcfDefinition.ChoiceDefinitions = choiceDefinitionList.ToArray();
            }
            baseWcfDefinition.SectionName = section.Name;
            break;
          }
          List<ChoiceDefinition> choiceDefinitionList1 = new List<ChoiceDefinition>();
          for (int index = 0; index < 3; ++index)
            choiceDefinitionList1.Add(new ChoiceDefinition()
            {
              Text = "Choice " + (index + 1).ToString(),
              Value = "Choice " + (index + 1).ToString()
            });
          baseWcfDefinition.ChoiceDefinitions = choiceDefinitionList1.ToArray();
        }
      }
      return baseWcfDefinition;
    }

    public static FieldDefinitionElement GetFieldDefinitionElement(
      string fieldName,
      ContentViewSectionElement section)
    {
      string key = fieldName + ".PersistedValue";
      FieldDefinitionElement definitionElement = (FieldDefinitionElement) null;
      if (section.Fields.ContainsKey(fieldName))
        definitionElement = section.Fields[fieldName];
      else if (section.Fields.ContainsKey(key))
        definitionElement = section.Fields[key];
      return definitionElement;
    }

    /// <summary>
    /// Gets the views for a specified field type, content and fieldName.
    /// </summary>
    /// <param name="fieldType">Type of the field.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    public static IDictionary<string, FieldViewInfo> GetFieldViewInfo(
      Type fieldType,
      Type contentType,
      string fieldName)
    {
      Dictionary<string, FieldViewInfo> fieldViewInfo1 = new Dictionary<string, FieldViewInfo>();
      foreach (DetailFormViewElement view in CustomFieldsContext.GetViews(contentType.FullName))
      {
        foreach (ContentViewSectionElement section in (ConfigElementCollection) view.Sections)
        {
          if (section != null)
          {
            FieldDefinitionElement definitionElement = WcfDefinitionBuilder.GetFieldDefinitionElement(fieldName, section);
            if (definitionElement != null)
            {
              FieldViewInfo fieldViewInfo2 = new FieldViewInfo()
              {
                FieldName = definitionElement.FieldName,
                SectionName = section.Name,
                ViewName = view.ViewName,
                Hidden = definitionElement.Hidden,
                ControlDefinitionName = view.ControlDefinitionName
              };
              string key = fieldViewInfo2.FieldName + "_" + (fieldViewInfo2.ViewName + "_") + fieldViewInfo2.ControlDefinitionName;
              if (!fieldViewInfo1.ContainsKey(key))
                fieldViewInfo1.Add(key, fieldViewInfo2);
            }
          }
        }
      }
      return (IDictionary<string, FieldViewInfo>) fieldViewInfo1;
    }

    private static void BuildVisibleViewsInfo(
      Type fieldType,
      Type contentType,
      string fieldName,
      WcfFieldDefinition definition,
      string command)
    {
      List<DetailFormViewElement> views = CustomFieldsContext.GetViews(contentType.FullName);
      List<string> stringList = new List<string>();
      IDictionary<string, FieldViewInfo> fieldViewInfo1 = WcfDefinitionBuilder.GetFieldViewInfo(fieldType, contentType, fieldName);
      foreach (DetailFormViewElement detailFormViewElement in views)
      {
        DetailFormViewElement singleView = detailFormViewElement;
        bool flag1 = false;
        FieldViewInfo fieldViewInfo2 = fieldViewInfo1.Where<KeyValuePair<string, FieldViewInfo>>((Func<KeyValuePair<string, FieldViewInfo>, bool>) (v => v.Value.ViewName == singleView.ViewName && v.Value.ControlDefinitionName == singleView.ControlDefinitionName)).Select<KeyValuePair<string, FieldViewInfo>, FieldViewInfo>((Func<KeyValuePair<string, FieldViewInfo>, FieldViewInfo>) (v => v.Value)).FirstOrDefault<FieldViewInfo>();
        if (fieldViewInfo2 != null)
        {
          bool? hidden = fieldViewInfo2.Hidden;
          int num;
          if (hidden.HasValue)
          {
            hidden = fieldViewInfo2.Hidden;
            bool flag2 = true;
            num = hidden.GetValueOrDefault() == flag2 & hidden.HasValue ? 1 : 0;
          }
          else
            num = 0;
          flag1 = num != 0;
        }
        bool flag3 = command == "editDefaultField";
        if (fieldViewInfo2 == null & flag3)
          flag1 = true;
        if (!flag1)
        {
          string str = singleView.ControlDefinitionName + " > " + singleView.ViewName;
          stringList.Add(str);
        }
      }
      definition.VisibleViews = stringList.Count != views.Count ? stringList.ToArray() : new string[0];
      definition.Hidden = stringList.Count == 0;
    }
  }
}
