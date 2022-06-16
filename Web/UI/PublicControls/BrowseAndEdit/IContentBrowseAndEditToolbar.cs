// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.IContentBrowseAndEditToolbar
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.UI.ContentUI;

namespace Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit
{
  public interface IContentBrowseAndEditToolbar : IBrowseAndEditToolbar
  {
    /// <summary>Configures the content toolbar.</summary>
    /// <param name="host">The container for specific views.</param>
    /// <param name="contentItem">The content item.</param>
    void Configure(ContentView host, Content contentItem);

    /// <summary>Configures the content toolbar.</summary>
    /// <param name="host">The container for specific views.</param>
    /// <param name="contentItem">The content item.</param>
    /// <param name="parentItem">The parent item.</param>
    void Configure(ContentView host, Content contentItem, IDataItem parentItem);
  }
}
