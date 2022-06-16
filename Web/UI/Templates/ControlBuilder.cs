// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.ControlBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  [DebuggerDisplay("{TagName} ObjectType={ObjectType}")]
  internal class ControlBuilder : ObjectBuilder
  {
    internal ControlBuilder(ObjectBuilder parent, string html, string templatePath)
      : base(parent, html, templatePath)
    {
    }

    internal ControlBuilder(ObjectBuilder parent, string html)
      : base(parent, html)
    {
    }

    internal ControlBuilder(ObjectBuilder parent)
      : base(parent)
    {
    }

    internal virtual Control CreateControl(Control bindingContainer) => this.CreateControl(bindingContainer, (PlaceHoldersCollection) null);

    internal virtual Control CreateControl(
      Control bindingContainer,
      PlaceHoldersCollection placeHolders)
    {
      Control control = (Control) this.CreateObject(bindingContainer, placeHolders);
      if (!(control is HtmlGenericControl htmlGenericControl))
        return control;
      htmlGenericControl.TagName = this.TagName;
      return control;
    }

    internal override object CreateObject(Control bindingContainer) => (object) this.CreateControl(bindingContainer);
  }
}
