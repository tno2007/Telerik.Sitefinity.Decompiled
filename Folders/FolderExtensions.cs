// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Folders.FolderExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

namespace Telerik.Sitefinity.Folders
{
  internal static class FolderExtensions
  {
    private static Dictionary<Type, IEnumerable<Type>> folderItemTypesForManager = new Dictionary<Type, IEnumerable<Type>>();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Model.Folder" /> instance.
    /// </summary>
    public static Folder CreateFolder(this IFolderOAProvider provider, Guid id)
    {
      if (!(provider is DataProviderBase dataProviderBase))
        throw new ArgumentException("The object is not inheriting from DataProviderBase.");
      Folder entity = new Folder(dataProviderBase.ApplicationName, id);
      entity.Provider = (object) provider;
      provider.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Model.Folder" /> instance.
    /// </summary>
    public static Folder CreateFolder(this IFolderOAProvider provider) => provider.CreateFolder(Guid.NewGuid());

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Model.Folder" /> for removal.
    /// </summary>
    /// <param name="folder">The folder to delete.</param>
    public static void Delete(this IFolderOAProvider provider, Folder folder) => provider.GetContext().Remove((object) folder);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Model.Folder" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Model.Folder" /> objects.
    /// </returns>
    public static IQueryable<Folder> GetFolders(this IFolderOAProvider provider)
    {
      string appName = provider is DataProviderBase dataProvider ? dataProvider.ApplicationName : throw new ArgumentException("The object is not inheriting from DataProviderBase.");
      return SitefinityQuery.Get<Folder>(dataProvider, MethodBase.GetCurrentMethod()).Where<Folder>((Expression<Func<Folder, bool>>) (f => f.ApplicationName == appName));
    }

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Model.Folder" /> objects in scope of given type T.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Model.Folder" /> objects.
    /// </returns>
    public static IQueryable<Folder> GetFolders<T>(this IFolderOAProvider provider) where T : IDataItem
    {
      string appName = provider is DataProviderBase dataProvider ? dataProvider.ApplicationName : throw new ArgumentException("The object is not inheriting from DataProviderBase.");
      return SitefinityQuery.Get<Folder>(dataProvider, MethodBase.GetCurrentMethod()).Join<Folder, T, Guid, Folder>((IEnumerable<T>) Queryable.Cast<T>(SitefinityQuery.Get(typeof (T), dataProvider)), (Expression<Func<Folder, Guid>>) (f => f.RootId), (Expression<Func<T, Guid>>) (l => l.Id), (Expression<Func<Folder, T, Folder>>) ((f, l) => f)).Where<Folder>((Expression<Func<Folder, bool>>) (f => f.ApplicationName == appName));
    }

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.Model.Folder" /> by its id.
    /// </summary>
    /// <param name="id">Id of the folder to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Model.Folder" />.</returns>
    public static Folder GetFolder(this IFolderOAProvider provider, Guid id) => !(id == Guid.Empty) ? provider.GetContext().GetItemById<Folder>(id.ToString()) : throw new ArgumentException("Argument 'id' cannot be empty GUID.");

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Model.Folder" /> instance.
    /// </summary>
    public static Folder CreateFolder(this IFolderManager manager) => ((IFolderOAProvider) manager.Provider).CreateFolder();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Model.Folder" /> instance.
    /// </summary>
    public static Folder CreateFolder(this IFolderManager manager, Guid id) => ((IFolderOAProvider) manager.Provider).CreateFolder(id);

    /// <summary>
    /// Marks an <see cref="T:Telerik.Sitefinity.Model.Folder" /> for removal.
    /// </summary>
    /// <param name="folder">The folders to delete.</param>
    public static void Delete(this IFolderManager manager, Folder folder)
    {
      if (folder == null)
        throw new ArgumentNullException(nameof (folder));
      IQueryable<Folder> folders = manager.GetFolders();
      Expression<Func<Folder, bool>> predicate1 = (Expression<Func<Folder, bool>>) (f => f.ParentId == (Guid?) folder.Id);
      foreach (Folder folder1 in folders.Where<Folder>(predicate1).ToArray<Folder>())
        manager.Delete(folder1);
      foreach (Type folderItemType in FolderExtensions.GetFolderItemTypes(manager))
      {
        IQueryable<IFolderItem> source = Queryable.Cast<IFolderItem>(SitefinityQuery.Get(folderItemType, manager.Provider));
        Expression<Func<IFolderItem, bool>> predicate2 = (Expression<Func<IFolderItem, bool>>) (d => d.FolderId == (Guid?) folder.Id);
        foreach (IFolderItem folderItem in source.Where<IFolderItem>(predicate2).ToArray<IFolderItem>())
        {
          if (folderItem.Parent != null && folderItem.Parent.Id == folder.RootId)
            manager.DeleteItem((object) folderItem);
        }
      }
      ((IFolderOAProvider) manager.Provider).Delete(folder);
    }

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Model.Folder" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Model.Folder" /> objects.
    /// </returns>
    public static IQueryable<Folder> GetFolders(this IFolderManager manager) => (IQueryable<Folder>) ((IFolderOAProvider) manager.Provider).GetFolders().OrderBy<Folder, Lstring>((Expression<Func<Folder, Lstring>>) (f => f.Title));

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Model.Folder" /> objects in scope of given type T.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Model.Folder" /> objects.
    /// </returns>
    public static IQueryable<Folder> GetFolders<T>(this IFolderManager manager) where T : IDataItem => ((IFolderOAProvider) manager.Provider).GetFolders<T>();

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.Model.Folder" /> by its id.
    /// </summary>
    /// <param name="id">Id of the folder to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Model.Folder" />.</returns>
    public static Folder GetFolder(this IFolderManager manager, Guid id) => ((IFolderOAProvider) manager.Provider).GetFolder(id);

    /// <summary>Changes the folder of an item.</summary>
    /// <param name="manager">The manager.</param>
    /// <param name="item">The item.</param>
    /// <param name="newParentFolder">The new parent folder.</param>
    public static void ChangeItemFolder(
      this IFolderManager manager,
      IFolderItem item,
      Guid? newParentFolderId)
    {
      ILifecycleManager manager1 = manager as ILifecycleManager;
      ILifecycleDataItem lifecycleDataItem = item as ILifecycleDataItem;
      if (manager1 != null && lifecycleDataItem != null && lifecycleDataItem.Status != ContentLifecycleStatus.Temp)
        manager1.DoForAllVersions(lifecycleDataItem, (Action<ILifecycleDataItem>) (li =>
        {
          if (!(li is IFolderItem folderItem2))
            return;
          folderItem2.FolderId = newParentFolderId;
        }));
      else
        item.FolderId = newParentFolderId;
    }

    /// <summary>Gets the folder title path.</summary>
    /// <typeparam name="TRoot">The type of the root.</typeparam>
    /// <param name="manager">The manager.</param>
    /// <param name="folder">The folder.</param>
    /// <returns></returns>
    public static string GetFolderTitlePath<TRoot>(this IFolderManager manager, Folder folder) where TRoot : IContent
    {
      List<string> values = new List<string>();
      Guid rootId = folder.RootId;
      for (; folder != null; folder = folder.Parent)
        values.Add(folder.Title.Value);
      TRoot root = (TRoot) manager.GetItem(typeof (TRoot), rootId);
      values.Add(root.Title.Value);
      values.Reverse();
      return string.Join(" > ", (IEnumerable<string>) values);
    }

    /// <summary>
    /// Validates that the folder URL is unique in its scope or throws an exception otherwise.
    /// </summary>
    public static void ValidateFolderUrl(this IFolderManager manager, Folder folder)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FolderExtensions.\u003C\u003Ec__DisplayClass14_0 cDisplayClass140 = new FolderExtensions.\u003C\u003Ec__DisplayClass14_0()
      {
        folder = folder
      };
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass140.urlName = cDisplayClass140.folder.UrlName.ToLower();
      ParameterExpression parameterExpression;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      if (manager.GetFolders().Any<Folder>(Expression.Lambda<Func<Folder, bool>>((Expression) Expression.AndAlso(f.RootId == cDisplayClass140.folder.RootId && f.ParentId == cDisplayClass140.folder.ParentId && f.Id != cDisplayClass140.folder.Id, (Expression) Expression.Equal((Expression) Expression.Call((Expression) Expression.Call(f.UrlName, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass140, typeof (FolderExtensions.\u003C\u003Ec__DisplayClass14_0)), FieldInfo.GetFieldFromHandle(__fieldref (FolderExtensions.\u003C\u003Ec__DisplayClass14_0.urlName))))), parameterExpression)))
      {
        // ISSUE: reference to a compiler-generated field
        throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(Res.Get<ErrorMessages>().DuplicateUrlException, (object) cDisplayClass140.folder.UrlName.Value), (Exception) null);
      }
    }

    private static IEnumerable<Type> GetFolderItemTypes(IFolderManager manager)
    {
      Type folderItemInterface = typeof (IFolderItem);
      Type type = manager.GetType();
      if (!FolderExtensions.folderItemTypesForManager.ContainsKey(type))
      {
        lock (FolderExtensions.folderItemTypesForManager)
        {
          if (!FolderExtensions.folderItemTypesForManager.ContainsKey(type))
          {
            Type[] array = ((IEnumerable<Type>) manager.Provider.GetKnownTypes()).Where<Type>((Func<Type, bool>) (t => t.ImplementsInterface(folderItemInterface))).ToArray<Type>();
            FolderExtensions.folderItemTypesForManager[type] = (IEnumerable<Type>) array;
          }
        }
      }
      return FolderExtensions.folderItemTypesForManager[type];
    }
  }
}
