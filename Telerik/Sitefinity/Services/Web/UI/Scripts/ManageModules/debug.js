﻿Type.registerNamespace("Telerik.Sitefinity.Services.Web.UI");

Telerik.Sitefinity.Services.Web.UI.ManageModules = function (element) {
    Telerik.Sitefinity.Services.Web.UI.ManageModules.initializeBase(this, [element]);

    this._grid = null;
    this._webServiceUrl = null;
    this._dataSource = null;
    this._clientLabelManager = null;
    this._uninstallConfirmationDialog = null;
    this._deactivateConfirmationDialog = null;
    this._deleteConfirmationDialog = null;
    this._licenseRestrictionDialog = null;

    this._selectors = null;
    this._gridDataBoundDelegate = null;
    this._documentReadyDelegate = null;
    this._moduleDetailsWindow = null;

    this._loadingHTML = '<div id="sf_moduleStatusLoading" style="display: none; "><div class="RadAjax"><div class="raDiv">Loading</div><div class="raColor"></div></div><div class="sfWhiteBgDiv"></div></div>';
}

Telerik.Sitefinity.Services.Web.UI.ManageModules.prototype = {

    /* ----------------------------- setup and teardown ----------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Services.Web.UI.ManageModules.callBaseMethod(this, 'initialize');

        this._selectors = {
            gridViewRowTemplate: "#modulesManagementGridRowTemplate",
            grid: "#modulesManagementGrid",
            installNewModuleLink: "#installNewModule"
        };

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
        
        this._documentReadyDelegate = Function.createDelegate(this, this._documentReadyHandler);
        this._gridDataBoundDelegate = Function.createDelegate(this, this._gridDataBoundHandler);
        $(document).ready(this._documentReadyDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Services.Web.UI.ManageModules.callBaseMethod(this, 'dispose');

        if (this._documentReadyDelegate) {
            delete this._documentReadyDelegate;
        }

         if (this._gridDataBoundDelegate) {
            delete this._gridDataBoundDelegate;
        }
    },

    initializeDataSource: function () {
        this._dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: this.get_webServiceUrl(),
                    contentType: 'application/json; charset=utf-8',
                    type: "GET",
                    dataType: "json"
                },
                parameterMap: function (options) {
                    var queryObj = {
                        skip: options.skip,
                        take: options.take
                    };
                    if(options.filter)
                    {
                        var filter = options.filter;
                        if(filter.filters[0].field)
                            queryObj.field = filter.filters[0].field;
                        queryObj.operation = filter.filters[0].operator;
                        queryObj.value = filter.filters[0].value;
                        var taxonomyName = options.filter.filters[0].taxonomyName;
                        if(taxonomyName)
                            queryObj.taxonomyName = taxonomyName;
                    }
                    return queryObj;
                }
            },
            schema: {
                data: "Items",
                total: "TotalCount"
            },
            converters: {
                "text json": function (data) {
                    return Sys.Serialization.JavaScriptSerializer.deserialize(data);
                }
            },
            pageSize: 50,
            serverPaging: true,
            serverFiltering: true,
            change: this._changeDataSourceHandler.apply(this),
			requestStart: this._dataSourceRequestStartHandler.apply(this),
			error: this._dataSourceErrorHandler.apply(this)
        });
    },

    initializeGridView: function () {
        $(this.get_grid()).kendoGrid({
            dataSource: this._dataSource,
            rowTemplate: $.proxy(kendo.template($(this.getSelectors().gridViewRowTemplate).html()), this),
            scrollable: false,
            pageable: true,
            autoBind: true,
            dataBound: this._gridDataBoundDelegate
        });
    },
    
    getSelectors: function () {
        return this._selectors;
    },

    // ----------------------------------------- Event handlers ---------------------------------------

    _documentReadyHandler: function () {
        this.initializeDataSource();
        this.initializeGridView();
        this.showGridView();

        var self = this;
        $(this.getSelectors().installNewModuleLink).click(function () {
            self.get_moduleDetailsWindow().show(self, null);
        });
        
        jQuery("body").addClass("sfNoSidebar");   
        jQuery(this._loadingHTML).appendTo("body");
    },

    _gridDataBoundHandler: function (e) {
        var data = jQuery(this.get_grid()).data("kendoGrid").dataSource.data();
        var that = this;
        var hasFailedModule = false;
        for (var i = 0; i < data.length; i++) {

            var setClick = function (dataItem) {
                that._bindAnchorClick(that, dataItem, that.get_id() + "_MMGrid_Uninstall_" + dataItem.ClientId, Telerik.Sitefinity.Services.Web.UI.ModuleOperation.Uninstall);
                that._bindAnchorClick(that, dataItem, that.get_id() + "_MMGrid_Install_" + dataItem.ClientId, Telerik.Sitefinity.Services.Web.UI.ModuleOperation.Install);
                that._bindAnchorClick(that, dataItem, that.get_id() + "_MMGrid_Activate_" + dataItem.ClientId, Telerik.Sitefinity.Services.Web.UI.ModuleOperation.Activate);
                that._bindAnchorClick(that, dataItem, that.get_id() + "_MMGrid_Deactivate_" + dataItem.ClientId, Telerik.Sitefinity.Services.Web.UI.ModuleOperation.Deactivate);
                that._bindAnchorClick(that, dataItem, that.get_id() + "_MMGrid_Delete_" + dataItem.ClientId, Telerik.Sitefinity.Services.Web.UI.ModuleOperation.Delete);
                
                var editAnchor = jQuery("#" + that.get_id() + "_Edit_" + dataItem.ClientId);
                editAnchor.click(function (){
                    that.get_moduleDetailsWindow().show(that, dataItem);
                });
            }
                        
            setClick(data[i]);

            if (data[i].ErrorMessage && data[i].IsModuleLicensed) {
                hasFailedModule = true;
            }
        }
        if (hasFailedModule)
            $("#moduleOperationErrorMessage").show();
        else
            $("#moduleOperationErrorMessage").hide();

        $(".sfActionsMenu").kendoMenu({ animation: false, openOnClick: true });
        jQuery("[id$='modulesManagementGrid'] .sfMoreDetails").click(function () {
            var details = jQuery(this).siblings(".sfDetailsPopup");
            details.toggle();
            if(details.is(":visible")) {
                jQuery(this).parents(".sfDetailsPopupWrp").css("z-index","100");
            } else {
                jQuery(this).parents(".sfDetailsPopupWrp").css("z-index","10");
            }
        });
    },
    
    _bindAnchorClick: function (manageModulesItem, dataItem, id, operation){
        var anch = $("#" + id);
        if (anch){
            switch (operation) {
                case Telerik.Sitefinity.Services.Web.UI.ModuleOperation.Install:
                    anch.click(function () {
                            
                        if (dataItem.IsModuleLicensed) {
                            manageModulesItem._executeOperation(dataItem, operation, Telerik.Sitefinity.Services.Web.UI.StartupType.OnApplicationStart); 
                        }
                        else{
                            manageModulesItem._displayLicenseMessage(manageModulesItem, 'LicenseNotGrantedInstallTitle', 'LicenseNotGrantedInstallMessage');
                        }
                     });
                    break;
                case Telerik.Sitefinity.Services.Web.UI.ModuleOperation.Activate:
                    anch.click(function () {
                        if (dataItem.IsModuleLicensed) {
                            manageModulesItem._executeOperation(dataItem, operation);
                        }
                        else{
                            manageModulesItem._displayLicenseMessage(manageModulesItem, 'LicenseNotGrantedActivateTitle', 'LicenseNotGrantedActivateMessage');
                        }
                      });
                    break;
                case Telerik.Sitefinity.Services.Web.UI.ModuleOperation.Edit:
                    anch.click(function () {
                        if (dataItem.IsModuleLicensed) {
                            manageModulesItem._executeOperation(dataItem, operation);
                        }
                        else{
                            manageModulesItem._displayLicenseMessage(manageModulesItem, 'LicenseNotGrantedEditTitle', 'LicenseNotGrantedEditMessage');
                        }
                      });
                    break;
                case Telerik.Sitefinity.Services.Web.UI.ModuleOperation.Uninstall:
                    var that = this;
                    anch.click(function (){
                        that.get_uninstallConfirmationDialog().set_title(that.get_clientLabelManager().getLabel('Labels', 'UninstallModuleTitle'));
                        that.get_uninstallConfirmationDialog().set_message(that.get_clientLabelManager().getLabel('Labels', 'UninstallModuleMessage'));

                        var self = that;
                        var promptCallback = function (sender, args) {
                            if (args.get_commandName() == "uninstall") {
                                self._executeOperation(dataItem, operation);
                            }
                        };

                        that.get_uninstallConfirmationDialog().show_prompt(null, null, promptCallback);
                    });
                   
                    break;
                case Telerik.Sitefinity.Services.Web.UI.ModuleOperation.Deactivate: 
                    var that = this;
                    anch.click(function (){
                        that.get_deactivateConfirmationDialog().set_title(that.get_clientLabelManager().getLabel('Labels', 'DeactivateModuleTitle'));
                        var message = that.get_clientLabelManager().getLabel('Labels', 'DeactivateModuleMessage');
                        
                        if (dataItem.IsSystemModule) {
                            var warning = that.get_clientLabelManager().getLabel('Labels', 'DeactivateSystemModuleWarningMessage');
                            message = String.format("<p class='sfMBottom15'>{0}</p>{1}", warning, message);
                        }

                        that.get_deactivateConfirmationDialog().set_message(message);

                        var self = that;
                        var promptCallback = function (sender, args) {
                            if (args.get_commandName() == "deactivate") {
                                self._executeOperation(dataItem, operation);
                            }
                        };

                        that.get_deactivateConfirmationDialog().show_prompt(null, null, promptCallback);
                    });
                    break;
                case Telerik.Sitefinity.Services.Web.UI.ModuleOperation.Delete:
                    var that = this;
                    anch.click(function (){
                        that.get_deleteConfirmationDialog().set_title(that.get_clientLabelManager().getLabel('Labels', 'DeleteModuleTitle'));
                        that.get_deleteConfirmationDialog().set_message(that.get_clientLabelManager().getLabel('Labels', 'DeleteModuleMessage'));

                        var self = that;
                        var promptCallback = function (sender, args) {
                            if (args.get_commandName() == "delete") {
                                self._executeOperation(dataItem, operation);
                            }
                        };

                        that.get_deleteConfirmationDialog().show_prompt(null, null, promptCallback);
                    });
                    break;
                default:
                    alert('Unsupported module operation: ' + operation);
                    break;
            }
        }
    },

    _showConfirmationScreen: function(confirmationScreenId, dataItem, operation) {
        var confScreen = $("#" + confirmationScreenId);
        if (confScreen){
            var kendoWindow = $("<div />").kendoWindow({
//                    title: $('#<%= deleteModuleTitleHidden.ClientID %>').val(),
                    resizable: false,
                    modal: true,
                    animation: false
                });
                kendoWindow.addClass("sfSelectorDialog");
        }
    },

    _changeDataSourceHandler: function() {
//        console.log('change ds');
    },

    _dataSourceRequestStartHandler: function () {
//        console.log('start request');
    },
    
    _dataSourceErrorHandler: function (jqXHR) {
//        console.log('error handler');
//        alert('Failed to load data');
    },

    showGridView: function () {
        $(this.get_grid()).show();
    },

    fetchDataSource: function () {
        this._dataSource.fetch();
    },

    _executeOperation: function (dataItem, operation, startupType){
        var data = {};
        if (startupType != undefined)
            dataItem.StartupType = startupType;
        data.Item = dataItem;
        var self = this;
        this._setLoadingDim(true);
        $.ajax({
            type: 'PUT',
            url: this.get_webServiceUrl() + '?operation=' + operation,
            converters: {
                "text json": function (data) {
                    return Sys.Serialization.JavaScriptSerializer.deserialize(data);
                }
            },
            contentType: "application/json",
            processData: false,
            data: Telerik.Sitefinity.JSON.stringify(dataItem),
            success: function (result, args) {
                //reload the window to refresh the menu items
                window.location.reload();
            },
            error: function (jqXHR, textStatus, errorThrown){
                alert(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
                //rebind the grid to load the new data
                jQuery(self.get_grid()).data("kendoGrid").dataSource.read();
                self._setLoadingDim(false);
            }
        });
    },
    // ----------------------------------------- Private methods ---------------------------------------
    _setLoadingDim: function(flag) {
        if(flag) {
            jQuery("body").addClass("sfOverflowHidden");
            //jQuery('<div class="RadAjax"><div class="raDiv">Loading</div><div class="raColor"></div></div><div class="sfWhiteBgDiv"></div>').appendTo("body");
            jQuery("#sf_moduleStatusLoading").show();
        } else {
            jQuery("body").removeClass("sfOverflowHidden");
            jQuery("#sf_moduleStatusLoading").hide();       
        }
    },
    
    _displayLicenseMessage: function(manageModulesItem, titleResource, messageResource)
    {
        var resourceClass ='Labels';
        var moduleNotSupportedMessage = manageModulesItem.get_clientLabelManager().getLabel(resourceClass, 'ModuleNotSupportedByLicenseMessage');
        var contactSalesDepartmentMessage = manageModulesItem.get_clientLabelManager().getLabel(resourceClass, 'ContactSitefinitySalesMessage');
        var actionMessage = manageModulesItem.get_clientLabelManager().getLabel(resourceClass, messageResource);
        var message = String.format("<p class='sfMBottom15'>{0}<br/>{1}</p> {2}", moduleNotSupportedMessage, actionMessage, contactSalesDepartmentMessage);
        
        manageModulesItem.get_licenseRestrictionDialog().set_message(message);
        manageModulesItem.get_licenseRestrictionDialog().set_title(manageModulesItem.get_clientLabelManager().getLabel(resourceClass, titleResource));
        manageModulesItem.get_licenseRestrictionDialog().show_prompt(null, null, null);
    },

    // ----------------------------------------- Properties ---------------------------------------

    get_grid: function () {
        return this._grid;
    },
    set_grid: function (value) {
        this._grid = value;
    },
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    get_moduleDetailsWindow: function () {
        return this._moduleDetailsWindow;
    },
    set_moduleDetailsWindow: function (value) {
        this._moduleDetailsWindow = value;
    },
    get_uninstallConfirmationDialog: function () {
        return this._uninstallConfirmationDialog;
    },
    set_uninstallConfirmationDialog: function (value) {
        this._uninstallConfirmationDialog = value;
    },
    get_deactivateConfirmationDialog: function () {
        return this._deactivateConfirmationDialog;
    },
    set_deactivateConfirmationDialog: function (value) {
        this._deactivateConfirmationDialog = value;
    },
    get_deleteConfirmationDialog: function () {
        return this._deleteConfirmationDialog;
    },
    set_deleteConfirmationDialog: function (value) {
        this._deleteConfirmationDialog = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_licenseRestrictionDialog: function () {
        return this._licenseRestrictionDialog;
    },
    set_licenseRestrictionDialog: function (value) {
        this._licenseRestrictionDialog = value;
    }
}

Telerik.Sitefinity.Services.Web.UI.ManageModules.registerClass('Telerik.Sitefinity.Services.Web.UI.ManageModules', Sys.UI.Control);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

// ------------------------------------------------------------------------
// Module status Enum 
// ------------------------------------------------------------------------
Type.registerNamespace("Telerik.Sitefinity.Services.Web.UI");

Telerik.Sitefinity.Services.Web.UI.ModuleStatus = function () {
};
Telerik.Sitefinity.Services.Web.UI.ModuleStatus.prototype = {
        NotInstalled: 0,
        Inactive: 1,
        Active: 2,
        Failed: 3
};
Telerik.Sitefinity.Services.Web.UI.ModuleStatus.registerEnum("Telerik.Sitefinity.Services.Web.UI.ModuleStatus");


// ------------------------------------------------------------------------
// Module type Enum 
// ------------------------------------------------------------------------
Type.registerNamespace("Telerik.Sitefinity.Services.Web.UI");

Telerik.Sitefinity.Services.Web.UI.ModuleType = function () {
};
Telerik.Sitefinity.Services.Web.UI.ModuleType.prototype = {
       Static: 0,
       Dynamic: 1
};
Telerik.Sitefinity.Services.Web.UI.ModuleType.registerEnum("Telerik.Sitefinity.Services.Web.UI.ModuleType");


// ------------------------------------------------------------------------
// ModuleOperation Enum 
// ------------------------------------------------------------------------
Type.registerNamespace("Telerik.Sitefinity.Services.Web.UI");

Telerik.Sitefinity.Services.Web.UI.ModuleOperation = function () {
};
Telerik.Sitefinity.Services.Web.UI.ModuleOperation.prototype = {
    Install: 0,
    Uninstall: 1,
    Activate: 2,
    Deactivate: 3,
    Delete: 4,
    Edit: 5
};
Telerik.Sitefinity.Services.Web.UI.ModuleOperation.registerEnum("Telerik.Sitefinity.Services.Web.UI.ModuleOperation");


// ------------------------------------------------------------------------
// StartupType Enum 
// ------------------------------------------------------------------------
Type.registerNamespace("Telerik.Sitefinity.Services.Web.UI");

Telerik.Sitefinity.Services.Web.UI.StartupType = function () {
};
Telerik.Sitefinity.Services.Web.UI.StartupType.prototype = {
    OnApplicationStart: 0,
    OnFirstCall: 1,
    Manual: 2,
    Disabled: 3
};
Telerik.Sitefinity.Services.Web.UI.StartupType.registerEnum("Telerik.Sitefinity.Services.Web.UI.StartupType");
