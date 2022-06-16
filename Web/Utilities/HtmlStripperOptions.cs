// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.HtmlParsing.HtmlStripperOptions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Utilities.HtmlParsing
{
  internal class HtmlStripperOptions
  {
    public bool UseWhiteList { get; set; }

    public IList<HtmlStripperTag> Tags { get; set; }

    public string AttributeExpression { get; set; }

    public string ValueExpression { get; set; }
  }
}
