Type.registerNamespace("Telerik.Sitefinity.Web.UI");

// ------------------------------------------------------------------------
// PropertyEditor class
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.PropertyEditor = function (element) {
    this._controlId = null;
    this._implementsDesigner;
    this._designer = null;
    this._propertiesServiceUrl = null;
    this._originalControl = null;
    this._control = null;
    this._propertyBag = null;
    this._propertyGrid = null;
    this._dirtyPropertyBag = [];
    this._saveButton = null;
    this._saveConfirmationDialog = null;
    this._saveAllTranslationsButton = null;
    this._saveChangesAllTranslationsDelegate = null;
    this._cancelButton = null;
    this._okButton = null;
    this._advancedModeButton = null;
    this._simpleModeButton = null;
    this._modesMultiPageId = null;
    this._modesMultiPage = null;
    this._hideAdvancedMode = false;
    this._pageId = null;
    this._mediaType = null;
    this._uiCulture = null;
    this._unlicensedMode = false;
    this._checkLiveVersion = false;
    this._upgradePageVersion = false;
    this._forceReload = false;
    this._beforeDialogCloseDelegate = null;
    this._isOpenedByBrowseAndEdit = false;

    this.SaveModeDefault = 0;
    this.SaveModeAllTranslations = 1;
    this.SaveModeCurrentTranslationOnly = 2;
    this._titleElement = null;
    this._providersSelector = null;
    this._orLabel = null;

    this._saveChangesFailureDelegate = null;
    
    Telerik.Sitefinity.Web.UI.PropertyEditor.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.PropertyEditor.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {

        this._propertyBag = Sys.Serialization.JavaScriptSerializer.deserialize(this._propertyBag);
        this._buildControl();
        // keep the original properties of the control in a separate object so that we
        // can compare the changes when user saves the control and can update only the
        // changed properties
        this._originalControl = new Object();
        jQuery.extend(true, this._originalControl, this._control);

        if (this._advancedModeButton) {
            this._advancedModeButtonDelegate = Function.createDelegate(this, this._advancedModeButtonClicked);
            $addHandler(this._advancedModeButton, 'click', this._advancedModeButtonDelegate);
        }

        if (this._simpleModeButton) {
            this._simpleModeButtonDelegate = Function.createDelegate(this, this._simpleModeButtonClicked);
            $addHandler(this._simpleModeButton, 'click', this._simpleModeButtonDelegate);
        }

        if (this._saveButton) {
            this._saveChangesDelegate = Function.createDelegate(this, this._saveChanges);
            $addHandler(this._saveButton, 'click', this._saveChangesDelegate);
        }

        if (this.get_saveAllTranslationsButton()) {
            this._saveChangesAllTranslationsDelegate = Function.createDelegate(this, this._saveChangesAllTranslations);
            $addHandler(this.get_saveAllTranslationsButton(), 'click', this._saveChangesAllTranslationsDelegate);
        }

        if (this._cancelButton) {
            this._closeEditorDelegate = Function.createDelegate(this, this._closeEditor);
            $addHandler(this._cancelButton, 'click', this._closeEditorDelegate);
        }

        if (this._okButton) {
            $addHandler(this._okButton, 'click', this._closeEditorDelegate);
        }

        //if we have a designer closing of the dialog in advanced mode swithes to the designer view
        if (this._implementsDesigner) {
            this._beforeDialogCloseDelegate = Function.createDelegate(this, this._beforeDialogCloseHandler);
            this.get_radWindow().add_beforeClose(this._beforeDialogCloseDelegate);
        }
        this._pageLoadedDelegate = Function.createDelegate(this, this._pageLoaded);
        Sys.Application.add_load(this._pageLoadedDelegate);

        this._saveChangesFailureDelegate = Function.createDelegate(this, this._saveChangesFailure);

        Telerik.Sitefinity.Web.UI.PropertyEditor.callBaseMethod(this, 'initialize');
    },
    dispose: function () {

        if (this._advancedModeButtonDelegate) {
            delete this._advancedModeButtonDelegate;
        }

        if (this._simpleModeButtonDelegate) {
            delete this._simpleModeButtonDelegate;
        }

        if (this._saveChangesDelegate) {
            delete this._saveChangesDelegate;
        }

        if (this._saveChangesAllTranslationsDelegate) {
            delete this._saveChangesAllTranslationsDelegate;
        }

        if (this._closeEditorDelegate) {
            delete this._closeEditorDelegate;
        }

        if (this._closeEditorDelegate) {
            delete this._closeEditorDelegate;
        }

        if (this._pageLoadedDelegate) {
            delete this._pageLoadedDelegate;
        }

        if (this._propertyBag) {
            delete this._propertyBag;
        }

        if (this._dirtyPropertyBag) {
            delete this._dirtyPropertyBag;
        }

        if (this._implementsDesigner) {
            if (this._beforeDialogCloseDelegate) {
                if (this.get_radWindow()) {
                    this.get_radWindow().remove_beforeClose(this._beforeDialogCloseDelegate);
                }
                delete this._beforeDialogCloseDelegate;
            }
        }

        if (this._saveChangesFailureDelegate) {
            delete this._saveChangesFailureDelegate;
        }

        Telerik.Sitefinity.Web.UI.PropertyEditor.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */

    // changes the value of the property specified by the property path
    changePropertyValue: function (propertyPath, value) {

        var _propertyInfo = { 'WasDirty': false };
        var _property = this.findProperty(propertyPath, _propertyInfo);

        var _value = value;
        if (_value == null) {
            // set the default value
            switch (_property.ItemTypeName) {
                case 'System.Boolean':
                    _value = 'false';
                    break;
                case 'System.Int16':
                case 'System.Int32':
                case 'System.Int64':
                    _value = 0;
                    break;
            }
        }

        if (_property) {
            _property.NeedsEditor = 'false';
        }

        if (_propertyInfo.WasDirty) {
            // if property was already dirty, just update the value
            _property.PropertyValue = _value;
            _property.NeedsEditor = 'false';
        } else {
            // otherwise add it to the dirty bag
            _property.PropertyValue = _value;
            _property.NeedsEditor = 'false';
            this._dirtyPropertyBag.push(_property);
        }
    },

    // finds the property in the property bag through the property path
    findProperty: function (propertyPath, _propertyInfo) {
        var _propertyIndex = this._findPropertyIndex(propertyPath, this._dirtyPropertyBag);
        if (_propertyIndex != -1) {
            _propertyInfo.WasDirty = true;
            return this._dirtyPropertyBag[_propertyIndex];
        } else {
            _propertyIndex = this._findPropertyIndex(propertyPath, this._propertyBag);
            if (_propertyIndex != -1) {
                _propertyInfo.WasDirty = false;
                return this._propertyBag[_propertyIndex];
            } else {
                alert('Property with path "' + propertyPath + '" cannot be found.');
            }
        }
    },

    // This function allows other objects to subscribe to the beforeSaveChanges event which is fired 
    // when the property editor start the process of saving changes, the event can cancel the saving process
    add_beforeSaveChanges: function (delegate) {
        this.get_events().addHandler('beforeSaveChanges', delegate);
    },

    // This function allows other objects to unsubscribe  to the beforeSaveChanges event which is fired 
    // when the property editor start the process of saving changes, the event can cancel the saving process
    remove_beforeSaveChanges: function (delegate) {
        this.get_events().removeHandler('beforeSaveChanges', delegate);
    },

    // This function allows other objects to subscribe to the completeSaveChanges event which is fired 
    // when the property editor finish the process of saving changes. This event will be fired in both 
    // success or Failure cases.
    add_completeSaveChanges: function (delegate) {
        this.get_events().addHandler('completeSaveChanges', delegate);
    },

    // This function allows other objects to unsubscribe  to the completeSaveChanges event which is fired 
    // when the property editor finish the process of saving changes.
    remove_completeSaveChanges: function (delegate) {
        this.get_events().removeHandler('completeSaveChanges', delegate);
    },

    saveEditorChanges: function () {
        this._saveChanges();
    },

    showProvidersSelector: function () {
        if (this._providersSelector) {
            $('#' + this._providersSelector.get_id()).show();
        }
    },

    hideProvidersSelector: function () {
        if (this._providersSelector) {
            $('#' + this._providersSelector.get_id()).hide();
        }
    },


    /* ************************* events ************************* */


    /* ************************* event handlers ************************* */
    // called when page is loaded
    _pageLoaded: function () {
        this._modesMultiPage = $find(this._modesMultiPageId);

        if (!this._hideAdvancedMode) {
            this._showMode(this._implementsDesigner);
            if (this.get_saveAllTranslationsButton() != null && this._implementsDesigner) {
                jQuery("body").addClass("sfMultiLangWithViewsDlg");
            }
        }
        else {
            jQuery(this.get_advancedModeButton()).hide();
            jQuery(this.get_simpleModeButton()).hide();
            this._modesMultiPage.set_selectedIndex(0);
        }
        dialogBase.resizeToContent();
    },

    // saves the changes made to the control properties
    _saveChanges: function (evt, saveToAllTranslations) {
        if (this.get_saveAllTranslationsButton() != null && this.get_saveConfirmationDialog() != null && saveToAllTranslations) {
            var that = this;
            var currentWidth = $(this._element).css('width');
            this.get_saveConfirmationDialog().show_prompt(null, null, function (sender, args) {
                if (args.get_commandName() == "save") {
                    that._saveChangesInternal(evt, saveToAllTranslations);
                }
                $(that._element).css('width', currentWidth);
                if (dialogBase)
                    dialogBase.resizeToContent();
            });
            
            if (/px$/.test(currentWidth) && parseInt(currentWidth) < 500)
                $(this._element).css('width', "500px");
            
            if (dialogBase)
                dialogBase.resizeToContent();
            this.get_saveConfirmationDialog()._hideOverlay();
        }
        else {
            this._saveChangesInternal(evt, saveToAllTranslations);
        }
    },

    _saveChangesInternal: function (evt, saveToAllTranslations) {
        var eventArgs = new Sys.CancelEventArgs();
        this._raiseBeforeSaveChanges(eventArgs);
        if (eventArgs.get_cancel()) {
            return;
        }

        var saveMode = this.SaveModeDefault;
        if (this.get_saveAllTranslationsButton() != null) {
            saveMode = saveToAllTranslations ? this.SaveModeAllTranslations : this.SaveModeCurrentTranslationOnly;
        }

        //#351588/osmak/2010-04-12: if (this._designer && !this._isInAdvancedMode() {
        if (this._designer) {
            this._designer.applyChanges();

            this._examineControlChanges();
        }

        if (this._dirtyPropertyBag.length == 0 && saveMode != this.SaveModeAllTranslations) {
            if (this._forceReload) {
                this._forceReload = false;
                dialogBase.get_radWindow().close('reload');
            }
            else {
                dialogBase.close();
            }
        }
        else {
            this._invokePut(saveMode);
        }
    },

    _saveChangesAllTranslations: function (evt) {
        this._saveChanges(evt, true);
    },

    _invokePut: function (saveMode) {
        var manager = new Telerik.Sitefinity.Data.ClientManager();

        if (this._uiCulture) {
            manager.set_uiCulture(this._uiCulture);
        }

        var _url = this._propertiesServiceUrl;

        var _urlParams = [];
        _urlParams["pageId"] = this._pageId;
        _urlParams["mediaType"] = this._mediaType;
        _urlParams["checkLiveVersion"] = this._checkLiveVersion;
        _urlParams["upgradePageVersion"] = this._upgradePageVersion;
        _urlParams["propertyLocalization"] = saveMode;
        _urlParams["isOpenedByBrowseAndEdit"] = this._isOpenedByBrowseAndEdit + "";

        var _keys = [];

        manager.InvokePut(_url, _urlParams, _keys, this._dirtyPropertyBag, this._saveChangesSuccess, this._saveChangesFailureDelegate, this);
    },

    // function that is called upon successful saving of the properties
    _saveChangesSuccess: function (sender, result) {        
        sender._raiseCompleteSaveChanges(result);
        dialogBase.get_radWindow().close('reload');
    },

    // function that is called if properties were not save successfully
    _saveChangesFailure: function (sender, result) {     
        this._raiseCompleteSaveChanges(result);
        alert(sender.Detail)
    },

    // uses the global dialogBase variable to close the window
    _closeEditor: function () {
        dialogBase.close();
    },

    // handles the click event of the advanced mode button
    _advancedModeButtonClicked: function () {
        if (this._designer) {
            this._designer.applyChanges();
        }
        this._updatePropertyBag();
        this._showMode(false);
        dialogBase.resizeToContent();
    },

    // handles the click event of the simple mode button
    _simpleModeButtonClicked: function () {
        this._switchToSimpleMode();
    },

    //fired before closing of the dialog
    _beforeDialogCloseHandler: function (sender, args) {

        // in advanced mode close switches to simple mode
        if (this._isInAdvancedMode()) {
            args.set_cancel(true);
            this._switchToSimpleMode();
        }
    },

    /* ************************* private methods ************************* */
    _switchToSimpleMode: function () {
        this._buildControl();
        if (this._designer) {
            this._designer.refreshUI();
        }
        this._showMode(true);
        dialogBase.resizeToContent();
    },

    // builds the control object from the property bag
    _buildControl: function () {
        if (this._propertyBag == null) {
            alert('Control object cannot be built without the property bag.');
        }

        this._control = new Object();
        var bagCount = this._propertyBag.length;
        // iterate through the whole property bag in order to build the object graph
        for (bagIter = 0; bagIter < bagCount; bagIter++) {
            var wcfProperty = this._propertyBag[bagIter];
            // split the property path to get the property hierarchy
            var chunks = this._getPropertyChunks(wcfProperty.PropertyPath);
            this._addProperties(this._control, chunks, wcfProperty.PropertyValue, wcfProperty.ClientPropertyTypeName);
        }
    },

    // updates the property bag from the control object
    _updatePropertyBag: function () {
        var bagCount = this._propertyBag.length;
        // iterate through the whole property bag in order to build the object graph
        for (bagIter = 0; bagIter < bagCount; bagIter++) {
            var wcfProperty = this._propertyBag[bagIter];
            var chunks = this._getPropertyChunks(wcfProperty.PropertyPath).reverse();
            var chunksCount = chunks.length;
            var newValue = this._control[chunks[0]];
            for (var chIter = 1; chIter < chunksCount; chIter++) {
                if (newValue != null) {
                    newValue = newValue[chunks[chIter]];
                }
            }

            if (newValue != null && typeof (newValue) != 'object') {
                this._propertyBag[bagIter].PropertyValue = newValue.toString();
            }
        }
    },

    // recursively adds the properties to the control object
    _addProperties: function (parentObject, chunks, propertyValue, clientPropertyTypeName) {
        if (!(chunks && chunks.length > 0)) {
            return;
        }

        var chunk = chunks.pop();
        // add object to the parent object if it doesn't already exist
        if (!parentObject[chunk]) {
            parentObject[chunk] = new Object();
            if (chunks.length > 0) {
                parentObject = parentObject[chunk];
            }
        } else {
            // otherwise set the parent object and continue            
            parentObject = parentObject[chunk];
        }
        if (chunks.length > 0) {
            this._addProperties(parentObject, chunks, propertyValue, clientPropertyTypeName);
        } else {
            // Pavel: there are cases when the property is already set:
            // somebody can set an object as a value
            // in such cases we should set the value not to a new property but to the current one
            parentObject[chunk] = (propertyValue) ? this._castValue(propertyValue, clientPropertyTypeName) : null;
        }
    },

    _castValue: function (value, dotNetType) {
        switch (dotNetType) {
            case 'System.Boolean':
                return value.toString().toLowerCase() == 'true';
                break;
            case 'System.Int32':
            case 'System.Int16':
            case 'System.Int64':
                return Number(value);
            default:
                return value;
        }
    },

    // takes the property path, splits it on level delimites and returns
    // the array that represents the property hierarchy where each item
    // is the parent of the next item
    _getPropertyChunks: function (propertyPath) {
        var chunks = new Array();
        var propertyPathParts = propertyPath.split('/');
        var partCounter = propertyPathParts.length;
        while (partCounter--) {
            if (propertyPathParts[partCounter] != null && propertyPathParts[partCounter].length > 0) {
                chunks.push(propertyPathParts[partCounter]);
            }
        }
        return chunks;
    },

    // shows a given mode
    _showMode: function (showSimpleMode) {
        if (this.get_advancedModeButton()) {
            this.get_advancedModeButton().style.display = (showSimpleMode) ? 'block' : 'none';
        }

        if (this.get_simpleModeButton()) {
            if (this._implementsDesigner) {
                this.get_simpleModeButton().style.display = (!showSimpleMode) ? 'block' : 'none';
            } else {
                this.get_simpleModeButton().style.display = 'none';
            }
        }

        this._modesMultiPage.set_selectedIndex((!showSimpleMode) ? 1 : 0);
        if (showSimpleMode) {
            jQuery("body").addClass("sfDesignerSimpleMode").removeClass("sfDesignerAdvancedMode");
        }
        else {
            jQuery("body").addClass("sfDesignerAdvancedMode").removeClass("sfDesignerSimpleMode");
        }

        if (!showSimpleMode) {
            this._propertyGrid._changeView(true);
        }
    },

    _isInAdvancedMode: function () {
        return this._modesMultiPage.get_selectedIndex() == 1;
    },

    // searches the dirtyPropertyBag for the property specified
    // by the propertyPath and returns the index of the property
    // inside of the dirtyPropertyBag. Returns -1 if property does
    // not exits.
    _findPropertyIndex: function (propertyPath, bag) {
        var bagCount = bag.length;
        for (var i = 0; i < bagCount; i++) {
            if (bag[i].PropertyPath == propertyPath) {
                return i;
            }
        }
        return -1;
    },

    // examines the changes made to the control object and populates the
    // dirty property bag with the changed objects
    _examineControlChanges: function () {
        // compare the originalControl with the control object
        for (var propertyName in this._originalControl) {
            this._compareProperties(propertyName, '', this._originalControl, this._control);
        }
    },

    // recurcively compares all the properties of the original object that was loaded when page
    // was loaded and live object on which designer and property editor were working.
    _compareProperties: function (propertyName, propertyPath, originalObject, liveObject) {
        propertyPath += '/' + propertyName;
        if (this._isComplexProperty(propertyName, originalObject)) {
            // means there are child properties
            for (var childPropertyName in originalObject[propertyName]) {
                this._compareProperties(childPropertyName, propertyPath, originalObject[propertyName], liveObject[propertyName]);
            }
        } else {
            // no child properties; compare the values
            if (originalObject[propertyName] != liveObject[propertyName]) {
                this.changePropertyValue(propertyPath, liveObject[propertyName]);
            }
        }
    },

    // determines whether the property with the specified name on the given
    // object it complex property or a value property. Returns true if it is 
    // a complext property; otherwise false.
    _isComplexProperty: function (propertyName, originalObject) {
        // complex properties cannot be null by their definition
        if (!originalObject[propertyName]) {
            return false;
        }
        return (originalObject.propertyIsEnumerable(propertyName) && !this._isPrimitive(originalObject[propertyName]));
    },

    _isPrimitive: function (obj) {
        var objType = typeof (obj);
        switch (objType) {
            case 'string':
            case 'boolean':
            case 'number':
                return true;
            default:
                return false;
        }
    },

    // Raises the beforeSaveChanges event, cancelEventArgs should be of type Sys.CancelEventArgs
    _raiseBeforeSaveChanges: function (cancelEventArgs) {
        var args = cancelEventArgs;
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('beforeSaveChanges');
            if (h) h(this, args);
        }
        return cancelEventArgs;
    },

    // Raises the completeSaveChanges event, cancelEventArgs should be of type Sys.CancelEventArgs
    _raiseCompleteSaveChanges: function (result) {
        var arg = result;
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('completeSaveChanges');
            if (h) h(arg);
        }        
    },

    /* ************************* properties ************************* */

    // gets the client side representation of the server side control
    get_control: function () {
        return this._control;
    },
    // gets the array of deserialized WcfControlProperty objects representing the     
    get_propertyBag: function () {
        return this._propertyBag;
    },
    // gets the reference to the button that switches the editor into advanced mode
    get_advancedModeButton: function () {
        return this._advancedModeButton;
    },
    // sets the reference to the button that switches the editor into advanced mode
    set_advancedModeButton: function (value) {
        this._advancedModeButton = value;
    },
    // gets the reference to the button that switches the editor into simple mode
    get_simpleModeButton: function () {
        return this._simpleModeButton;
    },
    // sets the reference to the button that switches the editor into simple mode
    set_simpleModeButton: function (value) {
        this._simpleModeButton = value;
    },
    // gets the flag indicating if the property editor needs to invoke reload for the edited control
    get_forceReload: function () {
        return this._forceReload;
    },
    // sets the flag indicating if the property editor needs to invoke reload for the edited control
    set_forceReload: function (value) {
        this._forceReload = value;
    },
    // gets the reference to the save button of the editor
    get_saveButton: function () {
        return this._saveButton;
    },
    // sets the reference to the save button of the editor
    set_saveButton: function (value) {
        this._saveButton = value;
    },
    // gets the reference to the save button of the editor
    get_saveAllTranslationsButton: function () {
        return this._saveAllTranslationsButton;
    },
    // sets the reference to the save button of the editor
    set_saveAllTranslationsButton: function (value) {
        this._saveAllTranslationsButton = value;
    },
    // gets the reference to the cancel button of the editor
    get_cancelButton: function () {
        return this._cancelButton;
    },
    // sets the reference to the cancel button of the editor
    set_cancelButton: function (value) {
        this._cancelButton = value;
    },
    // gets the reference to the ok button of the editor
    get_okButton: function () {
        return this._okButton;
    },
    // sets the reference to the ok button of the editor
    set_okButton: function (value) {
        this._okButton = value;
    },
    // gets the reference to the client side designer componenent
    // if available; returns null otherwise.
    get_designer: function () {
        return this._designer;
    },
    // sets the reference to the client side designer component
    set_designer: function (value) {
        if (value == null) return;
        if (Object.getType(value).implementsInterface(Telerik.Sitefinity.Web.UI.ControlDesign.IControlDesigner)) {
            this._designer = value;
        }
    },

    get_propertyGrid: function () {
        return this._propertyGrid;
    },
    set_propertyGrid: function (value) {
        this._propertyGrid = value;
    }
    ,
    get_isOpenedByBrowseAndEdit: function () {
        return this._isOpenedByBrowseAndEdit;
    },
    set_isOpenedByBrowseAndEdit: function (value) {
        this._isOpenedByBrowseAndEdit = value;
    },
    get_titleElement: function () {
        return this._titleElement ? this._titleElement : jQuery("#propertyEditor h1")[0];
    },
    set_titleElement: function (value) {
        this._titleElement = value;
    },
    get_title: function () {
        var titleElement = this.get_titleElement();
        if (!titleElement) return "";
        return titleElement.innerHTML;
    },
    set_title: function (value) {
        var titleElement = this.get_titleElement();
        if (titleElement) titleElement.innerHTML = value;
    },
    get_providersSelector: function () {
        return this._providersSelector;
    },
    set_providersSelector: function (value) {
        this._providersSelector = value;
    },
    get_orLabel: function () {
        return this._orLabel;
    },
    set_orLabel: function (value) {
        this._orLabel = value;
    },
    get_saveConfirmationDialog: function () {
        return this._saveConfirmationDialog;
    },
    set_saveConfirmationDialog: function (value) {
        this._saveConfirmationDialog = value;
    }
};
Telerik.Sitefinity.Web.UI.PropertyEditor.registerClass('Telerik.Sitefinity.Web.UI.PropertyEditor', Telerik.Sitefinity.Web.UI.AjaxDialogBase);