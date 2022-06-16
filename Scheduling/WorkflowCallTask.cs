// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.WorkflowCallTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Xml.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Scheduling
{
  /// <summary>
  /// a scheduler task for calling a content workflow with specific operation on a specific date/period
  ///  </summary>
  public class WorkflowCallTask : ScheduledTask
  {
    private string contentTypeName;
    private string currentCulture;
    private string currentUiCulture;
    internal const string WorkflowCallTaskName = "WorkflowCallTask";

    public override string TaskName => nameof (WorkflowCallTask);

    /// <summary>The user id.</summary>
    public string UserId { get; set; }

    /// <summary>Gets or sets the name of the operation.</summary>
    /// <value>The name of the operation.</value>
    public string OperationName { get; set; }

    /// <summary>Gets or sets the type of the content.</summary>
    /// <value>The type of the content.</value>
    public Type ContentType
    {
      get
      {
        if (this.contentTypeName.IsNullOrEmpty())
          return (Type) null;
        return SystemManager.TypeRegistry.IsRegistered(this.contentTypeName) ? TypeResolutionService.ResolveType(this.contentTypeName) : (Type) null;
      }
      set
      {
        if (value == (Type) null)
          this.contentTypeName = (string) null;
        else
          this.contentTypeName = value.FullName;
      }
    }

    /// <summary>Gets or sets the content item master id.</summary>
    /// <value>The content item master id.</value>
    public Guid ContentItemMasterId { get; set; }

    /// <summary>Gets or sets the name of the content provider.</summary>
    /// <value>The name of the content provider.</value>
    public string ContentProviderName { get; set; }

    /// <summary>Gets or sets the workflow context bag.</summary>
    /// <value>The workflow context bag.</value>
    public Dictionary<string, string> WorkflowContextBag { get; set; }

    /// <summary>Gets or sets the current UI culture.</summary>
    /// <value>The current UI culture.</value>
    public string CurrentUiCulture
    {
      get => string.IsNullOrEmpty(this.currentUiCulture) ? SystemManager.CurrentContext.Culture.Name : this.currentUiCulture;
      set => this.currentUiCulture = value;
    }

    /// <summary>Gets or sets the current culture.</summary>
    /// <value>The current culture.</value>
    public string CurrentCulture
    {
      get => string.IsNullOrEmpty(this.currentCulture) ? Thread.CurrentThread.CurrentCulture.Name : this.currentCulture;
      set => this.currentCulture = value;
    }

    private int RetriedCount { get; set; }

    public override string GetCustomData()
    {
      XElement xelement = new XElement((XName) "TaskData");
      xelement.SetAttributeValue((XName) "ContentItemMasterId", (object) this.ContentItemMasterId);
      xelement.SetAttributeValue((XName) "ContentProviderName", (object) this.ContentProviderName);
      xelement.SetAttributeValue((XName) "ContentType", (object) this.ContentType.FullName);
      xelement.SetAttributeValue((XName) "OperationName", (object) this.OperationName);
      xelement.SetAttributeValue((XName) "CurrentUiCulture", (object) this.CurrentUiCulture);
      xelement.SetAttributeValue((XName) "CurrentCulture", (object) this.CurrentCulture);
      xelement.SetAttributeValue((XName) "RetriedCount", (object) this.RetriedCount);
      xelement.SetAttributeValue((XName) "UserId", (object) this.UserId);
      return xelement.ToString();
    }

    public override void SetCustomData(string customData)
    {
      XElement element = XDocument.Parse(customData).Element((XName) "TaskData");
      Guid result1;
      Guid.TryParse(this.GetElementValue(element, "ContentItemMasterId"), out result1);
      this.ContentItemMasterId = result1;
      this.ContentProviderName = this.GetElementValue(element, "ContentProviderName");
      this.OperationName = this.GetElementValue(element, "OperationName");
      this.contentTypeName = this.GetElementValue(element, "ContentType");
      this.ContentProviderName = this.GetElementValue(element, "ContentProviderName");
      this.CurrentUiCulture = this.GetElementValue(element, "CurrentUiCulture");
      this.CurrentCulture = this.GetElementValue(element, "CurrentCulture");
      this.UserId = this.GetElementValue(element, "UserId");
      Guid result2;
      if (Guid.TryParse(this.GetElementValue(element, "SiteId"), out result2))
        this.SiteId = result2;
      XAttribute xattribute = element.Attribute(XName.Get("RetriedCount"));
      this.RetriedCount = xattribute != null ? int.Parse(xattribute.Value) : 0;
    }

    /// <summary>
    /// Builds a unique key for the task based on the parameters.
    /// </summary>
    /// <returns></returns>
    public override string BuildUniqueKey()
    {
      if (string.IsNullOrEmpty(this.OperationName))
        throw new ApplicationException("Unable to build unqiue key for WorkflowCallTask. Missing OperationName.");
      Guid contentItemMasterId = this.ContentItemMasterId;
      if (this.ContentItemMasterId == Guid.Empty)
        throw new ApplicationException("Unable to build unqiue key for WorkflowCallTask. Missing ContentItemMasterId.");
      string rootKey = (string) null;
      if (this.ContentType == typeof (PageNode))
      {
        ISite site = SystemManager.CurrentContext.CurrentSite;
        if (this.SiteId != Guid.Empty && site.Id != this.SiteId)
          site = SystemManager.CurrentContext.MultisiteContext.GetSiteById(this.SiteId);
        rootKey = site.SiteMapRootNodeId.ToString();
      }
      return string.Format("{0}|{1}|{2}", (object) WorkflowCallTask.BuildScopeKey(this.contentTypeName, this.ContentProviderName, this.Language, rootKey), (object) this.ContentItemMasterId, (object) this.OperationName);
    }

    /// <summary>Executes the task.</summary>
    public override void ExecuteTask()
    {
      try
      {
        Dictionary<string, string> contextBag = this.WorkflowContextBag ?? new Dictionary<string, string>();
        contextBag.Add("ContentType", this.ContentType.FullName);
        contextBag.Add("ExecutionSource", nameof (WorkflowCallTask));
        CultureInfo cultureInfo = (CultureInfo) null;
        if (this.Language != null)
          cultureInfo = CultureInfo.GetCultureInfo(this.Language);
        if (string.IsNullOrEmpty(this.currentUiCulture) && cultureInfo != null && SystemManager.CurrentContext.AppSettings.Multilingual)
          contextBag.Add("Language", cultureInfo.Name);
        else
          contextBag.Add("Language", this.currentUiCulture);
        using (new RunWithUserRegion(this.UserId != null ? Guid.Parse(this.UserId) : Guid.Empty))
          WorkflowManager.MessageWorkflow(this.ContentItemMasterId, this.ContentType, this.ContentProviderName, this.OperationName == "Publish" ? "ScheduledPublish" : this.OperationName, false, contextBag);
      }
      catch (Exception ex)
      {
        if (this.RetriedCount < 10)
          this.Reschedule();
        else
          throw;
      }
    }

    internal static Guid ResolveItemIdFromKey(string key, int offset, out string operationName)
    {
      string[] strArray = key.Substring(offset).Split('|');
      Guid result;
      if (Guid.TryParse(strArray[0], out result))
      {
        operationName = strArray.Length <= 1 ? "Publish" : strArray[1];
        return result;
      }
      operationName = string.Empty;
      return Guid.Empty;
    }

    internal static string BuildScopeKey(
      string itemTypeName,
      string itemProvider,
      string culture,
      string rootKey)
    {
      if (!string.IsNullOrEmpty(culture))
        culture = AppSettings.CurrentSettings.GetCultureName(CultureInfo.GetCultureInfo(culture));
      return string.Format("{0}:{1}:{2}:{3}:{4}", (object) nameof (WorkflowCallTask), (object) itemTypeName, (object) itemProvider, (object) culture, (object) rootKey);
    }

    private string GetElementValue(XElement element, string attibuteName) => element.Attribute(XName.Get(attibuteName))?.Value;

    private void Reschedule()
    {
      WorkflowCallTask task = new WorkflowCallTask();
      task.ContentItemMasterId = this.ContentItemMasterId;
      task.ContentProviderName = this.ContentProviderName;
      task.ContentType = this.ContentType;
      task.OperationName = this.OperationName;
      task.Title = this.Title;
      task.SiteId = this.SiteId;
      task.Language = this.Language;
      task.RetriedCount = this.RetriedCount + 1;
      SchedulingManager manager = SchedulingManager.GetManager();
      double num1 = Math.Pow(2.0, (double) this.RetriedCount);
      double num2 = num1 + num1 * (double) new Random().Next(-1000, 1000) / 3000.0;
      task.ExecuteTime = DateTime.UtcNow.AddMilliseconds(num2 * 60.0 * 1000.0);
      manager.AddTask((ScheduledTask) task);
      manager.SaveChanges();
    }
  }
}
