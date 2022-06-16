// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.Services.WcfMarketContentItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.Services
{
  /// <summary>Represents summary information about a content item.</summary>
  [DataContract]
  public class WcfMarketContentItem
  {
    protected UrlDataProviderBase provider;
    private string owner;
    private string title;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.Services.ContentViewModel" /> class.
    /// </summary>
    public WcfMarketContentItem()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.Services.ContentViewModel" /> class.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="provider">The provider.</param>
    public WcfMarketContentItem(IDataItem dataItem, UrlDataProviderBase provider)
    {
      this.provider = provider;
      this.DataItem = dataItem;
    }

    /// <summary>Gets or sets the content item.</summary>
    /// <value>The content item.</value>
    public IDataItem DataItem { get; set; }

    /// <summary>Gets or sets the pageId.</summary>
    /// <value>The pageId.</value>
    [DataMember]
    public Guid Id
    {
      get => this.DataItem.Id;
      set
      {
        if (!(this.DataItem is Content dataItem))
          return;
        dataItem.Id = value;
      }
    }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    [DataMember]
    public string Title
    {
      get
      {
        if (string.IsNullOrEmpty(this.title))
        {
          if (this.DataItem is IHasTitle dataItem1)
          {
            this.title = dataItem1.GetTitle();
            return this.title;
          }
          if (this.DataItem is IUserProfile dataItem2)
          {
            this.title = dataItem2.GetUserDisplayName();
            return this.title;
          }
          if (this.DataItem is IContent dataItem3)
          {
            this.title = (string) dataItem3.Title;
            return this.title;
          }
          if (this.DataItem is DynamicContent dataItem4)
          {
            this.title = ModuleBuilderManager.GetTypeMainProperty(dataItem4.GetType()).GetValue((object) dataItem4).ToString();
            return this.title;
          }
        }
        return this.title;
      }
      set => this.title = value;
    }

    /// <summary>Gets or sets the owner.</summary>
    /// <value>The owner.</value>
    [DataMember]
    public string Owner
    {
      get
      {
        if (this.owner == null)
        {
          if (this.DataItem is IUserProfile)
            return (string) null;
          if (this.DataItem is IOwnership dataItem)
            this.owner = UserProfilesHelper.GetUserDisplayName(dataItem.Owner);
        }
        return this.owner;
      }
      set
      {
        if (!(this.DataItem is IOwnership dataItem))
          return;
        dataItem.Owner = new Guid(value);
      }
    }

    /// <summary>Gets or sets the author.</summary>
    /// <value>The author.</value>
    [DataMember]
    public virtual string Author
    {
      get => this.Owner;
      set
      {
      }
    }

    /// <summary>Gets or sets the URL.</summary>
    /// <value>The URL.</value>
    [DataMember]
    public string Url
    {
      get
      {
        try
        {
          return this.DataItem is ILocatable dataItem ? this.provider.GetItemUrl(dataItem) : string.Empty;
        }
        catch (Exception ex)
        {
          return string.Empty;
        }
      }
      set
      {
        if (!(this.DataItem is ILocatable dataItem))
          return;
        dataItem.UrlName = (Lstring) value;
      }
    }

    /// <summary>Gets or sets the date created.</summary>
    /// <value>The date created.</value>
    [DataMember]
    public DateTime DateCreated
    {
      get
      {
        if (this.DataItem is IContent dataItem1)
          return dataItem1.DateCreated;
        if (this.DataItem is DynamicContent dataItem2)
          return dataItem2.LastModified;
        return this.DataItem is PageNode dataItem3 ? dataItem3.DateCreated : new DateTime();
      }
      set
      {
        if (this.DataItem is Content dataItem)
        {
          dataItem.DateCreated = value;
        }
        else
        {
          if (!(this.DataItem is PageNode))
            return;
          (this.DataItem as PageNode).DateCreated = value;
        }
      }
    }

    /// <summary>Gets or sets the date modified.</summary>
    /// <value>The date modified.</value>
    [DataMember]
    public DateTime DateModified
    {
      get => this.DataItem.LastModified;
      set => this.DataItem.LastModified = value;
    }

    /// <summary>Gets or sets the publication date.</summary>
    /// <value>The publication date.</value>
    [DataMember]
    public DateTime? PublicationDate
    {
      get
      {
        if (this.DataItem is IUserProfile)
          return new DateTime?();
        return this.DataItem is IScheduleable dataItem ? new DateTime?(dataItem.PublicationDate) : new DateTime?();
      }
      set
      {
        if (!(this.DataItem is IScheduleable dataItem) || !value.HasValue)
          return;
        dataItem.PublicationDate = value.Value;
      }
    }

    /// <summary>Gets or sets the expiration date.</summary>
    /// <value>The expiration date.</value>
    [DataMember]
    public DateTime? ExpirationDate
    {
      get => this.DataItem is IScheduleable dataItem ? dataItem.ExpirationDate : new DateTime?();
      set
      {
        if (!(this.DataItem is IScheduleable dataItem))
          return;
        dataItem.ExpirationDate = value;
      }
    }
  }
}
