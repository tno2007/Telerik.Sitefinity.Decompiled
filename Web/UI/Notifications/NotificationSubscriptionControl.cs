// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Notifications.NotificationSubscriptionControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI.Notifications
{
  /// <summary>
  /// Base control that handles the calls for notification subscription.
  /// </summary>
  public class NotificationSubscriptionControl : SimpleScriptView
  {
    internal const string ScriptName = "Telerik.Sitefinity.Web.UI.Notifications.NotificationSubscriptionControl.js";
    internal static readonly string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.NotificationSubscriptionControl.ascx";
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath(NotificationSubscriptionControl.layoutTemplateName);

    /// <summary>Obsolete. Use LayoutTemplatePath instead.</summary>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>Gets the layout template's relative or virtual path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? NotificationSubscriptionControl.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the item id that the user will be subscribed/unsubscribed for.
    /// </summary>
    /// <value>The item id.</value>
    public virtual string SubscriptionItemKey { get; set; }

    /// <summary>The subscribe URL</summary>
    public virtual string SubscribeUrl { set; get; }

    /// <summary>The unsubscribe URL</summary>
    public virtual string UnsubscribeUrl { set; get; }

    /// <summary>The check subscription status</summary>
    public virtual string CheckSubscriptionStatusUrl { set; get; }

    /// <summary>The subscribe anchor text</summary>
    public virtual string SubscribeAnchorText { set; get; }

    /// <summary>The unsubscribe literal text</summary>
    public virtual string UnsubscribeLiteralText { set; get; }

    /// <summary>The unsubscribe anchor text</summary>
    public virtual string UnsubscribeAnchorText { set; get; }

    /// <summary>The successfully subscribed literal text</summary>
    public virtual string SuccessfullySubscribedLiteralText { set; get; }

    /// <summary>The successfully subscribed anchor text</summary>
    public virtual string SuccessfullySubscribedAnchorText { set; get; }

    /// <summary>The successfully unsubscribed literal text</summary>
    public virtual string SuccessfullyUnsubscribedLiteralText { set; get; }

    /// <summary>The successfully unsubscribed anchor text</summary>
    public virtual string SuccessfullyUnsubscribedAnchorText { set; get; }

    private HtmlGenericControl SubscribeWrp => this.Container.GetControl<HtmlGenericControl>("subscribeWrp", true);

    private HtmlAnchor SubscribeAnchor => this.Container.GetControl<HtmlAnchor>("subscribe", true);

    private HtmlGenericControl UnsubscribeWrp => this.Container.GetControl<HtmlGenericControl>("unsubscribeWrp", true);

    private Literal UnsubscribeLiteral => this.Container.GetControl<Literal>("unsubscribeLiteral", true);

    private HtmlAnchor UnsubscribeAnchor => this.Container.GetControl<HtmlAnchor>("unsubscribe", true);

    private HtmlGenericControl SuccessfullySubscribedWrp => this.Container.GetControl<HtmlGenericControl>("successfullySubscribedWrp", true);

    private Literal SuccessfullySubscribedLiteral => this.Container.GetControl<Literal>("successfullySubscribedLiteral", true);

    private HtmlAnchor SuccessfullySubscribedAnchor => this.Container.GetControl<HtmlAnchor>("successfullySubscribed", true);

    private HtmlGenericControl SuccessfullyUnsubscribedWrp => this.Container.GetControl<HtmlGenericControl>("successfullyUnsubscribedWrp", true);

    private Literal SuccessfullyUnsubscribedLiteral => this.Container.GetControl<Literal>("successfullyUnsubscribedLiteral", true);

    private HtmlAnchor SuccessfullyUnsubscribedAnchor => this.Container.GetControl<HtmlAnchor>("successfullyUnsubscribed", true);

    protected override void InitializeControls(GenericContainer container)
    {
      this.SubscribeAnchor.InnerText = this.SubscribeAnchorText;
      this.UnsubscribeLiteral.Text = this.UnsubscribeLiteralText;
      this.UnsubscribeAnchor.InnerText = this.UnsubscribeAnchorText;
      this.SuccessfullySubscribedLiteral.Text = this.SuccessfullySubscribedLiteralText;
      this.SuccessfullySubscribedAnchor.InnerText = this.SuccessfullySubscribedAnchorText;
      this.SuccessfullyUnsubscribedLiteral.Text = this.SuccessfullyUnsubscribedLiteralText;
      this.SuccessfullyUnsubscribedAnchor.InnerText = this.SuccessfullyUnsubscribedAnchorText;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.TelerikSitefinity;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddProperty("subscribeUrl", (object) this.SubscribeUrl);
      controlDescriptor.AddProperty("unsubscribeUrl", (object) this.UnsubscribeUrl);
      controlDescriptor.AddProperty("checkSubscriptionStatus", (object) this.CheckSubscriptionStatusUrl);
      controlDescriptor.AddProperty("subscriptionItemKey", (object) this.SubscriptionItemKey);
      controlDescriptor.AddElementProperty("subscribeWrp", this.SubscribeWrp.ClientID);
      controlDescriptor.AddElementProperty("unsubscribeWrp", this.UnsubscribeWrp.ClientID);
      controlDescriptor.AddElementProperty("successfullySubscribedWrp", this.SuccessfullySubscribedWrp.ClientID);
      controlDescriptor.AddElementProperty("successfullyUnsubscribedWrp", this.SuccessfullyUnsubscribedWrp.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Notifications.NotificationSubscriptionControl.js", typeof (NotificationSubscriptionControl).Assembly.GetName().ToString())
    };
  }
}
