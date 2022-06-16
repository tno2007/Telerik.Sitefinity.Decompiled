// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.NavTransformationTemplate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.ResponsiveDesign;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration;
using Telerik.Sitefinity.ResponsiveDesign.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  [ControlTemplateInfo(AreaName = "Navigation", ControlDisplayName = "NavigationMobile", ResourceClassId = "Labels")]
  public class NavTransformationTemplate : RenderTemplate
  {
    private bool? isApplicable;
    private GenericContainer container;

    /// <summary>Gets or sets the name of the transformation.</summary>
    public virtual string TransformationName { get; set; }

    /// <summary>
    /// Gets or sets the name of the responsive design provider.
    /// </summary>
    public virtual string ResponsiveDesignProviderName { get; set; }

    /// <summary>Gets or sets the container.</summary>
    protected internal override GenericContainer Container
    {
      get
      {
        if (this.IsApplicable)
          return base.Container;
        return this.container == null ? new GenericContainer() : this.container;
      }
    }

    private bool IsApplicable
    {
      get
      {
        if (!this.isApplicable.HasValue)
          this.isApplicable = new bool?(this.GetIsApplicable());
        return this.isApplicable.Value;
      }
    }

    private List<string> GetParentCssClasses()
    {
      List<string> parentCssClasses = new List<string>();
      for (Control parent = this.Parent; parent != null; parent = parent.Parent)
      {
        if (parent is WebControl webControl && webControl.CssClass != null)
          parentCssClasses.AddRange(((IEnumerable<string>) webControl.CssClass.Split(new char[1]
          {
            ' '
          }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (c => c.Trim())));
      }
      return parentCssClasses;
    }

    private bool GetIsApplicable()
    {
      if (!SystemManager.IsModuleEnabled("ResponsiveDesign"))
        return false;
      if (this.TransformationName.IsNullOrEmpty())
        return true;
      NavigationTransformationElement navigationTransformation1 = Config.Get<ResponsiveDesignConfig>().NavigationTransformations[this.TransformationName];
      if (navigationTransformation1 == null || !navigationTransformation1.IsActive)
        return false;
      ResponsiveDesignManager manager = ResponsiveDesignManager.GetManager(this.ResponsiveDesignProviderName);
      IQueryable<NavigationTransformation> queryable = manager.GetNavigationTransformations().Where<NavigationTransformation>((Expression<Func<NavigationTransformation, bool>>) (t => t.TransformationName == this.TransformationName));
      List<string> source = (List<string>) null;
      foreach (NavigationTransformation navigationTransformation2 in (IEnumerable<NavigationTransformation>) queryable)
      {
        if (manager.GetMediaQuery(navigationTransformation2.ParentMediaQueryId).IsActive)
        {
          if (navigationTransformation2.CssClasses.IsNullOrEmpty())
            return true;
          IEnumerable<string> strings = ((IEnumerable<string>) navigationTransformation2.CssClasses.Split(new char[1]
          {
            ','
          }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (c => c.Trim()));
          if (source == null)
            source = this.GetParentCssClasses();
          foreach (string str in strings)
          {
            string cssClass = str;
            if (source.Any<string>((Func<string, bool>) (c => c.Equals(cssClass, StringComparison.OrdinalIgnoreCase))))
              return true;
          }
        }
      }
      return false;
    }
  }
}
