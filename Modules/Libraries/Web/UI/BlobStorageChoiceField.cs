// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorageChoiceField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  [FieldDefinitionElement(typeof (BlobStorageChoiceFieldElement))]
  public class BlobStorageChoiceField : ChoiceField
  {
    private string defaultProviderName;
    private ConfigElementDictionary<string, DataProviderSettings> provs;
    internal const string script = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.BlobStorageChoiceField.js";

    protected virtual string DefaultProvider
    {
      get
      {
        if (this.defaultProviderName == null)
          this.defaultProviderName = Config.Get<LibrariesConfig>().BlobStorage.DefaultProvider;
        return this.defaultProviderName;
      }
    }

    protected virtual ConfigElementDictionary<string, DataProviderSettings> Providers
    {
      get
      {
        if (this.provs == null)
          this.provs = Config.Get<LibrariesConfig>().BlobStorage.Providers;
        return this.provs;
      }
    }

    protected virtual string GetDefaultProviderTextLocalized(string text) => string.Format(Res.Get<LibrariesResources>().DefaultStorageProvider, (object) text);

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.RenderChoicesAs = RenderChoicesAs.DropDown;
    }

    protected override void InitializeControls(GenericContainer container)
    {
      this.PopulateProviderChoices();
      base.InitializeControls(container);
    }

    protected void PopulateProviderChoices()
    {
      this.Choices.Clear();
      if (this.Providers == null || this.Providers.Count <= 0)
        return;
      foreach (string key in (IEnumerable<string>) this.Providers.Keys)
      {
        ChoiceItem choiceItem = new ChoiceItem();
        choiceItem.Value = key;
        choiceItem.Text = !(this.Providers.GetElementByKey(key) is DataProviderSettings elementByKey) || string.IsNullOrWhiteSpace(elementByKey.Name) ? key : elementByKey.Name;
        choiceItem.Enabled = elementByKey.Enabled;
        if (this.DefaultProvider != null && key == this.DefaultProvider)
          choiceItem.Text = this.GetDefaultProviderTextLocalized(choiceItem.Text);
        if (elementByKey.Enabled)
          this.Choices.Add(choiceItem);
      }
      if (this.DefaultProvider == null)
        return;
      this.DefaultValue = (object) this.DefaultProvider;
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (BlobStorageChoiceField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.BlobStorageChoiceField.js", fullName)
      };
    }
  }
}
