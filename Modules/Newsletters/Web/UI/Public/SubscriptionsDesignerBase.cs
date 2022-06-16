// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscriptionsDesignerBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public
{
  /// <summary>
  /// Base designer class for <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscribeForm" /> control.
  /// </summary>
  public abstract class SubscriptionsDesignerBase : ControlDesignerBase
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.Scripts.SubscriptionsDesignerBase.js";

    /// <summary>
    /// Gets the reference to the mailing list selector control.
    /// </summary>
    protected virtual RadComboBox MailingListSelector => this.Container.GetControl<RadComboBox>("mailingListSelector", false);

    /// <summary>Gets the reference to the widget title text field.</summary>
    protected virtual TextField WidgetTitle => this.Container.GetControl<TextField>("widgetTitle", false);

    /// <summary>
    /// Gets the reference to the widget description text field.
    /// </summary>
    protected virtual TextField WidgetDescription => this.Container.GetControl<TextField>("widgetDescription", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container.</param>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.MailingListSelector == null)
        return;
      foreach (MailingList mailingList in (IEnumerable<MailingList>) NewslettersManager.GetManager().GetMailingLists())
        this.MailingListSelector.Items.Add(new RadComboBoxItem((string) mailingList.Title, mailingList.Id.ToString()));
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      if (this.MailingListSelector != null)
        controlDescriptor.AddComponentProperty("mailingListRadCombo", this.MailingListSelector.ClientID);
      if (this.WidgetTitle != null)
        controlDescriptor.AddComponentProperty("widgetTitleTextField", this.WidgetTitle.ClientID);
      if (this.WidgetDescription == null)
        return (IEnumerable<ScriptDescriptor>) source;
      controlDescriptor.AddComponentProperty("widgetDescriptionTextField", this.WidgetDescription.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.Scripts.SubscriptionsDesignerBase.js", typeof (SubscriptionsDesignerBase).Assembly.FullName)
    };
  }
}
