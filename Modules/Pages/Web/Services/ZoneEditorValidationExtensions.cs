// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.ZoneEditorValidationExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>
  /// Extension class that provides the functionality for checking the locking of the content
  /// </summary>
  public static class ZoneEditorValidationExtensions
  {
    internal const string OwnerHasChangedErrorMessage = "The owner of this page has changed and your changes cannot be saved.";
    internal const string PageHasBeenUnlockedErrorMessage = "This page has been unlocked by another user and your changes cannot be saved.";
    internal const string ControlNotFoundErrorMessage = "The control was not found. If you are editing a page it might have been unlocked by another user and your changes cannot be saved.";

    /// <summary>
    /// Checks if the content in webservice args is locked by another user
    /// </summary>
    public static ItemState IsLocked(
      this ZoneEditorWebServiceArgs state,
      out Guid lockedById)
    {
      if (state == null)
        throw new ArgumentNullException();
      return state.PageId.IsLocked(state.MediaType, out lockedById);
    }

    /// <summary>
    /// Checks if the content in webservice args is locked by another user
    /// </summary>
    public static ItemState IsLocked(
      this Guid pageId,
      DesignMediaType mediaType,
      out Guid lockedById)
    {
      lockedById = Guid.Empty;
      try
      {
        switch (mediaType)
        {
          case DesignMediaType.Page:
          case DesignMediaType.NewsletterCampaign:
          case DesignMediaType.NewsletterTemplate:
            PageManager manager1 = PageManager.GetManager();
            if (manager1.GetTemplates().FirstOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == pageId)) != null)
              return ItemState.Ready;
            PageDraft draft1 = manager1.GetDraft<PageDraft>(pageId);
            lockedById = draft1.ParentPage.LockedBy;
            break;
          case DesignMediaType.Template:
            PageManager manager2 = PageManager.GetManager();
            if (manager2.GetTemplates().FirstOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == pageId)) != null)
              return ItemState.Ready;
            TemplateDraft draft2 = manager2.GetDraft<TemplateDraft>(pageId);
            lockedById = draft2.ParentTemplate.LockedBy;
            break;
          case DesignMediaType.Form:
            FormDraft draft3 = FormsManager.GetManager().GetDraft(pageId);
            lockedById = draft3.ParentForm.LockedBy;
            break;
        }
      }
      catch (NoSuchObjectException ex)
      {
        return ItemState.Deleted;
      }
      catch (ItemNotFoundException ex)
      {
        return ItemState.Deleted;
      }
      return ZoneEditorValidationExtensions.IsLockedByOtherUser(lockedById) ? ItemState.Locked : ItemState.Ready;
    }

    /// <summary>Checks if the live version of the item is locked</summary>
    public static ItemState IsLiveLocked(
      this Guid pageId,
      DesignMediaType mediaType,
      out Guid lockedById)
    {
      lockedById = Guid.Empty;
      try
      {
        switch (mediaType)
        {
          case DesignMediaType.Page:
          case DesignMediaType.NewsletterCampaign:
          case DesignMediaType.NewsletterTemplate:
            PageData pageData = PageManager.GetManager().GetPageData(pageId);
            lockedById = pageData.LockedBy;
            break;
          case DesignMediaType.Template:
            PageTemplate template = PageManager.GetManager().GetTemplate(pageId);
            lockedById = template.LockedBy;
            break;
          case DesignMediaType.Form:
            FormDescription form = FormsManager.GetManager().GetForm(pageId);
            lockedById = form.LockedBy;
            break;
        }
      }
      catch (NoSuchObjectException ex)
      {
        return ItemState.Deleted;
      }
      catch (ItemNotFoundException ex)
      {
        return ItemState.Deleted;
      }
      return ZoneEditorValidationExtensions.IsLockedByOtherUser(lockedById) ? ItemState.Locked : ItemState.Ready;
    }

    public static void ThrowExceptionIfLocked(
      this Guid pageId,
      DesignMediaType mediaType,
      string operationName,
      bool checkLive = false)
    {
      Guid lockedById;
      ItemState itemState = !checkLive ? pageId.IsLocked(mediaType, out lockedById) : pageId.IsLiveLocked(mediaType, out lockedById);
      if (itemState != ItemState.Ready)
        throw new ContentLockedException(itemState, lockedById, ZoneEditorValidationExtensions.HtmlSanitize(operationName));
    }

    public static void ThrowExceptionIfLocked(
      this ZoneEditorWebServiceArgs state,
      string operationName)
    {
      Guid lockedById;
      ItemState itemState = state.IsLocked(out lockedById);
      if (itemState != ItemState.Ready)
        throw new ContentLockedException(itemState, lockedById, ZoneEditorValidationExtensions.HtmlSanitize(operationName));
    }

    internal static void ValidateChange(
      Guid pageId,
      DesignMediaType mediaType,
      string operationName,
      bool checkLive = false,
      bool validateChange = true)
    {
      pageId.ThrowExceptionIfLocked(mediaType, operationName, checkLive);
      if (!validateChange || mediaType != DesignMediaType.Page || !ZoneEditorValidationExtensions.ShouldValidateOperation(operationName))
        return;
      PageManager manager = PageManager.GetManager();
      PageData pageData;
      if (checkLive)
        pageData = manager.GetPageData(pageId);
      else
        pageData = manager.GetDrafts<PageDraft>().FirstOrDefault<PageDraft>((Expression<Func<PageDraft, bool>>) (x => x.Id == pageId)).ParentPage;
      if (PageManager.IsPageDataOwnerChanged(pageData))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "The owner of this page has changed and your changes cannot be saved.", (Exception) null);
      if (!PageManager.IsPageDataStillLocked(pageData))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "This page has been unlocked by another user and your changes cannot be saved.", (Exception) null);
    }

    public static void ValidateChange(this ZoneEditorWebServiceArgs state, string operationName)
    {
      state.ThrowExceptionIfLocked(operationName);
      if (state.MediaType != DesignMediaType.Page || !ZoneEditorValidationExtensions.ShouldValidateOperation(operationName))
        return;
      PageData pageData = PageManager.GetManager().GetPageNode(state.PageNodeId).GetPageData();
      if (PageManager.IsPageDataOwnerChanged(pageData))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "The owner of this page has changed and your changes cannot be saved.", (Exception) null);
      if (!PageManager.IsPageDataStillLocked(pageData))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "This page has been unlocked by another user and your changes cannot be saved.", (Exception) null);
    }

    private static string HtmlSanitize(string input)
    {
      input = ObjectFactory.Resolve<IHtmlSanitizer>().Sanitize(input);
      return input;
    }

    /// <summary>Checks if the items is locked by another user</summary>
    /// <param name="lockedById"></param>
    private static bool IsLockedByOtherUser(Guid lockedById) => !(lockedById == Guid.Empty) && lockedById != SecurityManager.GetCurrentUserId();

    private static bool ShouldValidateOperation(string operationName) => ((IEnumerable<string>) new string[2]
    {
      "UpdateControlState",
      "SaveProperties"
    }).Contains<string>(operationName);
  }
}
