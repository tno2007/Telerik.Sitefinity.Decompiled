// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.NewImplementation.Pipes.FormInboundPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Publishing.NewImplementation.Pipes
{
  /// <summary>Form inbound pipe</summary>
  [PipeDesigner]
  public class FormInboundPipe : ContentInboundPipe
  {
    /// <summary>The pipe name</summary>
    public new const string PipeName = "FormInboundPipe";

    /// <summary>
    /// Used to get the default settings for this pipe, without creating persistent object
    /// </summary>
    /// <returns>The settings</returns>
    public new static SitefinityContentPipeSettings GetTemplatePipeSettings()
    {
      SitefinityContentPipeSettings templatePipeSettings = new SitefinityContentPipeSettings();
      templatePipeSettings.IsInbound = true;
      templatePipeSettings.PipeName = nameof (FormInboundPipe);
      templatePipeSettings.IsActive = true;
      templatePipeSettings.InvocationMode = PipeInvokationMode.Push;
      templatePipeSettings.ContentTypeName = typeof (FormDescription).FullName;
      templatePipeSettings.UIName = Res.Get<FormsResources>().Form;
      return templatePipeSettings;
    }

    /// <summary>
    /// Determines whether this instance [can process item] the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>
    /// <c>true</c> if this instance [can process item] the specified item; otherwise, <c>false</c>.
    /// </returns>
    public override bool CanProcessItem(object item)
    {
      switch (item)
      {
        case null:
          return false;
        case WrapperObject _:
          return this.CanProcessItem(((WrapperObject) item).WrappedObject);
        case PublishingSystemEventInfo _:
          PublishingSystemEventInfo publishingSystemEventInfo = (PublishingSystemEventInfo) item;
          return this.IsItemSupported(publishingSystemEventInfo) && publishingSystemEventInfo.ItemAction == "SystemObjectDeleted" || this.CanProcessItem(publishingSystemEventInfo.Item);
        case FormDescription _:
          return true;
        default:
          return false;
      }
    }

    /// <summary>Pushes the data.</summary>
    /// <param name="items">The items.</param>
    public override void PushData(IList<PublishingSystemEventInfo> items)
    {
      List<WrapperObject> items1 = new List<WrapperObject>();
      List<WrapperObject> items2 = new List<WrapperObject>();
      foreach (PublishingSystemEventInfo publishingSystemEventInfo in (IEnumerable<PublishingSystemEventInfo>) items)
      {
        try
        {
          string contentTypeName = ((SitefinityContentPipeSettings) this.PipeSettings).ContentTypeName;
          if (!string.IsNullOrEmpty(contentTypeName) && publishingSystemEventInfo.ItemType != contentTypeName)
          {
            if (!TypeResolutionService.ResolveType(contentTypeName).IsAssignableFrom(TypeResolutionService.ResolveType(publishingSystemEventInfo.ItemType)))
              continue;
          }
          WrapperObject wrapperObject1 = new WrapperObject(this.PipeSettings, publishingSystemEventInfo.Item);
          wrapperObject1.Language = publishingSystemEventInfo.Language;
          if (this.PipeSettings.LanguageIds.Count > 0)
          {
            if (!this.PipeSettings.LanguageIds.Contains(wrapperObject1.Language))
              continue;
          }
          string itemAction = publishingSystemEventInfo.ItemAction;
          if (!(itemAction == "SystemObjectDeleted"))
          {
            if (!(itemAction == "SystemObjectAdded"))
            {
              if (itemAction == "SystemObjectModified")
              {
                WrapperObject wrapperObject2 = this.GetConvertedItemsForMapping((object) publishingSystemEventInfo).First<WrapperObject>();
                items2.Add(wrapperObject2);
                items1.Add(wrapperObject2);
              }
            }
            else
            {
              WrapperObject wrapperObject3 = this.GetConvertedItemsForMapping((object) publishingSystemEventInfo).First<WrapperObject>();
              items1.Add(wrapperObject3);
            }
          }
          else
            items2.Add(wrapperObject1);
        }
        catch (Exception ex)
        {
          this.HandleError("Error when push data for item action {0} for item {1}.".Arrange((object) publishingSystemEventInfo.ItemAction, publishingSystemEventInfo.Item), ex);
        }
      }
      IPublishingPointBusinessObject publishingPoint = PublishingSystemFactory.GetPublishingPoint(this.PipeSettings.PublishingPoint);
      if (items2.Count > 0)
        publishingPoint.RemoveItems((IList<WrapperObject>) items2);
      if (items1.Count <= 0)
        return;
      publishingPoint.AddItems((IList<WrapperObject>) items1);
    }

    /// <summary>Gets the converted items for mapping.</summary>
    /// <param name="itemInfos">The items.</param>
    /// <returns>The converted items.</returns>
    public virtual IEnumerable<WrapperObject> GetConvertedItemsForMapping(
      params object[] itemInfos)
    {
      List<WrapperObject> convertedItemsForMapping = new List<WrapperObject>();
      foreach (PublishingSystemEventInfo itemInfo in itemInfos.OfType<PublishingSystemEventInfo>())
      {
        WrapperObject wrapperObject = new WrapperObject(this.PipeSettings, itemInfo.Item, itemInfo.Language);
        this.SetWrapperObjectProperties(wrapperObject, this.GetForm(itemInfo) ?? throw new ArgumentException("Unable to find form in the WrapperObject", "items"));
        convertedItemsForMapping.Add(wrapperObject);
      }
      return (IEnumerable<WrapperObject>) convertedItemsForMapping;
    }

    /// <summary>Sets the wrapper object properties.</summary>
    /// <param name="wrapperObject">The wrapper object.</param>
    /// <param name="form">The form.</param>
    protected virtual void SetWrapperObjectProperties(WrapperObject wrapperObject, IContent form)
    {
      string empty = string.Empty;
      try
      {
        FormDescription formDescription = form as FormDescription;
        StringBuilder stringBuilder = new StringBuilder();
        foreach (FormControl control in (IEnumerable<FormControl>) formDescription.Controls)
        {
          if (FormsManager.GetFormControlType((ControlData) control).FullName.Equals("Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers.ContentBlockController"))
          {
            ControlProperty controlProperty1 = control.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name.Equals("Settings")));
            if (controlProperty1 != null)
            {
              ControlProperty controlProperty2 = controlProperty1.ChildProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name.Equals("Content")));
              if (controlProperty2 != null)
                stringBuilder.AppendLine(controlProperty2.Value);
            }
          }
        }
        wrapperObject.AddProperty("Content", (object) stringBuilder.ToString());
        IContent contentItem = this.GetContentItem(wrapperObject);
        PublishingUtilities.AddContentUsages(wrapperObject, (IDataItem) contentItem);
        PublishingUtilities.AddItemCategories(wrapperObject, (IDataItem) contentItem);
        wrapperObject.AddProperty("Provider", (object) contentItem.GetProviderName());
        if (!wrapperObject.HasProperty("PipeId"))
          wrapperObject.AddProperty("PipeId", (object) string.Empty);
        if (!wrapperObject.HasProperty("ContentType"))
          wrapperObject.AddProperty("ContentType", (object) typeof (FormDescription).FullName);
        if (wrapperObject.HasProperty("PublicationDate"))
          return;
        wrapperObject.AddProperty("PublicationDate", (object) formDescription.PublicationDate);
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
      }
    }

    /// <summary>Gets page node form publishing system event info.</summary>
    /// <param name="itemInfo">The event info.</param>
    /// <returns>The page node from the event info.</returns>
    private IContent GetForm(PublishingSystemEventInfo itemInfo) => !(itemInfo.Item is WrapperObject) || ((WrapperObject) itemInfo.Item).WrappedObject == null ? (IContent) (itemInfo.Item as FormDescription) : this.GetContentItem((WrapperObject) itemInfo.Item);

    private bool IsItemSupported(PublishingSystemEventInfo item) => string.Equals(item.ItemType, typeof (FormDescription).FullName);

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public override string Name => nameof (FormInboundPipe);
  }
}
