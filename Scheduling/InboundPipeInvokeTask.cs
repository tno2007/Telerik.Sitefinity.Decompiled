// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.InboundPipeInvokeTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Scheduling
{
  public class InboundPipeInvokeTask : ScheduledTask
  {
    private PipeSettings activePipeSettings;

    public override string TaskName => "PublishingSystemInvokerTask";

    public Guid PipeSettingsId { get; set; }

    internal IEnumerable<PublishingSystemEventInfo> PublishingArguments { get; set; }

    public PipeSettings ActivePipeSettings
    {
      get
      {
        if (this.activePipeSettings == null)
          this.activePipeSettings = PublishingManager.GetManager().GetPipeSettings().Where<PipeSettings>((Expression<Func<PipeSettings, bool>>) (p => p.Id == this.PipeSettingsId && p.IsActive && p.PublishingPoint.IsActive)).FirstOrDefault<PipeSettings>();
        return this.activePipeSettings;
      }
    }

    public new Guid SiteId { get; set; }

    /// <inheritdoc />
    public override void ExecuteTask() => SystemManager.RunWithElevatedPrivilege(new SystemManager.RunWithElevatedPrivilegeDelegate(this.ExecuteTaskInSite), (object[]) null);

    /// <inheritdoc />
    public override void SetCustomData(string customData) => SystemManager.RunWithElevatedPrivilege(new SystemManager.RunWithElevatedPrivilegeDelegate(this.LoadData), new object[1]
    {
      (object) customData
    });

    /// <inheritdoc />
    public override string GetCustomData()
    {
      XElement xelement = new XElement((XName) "TaskData");
      XElement content1 = new XElement((XName) "PipeInfo");
      content1.SetAttributeValue((XName) "pipeId", (object) this.PipeSettingsId);
      content1.SetAttributeValue((XName) "siteId", (object) this.SiteId);
      xelement.Add((object) content1);
      if (this.PublishingArguments != null)
      {
        foreach (PublishingSystemEventInfo publishingArgument in this.PublishingArguments)
        {
          XElement content2 = new XElement((XName) "HandleItemAction");
          content2.SetAttributeValue((XName) "itemId", (object) this.GetItemId(publishingArgument.Item));
          content2.SetAttributeValue((XName) "itemType", (object) publishingArgument.ItemType);
          content2.SetAttributeValue((XName) "itemProvider", (object) this.GetItemProvider(publishingArgument.Item));
          content2.SetAttributeValue((XName) "action", (object) publishingArgument.ItemAction);
          content2.SetAttributeValue((XName) "Language", (object) publishingArgument.Language);
          xelement.Add((object) content2);
        }
      }
      return xelement.ToString();
    }

    private void ExecuteTask(object[] parameters)
    {
      if (this.PipeSettingsId != Guid.Empty)
        this.ToPublishingPointAndReschedule();
      else
        PublishingManager.CallSubscribedPipes(this.PublishingArguments);
    }

    private void ExecuteTaskInSite(object[] parameters)
    {
      ISite site = (ISite) null;
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null && this.SiteId != Guid.Empty)
        site = multisiteContext.GetSiteById(this.SiteId);
      if (site != null)
      {
        using (new SiteRegion(site))
          this.ExecuteTask(parameters);
      }
      else
        this.ExecuteTask(parameters);
    }

    private void ToPublishingPointAndReschedule()
    {
      if (this.ActivePipeSettings == null)
        return;
      try
      {
        this.ToPublishingPoint();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        PublishingManager.ReschedulePublishingPointPipes(this.ActivePipeSettings.PublishingPoint, this.TransactionName);
        TransactionManager.CommitTransaction(this.TransactionName);
        SchedulingManager.RescheduleNextRun();
      }
    }

    private void ToPublishingPoint()
    {
      IPipe pipe = PublishingSystemFactory.GetPipe(this.ActivePipeSettings);
      if (!(pipe is IInboundPipe))
        return;
      (pipe as IInboundPipe).ToPublishingPoint();
    }

    private void LoadData(object[] parameters)
    {
      IEnumerable<XElement> xelements = XDocument.Parse((string) parameters[0]).Element((XName) "TaskData").Elements();
      List<PublishingSystemEventInfo> publishingSystemEventInfoList = new List<PublishingSystemEventInfo>();
      foreach (XElement element in xelements)
      {
        if (element.Name.LocalName == "PipeInfo")
        {
          string elementValue1 = this.GetElementValue(element, "pipeId");
          if (!string.IsNullOrEmpty(elementValue1))
            this.PipeSettingsId = Guid.Parse(elementValue1);
          string elementValue2 = this.GetElementValue(element, "siteId");
          if (!string.IsNullOrEmpty(elementValue2))
            this.SiteId = Guid.Parse(elementValue2);
        }
        else
        {
          string elementValue3 = this.GetElementValue(element, "itemType");
          Guid id = Guid.Parse(this.GetElementValue(element, "itemId"));
          string elementValue4 = this.GetElementValue(element, "itemProvider");
          string elementValue5 = this.GetElementValue(element, "action");
          string elementValue6 = this.GetElementValue(element, "Language");
          object obj;
          if (elementValue5 == "SystemObjectDeleted")
          {
            WrapperObject wrapperObject = new WrapperObject((object) null);
            wrapperObject.AddProperty("Id", (object) id);
            wrapperObject.AddProperty("ItemId", (object) id);
            wrapperObject.AddProperty("OriginalItemId", (object) id);
            wrapperObject.AddProperty("OriginalContentId", (object) id);
            wrapperObject.AddProperty("ProviderName", (object) elementValue4);
            obj = (object) wrapperObject;
          }
          else
            obj = ManagerBase.GetMappedManager(elementValue3, elementValue4).GetItem(TypeResolutionService.ResolveType(elementValue3), id);
          publishingSystemEventInfoList.Add(new PublishingSystemEventInfo()
          {
            Item = obj,
            ItemAction = elementValue5,
            ItemType = elementValue3,
            Language = elementValue6
          });
        }
      }
      this.PublishingArguments = (IEnumerable<PublishingSystemEventInfo>) publishingSystemEventInfoList;
    }

    private string GetElementValue(XElement element, string attibuteName)
    {
      XAttribute xattribute = element.Attribute(XName.Get(attibuteName));
      return xattribute == null ? string.Empty : xattribute.Value;
    }

    private Guid GetItemId(object item)
    {
      switch (item)
      {
        case IDataItem dataItem:
          return dataItem.Id;
        case WrapperObject wrapperObject:
          return wrapperObject.GetPropertyOrDefault<Guid>("Id");
        default:
          return Guid.Empty;
      }
    }

    private string GetItemProvider(object item)
    {
      switch (item)
      {
        case IDataItem dataItem:
          return dataItem.GetProviderName();
        case WrapperObject wrapperObject:
          return wrapperObject.GetPropertyOrDefault<string>("ProviderName");
        default:
          return string.Empty;
      }
    }
  }
}
