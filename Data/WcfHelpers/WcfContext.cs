// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.WcfHelpers.WcfContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data.WcfHelpers
{
  /// <summary>
  /// Retrieves common query string parameters used by Sitefinity's web services
  /// </summary>
  public static class WcfContext
  {
    /// <summary>Query string parameter name for manager type</summary>
    public static readonly string ManagerTypeParamName = "managerType";
    /// <summary>Query string parameter name for provider name</summary>
    public static readonly string ProviderNameParamName = "provider";
    /// <summary>
    /// Alternative query string parameter name for provider name
    /// </summary>
    public static readonly string AlternativeProviderNameParamName = "providerName";
    private static WcfContext.ProvidersContext providersContext = new WcfContext.ProvidersContext();
    private static WcfContext.TypeSubstitutionContext typeSubstitutions = new WcfContext.TypeSubstitutionContext();

    /// <summary>
    /// Retrieves information about type name-provider name mapping
    /// </summary>
    public static WcfContext.ProvidersContext Providers => WcfContext.providersContext;

    /// <summary>Retrieves information about type substitution mapping</summary>
    public static WcfContext.TypeSubstitutionContext TypeSubstitutions => WcfContext.typeSubstitutions;

    /// <summary>
    /// Retrieves the maximum number of items requested by the service
    /// </summary>
    public static int? Take
    {
      get
      {
        int result;
        return SystemManager.CurrentHttpContext != null && int.TryParse(HttpUtility.UrlDecode(SystemManager.CurrentHttpContext.Request.QueryString["take"]), out result) ? new int?(result) : new int?();
      }
    }

    /// <summary>
    /// Returns manager type if provided in query string or returns null on error
    /// </summary>
    public static Type ManagerType
    {
      get
      {
        Type managerType = (Type) null;
        if (SystemManager.CurrentHttpContext != null)
        {
          string name = SystemManager.CurrentHttpContext.Request.QueryString[WcfContext.ManagerTypeParamName];
          if (!name.IsNullOrWhitespace())
            managerType = TypeResolutionService.ResolveType(name, false, true);
        }
        return managerType;
      }
    }

    /// <summary>
    /// Returns provider name if specified in query string as a parameter
    /// </summary>
    public static string ProviderName
    {
      get
      {
        string empty = string.Empty;
        if (SystemManager.CurrentHttpContext != null)
        {
          HttpRequestBase request = SystemManager.CurrentHttpContext.Request;
          empty = request.QueryString[WcfContext.AlternativeProviderNameParamName];
          if (empty.IsNullOrWhitespace())
            empty = request.QueryString[WcfContext.ProviderNameParamName];
        }
        return empty;
      }
    }

    /// <summary>Retrieves the number of items to skip</summary>
    public static int? Skip
    {
      get
      {
        int result;
        return SystemManager.CurrentHttpContext != null && int.TryParse(HttpUtility.UrlDecode(SystemManager.CurrentHttpContext.Request.QueryString["skip"]), out result) ? new int?(result) : new int?();
      }
    }

    /// <summary>
    /// Retrieves any Dynamic-Linq expression used for sorting
    /// </summary>
    public static string Sort => SystemManager.CurrentHttpContext != null ? HttpUtility.UrlDecode(SystemManager.CurrentHttpContext.Request.QueryString["sortExpression"]) : (string) null;

    /// <summary>
    /// Retrieves any Dynamic-Linq expression used for filtering
    /// </summary>
    public static string Filter => SystemManager.CurrentHttpContext != null ? HttpUtility.UrlDecode(SystemManager.CurrentHttpContext.Request.QueryString["filter"]) : (string) null;

    /// <summary>Retrieves the itemType querystring parameter</summary>
    public static Type ItemType
    {
      get
      {
        if (SystemManager.CurrentHttpContext == null)
          return (Type) null;
        string name = HttpUtility.UrlDecode(SystemManager.CurrentHttpContext.Request.QueryString["itemType"]);
        Type itemType = (Type) null;
        if (!string.IsNullOrEmpty(name))
          itemType = TypeResolutionService.ResolveType(name, false);
        return itemType;
      }
    }

    public static string CurrentTransactionName
    {
      get => SystemManager.CurrentHttpContext.Items.Contains((object) "webServiceTransactionName") ? (string) SystemManager.CurrentHttpContext.Items[(object) "webServiceTransactionName"] : (string) null;
      set => SystemManager.CurrentHttpContext.Items[(object) "webServiceTransactionName"] = (object) value;
    }

    /// <summary>
    /// Retrieve a manager for <paramref name="itemTypeName" />.
    /// If type substitution or provider name are in query string, they will be used.
    /// </summary>
    /// <param name="itemTypeName">Full type name of the item type to get a manager for</param>
    /// <returns>Manager instance, or <c>null</c> on error.</returns>
    public static IManager GetManager(string itemTypeName, string tranName) => WcfContext.GetManager(TypeResolutionService.ResolveType(itemTypeName), tranName);

    /// <summary>
    /// Retrieve a manager for <paramref name="itemTypeName" />.
    /// If type substitution or provider name are in query string, they will be used.
    /// </summary>
    /// <param name="itemType">Item type name to get a manager for.</param>
    /// <returns>Manager instance, or <c>null</c> on error.</returns>
    public static IManager GetManager(Type itemType, string tranName)
    {
      string fullName = itemType.FullName;
      string providerName = WcfContext.Providers[fullName];
      if (string.IsNullOrWhiteSpace(providerName))
        providerName = WcfContext.ProviderName;
      Type itemType1 = WcfContext.ItemType;
      if (itemType1 != (Type) null && itemType.IsAssignableFrom(itemType1))
      {
        itemType = itemType1;
      }
      else
      {
        string typeSubstitution = WcfContext.TypeSubstitutions[fullName];
        if (!string.IsNullOrEmpty(typeSubstitution))
        {
          Type c = TypeResolutionService.ResolveType(typeSubstitution, false);
          if (c != (Type) null && itemType.IsAssignableFrom(c))
            itemType = c;
        }
      }
      return tranName.IsNullOrWhitespace() ? ManagerBase.GetMappedManager(itemType, providerName) : ManagerBase.GetMappedManagerInTransaction(itemType, providerName, tranName);
    }

    /// <summary>
    /// Retrieve a CLR type substituion for <paramref name="originalTypeName" />
    /// </summary>
    /// <param name="originalTypeName">Full type name to get a substitution for</param>
    /// <returns>If nothing is specified in query string, or can't resolve the type,
    /// will return <c>null</c>. Otherwize, returns the substitution type.</returns>
    public static Type GetSubstitution(string originalTypeName)
    {
      string typeSubstitution = WcfContext.TypeSubstitutions[originalTypeName];
      return !string.IsNullOrEmpty(typeSubstitution) ? TypeResolutionService.ResolveType(typeSubstitution, false) : (Type) null;
    }

    /// <summary>
    /// Retrieve a CLR type substituion for <paramref name="originalType" />
    /// </summary>
    /// <param name="originalType">Type to get a substitution for</param>
    /// <returns>If nothing is specified in query string, or can't resolve the type,
    /// will return <c>null</c>. Otherwize, returns the substitution type.</returns>
    public static Type GetSubstitution(Type originalType) => WcfContext.GetSubstitution(originalType.FullName);

    /// <summary>
    /// Retrieves information about type substition from the query string
    /// </summary>
    public class TypeSubstitutionContext
    {
      /// <summary>
      /// Retrieve a full type name of a substituion for <paramref name="originalTypeName" />
      /// </summary>
      /// <param name="originalTypeName">Full name of the type to get a substitute for</param>
      /// <returns>Full type name of the substituion if found, or empty string otherwize.</returns>
      public string this[string originalTypeName] => SystemManager.CurrentHttpContext != null && !string.IsNullOrEmpty(originalTypeName) ? HttpUtility.UrlDecode(SystemManager.CurrentHttpContext.Request.QueryString[HttpUtility.UrlEncode(originalTypeName)]) : string.Empty;
    }

    /// <summary>
    /// Retrieve item type name -&gt; provider type name mappings from query string
    /// </summary>
    public class ProvidersContext
    {
      /// <summary>
      /// Retrieve the full provider type name associated with <paramref name="itemTypeName" />
      /// </summary>
      /// <param name="itemTypeName">Full type name of the item type to get a mapping for</param>
      /// <returns>Full type name of the mapped provider or empty string on failure.</returns>
      public string this[string itemTypeName] => SystemManager.CurrentHttpContext != null && !string.IsNullOrEmpty(itemTypeName) ? HttpUtility.UrlDecode(SystemManager.CurrentHttpContext.Request.QueryString[HttpUtility.UrlEncode(itemTypeName + "_providerName")]) : string.Empty;
    }
  }
}
