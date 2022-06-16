// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.IFormMultipageDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Text;
using System.Web.UI;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>
  /// Defines methods for decorating forms in MVC and WebForms
  /// </summary>
  public interface IFormMultipageDecorator
  {
    /// <summary>
    /// Adds multipage separators to Web Control. The separators will wrap each page separated
    /// from IFormPageBreak controls
    /// </summary>
    /// <param name="control">The control to which we could add the separators</param>
    void AddMultiPageFormSeparators(Control control);

    /// <summary>Wraps specific content with multipage separators</summary>
    /// <param name="formControlsMarkup">The markup of the content</param>
    void WrapFormPage(StringBuilder formControlsMarkup);

    /// <summary>
    /// Appends the multipage separators in the format end tag, begin tag
    /// </summary>
    /// <param name="control">String representation of the control</param>
    /// <returns>String representation of the control with the appended separator</returns>
    string AppendMultipageFormSeparatorsDevider(string control);

    /// <summary>Gets the first separator begin tag</summary>
    string FirstSeparatorBegin { get; }

    /// <summary>Gets the separator begin tag</summary>
    string SeparatorBegin { get; }

    /// <summary>Gets the separator end tag</summary>
    string SeparatorEnd { get; }
  }
}
