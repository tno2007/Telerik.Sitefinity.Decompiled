// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.HierarchicalContentViewModelBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Modules
{
  [DataContract]
  public class HierarchicalContentViewModelBase : ContentViewModelBase
  {
    private string url;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.HierarchicalContentViewModelBase" /> class.
    /// </summary>
    public HierarchicalContentViewModelBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.HierarchicalContentViewModelBase" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="liveItem">The live content item relevant to the master content item</param>
    /// <param name="tempItem">The temp content item relevant to the master content item.</param>
    public HierarchicalContentViewModelBase(
      Content contentItem,
      ContentDataProviderBase provider,
      Content liveItem,
      Content tempItem)
      : base(contentItem, provider, liveItem, tempItem)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.HierarchicalContentViewModelBase" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    public HierarchicalContentViewModelBase(Content contentItem, ContentDataProviderBase provider)
      : base(contentItem, provider)
    {
    }

    protected override Content GetLive() => throw new NotImplementedException();

    protected override Content GetTemp() => throw new NotImplementedException();

    /// <summary>Gets or sets the URL.</summary>
    /// <value>The URL.</value>
    [DataMember]
    public string Url
    {
      get => this.ContentItem == null ? this.url : this.GetItemUrl(this.ContentItem);
      set
      {
        this.url = value;
        if (this.ContentItem == null)
          return;
        this.ContentItem.UrlName = (Lstring) value;
      }
    }
  }
}
