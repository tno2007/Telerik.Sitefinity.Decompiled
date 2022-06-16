﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences.CodeReferenceDefaultArticle
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences
{
  public class CodeReferenceDefaultArticle : SimpleView
  {
    private readonly string title;
    private readonly string description;
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleBuilder.CodeReferenceDefaultArticle.ascx");

    public CodeReferenceDefaultArticle(string title, string description)
    {
      this.title = title;
      this.description = description;
    }

    /// <summary>
    /// Gets the reference to the control that displays the title of the topic.
    /// </summary>
    protected virtual Label Title => this.Container.GetControl<Label>("title", true);

    /// <summary>
    /// Gets the reference to the control that displays the description of the topic.
    /// </summary>
    protected virtual Label Description => this.Container.GetControl<Label>("description", true);

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = CodeReferenceDefaultArticle.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    protected override void InitializeControls(GenericContainer container)
    {
      this.Title.Text = this.title;
      this.Description.Text = this.description;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
