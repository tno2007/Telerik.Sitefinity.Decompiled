// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.FormsSiteNode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Web;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Forms.Web
{
  /// <summary>Represents a SiteMap node for templates.</summary>
  public class FormsSiteNode : SiteMapNode, ISitefinitySiteMapNode
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.FormsSiteNode" /> class.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="formsProviderName">Name of the forms data provider.</param>
    /// <param name="formName">Name of the form.</param>
    public FormsSiteNode(SiteMapBase provider, string formsProviderName, string formName)
      : base((SiteMapProvider) provider, formName)
    {
      if (provider == null)
        throw new ArgumentNullException(nameof (provider));
      this.FormsProviderName = formsProviderName;
    }

    /// <summary>
    /// Gets or sets the URL of the page that the <see cref="T:System.Web.SiteMapNode" /> object represents.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The URL of the page that the node represents. The default is <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// The node is read-only.
    /// </exception>
    public override string Url
    {
      get
      {
        if (string.IsNullOrEmpty(this.FormsProviderName))
          return "~/" + "Sitefinity" + "/Forms/" + this.Key;
        return "~/" + "Sitefinity" + "/Forms/" + this.FormsProviderName + "/" + this.Key;
      }
      set => throw new NotSupportedException();
    }

    /// <summary>Gets the pageId of the node (Page / Taxon).</summary>
    /// <value>The pageId.</value>
    public Guid Id { get; private set; }

    /// <summary>Gets or sets the name of the forms data provider.</summary>
    /// <value>The name of the forms data provider.</value>
    public string FormsProviderName { get; private set; }

    /// <summary>Gets or sets the ordinal number of the node.</summary>
    /// <value>The ordinal.</value>
    public float Ordinal { get; set; }

    /// <summary>
    /// Gets or sets a collection of additional attributes beyond the strongly typed properties that are defined for the <see cref="T:System.Web.SiteMapNode" /> class.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of additional attributes for the <see cref="T:System.Web.SiteMapNode" /> beyond <see cref="P:System.Web.SiteMapNode.Title" />, <see cref="P:System.Web.SiteMapNode.Description" />, <see cref="P:System.Web.SiteMapNode.Url" />, and <see cref="P:System.Web.SiteMapNode.Roles" />; otherwise, null, if no attributes exist.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// The node is read-only.
    /// </exception>
    NameValueCollection ISitefinitySiteMapNode.Attributes => this.Attributes;
  }
}
