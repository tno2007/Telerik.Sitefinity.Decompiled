// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.NavTransformationElementViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration;
using Telerik.Sitefinity.ResponsiveDesign.Model;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings
{
  [DataContract]
  public class NavTransformationElementViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.NavTransformationElementViewModel" /> class.
    /// </summary>
    public NavTransformationElementViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.NavTransformationElementViewModel" /> class.
    /// </summary>
    /// <param name="element">The config element.</param>
    public NavTransformationElementViewModel(NavigationTransformationElement element)
    {
      this.Name = element.Name;
      this.Title = element.Title;
      this.Css = element.TransformationCss;
      this.IsActive = element.IsActive;
      ResponsiveDesignManager manager = ResponsiveDesignManager.GetManager();
      IQueryable<Guid> transformationQueryIds = manager.GetNavigationTransformations().Where<NavigationTransformation>((Expression<Func<NavigationTransformation, bool>>) (t => t.TransformationName == element.Name)).Select<NavigationTransformation, Guid>((Expression<Func<NavigationTransformation, Guid>>) (t => t.ParentMediaQueryId));
      this.UsedIn = manager.GetMediaQueries().Where<MediaQuery>((Expression<Func<MediaQuery, bool>>) (q => transformationQueryIds.Contains<Guid>(q.Id))).Select<MediaQuery, string>((Expression<Func<MediaQuery, string>>) (q => q.Name)).ToArray<string>();
    }

    /// <summary>Gets or sets the name.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the title.</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the CSS.</summary>
    [DataMember]
    public string Css { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is active.
    /// </summary>
    [DataMember]
    public bool IsActive { get; set; }

    /// <summary>Gets or sets the used in.</summary>
    [DataMember]
    public string[] UsedIn { get; set; }
  }
}
