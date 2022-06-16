// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.FormMultipageDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>
  /// Defines methods for decorating forms in MVC and WebForms
  /// </summary>
  public class FormMultipageDecorator : IFormMultipageDecorator
  {
    private readonly string separatorBegin = "<div data-sf-role=\"separator\" style=\"display:none;\">";
    private readonly string firstSeparatorBegin = "<div data-sf-role=\"separator\" >";
    private readonly string separatorEnd = "</div>";

    /// <inheritdoc />
    public virtual void AddMultiPageFormSeparators(Control control)
    {
      if (control == null)
        return;
      ControlCollection controls = control.Controls;
      List<int> intList = new List<int>();
      int num1 = 0;
      foreach (object obj in controls)
      {
        if (obj is MvcProxyBase mvcProxyBase && !mvcProxyBase.ControllerName.IsNullOrEmpty())
        {
          Type type = TypeResolutionService.ResolveType(mvcProxyBase.ControllerName, false);
          if (type != (Type) null && type.ImplementsInterface(typeof (IFormPageBreak)))
            intList.Add(num1);
        }
        ++num1;
      }
      if (intList.Count <= 0)
        return;
      controls.AddAt(0, (Control) new LiteralControl(this.FirstSeparatorBegin));
      controls.Add((Control) new LiteralControl(this.SeparatorEnd));
      int num2 = 2;
      foreach (int num3 in intList)
      {
        controls.AddAt(num3 + num2, (Control) new LiteralControl(this.SeparatorEnd + this.SeparatorBegin));
        ++num2;
      }
    }

    /// <inheritdoc />
    public virtual void WrapFormPage(StringBuilder formControlsMarkup)
    {
      formControlsMarkup.Insert(0, this.FirstSeparatorBegin);
      formControlsMarkup.Append(this.SeparatorEnd);
    }

    /// <inheritdoc />
    public virtual string AppendMultipageFormSeparatorsDevider(string control)
    {
      control = control + this.SeparatorEnd + this.SeparatorBegin;
      return control;
    }

    /// <inheritdoc />
    public virtual string SeparatorBegin => this.separatorBegin;

    /// <inheritdoc />
    public virtual string FirstSeparatorBegin => this.firstSeparatorBegin;

    /// <inheritdoc />
    public virtual string SeparatorEnd => this.separatorEnd;
  }
}
