// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.UI.Designers.PublishingPointTypeSelectorField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Publishing.Web.UI.Designers
{
  public class PublishingPointTypeSelectorField : ChoiceField
  {
    protected override void InitializeControls(GenericContainer container)
    {
      IEnumerable<string> strings = PublishingSystemFactory.GetRegisteredPublishingPointNames().Where<string>((Func<string, bool>) (pp => pp != "ExportSiteSyncPersistentPublishingPoint"));
      this.Choices.Clear();
      foreach (string resourceKeyValue in strings)
      {
        string str = Res.ResolveLocalizedValue("PublishingMessages", resourceKeyValue);
        this.Choices.Add(new ChoiceItem()
        {
          Text = str,
          Value = resourceKeyValue
        });
      }
      base.InitializeControls(container);
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ((ScriptComponentDescriptor) scriptDescriptors.Last<ScriptDescriptor>()).Type = typeof (ChoiceField).FullName;
      return scriptDescriptors;
    }
  }
}
