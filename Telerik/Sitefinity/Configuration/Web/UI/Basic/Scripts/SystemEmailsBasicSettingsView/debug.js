Type.registerNamespace("Telerik.Sitefinity.Configuration.Web.UI.Basic");

Telerik.Sitefinity.Configuration.Web.UI.Basic.SystemEmailsBasicSettingsView = function (element) {
    Telerik.Sitefinity.Configuration.Web.UI.Basic.SystemEmailsBasicSettingsView.initializeBase(this, [element]);
    this._window = null;
    this._windowBody = null;

    this._systemEmailsGrid = null;
    this._serviceUrl = null;
    this._clientLabelManager = null;
    this._messageTemplateEditor = null;
    this._siteSelector = null;
    this._siteId = null;

    this._basicSettingsSitePanel = null;
    this._changeInheritanceDelegate = null;

    this._isInherited = false;
}

Telerik.Sitefinity.Configuration.Web.UI.Basic.SystemEmailsBasicSettingsView.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        Telerik.Sitefinity.Configuration.Web.UI.Basic.SystemEmailsBasicSettingsView.callBaseMethod(this, "initialize");

        $(document).ready(this.onReady.bind(this));

        if (this.get_basicSettingsSitePanel()) {
            if (!this._changeInheritanceDelegate) {
                this._changeInheritanceDelegate = Function.createDelegate(this, this._onChangeInheritance);
            }
            this.get_basicSettingsSitePanel().add_command(this._changeInheritanceDelegate);
        }
    },
    dispose: function () {
        Telerik.Sitefinity.Configuration.Web.UI.Basic.SystemEmailsBasicSettingsView.callBaseMethod(this, "dispose");

        if (this._changeInheritanceDelegate) {
            this.get_basicSettingsSitePanel().remove_command(this._changeInheritanceDelegate)
            delete this._changeInheritanceDelegate;
        }
    },

    onReady: function () {
        var that = this;
        var columns = [
            {
                title: this.get_clientLabelManager().getLabel("ConfigDescriptions", "Subject"),
                template: "<div><span class='sfItemTitle sfLnk sfavailable'>#: IsModified ? VariationSubject : Subject #</span></div>",
                encoded: true,
                attributes: {
                    "class": "sfTitleCol"
                },
                headerAttributes: {
                    "class": "sfTitleCol"
                }
            },
            {
                title: this.get_clientLabelManager().getLabel("ConfigDescriptions", "UsedIn"),
                template: "<div><span>#:  UsedIn #</span></div>",

                attributes: {
                    "class": "sfShort sfAlignLeft"
                },
                headerAttributes: {
                    "class": "sfShort sfAlignLeft"
                }
            },
            {
                title: this.get_clientLabelManager().getLabel("ConfigDescriptions", "Modified"),
                template: "<em>" + 
                                "#= (VariationSubject && VariationSubject != Subject) " +
                                "|| (VariationBodyHtml && VariationBodyHtml != BodyHtml) " + 
                                "|| (VariationSenderEmailAddress && SenderEmailAddress != VariationSenderEmailAddress)  " + 
                                "|| (VariationSenderName && SenderName != VariationSenderName)  " + 
                                "? 'Modified' : ''#" +
                        "</em>",

                attributes: {
                    "class": "sfShort sfAlignCenter"
                },
                headerAttributes: {
                    "class": "sfShort sfAlignCenter"
                }
            }
        ];

        var url = this.get_serviceUrl() + "/system_emails?siteId=" + this.get_siteId();

        $(this.get_systemEmailsGrid()).kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: url,
                        contentType: 'application/json; charset=utf-8',
                        type: "GET",
                        dataType: "json"
                    },
                },
                schema: {
                    data: function(response) {
                        
                        if (response.Context) { 
                            for(var i = 0; i < response.Context.length; i++) {
                                if (response.Context[i].Key === "IsInherited") {
                                    if (response.Context[i].Value === "True") {
                                        that._isInherited = true;
                                    } else {
                                        that._isInherited = false;
                                    }
                                        
                                    break;
                                }
                            }
                        }

                        return response.Items;
                    },
                    total: "TotalCount"
                }
            },
            columns: columns,
            scrollable: false,
            dataBound: $.proxy(this._onPageDataBound, this)
        });
    },
    openEditWindow: function (dataItem, successCallback) {
        if(this.get_basicSettingsSitePanel() && this._isInherited) {
            return;
        }
        this.get_messageTemplateEditor().open(dataItem, successCallback)
    },
   _onPageDataBound: function (arg) {
        var that = this;

        if (this.get_basicSettingsSitePanel()) {
            this.get_basicSettingsSitePanel().set_inherits(this._isInherited);
            this.get_basicSettingsSitePanel().refresh();
            $(".sfBasicSettingsWrp .sfLinkBtn").show();

            if(this._isInherited) { 
                $(this.get_systemEmailsGrid()).addClass("sfDisabledGrid");
            } else {
                $(this.get_systemEmailsGrid()).removeClass("sfDisabledGrid");
            }
        }

        var pageGrid = $(this.get_systemEmailsGrid()).data("kendoGrid");

        $.each(arg.sender.dataItems(), function (i, item) {
            var dataItem = item;

            $(arg.sender.table).find('tr[data-uid=' + dataItem.uid + ']').click(function () {

                that.openEditWindow(dataItem, function () { pageGrid.dataSource.read(); });
            });
        });
    },
    _onChangeInheritance: function (sender, args) {
        
         if (args._commandName == "changeInheritance") {

             if (args._commandArgument == "break") {
                this._action = "break";

                var url = this.get_serviceUrl() + "/create_variations";
                this._changeInheritanceServiceCall(url);

             }
             else if (args._commandArgument == "inherit") {

                if (this._action == "break") {
                    this._action = null;
                } else {
                    this._action = "inherit";
                }

                var url = this.get_serviceUrl() + "/delete_variations";
                this._changeInheritanceServiceCall(url);
            }

            this.get_basicSettingsSitePanel().refresh();
         }
     },
    _changeInheritanceServiceCall: function(url) {
        var that = this;
        var pageGrid = $(this.get_systemEmailsGrid()).data("kendoGrid");

        $.ajax({
            type: "PUT",
            data: JSON.stringify(this.get_siteId()),
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        })
        .done(function (result) {
            pageGrid.dataSource.read();
        })
        .fail(function (err) {
            alert(err);
        });
    },
    get_systemEmailsGrid: function () {
        return this._systemEmailsGrid;
    },
    set_systemEmailsGrid: function (value) {
        this._systemEmailsGrid = value;
    },
    get_serviceUrl: function () {
        return this._serviceUrl;
    },
    set_serviceUrl: function (value) {
        this._serviceUrl = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_messageTemplateEditor: function () {
        return this._messageTemplateEditor;
    },
    set_messageTemplateEditor: function (value) {
        this._messageTemplateEditor = value;
    },
    get_siteSelector: function () {
        return this._siteSelector;
    },
    set_siteSelector: function (value) {
        this._siteSelector = value;
    },
    get_siteId: function () {
        return this._siteId;
    },
    set_siteId: function (value) {
        this._siteId = value;
    },
    get_basicSettingsSitePanel: function () {
        return this._basicSettingsSitePanel;
    },
    set_basicSettingsSitePanel: function (value) {
        this._basicSettingsSitePanel = value;
    }
}

Telerik.Sitefinity.Configuration.Web.UI.Basic.SystemEmailsBasicSettingsView.registerClass("Telerik.Sitefinity.Configuration.Web.UI.Basic.SystemEmailsBasicSettingsView", Sys.UI.Control);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();