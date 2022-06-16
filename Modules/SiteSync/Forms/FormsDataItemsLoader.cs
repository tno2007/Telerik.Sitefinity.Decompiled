// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.FormsDataItemsLoader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.SiteSync
{
  internal class FormsDataItemsLoader : PagesDataItemsLoader
  {
    public override IList<object> LoadDataItem(
      object item,
      string language,
      out Dictionary<Guid, ObjectData> controlDependency)
    {
      List<object> controlPropertyCollection = new List<object>();
      controlDependency = new Dictionary<Guid, ObjectData>();
      switch (item)
      {
        case FormDescription _:
          using (IEnumerator<FormControl> enumerator = ((FormDescription) item).Controls.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              FormControl current = enumerator.Current;
              controlPropertyCollection.Add((object) current);
              this.GetControlProperties((ObjectData) current, (IList<object>) controlPropertyCollection, controlDependency);
            }
            break;
          }
        case FormDraft _:
          using (IEnumerator<FormDraftControl> enumerator = ((FormDraft) item).Controls.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              FormDraftControl current = enumerator.Current;
              controlPropertyCollection.Add((object) current);
              this.GetControlProperties((ObjectData) current, (IList<object>) controlPropertyCollection, controlDependency);
            }
            break;
          }
      }
      return (IList<object>) controlPropertyCollection;
    }

    public virtual void SetFormProperties(
      Type itemType,
      string providerName,
      WrapperObject item,
      object dataItem)
    {
      FormDescription form = dataItem as FormDescription;
      if (form != null)
      {
        List<Guid> list1 = FormsManager.GetManager().GetSiteFormLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == form.Id)).Select<SiteItemLink, Guid>((Expression<Func<SiteItemLink, Guid>>) (l => l.SiteId)).ToList<Guid>();
        item.AddProperty("SiteIds", (object) list1);
        if (SystemManager.IsModuleEnabled("Synchronization"))
        {
          ServiceContext serviceContext = FormsModule.GetServiceContext();
          INotificationService notificationService = SystemManager.GetNotificationService();
          Guid subscriptionListId = form.SubscriptionListId;
          ConfigSection configSection = Config.GetConfigSection("SiteSyncConfig");
          object obj = configSection.GetType().GetProperty("Forms").GetValue((object) configSection, (object[]) null);
          if ((bool) obj.GetType().GetProperty("SyncSubscribersList").GetValue(obj, (object[]) null) && subscriptionListId != Guid.Empty)
          {
            List<string> list2 = new HashSet<string>(notificationService.GetSubscribers(serviceContext, subscriptionListId, (QueryParameters) null).Where<ISubscriberResponse>((Func<ISubscriberResponse, bool>) (s => s.Email.ToLower() == s.ResolveKey.ToLower())).Select<ISubscriberResponse, string>((Func<ISubscriberResponse, string>) (s => s.Email)), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).ToList<string>();
            item.AddProperty("SubscribersEmails", (object) list2);
          }
        }
      }
      if (!(dataItem is FormDraft formDraft))
        return;
      Guid id = formDraft.ParentForm.Id;
      item.AddProperty("ParentId", (object) id);
    }

    public override void SetControlPropertiesData(
      Type itemType,
      string providerName,
      WrapperObject item,
      object dataItem)
    {
      if (typeof (FormDraftControl).IsAssignableFrom(itemType))
      {
        FormDraftControl formDraftControl = (FormDraftControl) dataItem;
        if (formDraftControl.Form == null)
          return;
        item.AdditionalProperties.Add("ParentControlPropertyId", (object) formDraftControl.Form.Id);
      }
      else if (typeof (FormControl).IsAssignableFrom(itemType))
      {
        FormControl controlData = (FormControl) dataItem;
        if (controlData.Form != null)
          item.AdditionalProperties.Add("ParentControlPropertyId", (object) controlData.Form.Id);
        Control control = FormsManager.GetManager().LoadControl((ObjectData) controlData, (CultureInfo) null);
        if (!(ObjectFactory.Resolve<IControlBehaviorResolver>().GetBehaviorObject(control) is IFormFieldControl behaviorObject))
          return;
        string fieldName = behaviorObject.MetaField.FieldName;
        item.AdditionalProperties.Add("ActualMetaFieldName", (object) fieldName);
      }
      else
        base.SetControlPropertiesData(itemType, providerName, item, dataItem);
    }
  }
}
