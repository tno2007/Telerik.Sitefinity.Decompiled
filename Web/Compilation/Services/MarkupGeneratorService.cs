// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Compilation.Services.MarkupGeneratorService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.Compilation.Model;

namespace Telerik.Sitefinity.Web.Compilation.Services
{
  [PrecompilationAuthFilter]
  internal class MarkupGeneratorService : Service
  {
    [AddHeader(ContentType = "application/json")]
    public TemplateMarkupResponseCollection<PageMarkupModel> Post(
      PageMarkupRequest request)
    {
      PageManager manager = PageManager.GetManager();
      List<Guid> requestedPageIds = request.Keys.Select<string, Guid>((Func<string, Guid>) (x => Guid.Parse(x))).ToList<Guid>();
      List<IGrouping<Guid, \u003C\u003Ef__AnonymousType28<Guid, Guid>>> list = manager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (x => requestedPageIds.Contains(x.Id))).ToList<PageNode>().Select(x => new
      {
        RootNodeId = x.RootNodeId,
        Id = x.Id
      }).GroupBy(x => x.RootNodeId).ToList<IGrouping<Guid, \u003C\u003Ef__AnonymousType28<Guid, Guid>>>();
      List<PageMarkupModel> items = new List<PageMarkupModel>();
      foreach (IGrouping<Guid, \u003C\u003Ef__AnonymousType28<Guid, Guid>> source in list)
      {
        IList<PageMarkupModel> pageMarkup = PageMarkupResolver.GetPageMarkup(source.Key, (IList<Guid>) source.Select(x => x.Id).ToList<Guid>());
        items.AddRange((IEnumerable<PageMarkupModel>) pageMarkup);
      }
      return new TemplateMarkupResponseCollection<PageMarkupModel>((IList<PageMarkupModel>) items);
    }

    [AddHeader(ContentType = "application/json")]
    public TemplateKeyResponse Get(PageKeyRequest request) => this.Get<IPageKeyStrategy>(request.StrategyKey, (Func<IPageKeyStrategy, IEnumerable<string>>) (x => x.GetPageIds().Select<Guid, string>((Func<Guid, string>) (y => y.ToString()))));

    [AddHeader(ContentType = "application/json")]
    public TemplateKeyResponse Get(TemplateKeyRequest request) => this.Get<ITemplateKeyStrategy>(request.StrategyKey, (Func<ITemplateKeyStrategy, IEnumerable<string>>) (x => (IEnumerable<string>) x.GetKeys()));

    [AddHeader(ContentType = "application/json")]
    public TemplateMarkupResponseCollection Post(
      TemplateMarkupRequest request)
    {
      List<TemplateMarkupModel> items = new List<TemplateMarkupModel>();
      foreach (string key in request.Keys)
      {
        TemplateMarkupModel templateMarkupModel = TemplateMarkupResolver.Resolve(key);
        items.Add(templateMarkupModel);
      }
      return new TemplateMarkupResponseCollection((IList<TemplateMarkupModel>) items);
    }

    private TemplateKeyResponse Get<TStrategy>(
      string key,
      Func<TStrategy, IEnumerable<string>> action)
    {
      return new TemplateKeyResponse(action((!string.IsNullOrEmpty(key) && ObjectFactory.IsTypeRegistered<TStrategy>(key) ? ObjectFactory.Resolve<TStrategy>(key) : throw new ArgumentOutOfRangeException("Invalid key specified in strategy")) ?? throw new InvalidOperationException(string.Format("Cannot resolve strategy for key: {0}. Please make sure it is properly registered", (object) key))));
    }
  }
}
