// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Compilation.Model.PageMarkupModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;

namespace Telerik.Sitefinity.Web.Compilation.Model
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class PageMarkupModel : TemplateMarkupModel
  {
    public string Key { get; set; }

    public string CultureName { get; set; }

    public string VersionKey { get; set; }

    public string Url { get; set; }

    public string ErrorMessage { get; set; }
  }
}
