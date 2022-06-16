// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.StsLoginForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Installer;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>Control for the backend Login page</summary>
  public class StsLoginForm : ViewBase
  {
    /// <summary>Specifies the name of the embedded template.</summary>
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.StsLoginForm.ascx");
    /// <summary>The user name client control id.</summary>
    private const string userNameControlId = "wrap_name";

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? StsLoginForm.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      this.ProvidersPanel.Visible = false;
      UserManager manager = UserManager.GetManager();
      IEnumerable<DataProviderBase> contextProviders = manager.GetContextProviders();
      if (contextProviders.Count<DataProviderBase>() > 1)
      {
        DataProviderBase defaultContextProvider = manager.GetDefaultContextProvider();
        this.ProvidersList.Items.Clear();
        foreach (DataProviderBase dataProviderBase in contextProviders)
        {
          dataProviderBase.SuppressSecurityChecks = true;
          try
          {
            ListItem listItem = new ListItem(dataProviderBase.Title, dataProviderBase.Name);
            if (defaultContextProvider.Name == dataProviderBase.Name)
              listItem.Selected = true;
            this.ProvidersList.Items.Add(listItem);
          }
          finally
          {
            dataProviderBase.SuppressSecurityChecks = false;
          }
        }
        this.ProvidersPanel.Visible = true;
      }
      string str = this.Page.Items[(object) "sf_message"] as string;
      if (!string.IsNullOrEmpty(str))
        this.ErrorLabel.Text = str;
      StartupConfig startupConfig = Config.Get<StartupConfig>();
      if (!startupConfig.Initialized || string.IsNullOrEmpty(startupConfig.UserName) || string.IsNullOrEmpty(startupConfig.Password))
        return;
      this.StartupHintPanel.Visible = true;
      ((Label) this.DefaultUser).Text = startupConfig.UserName;
      ((Label) this.DefaultPassword).Text = startupConfig.Password;
    }

    /// <inheritdoc />
    protected override void OnPreRender(EventArgs e)
    {
      if (this.Page != null && !this.IsDesignMode())
        this.Page.SetFocus("wrap_name");
      base.OnPreRender(e);
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the reference to the providers list control.</summary>
    protected virtual HtmlSelect ProvidersList => this.Container.GetControl<HtmlSelect>(nameof (ProvidersList), true);

    /// <summary>Gets the reference to the providers label control.</summary>
    protected virtual Label ProvidersLabel => this.Container.GetControl<Label>("ProviderLabel", true);

    /// <summary>Gets a reference to the error label.</summary>
    protected virtual SitefinityLabel ErrorLabel => this.Container.GetControl<SitefinityLabel>("errorLabel", false);

    /// <summary>Gets the reference the providers panel.</summary>
    protected virtual HtmlGenericControl ProvidersPanel => this.Container.GetControl<HtmlGenericControl>(nameof (ProvidersPanel), true);

    private HtmlGenericControl StartupHintPanel => this.Container.GetControl<HtmlGenericControl>(nameof (StartupHintPanel), false);

    private ITextControl DefaultUser => this.Container.GetControl<ITextControl>(nameof (DefaultUser), false);

    private ITextControl DefaultPassword => this.Container.GetControl<ITextControl>(nameof (DefaultPassword), false);
  }
}
