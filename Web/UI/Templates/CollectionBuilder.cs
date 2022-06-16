// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.CollectionBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  internal class CollectionBuilder : ObjectBuilder
  {
    internal CollectionBuilder(string html, PropertyBuilder parent)
      : base((ObjectBuilder) parent, html, (string) null)
    {
    }

    internal override void ParseHtml(string html) => this.ParseHtml((TemplateParser) new CollectionParser(this, html));

    internal override Type ObjectType
    {
      get => this.Parent.ObjectType;
      set => this.Parent.ObjectType = value;
    }

    internal PropertyBuilder Parent => (PropertyBuilder) base.Parent;

    internal override object CreateObject(Control bindingConainer) => throw new NotSupportedException();

    internal void SetCollection(IList list, Control bindingContainer)
    {
      foreach (ObjectBuilder childBuilder in this.ChildBuilders)
      {
        object obj = childBuilder.CreateObject(bindingContainer);
        if (!(list is ListItemCollection) || obj is ListItem)
          list.Add(obj);
      }
    }
  }
}
