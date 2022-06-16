// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Designer.CommentsWidgetDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.ControlDesign.Selectors;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Designer
{
  /// <summary>
  /// Represents a designer for the <typeparamref name="SitefinityWebApp.Comments.CommentsWidget.Comments.CommentsWidget" /> widget
  /// </summary>
  public class CommentsWidgetDesigner : ControlDesignerBase
  {
    public static readonly string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Comments.CommentsWidgetDesigner.ascx";
    public const string scriptReference = "Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Designer.CommentsWidgetDesigner.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath(CommentsWidgetDesigner.layoutTemplateName);

    /// <summary>Obsolete. Use LayoutTemplatePath instead.</summary>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>Gets the layout template's relative or virtual path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CommentsWidgetDesigner.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected virtual TemplateSelector TemplateSelectorControl => this.Container.GetControl<TemplateSelector>("commentsTemplatesSelector", true);

    protected virtual TemplateSelector CommentsListViewTemplateSelectorControl => this.Container.GetControl<TemplateSelector>("commentsListViewTemplateSelector", true);

    protected virtual TemplateSelector CommentsSubmitFormTemplateSelectorControl => this.Container.GetControl<TemplateSelector>("commentsSubmitFormTemplateSelectorControl", true);

    protected override void InitializeControls(GenericContainer container)
    {
      this.TemplateSelectorControl.DesignedControlType = this.PropertyEditor.Control.GetType().FullName;
      this.CommentsListViewTemplateSelectorControl.DesignedControlType = typeof (CommentsListView).FullName;
      this.CommentsSubmitFormTemplateSelectorControl.DesignedControlType = typeof (CommentsSubmitForm).FullName;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("commentsTemplatesSelector", this.TemplateSelectorControl.ClientID);
      controlDescriptor.AddComponentProperty("commentsListViewTemplateSelector", this.CommentsListViewTemplateSelectorControl.ClientID);
      controlDescriptor.AddComponentProperty("commentsSubmitFormTemplateSelectorControl", this.CommentsSubmitFormTemplateSelectorControl.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of ScriptReference objects that define script resources that the control requires.
    /// </summary>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Designer.CommentsWidgetDesigner.js", typeof (CommentsWidgetDesigner).Assembly.GetName().ToString())
    };
  }
}
