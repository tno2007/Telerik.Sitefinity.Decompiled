// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPageContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services.RelatedData.Messages;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services.Model
{
  [DataContract]
  public class WcfPageContext : ItemContext<WcfPage>
  {
    private string itemType = typeof (PageNode).FullName;

    /// <summary>
    /// Type of the content item.
    /// By default it is Telerik.Sitefinity.Pages.ModelPagе.PageNode
    /// </summary>
    [DataMember]
    public string ItemType
    {
      get => this.itemType;
      set => this.itemType = value;
    }

    /// <summary>Contains the changes made to all related data fields</summary>
    [DataMember]
    public ContentLinkChange[] ChangedRelatedData { get; set; }
  }
}
