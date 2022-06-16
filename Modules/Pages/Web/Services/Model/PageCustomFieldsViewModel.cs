// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageCustomFieldsViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services.Model
{
  /// <summary>A container for the custom fields for the page.</summary>
  [DataContract]
  [SurrogateMetaData("GetData", typeof (PageCustomFieldsViewModel))]
  public class PageCustomFieldsViewModel
  {
    private Type metaType = typeof (PageNode);
    private PageNode node;

    /// <summary>Creates a new instanced of the class.</summary>
    public PageCustomFieldsViewModel()
    {
    }

    /// <summary>Creates a new instanced of the class.</summary>
    /// <param name="node">The <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> </param>
    public PageCustomFieldsViewModel(PageNode node)
      : this()
    {
      this.node = node;
    }

    internal PageNode GetNode() => this.node;

    /// <summary>
    /// Gets the dynamic properties(custom fields) that need to be serialized.
    /// </summary>
    internal static ICustomSurrogateDescriptor GetData() => (ICustomSurrogateDescriptor) new PageCustomFieldsSurrogateDescriptor()
    {
      InheritsFromSourceType = true,
      Properties = new PropertyDescriptorCollection(PageNode.SurrogateFieldProps.ToArray<PropertyDescriptor>())
    };
  }
}
