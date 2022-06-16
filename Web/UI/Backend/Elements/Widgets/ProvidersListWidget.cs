// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ProvidersListWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>
  /// Type that constructs provider list widget user interface element. All widgets should inherit <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.IWidget" /> interface.
  /// </summary>
  public class ProvidersListWidget : SimpleView, IWidget
  {
    private const string notCorrectInterface = "The Definition of {0} control does not implement {1} interface.";
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.ProvidersListButton.ascx");

    /// <summary>Gets or sets the definition.</summary>
    /// <value>The definition.</value>
    public IWidgetDefinition Definition { get; set; }

    public void Configure(IWidgetDefinition definition) => this.Definition = definition;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.ConstructWidget();

    /// <summary>
    /// Method responsible for constructing the menu widget based on <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IActionMenuWidgetDefinition" /> contract definition.
    /// </summary>
    private void ConstructWidget()
    {
      IProvidersListWidgetDefinition widgetDefinition = typeof (IProvidersListWidgetDefinition).IsAssignableFrom(this.Definition.GetType()) ? this.Definition as IProvidersListWidgetDefinition : throw new InvalidOperationException(string.Format("The Definition of {0} control does not implement {1} interface.", (object) this.GetType().FullName, (object) typeof (IProvidersListWidgetDefinition).FullName));
      this.Providers.ItemTypeName = widgetDefinition.DataItemType;
      this.Providers.ManagerTypeName = widgetDefinition.ManagerType;
      this.Providers.SelectProviderMessage = widgetDefinition.SelectProviderMessage;
      this.Providers.SelectProviderMessageCssClass = widgetDefinition.SelectProviderMessageCssClass;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ProvidersListWidget.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Reference to the providers list control in the toolbox item template
    /// </summary>
    protected virtual ProvidersList Providers => this.Container.GetControl<ProvidersList>("providersList", true);
  }
}
