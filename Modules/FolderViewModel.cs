// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.FolderViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>
  /// WCF type that represents the <see cref="T:Telerik.Sitefinity.Model.Folder" />.
  /// </summary>
  [DataContract]
  public class FolderViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.FolderViewModel" /> class.
    /// </summary>
    public FolderViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.FolderViewModel" /> class from a library.
    /// </summary>
    /// <param name="library">The library.</param>
    /// <param name="baseUrl">The base URL.</param>
    /// <param name="providerName">Name of the provider.</param>
    internal FolderViewModel(Library library, string baseUrl, string providerName)
    {
      string str;
      if (!string.IsNullOrEmpty(baseUrl))
      {
        string itemUrl = LibrariesManager.GetManager(providerName).GetItemUrl((ILocatable) library);
        str = string.IsNullOrEmpty(providerName) ? baseUrl + itemUrl + "/" : baseUrl + itemUrl + "/?provider=" + providerName;
      }
      else
        str = "";
      this.Title = (string) library.Title;
      this.Id = library.Id;
      this.ProviderName = providerName;
      this.Url = str;
    }

    /// <summary>Gets/Sets the folder id.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; set; }

    /// <summary>Gets/Sets the folder title.</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets/Sets the root folder id.</summary>
    [DataMember]
    public Guid RootId { get; set; }

    /// <summary>Gets/Sets the parent folder id.</summary>
    [DataMember]
    public Guid? ParentId { get; set; }

    /// <summary>Gets/Sets if the folder has children.</summary>
    [DataMember]
    public bool HasChildren { get; set; }

    /// <summary>Gets/Sets the folder url.</summary>
    [DataMember]
    public string Url { get; set; }

    /// <summary>Gets/Sets the folder path.</summary>
    [DataMember]
    public string Path { get; set; }
  }
}
