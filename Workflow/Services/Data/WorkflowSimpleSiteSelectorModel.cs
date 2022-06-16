// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Services.Data.WorkflowSimpleSiteSelectorModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration.Web;

namespace Telerik.Sitefinity.Workflow.Services.Data
{
  /// <summary>
  /// This class is used to define the scope of a given workflow definition.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  [DataContract]
  public class WorkflowSimpleSiteSelectorModel
  {
    /// <summary>Gets or sets the site id.</summary>
    [DataMember]
    public Guid SiteId { get; set; }

    /// <summary>Gets or sets the site name.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the cultures.</summary>
    [DataMember]
    public IList<CultureViewModel> Cultures { get; set; }

    /// <summary>Gets or sets the site map root node id.</summary>
    [DataMember]
    public Guid SiteMapRootNodeId { get; set; }
  }
}
