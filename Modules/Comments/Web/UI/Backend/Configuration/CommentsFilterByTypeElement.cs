// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Configuration.CommentsFilterByTypeElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Configuration
{
  internal class CommentsFilterByTypeElement : ContentFilteringWidgetDefinitionElement
  {
    private Lazy<ConfigElementList<FilterRangeElement>> items;

    public CommentsFilterByTypeElement(ConfigElement parent)
      : base(parent)
    {
      this.items = new Lazy<ConfigElementList<FilterRangeElement>>((Func<ConfigElementList<FilterRangeElement>>) (() => this.GetItems()));
    }

    private ConfigElementList<FilterRangeElement> GetItems()
    {
      IEnumerable<CommentableTypeElement> elements = this.GetCommentsModuleConfig().CommentableTypes.Elements;
      ConfigElementList<FilterRangeElement> parent = new ConfigElementList<FilterRangeElement>((ConfigElement) this);
      foreach (CommentableTypeElement commentableTypeElement in elements)
        parent.Add(new FilterRangeElement((ConfigElement) parent)
        {
          Key = commentableTypeElement.ItemType,
          Value = commentableTypeElement.FriendlyName
        });
      return parent;
    }

    private CommentsModuleConfig GetCommentsModuleConfig()
    {
      ConfigElement configElement1 = (ConfigElement) this;
      while (!(configElement1 is CommentsModuleConfig commentsModuleConfig))
      {
        configElement1 = configElement1.Parent;
        if (configElement1 == null)
        {
          ConfigElement configElement2 = (ConfigElement) Telerik.Sitefinity.Configuration.Config.Get<CommentsModuleConfig>();
          break;
        }
      }
      return commentsModuleConfig;
    }

    [Browsable(false)]
    public override ConfigElementList<FilterRangeElement> PredefinedFilteringRanges => this.items.Value;
  }
}
