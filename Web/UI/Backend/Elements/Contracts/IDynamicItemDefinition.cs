// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IDynamicItemDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// A contract providing all members that need to be implemented by widgets which are going to fire a command dynamically
  /// </summary>
  public interface IDynamicItemDefinition : IDefinition
  {
    /// <summary>Gets or sets the parameters.</summary>
    /// <value>The parameters.</value>
    NameValueCollection Parameters { get; }

    /// <summary>Gets or sets the resource class id.</summary>
    /// <value>The resource class id.</value>
    string ResourceClassId { get; set; }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    string Title { get; set; }

    /// <summary>Gets or sets the value.</summary>
    /// <value>The value.</value>
    string Value { get; set; }
  }
}
