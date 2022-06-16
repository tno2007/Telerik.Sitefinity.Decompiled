Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingField.initializeBase(this, [element]);

	this._selectorWindow = null;
    this._selector = null;
	this._commandBar = null;
	this._openSelectorLink = null;
	this._mappedFieldsInfo = null;
	this._resources = null;

	this._selectorWindowDialog = null;
	this._commandBarCommandDelegate = null;
    this._openSelectorLinkCommandDelegate = null;
    this._selectorDialogZIndex = 10501;
	this._defaultValue = '';
}
Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingField.prototype =
{
    /* -------------------- set up and tear down ----------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingField.callBaseMethod(this, "initialize");
        
		this._value = this._defaultValue;

        if (this._commandBarCommandDelegate == null)
            this._commandBarCommandDelegate = Function.createDelegate(this, this.commandBar_Command);
    
        if (this._commandBar != null)
            this._commandBar.add_command(this._commandBarCommandDelegate);
		
        if (this._openSelectorLink) {
            if (this._openSelectorLinkCommandDelegate == null)
                this._openSelectorLinkCommandDelegate = Function.createDelegate(this, this.openSelectorCommand);
            $addHandler(this._openSelectorLink, "click", this._openSelectorLinkCommandDelegate);
        }
	    this._initSelectorDialog();
		
        // Prevent memory leaks
	    $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },
	dispose: function () {
        if (this._commandBarCommandDelegate != null) {
            this._commandBar.remove_command(this._commandBarCommandDelegate);
            delete this._commandBarCommandDelegate;
        }
        if (this._openSelectorLinkCommandDelegate != null)
            delete this._openSelectorLinkCommandDelegate;

        if (this._openSelectorLink)
            $clearHandlers(this._openSelectorLink);

        this._commandBar = null;
		
        Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingField.callBaseMethod(this, "dispose");
	},

    /* --------- Public Methods ------------ */

    reset: function () {
        this._value = this._defaultValue;
		if (this.get_selector()) {
			this.get_selector().reset();
		}
		this._resetMappingFieldsInfo();
        Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingField.callBaseMethod(this, "reset");
    },

    /* --------- Event Handlers ------------ */
    commandBar_Command: function (sender, args) {
        switch (args.get_commandName()) {
            case "save":
                if (this._selector.validate()) {
                    this._saveChanges();
                    this._selector.clearSelection();
                    this._close();
                }
                break;
            case "cancel":
                this._selector.clearSelection();
                this._close();
                break;
        }
    },
    openSelectorCommand: function (e) {
		this._selectorWindowDialog.dialog("open");

		this.get_selector().loadMappings(this._value);
        this._selectorWindowDialog.dialog( "option", "position", { my: "center", of: window } );

		jQuery(this._selectorWindowDialog).parent().css({ "top": this._dialogScrollTop() });

    },
    /* ------------ Private Methods -------------- */
    _saveChanges: function () {
        var fieldsJson = this._selector.get_value();
        if (fieldsJson) {
            this.set_value(fieldsJson);
        }
        else {
            this.set_value('');
        } 
    },
    _initSelectorDialog: function () {
        var that = this;
		this._selectorWindowDialog = jQuery(this._selectorWindow).dialog({
			autoOpen: false,
			modal: true,
			width: "auto",
			height: "auto",
			closeOnEscape: true,
			resizable: false,
			draggable: false,
			classes: {
			    "ui-dialog": "sfSelectorDialog sfZIndexXL"
			}
		}); 
	},
    /* calculates top position of selector dialog */
    _dialogScrollTop: function () {
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        return scrollTop;
    },

    _close: function (context) {
        this._selectorWindowDialog.dialog("close");
    },

	_setDataMappingsInfo: function() {
		 if (this._value && this._selector) {	
		     var mappingsCount = this._selector._getDataMappingsCount();
		     if (mappingsCount > 0) {
		         var label = this._resources.FieldsMapped;
		         if (mappingsCount === 1) {
		             label = this._resources.FieldMapped;
		         }
		         this._mappedFieldsInfo.innerHTML = mappingsCount + ' ' + label;
		         jQuery(this._mappedFieldsInfo).show();

		         jQuery(this._openSelectorLink).text(this._resources.EditMapping);
		     }
		     else {
		         this._resetMappingFieldsInfo();
		     }
		}
	},
	
	_resetMappingFieldsInfo: function() {
		jQuery(this._mappedFieldsInfo).innerHTML = '';
		jQuery(this._mappedFieldsInfo).hide();

	    jQuery(this._openSelectorLink).text(this._resources.CreateMapping);
	},
	/* ------------ Properties -------------- */
    // Gets the value of the field control.
    get_value: function () {
        return this._value;
    },
    // Sets the value of the field control.
    set_value: function (value) {
        this._value = value;
		if (this.get_selector()) {
			this.get_selector().set_value(this._value);
		}
		this._setDataMappingsInfo();
    },
	set_dataContext: function (value) {
       this.get_selector().set_dataContext(value);
	},
    get_commandBar: function () {
        return this._commandBar;
    },
    set_commandBar: function (value) {
        this._commandBar = value;
    },
    get_openSelectorLink: function () {
        return this._openSelectorLink;
    },
    set_openSelectorLink: function (value) {
        this._openSelectorLink = value;
    },
    get_selectorWindow: function () {
        return this._selectorWindow;
    },
    set_selectorWindow: function (value) {
        this._selectorWindow = value;
    },
    get_selector: function () {
        return this._selector;
    },
    set_selector: function (value) {
        this._selector = value;
    },
	get_mappedFieldsInfo: function () {
        return this._mappedFieldsInfo;
    },
    set_mappedFieldsInfo: function (value) {
        this._mappedFieldsInfo = value;
    },
    get_resources: function () {
        return this._resources;
    },
    set_resources: function (value) {
        this._resources = value;
    },
    get_selectorWindowDialog: function () {
        return this._selectorWindowDialog;
    },
	get_selectorDialogZIndex: function () {
        return this._selectorDialogZIndex;
    },
    set_selectorDialogZIndex: function (value) {
        this._selectorDialogZIndex = value;
    }
};
Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);
