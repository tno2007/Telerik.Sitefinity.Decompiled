// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorage.BlobStorageProviderSettingsDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorage
{
  public class BlobStorageProviderSettingsDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.BlobStorageProviderSettingsDialog.ascx");
    private IBlobSettingsView settingsView;
    private string providerName;
    private bool isEditMode;
    private bool isSecondScreen;
    private string blobStorageType;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorage.BlobStorageProviderSettingsDialog" /> class.
    /// </summary>
    public BlobStorageProviderSettingsDialog() => this.LayoutTemplatePath = BlobStorageProviderSettingsDialog.layoutTemplatePath;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    public virtual PlaceHolder FirstScreenButtons => this.Container.GetControl<PlaceHolder>("firstScreenButtons", true);

    public virtual PlaceHolder SecondScreenButtons => this.Container.GetControl<PlaceHolder>("secondScreenButtons", true);

    public virtual PlaceHolder FirstScreenControls => this.Container.GetControl<PlaceHolder>("firstScreenControls", true);

    public virtual PlaceHolder SecondScreenControls => this.Container.GetControl<PlaceHolder>("secondScreenControls", true);

    public virtual Label SettingsForLabel => this.Container.GetControl<Label>("settingsForLabel", true);

    public virtual Message TestConnectionResult => this.Container.GetControl<Message>("testConnectionResult", true);

    public virtual TextField ProviderNameField => this.Container.GetControl<TextField>("nameField", true);

    public virtual LinkButton ContinueButton => this.Container.GetControl<LinkButton>("continueButton", true);

    public virtual LinkButton DoneButton => this.Container.GetControl<LinkButton>("doneButton", true);

    public virtual LinkButton BackButton => this.Container.GetControl<LinkButton>("backButton", true);

    public virtual ChoiceField ProviderTypeChoiceField => this.Container.GetControl<ChoiceField>("typeField", true);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    public virtual LinkButton TestConnectionButton => this.Container.GetControl<LinkButton>("testConnectionButton", true);

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      if (this.Page == null)
        return;
      this.Page.RegisterRequiresControlState((Control) this);
    }

    /// <summary>
    /// Restores control state information from a previous page request that was saved by the SaveControlState method.
    /// </summary>
    /// <param name="savedState"></param>
    protected override void LoadControlState(object savedState)
    {
      object[] objArray = (object[]) savedState;
      this.blobStorageType = (string) objArray[0];
      this.providerName = (string) objArray[1];
      this.isSecondScreen = (bool) objArray[2];
      this.isEditMode = (bool) objArray[3];
    }

    /// <summary>
    /// Saves server control state changes that have occurred since the time the page was posted back to the server.
    /// </summary>
    /// <returns>Returns the server control's current state.</returns>
    protected override object SaveControlState() => (object) new object[4]
    {
      (object) this.blobStorageType,
      (object) this.providerName,
      (object) this.isSecondScreen,
      (object) this.isEditMode
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      this.DoneButton.Click += new EventHandler(this.DoneButton_Click);
      this.TestConnectionButton.Click += new EventHandler(this.TestConnectionButton_Click);
      this.ContinueButton.Click += new EventHandler(this.ContinueButton_Click);
      this.BackButton.Click += new EventHandler(this.BackButton_Click);
      this.PopulateProviderTypeChoiceField();
      LibrariesConfig librariesConfig = Config.Get<LibrariesConfig>();
      if (!this.Page.IsPostBack)
      {
        this.providerName = this.Page.Request.QueryString["providerName"];
        if (!this.providerName.IsNullOrEmpty())
        {
          this.isEditMode = true;
          this.isSecondScreen = true;
          DataProviderSettings provider = librariesConfig.BlobStorage.Providers[this.providerName];
          if (provider != null)
          {
            string fullName = provider.ProviderType.FullName;
            foreach (BlobStorageTypeConfigElement typeConfigElement in (IEnumerable<BlobStorageTypeConfigElement>) librariesConfig.BlobStorage.BlobStorageTypes.Values)
            {
              if (typeConfigElement.ProviderType.FullName == fullName)
              {
                this.blobStorageType = typeConfigElement.Name;
                break;
              }
            }
          }
        }
      }
      if (string.IsNullOrEmpty(this.blobStorageType))
        return;
      BlobStorageTypeConfigElement blobStorageType = librariesConfig.BlobStorage.BlobStorageTypes[this.blobStorageType];
      if (blobStorageType == null)
        return;
      string settingsViewTypeOrPath = blobStorageType.SettingsViewTypeOrPath;
      Control child = settingsViewTypeOrPath.IsNullOrEmpty() ? (Control) new EmptyBlobSettingsView() : (!settingsViewTypeOrPath.StartsWith("~/") ? (Control) Activator.CreateInstance(TypeResolutionService.ResolveType(settingsViewTypeOrPath)) : (Control) ControlUtilities.LoadControl(settingsViewTypeOrPath));
      this.SecondScreenControls.Controls.Add(child);
      this.settingsView = child as IBlobSettingsView;
      DataProviderSettings providerSettings;
      if (this.Page.IsPostBack || !librariesConfig.BlobStorage.Providers.TryGetValue(this.providerName, out providerSettings))
        return;
      this.settingsView.Settings = providerSettings.Parameters;
    }

    private void BackButton_Click(object sender, EventArgs e)
    {
      this.isSecondScreen = false;
      this.RecreateChildControls();
      this.ProviderNameField.Value = (object) this.providerName;
      foreach (ChoiceItem choice in this.ProviderTypeChoiceField.Choices)
      {
        if (choice.Value == this.blobStorageType)
        {
          choice.Selected = true;
          break;
        }
      }
    }

    private void ContinueButton_Click(object sender, EventArgs e)
    {
      string key = this.ProviderNameField.Value as string;
      if (Config.Get<LibrariesConfig>().BlobStorage.Providers[key] != null)
      {
        this.TestConnectionResult.ShowNegativeMessage(string.Format(Res.Get<LibrariesResources>().ProviderNameAlreadyUsed, (object) key));
      }
      else
      {
        this.providerName = this.ProviderNameField.Value as string;
        this.blobStorageType = this.ProviderTypeChoiceField.Value as string;
        this.isSecondScreen = true;
        this.RecreateChildControls();
      }
    }

    private void TestConnectionButton_Click(object sender, EventArgs e)
    {
      this.Page.Server.ScriptTimeout = 600;
      bool existing;
      DataProviderSettings providerSettings = this.GetCurrentProvider(Config.Get<LibrariesConfig>(), out existing);
      if (existing)
        providerSettings = this.CloneProviderSettings(providerSettings);
      this.CopyNewSettings(providerSettings);
      Exception error;
      if (this.TestConnection(providerSettings, out error))
        this.TestConnectionResult.ShowPositiveMessage(Res.Get<LibrariesResources>().SettingsAreValid);
      else
        this.TestConnectionResult.ShowNegativeMessage(string.Format(Res.Get<LibrariesResources>().ThereIsProblemWithTheSettings, (object) error.Message));
      this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "resizeDlg();", true);
    }

    private bool TestConnection(DataProviderSettings settings, out Exception error)
    {
      try
      {
        return DummyManager.CreateInstantProvider<BlobStorageProvider>(settings).TestConnection(out error);
      }
      catch (Exception ex)
      {
        error = ex;
        return false;
      }
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.FirstScreenControls.Visible = this.FirstScreenButtons.Visible = !this.isSecondScreen;
      this.SecondScreenControls.Visible = this.SecondScreenButtons.Visible = this.isSecondScreen;
      if (this.isEditMode)
      {
        this.BackButton.Visible = false;
        if (!string.IsNullOrWhiteSpace(this.providerName))
        {
          this.FirstScreenControls.Visible = this.FirstScreenButtons.Visible = false;
          this.SecondScreenControls.Visible = this.SecondScreenButtons.Visible = !this.FirstScreenControls.Visible;
          string providerTitle = this.GetProviderTitle(this.providerName);
          this.SettingsForLabel.Text = HttpUtility.HtmlEncode(string.Format(Res.Get<LibrariesResources>().SettingsFor, (object) providerTitle));
          this.SettingsForLabel.Visible = this.SecondScreenControls.Visible;
        }
      }
      if (this.providerName == null)
        return;
      string providerTitle1 = this.GetProviderTitle(this.providerName);
      this.SettingsForLabel.Text = HttpUtility.HtmlEncode(string.Format(Res.Get<LibrariesResources>().SettingsFor, (object) providerTitle1));
      this.SettingsForLabel.Visible = this.SecondScreenControls.Visible;
    }

    private void DoneButton_Click(object sender, EventArgs e)
    {
      this.SaveSettings();
      this.CloseDialog();
    }

    private void CloseDialog() => this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeDialog", "Sys.Application.add_load(function(){dialogBase.closeAndRebind();})", true);

    private void PopulateProviderTypeChoiceField()
    {
      this.ProviderTypeChoiceField.Choices.Clear();
      if (this.ProviderTypeChoiceField.Choices.Count != 0)
        return;
      foreach (BlobStorageTypeConfigElement configElement in (IEnumerable<BlobStorageTypeConfigElement>) Config.Get<LibrariesConfig>().BlobStorage.BlobStorageTypes.Values)
        this.ProviderTypeChoiceField.Choices.Add(new ChoiceItem()
        {
          Text = configElement.GetEffectiveTitle(),
          Value = configElement.Name,
          Enabled = true
        });
    }

    private void CopyNewSettings(DataProviderSettings provider)
    {
      NameValueCollection settings = this.settingsView.Settings;
      NameValueCollection parameters = provider.Parameters;
      foreach (object key in settings.Keys)
      {
        if (parameters[key as string] != null)
          parameters[key as string] = settings[key as string];
        else
          parameters.Add(key as string, settings[key as string]);
      }
    }

    private DataProviderSettings CloneProviderSettings(
      DataProviderSettings provider)
    {
      return new DataProviderSettings()
      {
        Name = provider.Name,
        ProviderType = provider.ProviderType,
        Parameters = new NameValueCollection(provider.Parameters)
      };
    }

    private DataProviderSettings GetCurrentProvider(
      LibrariesConfig config,
      out bool existing)
    {
      existing = true;
      if (config == null)
        config = Config.Get<LibrariesConfig>();
      DataProviderSettings currentProvider;
      if (!config.BlobStorage.Providers.TryGetValue(this.providerName, out currentProvider))
      {
        currentProvider = new DataProviderSettings((ConfigElement) config.BlobStorage.Providers);
        currentProvider.Name = this.providerName;
        currentProvider.ProviderType = config.BlobStorage.BlobStorageTypes[this.blobStorageType].ProviderType;
        existing = false;
      }
      return currentProvider;
    }

    private void SaveSettings()
    {
      ConfigManager manager = ConfigManager.GetManager();
      LibrariesConfig section = manager.GetSection<LibrariesConfig>();
      bool existing;
      DataProviderSettings currentProvider = this.GetCurrentProvider(section, out existing);
      this.CopyNewSettings(currentProvider);
      IList<string> stringList = (IList<string>) new List<string>();
      foreach (string allKey in currentProvider.Parameters.AllKeys)
      {
        if (string.IsNullOrEmpty(currentProvider.Parameters[allKey]))
          stringList.Add(allKey);
      }
      foreach (string name in (IEnumerable<string>) stringList)
        currentProvider.Parameters.Remove(name);
      if (!existing)
        section.BlobStorage.Providers.Add(currentProvider);
      manager.SaveSection((ConfigSection) section, true);
    }

    private string GetProviderTitle(string providerName)
    {
      string providerTitle = providerName;
      DataProviderSettings provider = Config.Get<LibrariesConfig>().BlobStorage.Providers[providerName];
      if (provider != null)
        providerTitle = provider.GetEffectiveTitle();
      return providerTitle;
    }
  }
}
