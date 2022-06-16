// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>
  /// Type that constructs literal widget user interface element. All widgets should inherit <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.IWidget" /> interface.
  /// </summary>
  public class CommandWidget : Widget, IWidget
  {
    private const string notCorrectInterface = "The Definition of CommandWidget control does not implement ICommandWidgetDefinition interface.";
    private const string notDefinedCommandButtonType = "The CommandButtonType at definiton of CommandWidget must be assigned CommandButtonType enumeration, different than CommandButtonType.None.";
    private const string DefaultDisabledCssClass = "sfDisabledLinkBtn";
    internal string ButtonClientId;
    internal bool commandEnabled = true;
    public const string ICommandWidgetScriptPath = "Telerik.Sitefinity.Web.SitefinityJS.UI.ICommandWidget.js";
    public const string CommandWidgetScriptPath = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.CommandWidget.js";

    /// <summary>
    /// Gets or sets the definition for constuction the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget" /> control.
    /// </summary>
    /// <value>The definition.</value>
    public new virtual IWidgetDefinition Definition { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? this.DetermineTemplatePath() : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the widget should issue a command.
    /// </summary>
    /// <value><c>true</c> if command is enabled; otherwise, <c>false</c>.</value>
    public bool CommandEnabled
    {
      get => this.commandEnabled;
      set => this.commandEnabled = value;
    }

    /// <summary>
    /// Gets or sets the name of the command that widget fires.
    /// </summary>
    public virtual string CommandName { get; set; }

    /// <summary>Gets or sets the command argument</summary>
    public virtual string CommandArgument { get; set; }

    /// <summary>
    /// Gets or sets the type of the commmand button that ought to represent the command widget
    /// </summary>
    public virtual CommandButtonType ButtonType { get; set; }

    /// <summary>
    /// Gets or sets the Navigate Url for the command button to redirect.
    /// </summary>
    public virtual string NavigateUrl { get; set; }

    /// <summary>
    /// Gets or sets a secured object related to this widget bar, for applying permissions to the secured widgets.
    /// </summary>
    /// <value>The related secured object.</value>
    public ISecuredObject RelatedSecuredObject { get; set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget" /> is clickable.
    /// </summary>
    /// <value><c>true</c> if clickable; otherwise, <c>false</c>.</value>
    protected virtual bool Clickable => true;

    /// <summary>
    /// Gets or sets the  CSS class to set when the widget is disabled.
    /// </summary>
    /// <value>The disabled CSS class.</value>
    protected new virtual string DisabledCssClass { get; set; }

    /// <summary>Gets or sets the button CSS class.</summary>
    /// <value>The button CSS class.</value>
    protected virtual string ButtonCssClass { get; set; }

    /// <summary>
    /// Determines this command is a filter command (e.g. a filter on the sidebar)
    /// </summary>
    protected internal virtual bool IsFilterCommand { get; set; }

    /// <summary>Gets the link button control.</summary>
    public HtmlAnchor Button => this.Container.GetControl<HtmlAnchor>("buttonLink", false);

    /// <summary>Configures the specified definition.</summary>
    /// <param name="definition">The definition.</param>
    public override void Configure(IWidgetDefinition definition)
    {
      if (!typeof (ICommandWidgetDefinition).IsAssignableFrom(definition.GetType()))
        throw new InvalidOperationException("The Definition of CommandWidget control does not implement ICommandWidgetDefinition interface.");
      base.Configure(definition);
      this.Definition = definition;
      ICommandWidgetDefinition definition1 = this.Definition as ICommandWidgetDefinition;
      this.CommandName = definition1.CommandName;
      this.CommandArgument = definition1.CommandArgument;
      this.ButtonType = definition1.ButtonType;
      this.NavigateUrl = definition1.NavigateUrl;
      this.ButtonCssClass = definition1.ButtonCssClass;
      this.IsFilterCommand = definition1.IsFilterCommand;
      bool flag = definition1.Visible.HasValue && !definition1.Visible.Value;
      if (!this.IsCurrentUserUnrestricted() && !flag && definition is ISecuredCommandWidget securedCommandWidget)
      {
        ISecuredObject relatedSecuredObject = (ISecuredObject) null;
        if (this.RelatedSecuredObject == null && !securedCommandWidget.RelatedSecuredObjectId.IsNullOrWhitespace() && !securedCommandWidget.RelatedSecuredObjectTypeName.IsNullOrWhitespace())
        {
          Type itemType = WcfHelper.ResolveEncodedTypeName(securedCommandWidget.RelatedSecuredObjectTypeName);
          Guid result;
          if (Guid.TryParse(securedCommandWidget.RelatedSecuredObjectId, out result))
          {
            IManager manager = securedCommandWidget.RelatedSecuredObjectManagerTypeName.IsNullOrWhitespace() ? ManagerBase.GetMappedManager(itemType, securedCommandWidget.RelatedSecuredObjectProviderName) : ManagerBase.GetManager(securedCommandWidget.RelatedSecuredObjectManagerTypeName, securedCommandWidget.RelatedSecuredObjectProviderName);
            if (manager != null)
            {
              bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
              manager.Provider.SuppressSecurityChecks = true;
              object obj = manager.GetItem(itemType, result);
              manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
              if (obj != null)
                relatedSecuredObject = obj as ISecuredObject;
            }
          }
        }
        else
          relatedSecuredObject = this.RelatedSecuredObject;
        if (relatedSecuredObject != null)
          this.Visible = ((ISecuredCommandWidget) definition).IsAllowed(relatedSecuredObject);
      }
      if (!this.Visible)
        return;
      foreach (ICommandWidgetRestrictionStrategy restrictionStrategy in ObjectFactory.Container.ResolveAll<ICommandWidgetRestrictionStrategy>())
      {
        Type contentTypeContext = this.GetContentTypeContext();
        ICommandWidgetDefinition def = definition1;
        Type contentType = contentTypeContext;
        if (restrictionStrategy.IsRestricted(def, contentType))
        {
          this.Visible = false;
          break;
        }
      }
    }

    protected virtual Type GetContentTypeContext() => this.Context.Items[(object) "sf_content_type_context_item"] as Type;

    protected virtual bool IsCurrentUserUnrestricted() => ClaimsManager.IsUnrestricted();

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      this.ConstructWidget();
      base.OnPreRender(e);
    }

    /// <summary>Constructs the widget.</summary>
    protected internal virtual void ConstructWidget()
    {
      ITextControl control = this.Container.GetControl<ITextControl>("buttonText", false);
      if (control != null)
        control.Text = this.GetLabel(this.ResourceClassId, this.Text);
      if (this.Button != null)
      {
        if (!string.IsNullOrEmpty(this.Text))
          ControlUtilities.SetControlIdFromName(this.Text, (Control) this.Button);
        if (string.IsNullOrEmpty(this.NavigateUrl))
          this.Button.Attributes["onclick"] = "return false;";
        else if (this.Definition is ICommandWidgetDefinition definition && definition.OpenInSameWindow)
          this.Button.Attributes["href"] = this.NavigateUrl;
        else
          this.Button.Attributes["onclick"] = "window.open('" + this.NavigateUrl + "'); return false;";
        this.Button.Attributes["tabindex"] = this.TabIndex.ToString();
        if (!string.IsNullOrEmpty(this.ButtonCssClass))
          this.Button.Attributes["class"] = this.ButtonCssClass;
      }
      this.TabIndex = (short) -1;
    }

    /// <summary>
    /// Determines the name of the template depending of ButtonType enumeration value.
    /// ButtonType is a property of Command Widget Definition.
    /// </summary>
    /// <returns></returns>
    protected internal virtual string DetermineTemplatePath()
    {
      string empty = string.Empty;
      switch (this.ButtonType)
      {
        case CommandButtonType.Standard:
          return CommandWidget.ButtonTemplates.Standard;
        case CommandButtonType.Cancel:
          return CommandWidget.ButtonTemplates.Cancel;
        case CommandButtonType.Create:
          return CommandWidget.ButtonTemplates.Create;
        case CommandButtonType.Save:
          return CommandWidget.ButtonTemplates.Save;
        case CommandButtonType.SaveAndContinue:
          return CommandWidget.ButtonTemplates.SaveAndContinue;
        case CommandButtonType.SimpleLinkButton:
          return CommandWidget.ButtonTemplates.SimpleLink;
        default:
          throw new ArgumentException("The CommandButtonType at definiton of CommandWidget must be assigned CommandButtonType enumeration, different than CommandButtonType.None.");
      }
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      CommandWidget commandWidget = this;
      // ISSUE: reference to a compiler-generated method
      ScriptBehaviorDescriptor behaviorDescriptor = commandWidget.\u003C\u003En__0().First<ScriptDescriptor>() as ScriptBehaviorDescriptor;
      behaviorDescriptor.AddProperty("_clickable", (object) commandWidget.Clickable);
      behaviorDescriptor.AddProperty("_spanClientId", (object) commandWidget.ClientID);
      if (commandWidget.Button != null)
        behaviorDescriptor.AddProperty("_linkClientId", (object) commandWidget.Button.ClientID);
      behaviorDescriptor.AddProperty("_isFilterCommand", (object) commandWidget.IsFilterCommand);
      ITextControl control = commandWidget.Container.GetControl<ITextControl>("buttonText", false);
      if (control != null)
        behaviorDescriptor.AddElementProperty("buttonTextElem", ((Control) control).ClientID);
      behaviorDescriptor.AddProperty("_disabledCssClass", string.IsNullOrEmpty(commandWidget.DisabledCssClass) ? (object) "sfDisabledLinkBtn" : (object) commandWidget.DisabledCssClass);
      behaviorDescriptor.AddProperty("enabled", (object) commandWidget.CommandEnabled);
      behaviorDescriptor.AddProperty("_commandName", (object) commandWidget.CommandName);
      behaviorDescriptor.AddProperty("_commandArgument", (object) commandWidget.CommandArgument);
      yield return (ScriptDescriptor) behaviorDescriptor;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      CommandWidget commandWidget = this;
      // ISSUE: reference to a compiler-generated method
      foreach (ScriptReference scriptReference in commandWidget.\u003C\u003En__1())
        yield return scriptReference;
      yield return new ScriptReference()
      {
        Assembly = commandWidget.GetType().Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.SitefinityJS.UI.ICommandWidget.js"
      };
      yield return new ScriptReference()
      {
        Assembly = commandWidget.GetType().Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.CommandWidget.js"
      };
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => base.TagKey == HtmlTextWriterTag.Unknown ? HtmlTextWriterTag.Li : base.TagKey;

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ButtonTemplates
    {
      public static readonly string Create = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.CreateButton.ascx");
      public static readonly string Standard = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.NormalButton.ascx");
      public static readonly string Save = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.SaveButton.ascx");
      public static readonly string SaveAndContinue = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.SaveAndContinueButton.ascx");
      public static readonly string Cancel = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.CancelButton.ascx");
      public static readonly string SimpleLink = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.SimpleLinkButton.ascx");
    }
  }
}
