// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.ConnectorFormDataContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>
  /// Contains the data context for form submitted data. Used in <see cref="T:Telerik.Sitefinity.Modules.Forms.IConnectorFormDataSender" />.
  /// </summary>
  public class ConnectorFormDataContext
  {
    /// <summary>
    /// Gets or sets the <see cref="T:System.Web.HttpRequestBase" /> httpRequest from the form submission.
    /// </summary>
    public HttpRequestBase HttpRequest { get; set; }

    /// <summary>
    /// Gets or sets the page URL where the form was submitted.
    /// </summary>
    public Uri SubmitPageUrl { get; set; }

    /// <summary>
    /// Gets or sets the extended widget designer settings for the used connector data sender.
    /// </summary>
    public IDictionary<string, string> WidgetDesignerSettings { get; set; }

    /// <summary>
    /// Gets or sets the form attributes for the submitted form.
    /// </summary>
    public IDictionary<string, string> FormDescriptionAttributeSettings { get; set; }
  }
}
