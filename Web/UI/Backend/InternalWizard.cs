// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.InternalWizard
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  internal class InternalWizard : CompositeControl
  {
    private bool _activeStepIndexSet;
    private HtmlGenericControl _sideBarDiv;
    private InternalWizardStepCollection _wizardStepCollection;
    internal bool _displaySideBar;
    private Repeater _sideBarRepeater;
    private ITemplate _sideBarTemplate;
    private ITemplate _navigationTemplate;
    private IButtonControl _commandSender;
    private InternalWizard.NavigationTemplateContainer _navigationTemplateContainer;
    private MultiView _multiView;
    private Stack _historyStack;
    private static readonly object _eventActiveStepChanged = new object();
    private static readonly object _eventCancelButtonClick;
    private static readonly object _eventFinishButtonClick = new object();
    private static readonly object _eventNextButtonClick = new object();
    private static readonly object _eventPreviousButtonClick = new object();
    protected static readonly string CancelButtonID;
    public static readonly string CancelCommandName;
    protected static readonly string SideBarID;
    protected static readonly string FinishButtonID;
    protected static readonly string FinishPreviousButtonID;
    public static readonly string MoveCompleteCommandName;
    public static readonly string MoveNextCommandName;
    public static readonly string MovePreviousCommandName;
    public static readonly string MoveToCommandName;
    protected static readonly string SideBarButtonID;
    protected static readonly string StartNextButtonID;
    protected static readonly string StepNextButtonID;
    protected static readonly string StepPreviousButtonID;

    /// <summary>Occurs when the user switches to a new step in the control.</summary>
    [Category("Action")]
    public event EventHandler ActiveStepChanged
    {
      add => this.Events.AddHandler(InternalWizard._eventActiveStepChanged, (Delegate) value);
      remove => this.Events.RemoveHandler(InternalWizard._eventActiveStepChanged, (Delegate) value);
    }

    /// <summary>Occurs when the Cancel button is clicked.</summary>
    [Category("Action")]
    public event EventHandler CancelButtonClick
    {
      add => this.Events.AddHandler(InternalWizard._eventCancelButtonClick, (Delegate) value);
      remove => this.Events.RemoveHandler(InternalWizard._eventCancelButtonClick, (Delegate) value);
    }

    /// <summary>Occurs when the Finish button is clicked.</summary>
    [Category("Action")]
    public event InternalWizardNavigationEventHandler FinishButtonClick
    {
      add => this.Events.AddHandler(InternalWizard._eventFinishButtonClick, (Delegate) value);
      remove => this.Events.RemoveHandler(InternalWizard._eventFinishButtonClick, (Delegate) value);
    }

    /// <summary>Occurs when the Next button is clicked.</summary>
    [Category("Action")]
    public event InternalWizardNavigationEventHandler NextButtonClick
    {
      add => this.Events.AddHandler(InternalWizard._eventNextButtonClick, (Delegate) value);
      remove => this.Events.RemoveHandler(InternalWizard._eventNextButtonClick, (Delegate) value);
    }

    /// <summary>Occurs when the Previous button is clicked.</summary>
    [Category("Action")]
    public event InternalWizardNavigationEventHandler PreviousButtonClick
    {
      add => this.Events.AddHandler(InternalWizard._eventPreviousButtonClick, (Delegate) value);
      remove => this.Events.RemoveHandler(InternalWizard._eventPreviousButtonClick, (Delegate) value);
    }

    /// <summary>Raises the <see cref="E:Telerik.Cms.Web.UI.InternalWizard.ActiveStepChanged"></see> event.</summary>
    /// <param name="source">The source of the event.</param>
    /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
    protected virtual void OnActiveStepChanged(object source, EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[InternalWizard._eventActiveStepChanged];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    /// <summary>Raises the <see cref="E:Telerik.Cms.Web.UI.InternalWizard.CancelButtonClick"></see> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs"></see> containing the event data.</param>
    protected virtual void OnCancelButtonClick(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[InternalWizard._eventCancelButtonClick];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    /// <summary>Raises the <see cref="E:Telerik.Cms.Web.UI.InternalWizard.NextButtonClick"></see> event.</summary>
    /// <param name="e">A <see cref="T:Telerik.Cms.Web.UI.InternalWizardNavigationEventArgs"></see> containing the event data.</param>
    protected virtual void OnNextButtonClick(InternalWizardNavigationEventArgs e)
    {
      InternalWizardNavigationEventHandler navigationEventHandler = (InternalWizardNavigationEventHandler) this.Events[InternalWizard._eventNextButtonClick];
      if (navigationEventHandler == null)
        return;
      navigationEventHandler((object) this, e);
    }

    /// <summary>Raises the <see cref="E:Telerik.Cms.Web.UI.InternalWizard.PreviousButtonClick"></see> event.</summary>
    /// <param name="e">A <see cref="T:Telerik.Cms.Web.UI.InternalWizardNavigationEventArgs"></see> containing event data.</param>
    protected virtual void OnPreviousButtonClick(InternalWizardNavigationEventArgs e)
    {
      InternalWizardNavigationEventHandler navigationEventHandler = (InternalWizardNavigationEventHandler) this.Events[InternalWizard._eventPreviousButtonClick];
      if (navigationEventHandler == null)
        return;
      navigationEventHandler((object) this, e);
    }

    /// <summary>Raises the <see cref="E:Telerik.Cms.Web.UI.InternalWizard.FinishButtonClick"></see> event.</summary>
    /// <param name="e">A <see cref="T:Telerik.Cms.Web.UI.InternalWizardNavigationEventArgs"></see> containing the event data.</param>
    protected virtual void OnFinishButtonClick(InternalWizardNavigationEventArgs e)
    {
      InternalWizardNavigationEventHandler navigationEventHandler = (InternalWizardNavigationEventHandler) this.Events[InternalWizard._eventFinishButtonClick];
      if (navigationEventHandler != null)
        navigationEventHandler((object) this, e);
      string destinationPageUrl = this.FinishDestinationPageUrl;
      if (RouteHelper.IsAbsoluteUrl(destinationPageUrl) || string.IsNullOrEmpty(destinationPageUrl))
        return;
      this.Page.Response.Redirect(this.ResolveClientUrl(destinationPageUrl), false);
    }

    static InternalWizard()
    {
      InternalWizard._eventCancelButtonClick = new object();
      InternalWizard.CancelCommandName = "Cancel";
      InternalWizard.MoveNextCommandName = "MoveNext";
      InternalWizard.MovePreviousCommandName = "MovePrevious";
      InternalWizard.MoveToCommandName = "Move";
      InternalWizard.MoveCompleteCommandName = "MoveComplete";
      InternalWizard.CancelButtonID = "CancelButton";
      InternalWizard.StartNextButtonID = "StartNextButton";
      InternalWizard.StepPreviousButtonID = "StepPreviousButton";
      InternalWizard.StepNextButtonID = "StepNextButton";
      InternalWizard.FinishButtonID = "FinishButton";
      InternalWizard.FinishPreviousButtonID = "FinishPreviousButton";
      InternalWizard.SideBarID = "SideBarList";
      InternalWizard.SideBarButtonID = "SideBarButton";
    }

    public InternalWizard() => this._displaySideBar = true;

    /// <summary>Gets or sets the text caption that is displayed for the Cancel button.</summary>
    /// <returns>The text caption displayed for Cancel on the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see>. The default is "Cancel". The default text for the control is localized based on the current locale for the server.</returns>
    [Localizable(true)]
    [Category("Appearance")]
    public virtual string CancelButtonText
    {
      get => this.ViewState[nameof (CancelButtonText)] is string str ? str : "Cancel";
      set
      {
        if (!(value != this.CancelButtonText))
          return;
        this.ViewState[nameof (CancelButtonText)] = (object) value;
      }
    }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    [Localizable(true)]
    [Category("Appearance")]
    public virtual string Title
    {
      get => this.ViewState[nameof (Title)] is string str ? str : string.Empty;
      set => this.ViewState[nameof (Title)] = (object) value;
    }

    [Localizable(true)]
    [Category("Appearance")]
    public virtual string WelcomeMessage
    {
      get => this.ViewState[nameof (WelcomeMessage)] is string str ? str : string.Empty;
      set => this.ViewState[nameof (WelcomeMessage)] = (object) value;
    }

    /// <summary>Gets or sets the text caption that is displayed for the Finish button.</summary>
    /// <returns>The text caption displayed for Finish on the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see>. The default is "Finish". The default text for the control is localized based on the current locale for the server.</returns>
    [Category("Appearance")]
    [Localizable(true)]
    public virtual string CompleteButtonText
    {
      get => this.ViewState[nameof (CompleteButtonText)] is string str ? str : "I'm done";
      set => this.ViewState[nameof (CompleteButtonText)] = (object) value;
    }

    /// <summary>Gets or sets the text caption that is displayed for the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start"></see> step.</summary>
    /// <returns>The text caption displayed for Next on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start"></see> of the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see>. The default is "Next". The default text for the control is localized based on the current locale for the server.</returns>
    [Localizable(true)]
    [Category("Appearance")]
    public virtual string NextButtonText
    {
      get => this.ViewState[nameof (NextButtonText)] is string str ? str : "Continue";
      set => this.ViewState[nameof (NextButtonText)] = (object) value;
    }

    /// <summary>Gets or sets the text caption that is displayed for the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish"></see> step.</summary>
    /// <returns>The text caption displayed for Previous on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish"></see> of the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see>. The default is "Previous". The default text for the control is localized based on the current locale for the server.</returns>
    [Localizable(true)]
    [Category("Appearance")]
    public virtual string PreviousButtonText
    {
      get => this.ViewState[nameof (PreviousButtonText)] is string str ? str : "Back";
      set => this.ViewState[nameof (PreviousButtonText)] = (object) value;
    }

    /// <summary>Gets or sets the URL that the user is redirected to when they click the Finish button.</summary>
    /// <returns>The URL that the user is redirected to when they click Finish on the <see cref="T:System.Web.UI.WebControls.Wizard"></see>. The default is an empty string ("").</returns>
    [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue("")]
    [Themeable(false)]
    [Category("Behavior")]
    [UrlProperty]
    public virtual string FinishDestinationPageUrl
    {
      get => (string) this.ViewState[nameof (FinishDestinationPageUrl)] ?? string.Empty;
      set => this.ViewState[nameof (FinishDestinationPageUrl)] = (object) value;
    }

    /// <summary>Gets or sets a Boolean value indicating whether to display the sidebar area on the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see> control.</summary>
    /// <returns>true to display the sidebar area on the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see>; otherwise, false. The default is true.This property cannot be set by themes or style sheet themes. For more information, see <see cref="T:System.Web.UI.ThemeableAttribute"></see> and ASP.NET Themes Overview.</returns>
    [Category("Behavior")]
    [Themeable(false)]
    [DefaultValue(true)]
    public virtual bool DisplaySideBar
    {
      get => this._displaySideBar;
      set
      {
        if (value == this._displaySideBar)
          return;
        this._displaySideBar = value;
        this._sideBarDiv = (HtmlGenericControl) null;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets or sets the template that is used to display the sidebar area on the control.</summary>
    /// <returns>An <see cref="T:System.Web.UI.ITemplate"></see> that contains the template for displaying the sidebar area on the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see>. The default is null.</returns>
    [DefaultValue(null)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (InternalWizard))]
    [Browsable(false)]
    public virtual ITemplate SideBarTemplate
    {
      get => this._sideBarTemplate;
      set
      {
        this._sideBarTemplate = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets or sets the template that is used to display the navigation area on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start"></see> step of the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see> control.</summary>
    /// <returns>An <see cref="T:System.Web.UI.ITemplate"></see> that contains the template for displaying the navigation area on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start"></see> for the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see>. The default is null.</returns>
    [Browsable(false)]
    [DefaultValue(null)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (InternalWizard))]
    public virtual ITemplate NavigationTemplate
    {
      get => this._navigationTemplate;
      set
      {
        this._navigationTemplate = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets a collection containing all the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see> objects that are defined for the control.</summary>
    /// <returns>A <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> representing all the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see> objects defined for the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see>.</returns>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [Themeable(false)]
    public virtual InternalWizardStepCollection WizardSteps
    {
      get
      {
        if (this._wizardStepCollection == null)
          this._wizardStepCollection = new InternalWizardStepCollection(this);
        return this._wizardStepCollection;
      }
    }

    /// <summary>Gets the step in the <see cref="P:System.Web.UI.WebControls.InternalWizard.WizardSteps"></see> collection that is currently displayed to the user.</summary>
    /// <returns>The <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see> that is currently displayed to the user.</returns>
    /// <exception cref="T:System.InvalidOperationException">The corresponding <see cref="P:System.Web.UI.WebControls.InternalWizard.ActiveStepIndex"></see> is less than -1 or greater than the number of <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see> objects in the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see>.</exception>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public InternalWizardStep ActiveStep
    {
      get
      {
        if (this.ActiveStepIndex < -1 || this.ActiveStepIndex >= this.WizardSteps.Count)
          throw new InvalidOperationException("Wizard_ActiveStepIndex_out_of_range");
        return this.MultiView.GetActiveView() as InternalWizardStep;
      }
    }

    /// <summary>Gets or sets the index of the current <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see> object.</summary>
    /// <returns>The index of the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see> that is currently displayed in the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see> control.</returns>
    [DefaultValue(-1)]
    [Themeable(false)]
    [Category("Behavior")]
    public virtual int ActiveStepIndex
    {
      get => this.MultiView.ActiveViewIndex;
      set
      {
        if (value < -1 || value >= this.WizardSteps.Count)
          throw new ArgumentOutOfRangeException();
        if (this.MultiView.ActiveViewIndex == value)
          return;
        this.MultiView.ActiveViewIndex = value;
        this._activeStepIndexSet = true;
      }
    }

    /// <summary>Uses a Boolean value to determine whether the <see cref="P:System.Web.UI.WebControls.InternalWizard.ActiveStep"></see> property can be set to the <see cref="T:Telerik.Cms.Web.UI.WizardStepBase"></see> object that corresponds to the index that is passed in.</summary>
    /// <returns>false if the index passed in refers to a <see cref="T:Telerik.Cms.Web.UI.WizardStepBase"></see> that has already been accessed and its <see cref="P:System.Web.UI.WebControls.WizardStepBase.AllowReturn"></see> property is set to false; otherwise, true.</returns>
    /// <param name="index">The index of the <see cref="T:Telerik.Cms.Web.UI.WizardStepBase"></see> object being checked.</param>
    protected virtual bool AllowNavigationToStep(int index) => this._historyStack == null || !this._historyStack.Contains((object) index) || this.WizardSteps[index].AllowReturn;

    /// <summary>Returns the <see cref="T:Telerik.Cms.Web.UI.WizardStepType"></see> value for the specified <see cref="T:Telerik.Cms.Web.UI.WizardStepBase"></see> object.</summary>
    /// <returns>One of the <see cref="T:Telerik.Cms.Web.UI.WizardStepType"></see> values.</returns>
    /// <param name="index">The index of the <see cref="T:Telerik.Cms.Web.UI.WizardStepBase"></see> for which the associated <see cref="T:Telerik.Cms.Web.UI.WizardStepType"></see>  is returned.</param>
    /// <param name="wizardStep">The <see cref="T:Telerik.Cms.Web.UI.WizardStepBase"></see> for which the associated <see cref="T:Telerik.Cms.Web.UI.WizardStepType"></see>  is returned.</param>
    public WizardStepType GetStepType(InternalWizardStep wizardStep, int index)
    {
      if (wizardStep.StepType != WizardStepType.Auto)
        return wizardStep.StepType;
      if (this.WizardSteps.Count == 1 || index < this.WizardSteps.Count - 1 && this.WizardSteps[index + 1].StepType == WizardStepType.Complete)
        return WizardStepType.Finish;
      if (index == 0)
        return WizardStepType.Start;
      return index == this.WizardSteps.Count - 1 ? WizardStepType.Finish : WizardStepType.Step;
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.Controls.Clear();
      if (this.DisplaySideBar)
      {
        this._sideBarDiv = new HtmlGenericControl("div");
        this._sideBarDiv.Attributes.Add("class", "sfWizardSteps");
        this.Controls.Add((Control) this._sideBarDiv);
        ITemplate template = this.SideBarTemplate;
        if (template == null)
        {
          this._sideBarDiv.EnableViewState = false;
          template = (ITemplate) new InternalWizard.DefaultSideBarTemplate(this);
        }
        else
          this._sideBarDiv.EnableTheming = this.EnableTheming;
        template.InstantiateIn((Control) this._sideBarDiv);
        this._sideBarRepeater = this._sideBarDiv.FindControl(InternalWizard.SideBarID) as Repeater;
      }
      HtmlGenericControl child1 = new HtmlGenericControl("div");
      child1.Attributes.Add("class", "sfWizardContent sfClearfix");
      this.Controls.Add((Control) child1);
      child1.Controls.Add((Control) this.MultiView);
      HtmlGenericControl child2 = new HtmlGenericControl("div");
      child2.Attributes.Add("class", "sfButtonArea sfMainFormBtns");
      this.Controls.Add((Control) child2);
      this._navigationTemplateContainer = new InternalWizard.NavigationTemplateContainer(this);
      child2.Controls.Add((Control) this._navigationTemplateContainer);
      ITemplate navigationTemplate = this.NavigationTemplate;
      if (navigationTemplate == null)
      {
        this._navigationTemplateContainer.EnableViewState = false;
        ((ITemplate) new InternalWizard.DefaultNavigationTemplate(this)).InstantiateIn((Control) this._navigationTemplateContainer);
      }
      else
      {
        navigationTemplate.InstantiateIn((Control) this._navigationTemplateContainer);
        this._navigationTemplateContainer.RegisterButtonCommandEvents();
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.ApplyControlProperties();
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected override void AddAttributesToRender(HtmlTextWriter writer)
    {
      base.AddAttributesToRender(writer);
      if (!string.IsNullOrEmpty(this.CssClass))
        return;
      writer.AddAttribute(HtmlTextWriterAttribute.Class, "sfWizard");
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      if (this.ActiveStepIndex == -1 && this.WizardSteps.Count > 0 && !this.DesignMode)
        this.ActiveStepIndex = 0;
      this.EnsureChildControls();
      if (this.Page == null)
        return;
      this.Page.RegisterRequiresControlState((Control) this);
    }

    protected override object SaveControlState()
    {
      int activeStepIndex = this.ActiveStepIndex;
      if (this._historyStack == null || this._historyStack.Count == 0 || (int) this._historyStack.Peek() != activeStepIndex)
        this.History.Push((object) this.ActiveStepIndex);
      object x = base.SaveControlState();
      bool flag = this._historyStack != null && this._historyStack.Count > 0;
      if (!(x != null | flag) && activeStepIndex == -1)
        return (object) null;
      object y = flag ? (object) this._historyStack.ToArray() : (object) (object[]) null;
      return (object) new Triplet(x, y, (object) activeStepIndex);
    }

    protected override void LoadControlState(object state)
    {
      if (!(state is Triplet triplet))
        return;
      base.LoadControlState(triplet.First);
      if (triplet.Second is Array second)
      {
        Array.Reverse(second);
        this._historyStack = new Stack((ICollection) second);
      }
      this.ActiveStepIndex = (int) triplet.Third;
    }

    protected override bool OnBubbleEvent(object source, EventArgs e)
    {
      bool flag1 = false;
      if (e is CommandEventArgs)
      {
        CommandEventArgs commandEventArgs = (CommandEventArgs) e;
        if (string.Equals(InternalWizard.CancelCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
        {
          this.OnCancelButtonClick(EventArgs.Empty);
          return true;
        }
        int activeStepIndex = this.ActiveStepIndex;
        int nextStepIndex1 = activeStepIndex;
        bool flag2 = true;
        WizardStepType stepType = this.GetStepType(this.WizardSteps[activeStepIndex]);
        InternalWizardNavigationEventArgs navigationEventArgs = new InternalWizardNavigationEventArgs(activeStepIndex, nextStepIndex1);
        if (this._commandSender != null && this.Page != null && !this.Page.IsValid)
          navigationEventArgs.Cancel = true;
        bool usePreviousStepIndex = false;
        this._activeStepIndexSet = false;
        if (string.Equals(InternalWizard.MoveNextCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
        {
          if (flag2 && stepType != WizardStepType.Start && stepType != WizardStepType.Step)
            throw new InvalidOperationException();
          if (activeStepIndex < this.WizardSteps.Count - 1)
            navigationEventArgs.SetNextStepIndex(activeStepIndex + 1);
          this.OnNextButtonClick(navigationEventArgs);
          flag1 = true;
        }
        else if (string.Equals(InternalWizard.MovePreviousCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
        {
          if (flag2 && stepType != WizardStepType.Step && stepType != WizardStepType.Finish)
            throw new InvalidOperationException();
          usePreviousStepIndex = true;
          int previousStepIndex = this.GetPreviousStepIndex(false);
          if (previousStepIndex != -1)
            navigationEventArgs.SetNextStepIndex(previousStepIndex);
          this.OnPreviousButtonClick(navigationEventArgs);
          flag1 = true;
        }
        else if (string.Equals(InternalWizard.MoveCompleteCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
        {
          if (flag2 && stepType != WizardStepType.Finish)
            throw new InvalidOperationException();
          if (activeStepIndex < this.WizardSteps.Count - 1)
            navigationEventArgs.SetNextStepIndex(activeStepIndex + 1);
          this.OnFinishButtonClick(navigationEventArgs);
          flag1 = true;
        }
        else if (string.Equals(InternalWizard.MoveToCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
        {
          int nextStepIndex2 = int.Parse((string) commandEventArgs.CommandArgument, (IFormatProvider) CultureInfo.InvariantCulture);
          navigationEventArgs.SetNextStepIndex(nextStepIndex2);
          flag1 = true;
        }
        if (flag1)
          this.SetActiveStep(activeStepIndex, navigationEventArgs, usePreviousStepIndex);
      }
      return flag1;
    }

    internal void SetActiveStep(
      int stepIndex,
      InternalWizardNavigationEventArgs args,
      bool usePreviousStepIndex,
      bool skipActiveStepIndexSet = false)
    {
      if (!args.Cancel)
      {
        if (!skipActiveStepIndexSet && this._activeStepIndexSet || !this.AllowNavigationToStep(args.NextStepIndex))
          return;
        if (usePreviousStepIndex)
          this.GetPreviousStepIndex(true);
        this.ActiveStepIndex = args.NextStepIndex;
      }
      else
        this.ActiveStepIndex = stepIndex;
    }

    internal void SkipSteps(int numSteps)
    {
      for (int index = 0; index < numSteps; ++index)
        this.History.Push((object) index);
    }

    /// <summary>Registers a new instance of the <see cref="T:Telerik.Cms.Web.UI.CommandEventHandler"></see> class for the specified <see cref="T:Telerik.Cms.Web.UI.IButtonControl"></see> object.</summary>
    /// <param name="button">The <see cref="T:Telerik.Cms.Web.UI.IButtonControl"></see> for which the new instance of <see cref="T:Telerik.Cms.Web.UI.CommandEventHandler"></see> is registered.</param>
    protected internal void RegisterCommandEvents(IButtonControl button)
    {
      if (button == null || !button.CausesValidation)
        return;
      button.Command += new CommandEventHandler(this.OnCommand);
    }

    private void OnCommand(object sender, CommandEventArgs e) => this._commandSender = sender as IButtonControl;

    private void MultiViewActiveViewChanged(object source, EventArgs e) => this.OnActiveStepChanged((object) this, EventArgs.Empty);

    internal MultiView MultiView
    {
      get
      {
        if (this._multiView == null)
        {
          this._multiView = new MultiView();
          this._multiView.EnableTheming = true;
          this._multiView.ID = "WizardMultiView";
          this._multiView.ActiveViewChanged += new EventHandler(this.MultiViewActiveViewChanged);
        }
        return this._multiView;
      }
    }

    internal void OnWizardStepsChanged()
    {
      if (this._sideBarRepeater == null)
        return;
      this._sideBarRepeater.DataSource = (object) this.WizardSteps;
      this._sideBarRepeater.DataBind();
    }

    internal Repeater SideBarRepeater => this._sideBarRepeater;

    internal bool SideBarEnabled => this._sideBarRepeater != null && this.DisplaySideBar;

    internal int GetPreviousStepIndex(bool popStack)
    {
      int previousStepIndex = -1;
      int activeStepIndex = this.ActiveStepIndex;
      if (this._historyStack != null && this._historyStack.Count != 0)
      {
        if (popStack)
        {
          previousStepIndex = (int) this._historyStack.Pop();
          if (previousStepIndex == activeStepIndex && this._historyStack.Count > 0)
            previousStepIndex = (int) this._historyStack.Pop();
        }
        else
        {
          previousStepIndex = (int) this._historyStack.Peek();
          if (previousStepIndex == activeStepIndex && this._historyStack.Count > 1)
          {
            int num = (int) this._historyStack.Pop();
            previousStepIndex = (int) this._historyStack.Peek();
            this._historyStack.Push((object) num);
          }
        }
        if (previousStepIndex == activeStepIndex)
          return -1;
      }
      return previousStepIndex;
    }

    internal WizardStepType GetStepType(int index) => this.GetStepType(this.WizardSteps[index], index);

    internal WizardStepType GetStepType(InternalWizardStep step)
    {
      int index = this.WizardSteps.IndexOf(step);
      return this.GetStepType(step, index);
    }

    internal virtual void ApplyControlProperties()
    {
      if (!this.DesignMode && (this.ActiveStepIndex < 0 || this.ActiveStepIndex >= this.WizardSteps.Count || this.WizardSteps.Count == 0))
        return;
      this.ApplyNavigationTemplateProperties();
      if (!this.SideBarEnabled)
        return;
      this._sideBarRepeater.DataSource = (object) this.WizardSteps;
      this._sideBarRepeater.DataBind();
    }

    private void ApplyNavigationTemplateProperties()
    {
      if (this._navigationTemplateContainer == null)
        return;
      IButtonControl previousButton = this._navigationTemplateContainer.PreviousButton;
      IButtonControl nextButton = this._navigationTemplateContainer.NextButton;
      IButtonControl finishButton = this._navigationTemplateContainer.FinishButton;
      if (previousButton != null)
      {
        if (this.ActiveStepIndex > 0 && this.AllowNavigationToStep(this.ActiveStepIndex - 1))
          ((Control) previousButton).Visible = true;
        else
          ((Control) previousButton).Visible = false;
      }
      if (nextButton != null)
        ((Control) nextButton).Visible = this.WizardSteps.Count - this.ActiveStepIndex > 1;
      if (finishButton != null)
        ((Control) finishButton).Visible = this.WizardSteps.Count - this.ActiveStepIndex == 1;
      if (this.Page == null)
        return;
      if (nextButton != null && ((Control) nextButton).Visible)
      {
        this.Page.Form.DefaultButton = ((Control) nextButton).UniqueID;
      }
      else
      {
        if (finishButton == null || !((Control) finishButton).Visible)
          return;
        this.Page.Form.DefaultButton = ((Control) finishButton).UniqueID;
      }
    }

    private Stack History
    {
      get
      {
        if (this._historyStack == null)
          this._historyStack = new Stack();
        return this._historyStack;
      }
    }

    internal class NavigationTemplateContainer : Control, INamingContainer
    {
      private InternalWizard _owner;
      private IButtonControl _cancelButton;
      private IButtonControl _finishButton;
      private IButtonControl _nextButton;
      private IButtonControl _previousButton;

      internal NavigationTemplateContainer(InternalWizard owner) => this._owner = owner;

      internal virtual void RegisterButtonCommandEvents()
      {
        this.Owner.RegisterCommandEvents(this.NextButton);
        this.Owner.RegisterCommandEvents(this.FinishButton);
        this.Owner.RegisterCommandEvents(this.PreviousButton);
        this.Owner.RegisterCommandEvents(this.CancelButton);
      }

      internal virtual IButtonControl CancelButton
      {
        get
        {
          if (this._cancelButton == null)
            this._cancelButton = this.FindControl(InternalWizard.CancelButtonID) as IButtonControl;
          return this._cancelButton;
        }
        set => this._cancelButton = value;
      }

      internal virtual IButtonControl FinishButton
      {
        get
        {
          if (this._finishButton == null)
            this._finishButton = this.FindControl(InternalWizard.FinishButtonID) as IButtonControl;
          return this._finishButton;
        }
        set => this._finishButton = value;
      }

      internal virtual IButtonControl NextButton
      {
        get
        {
          if (this._nextButton == null)
            this._nextButton = this.FindControl(InternalWizard.StepNextButtonID) as IButtonControl;
          return this._nextButton;
        }
        set => this._nextButton = value;
      }

      internal InternalWizard Owner => this._owner;

      internal virtual IButtonControl PreviousButton
      {
        get
        {
          if (this._previousButton == null)
            this._previousButton = this.FindControl(InternalWizard.StepPreviousButtonID) as IButtonControl;
          return this._previousButton;
        }
        set => this._previousButton = value;
      }
    }

    private class DefaultSideBarTemplate : ITemplate
    {
      private InternalWizard _owner;

      internal DefaultSideBarTemplate(InternalWizard owner) => this._owner = owner;

      public void InstantiateIn(Control container)
      {
        HtmlGenericControl child1 = new HtmlGenericControl("ol");
        container.Controls.Add((Control) child1);
        Repeater child2;
        if (this._owner.SideBarRepeater == null)
        {
          child2 = new Repeater();
          child2.ID = InternalWizard.SideBarID;
          child2.ItemTemplate = (ITemplate) new InternalWizard.DefaultSideBarRepeaterItemTemplate(this._owner);
        }
        else
          child2 = this._owner.SideBarRepeater;
        child1.Controls.Add((Control) child2);
      }
    }

    private class DefaultSideBarRepeaterItemTemplate : ITemplate
    {
      private InternalWizard _owner;

      internal DefaultSideBarRepeaterItemTemplate(InternalWizard owner) => this._owner = owner;

      public void InstantiateIn(Control container)
      {
        HtmlGenericControl child = new HtmlGenericControl("li");
        child.DataBinding += new EventHandler(this.li_DataBinding);
        container.Controls.Add((Control) child);
      }

      private void li_DataBinding(object sender, EventArgs e)
      {
        HtmlGenericControl htmlGenericControl = (HtmlGenericControl) sender;
        RepeaterItem namingContainer = (RepeaterItem) htmlGenericControl.NamingContainer;
        InternalWizardStep dataItem = (InternalWizardStep) namingContainer.DataItem;
        htmlGenericControl.InnerText = dataItem.Title;
        if (namingContainer.ItemIndex == this._owner.ActiveStepIndex)
          htmlGenericControl.Attributes.Add("class", "sfSelStep" + namingContainer.ItemIndex.ToString());
        else
          htmlGenericControl.Attributes.Add("class", "sfStep" + namingContainer.ItemIndex.ToString());
      }
    }

    private sealed class DefaultNavigationTemplate : ITemplate
    {
      private InternalWizard _wizard;

      public DefaultNavigationTemplate(InternalWizard wizard) => this._wizard = wizard;

      void ITemplate.InstantiateIn(Control container)
      {
        LinkButton linkButton1 = new LinkButton();
        linkButton1.CausesValidation = false;
        linkButton1.CssClass = "sfWizardBack";
        linkButton1.ID = InternalWizard.StepPreviousButtonID;
        linkButton1.Visible = true;
        linkButton1.CommandName = InternalWizard.MovePreviousCommandName;
        linkButton1.TabIndex = this._wizard.TabIndex;
        linkButton1.Text = this._wizard.PreviousButtonText;
        this._wizard.RegisterCommandEvents((IButtonControl) linkButton1);
        container.Controls.Add((Control) linkButton1);
        HtmlGenericControl htmlGenericControl = new HtmlGenericControl("strong");
        LinkButton linkButton2 = new LinkButton();
        linkButton2.CausesValidation = true;
        linkButton2.CssClass = "sfLinkBtn sfSave";
        linkButton2.ID = InternalWizard.StepNextButtonID;
        linkButton2.Visible = true;
        linkButton2.CommandName = InternalWizard.MoveNextCommandName;
        linkButton2.TabIndex = this._wizard.TabIndex;
        this._wizard.RegisterCommandEvents((IButtonControl) linkButton2);
        container.Controls.Add((Control) linkButton2);
        HtmlGenericControl child1 = new HtmlGenericControl("strong");
        child1.Attributes.Add("class", "sfLinkBtnIn");
        child1.InnerText = this._wizard.NextButtonText;
        linkButton2.Controls.Add((Control) child1);
        LinkButton linkButton3 = new LinkButton();
        linkButton3.CausesValidation = true;
        linkButton3.CssClass = "sfLinkBtn sfSave";
        linkButton3.ID = InternalWizard.FinishButtonID;
        linkButton3.Visible = true;
        linkButton3.CommandName = InternalWizard.MoveCompleteCommandName;
        linkButton3.TabIndex = this._wizard.TabIndex;
        this._wizard.RegisterCommandEvents((IButtonControl) linkButton3);
        container.Controls.Add((Control) linkButton3);
        HtmlGenericControl child2 = new HtmlGenericControl("strong");
        child2.Attributes.Add("class", "sfLinkBtnIn");
        child2.InnerText = this._wizard.CompleteButtonText;
        linkButton3.Controls.Add((Control) child2);
      }
    }
  }
}
