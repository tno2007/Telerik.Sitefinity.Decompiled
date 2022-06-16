// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsByThreadGrid
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Backend
{
  public class CommentsByThreadGrid : KendoView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Comments.CommentsByThreadGrid.ascx");
    private const string viewScript = "Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Scripts.CommentsByThreadGrid.js";

    /// <summary>Gets or sets the thread key.</summary>
    /// <value>The thread key.</value>
    public string ThreadKey { get; set; }

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CommentsByThreadGrid.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// The script control type name passed to the <see cref="T:System.Web.UI.ScriptControlDescriptor" />. It defaults to the full name
    /// of the current object class. E.g. can be overriden to reuse the script of some of the base classes and just customize
    /// some controls server-side.
    /// </summary>
    protected virtual string ScriptDescriptorTypeName => this.GetType().FullName;

    /// <summary>
    /// Gets a reference to the div element that will be transformed into grid.
    /// </summary>
    protected virtual HtmlContainerControl Grid => this.Container.GetControl<HtmlContainerControl>("commentsGrid", true);

    private IThread CurrentThread { get; set; }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.Page.Request.QueryString == null || string.IsNullOrEmpty(this.Page.Request.QueryString["targetKey"]))
        return;
      this.ThreadKey = this.Page.Request.QueryString["targetKey"];
      this.CurrentThread = SystemManager.GetCommentsService().GetThread(this.ThreadKey);
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddElementProperty("grid", this.Grid.ClientID);
      string absolute = VirtualPathUtility.ToAbsolute("~/RestApi/comments-api");
      controlDescriptor.AddProperty("webServiceUrl", (object) absolute);
      controlDescriptor.AddProperty("threadKey", (object) this.ThreadKey);
      controlDescriptor.AddProperty("threadTitle", (object) this.CurrentThread.Title);
      controlDescriptor.AddProperty("commentsPerPage", (object) 50);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.Rating.rating.js", "Telerik.Sitefinity.Resources"),
      new ScriptReference("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Scripts.CommentsByThreadGrid.js", typeof (CommentsByThreadGrid).Assembly.FullName)
    };
  }
}
