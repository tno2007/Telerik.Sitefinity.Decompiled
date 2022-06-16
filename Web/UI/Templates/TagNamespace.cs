// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.TagNamespace
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Utilities.HtmlParsing;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  internal class TagNamespace
  {
    private string tagPrefix;
    private string nameSpace;

    internal TagNamespace(HtmlChunk chunk)
    {
      this.tagPrefix = chunk.GetParamValue(nameof (TagPrefix));
      this.nameSpace = chunk.GetParamValue(nameof (Namespace));
    }

    internal TagNamespace(string tagPrefix, string nameSpace)
    {
      this.tagPrefix = tagPrefix;
      this.nameSpace = nameSpace;
    }

    internal string TagPrefix => this.tagPrefix;

    internal string Namespace => this.nameSpace;
  }
}
