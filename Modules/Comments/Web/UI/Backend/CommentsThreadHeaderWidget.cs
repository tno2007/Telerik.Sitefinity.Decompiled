// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsThreadHeaderWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Backend
{
  /// <summary>
  /// This is class that represent comments list view in backend
  /// </summary>
  public class CommentsThreadHeaderWidget : SimpleScriptView
  {
    private const string viewScript = "Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Scripts.CommentsThreadHeaderWidget.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Comments.CommentsThreadHeaderWidget.ascx");
    internal const string clickMenuScript = "Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js";
    private string serviceUrl = "/fakeservice/";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CommentsThreadHeaderWidget.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    private IThread CurrentThread { get; set; }

    public string ServiceUrl
    {
      get => this.serviceUrl;
      set => this.serviceUrl = value;
    }

    /// <summary>
    /// The script control type name passed to the <see cref="T:System.Web.UI.ScriptControlDescriptor" />. It defaults to the full name
    /// of the current object class. E.g. can be overriden to reuse the script of some of the base classes and just customize
    /// some controls server-side.
    /// </summary>
    protected new virtual string ScriptDescriptorTypeName => typeof (CommentsThreadHeaderWidget).FullName;

    /// <summary>Gets the thread title.</summary>
    /// <value>The thread title.</value>
    protected virtual Literal ThreadTitle => this.Container.GetControl<Literal>("threadTitle", false);

    /// <summary>Gets the date.</summary>
    /// <value>The date.</value>
    protected virtual SitefinityLabel Date => this.Container.GetControl<SitefinityLabel>("date", false);

    /// <summary>Gets the user.</summary>
    /// <value>The user.</value>
    protected virtual SitefinityLabel User => this.Container.GetControl<SitefinityLabel>("user", false);

    /// <summary>Gets the number of comments.</summary>
    /// <value>The number of comments.</value>
    protected virtual Literal NumberOfComments => this.Container.GetControl<Literal>("numberOfComments", false);

    protected virtual HtmlGenericControl RatingSection => this.Container.GetControl<HtmlGenericControl>("ratingSection", false);

    protected virtual Decimal AverageRating { set; get; }

    /// <summary>Gets the close comment caption.</summary>
    /// <value>The close comment caption.</value>
    protected virtual Literal CloseCommentCaption => this.Container.GetControl<Literal>("closeCommentCaption", false);

    /// <summary>Gets the actions list.</summary>
    /// <value>The actions list.</value>
    protected virtual HtmlGenericControl ActionsList => this.Container.GetControl<HtmlGenericControl>("actionsList", true);

    protected virtual HyperLink ViewItem => this.Container.GetControl<HyperLink>(nameof (ViewItem), true);

    /// <summary>Gets the client label manager control.</summary>
    protected virtual ClientLabelManager ClientLabelManagerControl => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container</param>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SetCurrentThread();
      if (this.CurrentThread == null)
        return;
      this.ViewItem.NavigateUrl = CommentsUtilities.GetCommentedItemUrl(this.CurrentThread);
      this.ViewItem.Text = Res.Get<CommentsResources>("ViewItem");
      this.ThreadTitle.Text = this.CurrentThread.Title;
      this.Date.Text = this.CurrentThread.DateCreated.ToString("dd MMM yyyy, HH:MM");
      this.User.Text = CommentsUtilities.ResolveAuthorInfo(this.CurrentThread.Author).Name;
      int num = SystemManager.GetCommentsService().GetComments(new CommentFilter()
      {
        ThreadKey = {
          this.CurrentThread.Key
        }
      }).Count<IComment>();
      if (num > 1)
        this.NumberOfComments.Text = num.ToString() + " " + Res.Get<CommentsResources>("CommentsPluralTypeName").ToLower();
      this.AverageRating = this.CurrentThread.AverageRating.GetValueOrDefault(0M);
      if (this.CurrentThread.IsClosed)
        this.CloseCommentCaption.Text = Res.Get<CommentsResources>("AllowComments");
      else
        this.CloseCommentCaption.Text = Res.Get<CommentsResources>("CloseComments");
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void SetCurrentThread()
    {
      string threadKey = this.Page.Request.QueryString["targetKey"];
      if (threadKey.IsNullOrEmpty())
        return;
      this.CurrentThread = SystemManager.GetCommentsService().GetThread(threadKey);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddElementProperty("actionsMenu", this.ActionsList.ClientID);
      if (this.CurrentThread != null)
        controlDescriptor.AddProperty("thread", (object) this.CurrentThread);
      string absolute = VirtualPathUtility.ToAbsolute("~/RestApi/comments-api");
      controlDescriptor.AddProperty("webServiceUrl", (object) absolute);
      controlDescriptor.AddProperty("allowCommentsText", (object) Res.Get<CommentsResources>("AllowComments"));
      controlDescriptor.AddProperty("closeCommentsText", (object) Res.Get<CommentsResources>("CloseComments"));
      controlDescriptor.AddElementProperty("numberOfCommentsSection", this.Container.GetControl<HtmlGenericControl>("numberOfCommentsSection", false).ClientID);
      controlDescriptor.AddProperty("averageRating", (object) this.AverageRating);
      controlDescriptor.AddElementProperty("ratingSection", this.RatingSection.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManagerControl.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string fullName = typeof (CommentsThreadHeaderWidget).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.Rating.rating.js", "Telerik.Sitefinity.Resources"));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Scripts.CommentsThreadHeaderWidget.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQueryUI;
  }
}
