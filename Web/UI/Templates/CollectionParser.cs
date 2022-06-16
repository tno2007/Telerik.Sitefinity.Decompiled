// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.CollectionParser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Text;
using Telerik.Sitefinity.Utilities.HtmlParsing;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  internal class CollectionParser : TemplateParser
  {
    internal CollectionParser(CollectionBuilder parentBuilder, string html)
      : base((ObjectBuilder) parentBuilder, html)
    {
    }

    internal override bool ParseOpenTag(HtmlChunk chunk)
    {
      if (this.TagStack.Count == 0)
        return this.CreateObjectBuilder(chunk);
      if (this.StringBuffer == null)
        this.StringBuffer = new StringBuilder();
      return false;
    }

    internal override void ParseCloseTag(HtmlChunk chunk)
    {
      if (this.TagStack.Count != 0)
        return;
      this.FinalizeCurrentBuilder();
    }

    internal override void ParseText(HtmlChunk chunk)
    {
    }

    protected override ObjectBuilder CreateBuilder() => new ObjectBuilder(this.ParentBuilder);
  }
}
