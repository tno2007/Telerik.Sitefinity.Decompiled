// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.MergeTagSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>User interface element for inserting merge tags.</summary>
  public class MergeTagSelector : SimpleScriptView
  {
    private List<MergeTag> mergeTags;
    private const string webServiceUrl = "~/Sitefinity/Services/Newsletters/Campaign.svc/mergetags/";
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.MergeTagSelector.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.MergeTagSelector.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MergeTagSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the collection of merge tags available by this control.
    /// </summary>
    public List<MergeTag> MergeTags
    {
      get
      {
        if (this.mergeTags == null)
          this.mergeTags = new List<MergeTag>();
        return this.mergeTags;
      }
    }

    /// <summary>Gets the instance of the newsletters manager.</summary>
    protected NewslettersManager NewslettersManager => NewslettersManager.GetManager();

    /// <summary>Gets the choice field with merge tags.</summary>
    protected virtual ChoiceField MergeTagChoiceField => this.Container.GetControl<ChoiceField>("mergeTagChoiceField", true);

    /// <summary>Gets the reference to the insert merge tag button.</summary>
    protected virtual LinkButton InsertMergeTagButton => this.Container.GetControl<LinkButton>("insertMergeTagButton", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    protected override void InitializeControls(GenericContainer container)
    {
      this.MergeTagChoiceField.Choices.Clear();
      foreach (MergeTag mergeTag in this.MergeTags)
        this.MergeTagChoiceField.Choices.Add(new ChoiceItem()
        {
          Text = mergeTag.Title,
          Value = mergeTag.ComposedTag
        });
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (MergeTagSelector).FullName, this.ClientID);
      controlDescriptor.AddProperty("_webServiceUrl", (object) this.Page.ResolveUrl("~/Sitefinity/Services/Newsletters/Campaign.svc/mergetags/"));
      controlDescriptor.AddComponentProperty("mergeTagChoiceField", this.MergeTagChoiceField.ClientID);
      controlDescriptor.AddElementProperty("insertMergeTagButton", this.InsertMergeTagButton.ClientID);
      controlDescriptor.AddProperty("_mustChooseMergeTagMessage", (object) Res.Get<NewslettersResources>().MustChooseMergeTagBeforeInserting);
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
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.MergeTagSelector.js", typeof (MergeTagSelector).Assembly.FullName)
    };
  }
}
