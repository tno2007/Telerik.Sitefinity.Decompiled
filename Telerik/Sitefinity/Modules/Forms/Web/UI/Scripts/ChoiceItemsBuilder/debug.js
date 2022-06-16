﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Fields");

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.ChoiceItemsBuilder = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.ChoiceItemsBuilder.initializeBase(this, [element]);
    this._minimumItemsCount = null;
    this._choiceItemsContainer = null;
    this._choiceItems = [];
    this._template = null;
    this._errorMessageLabel = null;

    this._choiceItemsDataView = null;
    this._showDefaultItemSelector = null;

    // delegates
    this._dataViewOnCommandDelegate = null;
    this._sortStopDelegatge = null;
    this._sortStartDelegate = null;
    this._loadDelegate = null;
    this._choiceItemsRenderedDelegate = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.ChoiceItemsBuilder.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.ChoiceItemsBuilder.callBaseMethod(this, 'initialize');
        this._dataViewOnCommandDelegate = Function.createDelegate(this, this._dataViewCommandHandler);

        this._choiceItemsRenderedDelegate = Function.createDelegate(this, this._choiceItemsRenderedHandler);
        this._sortStopDelegatge = Function.createDelegate(this, this._sortStopHandler);
        this._sortStartDelegate = Function.createDelegate(this, this._sortStartHandler);
        jQuery(this._choiceItemsContainer).sortable(
                                                        { update: this._sortStopDelegatge,
                                                            start: this._sortStartDelegate,
                                                            axis: 'y',
                                                            forcePlaceholderSize: true
                                                            //handle: '.sfDragAndDropTreeTableColumn'
                                                        }
                                                   );


        this._loadDelegate = Function.createDelegate(this, this._loadHandler);

        Sys.Application.add_load(this._loadDelegate);

    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.ChoiceItemsBuilder.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    // Checks if all the items has correct values and shows an error message if needed
    validate: function () {
        var result = true;
        for (var i = 0, length = this._choiceItems.length; i < length; i++) {
            if (!this._choiceItems[i].Text || this._choiceItems[i].Text.trim() == "") {
                result = false;
                break;
            }
        }

        jQuery(this._errorMessageLabel).toggle(!result);
        return result;
    },

    /* --------------------------------- event handlers ---------------------------------- */
    _dataViewCommandHandler: function (sender, args) {
        var commandName = args.get_commandName();
        var index = args.get_commandArgument();
        switch (commandName) {
            case "Add":
                // fix for FireFox
                if ($telerik.isFirefox) {
                    this._updateChoiceItems();
                }

                this._choiceItems.beginUpdate();
                this._choiceItems.insert(index + 1, { Text: "", Value: "", Selected: false });
                this._choiceItems.endUpdate();
                dialogBase.resizeToContent();
                break;
            case "Remove":
                if (this._choiceItems.length > this._minimumItemsCount) {
                    var item = this._choiceItemsDataView.findContext(args.get_commandSource());
                    this._choiceItems.beginUpdate();
                    this._choiceItems.remove(item.dataItem);
                    this._choiceItems.endUpdate();
                }
                dialogBase.resizeToContent();
                break;
            case "DefaultChange":
                this._choiceItems.beginUpdate();
                var item = this._choiceItemsDataView.findContext(args.get_commandSource());
                var count = this._choiceItems.length;
                while (count--) {
                    this._choiceItems[count].Selected = false;
                }
                item.dataItem.Selected = true;
                this._choiceItems.endUpdate();
                break;
        }
    },

    _loadHandler: function (e) {
        this._choiceItemsDataView = $create(Sys.UI.DataView, {}, {}, {}, this._choiceItemsContainer);
        this._choiceItemsDataView.add_command(this._dataViewOnCommandDelegate);
        this._choiceItemsDataView.add_rendered(this._choiceItemsRenderedDelegate);
        this._choiceItemsDataView.set_data(this._choiceItems);
    },

    _choiceItemsRenderedHandler: function () {
        var jItems = jQuery(this.get_element()).find(".sfChoiceItemsBuilderDefaultSelectedRadioButton");
        if (this._showDefaultItemSelector === false)
            jItems.hide();
        else
            jItems.show();
    },

    _sortStopHandler: function (event, ui) {
        var el = ui.item;
        var newIndex = jQuery(this._choiceItemsContainer).children().index(el);
        var oldIndex = jQuery(el).data("startIndex");

        var dataItem = this._choiceItemsDataView.findContext(el[0]).dataItem;

        this._choiceItems.beginUpdate();
        this._choiceItems.remove(dataItem);
        this._choiceItems.insert(newIndex, dataItem);
        this._choiceItems.endUpdate();
    },

    _sortStartHandler: function (event, ui) {
        // fix for FireFox
        if ($telerik.isFirefox) {
            this._updateChoiceItems();
        }

        var el = ui.item;
        var startIndex = jQuery(this._choiceItemsContainer).children().index(el);
        jQuery(el).data("startIndex", startIndex);
    },

    /* --------------------------------- private methods --------------------------------- */

    _updateChoiceItems: function () {
        var arr = jQuery(this._choiceItemsContainer).find("input[type=text]");
        var oThis = this;
        jQuery.each(arr, function (i) {
            oThis._choiceItems[i].Text = this.value;
            oThis._choiceItems[i].Value = this.value;
        });
    },

    /* --------------------------------- properties -------------------------------------- */

    // Returns the current choice items
    get_choiceItems: function () {
        return this._choiceItems;
    },
    // Sets and binds the choice items
    set_choiceItems: function (value) {
        this._choiceItems = value;
        Sys.Observer.makeObservable(this._choiceItems);
        if (this._choiceItemsDataView) {
            this._choiceItemsDataView.set_data(this._choiceItems);
        }
    },
    // Returns the title of the default selected item from the available choice items
    get_defaultSelectedTitle: function () {
        var result = "";
        var count = this._choiceItems.length;
        while (count--) {
            var item = this._choiceItems[count];
            if (item.Selected) {
                result = item.Text;
            }
        }
        return result;
    },
    // TODO: work directly with choice items
    set_choiceItemsTitles: function (value, defaultSelectedTitle) {
        value = value.split(";");
        var choiceItems = [];
        var defaultSet = false;
        for (var i = 0, length = value.length; i < length; i++) {
            var v = value[i];
            if (v != "") {
                var choiceItem = { Text: v,
                    Value: v,
                    Selected: false
                };
                if (choiceItem.Text == defaultSelectedTitle) {
                    choiceItem.Selected = true;
                    if (!defaultSet) {
                        defaultSet = true;
                    }
                }
                choiceItems.push(choiceItem);
            }
        }

        if (!defaultSet) {
            choiceItems[0].Selected = true;
        }

        this.set_choiceItems(choiceItems);
    },

    // TODO: work directly with choice items
    get_choiceItemsTitles: function () {
        var result = [];
        for (var i = 0, length = this._choiceItems.length; i < length; i++) {
            result.push(this._choiceItems[i].Text);
        }

        return result.join(";");
    },

    get_choiceItemsContainer: function () { return this._choiceItemsContainer; },
    set_choiceItemsContainer: function (value) { this._choiceItemsContainer = value; },

    get_errorMessageLabel: function () { return this._errorMessageLabel; },
    set_errorMessageLabel: function (value) { this._errorMessageLabel = value; }
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.ChoiceItemsBuilder.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.ChoiceItemsBuilder', Sys.UI.Control);