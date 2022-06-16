// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Modules.Workflow.WorkflowElementFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Workflow.Configuration;

namespace Telerik.Sitefinity.Fluent.Modules.Workflow
{
  public class WorkflowElementFacade<TParentFacade>
  {
    private TParentFacade parentFacade;
    private WorkflowElement workflowElement;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.Workflow.WorkflowElementFacade`1" /> class.
    /// </summary>
    /// <param name="workflowElement">The workflow element.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public WorkflowElementFacade(WorkflowElement workflowElement, TParentFacade parentFacade)
    {
      this.parentFacade = (object) parentFacade != null ? parentFacade : throw new ArgumentNullException(nameof (parentFacade));
      this.workflowElement = workflowElement;
    }

    public WorkflowElementFacade<TParentFacade> LocalizeUsing<TResourceClass>() where TResourceClass : Resource
    {
      this.workflowElement.ResourceClassId = typeof (TResourceClass).Name;
      return this;
    }

    public WorkflowElementFacade<TParentFacade> SetTitle(string itemTitle)
    {
      this.workflowElement.Title = !string.IsNullOrEmpty(itemTitle) ? itemTitle : throw new ArgumentNullException(nameof (itemTitle));
      return this;
    }

    public WorkflowElementFacade<TParentFacade> SetServiceUrl(string serviceUrl)
    {
      this.workflowElement.ServiceUrl = !string.IsNullOrEmpty(serviceUrl) ? serviceUrl : throw new ArgumentNullException(nameof (serviceUrl));
      return this;
    }

    /// <summary>Returns back to the toolbox section fluent API.</summary>
    /// <returns>An instance of the <see cref="!:ToolboxSectionFacade" />.</returns>
    public TParentFacade Done() => this.parentFacade;
  }
}
