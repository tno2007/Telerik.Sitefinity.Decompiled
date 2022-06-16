// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OpenAccessExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Data;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Exceptions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.SitefinityExceptions;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Provided extension methods for OpenAccess related classes.
  /// </summary>
  public static class OpenAccessExtensions
  {
    private const string itemNotFoundExceptionMessageFormat = "You are trying to access {0} item with id {1} that no longer exists. The most probable reason is that it has been deleted by another user.";

    /// <summary>Gets the current object scope.</summary>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public static SitefinityOAContext GetContext(
      this IOpenAccessDataProvider provider)
    {
      return ((DataProviderBase) provider).GetTransaction() as SitefinityOAContext;
    }

    internal static IPersistentTypeDescriptor GetDynamicTypeDescriptor(
      this IOpenAccessDataProvider provider,
      Type itemType)
    {
      return provider.GetDynamicTypeDescriptor(itemType.FullName);
    }

    internal static IPersistentTypeDescriptor GetDynamicTypeDescriptor(
      this IOpenAccessDataProvider provider,
      string itemTypeName)
    {
      return provider.GetContext().PersistentMetaData.GetPersistentTypeDescriptor(itemTypeName) ?? throw new ArgumentNullException("No persistent type with name " + itemTypeName);
    }

    /// <summary>Gets the item by id.</summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="scope">The scope.</param>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public static TItem GetItemById<TItem>(this SitefinityOAContext oaContext, string id)
    {
      try
      {
        return oaContext.GetObjectById<TItem>(Database.OID.ParseObjectId(typeof (TItem), id));
      }
      catch (NoSuchObjectException ex)
      {
        throw new ItemNotFoundException("You are trying to access {0} item with id {1} that no longer exists. The most probable reason is that it has been deleted by another user.".Arrange((object) typeof (TItem).Name, (object) id));
      }
    }

    /// <summary>Gets an item by the specified id.
    /// A return value indicates whether the conversion succeeded.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="scope">The scope.</param>
    /// <param name="id">The id.</param>
    /// <param name="result">The result.</param>
    /// <returns></returns>
    public static bool TryGetItemById<TItem>(
      this SitefinityOAContext oaContext,
      string id,
      out TItem result)
    {
      result = default (TItem);
      try
      {
        result = oaContext.GetObjectById<TItem>(Database.OID.ParseObjectId(typeof (TItem), id));
        return true;
      }
      catch (NoSuchObjectException ex)
      {
      }
      return false;
    }

    /// <summary>Gets the item by id.</summary>
    /// <param name="scope">The scope.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public static object GetItemById(this SitefinityOAContext oaContext, Type itemType, string id)
    {
      try
      {
        return oaContext.GetObjectById(Database.OID.ParseObjectId(itemType, id));
      }
      catch (NoSuchObjectException ex)
      {
        throw new ItemNotFoundException("You are trying to access {0} item with id {1} that no longer exists. The most probable reason is that it has been deleted by another user.".Arrange((object) itemType.Name, (object) id));
      }
    }

    internal static void ExecuteNonQuery(this IDbCommand cmd, string commandText)
    {
      cmd.CommandText = commandText;
      cmd.ExecuteNonQuery();
    }
  }
}
