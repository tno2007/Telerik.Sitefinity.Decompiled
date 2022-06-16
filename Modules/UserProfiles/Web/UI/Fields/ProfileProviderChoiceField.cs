// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Fields.ProfileProviderChoiceField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.UserProfiles.Configuration;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Fields
{
  public class ProfileProviderChoiceField : ChoiceField
  {
    protected override void CreateChildControls()
    {
      foreach (DataProviderSettings provider in (ConfigElementCollection) Config.Get<UserProfilesConfig>().Providers)
      {
        if (provider.Enabled)
          this.Choices.Add(new ChoiceItem()
          {
            Text = provider.Name,
            Value = provider.Name
          });
      }
      if (this.Choices.Count <= 1)
        this.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
      base.CreateChildControls();
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ((ScriptComponentDescriptor) scriptDescriptors.Last<ScriptDescriptor>()).Type = typeof (ChoiceField).FullName;
      return scriptDescriptors;
    }
  }
}
