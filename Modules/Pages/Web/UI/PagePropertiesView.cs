// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.PagePropertiesView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>Represents the view for creating/editing pages.</summary>
  public class PagePropertiesView : PropertiesBaseView
  {
    private PageData currentPage;
    /// <summary>
    /// Gets the name of resource file representing the dialog for creating pages.
    /// </summary>
    public static readonly string CreateDialogTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.CreatePagePropertiesView.ascx");
    /// <summary>
    /// Gets the name of resource file representing the dialog for editing the page properties.
    /// </summary>
    public static readonly string EditDialogTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.EditPagePropertiesView.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get
      {
        if (!string.IsNullOrWhiteSpace(base.LayoutTemplatePath))
          return base.LayoutTemplatePath;
        return this.Mode == DialogModes.Create || this.Mode == DialogModes.SelectTemplate ? PagePropertiesView.CreateDialogTemplateName : PagePropertiesView.EditDialogTemplateName;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the current page.</summary>
    /// <value>The current page.</value>
    public PageData CurrentPage
    {
      get => this.currentPage;
      set => this.currentPage = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer) => base.InitializeControls(dialogContainer);

    /// <summary>Sets the page properties.</summary>
    protected override void SetProperties()
    {
      if (this.CurrentPage == null)
        return;
      this.MenuName.Text = (string) this.CurrentPage.Title;
      this.GeneratedUrlName.Text = (string) this.CurrentPage.UrlName;
      this.UrlName.Text = (string) this.CurrentPage.UrlName;
      this.Title.Text = (string) this.CurrentPage.HtmlTitle;
      this.Description.Text = (string) this.CurrentPage.Description;
      this.Keywords.Text = (string) this.CurrentPage.Keywords;
    }
  }
}
