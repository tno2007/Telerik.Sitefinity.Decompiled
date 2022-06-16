// called by the DetailFormView when it is loaded
function OnDetailViewLoaded(sender, args) {
    // the sender here is DetailFormView
    var currentForm = sender;
    Sys.Application.add_init(function () {
                                 $create(Telerik.Sitefinity.Modules.Forms.Web.UI.DetailFormViewExtension,
                                         { _detailFormView: currentForm },
                                         {},
                                         {},
                                         null);
                             });
}

Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI");

Telerik.Sitefinity.Modules.Forms.Web.UI.DetailFormViewExtension = function () {
    Telerik.Sitefinity.Modules.Forms.Web.UI.DetailFormViewExtension.initializeBase(this);
    // Main components
    this._detailFormView = {};
    this._binder = null;
    
    // Event delegates
    this._formClosingDelegate = null;
	this._detailFormViewDataBindDelegate = null;
    this._formCreatedDelegate = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.DetailFormViewExtension.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.DetailFormViewExtension.callBaseMethod(this, "initialize");
        this._binder = this._detailFormView.get_binder();

        this._formClosingDelegate = Function.createDelegate(this, this._formClosingHandler);
        this._detailFormView.add_formClosing(this._formClosingDelegate);

		if (this._detailFormView) {
            this._formCreatedDelegate = Function.createDelegate(this, this._formCreatedHandler);
            this._detailFormViewDataBindDelegate = Function.createDelegate(this, this._detailFormViewDataBind);
            this._detailFormView.add_onDataBind(this._detailFormViewDataBindDelegate);
            this._detailFormView.add_formCreated(this._formCreatedDelegate);
        }
    },

    dispose: function () {
        this._detailFormView.remove_formClosing(this._formClosingDelegate);
        delete this._formClosingDelegate;

		if (this._detailFormView && this.detailFormViewDataBindDelegate) {
            this._detailFormView.remove_onDataBind(this._detailFormViewDataBindDelegate);
            delete this._detailFormViewDataBindDelegate;
        }

        if (this._formCreatedDelegate) {
            if (this._detailFormView) {
                this._detailFormView.remove_formCreated(this._formCreatedDelegate);
            }
            delete this._formCreatedDelegate;
        }

        Telerik.Sitefinity.Modules.Forms.Web.UI.DetailFormViewExtension.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _formClosingHandler: function (sender, args) {
        var form = sender;
        var commandArgument = args.get_commandArgument();
        var isNew = args.get_isNew();
        if (form.get_isMultilingual() && commandArgument) {
            isNew = commandArgument.languageMode == "create";
        }
        if (isNew && args.get_isDirty()) {
            dialogBase.closeCreated(args.get_dataItem(), form);
            args.set_cancel(true);
        }
    },
    _detailFormViewDataBind: function (sender, dataItem)
    {
		if(dataItem && dataItem.Item)
		{
			var frameWork = dataItem.Item.Framework;
			if(frameWork == 0) // "Web forms only"
			{
				this._showHideMVCOnlyFrameworkFields(false);
			}
			else if(frameWork == 1) //MvcOnly
			{
				this._showHideMVCOnlyFrameworkFields(true);
			}
		}
        this._toggleDecTooltip();
    },
	_showHideMVCOnlyFrameworkFields: function (show)
	{
		if(show)
		{
			$(".MVCOnlyFrameworkFieldsCss").show();
		}
		else
		{
			$(".MVCOnlyFrameworkFieldsCss").hide();
		}
	},
    _formCreatedHandler: function (sender, args) {
        var dataItem = this._binder.get_dataItem();
        this._updateFrameworkOptions(dataItem.AvailableFrameworks);
    },
    _toggleSectionByName: function (name, hide) {
        var sectionIds = this._detailFormView.get_sectionIds();
        var sectionCount = sectionIds.length;

        for (var sectionIndex = 0; sectionIndex < sectionCount; sectionIndex++) {
            var sectionId = sectionIds[sectionIndex];
            var section = $find(sectionId);
            if (section.get_name() == name || section.get_fieldControlIds().some( function (el){ return el.indexOf(name) > -1; } )) {
                var sectionElm = jQuery(section.get_element());
                if (hide) {
                    sectionElm.hide();
                }
                else {
                    sectionElm.show();
                }
                break;
            }
        }
    },
    _updateFrameworkOptions: function(availability) {
        var mvcOption = jQuery("input[value='1']");
        var mvcOptionLabel = jQuery("input[value='1'] + label");
        var webFormsOption = jQuery("input[value='0']");
        var webFormsOptionLabel = jQuery("input[value='0'] + label");
        mvcOption.attr("checked", true);
        switch(availability) {
             case 2: //MVC only
                this._toggleSectionByName("FormFramework", true);
                break;
             case 1: //Hybrid and MVC
             case 0: // show all
                mvcOption.show();
                mvcOptionLabel.show();
                webFormsOption.show();
                webFormsOptionLabel.show();
                this._toggleSectionByName("FormFramework", false);
        }
	},
    _toggleDecTooltip: function() {
        var tooltipLocator = "#sendFormsDataToDecFieldTooltip";
        var popupLocator = ".sfDetailsPopup";
        var decTooltipPopupLocator = tooltipLocator + " ~ " + popupLocator;

        $(decTooltipPopupLocator).hide();

        $(document).on("click", tooltipLocator, function(event) {
            event.preventDefault();
            event.stopImmediatePropagation();
            jQuery(this).siblings(popupLocator).toggle();
        });

        $(document).on("click", decTooltipPopupLocator, function(event) {
            event.preventDefault();
            event.stopImmediatePropagation();
        });
    }
    /* -------------------- properties ---------------- */
}

Telerik.Sitefinity.Modules.Forms.Web.UI.DetailFormViewExtension.registerClass("Telerik.Sitefinity.Modules.Forms.Web.UI.DetailFormViewExtension", Sys.Component, Sys.IDisposable);
