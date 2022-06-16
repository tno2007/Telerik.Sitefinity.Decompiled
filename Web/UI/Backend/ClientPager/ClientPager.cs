// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.ClientPager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  public class ClientPager : SimpleScriptView
  {
    private readonly RadGridBinder gridBinder;
    private const string clientPagerScript = "Telerik.Sitefinity.Web.UI.Backend.Scripts.Pager.ClientPager.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pager.ClientPager.ascx");

    public ClientPager(RadGridBinder gridBinder)
    {
      this.gridBinder = gridBinder;
      this.EnsureChildControls();
    }

    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ClientPager.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects
    /// that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" />
    /// objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Backend.Scripts.Pager.ClientPager.js", this.GetType().Assembly.FullName)
    };

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      Control control1 = this.Container.GetControl<Control>("numeric", true);
      Control control2 = this.Container.GetControl<Control>("info", true);
      Control control3 = this.Container.GetControl<Control>("cmdPrev", true);
      Control control4 = this.Container.GetControl<Control>("cmdNext", true);
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (ClientPager).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("gridBinder", this.gridBinder.ClientID);
      controlDescriptor.AddProperty("_numericDivId", (object) control1.ClientID);
      controlDescriptor.AddProperty("itemsGridPagerTextFormat", (object) Res.Get<ContentResources>().ItemsGridPagerTextFormat);
      controlDescriptor.AddElementProperty("infoDiv", control2.ClientID);
      controlDescriptor.AddElementProperty("cmdPrev", control3.ClientID);
      controlDescriptor.AddElementProperty("cmdNext", control4.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }
  }
}
