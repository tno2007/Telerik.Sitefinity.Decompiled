// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.ServiceUtility
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceModel.Web;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.Web.Services;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.RelatedData.Messages;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Versioning.Web.Services;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// Helper class containing useful methods for services.
  /// This class facilitates the duplicate content feature.
  /// </summary>
  public static class ServiceUtility
  {
    /// <summary>
    /// Makes sure that the current user is logged on. If they are not, an special
    /// exception will be thrown that will be intercepted by ClientManager and they
    /// will be redirected to the login page with proper return url.
    /// </summary>
    /// <exception cref="T:Telerik.Sitefinity.Utilities.MS.ServiceModel.Web.WebProtocolException">
    /// Thrown when the user is not authenticated. Return code and error message are
    /// handled by ClientManager.
    /// </exception>
    public static void RequestAuthentication() => ServiceUtility.RequestAuthentication(false, false);

    /// <summary>
    /// Makes sure that the current user is logged on. If they are not, an special
    /// exception will be thrown that will be intercepted by ClientManager and they
    /// will be redirected to the login page with proper return url.
    /// </summary>
    /// <exception cref="T:Telerik.Sitefinity.Utilities.MS.ServiceModel.Web.WebProtocolException">
    /// Thrown when the user is not authenticated. Return code and error message are
    /// handled by ClientManager.
    /// </exception>
    /// <param name="needAdminRights">if set to <c>true</c> The authenticated user has to have admin rights.</param>
    public static void RequestAuthentication(bool needAdminRights) => ServiceUtility.RequestAuthentication(needAdminRights, false);

    /// <summary>
    /// Makes sure that the current user is logged on. If they are not, an special
    /// exception will be thrown that will be intercepted by ClientManager and they
    /// will be redirected to the login page with proper return url.
    /// </summary>
    /// <exception cref="T:Telerik.Sitefinity.Utilities.MS.ServiceModel.Web.WebProtocolException">
    /// Thrown when the user is not authenticated or authenticated but is not a backend user. Return code and error message are
    /// handled by ClientManager.
    /// </exception>
    public static void RequestBackendUserAuthentication() => ServiceUtility.RequestAuthentication((Func<SitefinityIdentity, bool>) (identity => !identity.IsBackendUser));

    /// <summary>
    /// Makes sure that the current user is logged on. If they are not, an special
    /// exception will be thrown that will be intercepted by ClientManager and they
    /// will be redirected to the login page with proper return url.
    /// </summary>
    /// <param name="demandBackendUser">if set to <c>true</c>the method will deny access for non-backend users</param>
    /// <param name="needAdminRights">if set to <c>true</c>the method will will deny access for non-admin users</param>
    /// <exception cref="T:Telerik.Sitefinity.Utilities.MS.ServiceModel.Web.WebProtocolException">
    /// Thrown when the user is not authenticated. Return code and error message are
    /// handled by ClientManager.
    ///   </exception>
    public static void RequestAuthentication(bool needAdminRights, bool demandBackendUser) => ServiceUtility.RequestAuthentication((Func<SitefinityIdentity, bool>) (identity =>
    {
      if (needAdminRights && !identity.IsUnrestricted)
        return true;
      return demandBackendUser && !identity.IsBackendUser;
    }));

    /// <summary>
    /// Requests user group admin. (Local admin, not global admin)
    /// </summary>
    internal static void RequestAdminAuthentication() => ServiceUtility.RequestAuthentication((Func<SitefinityIdentity, bool>) (identity => !identity.IsAdmin));

    internal static void RequestBackendUserAuthentication(
      string permissionSet,
      params string[] actions)
    {
      ServiceUtility.RequestAuthentication((Func<SitefinityIdentity, bool>) (identity => !identity.IsBackendUser || !AppPermission.Root.IsGranted(permissionSet, actions)));
    }

    /// <summary>
    /// Makes sure that the current user is logged on. If they are not, an special
    /// exception will be thrown that will be intercepted by ClientManager and they
    /// will be redirected to the login page with proper return url.
    /// </summary>
    /// <param name="demandBackendUser">if set to <c>true</c>the method will deny access for non-backend users</param>
    /// <param name="needAdminRights">if set to <c>true</c>the method will will deny access for non-admin users</param>
    /// <exception cref="T:Telerik.Sitefinity.Utilities.MS.ServiceModel.Web.WebProtocolException">
    /// Thrown when the user is not authenticated. Return code and error message are
    /// handled by ClientManager.
    ///   </exception>
    internal static void RequestAuthentication(Func<SitefinityIdentity, bool> isForbidden)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (currentIdentity != null && currentIdentity.IsAuthenticated && !currentIdentity.IsUnrestricted && isForbidden(currentIdentity))
        throw new WebProtocolException(HttpStatusCode.Forbidden, System.Enum.GetName(typeof (UserLoggingReason), (object) UserLoggingReason.NeedAdminRights), (Exception) null);
      if (SystemManager.CurrentHttpContext == null || SystemManager.CurrentHttpContext.User.Identity.IsAuthenticated)
        return;
      AuthenticationMode authenticationMode = SecurityManager.AuthenticationMode;
      switch (authenticationMode)
      {
        case AuthenticationMode.Claims:
          throw new WebProtocolException(HttpStatusCode.Unauthorized);
        case AuthenticationMode.Forms:
          string virtualPath = Config.Get<SecurityConfig>().Permissions["Backend"].AjaxLoginUrl;
          if (VirtualPathUtility.IsAppRelative(virtualPath))
            virtualPath = VirtualPathUtility.ToAbsolute(virtualPath);
          string loggingCookie = SecurityManager.GetLoggingCookie(SystemManager.CurrentHttpContext);
          SecurityConfig securityConfig = Config.Get<SecurityConfig>();
          SecurityManager.DeleteCookie(securityConfig.LoggingCookieName, securityConfig.AuthCookiePath, securityConfig.AuthCookieDomain, securityConfig.AuthCookieRequireSsl);
          SecurityManager.BuildLogoutCookie(loggingCookie == null ? (!virtualPath.EndsWith("/Ajax", StringComparison.InvariantCultureIgnoreCase) ? UserLoggingReason.SessionExpired : UserLoggingReason.UserLoggedOff) : (UserLoggingReason) System.Enum.Parse(typeof (UserLoggingReason), loggingCookie), SystemManager.CurrentHttpContext);
          throw new WebProtocolException(HttpStatusCode.PreconditionFailed, "..::login|session|expired::.." + virtualPath, (Exception) null);
        default:
          throw new ApplicationException(string.Format("Unsupported authentication mode: {0}", (object) authenticationMode));
      }
    }

    public static string EncodeServiceUrl(string url)
    {
      if (string.IsNullOrEmpty(url))
        return url;
      url = HttpUtility.UrlEncode(url);
      url = url.Replace(".", "-dot-");
      return url;
    }

    public static string DecodeServiceUrl(string url)
    {
      if (string.IsNullOrEmpty(url))
        return url;
      url = HttpUtility.UrlDecode(url);
      url = url.Replace("-dot-", ".");
      return url;
    }

    public static void DisableCache()
    {
      if (WebOperationContext.Current == null)
        return;
      OutgoingWebResponseContext outgoingResponse = WebOperationContext.Current.OutgoingResponse;
      foreach (KeyValuePair<string, string> cacheHeader in (IEnumerable<KeyValuePair<string, string>>) ServiceUtility.GetCacheHeaders())
        outgoingResponse.Headers.Add(cacheHeader.Key, cacheHeader.Value);
    }

    internal static IDictionary<string, string> GetCacheHeaders() => (IDictionary<string, string>) new Dictionary<string, string>()
    {
      {
        "Cache-Control",
        "no-cache, no-store, must-revalidate"
      },
      {
        "Pragma",
        "no-cache"
      },
      {
        "Expires",
        "0"
      }
    };

    /// <summary>Converts virtual path to an application absolute path</summary>
    /// <param name="relativeUrl">Application relative url.</param>
    /// <returns>
    /// If <paramref name="relativeUrl" /> is null, empty or whitespace, <paramref name="relativeUrl" /> is returned.
    /// Otherwize, converts <paramref name="relativeUrl" /> to application absolute path.
    /// </returns>
    /// <remarks>
    /// Unlike <see cref="M:System.Web.VirtualPathUtility.ToAbsolute(System.String)" />, this method
    /// does not throw an exception if <paramref name="relativeUrl" /> contains a query string.
    /// </remarks>
    public static string ToAbsolute(string relativeUrl)
    {
      if (relativeUrl.IsNullOrWhitespace())
        return relativeUrl;
      int num = relativeUrl.IndexOf('?');
      if (num == -1)
        return VirtualPathUtility.ToAbsolute(relativeUrl);
      string virtualPath = relativeUrl.Substring(0, num);
      string str = relativeUrl.Substring(num);
      return VirtualPathUtility.ToAbsolute(virtualPath) + str;
    }

    public static CollectionContext<T> HandleCollectionParameters<T>(
      IEnumerable<T> items,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      IQueryable<T> queryable = items.AsQueryable<T>();
      if (!string.IsNullOrEmpty(filter))
        queryable = queryable.Where<T>(filter);
      int num = Queryable.Count<T>(queryable);
      if (!string.IsNullOrEmpty(sortExpression))
        queryable = queryable.OrderBy<T>(sortExpression);
      if (skip > 0)
        queryable = Queryable.Skip<T>(queryable, skip);
      if (take > 0)
        queryable = Queryable.Take<T>(queryable, take);
      return new CollectionContext<T>((IEnumerable<T>) queryable)
      {
        TotalCount = num,
        IsGeneric = true
      };
    }

    /// <summary>
    /// The content editing operations in the services should be forbidded being accessed through front end url.
    /// </summary>
    internal static void ProtectBackendServices(bool allowOdataCall = false)
    {
      if (!SystemManager.CurrentHttpContext.Request.Url.AbsoluteUri.ToLower().Contains("/sitefinity/services") && (!allowOdataCall || !SystemManager.CurrentHttpContext.Request.Url.AbsoluteUri.ToLower().Contains("/sf/system")))
        throw new WebProtocolException(HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Sets up the item context for duplication. This is used when returning an item for duplication from the service.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="context">The item context.</param>
    internal static void SetUpAsDuplicate<T>(ContentItemContext<T> context) where T : class, IContent
    {
      context.LifecycleStatus = (WcfContentLifecycleStatus) null;
      context.LastApprovalTrackingRecord = (WcfApprovalTrackingRecord) null;
      context.AllowMultipleUrls = false;
      context.AdditionalUrlNames = new string[0];
      context.AdditionalUrlsRedirectToDefault = false;
      context.VersionInfo = (WcfChange) null;
      context.Warnings = (IEnumerable<ItemWarning>) new ItemWarning[0];
      if (context.Item is Content content && (context.ItemType == "Telerik.Sitefinity.GenericContent.Model.ContentItem" || context.ItemType == "Telerik.Sitefinity.Lists.Model.ListItem"))
        content.UrlName = (Lstring) string.Empty;
      ServiceUtility.SetUpAsDuplicateInternal<T>((ItemContext<T>) context);
    }

    /// <summary>
    /// Sets up the item context for duplication. This is used when returning an item for duplication from the service.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="context">The item context.</param>
    internal static void SetUpAsDuplicate(DynamicItemContext context)
    {
      context.HasLiveVersion = false;
      context.VersionInfo = (WcfChange) null;
      ServiceUtility.SetUpAsDuplicateInternal<DynamicContent>((ItemContext<DynamicContent>) context);
    }

    /// <summary>
    /// Sets up the item context for duplication. This is used when returning an item for duplication from the service.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="context">The item context.</param>
    private static void SetUpAsDuplicateInternal<T>(ItemContext<T> context) where T : class, IDataItem
    {
      if (context is RelatedDataItemContextBase<T> dataItemContextBase)
      {
        ContentLinksManager linksManager = ContentLinksManager.GetManager();
        Type contentType = context.Item.GetType();
        dataItemContextBase.ChangedRelatedData = TypeDescriptor.GetProperties(contentType).OfType<RelatedDataPropertyDescriptor>().SelectMany<RelatedDataPropertyDescriptor, ContentLink>((Func<RelatedDataPropertyDescriptor, IEnumerable<ContentLink>>) (x => (IEnumerable<ContentLink>) linksManager.GetContentLinks(context.Item.Id, contentType, x.Name))).Where<ContentLink>((Func<ContentLink, bool>) (cl => cl != null)).Select<ContentLink, ContentLinkChange>((Func<ContentLink, ContentLinkChange>) (cl => new ContentLinkChange()
        {
          ChildItemAdditionalInfo = cl.ChildItemAdditionalInfo,
          ChildItemId = cl.ChildItemId,
          ChildItemProviderName = cl.ChildItemProviderName,
          ChildItemType = cl.ChildItemType,
          ComponentPropertyName = cl.ComponentPropertyName,
          Ordinal = new float?(cl.Ordinal),
          IsChildDeleted = cl.IsChildDeleted,
          State = ContentLinkChangeState.Added
        })).ToArray<ContentLinkChange>();
        dataItemContextBase.ItemId = context.Item.Id;
      }
      context.Item = ServiceUtility.SetUpAsDuplicateItemInternal<T>(context.Item);
    }

    /// <summary>
    /// Sets up the item for duplication. This is used when returning an item for duplication from the service.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="item">The item.</param>
    /// <returns>A new item which is set up as a copy of the current item.</returns>
    private static T SetUpAsDuplicateItemInternal<T>(T item) where T : class, IDataItem
    {
      if (item is IApprovalWorkflowItem approvalWorkflowItem && approvalWorkflowItem.ApprovalWorkflowState != (Lstring) null)
      {
        approvalWorkflowItem.ApprovalWorkflowState.Value = string.Empty;
        approvalWorkflowItem.ApprovalWorkflowState.PersistedValue = string.Empty;
      }
      if (item is IContent content)
        content.DateCreated = DateTime.UtcNow;
      if (item is DynamicContent dynamicContent)
        dynamicContent.DateCreated = DateTime.UtcNow;
      if (item is ILocatable locatable)
        locatable.ClearUrls();
      item.LastModified = DateTime.UtcNow;
      T target = ReflectionHelper.ShallowCopyProperties<T>(item, default (T), (Func<PropertyDescriptor, bool>) (p => p.Name == "Id" || p.Name == "OriginalContentId"));
      if (target is IOwnership ownership)
        ownership.Owner = ClaimsManager.GetCurrentUserId();
      return ReflectionHelper.ShallowCopyFields<T>(item, target, (Func<FieldInfo, bool>) (f => f.FieldType.IsAssignableFrom(typeof (Guid))));
    }
  }
}
