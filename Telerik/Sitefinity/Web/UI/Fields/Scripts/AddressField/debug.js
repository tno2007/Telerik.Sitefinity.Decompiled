﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.AddressField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.AddressField.initializeBase(this, [element]);
    this._element = element;
    this._countriesComboBox = null;
    this._statesComboBox = null;
    this._cityTextField = null;
    this._zipTextField = null;
    this._streetTextField = null;
    this._latitudeTextField = null;
    this._longitudeTextField = null;
    this._address = null;
    this._countryChangedDelegate = null;
    this._latitudeTextFieldChangedDelegate = null;
    this._longitudeTextFieldChangedDelegate = null;
    this._cityTextFieldChangedDelegate = null;
    this._streetTextFieldChangedDelegate = null;
    this._stateLabel = null;
    this._stateProvinceData = null;
    this._countryData = null;
    this._selectStateProvincePhrase = null;
    this._selectCountryPhrase = null;
    this._mapClickedDelegate = null;
    this._currentPossition = null;
    this._isMapValueSet = false;
    this._isMapInitialized = false;
    this._countriesRequiredErrorMessage = null;
    this._statesRequiredErrorMessage = null;
    this._defaultMapZoomLevel = null;
    this._countryInitialZoomLevel = 4;
    this._cityInitialZoomLevel = 10;
    this._streetInitialZoomLevel = 15;
    this._minMapZoomLevel = 2;
    this._maxMapZoomLevel = 16;
    this._markerZoomLevel = this._defaultMapZoomLevel;

    this._mapCanvas = null;
    this._displayMode = null;
    this._workMode = null;
    this._isRequired = null;
    this._addressLabelReadMode = null;
    this._showMapButton = null;
    this._addressTemplate = null;
    this._readModeMapZoomLevel = null;
    this._readModeLat = null;
    this._readModeLon = null;
    this._isSettingValue = null;
    this._isMapExpanded = null;
    this._showMapButtonClikedDelegate = null;
    this._stateChangedDelegate = null;
    this._geoLocationServiceUrl = null;
    this._isApiKeyValid = null;
    this._coordinatesPane = null;
    this._googleMapsBasicSettingUrl = null;
    this._isBackendReadMode = null;
    this._enabled = null;
}

Telerik.Sitefinity.Web.UI.Fields.AddressField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.AddressField.callBaseMethod(this, "initialize");
        if (this._isFormOnlyOrHybridMode()) {
            if (this._countryChangedDelegate == null) {
                this._countryChangedDelegate = Function.createDelegate(this, this._countryChangedHandler);
            }
            if (this._countriesComboBox) {
                this._countriesComboBox.add_selectedIndexChanged(this._countryChangedDelegate);
                if (!this._enabled) {
                    this._countriesComboBox.disable();
                }
            }
            if (this._stateChangedDelegate == null) {
                this._stateChangedDelegate = Function.createDelegate(this, this._stateChangedHandler);
            }
            if (this._statesComboBox) {
                this._statesComboBox.add_selectedIndexChanged(this._stateChangedDelegate);
                if (!this._enabled) {
                    this._statesComboBox.disable();
                }
            }
        }
        if (this._isMapOnlyOrHybridMode()) {
            if (this._mapClickedDelegate == null) {
                this._mapClickedDelegate = Function.createDelegate(this, this._mapClickedHandler);
            }
            if (this._latitudeTextFieldChangedDelegate == null) {
                this._latitudeTextFieldChangedDelegate = Function.createDelegate(this, this._latitudeTextFieldChangedHandler);
            }
            if (this._longitudeTextFieldChangedDelegate == null) {
                this._longitudeTextFieldChangedDelegate = Function.createDelegate(this, this._longitudeTextFieldChangedHandler);
            }

            $(this.get_countriesRequiredErrorMessage()).hide();
            $(this.get_statesRequiredErrorMessage()).hide();

            this._initializeUI();

            if (this._showMapButton) {
                if (this._showMapButtonClikedDelegate == null) {
                    this._showMapButtonClikedDelegate = Function.createDelegate(this, this._showMapButtonHandler);
                }
                $addHandler(this._showMapButton, "click", this._showMapButtonClikedDelegate);
            }

            //Add handlers to changed coordinates for reverse geocoding
            if (this._latitudeTextField) {
                $(this.get_latitudeTextField()._element).change(this._latitudeTextFieldChangedDelegate);
                // disable setting of focus on set_value of TextField coming from ExpandHandler
                this.get_latitudeTextField().focus = function () { };
            }
            if (this._longitudeTextField) {
                $(this.get_longitudeTextField()._element).change(this._longitudeTextFieldChangedDelegate);
                // disable setting of focus on set_value of TextField coming from ExpandHandler
                this.get_longitudeTextField().focus = function () { };
            }
        }
        if (this._isHybridMode()) {
            if (this._cityTextFieldChangedDelegate == null) {
                this._cityTextFieldChangedDelegate = Function.createDelegate(this, this._cityTextFieldChangedHandler);
            }
            if (this._streetTextFieldChangedDelegate == null) {
                this._streetTextFieldChangedDelegate = Function.createDelegate(this, this._streetTextFieldChangedHandler);
            }
            if (this._cityTextField) {
                $(this.get_cityTextField()._element).change(this._cityTextFieldChangedDelegate);
            }
            if (this._streetTextField) {
                $(this.get_streetTextField()._element).change(this._streetTextFieldChangedDelegate);
            }
        }
    },

    dispose: function () {
        if (this._countryChangedDelegate != null) {
            delete this._countryChangedDelegate;
        }
        if (this._mapClickedDelegate != null) {
            delete this._mapClickedDelegate;
        }
        if (this._latitudeTextFieldChangedDelegate != null) {
            delete this._latitudeTextFieldChangedDelegate;
        }
        if (this._longitudeTextFieldChangedDelegate != null) {
            delete this._longitudeTextFieldChangedDelegate;
        }
        if (this._cityTextFieldChangedDelegate != null) {
            delete this._cityTextFieldChangedDelegate;
        }
        if (this._streetTextFieldChangedDelegate != null) {
            delete this._streetTextFieldChangedDelegate;
        }
        if (this._showMapButtonClikedDelegate != null) {
            delete this._showMapButtonClikedDelegate;
        }
        if (this._stateChangedDelegate != null) {
            delete this._stateChangedDelegate;
        }
        Telerik.Sitefinity.Web.UI.Fields.AddressField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    get_value: function () {
        if (this._isWriteMode()) {
            return this._getWriteModeValue();
        }
    },

    set_value: function (value) {
        this._isSettingValue = true;
        if (this._isWriteMode()) {
            this._setWriteModeValue(value);
        } else {
            this._setReadModeValue(value);
        }
        this._isSettingValue = false;
    },

    reset: function () {
        if (this._isWriteMode()) {
            this._resetWriteMode();
        } else {
            //this._resetReadMode();
        }
    },

    // Returns true if the value of the field is changed
    isChanged: function () {
        var prop = ["CountryCode", "StateCode", "City", "Zip", "Street", "MapZoomLevel", "Latitude", "Longitude"];
        var isChanged = false;
        var currentValue = this.get_value();
        if (this._address != null && currentValue != null) {
            for (var i = 0; i < prop.length; i++) {
                if (this._address[prop[i]] != currentValue[prop[i]]) {
                    isChanged = true;
                    break;
                }
            }
        }
        return isChanged;
    },

    //override the validate method
    validate: function () {
        $(this._element).find(".sfError").hide()
        var isControlValid = true;
        if (this._isRequired) {
            if (this._isFormOnlyOrHybridMode()) {

                var isCountryValid = this._validateCountriesComboBox();
                if ($(this._statesComboBox._element).is(":visible")) {
                    var isStateValid = this._validateStatesComboBox();
                    isControlValid = isControlValid && isStateValid;
                }
                var isCityFieldValid = this.get_cityTextField().validate();
                var isZipFieldValid = this.get_zipTextField().validate();
                var isTextFieldValid = this.get_streetTextField().validate();
                isControlValid = isControlValid && isCountryValid && isCityFieldValid && isZipFieldValid && isTextFieldValid;
            }
            if (this._isMapOnlyOrHybridMode()) {
                var isLatitudeValid = this.get_latitudeTextField().validate();
                var isLongitudeValid = this.get_longitudeTextField().validate();
                isControlValid = isControlValid && isLatitudeValid && isLongitudeValid;
            }
        }
        return isControlValid;
    },

    /* -------------------- event handlers ------------ */

    _showMapButtonHandler: function (sender, args) {
        var that = this;
        this._getMapWrapper().toggle({
            duration: 0
        });
        if (this._getMapWrapper().is(":visible")) {
            if (this._mapCanvas && typeof this._mapCanvas.gmap('get', 'map') == 'object') {
                this._refreshMapReadMode();
            } else {
                // initialize map and set it's value
                that._initializeMapReadMode();
            }
        }
    },

    _countryHasStates: function (countryIsoCode) {
        var hasStates = false;
        var countryData = Sys.Serialization.JavaScriptSerializer.deserialize(this._countryData);
        for (var i = 0; i < countryData.length; i++) {
            if (countryData[i].IsoCode == countryIsoCode) {
                hasStates = countryData[i].HasStates;
                break;
            }
        }
        return hasStates;
    },

    _countryChangedHandler: function (sender, args) {
        var comboValue = this.get_countriesComboBox().get_value();
        if (this._countryHasStates(comboValue)) {

            // show the states / provinces drop-down.
            $(this._stateLabel).show();
            $(this._stateLabel).parent("li").show();
            $(this._statesComboBox._element).show();

            stateCombo = this.get_statesComboBox();
            stateCombo.clearItems();
            var items = stateCombo.get_items();

            stateCombo.trackChanges();
            var comboItem = new Telerik.Web.UI.RadComboBoxItem();
            comboItem.set_value('');
            comboItem.set_text(this._selectStateProvincePhrase);
            items.add(comboItem);
            var currentCountryKey = 'sf' + comboValue + 'Country'
            var stateProvinceData = Sys.Serialization.JavaScriptSerializer.deserialize(this._stateProvinceData);
            for (var i = 0; i < stateProvinceData.length; i++) {
                var temp = stateProvinceData[i];
                if (temp.CountryKey == currentCountryKey) {
                    comboItem = new Telerik.Web.UI.RadComboBoxItem();
                    comboItem.set_value(temp.Abbreviation);
                    comboItem.set_text(temp.StateProvinceName);
                    items.add(comboItem);
                }
            }
            stateCombo.commitChanges();
            stateCombo.clearSelection();
            var textOfFirstItemInList = items._array[0].get_text();
            var firstItemInList = stateCombo.findItemByText(textOfFirstItemInList);
            firstItemInList.select();
        }
        else {
            this._resetStatesBoxComboBox();
        }
        if (this._isHybridMode() && !this._isSettingValue) {
            this._locateCountry(comboValue);
        }
    },

    _stateChangedHandler: function (sender, args) {
        if (this._isHybridMode() && !this._isSettingValue) {
            var countryIsoCode = this.get_countriesComboBox().get_value();
            if (this._countryHasStates(countryIsoCode)) {
                var stateIsoCode = this.get_statesComboBox().get_value();
                if (stateIsoCode != "") {
                    this._locateState(countryIsoCode, stateIsoCode);
                }
            }
        }
    },

    _cityTextFieldChangedHandler: function (sender, args) {
        if (this._isMapInitialized) {
            var city = this.get_cityTextField().get_value();
            if (city != "") {
                var state = "";
                if ($(this._statesComboBox._element).is(':visible')) {
                    state = " " + this.get_statesComboBox().get_value();
                }
                var address = city + state + ", " + this.get_countriesComboBox().get_text();
                this._findAddress(address, this._cityInitialZoomLevel);
            }
        }
    },

    _streetTextFieldChangedHandler: function (sender, args) {
        if (this._isMapInitialized) {
            var street = this.get_streetTextField().get_value();
            if (street != "") {
                var state = "";
                if ($(this._statesComboBox._element).is(':visible')) {
                    state = " " + this.get_statesComboBox().get_value();
                }

                var address = street + ", " + this.get_cityTextField().get_value() + state + ", " + this.get_countriesComboBox().get_text();
                this._findAddress(address, this._streetInitialZoomLevel);
            }
        }
    },

    _latitudeTextFieldChangedHandler: function (e) {
        e.preventDefault();
        if (this._isMapInitialized) {
            var isLatitudeValid = true;
            if (this._isRequired) {
                isLatitudeValid = this.get_latitudeTextField().validate();
            }
            if (isLatitudeValid) {
                var newLatitude = this.get_latitudeTextField().get_value();
                var lng = this.get_longitudeTextField().get_value();
                var latlng = new google.maps.LatLng(newLatitude, lng);
                this._addMarkerToMap(latlng, false);
            }
        }
    },

    _longitudeTextFieldChangedHandler: function (e) {
        e.preventDefault();
        if (this._isMapInitialized) {
            var isLongitudeValid = true;
            if (this._isRequired) {
                isLongitudeValid = this.get_longitudeTextField().validate();
            }
            if (isLongitudeValid) {
                var newLongitude = this.get_longitudeTextField().get_value();
                var lat = this.get_latitudeTextField().get_value();
                var latlng = new google.maps.LatLng(lat, newLongitude);
                this._addMarkerToMap(latlng, false);
            }
        }
    },

    _sectionDoToggleHandler: function (args) {
        if (args.Action == "expand") {
            if (this._mapCanvas && typeof this._mapCanvas.gmap('get', 'map') == 'object') {
                this._refreshMap();
            }
        }
    },

    /* --------------------  private methods ----------- */

    _refreshMapReadMode: function () {
        this._refreshMap();
        if (this._isBackendReadMode) {
            if (this._address != null) {
                this._changeZoom(this._address.MapZoomLevel);
                var possition = new google.maps.LatLng(this._address.Latitude, this._address.Longitude);
                this._addMarkerToMap(possition, false);
            }
        } else {
            if (this._readModeMapZoomLevel) {
                this._changeZoom(this._readModeMapZoomLevel);
            }
        }
    },

    _refreshMap: function () {
        this._mapCanvas.gmap('refresh');
        var markers = this._mapCanvas.gmap('get', 'markers');
        if (markers.length > 0) {
            var markerPosition = markers[0].position;
            var centerPosition = this._mapCanvas.gmap('get', 'map').center;
            if (markerPosition.kb != centerPosition.kb || markerPosition.lb != centerPosition.lb) {
                this._mapCanvas.gmap('get', 'map').setOptions({ 'center': markerPosition });
            }
        }
    },

    _initializeUI: function () {
        if (this._isApiKeyValid) {
            if (this._isWriteMode()) {
                this._initializeMap();
                if (this._coordinatesPane) {
                    $(this._coordinatesPane).find(".sfMoreDetails").click(function () {
                        $(this).closest(".sfExpandableSection").toggleClass("sfExpandedSection");
                    });
                }
            } else {
                if (this._isMapExpanded) {
                    this._initializeMapReadMode();
                }
            }
        } else {
            $(this._element).find("#googleMapsSettingsBtn").attr("href", this._googleMapsBasicSettingUrl);
        }
    },

    _getMapWrapper: function () {
        return $(this._element).find("#map_wrapper_read");
    },

    _getReadModeValue: function () {
        return this._address;
    },

    _getWriteModeValue: function () {
        var currentAddresValue = {};

        if (this._isFormOnlyMode() || (this._isFormOnlyOrHybridMode() && this._isApiKeyValid)) {
            var formData = this._getFormOnlyData();
            $.extend(currentAddresValue, formData);
        }

        if (this._isMapOnlyOrHybridMode() && this._isApiKeyValid) {
            var lat = this.get_latitudeTextField().get_value();
            var lng = this.get_longitudeTextField().get_value();
            var mapData = {};

            // if latitude and longitude are not changed return them as null
            mapData.Latitude = (lat != "" ? parseFloat(lat) : null);
            mapData.Longitude = (lng != "" ? parseFloat(lng) : null);
            mapData.MapZoomLevel = (lat != "" && lng != "" ? this._markerZoomLevel : null);
            $.extend(currentAddresValue, mapData);
        }

        return currentAddresValue;
    },

    _getFormOnlyData: function () {
        var countryCodeData = this.get_countriesComboBox().get_value();
        var stateCodeData = this.get_statesComboBox().get_value();
        var cityData = this.get_cityTextField().get_value();
        var zipData = this.get_zipTextField().get_value();
        var streetData = this.get_streetTextField().get_value();

        // if we are creating item or doesn't have view permissions and data is default for field (empty string for text fields), when field is disabled
        // return null so that when compared to default field values (when field permissions are applied) it is stated as not changed
        var formData = {
            "CountryCode": ((this._address == null || this._address.CountryCode == null) && !this._enabled && countryCodeData == "" ? null : countryCodeData),
            "StateCode": ((this._address == null || this._address.CountryCode == null) && !this._enabled && stateCodeData == "" ? null : stateCodeData),
            "City": ((this._address == null || this._address.CountryCode == null) && !this._enabled && cityData == "" ? null : cityData),
            "Zip": ((this._address == null || this._address.CountryCode == null) && !this._enabled && zipData == "" ? null : zipData),
            "Street": ((this._address == null || this._address.CountryCode == null) && !this._enabled && streetData == "" ? null : streetData)
        };
        return formData;
    },

    _setWriteModeValue: function (value) {
        if (value != null) {
            this._address = value;
            if (this._isFormOnlyOrHybridMode()) {
                if (value.CountryCode != null) {
                    if (this.get_countriesComboBox()) {

                        // field is disabled but view permissions are enabled and value is set
                        if (!this._enabled) {
                            this._countriesComboBox.enable();
                        }

                        this.get_countriesComboBox().clearSelection();
                        var selectedCountry = this.get_countriesComboBox().findItemByValue(value.CountryCode);
                        if (selectedCountry) {
                            selectedCountry.select();
                        } else {
                            // select default
                            var firstItemInList = this.get_countriesComboBox().findItemByText(this._selectCountryPhrase);
                            firstItemInList.select();
                        }

                        // disable the field if it was disabled before setting value
                        if (!this._enabled) {
                            this._countriesComboBox.disable();
                        }
                    }
                }
                if (value.StateCode != null && value.StateCode != '') {
                    if (this.get_statesComboBox()) {

                        // field is disabled but view permissions are enabled and value is set
                        if (!this._enabled) {
                            this._statesComboBox.enable();
                        }

                        this.get_statesComboBox().clearSelection();
                        var selectedState = this.get_statesComboBox().findItemByValue(value.StateCode);
                        if (selectedState) {
                            selectedState.select();
                        }

                        // disable the field if it was disabled before setting value
                        if (!this._enabled) {
                            this._statesComboBox.disable();
                        }
                    }
                }
                if (this.get_cityTextField()) {
                    this.get_cityTextField().set_value(value.City);
                }
                if (this.get_zipTextField()) {
                    this.get_zipTextField().set_value(value.Zip);
                }
                if (this.get_streetTextField()) {
                    this.get_streetTextField().set_value(value.Street);
                }
            }
            if (this._isMapOnlyOrHybridMode()) {
                if (this.get_latitudeTextField()) {
                    this.get_latitudeTextField().set_value(value.Latitude);
                }
                if (this.get_longitudeTextField()) {
                    this.get_longitudeTextField().set_value(value.Longitude);
                }
                this._setMapValue(value.Latitude, value.Longitude, value.MapZoomLevel);
                if (!this._isMapValueSet) {
                    if (this._enabled) {
                        this._setCurrentLocation();
                    }
                }
            }
        } else if (this._isMapInitialized && this._enabled && this._isMapOnlyOrHybridMode()) {
            this._setCurrentLocation();
        }
    },

    _setReadModeValue: function (value) {
        if (value != null) {
            this._address = value;

            if (this._isFormOnlyOrHybridMode()) {
                var readModeValue = this._generateAddressReadMode(value);
                $(this._addressLabelReadMode).html(readModeValue);
            }
            if (this._isMapOnlyOrHybridMode()) {
                if (this._mapCanvas && typeof this._mapCanvas.gmap('get', 'map') == 'object') {
                    this._refreshMapReadMode();
                } else {
                    if (this._isMapExpanded) {
                        var latlng = new google.maps.LatLng(value.Latitude, value.Longitude);
                        this._initializeMap(latlng, value.MapZoomLevel);
                    }
                }
            }
        }
    },

    _generateAddressReadMode: function (value) {
        var result = this._addressTemplate;

        var street = '';
        if (value.Street != '') {
            street = value.Street + ",";
        }
        result = result.replace(/#=Street#/gi, street);

        var zip = '';
        if (value.Zip != '') {
            zip = value.Zip + ",";
        }
        result = result.replace(/#=Zip#/gi, zip);

        var city = '';
        if (value.City != '') {
            city = value.City + ",";
        }
        result = result.replace(/#=City#/gi, city);

        var state = value.StateCode;
        if (value.StateCode) {
            var stateProvinceData = Sys.Serialization.JavaScriptSerializer.deserialize(this._stateProvinceData);
            for (var i = 0; i < stateProvinceData.length; i++) {
                if (stateProvinceData[i].Abbreviation == value.StateCode) {
                    state = stateProvinceData[i].StateProvinceName;
                    break;
                }
            }
        }
        result = result.replace(/#=State#/gi, state);

        var country = value.CountryCode;
        if (value.CountryCode) {
            var countryData = Sys.Serialization.JavaScriptSerializer.deserialize(this._countryData);
            for (var j = 0; j < countryData.length; j++) {
                if (countryData[j].IsoCode == value.CountryCode) {
                    country = countryData[j].Name;
                    break;
                }
            }
        }
        result = result.replace(/#=Country#/gi, country);

        return result.htmlEncode();
    },

    _resetWriteMode: function () {
        this._isMapValueSet = false;
        if (this._mapCanvas) {
            this._mapCanvas.gmap('clear', 'markers');
        }
        $(this._element).find(".sfError").hide();

        if (this._isFormOnlyOrHybridMode()) {
            this._resetCountriesComboBox();
            this._resetStatesBoxComboBox();
            if (this.get_cityTextField()) {
                this.get_cityTextField().set_value("");
            }
            if (this.get_zipTextField()) {
                this.get_zipTextField().set_value("");
            }
            if (this.get_streetTextField()) {
                this.get_streetTextField().set_value("");
            }
        }

        if (this._isMapOnlyOrHybridMode()) {
            if (this.get_latitudeTextField()) {
                this.get_latitudeTextField().set_value("");
            }
            if (this.get_longitudeTextField()) {
                this.get_longitudeTextField().set_value("");
            }

            this._markerZoomLevel = this._defaultMapZoomLevel;
        }
    },

    _resetStatesBoxComboBox: function () {
        $(this.get_statesRequiredErrorMessage()).hide();
        if (this._statesComboBox) {
            $(this._statesComboBox._element).hide();
        }
        $(this._stateLabel).hide();
        $(this._stateLabel).parent("li").hide();
        if (this.get_statesComboBox()) {
            this.get_statesComboBox().set_value('');
        }
        if (this.get_statesComboBox()) {
            this.get_statesComboBox()._selectedIndex = 0;
        }
    },

    _validateStatesComboBox: function () {
        if (this.get_statesComboBox().get_value().length == 0) {
            $(this.get_statesRequiredErrorMessage()).show();
            return false
        } else {
            $(this.get_statesRequiredErrorMessage()).hide();
            return true;
        }
    },

    _resetCountriesComboBox: function () {
        $(this.get_countriesRequiredErrorMessage()).hide();
        var countriesComboBox = this.get_countriesComboBox();
        if (countriesComboBox) {
            countriesComboBox.clearSelection();
            var firstItemInList = countriesComboBox.findItemByText(this._selectCountryPhrase);
            firstItemInList.select();
        }
    },

    _validateCountriesComboBox: function () {
        if (this.get_countriesComboBox().get_value().length == 0) {
            $(this.get_countriesRequiredErrorMessage()).show();
            return false;
        } else {
            $(this.get_countriesRequiredErrorMessage()).hide();
            return true;
        }
    },

    _initMapCanvas: function () {
        if (this._mapCanvas == null) {
            if (this._isWriteMode()) {
                this._mapCanvas = $(this._element).find('#map_canvas');
                if (this._isFormOnlyOrHybridMode()) {
                    this._resetStatesBoxComboBox();
                }
            } else {
                this._mapCanvas = $(this._element).find('#map_canvas_read');
            }
        }
    },

    _initializeMap: function (possition, zoomLevel) {
        this._initMapCanvas();
        var that = this;
        var mapOptions = {
            'minZoom': this._minMapZoomLevel,
            'zoom': this._defaultMapZoomLevel,
            'maxZoom': this._maxMapZoomLevel,
            'panControl': false
        };
        this._mapCanvas.gmap(mapOptions).bind('init', function (event, map) {
            if (that._isWriteMode()) {
                if (that._enabled) {
                    $(map).click(that._mapClickedDelegate);
                    $(map).addEventListener('zoom_changed', function () {
                        that._markerZoomLevel = that._getCurrentZoomLevel();
                    });
                }
            }
            if (possition) {
                that._addMarkerToMap(possition, true, zoomLevel);
            }
            that._isMapInitialized = true;
        });
    },

    _initializeMapReadMode: function () {
        this._getMapWrapper().show();
        if (this._isBackendReadMode && this._address != null) {
            var possition = new google.maps.LatLng(this._address.Latitude, this._address.Longitude);
            this._initializeMap(possition, this._address.MapZoomLevel);
        } else {
            var zoom = this._defaultMapZoomLevel;
            if (this._readModeMapZoomLevel) {
                zoom = this._readModeMapZoomLevel;
            }
            var readPossition = new google.maps.LatLng(this._readModeLat, this._readModeLon);
            this._initializeMap(readPossition, zoom);
        }
    },

    _setCurrentLocation: function () {
        var that = this;
        this._mapCanvas.gmap('getCurrentPosition', function (position, status) {
            if (!that._isMapValueSet) {
                if (status === 'OK') {
                    var currentPossition = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                    that._currentPossition = currentPossition;
                    that._addMarkerToMap(currentPossition, true);
                    that._isMapValueSet = true;
                } else {
                    var greenwichPossition = new google.maps.LatLng(51.4788, 0.0106);
                    that._mapCanvas.gmap('get', 'map').setOptions({ 'center': greenwichPossition });
                    that._isMapValueSet = true;
                    that._hideStreetView();
                }
            }
        });
    },

    _mapClickedHandler: function (event) {
        this._addMarkerToMap(event.latLng, true);
    },

    _addMarkerToMap: function (position, setLatLngFieldValues, zoomLevel) {
        if (setLatLngFieldValues) {
            if (this._isWriteMode()) {
                this.get_latitudeTextField().set_value(position.lat());
                this.get_longitudeTextField().set_value(position.lng());
            }
            if (zoomLevel) {
                this._changeZoom(zoomLevel);
            }
        }
        var that = this;
        this._mapCanvas.gmap('clear', 'markers');
        this._hideStreetView();
        var marker = this._mapCanvas.gmap('addMarker', {
            'position': position,
            'draggable': this._isWriteMode() && this._enabled,
            'bounds': false // if set to true overrides any zoom set in the constructor
        });
        this._markerZoomLevel = this._getCurrentZoomLevel();
        this._mapCanvas.gmap('get', 'map').setOptions({ 'center': position });
        if (this._isWriteMode() && this._enabled) {
            marker.dragend(function (event) {
                that._findLocation(event.latLng, this);
            }).click(function () {
                // this here is the marker - we have information about its position and can show it on click of the marker
                // this._map_canvas.gmap('openInfoWindow', {'content': 'Hello World!'}, this);
            });
        }
    },

    _findLocation: function (location, marker) {
        var that = this;
        this._mapCanvas.gmap('search', { 'location': location }, function (results, status) {
            if (status === 'OK') {
                // we receive list of results with very close params so take the first one
                var latitude = results[0].geometry.location.lat();
                var longitude = results[0].geometry.location.lng();
                that.get_latitudeTextField().set_value(latitude);
                that.get_longitudeTextField().set_value(longitude);
                marker.setTitle(results[0].formatted_address);
            }
        });
    },

    _findAddress: function (address, zoomLevel) {
        var that = this;
        this._mapCanvas.gmap('search', { 'address': address }, function (results, status) {
            if (status === 'OK') {
                // we receive list of results with very close params so take the first one
                var lat = results[0].geometry.location.lat();
                var lng = results[0].geometry.location.lng();
                var latlng = new google.maps.LatLng(lat, lng);
                that._addMarkerToMap(latlng, true, zoomLevel);
            }
        });
    },

    _locateCountry: function (isoCode) {
        if (this._isMapInitialized && isoCode != "") {
            var url = this._geoLocationServiceUrl + 'country/' + isoCode + '/';
            this._locateCountryOrState(url);
        }
    },

    _locateState: function (countryIsoCode, stateIsoCode) {
        if (this._isMapInitialized && countryIsoCode != "" && stateIsoCode != "") {
            var url = this._geoLocationServiceUrl + 'state/' + countryIsoCode + '/' + stateIsoCode + '/';
            this._locateCountryOrState(url);
        }
    },

    _locateCountryOrState: function (url) {
        if (this._isMapInitialized) {
            var that = this;
            $.ajax({
                url: url,
                dataType: 'json',
                type: "GET",
                success: function (result) {
                    if (result.Latitude !== undefined && result.Longitude !== undefined) {
                        var lat = result.Latitude;
                        var lng = result.Longitude;
                        var latlng = new google.maps.LatLng(lat, lng);
                        that._addMarkerToMap(latlng, true, that._countryInitialZoomLevel);
                    }
                }
            });
        }
    },

    _changeZoom: function (zoomLevel) {
        this._mapCanvas.gmap('option', 'zoom', zoomLevel);
    },

    _isWriteMode: function () {
        return this._displayMode == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write;
    },

    _getCurrentZoomLevel: function () {
        return this._mapCanvas.gmap('get', 'map').getZoom();
    },

    _setMapValue: function (lat, lng, zoomLevel) {
        if (lat && lng && this._isApiKeyValid) {
            this._markerZoomLevel = zoomLevel;
            this._changeZoom(zoomLevel);

            var latlng = new google.maps.LatLng(lat, lng);
            this._addMarkerToMap(latlng, true);
            this._isMapValueSet = true;
        }
    },

    _hideStreetView: function () {
        this._mapCanvas.gmap('get', 'map').getStreetView().setVisible(false);
    },

    _isFormOnlyOrHybridMode: function () {
        return this._workMode == 0 || this._workMode == 2;
    },

    _isMapOnlyOrHybridMode: function () {
        return this._workMode == 1 || this._workMode == 2;
    },

    _isFormOnlyMode: function () {
        return this._workMode == 0;
    },

    _isHybridMode: function () {
        return this._workMode == 2;
    },

    /* -------------------- properties ---------------- */

    get_countriesComboBox: function () {
        return this._countriesComboBox;
    },
    set_countriesComboBox: function (value) {
        this._countriesComboBox = value;
    },
    get_statesComboBox: function () {
        return this._statesComboBox;
    },
    set_statesComboBox: function (value) {
        this._statesComboBox = value;
    },
    get_cityTextField: function () {
        return this._cityTextField;
    },
    set_cityTextField: function (value) {
        this._cityTextField = value;
    },
    get_zipTextField: function () {
        return this._zipTextField;
    },
    set_zipTextField: function (value) {
        this._zipTextField = value;
    },
    get_streetTextField: function () {
        return this._streetTextField;
    },
    set_streetTextField: function (value) {
        this._streetTextField = value;
    },
    get_latitudeTextField: function () {
        return this._latitudeTextField;
    },
    set_latitudeTextField: function (value) {
        this._latitudeTextField = value;
    },
    get_longitudeTextField: function () {
        return this._longitudeTextField;
    },
    set_longitudeTextField: function (value) {
        this._longitudeTextField = value;
    },
    get_stateLabel: function () {
        return this._stateLabel;
    },
    set_stateLabel: function (value) {
        this._stateLabel = value;
    },
    get_addressLabelReadMode: function () {
        return this._addressLabelReadMode;
    },
    set_addressLabelReadMode: function (value) {
        this._addressLabelReadMode = value;
    },
    get_showMapButton: function () {
        return this._showMapButton;
    },
    set_showMapButton: function (value) {
        this._showMapButton = value;
    },
    get_isMapExpanded: function () {
        return this._isMapExpanded;
    },
    set_isMapExpanded: function (value) {
        this._isMapExpanded = value;
    },
    get_addressTemplate: function () {
        return this._addressTemplate;
    },
    set_addressTemplate: function (value) {
        this._addressTemplate = value;
    },
    get_statesRequiredErrorMessage: function () {
        return this._statesRequiredErrorMessage;
    },
    set_statesRequiredErrorMessage: function (value) {
        this._statesRequiredErrorMessage = value;
    },
    get_countriesRequiredErrorMessage: function () {
        return this._countriesRequiredErrorMessage;
    },
    set_countriesRequiredErrorMessage: function (value) {
        this._countriesRequiredErrorMessage = value;
    },
    get_coordinatesPane: function () {
        return this._coordinatesPane;
    },
    set_coordinatesPane: function (value) {
        this._coordinatesPane = value;
    },
    get_defaultMapZoomLevel: function () {
        return this._defaultMapZoomLevel;
    },
    set_defaultMapZoomLevel: function (value) {
        this._defaultMapZoomLevel = value;
    }
}

Telerik.Sitefinity.Web.UI.Fields.AddressField.registerClass("Telerik.Sitefinity.Web.UI.Fields.AddressField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);