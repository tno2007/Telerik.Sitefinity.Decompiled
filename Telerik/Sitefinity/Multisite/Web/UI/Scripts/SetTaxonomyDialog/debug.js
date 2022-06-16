﻿Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.UI");

Telerik.Sitefinity.Multisite.Web.UI.SetTaxonomyDialog = function (element) {
    Telerik.Sitefinity.Multisite.Web.UI.SetTaxonomyDialog.initializeBase(this, [element]);

    this._buttonDone = null;
    this._buttonDoneClickDelegate = null;
    this._buttonCancel = null;
    this._buttonCancelClickDelegate = null;
    this._otherSitesRadio = null;
    this._otherSitesRadioClickDelegate = null;
    this._spanOtherSites = null;
    this._buttonChangeSite = null;
    this._buttonChangeSiteClickDelegate = null;
    this._currentSiteRadio = null;
    this._currentSiteRadioClickDelegate = null;
    this._duplicateCheckbox = null;
    this._otherSitesPanel = null;

    this._sitesGrid = null;
    this._sitesGridDataBoundDelegate = null;
    this._shareViewData = null;
    this._buttonDoneShareView = null;
    this._buttonDoneShareViewClickDelegate = null;
    this._buttonCancelShareView = null;
    this._buttonCancelShareViewClickDelegate = null;

    this._secondaryButtonDone = null;
    this._secondaryButtonDoneClickDelegate = null;
    this._secondaryButtonCancel = null;
    this._secondaryButtonCancelClickDelegate = null;
    this._warningMessagePanel = null;

    this._taxonomyServiceUrl = null;
    this._taxonomyId = null;
    this._currentSiteTaxonomyId = null;
    this._taxonomyProviderName = null;
    this._taxonomyIsShared = false;
    this._currentSiteId = null;

    this._loadingCounter = 0;
    this._loadingView = null;
    this._messageControl = null;

    this._ajaxCompleteDelegate = null;
    this._ajaxFailDelegate = null;
}

Telerik.Sitefinity.Multisite.Web.UI.SetTaxonomyDialog.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Multisite.Web.UI.SetTaxonomyDialog.callBaseMethod(this, "initialize");

        this.duplicateCheckboxSelector = jQuery(this.get_duplicateCheckbox());

        this._initDelegates();
        this._addHandlers();


        if (this.get_taxonomyIsShared()) {

            jQuery(this.get_otherSitesRadio()).attr('checked', true);
            this.duplicateCheckboxSelector.prop('disabled', true);
            
        } else {
            jQuery(this.get_currentSiteRadio()).attr('checked', true);
            jQuery(this.get_otherSitesPanel()).hide();
        }

        this._loadSites();

        dialogBase.resizeToContent();
    },

    dispose: function () {
        this._removeHandlers();
        Telerik.Sitefinity.Multisite.Web.UI.SetTaxonomyDialog.callBaseMethod(this, "dispose");
    },

    /* Methods */

    _initDelegates: function () {
        this._buttonDoneClickDelegate = Function.createDelegate(this, this._buttonDoneClickHandler);
        this._buttonCancelClickDelegate = Function.createDelegate(this, this._buttonCancelClickHandler);
        this._buttonChangeSiteClickDelegate = Function.createDelegate(this, this._buttonChangeSiteClickHandler);
        this._otherSitesRadioClickDelegate = Function.createDelegate(this, this._otherSitesRadioClickHandler);
        this._currentSiteRadioClickDelegate = Function.createDelegate(this, this._currentSiteRadioClickHandler);

        this._sitesGridDataBoundDelegate = Function.createDelegate(this, this._sitesGridDataBoundHandler);
        this._buttonDoneShareViewClickDelegate = Function.createDelegate(this, this._buttonDoneShareViewClickHandler);
        this._buttonCancelShareViewClickDelegate = Function.createDelegate(this, this._buttonCancelShareViewClickHandler);

        this._secondaryButtonDoneClickDelegate = Function.createDelegate(this, this._secondaryButtonDoneHandler);
        this._secondaryButtonCancelClickDelegate = Function.createDelegate(this, this._secondaryButtonCancelHandler);

        this._ajaxCompleteDelegate = Function.createDelegate(this, this._ajaxCompleteHandler);
        this._ajaxFailDelegate = Function.createDelegate(this, this._ajaxFailHandler);
    },

    _addHandlers: function () {
        $addHandler(this.get_buttonDone(), "click", this._buttonDoneClickDelegate);
        $addHandler(this.get_buttonCancel(), "click", this._buttonCancelClickDelegate);
        $addHandler(this.get_buttonChangeSite(), "click", this._buttonChangeSiteClickDelegate);
        $addHandler(this.get_otherSitesRadio(), "click", this._otherSitesRadioClickDelegate);
        $addHandler(this.get_currentSiteRadio(), "click", this._currentSiteRadioClickDelegate);

        $addHandler(this.get_buttonDoneShareView(), "click", this._buttonDoneShareViewClickDelegate);
        $addHandler(this.get_buttonCancelShareView(), "click", this._buttonCancelShareViewClickDelegate);

        $addHandler(this.get_secondaryButtonDone(), 'click', this._secondaryButtonDoneClickDelegate);
        $addHandler(this.get_secondaryButtonCancel(), 'click', this._secondaryButtonCancelClickDelegate);
    },

    _removeHandlers: function () {

        if (this._buttonDoneClickDelegate) {
            if (this.get_buttonDone()) {
                $removeHandler(this.get_buttonDone(), "click", this._buttonDoneClickDelegate);
            }
            delete this._buttonDoneClickDelegate;
        }
        if (this._buttonCancelClickDelegate) {
            if (this.get_buttonCancel()) {
                $removeHandler(this.get_buttonCancel(), "click", this._buttonCancelClickDelegate);
            }
            delete this._buttonCancelClickDelegate;
        }
        if (this._buttonChangeSiteClickDelegate) {
            if (this.get_buttonChangeSite()) {
                $removeHandler(this.get_buttonChangeSite(), "click", this._buttonChangeSiteClickDelegate);
            }
            delete this._buttonChangeSiteClickDelegate;
        }
        if (this._otherSitesRadioClickDelegate) {
            if (this.get_otherSitesRadio()) {
                $removeHandler(this.get_otherSitesRadio(), "click", this._otherSitesRadioClickDelegate);
            }
            delete this._otherSitesRadioClickDelegate;
        }
        if (this._currentSiteRadioClickDelegate) {
            if (this.get_currentSiteRadio()) {
                $removeHandler(this.get_currentSiteRadio(), "click", this._currentSiteRadioClickDelegate);
            }
            delete this._currentSiteRadioClickDelegate;
        }

        if (this._sitesGridDataBoundDelegate) {
            delete this._sitesGridDataBoundDelegate;
        }
        if (this._buttonDoneShareViewClickDelegate) {
            if (this.get_buttonDoneShareView()) {
                $removeHandler(this.get_buttonDoneShareView(), 'click', this._buttonDoneShareViewClickDelegate);
            }
            delete this._buttonDoneShareViewClickDelegate;
        }
        if (this._buttonCancelShareViewClickDelegate) {
            if (this.get_buttonCancelShareView()) {
                $removeHandler(this.get_buttonCancelShareView(), 'click', this._buttonCancelShareViewClickDelegate);
            }
            delete this._buttonCancelShareViewClickDelegate;
        }

        if (this._secondaryButtonDoneClickDelegate) {
            if (this.get_secondaryButtonDone()) {
                $removeHandler(this.get_secondaryButtonDone(), 'click', this._secondaryButtonDoneClickDelegate);
            }
            delete this._secondaryButtonDoneClickDelegate;
        }
        if (this._secondaryButtonCancelClickDelegate) {
            if (this.get_secondaryButtonCancel()) {
                $removeHandler(this.get_secondaryButtonCancel(), 'click', this._secondaryButtonCancelClickDelegate);
            }
            delete this._secondaryButtonCancelClickDelegate;
        }

        if (this._ajaxCompleteDelegate) {
            delete this._ajaxCompleteDelegate;
        }
        if (this._ajaxFailDelegate) {
            delete this._ajaxFailDelegate;
        }
    },

    _loadSites: function () {
        var that = this;
        var getSitesSuccess = function (data, textStatus, jqXHR) {
            that._shareViewData = data;

            if (that.get_taxonomyIsShared()) {
                for (var i = 0; i < that._shareViewData.length; i++) {
                    if (that._shareViewData[i].Key === that.get_currentSiteTaxonomyId()) {
                        jQuery(that.get_spanOtherSites()).text(that._shareViewData[i].Value);
                        break;
                    }
                }
            } else {
                // Hide the span with selected sites if taxonomy is not shared
                jQuery(that.get_spanOtherSites()).hide();

                for (var j = 0; j < that._shareViewData.length; j++) {
                    if (that._shareViewData[j].Key === that.get_currentSiteTaxonomyId()) {
                        that._shareViewData.splice(j, 1);
                        break;
                    }
                }
            }
            that._initializeGrid();
        };

        this._setLoadingViewVisible(true);

        var sitesUrl = String.format("{0}/{1}/sites/?provider={2}",
               this.get_taxonomyServiceUrl(), this.get_taxonomyId(), this.get_taxonomyProviderName());

        jQuery.ajax({
            type: 'GET',
            url: sitesUrl,
            contentType: "application/json",
            processData: false,
            success: getSitesSuccess,
            error: this._ajaxFailDelegate,
            complete: this._ajaxCompleteDelegate
        });
    },

    _initializeGrid: function () {
        var dataSource = new kendo.data.DataSource({
            data: this._shareViewData
        });

        jQuery(this.get_sitesGrid()).kendoGrid({
            dataSource: dataSource,
            rowTemplate: jQuery.proxy(kendo.template(jQuery("#sitesGridRowTemplate").html()), this),
            autoBind: true,
            dataBound: this._sitesGridDataBoundDelegate,
            scrollable: false
        });

        this.resizeToContent();
    },

    _siteRadioId: function (id) {
        return "siteRadio_" + id;
    },

    _selectedShareTaxonomyId: function () {
        return jQuery('input[name="sitesGridRadio"]:checked').val();
    },

    /* /Methods */

    /* Event Handlers */

    _buttonDoneClickHandler: function (sender, args) {
        if ((this.get_taxonomyIsShared() === false && jQuery(this.get_currentSiteRadio()).is(':checked')) ||
            (this.get_taxonomyIsShared() === true && jQuery(this.get_otherSitesRadio()).is(':checked') && this._selectedShareTaxonomyId() === this.get_currentSiteTaxonomyId()) || 
            (jQuery(this.get_otherSitesRadio()).is(':checked') && this._selectedShareTaxonomyId() === undefined)) {
            dialogBase.close();
        } else {
            jQuery("#mainView").hide();
            jQuery("#secondaryView").show();

            // Warning message appears only, if Duplicate option IS NOT checked
            //if (!(this.duplicateCheckboxSelector.is(':checked'))) {
            //    jQuery(this.get_warningMessagePanel()).show();
            //} else {
            //    jQuery(this.get_warningMessagePanel()).hide();
            //}

            dialogBase.resizeToContent();
        }
    },

    _buttonCancelClickHandler: function (sender, args) {
        dialogBase.close();
    },

    _buttonChangeSiteClickHandler: function (sender, args) {
        jQuery("#mainView").hide();
        jQuery("#shareView").show();
        dialogBase.resizeToContent();
    },

    _otherSitesRadioClickHandler: function (sender, args) {
        jQuery(this.get_otherSitesPanel()).show();
        this.duplicateCheckboxSelector.prop('disabled', true);
        this.duplicateCheckboxSelector.prop("checked", false);

        dialogBase.resizeToContent();
    },

    _currentSiteRadioClickHandler: function (sender, args) {
        jQuery(this.get_otherSitesPanel()).hide();
        this.duplicateCheckboxSelector.prop('disabled', false);

        dialogBase.resizeToContent();
    },

    _sitesGridDataBoundHandler: function (sender, args) {
        if (this._shareViewData) {
            for (var i = 0; i < this._shareViewData.length; i++) {
                if (this._shareViewData[i].Key === this.get_currentSiteTaxonomyId()) {
                    jQuery("#" + this._siteRadioId(this._shareViewData[i].Key)).prop('checked', 'checked');
                    break;
                }
            }
        }
    },

    _buttonDoneShareViewClickHandler: function (sender, args) {
        jQuery("#mainView").show();
        jQuery("#shareView").hide();

        var selectedId = this._selectedShareTaxonomyId();
        for (var i = 0; i < this._shareViewData.length; i++) {
            if (this._shareViewData[i].Key === selectedId) {
                jQuery(this.get_spanOtherSites()).text(this._shareViewData[i].Value);
                jQuery(this.get_spanOtherSites()).show();
                break;
            }
        }

        dialogBase.resizeToContent();
    },

    _buttonCancelShareViewClickHandler: function (sender, args) {
        jQuery("#mainView").show();
        jQuery("#shareView").hide();
        dialogBase.resizeToContent();
    },

    _secondaryButtonDoneHandler: function (sender, args) {
        var doneSuccess = function (data, textStatus, jqXHR) {
            dialogBase.closeUpdated();
        };

        this._setLoadingViewVisible(true);
        jQuery("#secondaryView").hide();

        if (jQuery(this.get_otherSitesRadio()).is(':checked')) {
            var shareUrl = String.format("{0}/{1}/share/?provider={2}",
                this.get_taxonomyServiceUrl(), this._selectedShareTaxonomyId(), this.get_taxonomyProviderName());

            jQuery.ajax({
                type: 'PUT',
                url: shareUrl,
                contentType: 'application/json',
                processData: false,
                success: doneSuccess,
                error: this._ajaxFailDelegate,
                complete: this._ajaxCompleteDelegate
            });
        } else {
            var splitUrl = String.format("{0}/{1}/split/?provider={2}&duplicate={3}",
                this.get_taxonomyServiceUrl(), this.get_taxonomyId(), this.get_taxonomyProviderName(), this.duplicateCheckboxSelector.is(':checked'));

            jQuery.ajax({
                type: 'PUT',
                url: splitUrl,
                contentType: 'application/json',
                processData: false,
                success: doneSuccess,
                error: this._ajaxFailDelegate,
                complete: this._ajaxCompleteDelegate
            });
        }
    },

    _secondaryButtonCancelHandler: function (sender, args) {
        jQuery("#mainView").show();
        jQuery("#secondaryView").hide();
        dialogBase.resizeToContent();
    },

    _setLoadingViewVisible: function (loading) {
        if (loading) {
            this._loadingCounter++;
        }
        else {
            if (this._loadingCounter > 0) {
                this._loadingCounter--;
            }
        }
        if (this._loadingCounter > 0) {
            jQuery(this.get_loadingView()).show();
        }
        else {
            jQuery(this.get_loadingView()).hide();
        }
        dialogBase.resizeToContent();
    },

    _ajaxCompleteHandler: function (jqXHR, textStatus) {
        this._setLoadingViewVisible(false);
        dialogBase.resizeToContent();
    },

    _ajaxFailHandler: function (jqXHR, textStatus, errorThrown) {
        jQuery("#mainView").show();
        jQuery("#shareView").hide();
        jQuery("#secondaryView").hide();
        this.get_messageControl().showNegativeMessage(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
        dialogBase.resizeToContent();
    },

    /* /Event Handlers */

    /* Properties */

    get_buttonDone: function () {
        return this._buttonDone;
    },
    set_buttonDone: function (value) {
        this._buttonDone = value;
    },

    get_buttonCancel: function () {
        return this._buttonCancel;
    },
    set_buttonCancel: function (value) {
        this._buttonCancel = value;
    },

    get_otherSitesRadio: function () {
        return this._otherSitesRadio;
    },
    set_otherSitesRadio: function (value) {
        this._otherSitesRadio = value;
    },

    get_buttonChangeSite: function () {
        return this._buttonChangeSite;
    },
    set_buttonChangeSite: function (value) {
        this._buttonChangeSite = value;
    },

    get_spanOtherSites: function () {
        return this._spanOtherSites;
    },
    set_spanOtherSites: function (value) {
        this._spanOtherSites = value;
    },

    get_otherSitesPanel: function () {
        return this._otherSitesPanel;
    },
    set_otherSitesPanel: function (value) {
        this._otherSitesPanel = value;
    },

    get_currentSiteRadio: function () {
        return this._currentSiteRadio;
    },
    set_currentSiteRadio: function (value) {
        this._currentSiteRadio = value;
    },

    get_duplicateCheckbox: function () {
        return this._duplicateCheckbox;
    },
    set_duplicateCheckbox: function (value) {
        this._duplicateCheckbox = value;
    },

    get_sitesGrid: function () {
        return this._sitesGrid;
    },
    set_sitesGrid: function (value) {
        this._sitesGrid = value;
    },

    get_buttonDoneShareView: function () {
        return this._buttonDoneShareView;
    },
    set_buttonDoneShareView: function (value) {
        this._buttonDoneShareView = value;
    },

    get_buttonCancelShareView: function () {
        return this._buttonCancelShareView;
    },
    set_buttonCancelShareView: function (value) {
        this._buttonCancelShareView = value;
    },

    get_secondaryButtonDone: function () {
        return this._secondaryButtonDone;
    },
    set_secondaryButtonDone: function (value) {
        this._secondaryButtonDone = value;
    },

    get_secondaryButtonCancel: function () {
        return this._secondaryButtonCancel;
    },
    set_secondaryButtonCancel: function (value) {
        this._secondaryButtonCancel = value;
    },

    get_warningMessagePanel: function () {
        return this._warningMessagePanel;
    },
    set_warningMessagePanel: function (value) {
        this._warningMessagePanel = value;
    },

    get_loadingView: function () {
        return this._loadingView;
    },
    set_loadingView: function (value) {
        this._loadingView = value;
    },

    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    },

    get_taxonomyServiceUrl: function () {
        return this._taxonomyServiceUrl;
    },
    set_taxonomyServiceUrl: function (value) {
        this._taxonomyServiceUrl = value;
    },

    get_taxonomyId: function () {
        return this._taxonomyId;
    },
    set_taxonomyId: function (value) {
        this._taxonomyId = value;
    },

    get_currentSiteTaxonomyId: function () {
        return this._currentSiteTaxonomyId;
    },
    set_currentSiteTaxonomyId: function (value) {
        this._currentSiteTaxonomyId = value;
    },

    get_taxonomyProviderName: function () {
        return this._taxonomyProviderName;
    },
    set_taxonomyProviderName: function (value) {
        this._taxonomyProviderName = value;
    },

    get_taxonomyIsShared: function () {
        return this._taxonomyIsShared;
    },
    set_taxonomyIsShared: function (value) {
        this._taxonomyIsShared = value;
    },

    get_currentSiteId: function () {
        return this._currentSiteId;
    },
    set_currentSiteId: function (value) {
        this._currentSiteId = value;
    }

    /* /Properties */
}

Telerik.Sitefinity.Multisite.Web.UI.SetTaxonomyDialog.registerClass("Telerik.Sitefinity.Multisite.Web.UI.SetTaxonomyDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();


