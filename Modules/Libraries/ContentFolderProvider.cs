// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.ContentFolderProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>
  /// Libraries and folders provider for specified content type.
  /// </summary>
  internal class ContentFolderProvider
  {
    private const int DefaultSkip = 0;
    private const int DefaultTake = 20;
    private const string EmptySearch = "";
    private readonly LibrariesManager manager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.ContentFolderProvider" /> class.
    /// </summary>
    /// <param name="manager">The libraries manager.</param>
    public ContentFolderProvider(LibrariesManager manager) => this.manager = manager;

    /// <summary>Get a collection of libraries and folders.</summary>
    /// <param name="contentType"> Folder content type.</param>
    /// <param name="totalCount">Total folders and libraries count.</param>
    /// <param name="parentId"> The ID of the parent folder or library.</param>
    /// <param name="folderName">Folder and library name to search for.</param>
    /// <param name="skip">Folders and libraries to skip.</param>
    /// <param name="take">Folders and libraries to take.</param>
    /// <returns>All libraries and folders as a collection of<see cref="T:Telerik.Sitefinity.IFolder" />.</returns>
    public IEnumerable<IFolder> GetFolders(
      Type contentType,
      out int totalCount,
      Guid? parentId = null,
      string folderName = "",
      int skip = 0,
      int take = 20)
    {
      ContentFolderProvider.GuardType<IFolder>(contentType);
      Guid? nullable = parentId;
      Guid empty = Guid.Empty;
      return (nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == empty ? 1 : 0) : 1) : 0) != 0 ? this.GetAllFolders(contentType, out totalCount, folderName, skip, take) : this.GetChildFolders(contentType, parentId, out totalCount, folderName, skip, take);
    }

    /// <summary>Get a collection of ordered folders.</summary>
    /// <param name="contentType">Folder content type.</param>
    /// <param name="orderExpression">Specifies how folders will be ordered.</param>
    /// <param name="folderName">Folder and library name to search for.</param>
    /// <param name="take">Folders and libraries to take.</param>
    /// <returns>All libraries and folders as an ordered collection of <see cref="T:Telerik.Sitefinity.IFolder" />.</returns>
    public IEnumerable<IFolder> GetOrderedFolders(
      Type contentType,
      string orderExpression,
      string folderName = "",
      int take = 20)
    {
      ContentFolderProvider.GuardType<IFolder>(contentType);
      IQueryable<IFolder> source1 = this.GetLibraries(contentType);
      HashSet<Guid> rootLibrariesIds = new HashSet<Guid>((IEnumerable<Guid>) source1.Select<IFolder, Guid>((Expression<Func<IFolder, Guid>>) (x => x.Id)));
      IQueryable<Folder> source2 = this.manager.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => rootLibrariesIds.Contains(f.RootId)));
      if (!string.IsNullOrEmpty(folderName))
      {
        source1 = source1.Where<IFolder>((Expression<Func<IFolder, bool>>) (l => l.Title.StartsWith(folderName) || l.Title.Contains(string.Format(" {0}", folderName))));
        source2 = source2.Where<Folder>((Expression<Func<Folder, bool>>) (f => f.Title.StartsWith(folderName) || f.Title.Contains(string.Format(" {0}", folderName))));
      }
      ContentFolderProvider.OrderExpression expression = new ContentFolderProvider.OrderExpression(orderExpression);
      IQueryable<IFolder> source1_1 = ContentFolderProvider.OrderByExpression<IFolder>(source1, expression).Take<IFolder>(take);
      IQueryable<Folder> source2_1 = ContentFolderProvider.OrderByExpression<Folder>(source2, expression).Take<Folder>(take);
      int num = expression.IsDescending ? 1 : 0;
      return (IEnumerable<IFolder>) ContentFolderProvider.OrderByExpression<IFolder>(source1_1.Concat<IFolder>((IEnumerable<IFolder>) source2_1), expression).Take<IFolder>(take);
    }

    private static IQueryable<T> OrderByExpression<T>(
      IQueryable<T> source,
      ContentFolderProvider.OrderExpression expression)
      where T : IFolder
    {
      Type type = typeof (T);
      string propertyName = expression.PropertyName;
      bool isDescending = expression.IsDescending;
      PropertyInfo property = type.GetProperty(propertyName);
      if (property == (PropertyInfo) null)
        throw new ArgumentException(propertyName + "is invalid for the current type", nameof (expression));
      ParameterExpression parameterExpression = Expression.Parameter(type, "type");
      LambdaExpression lambdaExpression = Expression.Lambda((Expression) Expression.MakeMemberAccess((Expression) parameterExpression, (MemberInfo) property), parameterExpression);
      MethodCallExpression methodCallExpression = Expression.Call(typeof (Queryable), isDescending ? "OrderByDescending" : "OrderBy", new Type[2]
      {
        type,
        property.PropertyType
      }, source.Expression, (Expression) Expression.Quote((Expression) lambdaExpression));
      return source.Provider.CreateQuery<T>((Expression) methodCallExpression);
    }

    private static void GuardType<T>(Type type)
    {
      if (!typeof (T).IsAssignableFrom(type))
        throw new ArgumentException("The given type should implement" + typeof (T).FullName, type.FullName);
    }

    /// <summary>Get a collection of libraries and folders</summary>
    /// <param name="contentType">Folder content type.</param>
    /// <param name="totalCount">Total folders and libraries count.</param>
    /// <param name="folderName">Folder and library name to search for.</param>
    /// <param name="skip">Folders and libraries to skip.</param>
    /// <param name="take">Folders and libraries to take.</param>
    /// <returns>All libraries and folders as a collection of <see cref="T:Telerik.Sitefinity.IFolder" />.</returns>
    private IEnumerable<IFolder> GetAllFolders(
      Type contentType,
      out int totalCount,
      string folderName = "",
      int skip = 0,
      int take = 20)
    {
      ContentFolderProvider.GuardType<IFolder>(contentType);
      List<IFolder> source1 = new List<IFolder>();
      IQueryable<IFolder> libraries = this.GetLibraries(contentType);
      HashSet<Guid> rootLibrariesIds = new HashSet<Guid>((IEnumerable<Guid>) libraries.Select<IFolder, Guid>((Expression<Func<IFolder, Guid>>) (x => x.Id)));
      totalCount = libraries.Count<IFolder>();
      IQueryable<IFolder> source2 = libraries.Where<IFolder>((Expression<Func<IFolder, bool>>) (l => l.Title.StartsWith(folderName) || l.Title.Contains(string.Format(" {0}", folderName))));
      int num = source2.Count<IFolder>();
      List<IFolder> list = source2.Skip<IFolder>(skip).Take<IFolder>(take).ToList<IFolder>();
      int count1 = take - list.Count;
      if (count1 > 0)
      {
        IQueryable<Folder> source3 = this.manager.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => rootLibrariesIds.Contains(f.RootId)));
        totalCount += source3.Count<Folder>();
        IQueryable<Folder> source4 = source3.Where<Folder>((Expression<Func<Folder, bool>>) (f => f.Title.StartsWith(folderName) || f.Title.Contains(string.Format(" {0}", folderName))));
        int count2 = skip - num;
        if (count2 > 0)
          source4 = source4.Skip<Folder>(count2);
        IQueryable<Folder> second = source4.Take<Folder>(count1);
        list = list.Concat<IFolder>((IEnumerable<IFolder>) second).ToList<IFolder>();
      }
      list.OrderBy<IFolder, Lstring>((Func<IFolder, Lstring>) (f => f.Title));
      source1.AddRange((IEnumerable<IFolder>) list);
      return (IEnumerable<IFolder>) source1.OrderBy<IFolder, Lstring>((Func<IFolder, Lstring>) (x => x.Title));
    }

    /// <summary>Get a queryable of child folders.</summary>
    /// <param name="contentType">Folder content type.</param>
    /// <param name="parentId">The ID of the parent folder or library.</param>
    /// <returns>Child folders as a queryable of <see cref="T:Telerik.Sitefinity.IFolder" />.</returns>
    private IQueryable<IFolder> GetChildFolders(Type contentType, Guid? parentId)
    {
      ContentFolderProvider.GuardType<IFolder>(contentType);
      IQueryable<IFolder> libraries = this.GetLibraries(contentType);
      HashSet<Guid> rootLibrariesIds = new HashSet<Guid>((IEnumerable<Guid>) libraries.Select<IFolder, Guid>((Expression<Func<IFolder, Guid>>) (x => x.Id)));
      if (!parentId.HasValue)
        return libraries;
      return (IQueryable<IFolder>) Queryable.OfType<Folder>(this.manager.GetChildFolders(this.manager.FindFolderById(parentId.Value))).Where<Folder>((Expression<Func<Folder, bool>>) (f => rootLibrariesIds.Contains(f.RootId)));
    }

    /// <summary>Get a collection of child folders.</summary>
    /// <param name="contentType"> Folder content type.</param>
    /// <param name="parentId"> The ID of the parent folder or library.</param>
    /// <param name="totalCount">Total folders and libraries count.</param>
    /// <param name="folderName">Folder and library name to search for.</param>
    /// <param name="skip">Folders and libraries to skip.</param>
    /// <param name="take">Folders and libraries to take.</param>
    /// <returns>All child folders as a collection of<see cref="T:Telerik.Sitefinity.IFolder" />.</returns>
    private IEnumerable<IFolder> GetChildFolders(
      Type contentType,
      Guid? parentId,
      out int totalCount,
      string folderName = "",
      int skip = 0,
      int take = 20)
    {
      IQueryable<IFolder> childFolders = this.GetChildFolders(contentType, parentId);
      totalCount = childFolders.Count<IFolder>();
      return (IEnumerable<IFolder>) childFolders.Where<IFolder>((Expression<Func<IFolder, bool>>) (x => x.Title.StartsWith(folderName) || x.Title.Contains(string.Format(" {0}", folderName)))).Skip<IFolder>(skip).Take<IFolder>(take).OrderBy<IFolder, Lstring>((Expression<Func<IFolder, Lstring>>) (x => x.Title)).ToList<IFolder>();
    }

    private IQueryable<IFolder> GetLibraries(Type contentType) => this.manager.GetItems(contentType, (string) null, (string) null, 0, 0).OfType<IFolder>().AsQueryable<IFolder>();

    private struct OrderExpression
    {
      public OrderExpression(string expression)
      {
        string[] strArray = expression.Split(' ');
        this.PropertyName = strArray.Length == 2 ? strArray[0] : throw new ArgumentException(nameof (expression));
        this.IsDescending = strArray[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase);
      }

      public string PropertyName { get; set; }

      public bool IsDescending { get; set; }
    }
  }
}
