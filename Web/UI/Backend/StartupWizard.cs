// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.StartupWizard
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Microsoft.Practices.Unity;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Environment;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.Decorators;
using Telerik.Sitefinity.Installer;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.UserProfiles.Configuration;
using Telerik.Sitefinity.Project.Configuration;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.Zip;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>
  /// A wizard control for initializing the project on startup
  /// </summary>
  public class StartupWizard : CompositeControl
  {
    /// <summary>
    /// Variable, used for initial user login after complete wizard is ready
    /// </summary>
    internal static string InitialLoginUserName = string.Empty;
    internal static string InitialLoginValidationKey = string.Empty;
    internal const string InitialLoginCookieName = "sf_initial_login_key";
    private static readonly object syncLock = new object();
    private static volatile bool isStarting = false;
    private RoleDataProvider defaultRoleProvider;
    private RoleDataProvider roleProvider;
    private ConfigProvider configProvider;
    private MembershipDataProvider membershipProvider;
    private UserProfileDataProvider userProfilesDataProvider;
    private ITemplate _databaseFaqTemplate;
    private ITemplate _administratorFaqTemplate;
    private ITemplate _siteTemplateFaqTemplate;
    private InternalWizard wizard;
    private const int startingFillFactor = 80;
    private StartupWizard.DatabasePane databasePanel;
    private StartupWizard.AdministratorPane adminPanel;

    /// <summary>Gets or sets the database FAQ template.</summary>
    /// <value>The database FAQ template.</value>
    [Browsable(false)]
    [DefaultValue(null)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (StartupWizard))]
    public virtual ITemplate DatabaseFaqTemplate
    {
      get => this._databaseFaqTemplate;
      set
      {
        this._databaseFaqTemplate = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets or sets the administrator FAQ template.</summary>
    /// <value>The administrator FAQ template.</value>
    [Browsable(false)]
    [DefaultValue(null)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (StartupWizard))]
    public virtual ITemplate AdministratorFaqTemplate
    {
      get => this._administratorFaqTemplate;
      set
      {
        this._administratorFaqTemplate = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets or sets the site template FAQ template.</summary>
    /// <value>The site template FAQ template.</value>
    [Browsable(false)]
    [DefaultValue(null)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (StartupWizard))]
    public virtual ITemplate SiteTemplateFaqTemplate
    {
      get => this._siteTemplateFaqTemplate;
      set
      {
        this._siteTemplateFaqTemplate = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      if (Bootstrapper.IsSystemInitialized)
      {
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        if (currentIdentity == null || !currentIdentity.IsUnrestricted)
        {
          SecurityManager.RedirectToLogin(SystemManager.CurrentHttpContext);
          return;
        }
      }
      base.OnInit(e);
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.Controls.Clear();
      this.wizard = new InternalWizard();
      if (!Bootstrapper.IsDataInitialized)
        this.wizard.WizardSteps.Add(this.CreateDatabaseStep());
      this.wizard.WizardSteps.Add(this.CreateAdministratorStep());
      this.wizard.NextButtonClick += new InternalWizardNavigationEventHandler(this.wizard_NextButtonClick);
      this.wizard.FinishButtonClick += new InternalWizardNavigationEventHandler(this.wizard_FinishButtonClick);
      this.Controls.Add((Control) this.wizard);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (StartupWizard.isStarting)
        return;
      lock (StartupWizard.syncLock)
      {
        if (StartupWizard.isStarting)
          return;
        StartupWizard.isStarting = true;
        this.EnsureChildControls();
        this.ProcessStartupConfig();
      }
    }

    private void wizard_FinishButtonClick(object sender, InternalWizardNavigationEventArgs e)
    {
      ConfigProvider.DisableSecurityChecks = true;
      StartupWizard.IStartupWizardStepPane currentPane;
      if (!this.ProcessWizardStep((InternalWizard) sender, e.CurrentStepIndex, out currentPane))
      {
        e.Cancel = true;
      }
      else
      {
        Bootstrapper.Stop();
        DataConfig dataConfig = Config.Get<DataConfig>();
        ConfigManager manager = ConfigManager.GetManager();
        if (manager.StorageMode == ConfigStorageMode.Database || !SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile))
        {
          IConnectionStringSettings connString;
          if (dataConfig.TryGetConnectionString(this.TempConnectionName, out connString))
          {
            this.AddConnectionToWebConfig(connString);
            dataConfig.AddDummyConnection(this.TempConnectionName, connString);
          }
        }
        else
        {
          manager.Provider.SuppressSecurityChecks = true;
          DataConfig section = manager.GetSection<DataConfig>();
          IConnectionStringSettings connString;
          if (dataConfig.TryGetConnectionString(this.TempConnectionName, out connString))
          {
            ConnStringSettings element = (ConnStringSettings) null;
            if (!section.ConnectionStrings.TryGetValue("Sitefinity", out element))
            {
              element = new ConnStringSettings((ConfigElement) section.ConnectionStrings);
              element.Name = "Sitefinity";
              section.ConnectionStrings.Add(element);
            }
            element.ConnectionString = connString.ConnectionString;
            element.ProviderName = connString.ProviderName;
            element.DatabaseType = connString.DatabaseType;
          }
          manager.Provider.SuppressSecurityChecks = true;
          manager.SaveSection((ConfigSection) section, true);
        }
        string url = "~/" + "Sitefinity" + "/";
        string str = string.Empty;
        int authenticationMode = (int) SecurityManager.AuthenticationMode;
        StartupWizard.AdministratorPane administratorPane = currentPane as StartupWizard.AdministratorPane;
        if (authenticationMode == 0 && administratorPane != null)
        {
          User user = administratorPane.GetUser();
          if (user != null)
            str = user.UserName;
        }
        using (new FileSystemModeRegion())
          this.FinalizeStartupConfig(url);
        SystemManager.CleanApplication();
        SystemManager.ClearCurrentTransactions();
        SitefinityHttpModule.BootstrapAsync();
        if (!str.IsNullOrEmpty())
        {
          StartupWizard.InitialLoginUserName = str;
          StartupWizard.InitialLoginValidationKey = new Random().Next(1, 9999).ToString();
          this.Page.Response.Cookies.Add(new HttpCookie("sf_initial_login_key", StartupWizard.InitialLoginValidationKey)
          {
            Expires = DateTime.UtcNow.AddHours(1.0)
          });
        }
        this.Page.Response.Redirect(url, true);
      }
    }

    public bool AddConnectionToWebConfig(IConnectionStringSettings newConnectionSetting)
    {
      string connectionString = newConnectionSetting.ConnectionString;
      if (newConnectionSetting.DatabaseType != DatabaseType.MsSql)
      {
        string str = connectionString.TrimEnd(';') + ";Backend=" + OABackendNameAttribute.GetName((Enum) newConnectionSetting.DatabaseType);
      }
      System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~/");
      ConnectionStringsSection section = (ConnectionStringsSection) configuration.GetSection("connectionStrings");
      ConnectionStringSettings settings = (ConnectionStringSettings) null;
      if (section.ConnectionStrings.Count > 0)
        settings = section.ConnectionStrings["Sitefinity"];
      if (settings == null)
      {
        settings = new ConnectionStringSettings("Sitefinity", newConnectionSetting.ConnectionString);
        section.ConnectionStrings.Add(settings);
      }
      else
        settings.ConnectionString = newConnectionSetting.ConnectionString;
      if (newConnectionSetting.DatabaseType != DatabaseType.MsSql)
        settings.ConnectionString = settings.ConnectionString.TrimEnd(';') + ";Backend=" + OABackendNameAttribute.GetName((Enum) newConnectionSetting.DatabaseType);
      settings.ProviderName = newConnectionSetting.ProviderName;
      configuration.Save(ConfigurationSaveMode.Modified);
      return true;
    }

    private void wizard_NextButtonClick(object sender, InternalWizardNavigationEventArgs e)
    {
      if (!this.ProcessWizardStep((InternalWizard) sender, e.CurrentStepIndex))
      {
        e.Cancel = true;
      }
      else
      {
        if (!this.IsExpressInstall)
          return;
        this.PopulateAndSkipAdminPanel();
      }
    }

    private bool ProcessWizardStep(InternalWizard wizard, int stepIndex) => this.ProcessWizardStep(wizard, stepIndex, out StartupWizard.IStartupWizardStepPane _);

    private bool ProcessWizardStep(
      InternalWizard wizard,
      int stepIndex,
      out StartupWizard.IStartupWizardStepPane currentPane)
    {
      currentPane = (StartupWizard.IStartupWizardStepPane) wizard.WizardSteps[stepIndex].Controls[0];
      return currentPane.Validate() && currentPane.DoWork();
    }

    private InternalWizardStep CreateDatabaseStep()
    {
      StartupWizard.DatabasePane child = new StartupWizard.DatabasePane(this);
      this.databasePanel = child;
      InternalWizardStep databaseStep = new InternalWizardStep();
      databaseStep.Title = child.Title;
      databaseStep.Controls.Add((Control) child);
      Control faq = this.CreateFAQ();
      databaseStep.Controls.Add(faq);
      if (this._databaseFaqTemplate == null)
        return databaseStep;
      this._databaseFaqTemplate.InstantiateIn(faq);
      return databaseStep;
    }

    private InternalWizardStep CreateAdministratorStep()
    {
      StartupWizard.AdministratorPane child = new StartupWizard.AdministratorPane(this);
      this.adminPanel = child;
      InternalWizardStep administratorStep = new InternalWizardStep();
      administratorStep.Title = child.Title;
      administratorStep.Activate += new EventHandler(this.step_Activate);
      administratorStep.Controls.Add((Control) child);
      Control faq = this.CreateFAQ();
      administratorStep.Controls.Add(faq);
      if (this._administratorFaqTemplate == null)
        return administratorStep;
      this._administratorFaqTemplate.InstantiateIn(faq);
      return administratorStep;
    }

    private void step_Activate(object sender, EventArgs e) => ((StartupWizard.AdministratorPane) ((Control) sender).Controls[0]).Initialize();

    private Control CreateFAQ()
    {
      HtmlGenericControl faq = new HtmlGenericControl("div");
      faq.Attributes.Add("class", "sfFaq");
      return (Control) faq;
    }

    protected string TempConnectionName => "Sitefinity";

    protected RoleDataProvider GetDefaultRoleProvider()
    {
      if (this.defaultRoleProvider == null)
      {
        string defaultProviderName = ManagerBase<SecurityDataProvider>.GetDefaultProviderName();
        SecurityConfig securityConfig = Config.Get<SecurityConfig>();
        string parameter = securityConfig.RoleProviders[defaultProviderName].Parameters["connectionString"];
        this.roleProvider = this.CreateInstantProvider<RoleDataProvider>(defaultProviderName, parameter ?? this.TempConnectionName, securityConfig.RoleProviders);
      }
      return this.defaultRoleProvider;
    }

    protected ConfigProvider GetConfigProvider()
    {
      if (this.configProvider != null)
        return this.configProvider;
      ConfigManager manager = ConfigManager.GetManager();
      IDataProviderSettings settings;
      if (manager.ProvidersSettingsInternal.TryGetValue(manager.DefaultProviderDelegate(), out settings))
        this.configProvider = this.CreateInstantProvider<ConfigProvider>(manager.DefaultProviderDelegate(), this.TempConnectionName, settings);
      return manager.Provider;
    }

    protected RoleDataProvider GetRoleProvider()
    {
      if (this.roleProvider == null)
      {
        string key = "AppRoles";
        SecurityConfig securityConfig = Config.Get<SecurityConfig>();
        this.roleProvider = this.CreateInstantProvider<RoleDataProvider>("AppRoles", securityConfig.RoleProviders[key].Parameters["connectionString"] ?? this.TempConnectionName, securityConfig.RoleProviders);
      }
      return this.roleProvider;
    }

    protected MembershipDataProvider GetMembershipProvider()
    {
      if (this.membershipProvider == null)
      {
        SecurityConfig securityConfig = Config.Get<SecurityConfig>();
        string membershipProvider = securityConfig.DefaultBackendMembershipProvider;
        string parameter = securityConfig.MembershipProviders[membershipProvider].Parameters["connectionString"];
        this.membershipProvider = this.CreateInstantProvider<MembershipDataProvider>(securityConfig.DefaultBackendMembershipProvider, parameter ?? this.TempConnectionName, securityConfig.MembershipProviders);
      }
      return this.membershipProvider;
    }

    protected UserProfileDataProvider GetUserProfilesProvider()
    {
      if (this.userProfilesDataProvider == null)
      {
        UserProfilesConfig userProfilesConfig = Config.Get<UserProfilesConfig>();
        string defaultProvider = userProfilesConfig.DefaultProvider;
        string parameter = userProfilesConfig.Providers[defaultProvider].Parameters["connectionString"];
        this.userProfilesDataProvider = this.CreateInstantProvider<UserProfileDataProvider>(userProfilesConfig.DefaultProvider, parameter ?? this.TempConnectionName, userProfilesConfig.Providers);
      }
      return this.userProfilesDataProvider;
    }

    protected IConnectionStringSettings ConnectionString
    {
      get
      {
        DataConfig dataConfig = Config.Get<DataConfig>();
        IConnectionStringSettings connString;
        return dataConfig.TryGetConnectionString(this.TempConnectionName, out connString) || dataConfig.TryGetConnectionString("Sitefinity", out connString) ? connString : (IConnectionStringSettings) null;
      }
      set
      {
        Config.Get<DataConfig>().AddDummyConnection(this.TempConnectionName, value);
        this.roleProvider = (RoleDataProvider) null;
        this.membershipProvider = (MembershipDataProvider) null;
      }
    }

    private T CreateInstantProvider<T>(
      string providerName,
      string connectionString,
      ConfigElementDictionary<string, DataProviderSettings> providers)
      where T : DataProviderBase
    {
      DataProviderSettings settings;
      return providers.TryGetValue(providerName, out settings) ? this.CreateInstantProvider<T>(providerName, connectionString, (IDataProviderSettings) settings) : default (T);
    }

    private T CreateInstantProvider<T>(
      string providerName,
      string connectionString,
      IDataProviderSettings settings)
      where T : DataProviderBase
    {
      Type type = typeof (DummyManager);
      T instantProvider = (T) ObjectFactory.Resolve(settings.ProviderType);
      NameValueCollection parameters = settings.Parameters;
      NameValueCollection config = new NameValueCollection(parameters.Count, (IEqualityComparer) StringComparer.Ordinal);
      config["description"] = settings.Description;
      config["resourceClassId"] = settings.ResourceClassId;
      foreach (string name in (NameObjectCollectionBase) parameters)
        config[name] = parameters[name];
      config[nameof (connectionString)] = connectionString;
      if (this.IsExpressInstall && typeof (T) == typeof (MembershipDataProvider))
        config["minRequiredPasswordLength"] = "0";
      ObjectFactory.Container.RegisterType(type, type, providerName.ToUpperInvariant(), (LifetimeManager) new HttpRequestLifetimeManager(), (InjectionMember) new InjectionConstructor(new object[1]
      {
        (object) providerName
      }));
      instantProvider.Initialize(providerName, config, type);
      instantProvider.SuppressSecurityChecks = true;
      return instantProvider;
    }

    private bool IsExpressInstall => Config.Get<StartupConfig>().Enabled;

    private void ProcessStartupConfig()
    {
      if (this.Page.IsPostBack || !this.IsExpressInstall)
        return;
      StartupConfig config = Config.Get<StartupConfig>();
      if (this.databasePanel != null)
      {
        this.databasePanel.Initialize(config);
        if (!this.ProcessWizardStep(this.wizard, 0))
        {
          if (!string.IsNullOrEmpty(config.DBType))
            return;
          this.StartupConfigSetInitialized();
        }
        else
        {
          this.wizard.SetActiveStep(1, new InternalWizardNavigationEventArgs(0, 1), false, true);
          this.wizard.SkipSteps(1);
          if (!this.PopulateAndSkipAdminPanel())
            return;
          this.wizard_FinishButtonClick((object) this.wizard, new InternalWizardNavigationEventArgs(1, 2));
        }
      }
      else
      {
        if (!this.PopulateAndSkipAdminPanel())
          return;
        this.wizard_FinishButtonClick((object) this.wizard, new InternalWizardNavigationEventArgs(0, 0));
      }
    }

    private bool PopulateAndSkipAdminPanel()
    {
      StartupConfig config = Config.Get<StartupConfig>();
      if (config.SkipAdminUser)
        this.adminPanel.SkipAdminUser = true;
      else if (string.IsNullOrEmpty(config.Email))
        return false;
      this.adminPanel.Initialize(config);
      return true;
    }

    private void StartupConfigSetInitialized() => Config.ElevatedUpdateSection<StartupConfig>((Action<StartupConfig>) (config =>
    {
      config.SiteUrl = UrlPath.ResolveAbsoluteUrl("~/");
      config.Initialized = true;
    }));

    private void FinalizeStartupConfig(string url)
    {
      if (!this.IsExpressInstall)
        return;
      Config.ElevatedUpdateSection<StartupConfig>((Action<StartupConfig>) (config =>
      {
        url = UrlPath.ResolveAbsoluteUrl(url);
        config.SiteUrl = url;
        if (!config.Enabled)
          return;
        config.Clear();
        config.Initialized = true;
        config.Enabled = false;
      }));
    }

    /// <summary>
    /// Clears the page content and sends the given content as response with status code 500.
    /// </summary>
    /// <param name="content">The content.</param>
    private void WriteErrorPage(string content)
    {
      Page page = this.Page;
      page.Response.ClearContent();
      page.Response.ClearHeaders();
      page.Response.StatusCode = 500;
      page.Response.Write(content);
      page.Response.End();
    }

    private class DatabasePane : CompositeControl, StartupWizard.IStartupWizardStepPane
    {
      private StartupWizard wizard;
      private string titleText;
      private string errorMessage;
      private CmsDbType dbType;
      private bool windowsAuth;
      private IConnectionStringSettings connectionString;
      private HtmlGenericControl title;
      private HtmlGenericControl dbOptions;
      private List<RadioButton> rblDatabase;
      private HtmlGenericControl messageControl;
      private HtmlGenericControl fsSqlExpressProps;
      private TextBox txtSqlExpressInstanceName;
      private HtmlGenericControl fsSqlServerProps;
      private TextBox txtSqlServerName;
      private TextBox txtSqlServerPort;
      private TextBox txtSqlDatabaseName;
      private TextBox txtSqlUsername;
      private TextBox txtSqlPassword;
      private CheckBox checkSqlWindowsLogin;
      private HtmlGenericControl liSqlUsername;
      private HtmlGenericControl liSqlPassword;
      private HtmlGenericControl fsOracleProps;
      private TextBox txtOracleDatabase;
      private TextBox txtOracleUsername;
      private TextBox txtOraclePassword;
      private HtmlGenericControl fsAzureProps;
      private TextBox txtAzureName;
      private TextBox txtAzurePort;
      private TextBox txtAzureUsername;
      private TextBox txtAzurePassword;
      private TextBox txtAzureDatabaseName;
      private HtmlGenericControl fsMySQLProps;
      private TextBox txtMySQLServer;
      private TextBox txtMySQLServerPort;
      private TextBox txtMySQLUsername;
      private TextBox txtMySQLPassword;
      private TextBox txtMySQLDatabase;
      private HtmlGenericControl fsPostgreSQLProps;
      private TextBox txtPostgreSQLServer;
      private TextBox txtPostgreSQLServerPort;
      private TextBox txtPostgreSQLUserId;
      private TextBox txtPostgreSQLPassword;
      private TextBox txtPostgreSQLDatabase;
      private HtmlGenericControl fsSqlServerCEProps;
      private TextBox txtSqlServerCEInstanceName;
      private const string MYSQL_DEFAULT_PORT = "3306";
      private const string SQLSERVER_DEFAULT_PORT = "1433";
      private const string DEFAULT_SQLEXPRESS_INSTANCE = "SQLExpress";
      private const string DEFAULT_SQLCE_INSTANCE = "Sitefinity.sdf";
      private const string POSTGRESQL_DEFAULT_PORT = "5432";

      public DatabasePane(StartupWizard wizard)
      {
        this.titleText = "Set Database";
        this.wizard = wizard;
      }

      public string Title => this.titleText;

      public CmsDbType DbType
      {
        get
        {
          if (this.dbType == CmsDbType.NotSet)
            this.dbType = this.GetSelectedDatabaseType();
          return this.dbType;
        }
      }

      public bool Validate()
      {
        if (this.GetSelectedDatabaseType() == CmsDbType.NotSet)
        {
          if (!this.wizard.IsExpressInstall)
            this.errorMessage = "Database type is not selected";
          return false;
        }
        string str = this.TestConnection();
        if (string.IsNullOrEmpty(str))
          return true;
        this.errorMessage = str;
        return false;
      }

      private string TestConnection()
      {
        this.AttemptCreateDatabaseFiles(this.DbType);
        Exception err;
        return !OpenAccessDecorator.TestConnection(this.GetConnectionString(), out err) && !(err is DatabaseNotFoundException) ? err.Message : string.Empty;
      }

      private bool AttemptCreateDatabaseFiles(CmsDbType dbType)
      {
        if (dbType == CmsDbType.SqlExpress)
        {
          string path = SystemManager.CurrentHttpContext.Server.MapPath("~/App_Data/Sitefinity.mdf");
          if (!File.Exists(path))
          {
            using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Telerik.Sitefinity.Resources.Database.SqlExpress.zip"))
            {
              if (manifestResourceStream != null)
              {
                using (ZipFile zipFile = ZipFile.Read(manifestResourceStream))
                {
                  if (zipFile.Entries.Count == 1)
                  {
                    using (FileStream fileStream = new FileStream(path, FileMode.Create))
                      zipFile.Entries[0].Extract((Stream) fileStream);
                  }
                }
              }
            }
          }
        }
        return true;
      }

      public bool DoWork()
      {
        this.wizard.ConnectionString = this.GetConnectionString();
        return true;
      }

      public IConnectionStringSettings GetConnectionString()
      {
        if (this.connectionString == null)
        {
          this.EnsureChildControls();
          this.connectionString = (IConnectionStringSettings) new DummyConnectionStringSettings(this.wizard.TempConnectionName);
          StringBuilder builder = new StringBuilder();
          switch (this.DbType)
          {
            case CmsDbType.SqlExpress:
              string str1 = this.txtSqlExpressInstanceName.Text;
              if (string.IsNullOrEmpty(str1))
                str1 = "SQLExpress";
              this.connectionString.ConnectionString = string.Format("Data Source=.\\{0};Integrated Security=True;User Instance=True;AttachDBFilename=|DataDirectory|Sitefinity.mdf", (object) str1);
              this.connectionString.ProviderName = "System.Data.SqlClient";
              this.connectionString.DatabaseType = DatabaseType.MsSql;
              break;
            case CmsDbType.SqlServer:
              builder.AppendFormat("data source={0}", (object) this.txtSqlServerName.Text);
              string str2 = this.txtSqlServerPort.Text.Trim();
              if (str2 != string.Empty && !str2.Equals("1433"))
                builder.AppendFormat(",{0}", (object) str2);
              if (this.checkSqlWindowsLogin.Checked || this.windowsAuth)
              {
                builder.Append(";Integrated Security=SSPI");
              }
              else
              {
                DbConnectionStringBuilder.AppendKeyValuePair(builder, "UID", this.txtSqlUsername.Text);
                DbConnectionStringBuilder.AppendKeyValuePair(builder, "PWD", this.txtSqlPassword.Text);
              }
              string str3 = this.txtSqlDatabaseName.Text.Trim();
              if (str3 != string.Empty)
                DbConnectionStringBuilder.AppendKeyValuePair(builder, "initial catalog", str3);
              this.connectionString.ConnectionString = builder.ToString();
              this.connectionString.ProviderName = "System.Data.SqlClient";
              this.connectionString.DatabaseType = DatabaseType.MsSql;
              break;
            case CmsDbType.SqlAzure:
              builder.AppendFormat("Server={0}", (object) this.txtAzureName.Text);
              string str4 = this.txtAzurePort.Text.Trim();
              if (str4 != string.Empty && !str4.Equals("1433"))
                builder.AppendFormat(",{0}", (object) str4);
              DbConnectionStringBuilder.AppendKeyValuePair(builder, "User ID", this.txtAzureUsername.Text);
              DbConnectionStringBuilder.AppendKeyValuePair(builder, "Password", this.txtAzurePassword.Text);
              string str5 = this.txtAzureDatabaseName.Text.Trim();
              if (str5 != string.Empty)
                DbConnectionStringBuilder.AppendKeyValuePair(builder, "Database", str5);
              builder.AppendFormat(";Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
              this.connectionString.ConnectionString = builder.ToString();
              this.connectionString.ProviderName = "System.Data.SqlClient";
              this.connectionString.DatabaseType = DatabaseType.SqlAzure;
              break;
            case CmsDbType.SqlServerCE:
              AppDomain.CurrentDomain.SetData("SQLServerCompactEditionUnderWebHosting", (object) true);
              this.connectionString.DatabaseType = DatabaseType.SqlCE;
              this.connectionString.ProviderName = "System.Data.SqlServerCe.4.0";
              if (string.IsNullOrEmpty(this.txtSqlServerCEInstanceName.Text))
                this.txtSqlServerCEInstanceName.Text = "Sitefinity.sdf";
              this.connectionString.ConnectionString = string.Format("Data Source=|DataDirectory|{0}", (object) this.txtSqlServerCEInstanceName.Text);
              break;
            case CmsDbType.Oracle:
              DbConnectionStringBuilder.AppendKeyValuePair(builder, "Data Source", this.txtOracleDatabase.Text);
              DbConnectionStringBuilder.AppendKeyValuePair(builder, "User Id", this.txtOracleUsername.Text);
              DbConnectionStringBuilder.AppendKeyValuePair(builder, "Password", this.txtOraclePassword.Text);
              this.connectionString.ConnectionString = builder.ToString();
              this.connectionString.ProviderName = "System.Data.OracleClient";
              this.connectionString.DatabaseType = DatabaseType.Oracle;
              break;
            case CmsDbType.MySQL:
              builder.AppendFormat("Server={0}", (object) this.txtMySQLServer.Text);
              string str6 = this.txtMySQLServerPort.Text.Trim();
              if (str6 != string.Empty && !str6.Equals("3306"))
                builder.AppendFormat(";Port={0}", (object) str6);
              DbConnectionStringBuilder.AppendKeyValuePair(builder, "Uid", this.txtMySQLUsername.Text);
              DbConnectionStringBuilder.AppendKeyValuePair(builder, "Pwd", this.txtMySQLPassword.Text);
              string str7 = this.txtMySQLDatabase.Text.Trim();
              if (str7 != string.Empty)
                DbConnectionStringBuilder.AppendKeyValuePair(builder, "Database", str7);
              builder.Append(";CharacterSet=utf8");
              this.connectionString.ConnectionString = builder.ToString();
              this.connectionString.DatabaseType = DatabaseType.MySQL;
              this.connectionString.ProviderName = string.Empty;
              break;
            case CmsDbType.PostgreSQL:
              builder.AppendFormat("Server={0}", (object) this.txtPostgreSQLServer.Text);
              string a = this.txtPostgreSQLServerPort.Text.Trim();
              if (!string.IsNullOrEmpty(a) && !string.Equals(a, "5432"))
                builder.AppendFormat(";Port={0}", (object) a);
              DbConnectionStringBuilder.AppendKeyValuePair(builder, "User Id", this.txtPostgreSQLUserId.Text);
              DbConnectionStringBuilder.AppendKeyValuePair(builder, "Password", this.txtPostgreSQLPassword.Text);
              string str8 = this.txtPostgreSQLDatabase.Text.Trim();
              if (!string.IsNullOrEmpty(str8))
                DbConnectionStringBuilder.AppendKeyValuePair(builder, "Database", str8);
              this.connectionString.ConnectionString = builder.ToString();
              this.connectionString.DatabaseType = DatabaseType.PostgreSql;
              break;
            default:
              this.connectionString = (IConnectionStringSettings) null;
              break;
          }
        }
        return this.connectionString;
      }

      protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

      protected override void AddAttributesToRender(HtmlTextWriter writer)
      {
        if (writer == null)
          throw new ArgumentNullException(nameof (writer));
        base.AddAttributesToRender(writer);
        writer.AddAttribute("class", "sfWizardForm");
      }

      protected override void CreateChildControls()
      {
        this.title = new HtmlGenericControl("h2");
        this.title.Attributes.Add("class", "sfStep0");
        this.Controls.Add((Control) this.title);
        this.messageControl = new HtmlGenericControl("p");
        this.messageControl.Attributes.Add("class", "sfFailure");
        this.messageControl.Visible = false;
        this.Controls.Add((Control) this.messageControl);
        HtmlGenericControl child1 = new HtmlGenericControl("fieldset");
        child1.Attributes.Add("class", "sfForm");
        this.Controls.Add((Control) child1);
        HtmlGenericControl child2 = new HtmlGenericControl("div");
        child2.Attributes.Add("class", "sfFormIn");
        child1.Controls.Add((Control) child2);
        HtmlGenericControl child3 = new HtmlGenericControl("h3");
        child3.InnerText = "What database would you like to use?";
        child2.Controls.Add((Control) child3);
        this.dbOptions = new HtmlGenericControl("ol");
        this.dbOptions.Attributes.Add("class", "sfRadioList");
        child2.Controls.Add((Control) this.dbOptions);
        this.rblDatabase = new List<RadioButton>();
        child2.Controls.Add(this.CreateSqlExpressSettingsPanel(CmsDbType.SqlExpress.ToString(), "Microsoft SQL Server Express"));
        child2.Controls.Add(this.CreateSqlServerSettingsPanel(CmsDbType.SqlServer.ToString(), "Microsoft SQL Server"));
        if (Config.SectionHandler.Environment.Platform == SitefinityEnvironmentPlatform.WindowsAzureWebSite)
          child2.Controls.Add(this.CreateAzureSettingsPanel(CmsDbType.SqlAzure.ToString(), "Microsoft SQL Azure"));
        child2.Controls.Add(this.CreateOracleSettingsPanel(CmsDbType.Oracle.ToString(), "Oracle"));
        child2.Controls.Add(this.CreateMySQLSettingsPanel(CmsDbType.MySQL.ToString(), "MySQL"));
        if (SystemManager.EnablePostgreSQLOnStartup)
          child2.Controls.Add(this.CreatePostgreSQLSettingsPanel(CmsDbType.PostgreSQL.ToString(), "<strong>(BETA)</strong> PostgreSQL"));
        IConnectionStringSettings connectionString = this.wizard.ConnectionString;
        if (connectionString == null || string.IsNullOrEmpty(connectionString.ConnectionString))
          return;
        SqlConnectionStringBuilder connBuilder = (SqlConnectionStringBuilder) null;
        if (!this.TryParseSqlConnectionString(connectionString.ConnectionString, out connBuilder))
          return;
        if (!string.IsNullOrEmpty(connBuilder.AttachDBFilename))
        {
          this.SelectRadio(CmsDbType.SqlExpress);
          string str = connBuilder.DataSource;
          if (str.StartsWith(".\\"))
            str = str.Substring(2);
          this.txtSqlExpressInstanceName.Text = str;
        }
        else
        {
          this.SelectRadio(CmsDbType.SqlServer);
          this.txtSqlServerName.Text = connBuilder.DataSource;
          this.txtSqlDatabaseName.Text = connBuilder.InitialCatalog;
          this.checkSqlWindowsLogin.Checked = connBuilder.IntegratedSecurity;
        }
      }

      private bool TryParseSqlConnectionString(
        string connectionString,
        out SqlConnectionStringBuilder connBuilder)
      {
        connBuilder = (SqlConnectionStringBuilder) null;
        try
        {
          connBuilder = new SqlConnectionStringBuilder(connectionString);
          return true;
        }
        catch (Exception ex)
        {
          return false;
        }
      }

      internal void Initialize(StartupConfig config)
      {
        if (string.IsNullOrEmpty(config.DBType))
          return;
        string dbType1 = config.DBType;
        CmsDbType cmsDbType = CmsDbType.SqlExpress;
        string strB1 = cmsDbType.ToString();
        if (string.Compare(dbType1, strB1, StringComparison.InvariantCultureIgnoreCase) == 0)
          this.SetSqlExpressInstanceName(config.SqlInstance);
        string dbType2 = config.DBType;
        cmsDbType = CmsDbType.SqlServer;
        string strB2 = cmsDbType.ToString();
        if (string.Compare(dbType2, strB2, StringComparison.InvariantCultureIgnoreCase) == 0)
        {
          if (config.SqlAuthUserName.IsNullOrEmpty())
            this.SetWindowsAuthentication(true);
          else
            this.SetSqlAuthCredentials(config.SqlAuthUserName, config.SqlAuthUserPassword);
          this.SetSqlInstanceName(config.SqlInstance, config.DBName);
        }
        string dbType3 = config.DBType;
        cmsDbType = CmsDbType.Oracle;
        string strB3 = cmsDbType.ToString();
        if (string.Compare(dbType3, strB3, StringComparison.InvariantCultureIgnoreCase) == 0)
          this.SetOracleInstanceName(config.OracleDatabase, config.OracleUsername, config.OraclePassword);
        string dbType4 = config.DBType;
        cmsDbType = CmsDbType.MySQL;
        string strB4 = cmsDbType.ToString();
        if (string.Compare(dbType4, strB4, StringComparison.InvariantCultureIgnoreCase) == 0)
          this.SetMysqlInstanceName(config.MysqlServer, config.MysqlPort, config.MysqlUsername, config.MysqlPassword, config.MysqlDatabase);
        string dbType5 = config.DBType;
        cmsDbType = CmsDbType.PostgreSQL;
        string strB5 = cmsDbType.ToString();
        if (string.Compare(dbType5, strB5, StringComparison.InvariantCultureIgnoreCase) != 0)
          return;
        this.SetPostgreSqlInstanceName(config.PostgreSqlServer, config.PostgreSqlPort, config.PostgreSqlUserId, config.PostgreSqlPassword, config.PostgreSqlDatabase);
      }

      private void SelectRadio(CmsDbType dbType)
      {
        foreach (RadioButton radioButton in this.rblDatabase)
        {
          if (radioButton.ID.Equals(dbType.ToString()))
            radioButton.Checked = true;
          else
            radioButton.Checked = false;
        }
      }

      protected override void OnPreRender(EventArgs e)
      {
        base.OnPreRender(e);
        if (!string.IsNullOrEmpty(this.titleText))
          this.title.InnerText = this.titleText;
        if (!string.IsNullOrEmpty(this.errorMessage))
        {
          if (this.wizard.IsExpressInstall)
          {
            this.wizard.WriteErrorPage(this.errorMessage.Replace("<br />", System.Environment.NewLine));
            return;
          }
          this.messageControl.Visible = true;
          this.messageControl.InnerHtml = this.errorMessage;
        }
        else
          this.messageControl.Visible = false;
        StringBuilder stringBuilder = new StringBuilder();
        int num = 0;
        foreach (RadioButton radioButton in this.rblDatabase)
        {
          string str = string.Empty;
          if (this.FindControl(radioButton.ID + "Props") is HtmlGenericControl control)
          {
            if (stringBuilder.Length > 0)
              stringBuilder.Append(",");
            stringBuilder.AppendFormat("'{0}'", (object) control.ClientID);
            control.Style["display"] = radioButton.Checked ? "" : "none";
            str = control.ClientID;
          }
          radioButton.Attributes["onclick"] = string.Format("EnableSetting('{0}', databaseSettings);", (object) str);
          ++num;
        }
        string script = "\r\nfunction EnableProperties(enable, ids)\r\n{\r\n\tvar display = enable ? '' : 'none';\r\n\tfor(var i = 0; i < ids.length; i++)\r\n\t{\r\n\t\tdocument.getElementById(ids[i]).style.display = display;\r\n\t}\r\n}\r\nfunction EnableSetting(id, ids)\r\n{\r\n\tfor(var i = 0; i < ids.length; i++)\r\n\t{\r\n\t\tvar el = document.getElementById(ids[i]);\r\n\t\tif (ids[i] == id)\r\n\t\t\tel.style.display = '';\r\n\t\telse\r\n\t\t\tel.style.display = 'none';\r\n\t}\r\n}\r\n\r\nvar sqlProps = new Array('" + this.liSqlUsername.ClientID + "','" + this.liSqlPassword.ClientID + "');\r\nvar databaseSettings = new Array(" + stringBuilder.ToString() + ");";
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "sqlProps", script, true);
        this.liSqlUsername.Style["display"] = this.liSqlPassword.Style["display"] = this.checkSqlWindowsLogin.Checked ? "none" : "";
      }

      private void AddDbOption(string name, string title)
      {
        HtmlGenericControl child1 = new HtmlGenericControl("li");
        RadioButton child2 = new RadioButton();
        child2.ID = name;
        child2.GroupName = "database";
        child2.Text = !string.IsNullOrEmpty(title) ? title : name;
        child2.AutoPostBack = false;
        child1.Controls.Add((Control) child2);
        this.rblDatabase.Add(child2);
        this.dbOptions.Controls.Add((Control) child1);
      }

      private Control CreateSqlExpressSettingsPanel(string dbName, string title)
      {
        this.AddDbOption(dbName, title);
        this.fsSqlExpressProps = new HtmlGenericControl("fieldset");
        this.fsSqlExpressProps.ID = dbName + "Props";
        this.fsSqlExpressProps.Attributes.Add("class", "sfSecondaryFormGroup");
        HtmlGenericControl child1 = new HtmlGenericControl("h4");
        child1.InnerText = "SQL Server Express connection properties";
        this.fsSqlExpressProps.Controls.Add((Control) child1);
        HtmlGenericControl child2 = new HtmlGenericControl("ol");
        this.fsSqlExpressProps.Controls.Add((Control) child2);
        HtmlGenericControl child3 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child3);
        FieldLabel child4 = new FieldLabel();
        child4.TargetID = "SqlExpressInstance";
        child4.Text = "Instance";
        child4.CssClass = "sfTxtLbl";
        child3.Controls.Add((Control) child4);
        this.txtSqlExpressInstanceName = new TextBox();
        this.txtSqlExpressInstanceName.Text = "SQLExpress";
        this.txtSqlExpressInstanceName.ID = "SqlExpressInstance";
        this.txtSqlExpressInstanceName.CssClass = "sfTxt";
        child3.Controls.Add((Control) this.txtSqlExpressInstanceName);
        return (Control) this.fsSqlExpressProps;
      }

      private Control CreateSqlServerSettingsPanel(string dbName, string title)
      {
        this.AddDbOption(dbName, title);
        this.fsSqlServerProps = new HtmlGenericControl("fieldset");
        this.fsSqlServerProps.ID = dbName + "Props";
        this.fsSqlServerProps.Attributes.Add("class", "sfSecondaryFormGroup");
        HtmlGenericControl child1 = new HtmlGenericControl("h4");
        child1.InnerText = "SQL Server connection properties";
        this.fsSqlServerProps.Controls.Add((Control) child1);
        HtmlGenericControl child2 = new HtmlGenericControl("ol");
        this.fsSqlServerProps.Controls.Add((Control) child2);
        HtmlGenericControl child3 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child3);
        child3.Attributes.Add("class", "sfCheckBox");
        this.checkSqlWindowsLogin = new CheckBox();
        this.checkSqlWindowsLogin.Text = "Windows Authentication";
        this.checkSqlWindowsLogin.ID = "LoginType";
        this.checkSqlWindowsLogin.Attributes["onclick"] = "EnableProperties(!this.checked, sqlProps);";
        child3.Controls.Add((Control) this.checkSqlWindowsLogin);
        HtmlGenericControl child4 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child4);
        FieldLabel child5 = new FieldLabel();
        child5.TargetID = "SqlServerName";
        child5.Text = "Server";
        child5.CssClass = "sfTxtLbl";
        child4.Controls.Add((Control) child5);
        this.txtSqlServerName = new TextBox();
        this.txtSqlServerName.ID = "SqlServerName";
        this.txtSqlServerName.CssClass = "sfTxt";
        child4.Controls.Add((Control) this.txtSqlServerName);
        HtmlGenericControl child6 = new HtmlGenericControl("li");
        child6.Attributes.Add("class", "sfShortField40");
        child2.Controls.Add((Control) child6);
        FieldLabel child7 = new FieldLabel();
        child7.TargetID = "SqlServerPort";
        child7.Text = "Port";
        child7.CssClass = "sfTxtLbl";
        child6.Controls.Add((Control) child7);
        this.txtSqlServerPort = new TextBox();
        this.txtSqlServerPort.ID = "SqlServerPort";
        this.txtSqlServerPort.CssClass = "sfTxt";
        this.txtSqlServerPort.Text = "1433";
        child6.Controls.Add((Control) this.txtSqlServerPort);
        this.liSqlUsername = new HtmlGenericControl("li");
        child2.Controls.Add((Control) this.liSqlUsername);
        FieldLabel child8 = new FieldLabel();
        child8.TargetID = "SqlUsername";
        child8.Text = "Username";
        child8.CssClass = "sfTxtLbl";
        this.liSqlUsername.Controls.Add((Control) child8);
        this.txtSqlUsername = new TextBox();
        this.txtSqlUsername.ID = "SqlUsername";
        this.txtSqlUsername.CssClass = "sfTxt";
        this.liSqlUsername.Controls.Add((Control) this.txtSqlUsername);
        this.liSqlPassword = new HtmlGenericControl("li");
        child2.Controls.Add((Control) this.liSqlPassword);
        FieldLabel child9 = new FieldLabel();
        child9.TargetID = "SqlPassword";
        child9.Text = "Password";
        child9.CssClass = "sfTxtLbl";
        this.liSqlPassword.Controls.Add((Control) child9);
        this.txtSqlPassword = new TextBox();
        this.txtSqlPassword.ID = "SqlPassword";
        this.txtSqlPassword.CssClass = "sfTxt";
        this.txtSqlPassword.TextMode = TextBoxMode.Password;
        this.liSqlPassword.Controls.Add((Control) this.txtSqlPassword);
        HtmlGenericControl child10 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child10);
        FieldLabel child11 = new FieldLabel();
        child11.TargetID = "SqlDatabase";
        child11.Text = "Database";
        child11.CssClass = "sfTxtLbl";
        child10.Controls.Add((Control) child11);
        this.txtSqlDatabaseName = new TextBox();
        this.txtSqlDatabaseName.ID = "SqlDatabase";
        this.txtSqlDatabaseName.CssClass = "sfTxt";
        child10.Controls.Add((Control) this.txtSqlDatabaseName);
        return (Control) this.fsSqlServerProps;
      }

      private Control CreateOracleSettingsPanel(string dbName, string title)
      {
        this.AddDbOption(dbName, title);
        this.fsOracleProps = new HtmlGenericControl("fieldset");
        this.fsOracleProps.ID = dbName + "Props";
        this.fsOracleProps.Attributes.Add("class", "sfSecondaryFormGroup");
        HtmlGenericControl child1 = new HtmlGenericControl("h4");
        child1.InnerText = "Oracle connection properties";
        this.fsOracleProps.Controls.Add((Control) child1);
        HtmlGenericControl child2 = new HtmlGenericControl("ol");
        this.fsOracleProps.Controls.Add((Control) child2);
        HtmlGenericControl child3 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child3);
        FieldLabel child4 = new FieldLabel();
        child4.TargetID = "OraDatabase";
        child4.Text = "DataSource";
        child4.CssClass = "sfTxtLbl";
        child3.Controls.Add((Control) child4);
        this.txtOracleDatabase = new TextBox();
        this.txtOracleDatabase.ID = "OraDatabase";
        this.txtOracleDatabase.CssClass = "sfTxt";
        child3.Controls.Add((Control) this.txtOracleDatabase);
        HtmlGenericControl child5 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child5);
        FieldLabel child6 = new FieldLabel();
        child6.TargetID = "OraUsername";
        child6.Text = "Username";
        child6.CssClass = "sfTxtLbl";
        child5.Controls.Add((Control) child6);
        this.txtOracleUsername = new TextBox();
        this.txtOracleUsername.ID = "OraUsername";
        this.txtOracleUsername.CssClass = "sfTxt";
        child5.Controls.Add((Control) this.txtOracleUsername);
        HtmlGenericControl child7 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child7);
        FieldLabel child8 = new FieldLabel();
        child8.TargetID = "OraPassword";
        child8.Text = "Password";
        child8.CssClass = "sfTxtLbl";
        child7.Controls.Add((Control) child8);
        this.txtOraclePassword = new TextBox();
        this.txtOraclePassword.TextMode = TextBoxMode.Password;
        this.txtOraclePassword.ID = "OraPassword";
        this.txtOraclePassword.CssClass = "sfTxt";
        child7.Controls.Add((Control) this.txtOraclePassword);
        return (Control) this.fsOracleProps;
      }

      private Control CreateMySQLSettingsPanel(string dbName, string title)
      {
        this.AddDbOption(dbName, title);
        this.fsMySQLProps = new HtmlGenericControl("fieldset");
        this.fsMySQLProps.ID = dbName + "Props";
        this.fsMySQLProps.Attributes.Add("class", "sfSecondaryFormGroup");
        HtmlGenericControl child1 = new HtmlGenericControl("h4");
        child1.InnerText = "MySQL connection properties";
        this.fsMySQLProps.Controls.Add((Control) child1);
        HtmlGenericControl child2 = new HtmlGenericControl("ol");
        this.fsMySQLProps.Controls.Add((Control) child2);
        HtmlGenericControl child3 = new HtmlGenericControl("li");
        child3.Attributes.Add("class", "srvName");
        child2.Controls.Add((Control) child3);
        FieldLabel child4 = new FieldLabel();
        child4.TargetID = "MySQLServer";
        child4.Text = "Server";
        child4.CssClass = "sfTxtLbl";
        child3.Controls.Add((Control) child4);
        this.txtMySQLServer = new TextBox();
        this.txtMySQLServer.ID = "MySQLServer";
        this.txtMySQLServer.CssClass = "sfTxt";
        child3.Controls.Add((Control) this.txtMySQLServer);
        HtmlGenericControl child5 = new HtmlGenericControl("li");
        child5.Attributes.Add("class", "srvPort");
        child2.Controls.Add((Control) child5);
        FieldLabel child6 = new FieldLabel();
        child6.TargetID = "MySQLServerPort";
        child6.Text = "Port";
        child6.CssClass = "sfTxtLbl";
        child5.Controls.Add((Control) child6);
        this.txtMySQLServerPort = new TextBox();
        this.txtMySQLServerPort.ID = "MySQLServerPort";
        this.txtMySQLServerPort.Text = "3306";
        this.txtMySQLServerPort.CssClass = "sfTxt";
        child5.Controls.Add((Control) this.txtMySQLServerPort);
        HtmlGenericControl child7 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child7);
        FieldLabel child8 = new FieldLabel();
        child8.TargetID = "MySQLUsername";
        child8.Text = "Username";
        child8.CssClass = "sfTxtLbl";
        child7.Controls.Add((Control) child8);
        this.txtMySQLUsername = new TextBox();
        this.txtMySQLUsername.ID = "MySQLUsername";
        this.txtMySQLUsername.CssClass = "sfTxt";
        child7.Controls.Add((Control) this.txtMySQLUsername);
        HtmlGenericControl child9 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child9);
        FieldLabel child10 = new FieldLabel();
        child10.TargetID = "MySQLPassword";
        child10.Text = "Password";
        child10.CssClass = "sfTxtLbl";
        child9.Controls.Add((Control) child10);
        this.txtMySQLPassword = new TextBox();
        this.txtMySQLPassword.TextMode = TextBoxMode.Password;
        this.txtMySQLPassword.ID = "MySQLPassword";
        this.txtMySQLPassword.CssClass = "sfTxt";
        child9.Controls.Add((Control) this.txtMySQLPassword);
        HtmlGenericControl child11 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child11);
        FieldLabel child12 = new FieldLabel();
        child12.TargetID = "MySQLDatabase";
        child12.Text = "Database";
        child12.CssClass = "sfTxtLbl";
        child11.Controls.Add((Control) child12);
        this.txtMySQLDatabase = new TextBox();
        this.txtMySQLDatabase.ID = "MySQLDatabase";
        this.txtMySQLDatabase.CssClass = "sfTxt";
        child11.Controls.Add((Control) this.txtMySQLDatabase);
        return (Control) this.fsMySQLProps;
      }

      private Control CreateAzureSettingsPanel(string dbName, string title)
      {
        this.AddDbOption(dbName, title);
        this.fsAzureProps = new HtmlGenericControl("fieldset");
        this.fsAzureProps.ID = dbName + "Props";
        this.fsAzureProps.Attributes.Add("class", "sfSecondaryFormGroup");
        HtmlGenericControl child1 = new HtmlGenericControl("h4");
        child1.InnerText = "Azure connection properties";
        this.fsAzureProps.Controls.Add((Control) child1);
        HtmlGenericControl child2 = new HtmlGenericControl("ol");
        this.fsAzureProps.Controls.Add((Control) child2);
        HtmlGenericControl child3 = new HtmlGenericControl("li");
        child3.Attributes.Add("class", "srvName");
        child2.Controls.Add((Control) child3);
        FieldLabel child4 = new FieldLabel();
        child4.TargetID = "AzureServer";
        child4.Text = "Server";
        child4.CssClass = "sfTxtLbl";
        child3.Controls.Add((Control) child4);
        this.txtAzureName = new TextBox();
        this.txtAzureName.ID = "AzureServer";
        this.txtAzureName.CssClass = "sfTxt";
        child3.Controls.Add((Control) this.txtAzureName);
        HtmlGenericControl child5 = new HtmlGenericControl("li");
        child5.Attributes.Add("class", "srvPort");
        child2.Controls.Add((Control) child5);
        FieldLabel child6 = new FieldLabel();
        child6.TargetID = "AzureServerPort";
        child6.Text = "Port";
        child6.CssClass = "sfTxtLbl";
        child5.Controls.Add((Control) child6);
        this.txtAzurePort = new TextBox();
        this.txtAzurePort.ID = "AzureServerPort";
        this.txtAzurePort.Text = "1433";
        this.txtAzurePort.CssClass = "sfTxt";
        child5.Controls.Add((Control) this.txtAzurePort);
        HtmlGenericControl child7 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child7);
        FieldLabel child8 = new FieldLabel();
        child8.TargetID = "AzureUsername";
        child8.Text = "Username";
        child8.CssClass = "sfTxtLbl";
        child7.Controls.Add((Control) child8);
        this.txtAzureUsername = new TextBox();
        this.txtAzureUsername.ID = "AzureUsername";
        this.txtAzureUsername.CssClass = "sfTxt";
        child7.Controls.Add((Control) this.txtAzureUsername);
        HtmlGenericControl child9 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child9);
        FieldLabel child10 = new FieldLabel();
        child10.TargetID = "AzurePassword";
        child10.Text = "Password";
        child10.CssClass = "sfTxtLbl";
        child9.Controls.Add((Control) child10);
        this.txtAzurePassword = new TextBox();
        this.txtAzurePassword.TextMode = TextBoxMode.Password;
        this.txtAzurePassword.ID = "AzurePassword";
        this.txtAzurePassword.CssClass = "sfTxt";
        child9.Controls.Add((Control) this.txtAzurePassword);
        HtmlGenericControl child11 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child11);
        FieldLabel child12 = new FieldLabel();
        child12.TargetID = "AzureDatabase";
        child12.Text = "Database";
        child12.CssClass = "sfTxtLbl";
        child11.Controls.Add((Control) child12);
        this.txtAzureDatabaseName = new TextBox();
        this.txtAzureDatabaseName.ID = "AzureDatabase";
        this.txtAzureDatabaseName.CssClass = "sfTxt";
        child11.Controls.Add((Control) this.txtAzureDatabaseName);
        return (Control) this.fsAzureProps;
      }

      private Control CreateSQLServerCESettingsPanel(string dbName, string title)
      {
        this.AddDbOption(dbName, title);
        this.fsSqlServerCEProps = new HtmlGenericControl("fieldset");
        this.fsSqlServerCEProps.ID = dbName + "Props";
        this.fsSqlServerCEProps.Attributes.Add("class", "sfSecondaryFormGroup");
        HtmlGenericControl child1 = new HtmlGenericControl("h4");
        child1.InnerText = "SQL Server Compact connection properties";
        this.fsSqlServerCEProps.Controls.Add((Control) child1);
        HtmlGenericControl child2 = new HtmlGenericControl("ol");
        this.fsSqlServerCEProps.Controls.Add((Control) child2);
        HtmlGenericControl child3 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child3);
        FieldLabel child4 = new FieldLabel();
        child4.TargetID = "SqlCEInstance";
        child4.Text = "Instance";
        child4.CssClass = "sfTxtLbl";
        child3.Controls.Add((Control) child4);
        this.txtSqlServerCEInstanceName = new TextBox();
        this.txtSqlServerCEInstanceName.Text = "Sitefinity.sdf";
        this.txtSqlServerCEInstanceName.ID = "SqlCEInstance";
        this.txtSqlServerCEInstanceName.CssClass = "sfTxt";
        child3.Controls.Add((Control) this.txtSqlServerCEInstanceName);
        return (Control) this.fsSqlServerCEProps;
      }

      private Control CreatePostgreSQLSettingsPanel(string dbName, string title)
      {
        this.AddDbOption(dbName, title);
        this.fsPostgreSQLProps = new HtmlGenericControl("fieldset");
        this.fsPostgreSQLProps.ID = dbName + "Props";
        this.fsPostgreSQLProps.Attributes.Add("class", "sfSecondaryFormGroup");
        HtmlGenericControl child1 = new HtmlGenericControl("h4");
        child1.InnerText = "PostgreSQL connection properties";
        this.fsPostgreSQLProps.Controls.Add((Control) child1);
        HtmlGenericControl child2 = new HtmlGenericControl("ol");
        this.fsPostgreSQLProps.Controls.Add((Control) child2);
        HtmlGenericControl child3 = new HtmlGenericControl("li");
        child3.Attributes.Add("class", "srvName");
        child2.Controls.Add((Control) child3);
        FieldLabel child4 = new FieldLabel();
        child4.TargetID = "PostgreSQLServer";
        child4.Text = "Server";
        child4.CssClass = "sfTxtLbl";
        child3.Controls.Add((Control) child4);
        this.txtPostgreSQLServer = new TextBox();
        this.txtPostgreSQLServer.ID = "PostgreSQLServer";
        this.txtPostgreSQLServer.CssClass = "sfTxt";
        child3.Controls.Add((Control) this.txtPostgreSQLServer);
        HtmlGenericControl child5 = new HtmlGenericControl("li");
        child5.Attributes.Add("class", "srvPort");
        child2.Controls.Add((Control) child5);
        FieldLabel child6 = new FieldLabel();
        child6.TargetID = "PostgreSQLServerPort";
        child6.Text = "Port";
        child6.CssClass = "sfTxtLbl";
        child5.Controls.Add((Control) child6);
        this.txtPostgreSQLServerPort = new TextBox();
        this.txtPostgreSQLServerPort.ID = "PostgreSQLServerPort";
        this.txtPostgreSQLServerPort.Text = "5432";
        this.txtPostgreSQLServerPort.CssClass = "sfTxt";
        child5.Controls.Add((Control) this.txtPostgreSQLServerPort);
        HtmlGenericControl child7 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child7);
        FieldLabel child8 = new FieldLabel();
        child8.TargetID = "PostgreSQLId";
        child8.Text = "User Id";
        child8.CssClass = "sfTxtLbl";
        child7.Controls.Add((Control) child8);
        this.txtPostgreSQLUserId = new TextBox();
        this.txtPostgreSQLUserId.ID = "PostgreSQLId";
        this.txtPostgreSQLUserId.CssClass = "sfTxt";
        child7.Controls.Add((Control) this.txtPostgreSQLUserId);
        HtmlGenericControl child9 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child9);
        FieldLabel child10 = new FieldLabel();
        child10.TargetID = "PostgreSQLPassword";
        child10.Text = "Password";
        child10.CssClass = "sfTxtLbl";
        child9.Controls.Add((Control) child10);
        this.txtPostgreSQLPassword = new TextBox();
        this.txtPostgreSQLPassword.TextMode = TextBoxMode.Password;
        this.txtPostgreSQLPassword.ID = "PostgreSQLPassword";
        this.txtPostgreSQLPassword.CssClass = "sfTxt";
        child9.Controls.Add((Control) this.txtPostgreSQLPassword);
        HtmlGenericControl child11 = new HtmlGenericControl("li");
        child2.Controls.Add((Control) child11);
        FieldLabel child12 = new FieldLabel();
        child12.TargetID = "PostgreSQLDatabase";
        child12.Text = "Database";
        child12.CssClass = "sfTxtLbl";
        child11.Controls.Add((Control) child12);
        this.txtPostgreSQLDatabase = new TextBox();
        this.txtPostgreSQLDatabase.ID = "PostgreSQLDatabase";
        this.txtPostgreSQLDatabase.CssClass = "sfTxt";
        child11.Controls.Add((Control) this.txtPostgreSQLDatabase);
        return (Control) this.fsPostgreSQLProps;
      }

      private CmsDbType GetSelectedDatabaseType()
      {
        this.EnsureChildControls();
        CmsDbType selectedDatabaseType = CmsDbType.NotSet;
        foreach (RadioButton radioButton in this.rblDatabase)
        {
          if (radioButton.Checked)
          {
            selectedDatabaseType = (CmsDbType) Enum.Parse(typeof (CmsDbType), radioButton.ID);
            break;
          }
        }
        return selectedDatabaseType;
      }

      internal void SetWindowsAuthentication(bool value) => this.windowsAuth = value;

      internal void SetSqlExpressInstanceName(string instanceName)
      {
        this.EnsureChildControls();
        this.SelectRadio(CmsDbType.SqlExpress);
        this.txtSqlExpressInstanceName.Text = instanceName;
      }

      internal void SetSqlInstanceName(string instanceName, string dbName)
      {
        this.EnsureChildControls();
        this.SelectRadio(CmsDbType.SqlServer);
        this.txtSqlServerName.Text = instanceName;
        this.txtSqlDatabaseName.Text = dbName;
      }

      internal void SetSqlAuthCredentials(string sqlUserName, string sqlUserPassword)
      {
        this.EnsureChildControls();
        this.txtSqlUsername.Text = sqlUserName;
        this.txtSqlPassword.Text = sqlUserPassword;
      }

      internal void SetOracleInstanceName(
        string oracleDatabase,
        string oracleUsername,
        string oraclePassword)
      {
        this.EnsureChildControls();
        this.SelectRadio(CmsDbType.Oracle);
        this.txtOracleDatabase.Text = oracleDatabase;
        this.txtOracleUsername.Text = oracleUsername;
        this.txtOraclePassword.Text = oraclePassword;
      }

      internal void SetMysqlInstanceName(
        string mysqlServer,
        string mysqlPort,
        string mysqlUsername,
        string mysqlPassword,
        string mysqlDatabase)
      {
        this.EnsureChildControls();
        this.SelectRadio(CmsDbType.MySQL);
        this.txtMySQLServer.Text = mysqlServer;
        this.txtMySQLServerPort.Text = mysqlPort;
        this.txtMySQLUsername.Text = mysqlUsername;
        this.txtMySQLPassword.Text = mysqlPassword;
        this.txtMySQLDatabase.Text = mysqlDatabase;
      }

      internal void SetPostgreSqlInstanceName(
        string postgreSqlServer,
        string postgreSqlPort,
        string postgreSqlUserId,
        string postgreSqlPassword,
        string postgreSqlDatabase)
      {
        this.EnsureChildControls();
        this.SelectRadio(CmsDbType.PostgreSQL);
        this.txtPostgreSQLServer.Text = postgreSqlServer;
        this.txtPostgreSQLServerPort.Text = postgreSqlPort;
        this.txtPostgreSQLUserId.Text = postgreSqlUserId;
        this.txtPostgreSQLPassword.Text = postgreSqlPassword;
        this.txtPostgreSQLDatabase.Text = postgreSqlDatabase;
      }
    }

    private class AdministratorPane : 
      CompositeControl,
      StartupWizard.IStartupWizardStepPane,
      IPostBackEventHandler
    {
      private PlaceHolder adminFoundInfoHolder;
      private StartupWizard wizard;
      private HtmlGenericControl fs;
      private string firstNameInternal;
      private string lastNameInternal;
      private string passwordInternal;
      private string emailInternal;
      private bool adminFound;
      private bool validated;
      private string titleText;
      private string message;
      private HtmlGenericControl title;
      private HtmlGenericControl messageControl;
      private TextBox txtFirstName;
      private TextBox txtLastName;
      private TextBox txtPassword;
      private TextBox txtRePassword;
      private TextBox txtEmail;
      private HtmlGenericControl infoControl;

      public AdministratorPane(StartupWizard wizard)
      {
        this.titleText = "Register Administrator";
        this.wizard = wizard;
      }

      public string Title => this.titleText;

      public string Password => this.passwordInternal;

      public string Email => this.emailInternal;

      public bool SkipAdminUser { get; set; }

      public bool Validate()
      {
        if (this.SkipAdminUser)
          return true;
        if (!this.validated)
          this.ValidateMembership();
        if (this.adminFound)
          return true;
        this.EnsureChildControls();
        StringBuilder stringBuilder = new StringBuilder();
        this.firstNameInternal = this.txtFirstName.Text.Trim();
        if (string.IsNullOrEmpty(this.firstNameInternal))
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append("<br/>");
          stringBuilder.Append("<strong>First name</strong> can't be empty");
        }
        this.lastNameInternal = this.txtLastName.Text.Trim();
        if (string.IsNullOrEmpty(this.lastNameInternal))
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append("<br/>");
          stringBuilder.Append("<strong>Last name</strong> can't be empty");
        }
        this.passwordInternal = this.txtPassword.Text.Trim();
        if (string.IsNullOrEmpty(this.passwordInternal))
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append("<br/>");
          stringBuilder.Append("<strong>Password</strong> can't be empty");
        }
        else if (!this.passwordInternal.Equals(this.txtRePassword.Text.Trim(), StringComparison.Ordinal))
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append("<br/>");
          stringBuilder.Append("<strong>Passwords</strong> don't match");
        }
        this.emailInternal = this.txtEmail.Text.Trim();
        if (string.IsNullOrEmpty(this.emailInternal))
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append("<br/>");
          stringBuilder.Append("<strong>Email</strong> can't be empty");
        }
        else if (!Regex.IsMatch(this.emailInternal, "\\S+@\\S+\\.\\S+"))
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append("<br/>");
          stringBuilder.Append("<strong>Email</strong> format is not valid!");
        }
        if (stringBuilder.Length <= 0)
          return true;
        this.message = stringBuilder.ToString();
        return false;
      }

      public bool DoWork()
      {
        if (!this.adminFound)
        {
          Config.SafeMode = false;
          RoleDataProvider roleProvider = this.wizard.GetRoleProvider();
          ConfigProvider configProvider = this.wizard.GetConfigProvider();
          SecurityConfig section = configProvider.GetSection<SecurityConfig>();
          int num = !SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile) ? 1 : 0;
          if (num != 0)
          {
            EnvironmentVariables current = EnvironmentVariables.Current;
            UpdateContext updateContext = current.Update();
            if (current.GetValidationKey() == "Auto")
              updateContext.ValidationKey(SecurityManager.GetRandomKey(64));
            if (current.GetDecryptionKey() == "Auto")
              updateContext.DecryptionKey(SecurityManager.GetRandomKey(32));
            updateContext.Save();
          }
          else
          {
            if (section.ValidationKey == "Auto")
              section.ValidationKey = SecurityManager.GetRandomKey(64);
            if (section.DecryptionKey == "Auto")
              section.DecryptionKey = SecurityManager.GetRandomKey(32);
          }
          string randomKey = SecurityManager.GetRandomKey(32);
          if (section.SecurityTokenIssuers.Count == 0)
            section.SecurityTokenIssuers.Add(new SecurityTokenKeyElement((ConfigElement) section.SecurityTokenIssuers)
            {
              Realm = "http://localhost",
              Key = randomKey,
              Encoding = BinaryEncoding.Hexadecimal,
              MembershipProvider = section.DefaultBackendMembershipProvider
            });
          if (section.RelyingParties.Count == 0)
            section.RelyingParties.Add(new SecurityTokenKeyElement((ConfigElement) section.RelyingParties)
            {
              Realm = "http://localhost",
              Key = randomKey,
              Encoding = BinaryEncoding.Hexadecimal
            });
          string projectName = Config.Get<ProjectConfig>().ProjectName;
          if (!string.IsNullOrEmpty(projectName))
          {
            if (section.AuthCookieName == ".SFAUTH")
            {
              SecurityConfig securityConfig = section;
              securityConfig.AuthCookieName = securityConfig.AuthCookieName + "-" + projectName;
            }
            if (section.RolesCookieName == ".SFROLES")
            {
              SecurityConfig securityConfig = section;
              securityConfig.RolesCookieName = securityConfig.RolesCookieName + "-" + projectName;
            }
            if (section.LoggingCookieName == ".SFLOG")
            {
              SecurityConfig securityConfig = section;
              securityConfig.LoggingCookieName = securityConfig.LoggingCookieName + "-" + projectName;
            }
          }
          if (num == 0)
          {
            using (new FileSystemModeRegion())
              configProvider.SaveSection((ConfigSection) section);
          }
          Role role1 = (Role) null;
          foreach (ApplicationRole appRolesConfig in (IEnumerable<ApplicationRole>) section.ApplicationRoles.Values)
          {
            if (appRolesConfig.Id == Guid.Empty)
              appRolesConfig.Id = Guid.NewGuid();
            Role role2 = this.EnsureRole(roleProvider, appRolesConfig);
            if (role2.Name == "Administrators")
              role1 = role2;
          }
          configProvider.SaveSection((ConfigSection) section);
          SecurityManager.CurrentSettings.Reset();
          if (!this.SkipAdminUser)
          {
            MembershipDataProvider membershipProvider = this.wizard.GetMembershipProvider();
            User user = membershipProvider.GetUser(this.Email) ?? membershipProvider.GetUserByEmail(this.Email);
            if (user == null)
            {
              MembershipCreateStatus status;
              user = membershipProvider.CreateUser(this.Email, this.Password, "test", "yes", true, (object) null, out status);
              switch (status)
              {
                case MembershipCreateStatus.Success:
                  break;
                case MembershipCreateStatus.InvalidPassword:
                  this.message = "Please enter a valid password." + "<br/>" + SecurityUtility.GetPasswordRequirementsText(membershipProvider);
                  return false;
                case MembershipCreateStatus.InvalidQuestion:
                  this.message = "Please enter a valid question";
                  return false;
                case MembershipCreateStatus.InvalidAnswer:
                  this.message = "Please enter a valid answer";
                  return false;
                case MembershipCreateStatus.InvalidEmail:
                  this.message = "Please enter a valid email";
                  return false;
                case MembershipCreateStatus.DuplicateUserName:
                  this.message = "Duplicate username";
                  return false;
                case MembershipCreateStatus.DuplicateEmail:
                  this.message = "Duplicate email";
                  return false;
                default:
                  this.message = "The Administrator was not created. Please try again";
                  return false;
              }
            }
            user.FirstName = this.firstNameInternal;
            user.LastName = this.lastNameInternal;
            user.IsBackendUser = true;
            membershipProvider.CommitTransaction();
            UserProfileDataProvider profilesProvider = this.wizard.GetUserProfilesProvider();
            if (profilesProvider.GetUserProfile<SitefinityProfile>(user) == null)
            {
              string fullName = typeof (SitefinityProfile).FullName;
              SitefinityProfile profile = profilesProvider.CreateProfile(user, fullName) as SitefinityProfile;
              profile.FirstName = this.firstNameInternal;
              profile.LastName = this.lastNameInternal;
              UserProfileUrlData url = profilesProvider.CreateUrl(typeof (UserProfileUrlData)) as UserProfileUrlData;
              url.IsDefault = true;
              url.Url = profilesProvider.CompileItemUrl<SitefinityProfile>(profile);
              url.Culture = CultureInfo.InvariantCulture.LCID;
              profile.Urls.Add(url);
              profilesProvider.CommitTransaction();
            }
            roleProvider.AddUserToRole(user, role1);
          }
          roleProvider.CommitTransaction();
        }
        return true;
      }

      public bool Authenticate() => !this.SkipAdminUser && SecurityManager.AuthenticateUser(this.wizard.GetMembershipProvider().Name, this.Email, this.Password, false) == UserLoggingReason.Success;

      public User GetUser()
      {
        if (this.SkipAdminUser)
          return (User) null;
        MembershipDataProvider membershipProvider = this.wizard.GetMembershipProvider();
        return membershipProvider.GetUser(this.Email) ?? membershipProvider.GetUserByEmail(this.Email);
      }

      private Role EnsureRole(RoleDataProvider roles, ApplicationRole appRolesConfig)
      {
        Guid roleId = appRolesConfig.Id;
        string name = appRolesConfig.Name;
        return roles.GetRoles().Where<Role>((Expression<Func<Role, bool>>) (r => r.Id == roleId)).FirstOrDefault<Role>() ?? roles.CreateRole(roleId, name);
      }

      private void ValidateMembership()
      {
        RoleDataProvider roleProvider = this.wizard.GetRoleProvider();
        Role role = roleProvider.GetRoles().Where<Role>((Expression<Func<Role, bool>>) (r => r.Name == "Administrators")).FirstOrDefault<Role>();
        if (role != null)
        {
          IList<User> usersInRole = roleProvider.GetUsersInRole(role.Id);
          string[] strArray = new string[usersInRole.Count];
          for (int index = 0; index < usersInRole.Count; ++index)
            strArray[index] = usersInRole[index].UserName;
          if (strArray.Length != 0)
          {
            this.EnsureChildControls();
            this.infoControl.InnerHtml = string.Format("The Membership Services database already contains users with unrestricted permissions: <strong>{0}</strong>", (object) string.Join(", ", strArray));
            this.adminFound = true;
            this.validated = true;
            return;
          }
        }
        this.adminFound = false;
        this.validated = true;
      }

      protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

      protected override void AddAttributesToRender(HtmlTextWriter writer)
      {
        if (writer == null)
          throw new ArgumentNullException(nameof (writer));
        base.AddAttributesToRender(writer);
        writer.AddAttribute("class", "sfWizardForm");
      }

      protected override void OnInit(EventArgs e)
      {
        base.OnInit(e);
        if (this.Page == null)
          return;
        this.Page.RegisterRequiresControlState((Control) this);
      }

      protected override object SaveControlState() => (object) new object[2]
      {
        (object) this.adminFound,
        (object) this.validated
      };

      protected override void LoadControlState(object savedState)
      {
        object[] objArray = (object[]) savedState;
        this.adminFound = (bool) objArray[0];
        this.validated = (bool) objArray[1];
      }

      protected override void CreateChildControls()
      {
        this.Controls.Clear();
        this.title = new HtmlGenericControl("h2");
        this.title.Attributes.Add("class", "sfStep1");
        this.Controls.Add((Control) this.title);
        this.messageControl = new HtmlGenericControl("p");
        this.messageControl.Visible = false;
        this.messageControl.Attributes.Add("class", "sfFailure");
        this.Controls.Add((Control) this.messageControl);
        this.adminFoundInfoHolder = new PlaceHolder();
        this.Controls.Add((Control) this.adminFoundInfoHolder);
        this.infoControl = new HtmlGenericControl("p");
        this.infoControl.Attributes.Add("class", "sfFailure");
        this.adminFoundInfoHolder.Controls.Add((Control) this.infoControl);
        HtmlGenericControl child1 = new HtmlGenericControl("p");
        child1.Attributes.Add("class", "sfNote");
        child1.InnerHtml = "<strong>Note:</strong> You need to know the password of the user in order to log in.";
        this.adminFoundInfoHolder.Controls.Add((Control) child1);
        this.fs = new HtmlGenericControl("fieldset");
        this.fs.Attributes.Add("class", "sfForm");
        this.Controls.Add((Control) this.fs);
        HtmlGenericControl child2 = new HtmlGenericControl("div");
        child2.Attributes.Add("class", "sfFormIn");
        this.fs.Controls.Add((Control) child2);
        HtmlGenericControl child3 = new HtmlGenericControl("h3");
        child3.InnerText = "Who will be the most powerful user for this project?";
        child2.Controls.Add((Control) child3);
        HtmlGenericControl child4 = new HtmlGenericControl("ol");
        child2.Controls.Add((Control) child4);
        HtmlGenericControl child5 = new HtmlGenericControl("li");
        child4.Controls.Add((Control) child5);
        FieldLabel child6 = new FieldLabel();
        child6.TargetID = "FirstName";
        child6.Text = "First name";
        child6.CssClass = "sfTxtLbl";
        child5.Controls.Add((Control) child6);
        this.txtFirstName = new TextBox();
        this.txtFirstName.ID = "FirstName";
        this.txtFirstName.Text = string.Empty;
        this.txtFirstName.CssClass = "sfTxt";
        child5.Controls.Add((Control) this.txtFirstName);
        HtmlGenericControl child7 = new HtmlGenericControl("li");
        child4.Controls.Add((Control) child7);
        FieldLabel child8 = new FieldLabel();
        child8.TargetID = "LastName";
        child8.Text = "Last name";
        child8.CssClass = "sfTxtLbl";
        child7.Controls.Add((Control) child8);
        this.txtLastName = new TextBox();
        this.txtLastName.ID = "LastName";
        this.txtLastName.Text = string.Empty;
        this.txtLastName.CssClass = "sfTxt";
        child7.Controls.Add((Control) this.txtLastName);
        HtmlGenericControl child9 = new HtmlGenericControl("li");
        child4.Controls.Add((Control) child9);
        FieldLabel child10 = new FieldLabel();
        child10.TargetID = "Email";
        child10.Text = "Email";
        child10.CssClass = "sfTxtLbl";
        child9.Controls.Add((Control) child10);
        this.txtEmail = new TextBox();
        this.txtEmail.ID = "Email";
        this.txtEmail.CssClass = "sfTxt";
        child9.Controls.Add((Control) this.txtEmail);
        HtmlGenericControl child11 = new HtmlGenericControl("li");
        child4.Controls.Add((Control) child11);
        FieldLabel child12 = new FieldLabel();
        child12.TargetID = "Password";
        child12.Text = "Password";
        child12.CssClass = "sfTxtLbl";
        child11.Controls.Add((Control) child12);
        this.txtPassword = new TextBox();
        this.txtPassword.ID = "Password";
        this.txtPassword.CssClass = "sfTxt";
        this.txtPassword.TextMode = TextBoxMode.Password;
        child11.Controls.Add((Control) this.txtPassword);
        if (this.validated)
        {
          MembershipDataProvider membershipProvider = this.wizard.GetMembershipProvider();
          if (membershipProvider.MinRequiredPasswordLength > 0)
          {
            HtmlGenericControl child13 = new HtmlGenericControl("p");
            child13.Attributes.Add("class", "sfExample");
            child13.InnerHtml = SecurityUtility.GetPasswordLengthHint(membershipProvider.MinRequiredPasswordLength);
            child11.Controls.Add((Control) child13);
          }
          if (membershipProvider.MinRequiredNonAlphanumericCharacters > 0)
          {
            HtmlGenericControl child14 = new HtmlGenericControl("p");
            child14.Attributes.Add("class", "sfExample");
            child14.InnerHtml = SecurityUtility.GetPasswordAlphaNumCharactersHint(membershipProvider.MinRequiredNonAlphanumericCharacters);
            child11.Controls.Add((Control) child14);
          }
        }
        HtmlGenericControl child15 = new HtmlGenericControl("li");
        child4.Controls.Add((Control) child15);
        FieldLabel child16 = new FieldLabel();
        child16.TargetID = "RePassword";
        child16.Text = "Re-Password";
        child16.CssClass = "sfTxtLbl";
        child15.Controls.Add((Control) child16);
        this.txtRePassword = new TextBox();
        this.txtRePassword.ID = "RePassword";
        this.txtRePassword.CssClass = "sfTxt";
        this.txtRePassword.TextMode = TextBoxMode.Password;
        child15.Controls.Add((Control) this.txtRePassword);
      }

      protected override void OnPreRender(EventArgs e)
      {
        base.OnPreRender(e);
        if (!string.IsNullOrEmpty(this.titleText))
          this.title.InnerText = this.titleText;
        if (this.adminFound)
        {
          this.fs.Visible = false;
          this.adminFoundInfoHolder.Visible = true;
        }
        else
        {
          this.fs.Visible = true;
          this.adminFoundInfoHolder.Visible = false;
          if (!string.IsNullOrEmpty(this.message))
          {
            if (this.wizard.IsExpressInstall)
            {
              this.wizard.WriteErrorPage(this.message.Replace("<strong>", "").Replace("</strong>", "").Replace("<br/>", System.Environment.NewLine));
            }
            else
            {
              this.messageControl.Visible = true;
              this.messageControl.InnerHtml = this.message;
            }
          }
          else
          {
            this.messageControl.Visible = false;
            if (this.validated)
              return;
            this.fs.Visible = false;
            this.adminFoundInfoHolder.Visible = false;
            string script = "window.onload = function(){if (ShowValidatingMsg) ShowValidatingMsg();eval(\"" + this.Page.ClientScript.GetPostBackEventReference((Control) this, "ValidateMembership") + "\");};";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "validate", script, true);
            this.validated = true;
          }
        }
      }

      public void RaisePostBackEvent(string eventArgument)
      {
        if (!eventArgument.Equals("ValidateMembership"))
          return;
        this.ValidateMembership();
      }

      internal void Initialize()
      {
        this.validated = false;
        this.adminFound = false;
      }

      internal void Initialize(StartupConfig config)
      {
        this.EnsureChildControls();
        if (string.IsNullOrEmpty(config.Email))
          return;
        this.txtPassword.Text = config.Password;
        this.txtRePassword.Text = config.Password;
        this.txtEmail.Text = config.Email;
        this.txtFirstName.Text = config.FirstName;
        this.txtLastName.Text = config.LastName;
      }
    }

    private class ConnectionHolder
    {
      private char delimiter = Convert.ToChar(1);
      private CmsDbType _dbType;
      private string _connString;

      public ConnectionHolder(CmsDbType dbType, string connString)
      {
        this._dbType = dbType;
        this._connString = connString;
      }

      public ConnectionHolder(string objectString)
      {
        string[] strArray = objectString.Split(this.delimiter);
        this._dbType = (CmsDbType) Enum.Parse(typeof (CmsDbType), strArray[0]);
        this._connString = strArray[1];
      }

      public override string ToString() => string.Format("{0}{1}{2}", (object) this._dbType.ToString(), (object) this.delimiter, (object) this._connString);
    }

    private interface IStartupWizardStepPane
    {
      bool Validate();

      bool DoWork();
    }
  }
}
