// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.DynamicLists.DynamicListInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Newsletters.DynamicLists
{
  /// <summary>
  /// The class that holds information about the dynamic list.
  /// </summary>
  public class DynamicListInfo
  {
    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.DynamicLists.DynamicListInfo" /> type.
    /// </summary>
    /// <param name="providerName">The name of the provider from which the dynamic list is pulled.</param>
    /// <param name="key">Key of the dynamic list. This key needs to be unique inside of the dynamic list provider.</param>
    /// <param name="title">Title of the list. This is what user will see in the user interface.</param>
    public DynamicListInfo(string providerName, string key, string title)
    {
      this.ProviderName = providerName;
      this.Key = key;
      this.Title = title;
    }

    /// <summary>
    /// Gets the unique key of the dynamic list. Every <see cref="T:Telerik.Sitefinity.Modules.Newsletters.DynamicLists.IDynamicListProvider" /> needs to be able
    /// to resolve the dynamic list by the key.
    /// </summary>
    public string Key { get; private set; }

    /// <summary>Gets the title of the dynamic list.</summary>
    public string Title { get; private set; }

    /// <summary>
    /// Gets the name of the provider from which this dynamic list is pulled.
    /// </summary>
    public string ProviderName { get; private set; }
  }
}
