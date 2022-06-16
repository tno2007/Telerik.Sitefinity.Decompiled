// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.SelectParentTemplateView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>Represents the view for selecting parent templates.</summary>
  public class SelectParentTemplateView : SelectTemplateBaseView
  {
    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container.</param>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      if (this.Mode == DialogModes.SelectTemplate)
      {
        this.ReturnToProperties.Text = Res.Get<Labels>().ReturnToProperties;
        this.ViewTitle.Text = Res.Get<Labels>().SelectLayout;
      }
      else
      {
        if (this.Mode != DialogModes.ChangeTemplate)
          return;
        this.ReturnToProperties.Text = Res.Get<Labels>().ReturnToTemplates;
        this.ViewTitle.Text = Res.Get<Labels>().SelectAnotherTemplate;
      }
    }

    /// <summary>Gets the edit URL.</summary>
    /// <returns></returns>
    protected override string GetEditUrl() => RouteHelper.ResolveUrl("/Sitefinity/Template/" + (object) this.CurrentTemplate.Id, UrlResolveOptions.Rooted);

    /// <summary>Gets the templates by category.</summary>
    /// <param name="category">The category.</param>
    /// <param name="templateId">The currently created/edited template pageId.</param>
    /// <returns></returns>
    protected override List<PageTemplate> GetTemplates(
      Guid category,
      Guid templateId)
    {
      return this.PageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Category == category && t.Id != templateId)).OrderBy<PageTemplate, short>((Expression<Func<PageTemplate, short>>) (t => t.Ordinal)).ToList<PageTemplate>();
    }
  }
}
