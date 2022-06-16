//[BUILT-IN]ZoneEditor COMMANDS
//new
//delete
//duplicate
//indexchanged
//edit (does not do a webservice request, but ultimately leads to reload command)
//reload

//DOCK PROPERTIES
//controltype(in toolbox docks)
//controlid
//pageid
//layouttemplate

//DOCK ZONE PROPERTIES
//placeholderid

Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.ZoneEditor = function (element) {
    Telerik.Sitefinity.Web.UI.ZoneEditor.initializeBase(this, [element]);

    this._clientManager = null;

    this._lockingHandler = null;

    //TODO: FROM PETIO STILL: An array which holds all pending requests - the request will be executed once all previosly added requests are finished
    this._controlRequestStarted = false; //A flag which is used to allow only one request.
    this._pendingRequestsQueue = [];

    //IDs of child objects
    //The dock that will be cloned each time when a toolbox dock is dropped in a zone            
    this._commandMenuId = "";
    this._personalizationMenuId = "";
    this._windowMangerId = "";
    this._tabStripId = "";

    //Server props
    this._controlWrapperFactory = null;
    this._layoutWrapperFactory = null;

    this._webServiceUrl = "";
    this._propertyEditorUrl = "";
    this._segmentSelectorUrl = "";
    this._controlWebMethodName = "";
    this._layoutWebMethodName = "";

    this._emptyControlIds = null;

    //DockingZone IDs - to allow for docks to be properly initialized
    this._wrapperDockingZones = [];
    this._toolboxDockingZones = [];

    //TODO: Fill this with URLs of initially added CSS links
    //Keeps all CSS URLs that were included as a result of dragging a widget
    this._includedCssUrls = [];

    //Commands come from server - a name/value pair, allow for localization, as well as for any number of commands
    this._commands = {};

    //Client utility private variables
    this._currentEditedDock = null;
    this._toolboxDockingZonesUniqueNames = [];
    this._editMode = "Controls";
    this._enabled = true;

    this._layoutEditorId = null;
    this._themesEditorId = null;

    //NEW: Root level zones have no page ID, so controls will pick up root page id
    this._pageId = "";
    this._pageNodeId = "";
    this._url = "";
    this._appPath = "";
    this._mediaType = 0;
    this._currentLanguage = null;
    this._propertyServiceUrl = "";

    //Permissions
    this._PermissionsUrl = "";
    this._OverridenControlsDialogUrl = "";
    this._securedObjectType = "";
    this._managerClassName = "";
    this._notAuthorizedMessage = "";
    
    this._canCreate = false;
    this._localization = {};

    //Delegates
    this._commandSuccessDelegate = null;
    this._commandFailureDelegate = null;
    this._changeTemplateSuccessDelegate = null;
    this._changeTemplateFailureDelegate = null;

    //Selected template - for pages
    this._selectedTemplate = null;
    this._templateFieldControl = null;
    this._currentTemplateId = null;

    this._basePageServiceUrl = null;
    this._backToEditorTitle = null;

    this._isChangeMadeCookieName = "isChangeMade";
    this._isPageRefreshControlled = false;
    this._isUnlockingDone = false;
    this._isChangeMade = false;
    this._reloadInitiatedWithoutChanges = {};
    this._isNewDraft = false;
    this._hideSaveAllTranslations = false;
    this._placeholderPermissions = null;
    this._confirmDiscardOverride = null;
    this._confirmDeletedWidgetGenericDialog = null;
    this._deleteEditedWidgetConfirmationDialog = null;
    this._onBeforeExecCommands = [];

    // Personalization
    this._controlPersonalizationsService = null;
    this._personalizationDropDownCommand = null;
    this._forText = null;
    this._personalizedForText = null;

    // Forms
    this._headerPlaceholderName = "RadDockZoneHeader";
    this._footerPlaceholderName = "RadDockZoneFooter";
    this._bodyPlaceholderName = "RadDockZoneBody";
    this._allowedPageBreakPlaceholders = [this._bodyPlaceholderName];
    this._confirmDeleteFieldWithRules = "";
    this._formFieldsUsedInRules = [];
    this._getFormRulesSuccessDelegate = null;
    this._formsServiceUrl = "";
    this._refreshFormRulesEventName = "refresh_form_rules";
    this._hiddenFieldLabelAttribute = null;
}

Telerik.Sitefinity.Web.UI.ZoneEditor.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ZoneEditor.callBaseMethod(this, 'initialize');
        if (typeof this._placeholderPermissions != "undefined" && this._placeholderPermissions != null) {
            this._placeholderPermissions = Sys.Serialization.JavaScriptSerializer.deserialize(this._placeholderPermissions);
        }

        zoneEditorShared = this;

        this._localization = Sys.Serialization.JavaScriptSerializer.deserialize(this._localization);
        this._areYouSureMessage = this._getLocalizedMessage("");

        //If the control was disabled from the server
        if (!this._enabled) return;

        //Delegates for command success/fail
        this._commandSuccessDelegate = Function.createDelegate(this, this._commandSuccess);
        this._commandFailureDelegate = Function.createDelegate(this, this._commandFailure);

        //Delegates for template success/fail
        this._changeTemplateSuccessDelegate = Function.createDelegate(this, this._changeTemplateSuccess);
        this._changeTemplateFailureDelegate = Function.createDelegate(this, this._changeTemplateFailure);

        //Wrapper dock handlers
        this._wrapperDockPositionChangingDelegate = Function.createDelegate(this, this._onWrapperDockPositionChanging);
        this._wrapperDockPositionChangedDelegate = Function.createDelegate(this, this._onWrapperDockPositionChanged);
        this._wrapperDockCommandDelegate = Function.createDelegate(this, this._onWrapperDockCommand);
        //Toolbox dock handlers
        this._toolboxDockDragStartDelegate = Function.createDelegate(this, this._onToolboxDockDragStart);
        this._toolboxDockDragEndDelegate = Function.createDelegate(this, this._onToolboxDockDragEnd);
        //Common handler for all docks
        this._dockDragDelegate = Function.createDelegate(this, this._onDockDrag);

        //Common handlers for all docks
        this._dockStartDragDelegate = Function.createDelegate(this, this._onDockStartDrag);
        this._dockEnterZoneDelegate = Function.createDelegate(this, this._onEnterZone);
        this._dockEndDragDelegate = Function.createDelegate(this, this._onDockEndDrag);

        var toolboxZones = this._toolboxDockingZones;
        var forbiddenZoneArray = this._toolboxDockingZonesUniqueNames;

        //NEW: Set edit mode
        this.set_editMode(this._editMode);

        //Initialize toolbox docks first. Needed for preventing wrapper docks from docking into toolbox docking zones
        for (var i = 0; i < toolboxZones.length; i++) {
            var zone = $find(toolboxZones[i]);
            var docks = zone.get_docks();
            for (var j = 0; j < docks.length; j++) {
                this._configureToolboxDock(docks[j]);
            }

            //Add the zone's unique name to an array of forbidden zones
            forbiddenZoneArray[forbiddenZoneArray.length] = zone.get_uniqueName();
        }

        //        if (this._mediaType == 0 || this._mediaType == 1) {//e.g. Pages or Templates
        //            //set base zone as forbidden because we only want controls and layouts to be dropped in placeholders
        //            forbiddenZoneArray.push("RadDockZoneBody");
        //        }

        //Initialize wrapper docks
        var zones = this._wrapperDockingZones;
        for (var i = 0; i < zones.length; i++) {
            var zone = $find(zones[i]);
            if (!zone) continue; //TODO don't send missing zones from the server instead (ipelovski)

            //Process all docks - associate event handlers etc
            var docks = zone.get_docks();
            for (var j = 0; j < docks.length; j++) {
                this._configureWrapperDock(docks[j]);
            }

            //TODO: this might not be enough depending on the wireframe.
            zone.set_highlightedCssClass("zePlaceholderHighlighted");

            this._addPlaceholderLabelElement(zone.get_element());

            //"Fix" docking zone CSS according to SF needs
            this._configureZoneCss(zone);
        }

        this._windowClosedDelegate = Function.createDelegate(this, this._onEditWindowClose);
        this._personalizationWindowClosedDelegate = Function.createDelegate(this, this._onPersonalizationWindowClose);

        var menu = this._getCommandMenu();
        if (menu != null) menu.add_itemClicked(Function.createDelegate(this, this._onContextMenuClick));

        var personalizationMenu = this._getPersonalizationMenu();
        if (personalizationMenu != null) personalizationMenu.add_itemClicked(Function.createDelegate(this, this._onPersonalizationMenuClick));

        var thisObject = this;
        var emptyControlIds = this.get_emptyControlIds();
        for (var controlId in emptyControlIds) {
            var emptyControl = jQuery('#' + controlId);
            var dockControl = $find(emptyControlIds[controlId]);

            if (emptyControl && dockControl) {
                emptyControl.bind("click", { dock: dockControl }, function (event) {
                    thisObject.execCommand("edit", event.data.dock);
                });
            }
        }

        //Tabstrip
        var tabStrip = this._getTabStrip();
        if (tabStrip) tabStrip.add_tabSelected(Function.createDelegate(this, this._onTabSelected));

        //Update selected template in layouts tab for pages
        if (this._mediaType == 0 || this._mediaType == 1 || this._mediaType == 3 || this._mediaType == 4) {
            this._templateChangedDelegate = Function.createDelegate(this, this._templateChanged)
            this.get_templateFieldControl().remove_templateChanged(this._templateChangedDelegate);
            this.get_templateFieldControl().set_value(this.get_selectedTemplate());
            this.get_templateFieldControl().add_templateChanged(this._templateChangedDelegate);
            if (this._mediaType == 1) {
                this.get_templateFieldControl().set_currentTemplateId(this._currentTemplateId);
            }
        }


        this._confirmDiscardOverride = $find($('[id*="confirmDiscardOverride"]')[0].id);
        var editedWidgetConfirmationDialog = $('[id*="deleteEditedWidgetConfirmationDialog"]');
        if (editedWidgetConfirmationDialog.length > 0)
        {
            this._deleteEditedWidgetConfirmationDialog = $find(editedWidgetConfirmationDialog[0].id);
        }

        var confirmDeletedWidgetGenericDialog = $('[id*="deleteWidgetGenericConfirmationDialog"]');
        if (confirmDeletedWidgetGenericDialog.length > 0)
        {
            this._confirmDeletedWidgetGenericDialog = $find(confirmDeletedWidgetGenericDialog[0].id);
        }

        if ($telerik && $telerik.$) {
            $telerik.$(document).on("modalDialogClosed", jQuery.proxy(this._modalDialogClosedHandler, this));
            $telerik.$(document).on("personalizationDialogClosed", jQuery.proxy(this._onPersonalizationWindowCloseHandler, this));
        }

        if (this._mediaType === 2 && !this._isMultipageForm()) {
            this._hidePlaceholders([this._headerPlaceholderName, this._footerPlaceholderName]);
        }

        if (this._mediaType === 2 && this._isMultipageForm()) {
            // Append page labels if any
            this._updatePagesLabelsOnTheForm();
            var headerZone = $find(this._headerPlaceholderName);
            this._showZonePermanentLabel(headerZone);
            var footerZone = $find(this._footerPlaceholderName);
            this._showZonePermanentLabel(footerZone);
        }

        if (this._mediaType === 2){
            this._confirmFormFieldWidgetDeleteDelegate = Function.createDelegate(this, this._confirmDeleteFormFieldWidget);
            this.add_onBeforeExecCommand("formsConfirmation", this._confirmFormFieldWidgetDeleteDelegate);

            this._getFormRulesSuccessDelegate = Function.createDelegate(this, this._getFormRules);
            $(document).on(this._refreshFormRulesEventName, this._getFormRulesSuccessDelegate);
            $(document).trigger(this._refreshFormRulesEventName);
        }
    },

    dispose: function () {

        if (this._windowClosedDelegate) {
            delete this._windowClosedDelegate;
        }
        if (this._commandSuccessDelegate) {
            delete this._commandSuccessDelegate;
        }
        if (this._commandFailureDelegate) {
            delete this._commandFailureDelegate;
        }
        if (this._dockStartDragDelegate) {
            delete this._dockStartDragDelegate;
        }
        if (this._dockEnterZoneDelegate) {
            delete this._dockEnterZoneDelegate;
        }
        if (this._dockEndDragDelegate) {
            delete this._dockEndDragDelegate;
        }
        if (this._templateChangedDelegate) {
            this.get_templateFieldControl().remove_templateChanged(this._templateChangedDelegate);
            delete this._templateChangedDelegate;
        }
        if (this._changeTemplateSuccessDelegate) {
            delete this._changeTemplateSuccessDelegate;
        }
        if (this._changeTemplateFailureDelegate) {
            delete this._changeTemplateFailureDelegate;
        }
        if (this._confirmFormFieldWidgetDeleteDelegate) {
            delete this._confirmFormFieldWidgetDeleteDelegate;
        }
        if (this._getFormRulesSuccessDelegate){
            delete this._getFormRulesSuccessDelegate;
        }

        Telerik.Sitefinity.Web.UI.ZoneEditor.callBaseMethod(this, 'dispose');
    },

    //this flag is used when leaving the editor, so the user is notified for loosing his changes
    //this flag is set in this cases
    //1)set to true (serverside), when the current temp differs from the main draft
    //2)set to true when a control is dropped, deleted, moved in the layout
    //3)set to true when control properties are edited in through the control designers
    get_isChangeMade: function () {
        return this._isChangeMade;
    },

    set_isChangeMade: function (value) {

        this._isChangeMade = value;

    },

    //this flag is used to skip user confirmation for leaving changes and locking
    get_isPageRefreshControlled: function () {
        return this._isPageRefreshControlled;
    },
    set_isPageRefreshControlled: function (value) {
        this._isPageRefreshControlled = value;
    },

    get_isUnlockingDone: function () {
        return this._isUnlockingDone;
    },
    set_isUnlockingDone: function (value) {
        this._isUnlockingDone = value;
    },

    get_isNewDraft: function () {
        return this._isNewDraft;
    },
    set_isNewDraft: function (value) {
        this._isNewDraft = value;
    },

    get_hideSaveAllTranslations: function () {
        return this._hideSaveAllTranslations;
    },
    set_hideSaveAllTranslations: function (value) {
        this._hideSaveAllTranslations = value;
    },

    _modalDialogClosedHandler: function (e) {
        this.execCommand("reload", this._currentEditedDock);
    },

    _onPersonalizationWindowCloseHandler: function (event, args) {
        this._onPersonalizationWindowClose(this, { _argument: args });
    },

    //NOTE: The "tabselected" event must be used - and not the "tabselecting" - to avoid recursion from the programmatic change of the selected tab
    _onTabSelected: function (sender, args) {
        var val = args.get_tab().get_value();
        this.set_editMode(val);
    },

    _getLocalizedMessage: function (msgKey) {
        if (this._localization.hasOwnProperty(msgKey))
            return this._localization[msgKey];
        else
            return msgKey;
    },

    _getConfirmDeleteMessage: function () {
        if (this._editMode == "Layouts")
            return this._getLocalizedMessage("ZoneEditorConfirmDeleteLayout");
        else
            return this._getLocalizedMessage("ZoneEditorConfirmDeleteControl");
    },

    _getCurrentWebMethodName: function () {

        return this._editMode == "Layouts" ? this._layoutWebMethodName : this._controlWebMethodName;
    },

    _getClonedDock: function () {
        var dock = this._editMode == "Layouts" ? this.get_layoutWrapperFactory() : this.get_controlWrapperFactory();
        return this._cloneDock(dock);
    },


    //    //WCF web service support
    //    _invokeWebService: function (url, data, successDelegate) {
    //        var wRequest = new Sys.Net.WebRequest();
    //        wRequest.set_url(url);
    //        wRequest.set_httpVerb("PUT");
    //        if (data) {
    //            var postData = data; //In zone editor the object is already serialized //Sys.Serialization.JavaScriptSerializer.serialize(data);
    //            wRequest.set_body(postData);
    //            // setting content-length is not allowed
    //            // http://www.w3.org/TR/XMLHttpRequest2/#the-setrequestheader-method            
    //            //wRequest.get_headers()["Content-Length"] = postData.length;
    //            wRequest.get_headers()["Content-Type"] = "application/json";
    //        }

    //        wRequest.set_userContext(this.get_editMode());

    //        wRequest.add_completed(successDelegate);
    //        wRequest.invoke();
    //    },

    _createLayoutDockingZones: function (jsonObject, dock) {
        var clientIds = jsonObject.Attributes.clientIds.split(',');
        for (var i = 0; i < clientIds.length; i++) {
            var item = clientIds[i];

            if (item.indexOf("perm_") != -1 || item === "designerUrl" || item === "mastercontrolid")
                continue;
            //Add the zone as child element to this placholder
            var element = document.getElementById(item);
            if (typeof element === "undefined" || element === null) {
                var jElem = jQuery("*[id$=" + item + "]");
                if (jElem !== null && jElem.length > 0) {
                    if (jElem.length == 1)
                        element = jElem[0];
                    else {
                        for (var i = 0; i < jElem.length; i++) {
                            var currentElement = jElem[i];
                            if (currentElement !== null) {
                                if ($(currentElement).children().length === 0) {
                                    element = currentElement;
                                }
                            }
                        }
                    }
                }
            }
            if (typeof element === "undefined" || element === null) {
                alert("Cannot find docking zone");
                return;
            }
            //Zone must be added to the DOM before it is initialized, or else its initialization will fail with the RTL method!
            var zone = this._createDockingZone(element, "RadDockZone" + item);

            //Configure docking zone
            var zoneEl = zone.get_element();
            zoneEl.className = zoneEl.className.replace('zeDockZoneHasLabel', '');

            zoneEl.setAttribute("placeholderid", item);
            this._addPlaceholderLabelElement(zoneEl);

            //Add the zone to the list of zones
            this._wrapperDockingZones[this._wrapperDockingZones.length] = zone.get_id();

            //Configure the zone visually
            this._configureZoneCss(zone);
        }
    },

    _addPlaceholderLabelElement: function (zoneElement) {
        if (!zoneElement) {
            throw "_addPlaceholderLabelElement called for null zone element."
        }

        var layoutUpdater = LayoutUpdater.getInstance();

        var jZoneEl = jQuery(zoneElement);
        layoutUpdater.initializeZoneUI(jZoneEl, this);
    },

    _createDockingZone: function (element, id) {
        //Get the first zone from the wrapper zones array and clone it (cloning does not copy content)
        var zoneID = this._wrapperDockingZones[0];
        if (this._mediaType === 2) {
            // For the forms we have included Header and Footer placeholders
            // that is why in this case we need to get the body by placeholder id
            zoneID = this._bodyPlaceholderName;
        }

        var zone = $find(zoneID);
        //var clonedZone = zone.clone(id);
        var clonedZone = this._cloneDockZone(zone, id);
        //Add the cloned zone to the needed element
        element.appendChild(clonedZone.get_element());
        return clonedZone;
    },

    _markWidgetAsHidden: function (dock, elementAttributes) {
         var dockElement = dock.get_element();
         var attributes = elementAttributes ? elementAttributes : dockElement.attributes;
         var titleBar = dock.get_titleBar();

        if (titleBar && attributes && this._hiddenFieldLabelAttribute) {
            var captionElement = $(titleBar).find("em");
            if (captionElement.length > 0) {
                var hiddenFieldLabel = attributes[this._hiddenFieldLabelAttribute];
                if (hiddenFieldLabel) { 
                    var hiddenFieldText = hiddenFieldLabel.value ? hiddenFieldLabel.value : hiddenFieldLabel;
                    captionElement.attr(this._hiddenFieldLabelAttribute, hiddenFieldText);
                    captionElement.addClass("sfHiddenField");
                } else {
                    captionElement.removeClass("sfHiddenField");
                    captionElement.removeAttr(this._hiddenFieldLabelAttribute);
                }
            }
        }
    },

    _configureWidgetPersonalizationDropdown: function (dock, elementAttributes, controlId) {
        var personalizationMenuCommandElement = this._getPersonalizationDropDownCommandElement(dock),
            dockElement = dock.get_element(),
            name = this._personalizationDropDownCommand,
            attributes = elementAttributes ? elementAttributes : dockElement.attributes,
            titleBar = dock.get_titleBar();

        if (attributes["perm_" + name]) {
            if (attributes["perm_" + name].value === "False" || attributes["perm_" + name].value === "false") {
                personalizationMenuCommandElement.hide();
                if (titleBar) {
                    $(titleBar).removeClass("sfPersonilizedTitleBar");
                }
            }
            else {
                personalizationMenuCommandElement.show();
                if (titleBar) {
                    $(titleBar).addClass("sfPersonilizedTitleBar");
                }
            }
        }

        if (attributes["widgetSegments"]) {
            var widgetSegments = attributes["widgetSegments"].value ? jQuery.parseJSON(attributes["widgetSegments"].value) : jQuery.parseJSON(attributes["widgetSegments"]);
            if (!(widgetSegments && widgetSegments.length > 1)) {
                personalizationMenuCommandElement.hide();
                if (titleBar) {
                    $(titleBar).removeClass("sfPersonilizedTitleBar");
                }
            } else {
                if (!controlId) controlId = attributes["controlid"] ? attributes["controlid"].value : null;
                if (controlId) {

                    if (widgetSegments[widgetSegments.length - 1].CommandName === controlId) {
                        // Not specified segment
                        personalizationMenuCommandElement.text('');
                        personalizationMenuCommandElement.addClass('notSpecifiedCommand');
                    }
                    else {
                        // Specified segment
                        var currentSegment = widgetSegments.filter(function (s) { return s.CommandName === controlId; })[0];
                        if (currentSegment) {
                            personalizationMenuCommandElement.text(this._forText + ' ' + currentSegment.Text);
                        }
                        personalizationMenuCommandElement.removeClass('notSpecifiedCommand');
                    }
                }
            }
        }
        else if (titleBar && (dockElement.attributes && !dockElement.attributes["widgetSegments"])) {
            $(titleBar).removeClass("sfPersonilizedTitleBar");
        }

        var that = this;
        personalizationMenuCommandElement.unbind("click");
        personalizationMenuCommandElement.click(function () {
            var element = this;
            var personalizationMenu = that._getPersonalizationMenu();
            if (personalizationMenu) {
                if (personalizationMenu.__curentDock == dock && Sys.UI.DomElement.getVisible(personalizationMenu.get_contextMenuElement())) {
                    personalizationMenu.hide();
                }
                else {
                    window.setTimeout(Function.createDelegate(that, function () { that._showPersonalizationMenu(element, dock) }), 100);
                }
            }
        });
    },

    _configureWrapperDock: function (dock) {
        //Change commands to use a text instead of a picture
        var commands = dock.get_commands();
        if (this._checkCommands(commands)) {
            for (var name in commands) {
                var command = commands[name];
                if (dock.get_element().attributes["perm_" + name]) {
                    if (dock.get_element().attributes["perm_" + name].value === "False" || dock.get_element().attributes["perm_" + name].value === "false")
                        commands[name].set_visible(false);
                    else
                        commands[name].set_visible(true);

                }


                //In latest version of RadDock a commmand is <A><SPAN class=""/></A>
                command.get_element().firstChild.innerHTML = command.get_text();

            }
        }

        this._configureWidgetPersonalizationDropdown(dock);
        this._markWidgetAsHidden(dock);

        //NEW: Take care of titlebar - it should repaint itself
        dock.repaint();

        //Mechanism for preventing a wrapper dock from being added to a toolbox docking zone
        var forbiddenZones = Array.clone(this._toolboxDockingZonesUniqueNames);
        dock.set_forbiddenZones(forbiddenZones);

        if (this._isDockPageBreakField(dock)) {
            var allowedZones = Array.clone(this._allowedPageBreakPlaceholders);
            dock.set_allowedZones(allowedZones);
        }

        //Add client event handlers
        //Hook to the changing position in order to get the OLD docking zone
        dock.add_dockPositionChanging(this._wrapperDockPositionChangingDelegate);
        dock.add_dockPositionChanged(this._wrapperDockPositionChangedDelegate);
        dock.add_command(this._wrapperDockCommandDelegate);

        //Common handler for both kinds of docks
        dock.add_drag(this._dockDragDelegate);
        dock.add_enterZone(this._dockEnterZoneDelegate);
        dock.add_dragStart(this._dockStartDragDelegate);
        dock.add_dragEnd(this._dockEndDragDelegate);

        var thisObject = this;


        var additionalButtons = $('#' + dock.get_id()).find('a[dockId]').each(function (index) {
            $(this).click(function () {
                thisObject.execCommand("displayOverridenControls", dock);

            });
        });

        //jQuery(container).find(".sfAddContentWrp").click(function () {
        //    thisObject.execCommand("edit", dock);
        //});

    },

    _configureToolboxDock: function (dock) {
        //Add event handler
        dock.add_dragStart(this._toolboxDockDragStartDelegate);

        //Common handler for both kinds of docks
        dock.add_drag(this._dockDragDelegate);
    },

    //============================================= Dock event handlers ================================================
    _onToolboxDockDragStart: function (dock, args) {
        // get a reference to the RadDock HTML element
        var dockElement = dock.get_element();
        //Create a RadDock's clone
        //var clonedDock = dock.clone();
        var clonedDock = this._cloneDock(dock);

        //The cloned dock should not have the ToolboxDockDragStart event attached
        //Remove this handler or else we have a big problem!
        clonedDock.remove_dragStart(this._toolboxDockDragStartDelegate);
        //Add necessary handlers - only to the cloned dock and not to the original source!
        clonedDock.add_dragEnd(this._toolboxDockDragEndDelegate);
        clonedDock.add_enterZone(this._dockEnterZoneDelegate);

        //Set Toolbox zone as a forbidden for the cloned RadDock
        var zones = clonedDock.get_forbiddenZones();
        zones.push(dock.get_dockZone().get_uniqueName());

        if (this._isDockPageBreakField(clonedDock)) {
            var allowedZones = Array.clone(this._allowedPageBreakPlaceholders);
            clonedDock.set_allowedZones(allowedZones);
        }

        //        if (this._mediaType == 0 || this._mediaType == 1) {//e.g. Pages or Templates
        //            //set base zone as forbidden because we only want controls and layouts to be dropped in placeholders
        //            zones.push("RadDockZoneBody");
        //        }

        //Hide original RadDock.Once a cloned RadDock starts to drag we shouls show the orignal RadDock
        dockElement.style.display = "none";

        if (this._mediaType === 2) {
            if (this._isDockPageBreakField(dock)) {
                $('.rdPlaceHolder').addClass("sfPageBreak sfPageBreakDrag");
            }
        }

        //Create an event which will change the currently dragged RadDock - it will be cloned RadDock
        var ev = args.ownerEvent;
        ev = "originalEvent" in ev ? ev.originalEvent : ev;
        var handle = clonedDock.get_handle();

        if (document.createEvent) {
            // create event for FF and others
            var eventType = ev.type;
            if (/mouse/i.test(eventType)) {
                var evt = document.createEvent("MouseEvents");
                evt.initMouseEvent("mousedown", !ev.cancelBubble, ev.cancelable, ev.view,
                 ev.detail, ev.screenX, ev.screenY, ev.clientX, ev.clientY,
                 ev.ctrlKey, ev.altKey, ev.shiftKey, ev.metaKey,
                 ev.button, null);
                handle.dispatchEvent(evt);
            }
            else if (/pointer/i.test(eventType)) {
                var ie = window.navigator.msPointerEnabled;
                var time = new Date().getTime();
                if (ie) {
                    var eventType = "MSPointerDown";
                    if (window.navigator.pointerEnabled) {
                        eventType = "pointerdown";
                    } 
                    var evt = document.createEvent("MSPointerEvent");
                    evt.initPointerEvent(eventType,
				    	!ev.cancelBubble, ev.cancelable, ev.view,
				    	ev.detail, ev.screenX, ev.screenY, ev.clientX, ev.clientY,
				    	ev.ctrlKey, ev.altKey, ev.shiftKey, ev.metaKey,
				    	ev.button, null, ev.offsetX, ev.offsetY, ev.width, ev.height,
				    	ev.pressure, ev.rotation, ev.tiltX, ev.tiltY, ev.pointerId + time, ev.pointerType, time, ev.isPrimary);
                    handle.dispatchEvent(evt);
                } else {
                    var PointerEventInit = {
                        bubbles: !ev.cancelBubble,
                        cancelable: ev.cancelable,
                        view: ev.view,
                        detail: ev.detail,
                        screenX: ev.screenX,
                        screenY: ev.screenY,
                        clientX: ev.clientX,
                        clientY: ev.clientY,
                        ctrlKey: ev.ctrlKey,
                        altKey: ev.altKey,
                        shiftKey: ev.shiftKey,
                        metaKey: ev.metaKey,
                        button: ev.button,
                        offsetX: ev.offsetX,
                        offsetY: ev.offsetY,
                        widthArg: ev.width,
                        heightArg: ev.height,
                        pressure: ev.pressure,
                        rotation: ev.rotation,
                        tiltX: ev.tiltX,
                        tiltY: ev.tiltY,
                        pointerId: ev.pointerId + time,
                        pointerType: ev.pointerType,
                        isPrimary: ev.isPrimary
                    };

                    var event = new PointerEvent("pointerdown", PointerEventInit);
                    handle.dispatchEvent(event);
                }
            }
        } else {
            // create event for IE
            var evt = document.createEventObject();
            evt.clientX = ev.clientX;
            evt.clientY = ev.clientY;
            evt.button = ev.button;
            handle.fireEvent("onmousedown", evt)
        }

        //Show original RadDock
        dockElement.style.display = "";
        //Hack: a placeholder stays visible so we should hide it programatically
        dock.get_dockZone().hidePlaceholder();
        args.set_cancel(true);
    },


    _onToolboxDockDragEnd: function (dock, args) {
        var createNew = false;

        //Test if new zone belongs to the this._wrapperDockingZones 
        var curZone = dock.get_dockZone();
        var zones = this._wrapperDockingZones;
        for (var i = 0; i < zones.length; i++) {
            var zone = $find(zones[i]);
            if (zone == curZone) {
                createNew = true;
                break;
            }
        }

        //If dock is dropped at allowed location - call execCommand
        if (createNew && this._canCreate) this.execCommand("new", dock);

        if (this._mediaType === 2) {
            if (this._isDockPageBreakField(dock)) {
                if (this._isMultipageForm()) {
                    this._showPlaceholders([this._headerPlaceholderName, this._footerPlaceholderName]);
                }

                $('.rdPlaceHolder').removeClass("sfPageBreak sfPageBreakDrag");
            }
        }

        this._destroyDock(dock);

        //Fix the zone css
        this._configureZoneCss(dock.get_dockZone());
        if (!this._canCreate)
            alert(this._notAuthorizedMessage);

        if (this._mediaType === 2) {
            // Append page labels if any
            this._updatePagesLabelsOnTheForm();
        }
    },

    _onWrapperDockPositionChanging: function (dock, args) {
        //Set Old Zone and old index - because RadDock in its infinite wisdom does not support this out of the box
        dock.__zoneEditorLastDockZoneId = dock.get_dockZoneID();
        dock.__zoneEditorLastDockIndex = dock.get_index();
    },

    _onWrapperDockPositionChanged: function (dock, args) {
        this.execCommand("indexchanged", dock);
    },

    _onMultipleWrappersDockPositionChanged: function (docks) {
        if (docks && docks.length > 0) {
            var that = this;
            var delegateContainer = {};

            var successHandler = function () {
                that._commandSuccess.apply(this, arguments);

                if (docks.length > 0) {
                    var dock = docks[0];
                    docks = docks.splice(1);
                    that.execCommand("indexchanged", dock, delegateContainer.successDelegate);
                } else if (delegateContainer.successDelegate) {
                    delete delegateContainer.successDelegate;
                }
            };

            delegateContainer.successDelegate = Function.createDelegate(this, successHandler);

            var startingDock = docks[0];
            docks = docks.splice(1);
            that.execCommand("indexchanged", startingDock, delegateContainer.successDelegate);
        }
    },

    _returnDockToOldPosition: function (dock) {
        var oldZone = dock.__zoneEditorLastDockZoneId;
        if (oldZone) {
            var zone = $find(oldZone);
            if (zone) zone.dock(dock, dock.__zoneEditorLastDockIndex);
            dock.__zoneEditorLastDockZoneId = null;
            dock.__zoneEditorLastDockIndex = null;
        }
    },

    _templateChanged: function (sender, args) {
        var serviceUrl = this._webServiceUrl;
        if (this._mediaType == 1) {
            serviceUrl += 'Template/changeTemplate/';
        }
        else {
            serviceUrl += 'changeTemplate/';
        }

        var clientManager = this.get_clientManager();
        var urlParams = [];

        if (args.Template !== undefined) {
            urlParams["newTemplateId"] = args.Template.Id;
        } else {
            urlParams["newTemplateId"] = clientManager.GetEmptyGuid();
        }

        var keys = [];
        keys.push(this._pageId);
        clientManager.InvokePut(serviceUrl, urlParams, keys, this._pageId, this._changeTemplateSuccessDelegate, this._changeTemplateFailureDelegate);

    },

    _permissionsDialogClosed: function (sender, args) {

    },

    //============================================== Web-service related methods =============================================================//

    _getWebServiceParameters: function (commandName, dock) {
        var placeholderID = "";
        var zone = dock.get_dockZone();
        var attr = zone.get_element().attributes["placeholderid"];
        if (attr) placeholderID = attr.value;
        var perm = this._getPlaceholderPermissions(dock);
        var controlDataID = perm != null ? perm.ControlDataID : null;
        var prevPlaceholder = null;
        if (dock.__zoneEditorLastDockZoneId) {
            var prevDockZone = $find(dock.__zoneEditorLastDockZoneId);
            prevPlaceholder = prevDockZone.get_element().getAttribute("placeholderid");
        }

        var attributes = dock.get_element().attributes;
        var obj =
        {
            CommandName: commandName,
            ErrorMessage: "",
            PlaceholderId: placeholderID,
            ControlType: attributes["controltype"] ? attributes["controltype"].value : null, //Not always a type - existing controls contain just ID. On the server the ID can help determine all needed information
            LayoutTemlpate: attributes["layouttemplate"] ? attributes["layouttemplate"].value : null, //NEW: used to specify the exact path to a layout template
            Title: attributes["caption"] ? attributes["caption"].value : null,
            Description: attributes["description"] ? attributes["description"].value : null,
            ClassId: attributes["classId"] ? attributes["classId"].value : null,
            Ordinal: dock.get_index(), //The index -in case the dock is changing it
            DockId: dock.get_id(),      //System property: set the client DOCK ID so that we find it on our way back - after the web service
            Parameters: attributes["parameters"] ? Sys.Serialization.JavaScriptSerializer.deserialize(attributes["parameters"].value) : null,
            Attributes: {}, //NOTE: In fact the WCF service needs a completely different syntax - not key-value pairs - in order to properly deserialize this on the server!
            AddSubmit: attributes["addSubmit"] ? attributes["addSubmit"].value : null,
            FormSubmitType: attributes["formSubmitType"] ? attributes["formSubmitType"].value : null,
            reloadKey: attributes["reloadKey"] ? attributes["reloadKey"].value : null,
            designerUrl: attributes["designerUrl"] ? attributes["designerUrl"].value : null,
            widgetCommands: attributes["widgetCommands"] ? attributes["widgetCommands"].value : null,
            LayoutControlDataID: controlDataID,
            PreviousPlaceholderID: prevPlaceholder,
            ModuleName: attributes["modulename"] ? attributes["modulename"].value : null
        };

        //WCF service support - avoid SiteFinity server exception
        //Only provide a GUID in the JSON if it actually exists
        if (attributes["controlid"]) obj.Id = attributes["controlid"].value;

        //NEW: used to resolve the control ordering problems                        
        var previousSibling = zone.get_docks()[dock.get_index() - 1];
        if (previousSibling) {
            if (jQuery(previousSibling.get_element()).hasClass("zeOrphanedDock")) {
                obj.SiblingId = "00000000-0000-0000-0000-000000000000";
            } else {
                var siblingAttributes = previousSibling.get_element().attributes;
                if (siblingAttributes["controlid"]) obj.SiblingId = siblingAttributes["controlid"].value;
            }
        }

        //Support for PageID set as a part of the ControlData, not as a single property of the ZoneEditor. 
        //This allows for distinguishing to which Template(Page) a LayoutControl belongs
        obj.PageId = attributes["pageid"] ? attributes["pageid"].value : this._pageId;
        obj.PageNodeId = attributes["pagenodeid"] ? attributes["pagenodeid"].value : this._pageNodeId;
        obj.Url = this._url;
        obj.MediaType = this._mediaType;

        return obj;
    },


    //What is returned from web service?
    //1. DockId - so that we know where to load the stuff            
    //2. CommandName - so that we know what to do
    //3. Id - for newly created docks - this has to be set as an argument to the dock so that it can be edited!
    //4. Html - the encoded HTML that was returned from the server
    //5. ErrorMessage - if ErrorMessage is set, this means operation failed - and will be reverted on the client side
    _updateDockControlSettings: function (jsonObject, userContext) {
        //We need to know which the current dock that started the request was
        var dock = $find(jsonObject.DockId);

        var thisObject = this;

        if (jsonObject.CommandName == "rollback") {
            this.execCommand("reload", dock);
            return;
        }
        //Hide the loading panel - in case it was shown at all, of course
        this._hideLoadingPanel(dock);

        dock.set_enableDrag(true);


        //Enable dragging and dock commands

        var commands = dock.get_commands();
        if (this._checkCommands(commands)) {
            for (var name in commands) {
                if (jsonObject.Attributes["perm_" + name]) {
                    if (jsonObject.Attributes["perm_" + name] === "False")
                        commands[name].set_visible(false);
                    else
                        commands[name].set_visible(true);

                }
                else
                    commands[name].set_visible(true);

            }
        }

        this._configureWidgetPersonalizationDropdown(dock, jsonObject.Attributes, jsonObject.Id);
        this._markWidgetAsHidden(dock, jsonObject.Attributes);

        //Read the ErrorMessage from the response and only complete change if success        
        if (jsonObject.ErrorMessage) {
            this._restorePreviousState(jsonObject);
            return; //!
        }


        var commandName = jsonObject.CommandName;
        if (commandName == "delete") {
            var segmentsAttribute = dock.get_element().attributes["widgetSegments"];
            if (segmentsAttribute) {
                var widgetSegmentsSerialized = segmentsAttribute.value || segmentsAttribute;
                var segments = jQuery.parseJSON(widgetSegmentsSerialized);
                if (segments && segments.length > 0) {
                    var deletedSegments = segments.map(function (s) { return s.Text; });
                    if (deletedSegments && deletedSegments.length > 0) {
                        // Remvove Not Specified segment which is the last one
                        this.raiseEvent("personalizationSegmentRemoved", { segmentNames: deletedSegments.slice(0, deletedSegments.length - 1), mediaType: this._mediaType });
                    }
                }
            }

            dock.set_closed(true);
            var parentDockZone = dock.get_dockZone();
            //Remove RadDock from the zone            
            dock.undock();
            if (parentDockZone) {
                this._configureZoneCss(parentDockZone);
            }

            //Dispose children
            this._disposeElement(dock.get_contentContainer(), true);

            if (this._mediaType === 2) {
                var behaviourType = dock.get_element().getAttribute("behaviourobjecttype");
                if ((this._isDockPageBreakField(dock) || behaviourType === "Telerik.Sitefinity.Frontend.GridSystem.GridControl") && !this._isMultipageForm()) {
                    this._hidePlaceholders([this._headerPlaceholderName, this._footerPlaceholderName]);
                }
            }
        }

        //If Html property is set - result of "new" or "reload" - replace content of current dock with new content 
        if (jsonObject.Html) {
            var container = dock.get_contentContainer();

            //Dispose of all HTML + possible controls which can be in the dock to avoid leaks etc! For now just remove children
            this._disposeElement(container, true); //just the children

            //Change HTML
            container.innerHTML = jsonObject.Html;
            $telerik.evalScripts(container, function () { });

            var thisObject = this;
            jQuery(container).find(".sfAddContentWrp").click(function () {
                thisObject.execCommand("edit", dock);
            });

            if (jsonObject.CssLinkUrls && jQuery.isArray(jsonObject.CssLinkUrls)) {
                for (var i = 0; i < jsonObject.CssLinkUrls.length; i++) {
                    var cssLinkUrl = jsonObject.CssLinkUrls[i];
                    if (jQuery.inArray(cssLinkUrl, this._includedCssUrls) == -1) {
                        $('head').append('<link rel="stylesheet" href="' + cssLinkUrl + '" type="text/css" />');
                        this._includedCssUrls.push(cssLinkUrl);
                    }
                }
            }

            if (jsonObject.Scripts && jQuery.isArray(jsonObject.Scripts)) {
                for (var i = 0; i < jsonObject.Scripts.length; i++) {
                    var scriptUrl = jsonObject.Scripts[i];
                    $(container).append('<script type="text/javascript" src="' + scriptUrl + '" />');
                }
            }
        }

        var dockElement = dock.get_element();

        if (commandName != "indexchanged") {
            //Update other props - e.g. set a value for controlID (in case it was a new control created (or an existing control was duplicated)
            if (jsonObject.Id && dockElement.getAttribute("controlid") != jsonObject.Id) {
                dockElement.setAttribute("controlid", jsonObject.Id);
            }

            if (jsonObject.ControlType && dockElement.getAttribute("controltype") != jsonObject.ControlType) {
                dockElement.setAttribute("controltype", jsonObject.ControlType);
            }

            if (jsonObject.ModuleName && dockElement.getAttribute("moduleName") != jsonObject.ModuleName) {
                dockElement.setAttribute("moduleName", jsonObject.ModuleName);
            }

            if (jsonObject.Permissions && jsonObject.Permissions.length > 0) {
                var iter = jsonObject.Permissions.length;
                while (iter--) {
                    var perm = jsonObject.Permissions[iter];
                    dockElement.setAttribute(perm.Key, perm.Value);
                }
            }

            if (jsonObject.Attributes) {
                for (key in jsonObject.Attributes) {
                    dockElement.setAttribute(key, jsonObject.Attributes[key]);
                }
            }

            if (jsonObject.Attributes.reloadKey) {
                dockElement.setAttribute("reloadKey", jsonObject.Attributes.reloadKey);
            }

            if (jsonObject.Attributes.createDependentKeys) {
                dockElement.setAttribute("createDependentKeys", jsonObject.Attributes.createDependentKeys);
            }

            if (jsonObject.Attributes.deleteDependentKeys) {
                dockElement.setAttribute("deleteDependentKeys", jsonObject.Attributes.deleteDependentKeys);
            }

            if (jsonObject.Attributes.indexChangedDependentKeys) {
                dockElement.setAttribute("indexChangedDependentKeys", jsonObject.Attributes.indexChangedDependentKeys);
            }

            if (jsonObject.Attributes.reloadDependentKeys) {
                dockElement.setAttribute("reloadDependentKeys", jsonObject.Attributes.reloadDependentKeys);
            }

            if (jsonObject.Attributes.designerUrl) {
                dockElement.setAttribute("designerUrl", jsonObject.Attributes.designerUrl);
            }

            if (jsonObject.Attributes.widgetCommands && jsonObject.Attributes.widgetCommands !== "null") {
                dockElement.setAttribute("widgetCommands", jsonObject.Attributes.widgetCommands);
            }

            if (jsonObject.Attributes.widgetSegments && jsonObject.Attributes.widgetSegments !== "null") {
                dockElement.setAttribute("widgetSegments", jsonObject.Attributes.widgetSegments);
            }

            if (typeof jsonObject.CustomTitleHtml == "string" || jsonObject.CustomTitleHtml == null) {
                // sfCustomTitle sfCustomTitleWrapper
                var jDockElem = jQuery(dockElement);
                if (jDockElem.hasClass("sfCustomTitle")) {
                    jDockElem.addClass("sfCustomTitle");
                }
                var jWrapper = jDockElem.find(".sfCustomTitleWrapper");
                if (jWrapper.length > 0) {
                    jWrapper.html(jsonObject.CustomTitleHtml);
                }
                else if (jsonObject.CustomTitleHtml != null) {
                    var span = document.createElement("SPAN");
                    span.setAttribute("class", "sfCustomTitleWrapper");
                    span.innerHTML = jsonObject.CustomTitleHtml;
                    jDockElem.find(".rdCommands").after(span);
                }
            }


            var additionalButtons = $('#' + dock.get_id()).find('a[dockId]').each(function (index) {
                $(this).click(function () {
                    thisObject.execCommand("displayOverridenControls", dock);

                });
            });
        }

        // Reload controls which are dependent of this control
        this._loadDependentControls(jsonObject, dock);

        if (userContext == "Layouts" && jsonObject.ModifiedLayoutPermissios) {
            var mlpIter = jsonObject.ModifiedLayoutPermissios.length;
            while (mlpIter--) {
                var mplCur = jsonObject.ModifiedLayoutPermissios[mlpIter];
                this._placeholderPermissions[mplCur.Key] = mplCur.Value;
            }
        }

        //NEW: If usercontext was Layouts, process and create docking zones
        if (userContext == "Layouts"
            && (
                commandName == "new"
                || commandName == "reload"
                || commandName == "reloadDependant"
                || commandName == "duplicate")) {
            this._createLayoutDockingZones(jsonObject, dock);
        }
    },

    _getPlaceholderPermissions: function (dock) {
        var zone = dock.get_dockZone();
        var attr = zone.get_element().attributes["placeholderid"];
        if (attr) {
            var placeholderID = attr.value;
            if (this._placeholderPermissions.hasOwnProperty(placeholderID)) {
                var palceholderPermissios = this._placeholderPermissions[placeholderID];
                return palceholderPermissios;
            }
        }
        return null;
    },

    _canExecuteCommand: function (commandName, dock) {
        // step 1: check placeholder permissions first
        var ok = true;
        var palceholderPermissios = this._getPlaceholderPermissions(dock);
        if (palceholderPermissios != null) {
            if (commandName == "duplicate" || commandName == "new" || commandName == "indexchanged") {
                ok = palceholderPermissios.DropOn;
            }
        }
        if (!ok)
            return false;

        // step 2: check control permissions after that

        // Reload SHOULD always execute a command
        if (commandName == "reload" || commandName == "reloadDependant")
            return true;
        if (commandName == "new")
            return this._canCreate;
        var attrName = "perm_" + commandName;
        var elem = dock.get_element();
        var val = elem.getAttribute(attrName);
        val = val ? val.toLowerCase() : "false";
        return val == "true";
    },
    _executeEnableOverride: function (dock, overrideState, args) {


        var urlParams = [];
        var keys = [];
        args.id = dock.get_element().attributes["controlid"].value;
        //keys.push(dock.get_element().attributes["controlid"].value);
        keys.push(overrideState);
        args.DockId = dock.get_id();
        var context = { EditMode: this.get_editMode() };
        this.get_clientManager().InvokePut(this._webServiceUrl + "setoverride/", urlParams, keys, args, this._commandSuccessDelegate, this._commandFailureDelegate, this, false, null, context);

        //this._invokeWebService(url+"/", serializedDockInfo, Function.createDelegate(this, this._onClientResponseEnding));

        //       this.get_clientManager().InvokePut(this._webServiceUrl + "setoverride/", urlParams, keys,this._commandSuccess, this._commandFailureDelegate);
    },
    showConfirmDeleteWidgetDialog: function (title, message, callback, args) {
        if (this._confirmDeletedWidgetGenericDialog) {
            this._confirmDeletedWidgetGenericDialog.show_prompt(title, message, callback, args);
        }
    },
    _confirmDeleteFormFieldWidget: function (commandName, dock) {
        var deferred = $.Deferred();
        if (commandName === "beforedelete") {
            var controlKeyAttribute = dock.get_element().attributes["controlkey"];
            var controlKey = controlKeyAttribute ? controlKeyAttribute.value : null;
            if (controlKey && this._formFieldsUsedInRules.indexOf(controlKey) !== -1){
                this.showConfirmDeleteWidgetDialog(null, this._confirmDeleteFieldWithRules,
                    function (sender, args) {
                        if (args.get_commandName() === "confirmDelete") {
                            deferred.resolve();
                        } else {
                            deferred.reject();
                        }
                    });
            } else {
                deferred.resolve();
            }
        } else {
            deferred.resolve();
        }

        return deferred.promise();
    },
    _getFormRules: function () {
        var that = this;
        $.ajax({
            type: 'GET',
            url: this._formsServiceUrl + "/rules/" + that._pageId + "/",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (!data) return;
                var rules = JSON.parse(data);
                if (rules) { 
                    that._formFieldsUsedInRules = [];
                    rules.forEach(function (rule){
                        rule.Conditions.forEach(function (condition) {
                            that._formFieldsUsedInRules.push(condition.Id);
                        });

                        rule.Actions.forEach(function (action) {
                            that._formFieldsUsedInRules.push(action.Target);
                        });
                    });
                }
            }
        });
    },
    _executeWidgetRollback: function (dock, args) {

        var oLDialog = this._confirmDiscardOverride;

        var urlParams = [];
        var keys = [];
        keys.push(dock.get_element().attributes["controlid"].value);
        keys.push(this._pageId);
        var context = { EditMode: this.get_editMode() };
        args.DockId = dock.get_id();
        args.id = dock.get_element().attributes["controlid"].value;
        args.PageId = this._pageId;

        var segmentsAttribute = dock.get_element().attributes["widgetSegments"];
        if (segmentsAttribute) {
            var widgetSegmentsSerialized = segmentsAttribute.value || segmentsAttribute;
            var segments = jQuery.parseJSON(widgetSegmentsSerialized);
            if (segments && segments.length > 0) {
                var deletedSegments = segments.map(function (s) { return s.Text; });
                args.segmentNames = deletedSegments.slice(0, deletedSegments.length - 1)
            }
        }

        oLDialog.show_prompt(null, null, Function.createDelegate(this, this._handleDiscardOverride), args);


    },
    _handleDiscardOverride: function (sender, args) {
        if (args.get_commandName() == "rollback") {
            var dockargs = sender._lastContext;
            var context = { EditMode: this.get_editMode() };
            this.get_clientManager().InvokePut(this._webServiceUrl + "rollback/", null, null, dockargs, this._commandSuccessDelegate, this._commandFailureDelegate, this, false, null, context);
            this.raiseEvent("personalizationSegmentRemoved", { segmentNames: args.get_commandArgument().segmentNames, mediaType: this._mediaType });
        }
    },

    add_onBeforeExecCommand: function (key, action){
        if (typeof (action) !== "function")
            throw "Action must be a function";

        var existingItem = this._onBeforeExecCommands.filter(function (item) { return item.key === key; })[0];
        if (existingItem) {
            existingItem.action = action;
        } else {
            this._onBeforeExecCommands.push({key: key, action: action});
        }
    },

    remove_onBeforeExecCommand: function (key){
        var existingItem = this._onBeforeExecCommands.filter(function (item) { return item.key === key; })[0];
        if (existingItem) {
            var index = this._onBeforeExecCommands.indexOf(existingItem);
            this._onBeforeExecCommands.splice(index, 1);
        }
    },

    execCommand: function (commandName, dock, successHandler, errorHandler) {
        var that = this;
        var executeCommandPromises = [];
        for (var i = 0; i < this._onBeforeExecCommands.length; i++) {
            
            if (typeof (this._onBeforeExecCommands[i].action) === "function") {
                var onBeforeExecuteCommandResult = this._onBeforeExecCommands[i].action(commandName, dock);
                executeCommandPromises.push(onBeforeExecuteCommandResult);                     
            } 

            if (executeCommandPromises.length > 0) {
                $.when.apply($, executeCommandPromises).done(function() {
                    that._execCommandInternal(commandName, dock, successHandler, errorHandler);
                });  

                return;
            }
        }

        that._execCommandInternal(commandName, dock, successHandler, errorHandler);
    },

    _execCommandInternal: function (commandName, dock, successHandler, errorHandler) {
        //alert("execCommand: commandName=" + commandName);
        //before delete is a kind of fake command, so let it execute here without throwing a command event
        if (commandName == "beforedelete") {
            if (!this._canCreate) {
                alert(this._notAuthorizedMessage);
                return;
            }

            var delConfirmCallBackFn = Function.createDelegate(this, function (arg) {
                //If user clicked OK
                if (arg == true) this.execCommand("delete", dock);
            });

            var handleDeleteEditedWidget = Function.createDelegate(this, function (sender, args) { 
                if (args.get_commandName() == "confirmDelete") {
                    delConfirmCallBackFn(true);
                }
            });

            var displayOverridenControls = jQuery(dock.get_element()).find("[dockId='" + dock.get_id() + "']");
            if (displayOverridenControls != null && displayOverridenControls.length > 0) {
                var pagesCount = parseInt(displayOverridenControls.attr('overriddenControlsCount'));
                if (!isNaN(pagesCount) && (pagesCount > 0) && this._deleteEditedWidgetConfirmationDialog != null)
                {
                    var message = this._deleteEditedWidgetConfirmationDialog.get_initialMessage();
                    message = String.format(message, pagesCount);
                    this._deleteEditedWidgetConfirmationDialog.show_prompt(null, message, Function.createDelegate(this, handleDeleteEditedWidget), null);
                }
            }
            else {
                //this._getWindowManager().radconfirm(this._areYouSureMessage, delConfirmCallBackFn, 330, 100, null, this._areYouSureMessage);
                //TEMP: For development purposes - just call delete right away
                delConfirmCallBackFn(true);
            }

            return;
        }


        if (commandName == "displayWidgetOverrideText")
            return;
        //Raise event OnClientCommand. Create request params - supply command name and dock!        
        //1. The args object must contain the SAME props as the ones to be submitted to the web service
        //2. Developer must be able to change those - and the changed ones should be sent to the server
        //3. BUT for the info to be correct, some of the actions need that the dock is already cloned - and its ID is supplied
        var jsonObject = this._getWebServiceParameters(commandName, dock);
        //Remove certain information from the args object - e.g.the DockId!
        delete jsonObject.DockId;
        var args = new Sys.CancelEventArgs();
        $telerik.cloneJsObject(jsonObject, args);
        this.raiseEvent("command", args);

        if (args.get_cancel() || !this._canExecuteCommand(commandName, dock)) {
            //Restore old position if command was indexchanged
            if (commandName == "indexchanged") this._returnDockToOldPosition(dock);
            return;
        }
        //delete the _cancel prop to prevent it from being serialized
        delete args._cancel;

        if (commandName == "edit") {
            this._editItem(dock);
            return;
        }
        else if (commandName == "addPersonalizedVersion") {
            this._addPersonalizedVersion(dock);
            return;
        }
        else if (commandName == "removePersonalizedVersion") {
            this._removePersonalizedVersion(dock);
            return;
        }
        else if (commandName == "widgetOverride") {
            this._executeEnableOverride(dock, "true", args);
            return;
        }
        else if (commandName == "widgetDisableOverride") {
            this._executeEnableOverride(dock, "false", args);

            return;
        }
        else if (commandName == "rollback") {
            this._executeWidgetRollback(dock, args);
            return;
        }
        else if (commandName == "displayOverridenControls") {
            var controlId = dock.get_element().attributes["controlid"].value;
            var permUrl = this._OverridenControlsDialogUrl;

            var strippedTitle = dock.get_title().replace(/(<([^>]+)>)/ig, "");

            permUrl = ((permUrl.indexOf("?") < 0) ? permUrl : permUrl.substring(0, permUrl.indexOf("?"))) + "?";
            permUrl += "baseControlId=" + controlId;


            var manager = this._getWindowManager();
            var oWnd = manager.open(permUrl, "displaywidgetoverride");
            oWnd.set_visibleTitlebar(true);
            oWnd.set_visibleStatusbar(false);
            oWnd.set_width(425);
            oWnd.set_height(250);
            oWnd.set_autoSizeBehaviors(5);
            oWnd.center();
            Telerik.Sitefinity.centerWindowHorizontally(oWnd);

            var self = this;

            return;
        }
        else if (commandName == "indexchanged") {
            //Configure both the old and the new zone
            if (dock.__zoneEditorLastDockZoneId) {
                var prevDockZone = $find(dock.__zoneEditorLastDockZoneId);
                var oldPlaceholdID = prevDockZone.get_element().getAttribute("placeholderid");
                this._configureZoneCss(prevDockZone);
            }
            this._configureZoneCss(dock.get_dockZone());
        }
        else if (commandName == "new")//Configure the newly created wrapper dock
        {
            var cloned = this._getClonedDock();

            //Set title and other props!
            cloned.set_title(dock.get_title());

            //Add it to docking zone and position where the old dock was
            dock.get_dockZone().dock(cloned, dock.get_index());

            var originalElement = dock.get_element();
            var clonedElement = cloned.get_element();

            //Set its isPageBreak attribute!
            var isPageBreakAttr = originalElement.getAttribute("isPageBreak");
            if (isPageBreakAttr) {
                clonedElement.setAttribute("isPageBreak", isPageBreakAttr);
            }

            //Set its controltype attribute!
            var attr = originalElement.getAttribute("controltype");
            if (attr) clonedElement.setAttribute("controltype", attr);

            //Set the layouttemplate attribute
            var attr = originalElement.getAttribute("layouttemplate");
            if (attr) clonedElement.setAttribute("layouttemplate", attr);

            //Remove the controlid attr - just in case if it exists (it should not if implementation is proper)
            if (clonedElement.getAttribute("controlid")) {
                clonedElement.removeAttribute("controlid");
            }
            //Clear its content
            cloned.get_contentContainer().innerHTML = "";
            if (isPageBreakAttr === true || isPageBreakAttr === "True") {
                Sys.UI.DomElement.addCssClass($(cloned.get_contentContainer()).closest('.zeControlDock')[0], "sfPageBreak");
            }

            //Configure new dock
            this._configureWrapperDock(cloned);

            //Set the dock variable to point to the new dock!
            dock = cloned;
        }
        else if (commandName == "duplicate") {//if duplicate- we need another DOCK, a CLONED one
            if (!this._canCreate) {
                alert(this._notAuthorizedMessage);
                return;
            }

            var cloned = this._getClonedDock();

            //Set title and other props!
            cloned.set_title(dock.get_title());

            //Add it to docking zone and position after the old dock
            dock.get_dockZone().dock(cloned, dock.get_index() + 1);

            var originalElement = dock.get_element();
            var clonedElement = cloned.get_element();

            //Set its isPageBreak attribute!
            var isPageBreakAttr = originalElement.getAttribute("isPageBreak");
            if (isPageBreakAttr) {
                clonedElement.setAttribute("isPageBreak", isPageBreakAttr);
            }

            //Set its controltype attribute!
            var attr = originalElement.getAttribute("controltype");
            if (attr) clonedElement.setAttribute("controltype", attr);

            //Set the layouttemplate attribute
            var attr = originalElement.getAttribute("layouttemplate");
            if (attr) clonedElement.setAttribute("layouttemplate", attr);

            //Set the behaviourobjecttype attribute
            var attr = originalElement.getAttribute("behaviourobjecttype");
            if (attr) clonedElement.setAttribute("behaviourobjecttype", attr);

            //Set the openAdminAppEditor attribute
            var attr = originalElement.getAttribute("openAdminAppEditor");
            if (attr) clonedElement.setAttribute("openAdminAppEditor", attr);

            //Remove the controlid attr - just in case if it exists (it should not if implementation is proper)
            clonedElement.removeAttribute("controlid");
            //Clear its content
            cloned.get_contentContainer().innerHTML = "";
            if (isPageBreakAttr === true || isPageBreakAttr === "True") {
                Sys.UI.DomElement.addCssClass($(cloned.get_contentContainer()).closest('.zeControlDock')[0], "sfPageBreak");
            }

            //Configure new dock
            this._configureWrapperDock(cloned);

            //Set the dock variable to point to the new dock!
            dock = cloned;
            //            //NEW: A dock can contain a RadControl that came from web-service + its script elements!
            //            //This creates a huge problem when cloning such a control! Scripts will be evaled!
            //            //TODO: This should be added to $telerik.clone method!
            //            var container = dock.get_contentContainer();
            //            var scripts = container.getElementsByTagName("script");
            //            while (scripts.length > 0) {
            //                var script = scripts[0];
            //                script.parentNode.removeChild(script);
            //            }

            //            //Clone the dock
            //            dock = dock.clone();

            //            //Remove current content from the dock's content area. New content should come for the server                
            //            dock.get_contentContainer().innerHTML = "";
        } else if (commandName == "permissions") {
            var controlId = dock.get_element().attributes["mastercontrolid"].value;
            var permUrl = this._PermissionsUrl;

            var strippedTitle = dock.get_title().replace(/(<([^>]+)>)/ig, "");

            permUrl = ((permUrl.indexOf("?") < 0) ? permUrl : permUrl.substring(0, permUrl.indexOf("?"))) + "?";
            permUrl += "securedObjectID=" + controlId +
                "&" + "managerClassName=" + this._managerClassName +
                "&" + "securedObjectTypeName=" + this._securedObjectType +
                "&" + "title=" + encodeURIComponent(strippedTitle) +
                "&" + "showPermissionSetNameTitle=false" +
                "&" + "backLabelText=" + this._backToEditorTitle;

            var manager = this._getWindowManager();
            var oWnd = manager.open(permUrl, "permissions");
            oWnd.set_visibleStatusbar(false);
            oWnd.set_width(100);
            oWnd.set_height(100);
            oWnd.center();
            oWnd.maximize();
            var self = this;
            oWnd.add_close(function () {
                self.execCommand("reload", dock);
            });
            return;
        }


        //Set common settings
        dock.set_collapsed(false);    //Expand RadDock
        dock.set_enableDrag(false);   //Disable RadDock's dragging. It will be enabled once the response is finished
        this._showLoadingPanel(dock); //All commands (even indexchanged - will require confirmation from the server - so show the loading sign!)

        //Proceed with the request
        var params = this._getWebServiceParameters(commandName, dock);


        //OK, developer could have modified some of the stuff in the args object, so copy it over
        //NEW: but first CHANGE the Attributes array into the syntax that a WCF service expects        
        args.Attributes = this._serializeCollectionWCF(args.Attributes);
        $telerik.cloneJsObject(args, params);

        if (commandName == "reload" || commandName == "duplicate") {
            params["PageId"] = this._pageId;
        }

        var successDelegate = successHandler ? successHandler : this._commandSuccessDelegate,
            failureDelegate = errorHandler ? errorHandler : this._commandFailureDelegate;

        this._createXmlHttpRequest(params, commandName, successDelegate, failureDelegate);
    },

    _commandSuccess: function (caller, data, request, context) {

        //Get JSON object from server
        var jsonObject = data;
        //Fix the Attributes array
        jsonObject.Attributes = this._deserializeCollectionWCF(jsonObject.Attributes);


        //Raise ZoneEditor's own ResponseEnding event - let user change data in jsonObject AND/OR cancel the event completely!
        var args = new Sys.CancelEventArgs();
        $telerik.cloneJsObject(args, jsonObject);
        this.raiseEvent("responseEnding", jsonObject);
        if (jsonObject.get_cancel()) return;

        //Process returned web service response
        this._updateDockControlSettings(jsonObject, context.EditMode);

        this._addSubmitButton(jsonObject);

        if (jsonObject && jsonObject.CommandName === 'new') {
            jQuery(document).trigger({
                type: 'sf-zone-editor-item-dropped',
                sender: this,
                args: jsonObject
            });
        }

        if (context.Command !== 'reload' || !this._reloadInitiatedWithoutChanges[jsonObject.Id]) {
            this.set_isChangeMade(true);
        } else {
            delete this._reloadInitiatedWithoutChanges[jsonObject.Id];
        }

        if (this._mediaType === 2) {
            // Append page labels if any
            this._updatePagesLabelsOnTheForm();
            if (context.Command == "delete" && this._formFieldsUsedInRules && this._formFieldsUsedInRules.length > 0) {
                $(document).trigger(this._refreshFormRulesEventName);
            }
        }

        this.raiseEvent("commandSuccess", jsonObject);
    },

    _loadDependentControls: function (jsonObject, dock) {
        var dockElement = dock.get_element();
        var myID = dockElement.id;
        var controlsToUpdate = [];
        var dependentDocks = null;

        if (jsonObject.ReloadControlsWithSameKey == true) {
            dependentDocks = this._findDocksByReloadKey(myID, dockElement.getAttribute("reloadKey"));
            controlsToUpdate = controlsToUpdate.concat(dependentDocks);
        }

        var depndendCriteria = [
            { attr: "createDependentKeys", commands: ["new", "duplicate"] },
            { attr: "deleteDependentKeys", commands: ["delete"] },
            { attr: "indexChangedDependentKeys", commands: ["indexchanged"] },
            { attr: "reloadDependentKeys", commands: ["reload"] }
        ];

        for (var j = 0; j < depndendCriteria.length; j++) {
            var attr = dockElement.getAttribute(depndendCriteria[j].attr);
                
            if (attr) {
                var commands = depndendCriteria[j].commands;
                var keys = JSON.parse(attr);

                if (commands.indexOf(jsonObject.CommandName) != -1 && keys && keys.length > 0) {
                    for (var g = 0; g < keys.length; g++) {
                        dependentDocks = this._findDocksByReloadKey(myID, keys[g]);
                        controlsToUpdate = controlsToUpdate.concat(dependentDocks);
                    }
                }
            }
        }

        for (var i = 0; i < controlsToUpdate.length; i++) {
            var ctrlToUpd = controlsToUpdate[i];
            this.execCommand("reloadDependant", ctrlToUpd);
        }
    },

    _commandFailure: function (error, caller, context) {
        var handled = this.get_lockingHandler().tryHandleError(error);
        if (!handled) {
            if (typeof error.StatusCode == "number" && error.StatusCode == 403) {

            }
            alert(error.Detail);
        }
    },

    _changeTemplateSuccess: function (caller, data, request, context) {
        this.set_isPageRefreshControlled(true);
        document.location.href = document.location.href;
    },
    _changeTemplateFailure: function (error, caller, context) {
        alert(error.Detail);
    },

    //    _onClientResponseEnding: function (executor, eventArgs) {
    //        //Get JSON object from server
    //        var jsonObject = Sys.Serialization.JavaScriptSerializer.deserialize(executor.get_responseData());
    //        //Fix the Attributes array
    //        jsonObject.Attributes = this._deserializeCollectionWCF(jsonObject.Attributes);

    //        //Raise ZoneEditor's own ResponseEnding event - let user change data in jsonObject AND/OR cancel the event completely!
    //        var args = new Sys.CancelEventArgs();
    //        $telerik.cloneJsObject(args, jsonObject);
    //        this.raiseEvent("responseEnding", jsonObject);
    //        if (jsonObject.get_cancel()) return;

    //        //Process returned web service response
    //        this._updateDockControlSettings(jsonObject, executor._webRequest._userContext);

    //        //Set flag which indicates that the current request is finished, check for pendnginRequests and create them
    //        this._controlRequestStarted = false;
    //        this._createXmlHttpRequest();

    //        this._addSubmitButton(jsonObject);
    //    },

    //on adding first widget from toolbox(which is not layout)) automatically add submit button widget
    _addSubmitButton: function (jsonObject) {
        var addSubmit = jsonObject.Attributes["addSubmit"];
        if (addSubmit && addSubmit.toLowerCase() == "true") {
            this._dropSubmitButton(this._findDockByControlType(jsonObject.Attributes["formSubmitType"]), jsonObject.DockId);
        }
    },

    _dropSubmitButton: function (submitButtonDock, dockId) {
        if (submitButtonDock) {
            var clonedSubmitButtonDock = this._cloneDock(submitButtonDock);

            //The cloned dock should not have the ToolboxDockDragStart event attached
            clonedSubmitButtonDock.remove_dragStart(this._toolboxDockDragStartDelegate);
            if (clonedSubmitButtonDock != null) {
                var dock = $find(dockId);
                var previousDockZone = dock.get_dockZone();
                clonedSubmitButtonDock.set_dockZone(previousDockZone);
                this.execCommand("new", clonedSubmitButtonDock);
                this._destroyDock(clonedSubmitButtonDock);
                //Fix the zone css
                this._configureZoneCss(clonedSubmitButtonDock.get_dockZone());
            }
        }
    },

    _findDockByControlType: function (controlType) {
        var toolboxZones = this._toolboxDockingZones;
        for (var i = 0; i < toolboxZones.length; i++) {
            var zone = $find(toolboxZones[i]);
            var docks = zone.get_docks();
            for (var j = 0; j < docks.length; j++) {
                if (docks[j].get_element().attributes["controltype"].value.indexOf(controlType) != -1) {
                    return docks[j];
                }
            }
        }
        return null;
    },

    _findDocksByReloadKey: function (myID, reloadKey) {
        var ctrlsToUpdate = [];
        jQuery("*[reloadKey='" + reloadKey + "']:visible").each(function () {
            if (this.id != myID) {
                var dock = $find(this.id);
                if (dock != null && Object.getTypeName(dock) == "Telerik.Web.UI.RadDock") {
                    ctrlsToUpdate.push(dock);
                }
            }
        });
        return ctrlsToUpdate;
    },

    _createXmlHttpRequest: function (dockInfo, commandName, successDelegate, failDelegate) {
        //var serializedDockInfo = Sys.Serialization.JavaScriptSerializer.serialize(dockInfo);
        //        if (serializedDockInfo) this._pendingRequestsQueue.push(serializedDockInfo);

        //        //if previous request is not finished we should wait till it finish because only one xmlhttppanel is used
        //        if (this._controlRequestStarted == true || this._pendingRequestsQueue.length <= 0) return;
        //        this._controlRequestStarted = true;

        //        //Current info which should be sent to server
        //        var serializedDockInfo = this._pendingRequestsQueue[0];

        //        this._pendingRequestsQueue.shift();

        //Set the webMethodName - so that service "knows" which server method to invoke - depending on edit mode
        var webMethodName = this._getCurrentWebMethodName();

        var url = this._webServiceUrl;
        if (url.charAt(url.length - 1) != "/") url += "/";
        url += webMethodName;

        var context = { WebMethodName: webMethodName, Command: commandName, EditMode: this.get_editMode() };

        this.get_clientManager().InvokePut(url, null, null, dockInfo, successDelegate, failDelegate, this, false, null, context);
        //this._invokeWebService(url+"/", serializedDockInfo, Function.createDelegate(this, this._onClientResponseEnding));
    },



    //============================= dispose logic ==========================================================================//
    _disposeElement: function (element, childNodesOnly) {
        if (!element) return;

        if (element.nodeType === 1) {
            var children = element.getElementsByTagName("*");
            for (var i = children.length - 1; i >= 0; i--) {
                this._disposeElementInternal(children[i]);
            }
            if (!childNodesOnly) {
                this._disposeElementInternal(element);
            }
        }
    },

    _disposeElementInternal: function (element) {
        var d = element.dispose;
        if (d && typeof (d) === "function") {
            element.dispose();
        }
        else {
            var c = element.control;
            if (c && typeof (c.dispose) === "function") {
                c.dispose();
            }
        }
    },


    //==============================Utility methods ==========================================================================//
    _serializeCollectionWCF: function (collectionObj) {
        var array = [];
        if (!collectionObj) return array;
        for (var item in collectionObj) {
            array[array.length] = { Key: item, Value: collectionObj[item] };
        }
        return array;
    },

    _deserializeCollectionWCF: function (array) {
        var obj = {};
        if (!array) return obj;
        for (var i = 0; i < array.length; i++) {
            var item = array[i];
            obj[item.Key] = item.Value;
        }
        return obj;
    },


    _restorePreviousState: function (jsonObject) {
        //Let the user know something happened
        alert(jsonObject.ErrorMessage);

        var dock = $find(jsonObject.DockId);

        //Undo the action
        var commandName = jsonObject.CommandName;
        if (commandName == "delete") {
            //Do nothing
        }
        else if (commandName == "indexchanged") {
            //Return to old index
            this._returnDockToOldPosition(dock);
        }
        else if (commandName == "new" || commandName == "duplicate") {
            //Destroy dock
            this._destroyDock(dock);
        }
    },

    _onEditWindowClose: function (sender, args) {
        //value will hold the command name
        sender.remove_close(this._windowClosedDelegate);
        var value = args.get_argument();
        var handled = this.get_lockingHandler().tryHandleError(value);
        if (handled == false) {
            this.execCommand(value, this._currentEditedDock);
        }
        this._currentEditedDock = null;
    },

    selectSegment: function (segmentName) {
        var zones = this._wrapperDockingZones;
        for (var i = 0; i < zones.length; i++) {
            var zone = $find(zones[i]);
            if (!zone) continue;

            //Process all docks
            var docks = zone.get_docks();
            for (var j = 0; j < docks.length; j++) {
                var dock = docks[j];
                var dockElement = dock.get_element();

                var personalizationDropDownPermission = dockElement.attributes["perm_" + this._personalizationDropDownCommand] ? dockElement.attributes["perm_" + this._personalizationDropDownCommand].value : null;

                if (personalizationDropDownPermission !== "False" && personalizationDropDownPermission !== "false") {

                    if (dockElement.attributes["widgetSegments"]) {
                        var segmentsAttribute = dockElement.attributes["widgetSegments"].value || dockElement.attributes["widgetSegments"];
                        var segments = jQuery.parseJSON(segmentsAttribute);

                        var segmentCommand = segments.filter(function (s) { return s.Text === segmentName; })[0];

                        if (!segmentCommand) segmentCommand = segments[segments.length - 1];

                        if (segmentCommand) {
                            var controlId = segmentCommand.CommandName;
                            if (dockElement.attributes["controlid"].value != controlId) {

                                dockElement.attributes["controlid"].value = controlId;
                                this.execCommand("reload", dock);
                                this._reloadInitiatedWithoutChanges[controlId] = true;
                            }
                        }
                    }
                }
            }
        }

    },

    _onPersonalizationWindowClose: function (sender, args) {
        if (typeof sender.remove_close !== 'undefined')
            sender.remove_close(this._personalizationWindowClosedDelegate);

        var handled = this.get_lockingHandler().tryHandleError(args._argument);
        if (args._argument && args._argument.Id && handled === false && args._argument.Id != Telerik.Sitefinity.getEmptyGuid()) {
            this._getPersonalizationDropDownCommandElement(this._currentEditedDock).show();
            this._currentEditedDock.get_element().attributes["controlid"].value = args._argument.Id;

            this.execCommand("reload", this._currentEditedDock);
            this.raiseEvent("personalizationSegmentAdded", { segmentName: args._argument.SegmentName, mediaType: this._mediaType });
            this.raiseEvent("personalizationSegmentChanged", args._argument.SegmentName);
            this.execCommand("edit", this._currentEditedDock);
        }
        else {
            this._currentEditedDock = null;
        }
    },

    _addPersonalizedVersion: function (dock) {
        var dockElement = dock.get_element();
        var dockID = dockElement.attributes["controlid"].value;
        var url = this._segmentSelectorUrl;

        url = String.format("{0}?Id={1}&PageId={2}",
                                           url,
                                           dockID,
                                           this._pageId);

        //Set a property - keep the dock so that execCommand can be called when the window closes
        this._currentEditedDock = dock;


        if (!dockElement.attributes["designerUrl"]) {
            //Open Radwindow through RadWindowManager.open method
            var manager = this._getWindowManager();

            //Back reference to the zone eidtor
            var zoneEditor = this;
            manager.get_zoneEditor = function () { return zoneEditor; }
            var oWnd = manager.open(url, "SegmentSelectorDialog");
            oWnd.add_close(this._personalizationWindowClosedDelegate);
            oWnd.set_visibleStatusbar(false);
            //Width & Height auto size behaviours
            oWnd.set_autoSizeBehaviors(1 + 4);
            //Close behavoiur
            oWnd.set_behaviors(4);
            oWnd.set_keepInScreenBounds(false);
            oWnd.set_width(425);
            oWnd.set_height(250);
            oWnd.set_cssClass("sfpeDesigner");

            Telerik.Sitefinity.centerWindowHorizontally(oWnd);
        }
        else {//raise event to open custom dialog
            if ($telerik && $telerik.$) {
                var modalArgs = this.NeedsModalArgs(dockElement.getAttribute("designerUrl"), dockID, dockElement.getAttribute("moduleName"), dockElement.getAttribute("behaviourObjectType"), dockElement.getAttribute("openAdminAppEditor"));
                $telerik.$(document).trigger("needsPersonalizationModalDialog", [modalArgs]);
            }

            if (typeof CustomEvent == "function") {
                var sfEvt = new CustomEvent("sfModalDialogOpened");
                sfEvt.detail = null;
                document.dispatchEvent(sfEvt);
            }
        }
    },

    _removePersonalizedVersion: function (dock) {
        var that = this;
        var serviceUrl = this._controlPersonalizationsService;
        var dockElement = dock.get_element();
        var controlId = dockElement.attributes["controlid"].value;
        var _data = {
            "ControlId": controlId,
            "PageId": this._pageId
        }

        $.ajax({
            type: 'DELETE',
            dataType: "json",
            url: serviceUrl,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(_data),
            success: function (newId) {
                if (newId !== "00000000-0000-0000-0000-000000000000") {
                    dockElement.attributes["controlid"].value = newId;
                    that.execCommand("reload", dock);

                    segments = jQuery.parseJSON(dock.get_element().attributes["widgetSegments"].value);

                    var deletedSegment = segments.filter(function (s) { return s.CommandName === controlId })[0];
                    if (deletedSegment) {
                        that.raiseEvent("personalizationSegmentRemoved", { segmentNames: [deletedSegment.Text], mediaType: that._mediaType });
                    }

                    segments = segments.filter(function (s) { return s.CommandName != controlId });

                    dock.get_element().setAttribute("widgetSegments", JSON.stringify(segments));

                    var personalizationCommandElement = that._getPersonalizationDropDownCommandElement(dock);
                    if (personalizationCommandElement) {
                        if (!(segments && segments.length > 1)) {
                            personalizationCommandElement.hide();
                        }

                        var newSegment = segments.filter(function (s) { return s.CommandName == newId; })[0];
                        if (newSegment) {
                            personalizationCommandElement.text(newSegment.Text);
                        } else {
                            personalizationCommandElement.text("");
                        }
                    }
                }
            },
            error: function (arg) {
                alert("There was a problem!");
            }
        });
    },

    _editItem: function (dock) {        

        var dockElement = dock.get_element();
        var dockID = dockElement.attributes["controlid"].value;

        if (this._editMode == "Controls" || this._editMode == "Settings" || this._editMode == "Themes") {
            //Create the url depending on the RadDock's properties

            var url = dockElement.attributes["designerUrl"] ? dockElement.attributes["designerUrl"].value : this._propertyEditorUrl;
            url = String.format("{0}?Id={1}&MediaType={2}&PageId={3}&propertyValueCulture={4}&hideSaveAllTranslations={5}",
                                            url,
                                            dockID,
                                            this._mediaType,
                                            this._pageId,
                                            this.get_currentLanguage(),
                                            this.get_hideSaveAllTranslations());

            //Set a property - keep the dock so that execCommand can be called when the window closes
            this._currentEditedDock = dock;

            if (!dockElement.attributes["designerUrl"]) {

                //Open Radwindow through RadWindowManager.open method
                var manager = this._getWindowManager();

                //Back reference to the zone eidtor
                var zoneEditor = this;
                manager.get_zoneEditor = function () { return zoneEditor; }
                var oWnd = manager.open(url, "PropertyEditorDialog");
                oWnd.add_close(this._windowClosedDelegate);
                oWnd.set_visibleStatusbar(false);
                //Width & Height auto size behaviours
                oWnd.set_autoSizeBehaviors(1 + 4);
                //Close behavoiur
                oWnd.set_behaviors(4);
                oWnd.set_keepInScreenBounds(false);
                oWnd.set_width(425);
                oWnd.set_height(250);
                oWnd.set_cssClass("sfpeDesigner");

                Telerik.Sitefinity.centerWindowHorizontally(oWnd);
            }
            else {//raise event to open custom dialog
                if ($telerik && $telerik.$) {
                    var modalArgs = this.NeedsModalArgs(dockElement.getAttribute("designerUrl"), dockID, dockElement.getAttribute("moduleName"), dockElement.getAttribute("behaviourObjectType"), dockElement.getAttribute("openAdminAppEditor"));
                    this._dispatchNeedsModalEvent(document, modalArgs);
              }
            }

        } else if (this._editMode == "Layouts") {
            //Get the control container   
            var controlContainer = dockElement; //sender.get_currentLayoutControlContainer();
            //Call layoutEditor initialize method
            var layoutRoot = $(controlContainer).find(".sf_cols").get(0); // .getChildByClassName(controlContainer, "sf_cols");
            var layoutContainer = $(controlContainer).find(".rdContent").get(0);
            if (typeof (layoutContainer) == 'undefined')
                layoutContainer = $(layoutRoot).parent();


            if (!dockElement.attributes["designerUrl"]) {
                this._getLayoutEditor().SetUpEditor(layoutRoot, layoutContainer, dockID, this._mediaType, dock, this._pageId);
            }
            else if ($telerik && $telerik.$) {
                var modalArgs = this.NeedsGridModalArgs(dockElement.getAttribute("designerUrl"), dockID, layoutRoot, layoutContainer, dock);
                $telerik.$(document).trigger("needsGridModalDialog", [modalArgs]);

                if (typeof CustomEvent == "function") {
                    var sfEvt = new CustomEvent("sfModalDialogOpened");
                    sfEvt.detail = null;
                    document.dispatchEvent(sfEvt);
                }
            }
        }
    },

    //Arguments required by bootstrap modal dialog
    NeedsModalArgs: function (url, dockID, moduleName, controlType, openNewEditor) {
        var modalArgs = {
            url: url,
            openNewEditor: openNewEditor === "true",
            Id: dockID,
            MediaType: this._mediaType,
            PageId: this._pageId,
            PropertyValueCulture: this.get_currentLanguage(),
            HideSaveAllTranslations: this.get_hideSaveAllTranslations(),
            PropertyServiceUrl: this.get_propertyServiceUrl(),
            AppPath: this.get_appPath(),
            isMultiPageForm: this._isMultipageForm(),
            ModuleName: moduleName
        };

        return modalArgs;
    },

    //Arguments required by bootstrap modal dialog for editing layout widget
    NeedsGridModalArgs: function (url, dockID, layoutRoot, layoutContainer, dock) {
        var modalArgs = {
            url: url,
            Id: dockID,
            MediaType: this._mediaType,
            PageId: this._pageId,
            AppPath: this.get_appPath(),
            LayoutRoot: layoutRoot,
            LayoutContainer: layoutContainer,
            Dock: dock,
            GridTitle: dock.get_title(),
            ZoneEditorId: this.get_element().id
        };

        return modalArgs;
    },

    //NEW:Return a reference to the actual root layout DIV
    get_currentLayoutControlContainer: function () {
        if (this._currentEditedDock) {
            var container = this._currentEditedDock.get_contentContainer();
            return container;
        }
        return null;
    },


    _configureZoneCss: function (zone) {
        if (!zone) return;

        var zoneElement = zone.get_element();
        //If the zone is without docks the placeholder will not be visible
        if (zone.get_docks().length <= 0) {
            Sys.UI.DomElement.addCssClass(zoneElement, "zeDockZoneEmpty");
        } else {
            Sys.UI.DomElement.removeCssClass(zoneElement, "zeDockZoneEmpty");
        }
    },

    _showLoadingPanel: function (dock) {
        var titleBar = dock.get_titleBar();
        //controls shared from a template may not have the titlebar
        if (titleBar) {
            //Make sure the loading image is just set once
            var loadingDiv = $telerik.getElementByClassName(titleBar, "zeLoadingPanel", "span");

            if (!loadingDiv) {
                //create a div and show it instead the titlebarText;
                var loadingDiv = document.createElement("span");
                loadingDiv.className = "zeLoadingPanel";
                titleBar.appendChild(loadingDiv);
            }

            var titlebartext = titleBar.getElementsByTagName("em")[0];
            titlebartext.style.display = "none";
        }
    },

    _hideLoadingPanel: function (dock) {
        //hide the loading div and show the titlebarText;
        var titleBar = dock.get_titleBar();
        //controls shared from a template may not have the titlebar
        if (titleBar) {
            var loadingImg = $telerik.getElementByClassName(titleBar, "zeLoadingPanel");
            if (loadingImg) loadingImg.style.display = "none";

            var titlebartext = titleBar.getElementsByTagName("em")[0];
            titlebartext.style.display = "block";
        }
    },


    //_onDockDrag is set to both toolbox and non-toolbox docks
    _onDockDrag: function (dock, args) {
        //Configure zone css if the dock is over a dockZone or if it leave it

        var hitZone = dock._hitZone;
        var previousHitZone = dock._previousHitZone;
        if (hitZone) {
            this._configureZoneCss(hitZone);
            dock._previousHitZone = hitZone;
        }
        if (previousHitZone != null && previousHitZone != hitZone) {
            this._configureZoneCss(previousHitZone);
        }
    },

    //Called when started dragging a control from within a dock
    _onDockStartDrag: function (dock, args) {
        dock._previousHitZone = dock.get_dockZone();
    },
    _onEnterZone: function (dock, args) {
        if (args.dockZone) {
            var previousCurrents = document.getElementsByClassName("sfCurrentDragZone");

            for (var i = 0 ; i < previousCurrents.length; i++) {
                previousCurrents[i].classList.remove("sfCurrentDragZone");
            }

            args.dockZone._element.classList.add("sfCurrentDragZone");
        }
    },

    //Called when finished dragging a control from within a dock
    _onDockEndDrag: function (dock, args) {
        this._configureZoneCss(dock.get_dockZone());

        Sys.UI.DomElement.removeCssClass(dock.get_element(), "zeOrphanedDock");
    },

    _onWrapperDockCommand: function (dock, args) {
        var command = args.command;
        var commandName = command.get_name();

        if (commandName == "dropDownCommand") {
            var commandElement = command.get_element();
            var commandMenu = this._getCommandMenu();
            if (commandMenu) {
                //commandMenu.get_visible() at present does not work! It uses an invisible root element to determine visibility and always returns false.
                if (commandMenu.__curentDock == dock && Sys.UI.DomElement.getVisible(commandMenu.get_contextMenuElement())) {
                    commandMenu.hide();
                }
                else {
                    window.setTimeout(Function.createDelegate(this, function () { this._showContextMenu(commandElement, dock) }), 100);
                }
            }
        }
            //Else just let execCommand handle the case
        else this.execCommand(commandName, dock);
    },

    _showPersonalizationMenu: function (element, dock) {
        var personalizationMenu = this._getPersonalizationMenu(),
            dockElement = dock.get_element(),
            widgetSegments = dockElement.attributes["widgetSegments"],
            controlId = dockElement.attributes["controlid"],
            widgetSegment = null,
            menu = this._getPersonalizationMenu();

        menu.get_items().clear();

        if (widgetSegments && widgetSegments.value) {
            widgetSegments = jQuery.parseJSON(widgetSegments.value);
            if (widgetSegments.length > 0) {
                for (var i = 0 ; i < widgetSegments.length; i++) {
                    widgetSegment = widgetSegments[i];
                    var menuItem = new Telerik.Web.UI.RadMenuItem();
                    menuItem.set_value(widgetSegment.CommandName);
                    menuItem.set_navigateUrl(widgetSegment.ActionUrl);
                    menuItem.set_cssClass(widgetSegment.CssClass);
                    menuItem.set_text(widgetSegment.Text);
                    var attributes = menuItem.get_attributes();
                    attributes.setAttribute("needsModal", widgetSegment.NeedsModal);
                    personalizationMenu.get_items().add(menuItem);
                    if (controlId && controlId.value === widgetSegment.CommandName) {
                        $(menuItem.get_element()).addClass('sfSel');
                    }
                }

                personalizationMenu.__curentDock = dock;
                var bounds = $telerik.getBounds(element);
                personalizationMenu.showAt(bounds.x, bounds.y + bounds.height);
            }
            if (!$(menu.get_contextMenuElement()).find('strong.sfPersonalizationMenuTitle').length) {
                $(menu.get_contextMenuElement()).prepend('<strong class="sfPersonalizationMenuTitle sfMoreContextMenuTitle">' + this._personalizedForText + '</strong>');
            }
        }
    },

    _showContextMenu: function (element, dock) {
        var commandMenu = this._getCommandMenu();
        commandMenu.get_items().clear();

        //Hide permissions menu for layout controls and when editing a form
        var showPermissions = (this.get_editMode() != "Layouts" && this._mediaType != 2);

        //Create all commands
        //Get commands which should be created
        var widgetCommands = jQuery.parseJSON(dock.get_element().getAttribute("widgetCommands"));
        if (widgetCommands) {
            for (var i = 0 ; i < widgetCommands.length; i++) {
                if (showPermissions == false && widgetCommands[i].CommandName == "permissions") continue;

                var attrName = "perm_" + widgetCommands[i].CommandName;
                var elem = dock.get_element();
                var val = elem.getAttribute(attrName);
                if (val != null) {
                    val = val ? val.toLowerCase() : "false";
                    if (val == "false") continue;
                }

                var menuItem = new Telerik.Web.UI.RadMenuItem();
                menuItem.set_value(widgetCommands[i].CommandName);
                menuItem.set_navigateUrl(widgetCommands[i].ActionUrl);

                var cssClass = widgetCommands[i].CssClass;
                if (widgetCommands[i].CommandName === 'addPersonalizedVersion' && elem.getAttribute('perm_removePersonalizedVersion') === 'False') {
                    cssClass += ' sfSeparatorDown';
                }

                menuItem.set_cssClass(cssClass);
                menuItem.set_text(widgetCommands[i].Text);
                var attributes = menuItem.get_attributes();
                attributes.setAttribute("needsModal", widgetCommands[i].NeedsModal);
                commandMenu.get_items().add(menuItem);
            }
        }
        else {
            var commandsToDisplay = this.get_commands();
            for (var item in commandsToDisplay) {
                var menuItem = new Telerik.Web.UI.RadMenuItem();
                var itemKey = item.split(',', 2);
                if (showPermissions == false && itemKey[0] == "permissions") continue;

                var attrName = "perm_" + itemKey[0];
                var elem = dock.get_element();
                var val = elem.getAttribute(attrName);
                val = val ? val.toLowerCase() : "false";
                if (val == "false") continue;

                var cssClass = itemKey[1];
                if (itemKey[0] === 'addPersonalizedVersion' && elem.getAttribute('perm_removePersonalizedVersion') === 'False') {
                    cssClass += ' sfSeparatorDown';
                }

                menuItem.set_value(itemKey[0]);
                menuItem.set_cssClass(cssClass);
                menuItem.set_text(commandsToDisplay[item]);
                commandMenu.get_items().add(menuItem);
            }
        }

        if ($telerik && $telerik.$) {
            $telerik.$(document).trigger("commandMenuItemsCreated", [commandMenu]);
        }

        commandMenu.__curentDock = dock;
        var bounds = $telerik.getBounds(element);
        commandMenu.showAt(bounds.x, bounds.y + bounds.height);
    },

    _onContextMenuClick: function (sender, args) {
        var item = args.get_item();
        //Get exact dock that triggered the menu
        var dock = sender.__curentDock;
        var dockElement = dock.get_element();
        var dockID = dockElement.attributes["controlid"].value;

        var navigateUrl = item.get_navigateUrl();
        var attributes = item.get_attributes();
        var needsModal = attributes.getAttribute("needsModal");
        if (needsModal && navigateUrl && navigateUrl != "#") {
            this._currentEditedDock = dock;

            if ($telerik && $telerik.$) {
                var modalArgs = this.NeedsModalArgs(navigateUrl, dockID, null, dockElement.getAttribute("behaviourObjectType"), dockElement.getAttribute("openAdminAppEditor"));
                this._dispatchNeedsModalEvent(document, modalArgs);
            }

            args.get_domEvent().preventDefault();
        }

        var commandName = args.get_item().get_value();

        //Call execCommand to handle the command
        this.execCommand(commandName, dock);
    },

    _onPersonalizationMenuClick: function (sender, args) {
        var item = args.get_item();
        //Get exact dock that triggered the menu
        var dock = sender.__curentDock;
        var dockElement = dock.get_element();

        var controlid = item.get_value();
        dockElement.attributes["controlid"].value = controlid;
        this.execCommand("reload", dock);
        this._reloadInitiatedWithoutChanges[controlid] = true;

        this._getPersonalizationDropDownCommandElement(dock).text(item.get_text());
        this.raiseEvent("personalizationSegmentChanged", item.get_text().trim());
    },

    _dispatchNeedsModalEvent: function (element, args) {
        if (typeof CustomEvent == "function") {
            var evt = new CustomEvent('needsModalDialog');
            element.dispatchEvent(evt);

            var sfEvt = new CustomEvent('sfModalDialogOpened');
            document.dispatchEvent(sfEvt);
        } else if (!element.createEvent) {    // If IE < 9 event won't be fired
            // DO NOTHING!
        } else {    // If CuntomEvent object is not supported (e.g. IE9) -> create polyfil
            // CustomEvent polyfil
            var CustomEvent = function (event, params) {
                params = params || { bubbles: true, cancelable: true, detail: args };
                var evt = element.createEvent('CustomEvent');
                evt.initCustomEvent(event, params.bubbles, params.cancelable, params.detail);
                return evt;
            };
            CustomEvent.prototype = window.Event.prototype;
            window.CustomEvent = CustomEvent;
            // CustomEvent polyfil - END
            if (typeof CustomEvent == "function") {
                var evt = new CustomEvent("needsModalDialog", {detail: args,bubbles: true});
                element.dispatchEvent(evt);

                var sfEvt = new CustomEvent('sfModalDialogOpened');
                document.dispatchEvent(sfEvt);
            }
        }
    },

    _destroyDock: function (dock) {
        //Always dispose of this dock - it was just for temp purposes
        dock.set_closed(true);
        dock.undock(); //Ugly hack - otherwise the dock can be showed again if you drop the same dock
        dock.dispose();
        //TODO: Need to dispose of its content [controls] as well!
    },

    // TEMPORARY WORKAROUND FOR RadDock.clone. USE THIS INSTEAD OF RadDock.clone!
    _cloneDock: function (dock, id) {
        //Set unique ID to the dock
        if (!id) id = "RadDockClone_" + (new Date() - 100);

        //Dispose all browser event handlers, because in IE when cloning a HTML element, its events are cloned as well

        var handle = dock.get_handle();

        //Dispose dragging

        dock._setHandle(null);
        //Dispose command's handlers
        var commands = dock.get_commands();
        if (this._checkCommands(commands)) {
            for (var name in commands) $clearHandlers(commands[name].get_element());
        }

        //Clone the dock HTML element
        var element = dock.get_element();

        var newDockElement = element.cloneNode(true);

        //Make the necessary cnahges to the cloned element
        if (!newDockElement.getAttribute("control")) {
            newDockElement.removeAttribute("control");
        }

        //Get a reference to the title bar
        var rows = $telerik.getElementByClassName(newDockElement, "rdTable", "TABLE").rows;
        var title = $telerik.getElementByClassName(rows[0].cells[1], "rdTitleBar", "DIV");
        //Get a reference to the content area
        var content = $telerik.getElementByClassName(rows[1].cells[1], "rdContent", "DIV");
        //Get a reference to the hidden input which is used to store the raddock's state
        var clientHiddenInput = newDockElement.getElementsByTagName("input")[0];

        //Change title's id to use the cloned dock's id
        title.setAttribute("id", id + "_T");
        //Change content's id to use the cloned dock's id
        content.setAttribute("id", id + "_C");
        //Change dock's element id to use the cloned dock's id
        newDockElement.setAttribute("id", id);
        //Change hidden's element to use the cloned dock's id
        clientHiddenInput.setAttribute("id", id + "_ClientState");
        clientHiddenInput.setAttribute("name", id + "_ClientState");

        //add dock's element to the page - after the original dock            
        element.parentNode.insertBefore(newDockElement, element.nextSibling);
        //clone original dock's object
        var obj = $telerik.cloneControl(dock, Telerik.Web.UI.RadDock, newDockElement);
        delete obj._observerContext;

        //Re-enable dragging to the original dock
        dock._setHandle(handle);

        //Clear the .control reference from the commands the dock commands of the new dock!            
        if (obj.get_commandsContainer()) {
            var commandElements = obj.get_commandsContainer().getElementsByTagName("a");
            for (var i = 0; i < commandElements.length; i++) {
                var element = commandElements[i];
                //Remove control-it is copied when the element is cloned
                element.removeAttribute("control");
            }
        }

        obj.set_commands(Array.clone(dock._originalCommandsObject));
        obj._initializeCommands();

        // atach handlers to the original RadDock's commands
        if (this._checkCommands(commands)) {
            for (var command in commands) {
                var comObj = commands[command];
                var comElem = commands[command].get_element();
                $addHandlers(comElem, { "click": comObj.onCommand, "mousedown": comObj.onMouseDown }, comObj);
            }
        }

        //uniqueID is the old one,and it should be changed
        obj.set_uniqueID(id);
        //change the index for each raddock in the zone
        if (obj.get_dockZone()) obj.get_dockZone()._resetDockIndices();

        return obj;
    },

    // TEMPORARY WORKAROUND FOR RadDockZone.clone. USE THIS INSTEAD OF RadDockZone.clone!
    _cloneDockZone: function (zone, id) {
        if (!id) id = "RadDockZoneClone_" + (new Date() - 100);

        //Clone the RadDockZone HTML element
        var element = zone.get_element();

        var newZoneElement = element.cloneNode(true);
        newZoneElement.setAttribute("id", id);
        //Remove control-it is copied when the element is cloned
        if (!newZoneElement.getAttribute("control")) {
            newZoneElement.removeAttribute("control");
        }

        //Remove all cloned RadDocks from the zone
        newZoneElement.innerHTML = "";

        //Create PlaceHolder element and add it to the zoneElement
        var placeHolderElement = zone._placeholder.cloneNode(true);
        placeHolderElement.setAttribute("id", id + "_D");
        newZoneElement.appendChild(placeHolderElement);

        //Create ClientState element and add it to the zoneElement
        var clientStateElement = $get(zone._clientStateFieldID).cloneNode(true);
        clientStateElement.setAttribute("id", id + "_ClientState");
        newZoneElement.appendChild(clientStateElement);

        //The clear element is available only in HorizontalZone
        var clearElement;
        if (zone._clearElement) {
            clearElement = zone._clearElement.cloneNode(true);
            clearElement.setAttribute("id", id + "_C");
            newZoneElement.appendChild(clearElement);
        }

        //add dockZone's element to the page - after the original dockZone
        element.parentNode.insertBefore(newZoneElement, element.nextSibling);

        //clone original dockZone's object
        var clonedZone = $telerik.cloneControl(zone, Telerik.Web.UI.RadDockZone, newZoneElement);
        //Set proper properties
        clonedZone._uniqueName = id;
        clonedZone._placeholder = placeHolderElement;
        if (clearElement) clonedZone._clearElement = clearElement;

        return clonedZone;
    },

    // This is help method - checks if the "commands" variable is object or valid array for the iteration (not empty)
    _checkCommands: function (commands) {
        var valid = commands && (!jQuery.isArray(commands) || (commands.length && commands.length > 0));
        return valid;
    },

    _removeAllModeClasses: function () {
        var bodyQuery = jQuery(document.body);
        bodyQuery.removeClass('zeContentMode');
        bodyQuery.removeClass('zeThemesMode');
        bodyQuery.removeClass('zeLayoutMode');
        bodyQuery.removeClass('zeSettingsMode');
    },

    _showZonePermanentLabel: function (zone) {
        var zoneElement = $(zone.get_element());
        var zoneLabel = zoneElement.find('[data-sf-role="zone-label"]');
        if (zoneLabel.length === 0) {
            var zoneText = $(zoneElement.find('.zeDockZoneLabel').get(0)).text();
            var elementToPrependTheLabel = $(zoneElement.find('.emptyZoneDraggingText').get(0));
            $('<span data-sf-role="zone-label" class="sfZoneLabel">' + zoneText + '</span>').insertBefore(elementToPrependTheLabel);
        }
    },

    _showPlaceholders: function (placeholderNames) {
        var zones = placeholderNames;
        for (var i = 0; i < zones.length; i++) {
            var zone = $find(zones[i]);
            if (!zone) continue;

            var zoneElement = $(zone.get_element());
            if (!zoneElement.is(":visible")) {
                var pageContainer = $('#sfPageContainer').eq(0);
                var currentScroll = pageContainer.scrollTop();

                zoneElement.show();
                this._showZonePermanentLabel(zone);
                this._configureZoneCss(zone);

                if (zone.get_clientID() === this._headerPlaceholderName) {
                    var newScroll = ($(zone._element).outerHeight(true) || 0) + currentScroll;
                    pageContainer.scrollTop(newScroll);
                }
            }
        }
    },

    _hidePlaceholders: function (placeholderNames) {
        var zones = placeholderNames;

        var bodyZone = $find(this._bodyPlaceholderName);
        if (!bodyZone) return;

        var movedDocks = [];
        for (var i = 0; i < zones.length; i++) {
            var zone = $find(zones[i]);
            if (!zone) continue;

            var insertIndex = null;
            var docks = zone.get_docks();
            movedDocks = movedDocks.concat(docks);
            if (zone.get_clientID() === this._headerPlaceholderName) {
                docks = docks.reverse();
                insertIndex = 0;
            }

            for (var j = 0; j < docks.length; j++) {
                this._onWrapperDockPositionChanging(docks[j], {});

                docks[j].undock();

                if (insertIndex !== null) {
                    bodyZone.dock(docks[j], insertIndex);
                } else {
                    bodyZone.dock(docks[j]);
                }
            }

            var zoneElement = $(zone.get_element());
            zoneElement.hide();
        }

        this._configureZoneCss(bodyZone);
        this._onMultipleWrappersDockPositionChanged(movedDocks);
    },

    _isMultipageForm: function () {
        var that = this;

        var zones = this._allowedPageBreakPlaceholders;
        for (var i = 0; i < zones.length; i++) {
            var zone = $find(zones[i]);
            if (!zone) continue;

            var docks = zone.get_docks();
            var pageBreakFields = docks.filter(function (d) { return that._isDockPageBreakField(d); });

            if (pageBreakFields.length > 0) {
                return true;
            }
        }

        return false;
    },

    _updatePagesLabelsOnTheForm: function () {
        var that = this;

        var zones = this._allowedPageBreakPlaceholders;
        for (var i = 0; i < zones.length; i++) {
            var zone = $find(zones[i]);
            if (!zone) continue;

            $(zone.get_element()).find('[data-sf-role="page-label"]').remove();

            var docks = zone.get_docks();
            var pageBreakFields = docks.filter(function (d) { return that._isDockPageBreakField(d); });
            var pagesLength = pageBreakFields.length + 1;
            if (pageBreakFields.length > 0) {
                $('<span data-sf-role="page-label" class="sfPageNumber">' + 1 + ' of ' + pagesLength + '</span>').insertBefore(docks[0].get_element());
            }

            for (var j = 0; j < pageBreakFields.length; j++) {
                $('<span data-sf-role="page-label" class="sfPageNumber">' + (j + 2) + ' of ' + pagesLength + '</span>').insertAfter(pageBreakFields[j].get_element());
            }
        }
    },

    _isDockPageBreakField: function (dock) {
        var isPageBreakAttr = dock.get_element().getAttribute("isPageBreak");
        return isPageBreakAttr === "True" || isPageBreakAttr === "true";
    },

    get_currentLanguage: function () {
        return this._currentLanguage;
    },

    set_currentLanguage: function (value) {
        this._currentLanguage = value;
    },

    get_propertyServiceUrl: function () {
        return this._propertyServiceUrl;
    },

    set_propertyServiceUrl: function (value) {
        this._propertyServiceUrl = value;
    },

    get_selectedTemplate: function () {
        return this._selectedTemplate;
    },
    set_selectedTemplate: function (value) {
        this._selectedTemplate = value;
    },

    get_templateFieldControl: function () {
        return this._templateFieldControl;
    },
    set_templateFieldControl: function (value) {
        this._templateFieldControl = value;
    },

    //======================== getXXXControl methods  ===================================================================//
    _getWindowManager: function () {
        return $find(this._windowMangerId);
    },

    _getCommandMenu: function () {
        return $find(this._commandMenuId);
    },

    _getPersonalizationMenu: function () {
        return $find(this._personalizationMenuId);
    },

    _getPersonalizationDropDownCommandElement: function (dock) {
        var selector = '.' + this._personalizationDropDownCommand;
        return $($('#' + dock.get_id()).find(selector)[0]);
    },

    _getTabStrip: function () {
        return $find(this._tabStripId);
    },

    _getLayoutEditor: function () {
        return $find(this._layoutEditorId);
    },

    _getThemesEditor: function () {
        return $find(this._themesEditorId);
    },
    getSettingsEditor: function () {
        return $find(this._settingsEditorId);
    },

    get_clientManager: function () {
        if (this._clientManager == null) {
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
            if (this.get_currentLanguage()) {
                this._clientManager.set_uiCulture(this.get_currentLanguage());
            }
        }
        return this._clientManager;
    },

    //================ Public properties needed for the serverside descriptor.AddScriptProperty to work properly==============//
    get_wrapperDockingZones: function () {
        return this._wrapperDockingZones;
    },

    set_wrapperDockingZones: function (value) {
        this._wrapperDockingZones = value;
    },

    get_lockingHandler: function () {
        return this._lockingHandler;
    },

    set_lockingHandler: function (value) {
        this._lockingHandler = value;
    },

    get_toolboxDockingZones: function () {
        return this._toolboxDockingZones;
    },

    set_toolboxDockingZones: function (value) {
        this._toolboxDockingZones = value;
    },


    get_commands: function () {
        return this._commands;
    },

    set_commands: function (value) {
        this._commands = value;
    },

    get_appPath: function () {
        return this._appPath;
    },

    set_appPath: function (value) {
        this._appPath = value;
    },

    get_basePageServiceUrl: function () {
        return this._basePageServiceUrl;
    },

    set_basePageServiceUrl: function (value) {
        this._basePageServiceUrl = value;
    },

    //A setter and getter are needed because this prop is registered from the server with AddComponentProperty
    get_controlWrapperFactory: function () {
        return this._controlWrapperFactory;
    },

    set_controlWrapperFactory: function (value) {
        this._controlWrapperFactory = value;
    },

    get_emptyControlIds: function () {
        return this._emptyControlIds;
    },
    set_emptyControlIds: function (value) {
        this._emptyControlIds = value;
    },

    get_layoutWrapperFactory: function () {
        return this._layoutWrapperFactory;
    },

    set_layoutWrapperFactory: function (value) {
        this._layoutWrapperFactory = value;
    },


    get_editMode: function () {
        return this._editMode;
    },

    set_editMode: function (val) {
        var body = document.body;

        if (val != "EditPlainText") {
            this._removeAllModeClasses();
            switch (val) {
                case "Layouts":
                    $(body).addClass('zeLayoutMode');
                    break;
                case "Controls":
                    $(body).addClass('zeContentMode');
                    break;
                case "Themes":
                    $(body).addClass('zeThemesMode');
                    break;
                case "Settings":
                    $(body).addClass('zeSettingsMode');
                    break;
            }
        }

        //EXTRA code for IE6 and IE7 Call a special method that will go through all docks and hide those     
        if ($telerik.isIE6 || $telerik.isIE7) {
            var tables = document.getElementsByTagName("TABLE");
            for (var i = 0; i < tables.length; i++) {
                var table = tables[i];
                if (table.className == "rdTable") {
                    table.style.display = "block";
                    table.style.display = "";
                }
            }
        }

        if (val == this._editMode) return;

        //Set the mode so that the ZoneEditor knows which web-method to invoke and which dock factory ro clone
        this._editMode = val;

        //If the tab selected node is different - change it
        var tabstrip = this._getTabStrip();
        if (tabstrip) {
            var tab = tabstrip.get_selectedTab();
            if (tab.get_value() != val) {
                //Find the tab and change it
                var newTab = tabstrip.findTabByValue(val);
                if (newTab) newTab.set_selected(true);
            }
        }
    },

    get_currentTemplateId: function () {
        return this._currentTemplateId;
    },
    set_currentTemplateId: function (value) {
        this._currentTemplateId = value;
    },

    //========================== Client-side control events ============================================================//
    add_responseEnding: function (handler) {
        this.get_events().addHandler("responseEnding", handler);
    },

    remove_responseEnding: function (handler) {
        this.get_events().removeHandler("responseEnding", handler);
    },

    add_command: function (handler) {
        this.get_events().addHandler("command", handler);
    },

    remove_command: function (handler) {
        this.get_events().removeHandler("command", handler);
    },

    add_commandSuccess: function (handler) {
        this.get_events().addHandler("commandSuccess", handler);
    },

    remove_commandSuccess: function (handler) {
        this.get_events().removeHandler("commandSuccess", handler);
    },

    add_personalizationSegmentAdded: function (handler) {
        this.get_events().addHandler("personalizationSegmentAdded", handler);
    },

    remove_personalizationSegmentAdded: function (handler) {
        this.get_events().removeHandler("personalizationSegmentAdded", handler);
    },

    add_personalizationSegmentRemoved: function (handler) {
        this.get_events().addHandler("personalizationSegmentRemoved", handler);
    },

    remove_personalizationSegmentRemoved: function (handler) {
        this.get_events().removeHandler("personalizationSegmentRemoved", handler);
    },

    add_personalizationSegmentChanged: function (handler) {
        this.get_events().addHandler("personalizationSegmentChanged", handler);
    },

    remove_personalizationSegmentChanged: function (handler) {
        this.get_events().removeHandler("personalizationSegmentChanged", handler);
    }
};


Telerik.Sitefinity.Web.UI.ZoneEditor.registerClass('Telerik.Sitefinity.Web.UI.ZoneEditor', Telerik.Web.UI.RadWebControl);

//overrides evalScripts in order to exclude running of kendo templates
(function () {
    $telerik.evalScripts = function (b, a) {
        $telerik.registerSkins(b);
        var g = jQuery(b).find("script[type!='text/x-kendo-tmpl']");
        var j = 0, h = 0;
        var e = function (n, o) {
            if (n - h > 0 && ($telerik.isIE || $telerik.isSafari)) {
                window.setTimeout(function () {
                    e(n, o);
                }, 5);
            } else {
                var i = document.createElement("script");
                i.setAttribute("type", "text/javascript");
                document.getElementsByTagName("head")[0].appendChild(i);
                i.loadFinished = false;
                i.onload = function () {
                    if (!this.loadFinished) {
                        this.loadFinished = true;
                        h++;
                    }
                };
                i.onreadystatechange = function () {
                    if ("loaded" === this.readyState && !this.loadFinished) {
                        this.loadFinished = true;
                        h++;
                    }
                };
                i.setAttribute("src", o);
            }
        };
        var k = [];
        for (var c = 0, d = g.length; c < d; c++) {
            var f = g[c];
            if (f.src) {
                var m = f.getAttribute("src", 2);
                if (!$telerik.isScriptRegistered(m, b)) {
                    e(j++, m);
                }
            } else {
                Array.add(k, f.innerHTML);
            }
        }
        var l = function () {
            if (j - h > 0) {
                window.setTimeout(l, 20);
            } else {
                for (var i = 0;
                i < k.length;
                i++) {
                    $telerik.evalScriptCode(k[i]);
                }
                if (a) {
                    a();
                }
            }
        };
        l();
    };
})();
