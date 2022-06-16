// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.BinderContainer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Represents a client template object.</summary>
  [ParseChildren(true, "Markup")]
  public class BinderContainer : CompositeControl
  {
    private HtmlTextWriterTag containerTag;
    private HtmlTextWriterTag templateHolderTag;
    private bool renderContainer = true;

    /// <summary>
    /// Gets the name of the binder. This property is used by the ClientBinder and is not
    /// intended to be used by developers.
    /// </summary>
    public string BinderContainerId => this.ClientID + "_clientTemplate";

    /// <summary>
    /// Gets or sets a value indicating whether the dynamically generated container around the
    /// client template will be rendered. Note that without cotnainer, insert, update and delete
    /// functions will not work. By default, container is rendered.
    /// </summary>
    public bool RenderContainer
    {
      get => this.renderContainer;
      set => this.renderContainer = value;
    }

    /// <summary>Gets or sets the template.</summary>
    [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
    public string Markup { get; set; }

    /// <summary>
    /// Gets or sets the container tag which will wrap the client template.
    /// </summary>
    /// <remarks>
    /// Use HtmlTextWriterTag enumeration. For example, if your target is an unordered
    /// list, the ContainerTag should be "li".
    /// </remarks>
    public HtmlTextWriterTag ContainerTag
    {
      get
      {
        if (this.containerTag == HtmlTextWriterTag.Unknown)
          this.containerTag = HtmlTextWriterTag.Div;
        return this.containerTag;
      }
      set => this.containerTag = value;
    }

    /// <summary>
    /// Gets or sets the tag in which the client template is instantiated.
    /// </summary>
    /// <remarks>This property is different from the containerTag property,
    /// in so much that it is not actually used for instantiating a template, but
    /// only for holdnig it on the page. The importance of this property lays in the
    /// fact that templates such as option tag must reside in the select tag as parent
    /// even in the template, to ensure client templates work as expected and page validates.
    /// </remarks>
    public HtmlTextWriterTag TemplateHolderTag
    {
      get
      {
        if (this.templateHolderTag == HtmlTextWriterTag.Unknown)
          this.templateHolderTag = HtmlTextWriterTag.Div;
        return this.templateHolderTag;
      }
      set => this.templateHolderTag = value;
    }

    /// <summary>
    /// Gets or sets the data key names. DataKey names are the names of the properties
    /// which define the data item primary key. Use comma to separate keys, if data item
    /// is defined by more than one key.
    /// </summary>
    /// <remarks>
    /// Specifying data key names is obligatory for the automatic create, update and delete
    /// functions of the client binders.
    /// </remarks>
    public string DataKeyNames { get; set; }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the
    /// specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on
    /// the client.
    /// </summary>
    /// <param name="writer">
    /// An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream
    /// to render HTML content on the client.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.TemplateHolderTag == HtmlTextWriterTag.Tbody)
        writer.RenderBeginTag(HtmlTextWriterTag.Table);
      writer.AddAttribute(HtmlTextWriterAttribute.Id, this.BinderContainerId);
      writer.AddAttribute(HtmlTextWriterAttribute.Class, "sys-template");
      writer.RenderBeginTag(this.TemplateHolderTag);
      if (this.RenderContainer)
      {
        writer.AddAttribute(HtmlTextWriterAttribute.Class, "sys-container");
        writer.RenderBeginTag(this.ContainerTag);
      }
      writer.Write(ResourceParser.ParseResources(this.Markup));
      if (this.RenderContainer)
        writer.RenderEndTag();
      writer.RenderEndTag();
      if (this.TemplateHolderTag != HtmlTextWriterTag.Tbody)
        return;
      writer.RenderEndTag();
    }
  }
}
