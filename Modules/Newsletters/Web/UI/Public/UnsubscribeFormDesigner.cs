// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.UnsubscribeFormDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public
{
  /// <summary>
  /// User friendly control designer for the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.UnsubscribeForm" /> control.
  /// </summary>
  public class UnsubscribeFormDesigner : SubscriptionsDesignerBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Newsletters.Designers.UnsubscribeFormDesigner.ascx");
    internal new const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.Scripts.UnsubscribeFormDesigner.js";

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? UnsubscribeFormDesigner.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the reference to the mailing list selector control.
    /// </summary>
    protected virtual HtmlField HtmlTextControl => this.Container.GetControl<HtmlField>("htmlTextControl", true);

    /// <summary>Gets the reference to the merge tag selector control.</summary>
    protected virtual MergeTagSelector MergeTagSelector => this.Container.GetControl<MergeTagSelector>("htmlMergeTagSelector", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container.</param>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      foreach (MergeTag mergeTag in (IEnumerable<MergeTag>) NewslettersManager.GetManager().GetMergeTags())
      {
        if (!(mergeTag.PropertyName == "UnsubscribeLink"))
          this.MergeTagSelector.MergeTags.Add(mergeTag);
      }
      this.MergeTagSelector.MergeTags.Add(new MergeTag("Subscribe link", "SubscribeLink", typeof (MergeContextItems).Name));
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.</returns>
    /// <remarks></remarks>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("htmlMergeTagSelector", this.MergeTagSelector.ClientID);
      controlDescriptor.AddComponentProperty("htmlTextControl", this.HtmlTextControl.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.Scripts.UnsubscribeFormDesigner.js", typeof (UnsubscribeFormDesigner).Assembly.FullName)
    };
  }
}
