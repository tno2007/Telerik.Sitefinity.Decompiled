// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Compilation.Model.TemplateKeyResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Sitefinity.Web.Compilation.Model
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class TemplateKeyResponse
  {
    public TemplateKeyResponse(IEnumerable<string> ids) => this.Keys = (IEnumerable<string>) new List<string>(ids);

    public IEnumerable<string> Keys { get; set; }
  }
}
