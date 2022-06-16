﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ExportSubscribersDialog = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ExportSubscribersDialog.initializeBase(this, [element]);

    this._allSubscribersRadio = null;
    this._commaSeparatedListRadio = null;
    this._tabSeparatedListRadio = null;
    this._subsFromSelectedListsRadio = null;
    this._subsNoMailingListRadio = null;
    this._mailingListsPanel = null;
    this._exportSubscribersButton = null;
    this._selectListsButton = null;
    this._windowManager = null;
    this._selectListsDialog = null;
    this._cancelButton = null;
    this._exportSubscribersLabel = null;
    this._exportOptions = null;
    this._doNotExportExistingSubscribers = null;

    this._selectListsButtonDelegate = null;
    this._subsRadioClickDelegate = null;
    this._exportSubscribersButtonDelegate = null;
    this._cancelButtonClickDelegate = null;
    this._validateSubscribersSuccessDelegate = null;

    this._selectListsDialogLoadedDelegate = null;
    this._selectListsDialogShowDelegate = null;
    this._selectListsDialogCloseDelegate = null;

    this._selectedListsKeys = new Array();
    this._selectedLists = new Array();
    this._selectedListsDialog = null;
    this._selectedListsElement = null;
    this._selectListsLabel = null;
    this._clientLabelManager = null;
    this._listIdsHidden = null;
    this._messageControl = null;

    this._exportHttpHandlerUrl = null;
    this._subscriberServiceUrl = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ExportSubscribersDialog.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ExportSubscribersDialog.callBaseMethod(this, "initialize");

        this._subsRadioClickDelegate = Function.createDelegate(this, this._subsRadioClickHandler);
        $addHandler(this._subsFromSelectedListsRadio, 'click', this._subsRadioClickDelegate);
        $addHandler(this._allSubscribersRadio, 'click', this._subsRadioClickDelegate);
        $addHandler(this._subsNoMailingListRadio, 'click', this._subsRadioClickDelegate);

        this._exportSubscribersButtonDelegate = Function.createDelegate(this, this._exportSubscribersButtonHandler);
        $addHandler(this._exportSubscribersButton, 'click', this._exportSubscribersButtonDelegate);

        this._cancelButtonClickDelegate = Function.createDelegate(this, this._cancelButtonClickHandler);
        $addHandler(this._cancelButton, 'click', this._cancelButtonClickDelegate);

        this._selectListsButtonDelegate = Function.createDelegate(this, this._selectListsButtonHandler);
        $addHandler(this._selectListsButton, 'click', this._selectListsButtonDelegate);

        this._selectListsDialogLoadedDelegate = Function.createDelegate(this, this._selectListsDialogLoadedHandler);
        this._selectListsDialogShowDelegate = Function.createDelegate(this, this._selectListsDialogShow);

        this._selectListsDialogCloseDelegate = Function.createDelegate(this, this._selectListsDialogClosed);
        this.get_selectListsDialog().add_close(this._selectListsDialogCloseDelegate);

        this._validateSubscribersSuccessDelegate = Function.createDelegate(this, this._validateSubscribersSuccessHandler);
    },

    dispose: function () {
        if (this._subsRadioClickDelegate) {
            if (this._subsFromSelectedListsRadio) {
                $removeHandler(this._subsFromSelectedListsRadio, 'click', this._subsRadioClickDelegate);
            }
            if (this._allSubscribersRadio) {
                $removeHandler(this._allSubscribersRadio, 'click', this._subsRadioClickDelegate);
            }
            if (this._subsNoMailingListRadio) {
                $removeHandler(this._subsNoMailingListRadio, 'click', this._subsRadioClickDelegate);
            }

            delete this._subsRadioClickDelegate;
        }

        if (this._exportSubscribersButtonDelegate) {
            if (this._exportSubscribersButton) {
                $removeHandler(this._exportSubscribersButton, 'click', this._exportSubscribersButtonDelegate);
            }
            delete this._exportSubscribersButtonDelegate;
        }

        if (this._cancelButtonClickDelegate) {
            if (this._cancelButton) {
                $removeHandler(this._cancelButton, 'click', this._cancelButtonClickDelegate);
            }
            delete this._cancelButtonClickDelegate;
        }

        if (this._selectListsButtonDelegate) {
            if (this._selectListsButton) {
                $removeHandler(this._selectListsButton, 'click', this._selectListsButtonDelegate);
            }
            delete this._selectListsButtonDelegate;
        }

        if (this._selectListsDialogLoadedDelegate) {
            delete this._selectListsDialogLoadedDelegate;
        }

        if (this._selectListsDialogShowDelegate) {
            delete this._selectListsDialogShowDelegate;
        }

        if (this._selectListDialogCloseDelegate) {
            if (this.get_selectListsDialog()) {
                this.get_selectListsDialog().remove_close(this._selectListsDialogCloseDelegate);
            }
            delete this._selectListsDialogCloseDelegate;
        }

        if (this._validateSubscribersSuccessDelegate) {
            delete this._validateSubscribersSuccessDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ExportSubscribersDialog.callBaseMethod(this, "dispose");
    },

    show: function (listIds, listTitle) {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ExportSubscribersDialog.callBaseMethod(this, "show");

        this.reset();

        var title = null;
        if (listTitle) {
            title = String.format(this.get_clientLabelManager().getLabel("NewslettersResources", "ExportSubscribersFrom"), listTitle);
        }
        else {
            title = this.get_clientLabelManager().getLabel("NewslettersResources", "ExportSubscribers");
        }
        this.get_exportSubscribersLabel().innerHTML = title;

        if (listIds) {
            this.get_listIdsHidden().value = listIds;
            jQuery(this.get_exportOptions()).hide();
            jQuery(this.get_subsFromSelectedListsRadio()).attr('checked', 'checked');
            jQuery(this.get_doNotExportExistingSubscribers()).attr('checked', 'checked');
        }
        else {
            jQuery(this.get_exportOptions()).show();
        }
    },

    reset: function () {
        this.get_listIdsHidden().value = "";
        jQuery(this.get_commaSeparatedListRadio()).click();
        jQuery(this.get_allSubscribersRadio()).click();
        jQuery(this.get_doNotExportExistingSubscribers()).prop("checked", false);

        this.get_selectedListsElement().innerHTML = "";
        this._selectedLists = new Array();
        this.get_selectListsLabel().innerHTML = this.get_clientLabelManager().getLabel("NewslettersResources", "SelectMailingLists");
    },

    /* *************************** event handlers *************************** */

    _subsRadioClickHandler: function () {
        if (jQuery(this.get_subsFromSelectedListsRadio()).is(':checked')) {
            jQuery(this.get_mailingListsPanel()).show();
        }
        else {
            jQuery(this.get_mailingListsPanel()).hide();
        }
    },

    _exportSubscribersButtonHandler: function () {
        if (jQuery(this.get_subsFromSelectedListsRadio()).is(':checked') && (this.get_listIdsHidden().value == "")) {
            this.get_messageControl().showNegativeMessage(this.get_clientLabelManager().getLabel('NewslettersResources', 'NoMailingListSelected'));
        }
        else {
            this._validateSubscribers();
        }
    },

    _validateSubscribers: function () {
        jQuery.ajax({
            type: 'GET',
            url: this._subscriberServiceUrl + "?take=1",
            contentType: "application/json",
            processData: false,
            success: this._validateSubscribersSuccessDelegate
        });
    },

    _validateSubscribersSuccessHandler: function (result, args) {
        if (result.TotalCount == 0) {
            this.get_messageControl().showNegativeMessage(this.get_clientLabelManager().getLabel('NewslettersResources', 'NoSubscribers'));
        }
        else {
            var iframe;
            iframe = document.getElementById("hiddenDownloader");
            if (iframe === null) {
                iframe = document.createElement('iframe');
                iframe.id = "hiddenDownloader";
                iframe.style.visibility = "hidden";
                document.body.appendChild(iframe);
            }

            var fullExportHandlerUrl = this._exportHttpHandlerUrl;

            fullExportHandlerUrl += String.format("&isCSV={0}&ids={1}&exportExSub={2}&allSub={3}",
                                jQuery(this.get_commaSeparatedListRadio()).is(':checked'),
                                this.get_listIdsHidden().value,
								jQuery(this.get_doNotExportExistingSubscribers()).is(':checked'),
								jQuery(this.get_allSubscribersRadio()).is(':checked'));

            iframe.src = fullExportHandlerUrl;
            this.close();
        }
    },

    _cancelButtonClickHandler: function () {
        this.close();
    },

    _selectListsButtonHandler: function () {
        //need to minimize the kendo dialog here
        this.get_kendoWindow().minimize();

        this.get_selectListsDialog().add_pageLoad(this._selectListsDialogLoadedDelegate);
        this.get_selectListsDialog().show();
        Telerik.Sitefinity.centerWindowHorizontally(this.get_selectListsDialog());
    },

    _selectListsDialogLoadedHandler: function (sender, args) {
        this.get_selectListsDialog().remove_pageLoad(this._selectListsDialogLoadedDelegate);
        this.get_selectListsDialog().add_show(this._selectListsDialogShowDelegate);
        this._selectListsDialogShow();
    },

    _selectListsDialogShow: function (sender, args) {
        this.get_selectListsDialog().get_contentFrame().contentWindow.createDialog(this._selectedListsKeys);
    },

    _selectListsDialogClosed: function (sender, args) {
        // need to maximize the kendo dialog here
        this.get_kendoWindow().restore();

        var argument = args.get_argument();

        if (argument) {
            this.get_selectedListsElement().innerHTML = '';
            this._selectedLists = argument;
            var listIds = [];
            for (var i = 0; i < this._selectedLists.length; i++) {
                var selectedList = this._selectedLists[i];
                this._selectedListsKeys[i] = selectedList.Id;
                listIds.push(selectedList.Id);
                this._addListUIElement(selectedList, this._selectedListsKeys, this._selectedLists, this._selectedListsElement, this._selectListsLabel, this.get_clientLabelManager());
            }
            if (this._selectedLists.length > 0) {
                this.get_selectListsLabel().innerHTML = this.get_clientLabelManager().getLabel("Labels", "ChangeEllipsis");
            }
            else {
                this.get_selectListsLabel().innerHTML = this.get_clientLabelManager().getLabel("NewslettersResources", "SelectMailingLists");
            }

            this.get_listIdsHidden().value = listIds.join(",");
        }
    },

    _addListUIElement: function (list, selectedListsKeys, selectedLists, selectedListsElement, selectListsLabel, clientLabelManager) {
        var removeSpanId = "remove_" + list.Id;
        var alreadyExists = jQuery(this.get_selectedListsElement()).find("#" + removeSpanId).length > 0;

        if (!alreadyExists) {
            var newItem = document.createElement("LI");

            var removeSpan = document.createElement("SPAN");
            removeSpan.setAttribute("id", removeSpanId);
            jQuery(removeSpan).click(function () {
                var removedId = removeSpanId.substr(removeSpanId.indexOf('_') + 1);
                var itemIndex = selectedListsKeys.indexOf(removedId);
                if (itemIndex > -1) {
                    selectedLists.splice(itemIndex, 1);
                    selectedListsKeys.splice(itemIndex, 1);
                }

                selectedListsElement.removeChild(this.parentNode);
                if (selectedListsElement.innerHTML == "") {
                    selectListsLabel.innerHTML = clientLabelManager.getLabel("NewslettersResources", "SelectMailingLists");
                }
            });

            newItem.appendChild(document.createTextNode(list.Title));
            newItem.appendChild(removeSpan);
            selectedListsElement.appendChild(newItem);
        }
    },

    get_allSubscribersRadio: function () {
        return this._allSubscribersRadio;
    },
    set_allSubscribersRadio: function (value) {
        this._allSubscribersRadio = value;
    },
    get_commaSeparatedListRadio: function () {
        return this._commaSeparatedListRadio;
    },
    set_commaSeparatedListRadio: function (value) {
        this._commaSeparatedListRadio = value;
    },
    get_tabSeparatedListRadio: function () {
        return this._tabSeparatedListRadio;
    },
    set_tabSeparatedListRadio: function (value) {
        this._tabSeparatedListRadio = value;
    },
    get_subsFromSelectedListsRadio: function () {
        return this._subsFromSelectedListsRadio;
    },
    set_subsFromSelectedListsRadio: function (value) {
        this._subsFromSelectedListsRadio = value;
    },
    get_subsNoMailingListRadio: function () {
        return this._subsNoMailingListRadio;
    },
    set_subsNoMailingListRadio: function (value) {
        this._subsNoMailingListRadio = value;
    },
    get_mailingListsPanel: function () {
        return this._mailingListsPanel;
    },
    set_mailingListsPanel: function (value) {
        this._mailingListsPanel = value;
    },
    get_exportSubscribersButton: function () {
        return this._exportSubscribersButton;
    },
    set_exportSubscribersButton: function (value) {
        this._exportSubscribersButton = value;
    },
    get_selectListsButton: function () {
        return this._selectListsButton;
    },
    set_selectListsButton: function (value) {
        this._selectListsButton = value;
    },
    get_windowManager: function () {
        return this._windowManager;
    },
    set_windowManager: function (value) {
        this._windowManager = value;
    },
    get_selectListsDialog: function () {
        return this._selectListsDialog;
    },
    set_selectListsDialog: function (value) {
        this._selectListsDialog = value;
    },
    get_selectedListsElement: function () {
        return this._selectedListsElement;
    },
    set_selectedListsElement: function (value) {
        this._selectedListsElement = value;
    },
    get_selectListsLabel: function () {
        return this._selectListsLabel;
    },
    set_selectListsLabel: function (value) {
        this._selectListsLabel = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_listIdsHidden: function () {
        return this._listIdsHidden;
    },
    set_listIdsHidden: function (value) {
        this._listIdsHidden = value;
    },
    get_cancelButton: function () {
        return this._cancelButton;
    },
    set_cancelButton: function (value) {
        this._cancelButton = value;
    },
    get_exportSubscribersLabel: function () {
        return this._exportSubscribersLabel;
    },
    set_exportSubscribersLabel: function (value) {
        this._exportSubscribersLabel = value;
    },
    get_exportOptions: function () {
        return this._exportOptions;
    },
    set_exportOptions: function (value) {
        this._exportOptions = value;
    },
    get_doNotExportExistingSubscribers: function () {
        return this._doNotExportExistingSubscribers;
    },
    set_doNotExportExistingSubscribers: function (value) {
        this._doNotExportExistingSubscribers = value;
    },
    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ExportSubscribersDialog.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ExportSubscribersDialog',
                                                                                          Telerik.Sitefinity.Web.UI.Kendo.KendoWindow);