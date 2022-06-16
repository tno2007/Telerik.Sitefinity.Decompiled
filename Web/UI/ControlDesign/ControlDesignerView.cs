// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  /// This is a base custom scriptable control implementation of a control designer view, that specific views can inherit
  /// A designer view is a reusable settings screen on the designers for example: List settings (paging, sorting, etc)
  /// </summary>
  public abstract class ControlDesignerView : SimpleScriptView, IControlDesignerView
  {
    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public virtual string ViewName => (string) null;

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public virtual string ViewTitle => (string) null;

    /// <summary>Gets or sets the parent designer.</summary>
    /// <value>The parent designer.</value>
    protected ControlDesignerBase ParentDesigner { get; set; }

    /// <summary>Initializes the view.</summary>
    public virtual void InitView(ControlDesignerBase designer) => this.ParentDesigner = designer;

    /// <summary>Gets or sets the name of the designer template.</summary>
    /// <value>The name of the designer template.</value>
    public string DesignerTemplateName { get; set; }

    /// <summary>Gets or sets the designer template path.</summary>
    /// <value>The designer template path.</value>
    public string DesignerTemplatePath
    {
      get => this.LayoutTemplatePath;
      set => this.LayoutTemplatePath = value;
    }
  }
}
