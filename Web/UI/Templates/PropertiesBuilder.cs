// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.PropertiesBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  internal class PropertiesBuilder : ObjectBuilder
  {
    internal PropertiesBuilder(string html, ObjectBuilder parent)
      : base(parent, html, (string) null)
    {
      this.TagName = nameof (PropertiesBuilder);
    }

    internal override Type ObjectType
    {
      get => this.Parent.ObjectType;
      set => this.Parent.ObjectType = value;
    }

    internal override string DefaultProperty
    {
      get => this.Parent.DefaultProperty;
      set => this.Parent.DefaultProperty = value;
    }

    internal override void ParseHtml(string html) => this.ParseHtml((TemplateParser) new PropertiesParser((ObjectBuilder) this, html));

    internal override object CreateObject(Control bindingConainer) => throw new NotSupportedException();

    internal void SetProperties(object component, Control bindingContainer)
    {
      foreach (PropertyBuilder childBuilder in this.ChildBuilders)
        childBuilder.SetProperty(component, bindingContainer);
    }
  }
}
