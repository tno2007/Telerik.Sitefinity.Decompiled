// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersContentBlockDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>
  /// Specialized designer for the newsletters content block control.
  /// </summary>
  public class NewslettersContentBlockDesigner : ContentBlockDesignerBase
  {
    private Guid messageBodyId;
    private new string layoutTemplatePath;
    public static readonly string defaultLayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.NewslettersContentBlockDesigner.ascx");
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.NewslettersContentBlockDesigner.js";

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value>The path of the layout template for this control.</value>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(this.layoutTemplatePath))
          this.layoutTemplatePath = NewslettersContentBlockDesigner.defaultLayoutTemplatePath;
        return this.layoutTemplatePath;
      }
      set => this.layoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the name of the javascript type that the designer will use.
    /// The designers can reuse for exampel the base class implementation and just customize some labels
    /// </summary>
    /// <value>The name of the script descriptor type.</value>
    protected override string ScriptDescriptorTypeName => typeof (NewslettersContentBlockDesigner).FullName;

    /// <summary>Gets the id of the campaign message body.</summary>
    protected virtual Guid MessageBodyId
    {
      get
      {
        if (this.messageBodyId == Guid.Empty)
          this.messageBodyId = PageManager.GetManager().GetDraft<PageDraft>(this.PropertyEditor.PageId).ParentId;
        return this.messageBodyId;
      }
    }

    /// <summary>Gets the reference to the html merge tag selector.</summary>
    protected virtual MergeTagSelector HtmlMergeTagSelector => this.Container.GetControl<MergeTagSelector>("htmlMergeTagSelector", true);

    /// <summary>Gets the reference to the content merge tag selector.</summary>
    [Obsolete("Use HtmlMergeTagSelector property")]
    protected virtual MergeTagSelector ContentMergeTagSelector => this.Container.GetControl<MergeTagSelector>("contentMergeTagSelector", false);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.BindMergeTagSelector(this.HtmlMergeTagSelector);
      if (this.ContentMergeTagSelector == null)
        return;
      this.BindMergeTagSelector(this.ContentMergeTagSelector);
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
      controlDescriptor.AddComponentProperty("htmlMergeTagSelector", this.HtmlMergeTagSelector.ClientID);
      if (this.ContentMergeTagSelector == null)
        return (IEnumerable<ScriptDescriptor>) source;
      controlDescriptor.AddComponentProperty("contentMergeTagSelector", this.ContentMergeTagSelector.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.NewslettersContentBlockDesigner.js", typeof (NewslettersContentBlockDesigner).Assembly.FullName)
    };

    private void BindMergeTagSelector(MergeTagSelector selector)
    {
      NewslettersManager manager = NewslettersManager.GetManager();
      Campaign campaign = manager.GetCampaigns().Where<Campaign>((Expression<Func<Campaign, bool>>) (c => c.MessageBody.Id == this.MessageBodyId)).SingleOrDefault<Campaign>();
      MailingList mailingList = campaign == null ? (MailingList) null : campaign.List;
      IList<MergeTag> mergeTags = manager.GetMergeTags(mailingList);
      selector.MergeTags.Clear();
      selector.MergeTags.AddRange((IEnumerable<MergeTag>) mergeTags);
    }
  }
}
