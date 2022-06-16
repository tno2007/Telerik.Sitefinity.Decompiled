// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorage.DatabaseBlobSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorage
{
  public class DatabaseBlobSettingsView : AjaxDialogBase, IBlobSettingsView
  {
    private ConfigElementDictionary<string, ConnStringSettings> connectionSettings;
    private NameValueCollection settings;
    private bool IsInitialized;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.DatabaseBlobSettingsView.ascx");
    private static readonly string defaultConnectionStringDropDownValue = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorage.DatabaseBlobSettingsView" /> class.
    /// </summary>
    public DatabaseBlobSettingsView() => this.LayoutTemplatePath = DatabaseBlobSettingsView.layoutTemplatePath;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    public virtual ChoiceField DatabaseConnection => this.Container.GetControl<ChoiceField>("databaseConnection", true);

    protected virtual ConfigElementDictionary<string, ConnStringSettings> Providers
    {
      get
      {
        if (this.connectionSettings == null)
          this.connectionSettings = Config.Get<DataConfig>().ConnectionStrings;
        return this.connectionSettings;
      }
    }

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.DatabaseConnection.Choices.Count != 0)
        return;
      this.PopulateDatabaseConnections();
      if (this.settings != null)
      {
        this.Settings = this.settings;
        this.settings = (NameValueCollection) null;
      }
      this.IsInitialized = true;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    public NameValueCollection Settings
    {
      get => new NameValueCollection()
      {
        {
          "connectionString",
          this.DatabaseConnection.Value as string
        }
      };
      set
      {
        if (!this.IsInitialized)
          this.settings = value;
        if (value["connectionString"] != null)
        {
          foreach (ChoiceItem choice in this.DatabaseConnection.Choices)
          {
            if (choice.Value == value["connectionString"])
            {
              choice.Selected = true;
              break;
            }
          }
        }
        else
        {
          foreach (ChoiceItem choice in this.DatabaseConnection.Choices)
          {
            if (choice.Value == DatabaseBlobSettingsView.defaultConnectionStringDropDownValue)
            {
              choice.Selected = true;
              break;
            }
          }
        }
      }
    }

    private void PopulateDatabaseConnections()
    {
      this.DatabaseConnection.Choices.Clear();
      if (this.Providers != null && this.Providers.Count > 0)
      {
        this.DatabaseConnection.Choices.Add(this.GetDefaultChoiceItem());
        foreach (string key in (IEnumerable<string>) this.Providers.Keys)
          this.DatabaseConnection.Choices.Add(new ChoiceItem()
          {
            Value = key,
            Text = !(this.Providers.GetElementByKey(key) is ConnStringSettings elementByKey) || string.IsNullOrWhiteSpace(elementByKey.Name) ? key : elementByKey.Name,
            Enabled = true
          });
      }
      else
      {
        if (ConfigurationManager.ConnectionStrings == null || ConfigurationManager.ConnectionStrings.Count <= 0)
          return;
        this.DatabaseConnection.Choices.Add(this.GetDefaultChoiceItem());
        foreach (object connectionString in (ConfigurationElementCollection) ConfigurationManager.ConnectionStrings)
        {
          if (connectionString is ConnectionStringSettings connectionStringSettings && connectionStringSettings.ElementInformation.IsPresent)
            this.DatabaseConnection.Choices.Add(new ChoiceItem()
            {
              Value = connectionStringSettings.Name,
              Text = connectionStringSettings.Name,
              Enabled = true
            });
        }
      }
    }

    private ChoiceItem GetDefaultChoiceItem() => new ChoiceItem()
    {
      Value = DatabaseBlobSettingsView.defaultConnectionStringDropDownValue,
      Text = Res.Get<LibrariesResources>().SitefinityDefaultConnection,
      Enabled = true
    };
  }
}
