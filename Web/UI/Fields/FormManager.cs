// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.FormManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Provides controls with single registry mechanism in order to manage multi control dependant behavior.
  /// </summary>
  public class FormManager : ScriptControl
  {
    public const string script = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FormManager.js";
    public const string eventArgsScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FormManagerEventArgs.js";
    private const string allFormsValidationScript = "return Telerik.Sitefinity.Web.UI.Fields.FormManager.validateAll()";
    private Dictionary<string, string> controlIdMappings = new Dictionary<string, string>();
    private Dictionary<string, List<string>> validationGroupMappings = new Dictionary<string, List<string>>();

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      this.Initialize();
      base.OnInit(e);
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.FormManager" /> instance for a given <see cref="T:System.Web.UI.Page" />
    /// object.
    /// </summary>
    /// <returns>
    /// The current <see cref="T:Telerik.Sitefinity.Web.UI.Fields.FormManager" /> instance for the selected
    /// <see cref="T:System.Web.UI.Page" /> object, or null if no instance is defined.
    /// </returns>
    /// <param name="page">
    /// The page instance to retrieve the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.FormManager" />
    /// from.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="page" /> is null.
    /// </exception>
    public static FormManager GetCurrent(Page page)
    {
      if (page == null)
        throw new ArgumentNullException(Res.Get<ErrorMessages>().PageIsNull);
      return (FormManager) page.Items[(object) typeof (FormManager).FullName];
    }

    /// <summary>
    /// Registers validation on all form on the OnSubmit event of the page
    /// </summary>
    public void EnableValidateAll()
    {
      Type type = typeof (FormManager);
      string fullName = type.FullName;
      if (this.Page.ClientScript.IsOnSubmitStatementRegistered(type, fullName))
        return;
      this.Page.ClientScript.RegisterOnSubmitStatement(type, fullName, "return Telerik.Sitefinity.Web.UI.Fields.FormManager.validateAll()");
    }

    /// <summary>
    /// Registers the specified control mapping between server and clinet pageId.
    /// </summary>
    /// <param name="control">The control to register.</param>
    public void RegisterID(FieldControl control)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      if (control.ID != null && !this.controlIdMappings.ContainsKey(control.ID))
        this.controlIdMappings.Add(control.ID, control.ClientID);
      string key = string.IsNullOrEmpty(control.ValidationGroup) ? string.Empty : control.ValidationGroup;
      List<string> stringList;
      if (this.validationGroupMappings.TryGetValue(key, out stringList))
      {
        if (stringList.Contains(control.ClientID))
          return;
        stringList.Add(control.ClientID);
      }
      else
      {
        stringList = new List<string>() { control.ClientID };
        this.validationGroupMappings.Add(key, stringList);
      }
    }

    /// <summary>
    /// Unregisters the specified control mapping between server and clinet pageId.
    /// </summary>
    /// <param name="control">The control to unregister.</param>
    public void UnregisterID(Control control)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      if (control.ID == null)
        return;
      this.controlIdMappings.Remove(control.ID);
    }

    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// When overridden in a derived class, returns the <see cref="T:System.Web.UI.ScriptDescriptor" /> objects for the control.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptComponentDescriptor componentDescriptor = new ScriptComponentDescriptor(typeof (FormManager).FullName);
      componentDescriptor.AddProperty("_controlIdMappings", (object) this.controlIdMappings);
      componentDescriptor.AddProperty("_validationGroupMappings", (object) this.validationGroupMappings);
      return (IEnumerable<ScriptDescriptor>) new ScriptComponentDescriptor[1]
      {
        componentDescriptor
      };
    }

    /// <summary>
    /// When overridden in a derived class, returns the script files for the control.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection that contains ECMAScript (JavaScript) files that have been registered as embedded resources.
    /// </returns>
    protected override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (FormManager).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new ScriptReference[2]
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FormManagerEventArgs.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FormManager.js", fullName)
      };
    }

    /// <summary>Initializes this instance.</summary>
    private void Initialize()
    {
      if (this.Page == null)
        throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      if (this.DesignMode)
        return;
      FormManager current = FormManager.GetCurrent(this.Page);
      if (current != null)
      {
        this.controlIdMappings = current.controlIdMappings;
        this.validationGroupMappings = current.validationGroupMappings;
      }
      this.Page.Items[(object) typeof (FormManager).FullName] = (object) this;
    }
  }
}
