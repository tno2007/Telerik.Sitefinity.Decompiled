// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ClientTemplatesHolder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Represents a container for client side templates.</summary>
  [ToolboxItem(false)]
  [ParseChildren(typeof (ClientTemplate))]
  public class ClientTemplatesHolder : Control
  {
    private const string script = "\r\nvar ClientTemplates = {\r\n    Sets: [],\r\n    GetSet: function(id){\r\n        if (this.Sets[id] == undefined){\r\n            this.Sets[id] = new TemplateSet(id);\r\n        }\r\n        return this.Sets[id];\r\n    }\r\n}\r\nfunction TemplateSet(id){\r\n    this.ID = id;\r\n    this.Templates = [];\r\n    this.Pattern = /\\{#\\w+([.]\\w+)?#}/g;\r\n}\r\nTemplateSet.prototype.Replace = function(templateName, dataItem){\r\n    var template = this.Templates[templateName];\r\n    if (template == undefined){\r\n        return null;  \r\n    }\r\n\tvar matches = template.match(this.Pattern);\r\n\tif(matches != null)\r\n    {\r\n        for (var j = 0; j < matches.length; j++) {\r\n\t\t    var name = matches[j];\r\n\t\t    name = name.slice(2, name.length - 2);\r\n\t\t    if(name.indexOf('.') > -1) {\r\n                var collectionName = name.split('.')[0];\r\n                var collectionItemKey = name.split('.')[1];\r\n                template = template.replace(matches[j], dataItem[collectionName][collectionItemKey]);\r\n            }\r\n            else {\r\n                template = template.replace(matches[j], dataItem[name]);\r\n            }\r\n\t    }\r\n    }\r\n    return template;\r\n}\r\nTemplateSet.prototype.AddTemplate = function(name, html){\r\n    this.Templates[name] = Base64.decode(html);\r\n}\r\nTemplateSet.prototype.GetTemplate = function(name){\r\n    return this.Templates[name];\r\n}\r\n";

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs" /> object that contains the event data.
    /// </param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        return;
      Type type = typeof (ClientTemplatesHolder);
      this.Page.ClientScript.RegisterClientScriptResource(type, "Telerik.Cms.Web.UI.Resources.common.js");
      this.Page.ClientScript.RegisterClientScriptBlock(type, "RegisterArray", "\r\nvar ClientTemplates = {\r\n    Sets: [],\r\n    GetSet: function(id){\r\n        if (this.Sets[id] == undefined){\r\n            this.Sets[id] = new TemplateSet(id);\r\n        }\r\n        return this.Sets[id];\r\n    }\r\n}\r\nfunction TemplateSet(id){\r\n    this.ID = id;\r\n    this.Templates = [];\r\n    this.Pattern = /\\{#\\w+([.]\\w+)?#}/g;\r\n}\r\nTemplateSet.prototype.Replace = function(templateName, dataItem){\r\n    var template = this.Templates[templateName];\r\n    if (template == undefined){\r\n        return null;  \r\n    }\r\n\tvar matches = template.match(this.Pattern);\r\n\tif(matches != null)\r\n    {\r\n        for (var j = 0; j < matches.length; j++) {\r\n\t\t    var name = matches[j];\r\n\t\t    name = name.slice(2, name.length - 2);\r\n\t\t    if(name.indexOf('.') > -1) {\r\n                var collectionName = name.split('.')[0];\r\n                var collectionItemKey = name.split('.')[1];\r\n                template = template.replace(matches[j], dataItem[collectionName][collectionItemKey]);\r\n            }\r\n            else {\r\n                template = template.replace(matches[j], dataItem[name]);\r\n            }\r\n\t    }\r\n    }\r\n    return template;\r\n}\r\nTemplateSet.prototype.AddTemplate = function(name, html){\r\n    this.Templates[name] = Base64.decode(html);\r\n}\r\nTemplateSet.prototype.GetTemplate = function(name){\r\n    return this.Templates[name];\r\n}\r\n", true);
    }

    /// <summary>
    /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.
    /// </summary>
    /// <param name="writer">
    /// The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\n<script type=\"text/javascript\">\n//<![CDATA[\nvar temps=ClientTemplates.GetSet(\"");
      stringBuilder.Append(this.ClientID);
      stringBuilder.Append("\");\n");
      foreach (Control control in this.Controls)
      {
        ClientTemplate clientTemplate = (ClientTemplate) control;
        if (clientTemplate != null)
        {
          using (HtmlTextWriter writer1 = this.GetWriter())
          {
            control.RenderControl(writer1);
            string s = writer1.InnerWriter.ToString().Trim(' ', '\n', '\r', '\t');
            stringBuilder.Append("temps.AddTemplate(\"");
            stringBuilder.Append(clientTemplate.Name);
            stringBuilder.Append("\",\"");
            stringBuilder.Append(Convert.ToBase64String(Encoding.UTF8.GetBytes(s)));
            stringBuilder.Append("\");\n");
          }
        }
      }
      stringBuilder.Append("//]]>\n</script>\n");
      writer.Write(stringBuilder.ToString());
    }

    private HtmlTextWriter GetWriter() => this.Context == null ? new HtmlTextWriter((TextWriter) new StringWriter((IFormatProvider) CultureInfo.CurrentCulture)) : this.Context.Request.Browser.CreateHtmlTextWriter((TextWriter) new StringWriter((IFormatProvider) CultureInfo.CurrentCulture));
  }
}
