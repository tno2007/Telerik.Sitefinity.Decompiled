﻿Type.registerNamespace("Telerik.Sitefinity.ModuleEditor.Web.UI");

Telerik.Sitefinity.ModuleEditor.Web.UI.ViewsSelector = function (element) {
    Telerik.Sitefinity.ModuleEditor.Web.UI.ViewsSelector.initializeBase(this, [element]);
    this._itemsSelector = null;
    this._allScreensRadio = null;
    this._someScreensRadio = null;
    this._nowhereRadio = null;

    this._loadDelegate == null
    this._radioClickedDelegate = null;

    this._itemsSelectorBinded = false;
    this._selectedViews = null;
    this._selectedViewsBoundDelegate = null;
}

Telerik.Sitefinity.ModuleEditor.Web.UI.ViewsSelector.prototype = {

    initialize: function () {
        Telerik.Sitefinity.ModuleEditor.Web.UI.ViewsSelector.callBaseMethod(this, "initialize");

        this._loadDelegate = Function.createDelegate(this, this._load);
        Sys.Application.add_load(this._loadDelegate);

        this._radioClickedDelegate = Function.createDelegate(this, this._radioClicked);
        $addHandler(this._allScreensRadio, 'click', this._radioClickedDelegate);
        $addHandler(this._someScreensRadio, 'click', this._radioClickedDelegate);
        $addHandler(this._nowhereRadio, 'click', this._radioClickedDelegate);

        this._itemsSelectorBoundDelegate = Function.createDelegate(this, this._itemsSelectorBound);
        this._itemsSelector.add_binderDataBound(this._itemsSelectorBoundDelegate);
    },

    dispose: function () {
        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }
        if (this._radioClickedDelegate) {
            if (this._allScreensRadio) {
                $removeHandler(this._allScreensRadio, 'click', this._radioClickedDelegate);
            }
            if (this._someScreensRadio) {
                $removeHandler(this._someScreensRadio, 'click', this._radioClickedDelegate);
            }
            if (this._nowhereRadio) {
                $removeHandler(this._nowhereRadio, 'click', this._radioClickedDelegate);
            }
            delete this._radioClickedDelegate;
        }
        if (this._itemsSelectorBoundDelegate) {
            this._itemsSelector.remove_binderDataBound(this._itemsSelectorBoundDelegate);
            delete this._itemsSelectorBoundDelegate;
        }
        Telerik.Sitefinity.ModuleEditor.Web.UI.ViewsSelector.callBaseMethod(this, "dispose");
    },

    /* -------------------- Public methods ---------------- */

    getSelectedViews: function () {
        if (this._allScreensRadio.checked) {
            this._selectedViews = "allViews";
            //return "allViews";
        }
        else if (this._someScreensRadio.checked) {
            var viewNames = [];
            var viewCount = this._itemsSelector.getSelectedItems().length;
            for (var i = 0; i < viewCount; i++) {
                viewNames.push(this._itemsSelector.getSelectedItems()[i].Name);
            }
            this._selectedViews = viewNames;
            //return viewNames;
        }
        else {
            this._selectedViews = "nowhere";
            //return "nowhere";
        }
        return this._selectedViews;
    },

    setSelectedViews: function (selectedViews) {
        this._selectedViews = selectedViews;
        if (selectedViews === "allViews") {
            jQuery(this._allScreensRadio).click();
        }
        else if (selectedViews === "nowhere") {
            jQuery(this._nowhereRadio).click();
        }
        else if (selectedViews && selectedViews.length) {
            //            var selectedKeys = [];
            //            viewCount = selectedViews.length;
            //            for (var i = 0; i < viewCount; i++) {
            //                selectedItems.push(selectedViews[i]);
            //            }
            this._itemsSelector.set_selectedKeys(selectedViews);
            jQuery(this._someScreensRadio).click();
        }
    },

    /* -------------------- Event handlers ---------------- */

    _load: function () {
        //        if (this._selectedViews == null || this._selectedViews.length == 0) {
        //            this._allScreensRadio.click();
        //        }
    },

    _radioClicked: function (sender, args) {
        switch (sender.target) {
            case this._allScreensRadio:
                jQuery("#selectViewsWrapper").hide();
                break;
            case this._someScreensRadio:
                jQuery("#selectViewsWrapper").show();
                if (!this._itemsSelectorBinded) {
                    this._itemsSelector.bindSelector();
                    this._itemsSelectorBinded = true;
                }
                break;
            case this._nowhereRadio:
                jQuery("#selectViewsWrapper").hide();
                break;
        }
        dialogBase.resizeToContent();
    },

    _itemsSelectorBound: function () {
        dialogBase.resizeToContent();
    },

    /* -------------------- properties ---------------- */

    get_itemsSelector: function () {
        return this._itemsSelector;
    },
    set_itemsSelector: function (value) {
        this._itemsSelector = value;
    },

    get_allScreensRadio: function () {
        return this._allScreensRadio;
    },
    set_allScreensRadio: function (value) {
        this._allScreensRadio = value;
    },

    get_someScreensRadio: function () {
        return this._someScreensRadio;
    },
    set_someScreensRadio: function (value) {
        this._someScreensRadio = value;
    },

    get_nowhereRadio: function () {
        return this._nowhereRadio;
    },
    set_nowhereRadio: function (value) {
        this._nowhereRadio = value;
    }
};

Telerik.Sitefinity.ModuleEditor.Web.UI.ViewsSelector.registerClass("Telerik.Sitefinity.ModuleEditor.Web.UI.ViewsSelector", Sys.UI.Control);

