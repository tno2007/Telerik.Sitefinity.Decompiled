// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.DocumentInboundPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Documents;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  public class DocumentInboundPipe : MediaContentInboundPipe, IAsyncPushPipe, IPushPipe, IInboundPipe
  {
    private readonly ProvidersComponent providersComponent = new ProvidersComponent();
    public new static readonly string PipeName = "DocumentSearchInboundPipe";
    private string cachedDocumentsKey = nameof (cachedDocumentsKey);

    public override string Name => DocumentInboundPipe.PipeName;

    /// <summary>Used to get the default mappings for this pipe</summary>
    /// <returns>The settings</returns>
    public new static List<Mapping> GetDefaultMappings() => ContentInboundPipe.GetDefaultMappings();

    public override void PushData(IList<PublishingSystemEventInfo> items) => this.PushData(items, true);

    protected override void PushData(
      IList<PublishingSystemEventInfo> items,
      bool runAsynchroniously)
    {
      if (runAsynchroniously)
        this.PushDataAsync<DocumentInboundPipe>(items);
      else
        this.PushDataSynchronously(items);
    }

    /// <summary>Pushes the data synchronously.</summary>
    /// <param name="items">The items.</param>
    public virtual void PushDataSynchronously(IList<PublishingSystemEventInfo> items)
    {
      List<WrapperObject> items1 = new List<WrapperObject>();
      List<WrapperObject> items2 = new List<WrapperObject>();
      foreach (PublishingSystemEventInfo publishingSystemEventInfo in (IEnumerable<PublishingSystemEventInfo>) items)
      {
        if (publishingSystemEventInfo.ItemAction == "SystemObjectDeleted")
        {
          WrapperObject wrapperObject = new WrapperObject(this.PipeSettings, publishingSystemEventInfo.Item, publishingSystemEventInfo.Language);
          items2.Add(wrapperObject);
        }
        if (publishingSystemEventInfo.ItemAction == "SystemObjectModified" || publishingSystemEventInfo.ItemAction == "SystemObjectAdded")
        {
          WrapperObject wrapperObject = this.GetConvertedItemsForMapping((object) publishingSystemEventInfo).First<WrapperObject>();
          items1.Add(wrapperObject);
          items2.Add(wrapperObject);
        }
      }
      this.PublishingPoint.RemoveItems((IList<WrapperObject>) items2);
      this.PublishingPoint.AddItems((IList<WrapperObject>) items1);
    }

    internal new static SitefinityContentPipeSettings GetDefaultPipeSettings()
    {
      SitefinityContentPipeSettings defaultPipeSettings = new SitefinityContentPipeSettings();
      defaultPipeSettings.IsInbound = true;
      defaultPipeSettings.PipeName = DocumentInboundPipe.PipeName;
      defaultPipeSettings.IsActive = true;
      defaultPipeSettings.MaxItems = 0;
      defaultPipeSettings.InvocationMode = PipeInvokationMode.Push;
      defaultPipeSettings.ContentTypeName = typeof (Document).FullName;
      defaultPipeSettings.ResourceClassId = LibrariesModule.ResourceClassId;
      defaultPipeSettings.UIName = Res.Get<LibrariesResources>().DocumentsAndFiles;
      return defaultPipeSettings;
    }

    protected override void AddAdditionalProperties(
      WrapperObject wrapperObject,
      MediaContent mediaContent)
    {
      base.AddAdditionalProperties(wrapperObject, mediaContent);
      string fileContent = this.GetFileContent(mediaContent as Document);
      wrapperObject.SetOrAddProperty("Content", (object) fileContent);
    }

    private string GetFileContent(Document doc)
    {
      if (doc == null)
        return string.Empty;
      try
      {
        if (!this.CanExtractDocumentContent((IDataItem) doc))
          return string.Empty;
        Guid id = doc.Id;
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext != null)
        {
          IDictionary items = currentHttpContext.Items;
          if (items.Contains((object) this.cachedDocumentsKey))
          {
            IDictionary<Guid, string> dictionary = (IDictionary<Guid, string>) items[(object) this.cachedDocumentsKey];
            if (dictionary.ContainsKey(id))
              return dictionary[id];
          }
        }
        string mimeMapping = Telerik.Sitefinity.Modules.Libraries.Web.MimeMapping.GetMimeMapping(doc.Extension);
        string text = this.GetDocumentService().ExtractText(mimeMapping, BlobStorageManager.GetManager(doc.GetStorageProviderName()).GetDownloadStream((IBlobContent) doc));
        if (currentHttpContext != null)
        {
          IDictionary items = currentHttpContext.Items;
          currentHttpContext.Items[(object) this.cachedDocumentsKey] = (object) new Dictionary<Guid, string>();
          ((IDictionary<Guid, string>) items[(object) this.cachedDocumentsKey])[id] = text;
        }
        return text;
      }
      catch (Exception ex)
      {
        Exception exceptionToHandle = new Exception(string.Format("Error extracting the text content of a document '{0}' with id '{1}'", (object) ((IHasTitle) doc).GetTitle(), (object) doc.Id), ex);
        if (Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
          throw exceptionToHandle;
        return string.Empty;
      }
    }

    private bool CanExtractDocumentContent(IDataItem dataItem) => typeof (Document).IsAssignableFrom(dataItem.GetType()) && this.GetDocumentService().GetSupportedExtensions().Contains(((MediaContent) dataItem).MimeType);

    private IDocumentService GetDocumentService() => ServiceBus.ResolveService<IDocumentService>();
  }
}
