Type.registerNamespace("Telerik.Sitefinity.Localization.Web.UI");

Telerik.Sitefinity.Localization.Web.UI.LanguageListControl = function (element) {
    this._wrapper = null;
    this._titleLabelElement = null;

    //An object which has property with the language code of each available language and the Id of the HTML element for that language as value.
    this._languageElementIds = null;

    //An associative array used to store(cache) the HTML elements of all language elements using language code as key.
    this._languageElements = new Array();

    //An associative array used to store the IDs aof HTML elements and the language they are related to.
    this._languageCodes = new Array();

    //Contains the codes for the languages that are in use
    this._languagesInUse = null;

    this._addClickDelegate = null;
    this._editClickDelegate = null;
    this._handlePageLoadDelegate = null;

    this._createCommandName = null;
    this._editCommandName = null;

    Telerik.Sitefinity.Localization.Web.UI.LanguageListControl.initializeBase(this, [element]);
}
Telerik.Sitefinity.Localization.Web.UI.LanguageListControl.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Localization.Web.UI.LanguageListControl.callBaseMethod(this, "initialize");

        if (this._languageElementIds) {
            this._languageElementIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._languageElementIds);
        }
        if (this._languagesInUse) {
            this._languagesInUse = Sys.Serialization.JavaScriptSerializer.deserialize(this._languagesInUse);
        }

        this._handlePageLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        Sys.Application.add_load(this._handlePageLoadDelegate);

        this._addClickDelegate = Function.createDelegate(this, this._addButtonClicked);
        this._editClickDelegate = Function.createDelegate(this, this._editButtonClicked);

        for (var languageCode in this._languageElementIds) {
            var addButton = this.getAddButton(languageCode);
            $addHandler(addButton, "click", this._addClickDelegate);
            this._languageCodes[addButton.id] = languageCode;

            var editButton = this.getEditButton(languageCode);
            $addHandler(editButton, "click", this._editClickDelegate);
            this._languageCodes[editButton.id] = languageCode;
        }


    },

    dispose: function () {
        Telerik.Sitefinity.Localization.Web.UI.LanguageListControl.callBaseMethod(this, "dispose");

        if (this._handlePageLoadDelegate) {
            Sys.Application.remove_load(this._handlePageLoadDelegate);
            delete this._handlePageLoadDelegate;
        }
        if (this._editClickDelegate) {
            delete this._editClickDelegate;
        }
        if (this._addClickDelegate) {
            delete this._addClickDelegate;
        }
    },

    /* --------------------  public methods ----------- */

    //Sets the title
    setTitleLabel: function (newLabel) {
        this.get_titleLabelElement().innerHTML = newLabel;
    },

    //languageCode must be Two-Letter ISO language code
    getLanguageElement: function (languageCode) {
        var result = null;

        if (languageCode in this._languageElements == false) {
            if (languageCode in this._languageElementIds) {
                var elementId = this._languageElementIds[languageCode];
                result = jQuery('#' + elementId).get(0);
                this._languageElements[languageCode] = result;
            } else {
                result = null;
            }
        } else {
            result = this._languageElements[languageCode];
        }

        return result;
    },

    getLanguageElements: function () {
        var languageIdsCount = this._languageElementIds.length;
        while (languageIdsCount--) {
            var languageCode = this._languageElementIds[languageIdsCount];
            if (!languageCode in this._languageElements) {
                var elementId = this._languageElementIds[languageCode];
                result = jQuery('#' + elementId).get(0);
                this._languageElements[languageCode] = result;
            }
        }
        return this._languageElements;
    },

    setLanguageIsInUse: function (languageCode, isInUse) {
        var currentlyInUse = this.isLanguageInUse(languageCode);
        if (isInUse) {
            if (currentlyInUse == false) {
                this.get_languagesInUse().push(languageCode);
                this._updateLanguageButton(languageCode);
            }
        } else {
            if (currentlyInUse == true) {
                var newArray = jQuery.grep(this.get_languagesInUse(),
                    function (value) {
                        return value != languageCode;
                    }
                );
                this.set_languagesInUse(newArray);
                this._updateLanguageButton(languageCode);
            }
        }
    },

    isLanguageInUse: function (languageCode) {
        //TODO: optimize by making the 'languages in use' a dictionary.
        return jQuery.inArray(languageCode, this.get_languagesInUse()) > -1;
    },

    getAddButton: function (languageCode) {
        return this._getLanguageSubElement(languageCode, 'addButton');
    },

    getEditButton: function (languageCode) {
        return this._getLanguageSubElement(languageCode, 'editButton');
    },

    clearLanguagesInUse: function () {
        var currentlyUsed = this.get_languagesInUse();
        this.set_languagesInUse([]);
        var currentlyUsedLength = currentlyUsed.length;
        while (currentlyUsedLength--) {
            this._updateLanguageButton(currentlyUsed[currentlyUsedLength]);
        }
    },

    /* -------------------- private methods ----------- */

    _getLanguageSubElement: function (languageCode, elementId) {
        var result = null;
        var elm = this.getLanguageElement(languageCode);
        if (elm) {
            result = jQuery(elm).find('[id*=' + elementId + ']').get(0);
        }

        return result;
    },

    _getCommandArgs: function (commandType, languageCode) {
        var eventArgument = new Telerik.Sitefinity.CommandEventArgs(commandType, { language: languageCode });
        return eventArgument;
    },

    _updateLanguageButton: function (languageCode) {
        var currentlyInUse = this.isLanguageInUse(languageCode);

        var addButton = this.getAddButton(languageCode);
        var editButton = this.getEditButton(languageCode);

        if (currentlyInUse == true) {
            this._showHideButton(addButton, false);
            this._showHideButton(editButton, true);
            jQuery(editButton).parent().removeClass("sfNotTranslated");
        } else {
            this._showHideButton(editButton, false);
            this._showHideButton(addButton, true);
            jQuery(editButton).parent().addClass("sfNotTranslated");
        }
    },

    //val: TRUE means visible, FALSE - invisible
    _showHideButton: function (button, val) {
        if (val) {
            jQuery(button).show();
        } else {
            jQuery(button).hide();
        }
    },

    /* -------------------- events -------------------- */

    add_command: function (delegate) {
        /// <summary>Happens when a custom command is fired. Can be cancelled.</summary>
        this.get_events().addHandler('command', delegate);
    },

    remove_command: function (delegate) {
        /// <summary>Happens when a custom command was fired. Can be cancelled.</summary>
        this.get_events().removeHandler('command', delegate);
    },

    /* -------------------- event handlers ------------ */

    _handlePageLoad: function (sender, args) {

    },

    _addButtonClicked: function (e) {
        var elm = e.target;
        var id = elm.id;
        var languageCode = this._languageCodes[id];
        if (!languageCode) {
            languageCode = this._languageCodes[elm.parentNode.id];
        }
        var args = this._getCommandArgs(this._createCommandName, languageCode);
        this._fire_command(args);
    },
    _editButtonClicked: function (e) {
        var elm = e.target;
        var id = elm.id;

        var languageCode = this._languageCodes[id];
        if (!languageCode) {
            languageCode= this._languageCodes[elm.parentNode.id];
        }
        var args = this._getCommandArgs(this._editCommandName, languageCode);
        this._fire_command(args);
    },


    /* -------------------- event firing  ---------------- */

    _fire_command: function (args) {
        var handler = this.get_events().getHandler("command");
        if (handler) {
            handler(this, args);
        }
    },

    /* -------------------- properties ---------------- */

    get_titleLabelElement: function () {
        return this._titleLabelElement;
    },

    set_titleLabelElement: function (value) {
        this._titleLabelElement = value;
    },

    get_languagesInUse: function () {
        return this._languagesInUse;
    },

    set_languagesInUse: function (value) {
        this._languagesInUse = value;
    },

    get_languageElementIds: function () {
        return this._languageElementIds;
    },

    set_languageElementIds: function (value) {
        this._languageElementIds = value;
    },

    get_wrapper: function () {
        return this._wrapper;
    },

    set_wrapper: function (value) {
        this._wrapper = value;
    }
};

Telerik.Sitefinity.Localization.Web.UI.LanguageListControl.registerClass('Telerik.Sitefinity.Localization.Web.UI.LanguageListControl', Sys.UI.Control);