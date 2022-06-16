/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Selectors");

Telerik.Sitefinity.Web.UI.Selectors.ExternalPagesSelector = function (element) {
    Telerik.Sitefinity.Web.UI.Selectors.ExternalPagesSelector.initializeBase(this, [element]);
    this._itemsBuilder = null;

    this._saveChangesButton = null;
    this._titleTextBox = null;
    this._urlTextBox = null;
    this._openInNewWindowChoiceField = null;
    this._noPagesSelectedLabel == null

    this._pageLoadDelegate = null;
    this._pageClickedDelegate = null;
    this._addButtonClickedDelegate = null;
    this._urlFocusDelegate = null;
    this._fieldKeyPressDelegate = null;

    this._currentlyEditedIndex = -1;
    this._elementsCache = new Object();
}

Telerik.Sitefinity.Web.UI.Selectors.ExternalPagesSelector.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Selectors.ExternalPagesSelector.callBaseMethod(this, 'initialize');

        this._pageLoadDelegate = Function.createDelegate(this, this._pageLoadHandler);
        Sys.Application.add_load(this._pageLoadDelegate);

        this._pageClickedDelegate = Function.createDelegate(this, this._pageClickedHandler);
        this.get_itemsBuilder().add_itemClicked(this._pageClickedDelegate);

        this._itemsChangedDelegate = Function.createDelegate(this, this._itemsChangedHandler);
        this.get_itemsBuilder().add_itemsChanged(this._itemsChangedDelegate);

        this._urlFocusDelegate = Function.createDelegate(this, this._urlFocusHandler);
        $addHandler(this._urlTextBox.get_textElement(), "click", this._urlFocusDelegate);

        this._fieldKeyPressDelegate = Function.createDelegate(this, this._fieldKeyPressHandler);
        jQuery(this.get_titleTextBox()).bind('keyup', this._fieldKeyPressDelegate);

        this._addButtonClickedDelegate = Function.createDelegate(this, this._addButtonHandler);
        $addHandler(this.get_saveChangesButton(), "click", this._addButtonClickedDelegate);
    },

    dispose: function () {
		if (this._urlFocusDelegate)
		{
			if (this._urlTextBox)
			{
				$removeHandler(this._urlTextBox.get_textElement(), "click", this._urlFocusDelegate);
			}
			delete this._urlFocusDelegate;
		}

        if (this._pageClickedDelegate) {
            delete this._pageClickedDelegate;
        }

        if (this._pageLoadDelegate) {
            delete this._pageLoadDelegate;
        }

		Telerik.Sitefinity.Web.UI.Selectors.ExternalPagesSelector.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    get_selectedItems: function () {
        return this.get_itemsBuilder().getSelectedPages();
    },

    set_selectedItems: function (items) {
        if (!items || items.length == 0) {
            this._showHideNoSelectionLabel(true);
        }
        this.get_itemsBuilder().set_choiceItems(items);
    },

    /* --------------------------------- event handlers --------------------------------- */

    // Handles the add other choice field change
    _addOtherChoiceFieldValueChangedHandler: function (sender) {
        if (this._addOtherChoiceField.get_value() === "true") {
            jQuery(this._otherTitleTextField.get_element()).show();
        }
        else {
            jQuery(this._otherTitleTextField.get_element()).hide();
        }
        dialogBase.resizeToContent();
    },

    // Handles the page load event
    _pageLoadHandler: function () {
        this._refreshEditSection();
    },

    //Handles the click event of the Save button
    _saveChangesHandler: function () {
        var titleTextBox = this.get_titleTextBox();
        var urlTextBox = this.get_urlTextBox();

        var newPageItem = { Title: titleTextBox.value, Url: urlTextBox.get_value() };

        this.get_itemsBuilder().updateItem(this._currentlyEditedIndex, newPageItem);
    },

    //Handles the click event of the Add button
    _addButtonHandler: function () {
        var validInput = this._isInputValid() && this.get_urlTextBox().validate();
        if (validInput == true) {
            var titleTextBox = this.get_titleTextBox();
            var urlTextBox = this.get_urlTextBox();
            var newPageItem = { Title: titleTextBox.value, Url: urlTextBox.get_value(), Id: null };
            this.get_itemsBuilder().addNewPageLast(newPageItem);

            titleTextBox.value = "";
            urlTextBox.set_value(urlTextBox.defaultValue);
        } else {

        }
    },

    //handles the user click on a page
    _pageClickedHandler: function (sender, args) {
        var index = args.Index;
        var page = args.Item;

        this._currentlyEditedIndex = index;

        var titleTextBox = this.get_titleTextBox();
        titleTextBox.value = page.Title;

        var urlTextBox = this.get_urlTextBox();
        urlTextBox.set_value(page.Url);
    },

    _itemsChangedHandler: function (sender, args) {
        this._showHideNoSelectionLabel(args.ItemsCount == 0);
    },

    _urlFocusHandler: function (e) {
    	//jQuery(e.target).select();        
    },

    _fieldKeyPressHandler: function () {
        this._refreshEditSection();
    },

    /* --------------------------------- private methods --------------------------------- */

    _isInputValid: function () {
        var titleTextbox = this.get_titleTextBox();
        var urlTextbox = this.get_urlTextBox();
        if (jQuery.trim(titleTextbox.value).length > 0 && jQuery.trim(urlTextbox.get_value()).length > 0) {
            return true;
        } else {
            return false;
        }
    },
    _refreshEditSection: function () {
        var validInput = this._isInputValid();
        if (validInput == true) {
            this._enableEditSection();
        } else {
            this._disableEditSection();
        }
    },
    _disableEditSection: function () {
        this.get_saveChangesButton().disabled = true;
    },
    _enableEditSection: function () {
        this.get_saveChangesButton().disabled = false;
    },

    _findElement: function (id) {
        if (typeof (this._elementsCache[id]) !== 'undefined')
            return this._elementsCache[id];
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        this._elementsCache[id] = result;
        return result;
    },
    _showHideNoSelectionLabel: function (val) {
        if (val) {
            jQuery(this.get_noPagesSelectedLabel()).show();
        }
        else {
            jQuery(this.get_noPagesSelectedLabel()).hide();
        }
    },

    /* --------------------------------- properties --------------------------------- */

    //Gets the save changes button
    get_saveChangesButton: function () {
        if (this._saveChangesButton == null) {
            this._saveChangesButton = this._findElement("addButton");
        }
        return this._saveChangesButton;
    },

    //Gets the title text box
    get_titleTextBox: function () {
        if (this._titleTextBox == null) {
            this._titleTextBox = this._findElement("titleTextBox");
        }
        return this._titleTextBox;
    },

    //Gets the URL text box
    get_urlTextBox: function () {
        return this._urlTextBox;
    },
	set_urlTextBox: function (value) {
		this._urlTextBox = value;
	},

	get_openInNewWindowChoiceField: function () {
		return this._openInNewWindowChoiceField;
	},
	set_openInNewWindowChoiceField: function (value) {
		this._openInNewWindowChoiceField = value;
	},

    //gets the selected page label
    get_noPagesSelectedLabel: function () {
        if (this._noPagesSelectedLabel == null) {
            this._noPagesSelectedLabel = this._findElement("noPagesSelectedLabel");
        }
        return this._noPagesSelectedLabel;
    },

    // Gets the textfield for the instructions text
    get_itemsBuilder: function () { return this._itemsBuilder; },
    // Sets the textfield for the instructions text
    set_itemsBuilder: function (value) { this._itemsBuilder = value; }
}

Telerik.Sitefinity.Web.UI.Selectors.ExternalPagesSelector.registerClass('Telerik.Sitefinity.Web.UI.Selectors.ExternalPagesSelector', Sys.UI.Control);