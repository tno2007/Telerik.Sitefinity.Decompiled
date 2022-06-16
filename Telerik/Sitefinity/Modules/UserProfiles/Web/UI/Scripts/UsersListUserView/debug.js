/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type._registerScript("ContentSelectorsDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Security.Web.UI");

Telerik.Sitefinity.Security.Web.UI.UsersListUserView = function (element) {
    Telerik.Sitefinity.Security.Web.UI.UsersListUserView.initializeBase(this, [element]);

    this._clientLabelManager = null;
	this._parentDesigner = null;
	this._profileTypeSelector = null;
	this._oneUserOnlySelectButtonPanel = null;
	this._rolesPanel = null;
	this._userSelectorWrapper = null;
	this._userSelector = null;
	this._roleSelectorWrapper = null;
	this._roleSelector = null;
	this._selectUserButton = null;
	this._selectUserButtonLiteral = null;
	this._selectRolesButton = null;
	this._selectRolesButtonLiteral = null;

	this._profileTypeChangedDelegate = null;
	this._usersRadioClickDelegate = null;
	this._userSelectorClickDelegate = null;
	this._rolesSelectorClickDelegate = null;
	this._userSelectedDelegate = null;
	this._userSelectionCanceledDelegate = null;
	this._roleSelectedDelegate = null;

	this._userSelectorDialog = null;
	this._roleSelectorDialog = null;

	this._currentMode = null;
	this._selectedUserId = null;
	this._selectedUserProvider = null;
	this._selectedRoles = null;

	this._loadDelegate = null;
	this._showDelegate = null;
}

Telerik.Sitefinity.Security.Web.UI.UsersListUserView.prototype = {

	/* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
    	Telerik.Sitefinity.Security.Web.UI.UsersListUserView.callBaseMethod(this, 'initialize');

    	this._assignClickHandlers();

    	this._loadDelegate = Function.createDelegate(this, this._loadHandler);
    	Sys.Application.add_load(this._loadDelegate);

    	this._showDelegate = Function.createDelegate(this, this._showHandler);
    	dialogBase.get_radWindow().add_show(this._showDelegate);

    	// Prevent memory leaks
    	$(this).on("unload", function (e) {
    		jQuery.event.remove(this);
    		jQuery.removeData(this);
    	});
	},

	dispose: function () {
		if (this._loadDelegate) {
			delete this._loadDelegate;
		}

		if (this._showDelegate) {
		    if (dialogBase.get_radWindow()) {
		        dialogBase.get_radWindow().remove_show(this._showDelegate);
		    }
		    delete this._showDelegate;
		}

		if (this._profileTypeChangedDelegate) {
			this._profileTypeSelector.remove_valueChanged(this._profileTypeChangedDelegate);
			delete this._profileTypeChangedDelegate;
		}

		if (this._userSelectorClickDelegate) {
			delete this._userSelectorClickDelegate;
		}

		if (this._usersRadioClickDelegate) {
			delete this._usersRadioClickDelegate;
		}

		if (this._userSelectedDelegate) {
			delete this._userSelectedDelegate;
		}

		if (this._userSelectionCanceledDelegate) {
			delete this._userSelectionCanceledDelegate;
		}

		if (this._rolesSelectorClickDelegate) {
			delete this._rolesSelectorClickDelegate;
		}

		if (this._roleSelectedDelegate) {
			delete this._roleSelectedDelegate;
		}

		if (this._roleSelectionCanceledDelegate) {
			delete this._roleSelectionCanceledDelegate;
		}

		Telerik.Sitefinity.Security.Web.UI.UsersListUserView.callBaseMethod(this, 'dispose');
	},

	/* --------------------------------- public methods --------------------------------- */

    // refereshed the user interface. Call this method in case underlying control object
    // has been changed somewhere else then through this desinger.
    refreshUI: function () {
    	var masterDefinition = this.get_controlData().ControlDefinition.Views["UserProfilesFrontendMaster"];
    	var controlData = this.get_controlData();

    	this._profileTypeSelector.set_value(masterDefinition.ProfileTypeFullName);

    	switch (masterDefinition.UsersDisplayMode)
		{
			case "All":
				jQuery(this.get_element()).find(':radio[id$=All]').click();

				break;
			case "Specific":
				jQuery(this.get_element()).find(':radio[id$=Specific]').click();
				jQuery(this.get_selectUserButtonLiteral()).html(this._clientLabelManager.getLabel("Labels", "ChangeEllipsis"));

				this._selectedUserId = masterDefinition.UserId;
				this._selectedUserProvider = masterDefinition.Provider;

                if(masterDefinition.SelectedUserTitle != null)
				    jQuery("#selectedUserLabel").text(masterDefinition.SelectedUserTitle).show();

				break;
			case "FromRoles":
				jQuery(this.get_element()).find(':radio[id$=FromRoles]').click();
				jQuery(this.get_selectRolesButtonLiteral()).html(this._clientLabelManager.getLabel("Labels", "ChangeEllipsis"));

				if (controlData.Roles != "") {
					this._selectedRoles = Sys.Serialization.JavaScriptSerializer.deserialize(controlData.Roles);
					if (this._selectedRoles.length > 0) {
						var selectedRoleNames = "";
						for (var i = 0, len = this._selectedRoles.length; i < len; i++) {
							selectedRoleNames += '<li class="sfSelectedItem">' + this._selectedRoles[i].Title + '</li>';
							if (i + 1 == len)
								selectedRoleNames += '';
						}
						jQuery("#selectedRoleLabel").html(selectedRoleNames).show();
					}
				}

				break;
		}
	},

	// once the data has been modified, call this method to apply all the changes made
    // by this designer on the underlying control object.
    applyChanges: function () {
		var masterDefinitions = this.get_controlData().ControlDefinition.Views["UserProfilesFrontendMaster"];
		var controlData = this.get_controlData();

		masterDefinitions.ProfileTypeFullName = this._profileTypeSelector.get_value();
		masterDefinitions.UsersDisplayMode = this._currentMode;
		if (this._currentMode == "Specific" && this._selectedUserId != null)
		{
			masterDefinitions.UserId = this._selectedUserId;
			masterDefinitions.Provider = this._selectedUserProvider;
		}

		if (this._currentMode == "FromRoles" && this._selectedRoles != null)
		{
			controlData.Roles = Sys.Serialization.JavaScriptSerializer.serialize(this._selectedRoles);
		}
	},

	/* --------------------------------- event handlers --------------------------------- */

	_profileTypeChangedHandler: function (sender, args) {
		var masterDefinitions = this.get_controlData().ControlDefinition.Views["UserProfilesFrontendMaster"];
		masterDefinitions.ProfileTypeFullName = sender.get_value();
	},

    _loadHandler: function (sender, args) {
    	this._createSelectorDialogs();
    },

	_showHandler: function (sender, args) {
		dialogBase.resizeToContent();
	},

	_setUsersRadioHandler: function (e) {
		var radioID = e.target.value;
        switch (radioID) {
        	case "All":
        		jQuery(this._oneUserOnlySelectButtonPanel).hide();
        		jQuery(this._rolesPanel).hide();
				break;
			case "Specific":
        		jQuery(this._oneUserOnlySelectButtonPanel).show();
				jQuery(this._rolesPanel).hide();
				break;
			case "FromRoles":
				jQuery(this._oneUserOnlySelectButtonPanel).hide();
				jQuery(this._rolesPanel).show();
				break;
		}
        this._currentMode = radioID;
        dialogBase.resizeToContent();
	},

	_userSelectorClickHandler: function (sender, args) {
	    this._userSelectorDialog.dialog("open");
	    jQuery("body > form").hide();
	    dialogBase.resizeToContent();
	},

	_rolesSelectedClickHandler: function (sender, args) {
	    this._roleSelectorDialog.dialog("open");
	    jQuery("body > form").hide();
	    dialogBase.resizeToContent();
	},

	_userSelectedHandler: function (sender, args) {
	    this._userSelectorDialog.dialog("close");
	    jQuery("body > form").show();

		var selectedUserId = this.get_userSelector().get_selectedKeys()[0];
		var selectedItem = this.get_userSelector().getSelectedItems()[0];
		var selectedUserName = String.format("{0} ({1})", selectedItem.DisplayName, selectedItem.Email);
		jQuery("#selectedUserLabel").text(selectedUserName).show();
		jQuery(this.get_selectUserButtonLiteral()).text(this._clientLabelManager.getLabel("Labels", "ChangeEllipsis"));
		this._selectedUserId = selectedUserId;

		dialogBase.resizeToContent();
	},

	_userSelectionCanceledHandler: function (sender, args) {
	    this._userSelectorDialog.dialog("close");
	    jQuery("body > form").show();
	    dialogBase.resizeToContent();
	},

	_roleSelectedHandler: function (sender, args) {
		var controlData = this.get_controlData().ControlDefinition.Views["UserProfilesFrontendMaster"];
		
        this._roleSelectorDialog.dialog("close");
		jQuery("body > form").show();

		var selectedItems = this.get_roleSelector().getSelectedItems();
		var selectedRoleNames = "";
		var selectedRoles = [];
		for (var i = 0, len = selectedItems.length; i < len; i++)
		{
			selectedRoleNames += '<li class="sfSelectedItem">' + selectedItems[i].Name + '</li>';
			if (i + 1 == len)
				selectedRoleNames += '';

			var obj = { ProviderName: selectedItems[i].ProviderName, ItemId: selectedItems[i].Id, Title: selectedItems[i].Name };
			obj.__type = controlData.RolesItemInfoName;
			selectedRoles[selectedRoles.length] = obj;
		}
		jQuery("#selectedRoleLabel").html(selectedRoleNames).show();
		jQuery(this.get_selectRolesButtonLiteral()).text(this._clientLabelManager.getLabel("Labels", "ChangeEllipsis"));
		this._selectedRoles = selectedRoles;

		dialogBase.resizeToContent();
	},

	_roleSelectionCanceledHandler: function (sender, args) {
	    this._roleSelectorDialog.dialog("close");
	    jQuery("body > form").show();
	    dialogBase.resizeToContent();
	},

	/* --------------------------------- private methods --------------------------------- */

	get_parentRadioChoices: function () {
		if (!this._parentRadioChoices) {
			this._parentRadioChoices = jQuery(this.get_element()).find(':radio[name$=UsersSelection]'); // finds radio buttons with names ending with 'ParentSelection'
		}
		return this._parentRadioChoices;
	},

	_createSelectorDialogs: function () {
		this._userSelectorDialog = jQuery(this.get_userSelectorWrapper()).dialog(
            { autoOpen: false,
            	modal: false,
            	width: 355,
            	closeOnEscape: true,
            	resizable: false,
            	draggable: false,
                classes: {
                    "ui-dialog": "sfZIndexM"
                }
            });

		this._roleSelectorDialog = jQuery(this.get_roleSelectorWrapper()).dialog(
			{ autoOpen: false,
				modal: false,
				width: 355,
				closeOnEscape: true,
				resizable: false,
				draggable: false,
				classes: {
				    "ui-dialog": "sfZIndexM"
				}
			});
	},

	_assignClickHandlers: function () {
		this._profileTypeChangedDelegate = Function.createDelegate(this, this._profileTypeChangedHandler);
		this._profileTypeSelector.add_valueChanged(this._profileTypeChangedDelegate);

		this._usersRadioClickDelegate = Function.createDelegate(this, this._setUsersRadioHandler);
		this.get_parentRadioChoices().click(this._usersRadioClickDelegate);

		this._userSelectorClickDelegate = Function.createDelegate(this, this._userSelectorClickHandler);
		jQuery(this.get_selectUserButton()).click(this._userSelectorClickDelegate);

		this._rolesSelectorClickDelegate = Function.createDelegate(this, this._rolesSelectedClickHandler);
		jQuery(this.get_selectRolesButton()).click(this._rolesSelectorClickDelegate);

		this._userSelectedDelegate = Function.createDelegate(this, this._userSelectedHandler);
		this._userSelectionCanceledDelegate = Function.createDelegate(this, this._userSelectionCanceledHandler);

		jQuery("#doneSelectingUserButton").click(this._userSelectedDelegate);
		jQuery("#cancelSelectingUserButton").click(this._userSelectionCanceledDelegate);

		this._roleSelectedDelegate = Function.createDelegate(this, this._roleSelectedHandler);
		this._roleSelectionCanceledDelegate = Function.createDelegate(this, this._roleSelectionCanceledHandler);

		jQuery("#doneSelectingRoleButton").click(this._roleSelectedDelegate);
		jQuery("#cancelSelectingRoleButton").click(this._roleSelectionCanceledDelegate);
	},

	/* --------------------------------- properties --------------------------------- */

    // gets the reference to the parent designer component
    get_parentDesigner: function () {
        return this._parentDesigner;
    },
    // sets the reference to the parent designer component
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
    },

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

	get_profileTypeSelector: function () {
		return this._profileTypeSelector;
	},

	set_profileTypeSelector: function (value) {
		this._profileTypeSelector = value;
	},

	get_oneUserOnlySelectButtonPanel: function () {
		return this._oneUserOnlySelectButtonPanel;
	},

	set_oneUserOnlySelectButtonPanel: function (value) {
		this._oneUserOnlySelectButtonPanel = value;
	},

	get_rolesPanel: function () {
		return this._rolesPanel;
	},

	set_rolesPanel: function (value) {
		this._rolesPanel = value;
	},

	get_userSelectorWrapper: function () {
        return this._userSelectorWrapper;
    },

    set_userSelectorWrapper: function (value) {
        this._userSelectorWrapper = value;
    },

	get_userSelector: function () {
		return this._userSelector;
	},

	set_userSelector: function (value) {
		this._userSelector = value;
	},

	get_roleSelectorWrapper: function () {
		return this._roleSelectorWrapper;
	},

	set_roleSelectorWrapper: function (value) {
		this._roleSelectorWrapper = value;
	},

	get_roleSelector: function () {
		return this._roleSelector;
	},

	set_roleSelector: function (value) {
		this._roleSelector = value;
	},

	get_selectUserButton: function () {
        return this._selectUserButton;
    },

    set_selectUserButton: function (value) {
        this._selectUserButton = value;
    },

	get_selectUserButtonLiteral: function () {
		return this._selectUserButtonLiteral;
	},

	set_selectUserButtonLiteral: function (value) {
		this._selectUserButtonLiteral = value;
	},

	get_selectRolesButton: function () {
		return this._selectRolesButton;
	},

	set_selectRolesButton: function (value) {
		this._selectRolesButton = value;
	},

	get_selectRolesButtonLiteral: function () {
		return this._selectRolesButtonLiteral;
	},

	set_selectRolesButtonLiteral: function (value) {
		this._selectRolesButtonLiteral = value;
	},

	get_clientLabelManager: function () {
		return this._clientLabelManager;
	},

	set_clientLabelManager: function (value) {
		if (this._clientLabelManager != value) {
			this._clientLabelManager = value;
			this.raisePropertyChanged('clientLabelManager');
		}
	}
}

Telerik.Sitefinity.Security.Web.UI.UsersListUserView.registerClass('Telerik.Sitefinity.Security.Web.UI.UsersListUserView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
