// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.StateToolboxItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.ObjectModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Toolbox item that will construct a RadToolbar</summary>
  [ParseChildren(true)]
  public class StateToolboxItem : ToolboxItemBase, ICommandButton
  {
    public static readonly string UiPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.StateButton.ascx");
    private GenericContainer container;
    private ITemplate itemTemplate;
    private Collection<StateCommandItem> states;

    /// <summary>Gets or sets the name of the command.</summary>
    public string CommandName { get; set; }

    /// <summary>Gets the client pageId of the button control</summary>
    public virtual string ButtonClientId => this.StateControl.ClientID;

    /// <summary>Text for the toolbar</summary>
    public string Text { get; set; }

    /// <summary>Css class to apply to the rad toolbar</summary>
    public new string CssClass { get; set; }

    /// <summary>Skin to use for the rat toolbar</summary>
    public string Skin { get; set; }

    /// <summary>States defined for this toolbox item</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<StateCommandItem> States
    {
      get
      {
        if (this.states == null)
          this.states = new Collection<StateCommandItem>();
        return this.states;
      }
    }

    /// <summary>Gets or sets the item template for the toolbox item</summary>
    public override ITemplate ItemTemplate
    {
      get
      {
        if (this.itemTemplate == null)
          this.itemTemplate = ControlUtilities.GetTemplate(StateToolboxItem.UiPath, (string) null, Config.Get<ControlsConfig>().ResourcesAssemblyInfo, (string) null);
        return this.itemTemplate;
      }
      set => this.itemTemplate = value;
    }

    private GenericContainer Container
    {
      get
      {
        if (this.container == null)
        {
          this.container = new GenericContainer();
          this.ItemTemplate.InstantiateIn((Control) this.container);
        }
        return this.container;
      }
    }

    /// <summary>
    /// Generates the command item and returns instantiated control.
    /// </summary>
    /// <returns>Instance of the command button</returns>
    public Control GenerateCommandItem()
    {
      this.StateControl.Skin = this.Skin;
      this.StateControl.CssClass = this.CssClass;
      this.StateControl.Items.Add((RadToolBarItem) new RadToolBarButton()
      {
        CheckOnClick = false,
        ItemTemplate = (ITemplate) new StateToolboxItem.TextTemplate(this.Text)
      });
      foreach (StateCommandItem state in this.States)
      {
        RadToolBarItemCollection items = this.StateControl.Items;
        RadToolBarButton radToolBarButton = new RadToolBarButton(state.Text, state.IsChecked, this.Text);
        radToolBarButton.NavigateUrl = state.Url;
        radToolBarButton.CssClass = state.CssClass;
        radToolBarButton.Value = state.CommandName;
        items.Add((RadToolBarItem) radToolBarButton);
      }
      return (Control) this.Container;
    }

    private RadToolBar StateControl => this.Container.GetControl<RadToolBar>("stateButton", true);

    /// <summary>Implements ITemplate by rendereing a string</summary>
    private class TextTemplate : ITemplate
    {
      private string text;

      /// <summary>
      /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.StateToolboxItem.TextTemplate" /> class.
      /// </summary>
      /// <param name="text">The text.</param>
      public TextTemplate(string text) => this.text = text;

      /// <summary>
      /// When implemented by a class, defines the <see cref="T:System.Web.UI.Control" /> object that child controls and templates belong to. These child controls are in turn defined within an inline template.
      /// </summary>
      /// <param name="container">The <see cref="T:System.Web.UI.Control" /> object to contain the instances of controls from the inline template.</param>
      public void InstantiateIn(Control container) => container.Controls.Add((Control) new Literal()
      {
        Text = this.text
      });
    }
  }
}
