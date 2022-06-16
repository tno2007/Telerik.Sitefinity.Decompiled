// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.DescriptionField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  public class DescriptionField : TextField
  {
    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      this.TitleLabel.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
      this.DescriptionLabel.Visible = true;
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      (scriptDescriptors.Last<ScriptDescriptor>() as ScriptControlDescriptor).Type = typeof (TextField).FullName;
      return scriptDescriptors;
    }
  }
}
