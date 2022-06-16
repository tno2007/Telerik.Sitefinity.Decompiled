// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfileDetailsWindow
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration.Web.ViewModels;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>
  /// A base control managing a client window for creating and updating cache profiles.
  /// </summary>
  /// <seealso cref="T:Telerik.Sitefinity.Web.UI.SimpleScriptView" />
  public abstract class CacheProfileDetailsWindow : SimpleScriptView
  {
    internal const string JavaScriptReference = "Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.CacheProfileDetailsWindow.js";

    /// <summary>Gets the new profile.</summary>
    /// <value>The new profile.</value>
    internal abstract CacheProfileViewModel NewProfile { get; }

    /// <summary>Gets the HTML control containing the window body.</summary>
    /// <value>The window body.</value>
    protected abstract HtmlGenericControl WindowBody { get; }

    /// <summary>Gets the done button.</summary>
    /// <value>The done button.</value>
    protected abstract HtmlControl DoneButton { get; }

    /// <summary>Gets the cancel button.</summary>
    /// <value>The cancel button.</value>
    protected abstract HtmlControl CancelButton { get; }

    /// <summary>Gets the location drop down.</summary>
    /// <value>The location drop down.</value>
    protected abstract DropDownList LocationDropDown { get; }

    /// <summary>Gets the cache profiles settings service URL.</summary>
    /// <value>The service URL.</value>
    protected abstract string ServiceUrl { get; }

    /// <summary>
    /// The script control type name passed to the <see cref="T:System.Web.UI.ScriptControlDescriptor" />. It defaults to the full name
    /// of the current object class. E.g. can be overridden to reuse the script of some of the base classes and just customize
    /// some controls server-side.
    /// </summary>
    protected override string ScriptDescriptorTypeName => typeof (CacheProfileDetailsWindow).FullName;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (CacheProfileDetailsWindow).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("windowBody", this.WindowBody.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddProperty("serviceUrl", (object) this.ServiceUrl);
      controlDescriptor.AddProperty("newProfile", (object) this.NewProfile);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.CacheProfileDetailsWindow.js", typeof (CacheProfileDetailsWindow).Assembly.FullName)
    };

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container of the instantiated template.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.LocationDropDown.Items.AddRange(((IEnumerable<string>) Enum.GetNames(typeof (Telerik.Sitefinity.Web.OutputCacheLocation))).Select<string, ListItem>((Func<string, ListItem>) (l => new ListItem(Res.Get<ConfigDescriptions>(l), l))).ToArray<ListItem>());

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.
    /// </returns>
    /// <example>
    /// // The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.KendoWeb;
  }
}
