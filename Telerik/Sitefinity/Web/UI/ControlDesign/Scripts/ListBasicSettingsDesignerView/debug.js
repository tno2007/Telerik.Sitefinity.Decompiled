/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type._registerScript("ListBasicSettingsDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");


Telerik.Sitefinity.Web.UI.ControlDesign.ListBasicSettingsDesignerView = function (element) {
    Telerik.Sitefinity.Web.UI.ControlDesign.ListBasicSettingsDesignerView.initializeBase(this, [element]);
	this._dataFieldNameControlIdMap = null;
	this._parentDesigner = null;
	this._refreshing = false;
	this._listLimitControl = null;
	this._currentViewName = null;
	this._viewsList = null;
}

Telerik.Sitefinity.Web.UI.ControlDesign.ListBasicSettingsDesignerView.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {

        Telerik.Sitefinity.Web.UI.ControlDesign.ListBasicSettingsDesignerView.callBaseMethod(this, 'initialize');

        this._radioClickDelegate = Function.createDelegate(this, this._setMasterViewMode);
        this.get_radioChoices().click(this._radioClickDelegate);

        jQuery('#countersDetailsLink').click(function () {
            jQuery('#countersDetails').toggleClass("sfDisplayNone");
        });
        // prevent memory leaks for jQuery
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },

    dispose: function () {
        //Add custom dispose actions here
        Telerik.Sitefinity.Web.UI.ControlDesign.ListBasicSettingsDesignerView.callBaseMethod(this, 'dispose');
        if (this._radioClickDelegate) {
            delete this._radioClickDelegate;
        }
    },

    /* --------------------------------- public methods --------------------------------- */

    refreshUI: function () {
        this._refreshing = true;
        var control = this.get_controlData();
        this._adjustControlData(control);
        var currentView = this.get_currentView();

        // if the view is set in the advanced mode it might be something that is not defined
        if (currentView.AllowPaging) {
            this._displayAllowPaging(currentView.ItemsPerPage);
        }
        else {
            if (currentView.ItemsPerPage > 0) {
                this._displayLimitedList(currentView.ItemsPerPage);
            }
            else {
                this._displayShowAllItems();
            }
        }

        var sortExpressionField = this._getFieldControl("CommonMasterDefinition.SortExpression");
        if (sortExpressionField._get_listItemByValue(currentView.SortExpression).length == 0) {
            sortExpressionField.set_value("custom");
        }
        else {
            sortExpressionField.set_value(currentView.SortExpression);
        }
        this._refreshing = false;
    },

    applyChanges: function () {
        var parent = this.get_parentDesigner();
        var control = this.get_controlData();
        var itemsPerPage = 0;
        var allowPaging = false;
        for (var dataFieldName in this._dataFieldNameControlIdMap) {
            var field = this._getFieldControl(dataFieldName);

            if (dataFieldName == "CommonMasterDefinition.ItemsPerPage") {
                if (field.get_value() > 0) {
                    itemsPerPage = field.get_value();
                    allowPaging = true;
                }
            }
            else if (dataFieldName == "ListLimit") {
                //this is work-around of having 2 fieldControls bound to the same field
                //we use ItemsPerPage - for both limiting top X items or having X items per page.
                if (field.get_value() > 0) {
                    itemsPerPage = field.get_value();
                }
            }
            else if (dataFieldName == "CommonMasterDefinition.SortExpression") {
                if (field.get_value() != "custom") {
                    parent.setValueToControlData(this._resolvePropertyPath(field), field.get_value(), control);
                }
                else {
                    //hack: the control data is updated after the property persistence that is why we take the value from the property bag of  the property editor
                    var propertyInfo = {};
                    var sortProperty = parent.get_propertyEditor().findProperty("/ControlDefinition/Views/" + this.get_currentViewName() + "/SortExpression", propertyInfo);
                    if (propertyInfo.WasDirty) {
                        parent.setValueToControlData(this._resolvePropertyPath(field), sortProperty.PropertyValue, control);
                    }
                }
            }
            else {
                parent.setValueToControlData(this._resolvePropertyPath(field), field.get_value(), control);
            }
        }
        this.get_currentView().ItemsPerPage = itemsPerPage;
        this.get_currentView().AllowPaging = allowPaging;
    },

    /* --------------------------------- private methods --------------------------------- */

    // gets the reference to the field control by the field name that it edits
    _getFieldControl: function (dataFieldName) {
        return $find(this._dataFieldNameControlIdMap[dataFieldName]);
    },

    _templateValueChanged: function (sender) {
        if (sender.get_value() == "") {
            jQuery(this.get_editTemplateLink()).hide();
        }
        else {
            jQuery(this.get_editTemplateLink()).show();
        }
        // TODO: Refactor this to set the embedded template path when template has not been changed
        this.get_currentView().TemplateKey = sender.get_value();
    },

    // sets the the Master view mode show all/paged/top n
    _setMasterViewMode: function (sender) {
        var radioID = sender.target.value;
        var currentView = this.get_currentView();
        var limitControl = this._getFieldControl('ListLimit');
        var itemsPerPageControl = this._getFieldControl('CommonMasterDefinition.ItemsPerPage');
        switch (radioID) {
            case "listRadio_showAll":
                if (!this._refreshing) {
                    currentView.ItemsPerPage = 0;
                    currentView.AllowPaging = false;
                }
                itemsPerPageControl.reset();
                this.disableElement(itemsPerPageControl.get_element());
                limitControl.reset();
                this.disableElement(limitControl.get_element());
                break;
            case "listRadio_showPaged":
                if (!this._refreshing) {
                    currentView.AllowPaging = true;
                }
                this.enableElement(itemsPerPageControl.get_element());
                limitControl.reset();
                this.disableElement(limitControl.get_element());

                break;
            case "listRadio_showLimited":
                if (!this._refreshing) {
                    currentView.AllowPaging = false;
                }
                itemsPerPageControl.reset();
                this.disableElement(itemsPerPageControl.get_element());
                this.enableElement(limitControl.get_element());
                break;
        }
        dialogBase.resizeToContent();
    },

    _onControlValueUpdated: function (sender, args) {
        if (this._refreshing)
            return;

        if (sender.get_dataFieldName() == 'ListLimit') {
            //this is work-around of having 2 fieldControls bound to the same field
            //we use ItemsPerPage - for both limiting top X items or having X items per page.
            this.get_currentView().ItemsPerPage = sender.get_value();
            return;
        }

        this.get_parentDesigner().setValueToControlData(this._resolvePropertyPath(sender), sender.get_value(), this.get_controlData());
    },

    // this function demonstrates on the user interface that paging is allowed
    _displayAllowPaging: function (itemsPerPage) {
        jQuery(this.get_element()).find("input[type|=radio][id*='listRadio_showPaged']").click();
        this._getFieldControl("CommonMasterDefinition.ItemsPerPage").set_value(itemsPerPage);
    },

    // this function demonstrated on the user interface that paging is not allowed, but that
    // the list should show display limited number of items
    _displayLimitedList: function (itemsPerPage) {
        this._getFieldControl("ListLimit").set_value(itemsPerPage);
        jQuery(this.get_element()).find("input[type|=radio][id*='listRadio_showLimited']").click();
    },

    // this function demonstrated on the user interface that all items should be shown
    _displayShowAllItems: function () {
        jQuery(this.get_element()).find("input[type|=radio][id*='listRadio_showAll']").click();
    },

    /* --------------------------------- properties --------------------------------- */

    // gets the reference to the control that sets the list limit settings
    get_listLimitControl: function () {
        return this._listLimitControl
    },

    // sets the reference to the control that sets the list limit settings
    set_listLimitControl: function (value) {
        this._listLimitControl = value;
    },

    // gets all the radio buttons in the container of this control
    get_radioChoices: function () {
        if (!this._radioChoices) {
            this._radioChoices = jQuery(this.get_element()).find('input[type|=radio]');
        }
        return this._radioChoices;
    },

    // gets the object which represents the map of field properties and respective controls
    // that are used to edit them
    set_dataFieldNameControlIdMap: function (value) {
        this._dataFieldNameControlIdMap = value;
    },

    // sets the object which represents the map of field properties and respective controls
    // that are used to edit them
    get_dataFieldNameControlIdMap: function () {
        return this._dataFieldNameControlIdMap;
    },

    // gets the reference to the parent designer control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },

    // sets the reference fo the parent designer control
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
    },

    get_viewsList: function () {
        return this._viewsList;
    },

    set_viewsList: function (value) {
        if (this._viewsList != value) {
            this._viewsList = value;
        }
    },

    // gets the name of the currently selected master view name of the content view control
    get_currentViewName: function () {
        return (this._currentViewName) ? this._currentViewName : this.get_controlData().MasterViewName;
    },

    // gets the client side representation of the currently selected master view definition
    get_currentView: function () {
        var currentViewName = this.get_currentViewName();
        var data = this.get_controlData();
        var views = data.ControlDefinition.Views;
        if (views.hasOwnProperty(currentViewName)) {
            return views[currentViewName];
        }
        else {
            views = data.ControlDefinition.Views;
            for (var v in views) {
                var current = views[v];
                if (current.IsMasterView) {
                    return current;
                }
            }
            return null;
        }
    },

    // this fixes the data if there are some incompatible values set in advanced mode
    _adjustControlData: function (data) {
        var view = data.ControlDefinition.Views[this.get_currentViewName()];
        if (!view) {
            var views = data.ControlDefinition.Views;
            var viewName;
            for (var key in views) {
                if (views[key].IsMasterView) {
                    viewName = key;
                    break;
                }
            }
            data.MasterViewName = viewName;
        }
    },

    _resolvePropertyPath: function (fieldControl) {
        var dataFieldName = fieldControl.get_dataFieldName();
        var viewPath = "ControlDefinition.Views['" + this.get_currentViewName() + "']";
        var propertyPath = dataFieldName.replace('CommonMasterDefinition', viewPath);
        return propertyPath;
    },

    // gets the object that represents the client side representation of the control 
    // being edited
    get_controlData: function () {
        var parent = this.get_parentDesigner();
        if (parent) {
            var pe = parent.get_propertyEditor();
            if (pe) {
                return pe.get_control();
            }
        }
        alert('Control designer cannot find the control properties object!');
    },

    enableElement: function (domElement) {
        $(domElement).find('input').each(function () {
            $(this).removeAttr('disabled');
        });
    },

    disableElement: function (domElement) {
        $(domElement).find('input').each(function () {
            $(this).attr('disabled', 'disabled');
        });
    }
}
Telerik.Sitefinity.Web.UI.ControlDesign.ListBasicSettingsDesignerView.registerClass('Telerik.Sitefinity.Web.UI.ControlDesign.ListBasicSettingsDesignerView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
