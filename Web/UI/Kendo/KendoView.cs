// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Kendo.KendoView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI.Kendo
{
  /// <summary>Abstract class for views that use Telerik Kendo</summary>
  public abstract class KendoView : SimpleView, IScriptControl
  {
    internal const string sitefinityCoreScript = "Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.sitefinity.core.js";
    internal const string sitefinityFormScript = "Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.sitefinity.form.js";
    internal const string sitefinityMessageScript = "Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.sitefinity.message.js";
    internal const string sitefinityTemplatesScript = "Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.sitefinity.templates.js";
    internal const string sitefinityValidationScript = "Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.sitefinity.validation.js";

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs" /> object that contains the
    /// event data.
    /// </param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.JQuery | ScriptRef.KendoAll | ScriptRef.TelerikSitefinity).RegisterScriptControl<KendoView>(this);
    }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the
    /// specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents
    /// the output stream to render HTML content on the client.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript)
    /// client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" />
    /// objects.
    /// </returns>
    public virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[0];

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects
    /// that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" />
    /// objects.
    /// </returns>
    public virtual IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.sitefinity.core.js", typeof (KendoView).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.sitefinity.form.js", typeof (KendoView).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.sitefinity.templates.js", typeof (KendoView).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.sitefinity.validation.js", typeof (KendoView).Assembly.FullName)
    };
  }
}
