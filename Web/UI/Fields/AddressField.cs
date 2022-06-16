// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.AddressField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Locations;
using Telerik.Sitefinity.Locations.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  public class AddressField : FieldControl
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.AddressField.ascx");
    private const string jqueryUiMapScript = "Telerik.Sitefinity.Resources.Scripts.jquery.ui.map.js";
    private const string jqueryUiMapExtensionsScript = "Telerik.Sitefinity.Resources.Scripts.jquery.ui.map.extensions.js";
    private const string jqueryUiMapServicesScript = "Telerik.Sitefinity.Resources.Scripts.jquery.ui.map.services.js";
    internal const string scriptReference = "Telerik.Sitefinity.Web.UI.Fields.Scripts.AddressField.js";
    private static readonly string fieldDisplayModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js";
    private readonly string googleMapsBasicSettingUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Administration/Settings/Basic/GoogleMaps/");
    private AddressWorkMode workMode;
    private string geoLocationServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/CountryLocationService/");
    private Address addressData;
    private bool isRequired;
    private string addressTemplate = "#=Street# #=City# #=State# #=Country#";
    private bool isMapExpanded;
    private bool isApiKeyValid;
    public const int DEFAULT_MAP_ZOOM_LEVEL = 8;

    /// <summary>
    /// Gets or sets the address data used to set control values server side
    /// </summary>
    public Address AddressData
    {
      get => this.addressData;
      set
      {
        this.addressData = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets the front end template for form based the address. Default
    /// is #=Street# #=City# #=State# #=Country#
    /// </summary>
    public string AddressTemplate
    {
      get => this.addressTemplate;
      set => this.addressTemplate = value;
    }

    /// <summary>
    /// Determines if the map is expanded by default in front end
    /// </summary>
    public bool IsMapExpanded
    {
      get => this.isMapExpanded;
      set => this.isMapExpanded = value;
    }

    /// <summary>
    /// Gets or sets the work mode of the address field control
    /// </summary>
    public AddressWorkMode WorkMode
    {
      get => this.workMode;
      set => this.workMode = value;
    }

    /// <summary>Gets or sets whether field values are required</summary>
    public bool IsRequired
    {
      get => this.isRequired;
      set => this.isRequired = value;
    }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = AddressField.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the layout template name</summary>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Web.UI.RadComboBox" /> which lists all the countries
    /// of the world.
    /// </summary>
    protected virtual RadComboBox CountriesComboBox => this.Container.GetControl<RadComboBox>("countries_write", this.DisplayMode == FieldDisplayMode.Write && this.IsFormOnlyOrHybridMode());

    /// <summary>
    /// Gets the label for displaying error message when country is not selected
    /// </summary>
    public Label CountriesRequiredErrorMessage => this.Container.GetControl<Label>("countriesRequiredErrorMessage", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Web.UI.RadComboBox" /> which lists all the states
    /// of USA.
    /// </summary>
    protected virtual RadComboBox StatesComboBox => this.Container.GetControl<RadComboBox>("states_write", this.DisplayMode == FieldDisplayMode.Write && this.IsFormOnlyOrHybridMode());

    /// <summary>
    /// Gets the label for displaying error message when states combo box is shown by not selected
    /// </summary>
    protected virtual Label StatesRequiredErrorMessage => this.Container.GetControl<Label>("statesRequiredErrorMessage", true);

    /// <summary>
    /// Gets the reference to the State <see cref="T:System.Web.UI.WebControls.Label" />
    /// </summary>
    protected virtual Label StateLabel => this.Container.GetControl<Label>("statesLabel_write", this.DisplayMode == FieldDisplayMode.Write && this.IsFormOnlyOrHybridMode());

    /// <summary>
    /// Gets the reference to the City <see cref="T:Telerik.Sitefinity.Web.UI.Fields.TextField" />
    /// </summary>
    protected virtual TextField CityTextField => this.Container.GetControl<TextField>("city_write", this.DisplayMode == FieldDisplayMode.Write && this.IsFormOnlyOrHybridMode());

    /// <summary>
    /// Gets the reference to the Street <see cref="T:Telerik.Sitefinity.Web.UI.Fields.TextField" />
    /// </summary>
    protected virtual TextField StreetTextField => this.Container.GetControl<TextField>("street_write", this.DisplayMode == FieldDisplayMode.Write && this.IsFormOnlyOrHybridMode());

    /// <summary>
    /// Gets the reference to the Zip <see cref="T:Telerik.Sitefinity.Web.UI.Fields.TextField" />
    /// </summary>
    protected virtual TextField ZipTextField => this.Container.GetControl<TextField>("zip_write", this.DisplayMode == FieldDisplayMode.Write && this.IsFormOnlyOrHybridMode());

    /// <summary>
    /// Gets the reference to the Latitude <see cref="T:Telerik.Sitefinity.Web.UI.Fields.TextField" />
    /// </summary>
    protected virtual TextField LatitudeTextField => this.Container.GetControl<TextField>("latitude_write", this.DisplayMode == FieldDisplayMode.Write && this.IsMapOnlyOrHybridMode());

    /// <summary>
    /// Gets the reference to the Longitude <see cref="T:Telerik.Sitefinity.Web.UI.Fields.TextField" />
    /// </summary>
    protected virtual TextField LongitudeTextField => this.Container.GetControl<TextField>("longitude_write", this.DisplayMode == FieldDisplayMode.Write && this.IsMapOnlyOrHybridMode());

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Web.UI.SitefinityLabel" /> used for displaying address in read mode
    /// </summary>
    protected virtual SitefinityLabel AddressReadModeLabel => this.Container.GetControl<SitefinityLabel>("address_read", this.DisplayMode == FieldDisplayMode.Read && this.IsFormOnlyOrHybridMode());

    /// <summary>Gets a link for displaying the map in front end</summary>
    protected virtual LinkButton ShowMapButton => this.Container.GetControl<LinkButton>("showMapButton", this.DisplayMode == FieldDisplayMode.Read && this.IsMapOnlyOrHybridMode());

    /// <summary>Gets the wrapper container of coordinates text fields</summary>
    protected virtual HtmlGenericControl CoordinatesPane => this.Container.GetControl<HtmlGenericControl>("coordinatesPane", true);

    /// <summary>
    /// Gets the panel holding error message when api key is not set
    /// </summary>
    protected virtual Control ApiKeyErrorPanelWriteMode => this.Container.GetControl<Control>("apiKeyErrorPanel_write", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Initializes the controls.</summary>
    protected override void InitializeControls(GenericContainer container)
    {
      string googleMapApiV3Key = Config.Get<SystemConfig>().GeoLocationSettings.GoogleMapApiV3Key;
      if (this.ShouldHaveApiKey() && string.IsNullOrEmpty(googleMapApiV3Key))
      {
        this.ShowApiKeyError();
        this.isApiKeyValid = false;
      }
      else
      {
        this.HideApiKeyError();
        this.isApiKeyValid = true;
        if (this.DisplayMode == FieldDisplayMode.Write)
        {
          this.ConfigureWorkingMode();
          if (this.WorkMode != AddressWorkMode.FormOnly && this.WorkMode != AddressWorkMode.Hybrid)
            return;
          this.LoadWorldCountriesAndStates(this.CountriesComboBox, this.StatesComboBox);
        }
        else
        {
          if (this.AddressData == null)
            return;
          this.ConfigureWorkingMode();
          if (this.WorkMode != AddressWorkMode.FormOnly && this.WorkMode != AddressWorkMode.Hybrid)
            return;
          this.AddressReadModeLabel.Text = this.GenerateAddressReadMode();
        }
      }
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IAddressFieldDefinition addressFieldDefinition))
        return;
      this.WorkMode = addressFieldDefinition.WorkMode;
      this.IsRequired = addressFieldDefinition.IsRequired;
    }

    private void ShowApiKeyError()
    {
      if (this.DisplayMode == FieldDisplayMode.Write)
        this.ApiKeyErrorPanelWriteMode.Visible = true;
      this.Container.GetControl<Control>("mapOnlyContainer_" + this.DisplayMode.ToString(), true).Visible = false;
      this.Container.GetControl<Control>("formOnlyContainer_" + this.DisplayMode.ToString(), true).Visible = false;
    }

    private void HideApiKeyError()
    {
      if (this.DisplayMode != FieldDisplayMode.Write)
        return;
      this.ApiKeyErrorPanelWriteMode.Visible = false;
    }

    private bool ShouldHaveApiKey() => this.WorkMode == AddressWorkMode.Hybrid || this.WorkMode == AddressWorkMode.MapOnly;

    private string GenerateAddressReadMode()
    {
      string s = string.Empty;
      if (this.AddressTemplate != null)
      {
        string newValue1 = string.Empty;
        if (!string.IsNullOrEmpty(this.AddressData.Street))
          newValue1 = this.AddressData.Street + ",";
        string str1 = this.AddressTemplate.Replace("#=Street#", newValue1);
        string newValue2 = string.Empty;
        if (!string.IsNullOrEmpty(this.AddressData.Zip))
          newValue2 = this.AddressData.Zip + ",";
        string str2 = str1.Replace("#=Zip#", newValue2);
        string newValue3 = string.Empty;
        if (!string.IsNullOrEmpty(this.AddressData.City))
          newValue3 = this.AddressData.City + ",";
        string str3 = str2.Replace("#=City#", newValue3);
        CountryElement country = Config.Get<LocationsConfig>().Countries[this.AddressData.CountryCode];
        string newValue4 = string.Empty;
        string newValue5 = string.Empty;
        if (country != null)
        {
          newValue4 = country.Name;
          if (this.AddressData.StateCode != string.Empty && (this.AddressData.CountryCode == "CA" || this.AddressData.CountryCode == "US"))
          {
            StateProvinceElement statesProvince = Config.Get<LocationsConfig>().Countries[this.AddressData.CountryCode].StatesProvinces[this.AddressData.StateCode];
            if (statesProvince != null)
              newValue5 = statesProvince.Name;
          }
        }
        s = str3.Replace("#=Country#", newValue4).Replace("#=State#", newValue5);
      }
      return HttpUtility.HtmlEncode(s);
    }

    /// <summary>
    ///  Helper method for determining if control is required when WorkMode is FormOnly or Hybrid
    /// </summary>
    private bool IsFormOnlyOrHybridMode() => this.WorkMode == AddressWorkMode.FormOnly || this.WorkMode == AddressWorkMode.Hybrid;

    /// <summary>
    /// Helper method for determining if control is required when WorkMode is MapOnly or Hybrid
    /// </summary>
    private bool IsMapOnlyOrHybridMode() => this.WorkMode == AddressWorkMode.MapOnly || this.WorkMode == AddressWorkMode.Hybrid;

    private void ConfigureWorkingMode()
    {
      if (this.WorkMode == AddressWorkMode.FormOnly)
      {
        this.Container.GetControl<Control>("mapOnlyContainer_" + this.DisplayMode.ToString(), true).Visible = false;
      }
      else
      {
        if (this.WorkMode != AddressWorkMode.MapOnly)
          return;
        this.Container.GetControl<Control>("formOnlyContainer_" + this.DisplayMode.ToString(), true).Visible = false;
      }
    }

    /// <summary>
    /// Loads all the world countries, all the 50 states of the United States of America and all the provinces of Cananada.
    /// </summary>
    /// <param name="countriesComboBox">The instance of the <see cref="T:Telerik.Web.UI.RadComboBox" /> in which the countries ought to be loaded.</param>
    /// <param name="statesComboBox">The instance of the <see cref="T:Telerik.Web.UI.RadComboBox" /> in which the states and provinces ought to be loaded.</param>
    private void LoadWorldCountriesAndStates(
      RadComboBox countriesComboBox,
      RadComboBox statesComboBox)
    {
      countriesComboBox.Items.Clear();
      statesComboBox.Items.Clear();
      List<CountryElement> list1 = Config.Get<LocationsConfig>().Countries.Values.Where<CountryElement>((Func<CountryElement, bool>) (x => x.CountryIsActive)).OrderBy<CountryElement, string>((Func<CountryElement, string>) (x => x.Name)).ToList<CountryElement>();
      countriesComboBox.Items.Add(new RadComboBoxItem(Res.Get<AddressFieldResources>().SelectCountry, ""));
      foreach (CountryElement country in list1)
      {
        countriesComboBox.Items.Add(new RadComboBoxItem(country.Name, country.IsoCode));
        List<StateProvinceElement> list2 = country.StatesProvinces.Values.ToList<StateProvinceElement>();
        string desiredCountryCssClass = AddressField.GetDesiredCountryCssClass(country);
        foreach (StateProvinceElement stateProvinceElement in list2)
        {
          if (!string.IsNullOrWhiteSpace(stateProvinceElement.Abbreviation))
          {
            RadComboBoxItemCollection items = statesComboBox.Items;
            RadComboBoxItem radComboBoxItem = new RadComboBoxItem(stateProvinceElement.Name, stateProvinceElement.Abbreviation);
            radComboBoxItem.CssClass = desiredCountryCssClass;
            items.Add(radComboBoxItem);
          }
          else
          {
            RadComboBoxItemCollection items = statesComboBox.Items;
            RadComboBoxItem radComboBoxItem = new RadComboBoxItem(stateProvinceElement.Name, stateProvinceElement.Name);
            radComboBoxItem.CssClass = desiredCountryCssClass;
            items.Add(radComboBoxItem);
          }
        }
      }
    }

    private static string GetDesiredCountryCssClass(CountryElement country) => "sf" + country.IsoCode + "Country";

    private void LoadScriptDescriptorsWriteMode(ScriptControlDescriptor descriptor)
    {
      if (this.WorkMode == AddressWorkMode.FormOnly || this.WorkMode == AddressWorkMode.Hybrid)
      {
        descriptor.AddComponentProperty("countriesComboBox", this.CountriesComboBox.ClientID);
        descriptor.AddComponentProperty("statesComboBox", this.StatesComboBox.ClientID);
        descriptor.AddComponentProperty("cityTextField", this.CityTextField.ClientID);
        descriptor.AddComponentProperty("zipTextField", this.ZipTextField.ClientID);
        descriptor.AddComponentProperty("streetTextField", this.StreetTextField.ClientID);
        descriptor.AddElementProperty("stateLabel", this.StateLabel.ClientID);
        descriptor.AddElementProperty("countriesRequiredErrorMessage", this.CountriesRequiredErrorMessage.ClientID);
        descriptor.AddElementProperty("statesRequiredErrorMessage", this.StatesRequiredErrorMessage.ClientID);
      }
      if (this.WorkMode == AddressWorkMode.MapOnly || this.WorkMode == AddressWorkMode.Hybrid)
      {
        descriptor.AddComponentProperty("latitudeTextField", this.LatitudeTextField.ClientID);
        descriptor.AddComponentProperty("longitudeTextField", this.LongitudeTextField.ClientID);
        descriptor.AddElementProperty("coordinatesPane", this.CoordinatesPane.ClientID);
      }
      descriptor.AddProperty("_googleMapsBasicSettingUrl", (object) this.googleMapsBasicSettingUrl);
    }

    private void LoadScriptDescriptorsReadMode(ScriptControlDescriptor descriptor)
    {
      if (this.WorkMode == AddressWorkMode.FormOnly || this.WorkMode == AddressWorkMode.Hybrid)
      {
        descriptor.AddElementProperty("addressLabelReadMode", this.AddressReadModeLabel.ClientID);
        descriptor.AddProperty("addressTemplate", (object) this.AddressTemplate);
      }
      if (this.WorkMode != AddressWorkMode.MapOnly && this.WorkMode != AddressWorkMode.Hybrid)
        return;
      descriptor.AddElementProperty("showMapButton", this.ShowMapButton.ClientID);
      descriptor.AddProperty("isMapExpanded", (object) this.IsMapExpanded);
      if (this.AddressData == null)
        return;
      descriptor.AddProperty("_readModeMapZoomLevel", (object) this.AddressData.MapZoomLevel);
      descriptor.AddProperty("_readModeLat", (object) this.AddressData.Latitude);
      descriptor.AddProperty("_readModeLon", (object) this.AddressData.Longitude);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript)
    /// client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" />
    /// objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor descriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      if (this.DisplayMode == FieldDisplayMode.Write)
        this.LoadScriptDescriptorsWriteMode(descriptor);
      else
        this.LoadScriptDescriptorsReadMode(descriptor);
      descriptor.AddProperty("_displayMode", (object) this.DisplayMode);
      descriptor.AddProperty("_isRequired", (object) this.IsRequired);
      descriptor.AddProperty("_workMode", (object) this.WorkMode);
      descriptor.AddProperty("_selectStateProvincePhrase", (object) Res.Get<AddressFieldResources>().SelectState);
      descriptor.AddProperty("_selectCountryPhrase", (object) Res.Get<AddressFieldResources>().SelectCountry);
      descriptor.AddProperty("_geoLocationServiceUrl", (object) this.geoLocationServiceUrl);
      descriptor.AddProperty("_isApiKeyValid", (object) this.isApiKeyValid);
      descriptor.AddProperty("_isBackendReadMode", (object) (bool) (this.DisplayMode != FieldDisplayMode.Read ? 0 : (this.IsBackend() ? 1 : 0)));
      string stateDataString;
      string countryDataString;
      this.GetCountryAndStateDataAsJson(new JavaScriptSerializer(), out stateDataString, out countryDataString);
      descriptor.AddProperty("_stateProvinceData", (object) stateDataString);
      descriptor.AddProperty("_countryData", (object) countryDataString);
      descriptor.AddProperty("_enabled", (object) this.Enabled);
      descriptor.AddProperty("_defaultMapZoomLevel", (object) 8);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects
    /// that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" />
    /// objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().FullName;
      scriptReferences.Add(new ScriptReference(AddressField.fieldDisplayModeScript, typeof (AddressField).Assembly.FullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.AddressField.js", typeof (AddressField).Assembly.FullName));
      if (this.isApiKeyValid)
      {
        string path = "https://maps.googleapis.com/maps/api/js?key=" + Config.Get<SystemConfig>().GeoLocationSettings.GoogleMapApiV3Key + "&sensor=true&v=3.25.8";
        scriptReferences.Add(new ScriptReference(path));
        scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery.ui.map.js", fullName));
        scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery.ui.map.extensions.js", fullName));
        scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery.ui.map.services.js", fullName));
      }
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    internal void GetCountryAndStateDataAsJson(
      JavaScriptSerializer serializer,
      out string stateDataString,
      out string countryDataString)
    {
      List<AddressField.StateInfo> stateInfoList = new List<AddressField.StateInfo>();
      List<AddressField.CountryDataInfo> countryDataInfoList = new List<AddressField.CountryDataInfo>();
      foreach (CountryElement country in Config.Get<LocationsConfig>().Countries.Values.Where<CountryElement>((Func<CountryElement, bool>) (x => x.CountryIsActive)).OrderBy<CountryElement, string>((Func<CountryElement, string>) (x => x.Name)).ToList<CountryElement>())
      {
        List<StateProvinceElement> list = country.StatesProvinces.Values.ToList<StateProvinceElement>();
        countryDataInfoList.Add(new AddressField.CountryDataInfo()
        {
          IsoCode = country.IsoCode,
          Name = country.Name,
          HasStates = list.Count > 0
        });
        foreach (StateProvinceElement stateProvinceElement in list)
          stateInfoList.Add(new AddressField.StateInfo()
          {
            CountryKey = AddressField.GetDesiredCountryCssClass(country),
            Abbreviation = stateProvinceElement.Abbreviation,
            StateProvinceName = stateProvinceElement.Name
          });
      }
      stateDataString = serializer.Serialize((object) stateInfoList);
      countryDataString = serializer.Serialize((object) countryDataInfoList);
    }

    private struct StateInfo
    {
      public string CountryKey { get; set; }

      public string Abbreviation { get; set; }

      public string StateProvinceName { get; set; }
    }

    private struct CountryDataInfo
    {
      public string IsoCode { get; set; }

      public string Name { get; set; }

      public bool HasStates { get; set; }
    }
  }
}
