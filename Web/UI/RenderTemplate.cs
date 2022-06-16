// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.RenderTemplate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.SitefinityExceptions;

namespace Telerik.Sitefinity.Web.UI
{
  [ControlTemplateInfo(AreaName = "General", ControlDisplayName = "RenderTemplate", ResourceClassId = "Labels")]
  public class RenderTemplate : SimpleView
  {
    private string templateKey;

    /// <summary>Gets or sets the name of the template to render.</summary>
    public virtual string TemplateName { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>Gets or sets the template key.</summary>
    /// <value>The template key.</value>
    /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">Could not find a widget template with developer name \{0}\..Arrange(this.Name)</exception>
    public override string TemplateKey
    {
      get
      {
        if (this.TemplateName.IsNullOrEmpty())
          return base.TemplateKey;
        if (this.templateKey == null)
        {
          ParameterExpression parameterExpression;
          this.templateKey = PageManager.GetManager().GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (p => p.NameForDevelopers == this.TemplateName)).Select<ControlPresentation, string>(Expression.Lambda<Func<ControlPresentation, string>>((Expression) Expression.Call(p.Id, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), parameterExpression)).FirstOrDefault<string>();
        }
        return this.templateKey != null ? this.templateKey : throw new ItemNotFoundException("Could not find a widget template with developer name \"{0}\".".Arrange((object) this.TemplateName));
      }
      set => base.TemplateKey = value;
    }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }
  }
}
