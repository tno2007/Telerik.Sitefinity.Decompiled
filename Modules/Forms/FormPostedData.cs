// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.FormPostedData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Modules.Forms.Web;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>Contains all the data that is posted by a form.</summary>
  public class FormPostedData
  {
    /// <summary>Gets or sets the forms data.</summary>
    /// <value>The forms data.</value>
    public IDictionary<string, object> FormsData { get; set; }

    /// <summary>Gets or sets the files.</summary>
    /// <value>The files.</value>
    public IDictionary<string, List<FormHttpPostedFile>> Files { get; set; }
  }
}
