// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPageTemplateContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services.RelatedData.Messages;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services.Model
{
  /// <summary>Page template context.</summary>
  [DataContract]
  public class WcfPageTemplateContext : ItemContext<PageTemplateViewModel>
  {
    private string itemType = typeof (PageTemplate).FullName;

    /// <summary>
    /// Gets or sets a value containing the changes made to all related data fields
    /// </summary>
    [DataMember]
    public ContentLinkChange[] ChangedRelatedData { get; set; }

    /// <summary>
    /// Gets or sets the type of the content item. Set automatically when <see cref="!:Item" /> is set.
    /// </summary>
    [DataMember]
    public string ItemType
    {
      get => this.itemType;
      set => this.itemType = value;
    }
  }
}
