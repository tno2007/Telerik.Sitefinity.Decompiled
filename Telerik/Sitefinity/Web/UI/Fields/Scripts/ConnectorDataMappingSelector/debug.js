Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingSelector = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingSelector.initializeBase(this, [element]);
        this._webServiceUrl = null;
        this._autoCompleteWebServiceUrl = null;
        this._extenders = null;
        this._duplicateMappedFieldsFormat = null;

		this._dataContext = null;
		this._viewModel = null;
		this._template = null;
}
Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingSelector.prototype =
{
    /* -------------------- set up and tear down ----------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingSelector.callBaseMethod(this, "initialize");

		if (this._extenders) {
            this._extenders = Sys.Serialization.JavaScriptSerializer.deserialize(this._extenders);
        }

		this._selectors = {
            tableTemplate: "#table-template",
			mappingsElement: ".mappings",
			inputFields: "input.dataMappings",
			noFormFieldsDiv: ".noFormFieldsDiv",
            errorsWrapper: "#errorsWrapper",
            warningsWrapper: "#warningsWrapper"
        };        

        // prevent memory leaks
		jQuery(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });

        this._onLoadDelegate = Function.createDelegate(this, this._onLoadHandler);
        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingSelector.callBaseMethod(this, "dispose");

		if (this._onLoadDelegate) {
			Sys.Application.remove_load(this._onLoadDelegate);
			delete this._onLoadDelegate;
        }
    },

    /* --------- Public Methods ------------ */
    initializeViewModel: function () {
		var viewModel = kendo.observable({
			mapping: {
				Extenders: [],
				Fields: []
			}
		});
		
		viewModel.mapping.Extenders = this._extenders;
		this._viewModel = viewModel;
    },

	getSelectors: function () {
        return this._selectors;
	},

    reset: function () {
		this.clearSelection();
    },

	isEmpty: function (value) {
		return typeof value == 'string' && !value.trim() || typeof value == 'undefined' || value === null;
	},

	loadMappings: function(value) {
        this._resetWarningsWrapper();

		if(this._viewModel.mapping.Fields.length > 0)
		{
			this._showHideFieldsContainers(true);
			this._setMappingFieldsWriteMode();
			this._executeViewTemplate();
			this._loadMappingsFieldValues(value);
			this._loadAutocompleteData();
		}
		else
		{
			this._showHideFieldsContainers(false);
		    this._executeViewTemplate();
		}        
	},

	clearSelection: function() {
	    jQuery(this.getSelectors().inputFields).val('');
        jQuery(this.getSelectors().errorsWrapper).hide();
		this._cleanAutoCompletedFields();
	},

	getMappingsFromJson: function(jsonString){
		if(jsonString)
		{
			return jQuery.parseJSON(jsonString);
		}
		return {};
	},

	validate: function () {
	    var isValid = true;
	    var fields = jQuery(this.getSelectors().inputFields);
        var errorsWrapper = jQuery(this.getSelectors().errorsWrapper);
	    if (fields && fields.length > 0) {
	        var duplicateMappedFieldsFormat = this._duplicateMappedFieldsFormat;
	        var dict = {};
            var that = this; 

            var extendersValidationRegexMap = {};

	        fields.each(function () {
	            var elem = jQuery(this);
	            if (elem) {
                    var origValue = elem.val();
	                var value = origValue.trim().toUpperCase();
	                if (value) {
	                    var extenderName = elem.attr("extender");
	                    if (!dict.hasOwnProperty(extenderName)) {
	                        dict[extenderName] = {};
	                    }

	                    if (!dict[extenderName][value]) {
                            var extender = that._extenders.filter(function(ex) {
                                return ex.Key === extenderName;
                            })[0];

                            if (extender && extender.DataMappingFieldInputValidationRegex) {
                                var regex = extendersValidationRegexMap[extenderName];
                                if (!regex)
                                {
                                    regex = new RegExp(extender.DataMappingFieldInputValidationRegex);
                                    extendersValidationRegexMap[extenderName] = regex;
                                }

                                if (regex.test(value) === false) {
                                    errorsWrapper.html(String.format(extender.DataMappingFieldValidationErrorMessageFormat, extenderName, origValue));
	                                errorsWrapper.show();
	                                isValid = false;
                                }
                            }

	                        dict[extenderName][value] = true;
	                    } else {
	                        errorsWrapper.html(String.format(duplicateMappedFieldsFormat, extenderName, origValue));
	                        errorsWrapper.show();
	                        isValid = false;
	                    }
	                }
	            }
	        });
	    }

	    return isValid;
	},
    
    /* --------- Private methods ------------ */   
    _resetWarningsWrapper: function() {
        var warningsWrapper = jQuery(this.getSelectors().warningsWrapper);
        warningsWrapper.html('');
        warningsWrapper.hide();
    },

	_cleanAutoCompletedFields: function(){
		var fields = jQuery(this.getSelectors().inputFields);
		if(fields) {
			for (var i = 0; i < fields.length; i++) {
				var autocomplete = jQuery(fields[i]).data("kendoAutoComplete");
				if(autocomplete)
				{
					autocomplete.destroy();
				}
			}
		}
	},

	_getDataMappingsCount: function () {
	    var count = 0;
	    var mappings = this.getMappingsFromJson(this._value);
	    for (var extenderName in mappings) {
	        if (mappings.hasOwnProperty(extenderName)) {
	            for (var fieldName in mappings[extenderName]) {
	                if (mappings[extenderName].hasOwnProperty(fieldName)) {
	                    count++;
	                }
	            }
	        }
	    }

	    return count;
	},

	_showHideFieldsContainers: function(hasFields)
	{
		if(hasFields)
		{
			jQuery(this.getSelectors().mappingsElement).show();
			jQuery(this.getSelectors().noFormFieldsDiv).hide();
		}
		else
		{
			jQuery(this.getSelectors().mappingsElement).hide();
			jQuery(this.getSelectors().noFormFieldsDiv).show();
		}        
	},

	_bind: function () {
		if(this._dataContext && this._dataContext.Id)
		{
			this._requestFormFields(this._dataContext.Id);
		}
	},

	_requestFormFields: function (formId) {
	    var that = this;
		var dataSource = new kendo.data.DataSource({
			transport: {
				read: {
					url: String.format("{0}{1}/", this.get_webServiceUrl(), formId),
					dataType: "json"
				}
			},
			schema: {
                data: "Items",
                total: "TotalCount"
            },
			requestEnd: function (e) {
                var items = e.response && e.response.Items ? e.response.Items : [];
				that._viewModel.mapping.Fields = items;
			},
			error: function (jqXHR, textStatus, errorThrown) {
				var errText;
				if (jqXHR.errorThrown) {
				    errText = jqXHR.errorThrown;
				}
				else {
					errText = jqXHR.status;
				}
				alert(errText);
			}
		});
		dataSource.read();
	},

	_setMappingFieldsWriteMode: function(){
		if(this._extenders)
		{
			for (var i = 0; i < this._extenders.length; i++) {
				var extender = this._extenders[i];
				extender.WriteMode = true;
				if(extender.DependentControlsCssClass)
				{
					var that = this;
					jQuery("." + extender.DependentControlsCssClass).each(function(index, element) {
						var fieldControl = $find(element.id);
						if(fieldControl)
						{
                            var value = fieldControl.get_value();

                            // extender state checkbox (used for Sitefinity Insight extender)
                            if (fieldControl._element.className.indexOf('sendFormsDataToDecDependentCtrls') !== -1) {
                                if(value === 'false')
                                    extender.Disabled = true;                      
                                else if (value === 'true') {
                                    if (extender.Disabled)
                                        delete extender.Disabled;
                                }
                            }					

							if(that.isEmpty(value)) 
							{
								extender.WriteMode = false;
								return;
							}
						}
					});
				}
			}
		}
	},

	_loadAutocompleteData: function () {
	    if (this._extenders) {
	        for (var i = 0; i < this._extenders.length; i++) {
	            var extender = this._extenders[i];
	            if (extender.HasAutocomplete) {
					var postParamData = this._getRequiredControlsParamsValues(extender.AutocompleteRequiredControlsCssClass);
					this._addFieldsAutoComplete(extender, postParamData);
	            }
	        }
	    }
	},

	_addFieldsAutoComplete: function(extender, postParamData){
		var that = this;
		var fields = jQuery(this.getSelectors().inputFields + "[extender='" + extender.Key + "']");
		if(fields) {
			for (var i = 0; i < fields.length; i++) {
				jQuery(fields[i]).kendoAutoComplete({
					minLength : 0,
					filter : "contains",
					dataSource : new kendo.data.DataSource({
						serverFiltering: true, 
						transport : {
							read : function (options){
								that._requestAutocompleteData(extender, postParamData, options)
							}
						}
					})
				}).focus(function(){
					var autocomplete = jQuery(this).data("kendoAutoComplete");
					if(autocomplete)
					{
						autocomplete.search("");
					}
				}); 
                
                // Workaround to disable browsers' auto-complete, e.g. Chrome's autofill (72.x)
                var id = jQuery(fields[i]).attr('id');
                var noAutoFillVal = id + "-" + new Date().getTime();
                jQuery(fields[i]).attr('name', noAutoFillVal);
                jQuery(fields[i]).attr('autocomplete', noAutoFillVal);
			}
		}
	},

	_requestAutocompleteData: function (extender, data, options) {
		var term = '';
		if(options)
		{
            var firstFilter = options.data.filter.filters[0];
            if (firstFilter) {
                term = $.trim(firstFilter.value);
            }
		}
	 	var url =  String.format("{0}?extender={1}&term={2}", this.get_autoCompleteWebServiceUrl(), extender.Key, term);

	    $.ajax({
	        type: "POST",
	        url: url,
	        data: JSON.stringify(data),
	        contentType: "application/json" })
			.done(
	    	 $.proxy(function (result) {
				if(options)
				{
                    // When all new data is fetched, susbstitute the old
                    if (extender.AutoCompleteData && term === '')
                        extender.AutoCompleteData = result;                        

					options.success(result);
				}
            }, this)
			);
	},

	_getRequiredControlsParamsValues: function (requiredControlsCss) {
	    var params = [];
	    var that = this;
	    jQuery("." + requiredControlsCss).each(function (index, element) {
	        var fieldControl = $find(element.id);
	        var value = fieldControl.get_value();
	        if (fieldControl) {
	            if (!that.isEmpty(value)) {
				    params.push(value);
	            }
	        }
	    });
	    return params;
	},

	_executeViewTemplate: function () {
		if(this._template){
			var result = this._template(this._viewModel.mapping); //Execute the template
			jQuery(this.getSelectors().mappingsElement).html(result); //Append the result
		}
	},

    _attachInputFieldChangeEventListener : function(extender) {
		var extenderCodeName = extender.Key.replace(/\s/g, ""); // remove whitespaces      
		var warningClass = extenderCodeName + "_warning";
        var warningClassLocator = "." + warningClass;
        var warningsWrapper = jQuery(this.getSelectors().warningsWrapper); 
		
		var fieldsLocator =  this.getSelectors().inputFields + "[extender='" + extender.Key + "']";
        var that = this; 
        jQuery(fieldsLocator).on('change', function() {
			var warningId = extenderCodeName + "_warning_" + jQuery(this).attr('id');
			var warningIdLocator = "#" + warningId;
            var inputValue = jQuery(this).val().trim();
            if (inputValue !== '') {                
                var matches = extender.AutoCompleteData.filter(function(data) {
                    return data.toUpperCase() === inputValue.toUpperCase();
                }); 
                var inputValueMatchesAutoCompleteData = matches.length > 0 ? true: false;        

                if (inputValueMatchesAutoCompleteData === false) {                                    
                    that._cleanUpWarnings(warningIdLocator, warningClassLocator);

                    var warningMessage = String.format(extender.InvalidFieldValueMessageFormat, extender.Key, inputValue);                        
                    var warning = "<div class='" + warningClass + "' id='" + warningId + "'>" + warningMessage + "</div>";
                    warningsWrapper.append(warning);
                    warningsWrapper.show();
                } else {
                    that._cleanUpWarnings(warningIdLocator, warningClassLocator);
                }
            } else {
                that._cleanUpWarnings(warningIdLocator, warningClassLocator);
            }
        });
    },    

    _cleanUpWarnings: function(warningIdLocator, warningClassLocator) {
        var warningsWrapper = jQuery(this.getSelectors().warningsWrapper); 

        var warningExistsForField = jQuery(warningIdLocator).length > 0;
        if (warningExistsForField === true) {
            jQuery(warningIdLocator).remove();
        }

        if (jQuery(warningClassLocator).length === 0) {
            warningsWrapper.hide();
        }
    },

    _resolveConnectivityIssues: function() {
        var extenders = this._viewModel.mapping.Extenders;
        // If there are errors for the extender (e.g. connectivity issues or faulty setup), disable the fields - currently used for Sitefinity Insight extender
        var extendersWithConnectivityIssues = extenders.filter(function(ex) {
            return !ex.Disabled && ex.HasConnectivityIssues === true;
        });      

        for(var i = 0; i < extendersWithConnectivityIssues.length; i++) {
            var extender = extendersWithConnectivityIssues[i];
            var fieldsLocator =  this.getSelectors().inputFields + "[extender='" + extender.Key + "']";
            jQuery(fieldsLocator).prop('disabled', true);
            var warningsWrapper = jQuery(this.getSelectors().warningsWrapper);
	        warningsWrapper.append("<div>" + extender.ConnectivityIssuesMessage + "</div>");
	        warningsWrapper.show();
        }
    },

    _trackInvalidMappings: function() {
        var extenders = this._viewModel.mapping.Extenders;
        var extendersWithTrackingEnabled = extenders.filter(function(ex) {
            var fieldsEnabled = !(ex.Disabled || ex.HasConnectivityIssues);
            var shouldTrack = ex.TrackAndShowWarningsForInvalidFieldsMappings && ex.AutoCompleteData;
            return fieldsEnabled && shouldTrack;            
        }); 

        for(var i = 0; i < extendersWithTrackingEnabled.length; i++) {
            this._attachInputFieldChangeEventListener(extendersWithTrackingEnabled[i]);
        }
    },

	_loadMappingsFieldValues: function(value) {        
        this._resolveConnectivityIssues();
        this._trackInvalidMappings();

	    var mappings = this.getMappingsFromJson(value);                 
	    for (var extenderName in mappings) {
            var fieldsLocator = this.getSelectors().inputFields + "[extender='" + extenderName + "']";
	        if (mappings.hasOwnProperty(extenderName)) {
	            for (var fieldName in mappings[extenderName]) {
	                if (mappings[extenderName].hasOwnProperty(fieldName)) {
	                    var data = mappings[extenderName][fieldName];    
                        var fieldLocator = fieldsLocator + "[id='" + fieldName + "']";
                        jQuery(fieldLocator).val(data);                    	  
	                }
	            }
	        }

            jQuery(fieldsLocator).trigger('change');
	    }        
	},

	_getConnectorDataMappingFieldsValues: function() {
		var fields = jQuery(this.getSelectors().inputFields);
		var jsonString = '';
		if(fields && fields.length > 0) {
		    var json = {};
			fields.each(function () 
			{ 
				var elem = jQuery(this); 
				if(elem) {
					var value = elem.val();
					if(value)
					{
					    var extender = elem.attr("extender");
					    if (!json.hasOwnProperty(extender)) {
					        json[extender] = {};
					    }

					    var key = elem.attr("id");
					    json[extender][key] = value;
					}
				}
			});
			jsonString = JSON.stringify(json);
		}

		return jsonString;
	},
    /* --------- Event Handlers ------------ */
	 _onLoadHandler: function () {
		this.initializeViewModel();
		//Get the external template definition using a jQuery selector
		this._template = kendo.template(jQuery(this.getSelectors().tableTemplate).html());
    },
    /* ------------ Properties -------------- */
    // Gets the value of the field control.
    get_value: function () {
		this._value  = this._getConnectorDataMappingFieldsValues();
        return this._value;
    },
    // Sets the value of the field control.
    set_value: function (value) {
        this._value = value;
    },
	
	get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    
	set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    
	get_autoCompleteWebServiceUrl: function () {
        return this._autoCompleteWebServiceUrl;
    },
    
	set_autoCompleteWebServiceUrl: function (value) {
        this._autoCompleteWebServiceUrl = value;
    },
    
	get_extenders: function () {
        return this._extenders;
    },
    
	set_extenders: function (value) {
        this._extenders = value;
    },
	
	get_dataContext: function () {
        return this._dataContext;
    },

    set_dataContext: function (value) {
        this._dataContext = value;
		this._bind();
    }
};
Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingSelector.registerClass("Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingSelector", Sys.UI.Control);