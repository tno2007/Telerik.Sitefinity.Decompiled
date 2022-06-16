// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MessageBodyViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>
  /// View model class representing <see cref="T:Telerik.Sitefinity.Newsletters.Model.MessageBody" />.
  /// </summary>
  [DataContract]
  public class MessageBodyViewModel
  {
    /// <summary>Gets the unique identity of the data item.</summary>
    /// <value>The id.</value>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the name of the message body.</summary>
    /// <remarks>Used when message body is a template.</remarks>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the type of the message body.</summary>
    [DataMember]
    public MessageBodyType MessageBodyType { get; set; }

    /// <summary>Gets or sets the text of the message body.</summary>
    /// <remarks>
    /// This property is only used when message body type is plain text or html text.
    /// </remarks>
    [DataMember]
    public string BodyText { get; set; }

    /// <summary>
    /// Gets or sets the plain text version of the email message.
    /// </summary>
    /// <value>The plain text version.</value>
    [DataMember]
    public string PlainTextVersion { get; set; }

    /// <summary>
    /// Gets or sets the id of the template used for the internal page.
    /// </summary>
    [DataMember]
    public Guid InternalPageTemplateId { get; set; }

    [DataMember]
    public WcfPageTemplate InternalPageTemplate { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather this message body is a template. It is
    /// a template if true; otherwise false.
    /// </summary>
    [DataMember]
    public bool IsTemplate { get; set; }

    /// <summary>
    /// Gets or sets the user friendly label representing the type of the template.
    /// </summary>
    [DataMember]
    public string TemplateTypeUX { get; set; }

    /// <summary>
    /// Gets or sets the raw source in html of the message body.
    /// </summary>
    [DataMember]
    public string RawSourceHtml { get; set; }

    /// <summary>
    /// Gets or sets the raw source in plain text of the message body.
    /// </summary>
    [DataMember]
    public string RawSourcePlainText { get; set; }
  }
}
