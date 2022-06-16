// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.TemplateBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  internal class TemplateBuilder : ControlBuilder
  {
    internal TemplateBuilder(string html, PropertyBuilder parent)
      : base((ObjectBuilder) parent, html, (string) null)
    {
      this.TagName = nameof (TemplateBuilder);
    }

    internal override Control CreateControl(Control bindingContainer) => (Control) new TemplateBuilder.SimpleTmplate(this, bindingContainer);

    private class SimpleTmplate : Control, ITemplate
    {
      private TemplateBuilder parent;
      private Control bindingContainer;

      public SimpleTmplate(TemplateBuilder parent, Control bindingContainer)
      {
        this.parent = parent;
        this.bindingContainer = bindingContainer;
      }

      public void InstantiateIn(Control container)
      {
        foreach (ControlBuilder childBuilder in this.parent.ChildBuilders)
        {
          if (childBuilder is RootBuilder)
            ((RootBuilder) childBuilder).CreateChildControls(container, this.bindingContainer);
          else
            container.Controls.Add(childBuilder.CreateControl(this.bindingContainer));
        }
      }
    }
  }
}
