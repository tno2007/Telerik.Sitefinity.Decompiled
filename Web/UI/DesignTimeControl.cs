// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.DesignTimeControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents a control wrapper preventing control exceptions from been thrown during design time.
  /// </summary>
  public class DesignTimeControl : Control
  {
    private Exception exception;
    private Control originalControl;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.DesignTimeControl" /> class.
    /// </summary>
    /// <param name="originalControl">The original control.</param>
    public DesignTimeControl(Control originalControl) => this.originalControl = originalControl != null ? originalControl : throw new ArgumentNullException(nameof (originalControl));

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.DesignTimeControl" /> class.
    /// </summary>
    /// <param name="exception">The exception.</param>
    public DesignTimeControl(Exception exception) => this.exception = exception != null ? exception : throw new ArgumentNullException(nameof (exception));

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      if (this.exception != null)
        return;
      try
      {
        this.Controls.Add(this.originalControl);
      }
      catch (Exception ex)
      {
        this.exception = ex;
      }
    }

    /// <summary>
    /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.
    /// </summary>
    /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.exception == null)
      {
        try
        {
          StringWriter writer1 = new StringWriter();
          base.Render(new HtmlTextWriter((TextWriter) writer1));
          writer.Write(writer1.ToString());
          return;
        }
        catch (Exception ex)
        {
          this.exception = ex;
        }
      }
      Exceptions.HandleException(this.exception, ExceptionPolicyName.IgnoreExceptions);
      writer.Write(this.exception.Message);
    }
  }
}
