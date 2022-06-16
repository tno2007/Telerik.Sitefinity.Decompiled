// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.Resolvers.InlineEditingResolverBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.InlineEditing.Resolvers
{
  internal class InlineEditingResolverBase
  {
    public FieldDefinitionElement GetFieldDefinition(
      ILifecycleDataItem item,
      string fieldName)
    {
      DetailFormViewElement detailFormViewElement = CustomFieldsContext.GetViews(item.GetType().FullName).Where<DetailFormViewElement>((Func<DetailFormViewElement, bool>) (v => v.DisplayMode == FieldDisplayMode.Write)).LastOrDefault<DetailFormViewElement>();
      if (detailFormViewElement != null)
      {
        foreach (ContentViewSectionElement section in (ConfigElementCollection) detailFormViewElement.Sections)
        {
          if (section.Fields.Contains(fieldName))
            return section.Fields[fieldName];
        }
      }
      return (FieldDefinitionElement) null;
    }
  }
}
