// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services.MediaQueryViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Web.Services;
using Telerik.Sitefinity.ResponsiveDesign.Model;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services
{
  /// <summary>View model class for the media query type.</summary>
  [DataContract]
  public class MediaQueryViewModel
  {
    private string appliedToString;

    /// <summary>Gets or sets the id of the media query.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the name of the media query.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the rules of the media query.</summary>
    [DataMember]
    public MediaQueryRuleViewModel[] Rules { get; set; }

    /// <summary>Gets or sets the behavior of the media query.</summary>
    [DataMember]
    public string Behavior { get; set; }

    /// <summary>
    /// Gets or sets the additional css settings of the media query.
    /// </summary>
    [DataMember]
    public AdditionalCssViewModel AdditionalCss { get; set; }

    /// <summary>
    /// Gets or sets the information about the layout transformations.
    /// </summary>
    [DataMember]
    public LayoutTransformationViewModel[] LayoutTransformations { get; set; }

    /// <summary>
    /// Gets or sets the id of the page to which the user should be redirected
    /// if the media query is matched.
    /// </summary>
    [DataMember]
    public Guid MiniSitePageId { get; set; }

    /// <summary>
    /// Gets or sets the value which indicates if the media query is active. If true
    /// media query is active; otherwise media query is not active.
    /// </summary>
    [DataMember]
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the status (UI only property) of the media query.
    /// </summary>
    [DataMember]
    public string Status
    {
      get => !this.IsActive ? Res.Get<ResponsiveDesignResources>().Inactive : Res.Get<ResponsiveDesignResources>().Active;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets the description of the behavior user chose.
    /// </summary>
    [DataMember]
    public string BehaviorDescription
    {
      get => (ResponsiveDesignBehavior) Enum.Parse(typeof (ResponsiveDesignBehavior), this.Behavior) == ResponsiveDesignBehavior.TransformLayout ? Res.Get<ResponsiveDesignResources>().TransformLayout : string.Format(Res.Get<ResponsiveDesignResources>().OpenASpeciallyPreparedSite);
      set
      {
      }
    }

    /// <summary>Returns the number of pages that use this query.</summary>
    [DataMember]
    public int PagesCount { get; set; }

    /// <summary>Returns the number of pages that use this query.</summary>
    [DataMember]
    public int PageTemplatesCount { get; set; }

    /// <summary>
    /// Gets or sets the string that is used in the "Applied To" column.
    /// </summary>
    [DataMember]
    public string AppliedToString
    {
      get
      {
        if (this.appliedToString == null)
          this.appliedToString = ContentViewModel.GetStatisticsText(this.PagesCount, this.PageTemplatesCount);
        return this.appliedToString;
      }
      set
      {
      }
    }

    /// <summary>Gets or sets the transformations.</summary>
    [DataMember]
    public List<NavigationTransformationViewModel> NavigationTransformations { get; set; }
  }
}
