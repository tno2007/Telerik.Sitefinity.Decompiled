Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerBase = function (element) {
    Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerBase.initializeBase(this, [element]);
    this._designerViewsMap = null;
    this._menuTabStrip = null;
    this._clientTabSelectedDelegate = null;
    this._hiddenViews = [];
    this._message = null;

}

Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerBase.prototype = {

    /* ----------------------------- setup and teardown ----------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerBase.callBaseMethod(this, 'initialize');
        Sys.Application.add_load(Function.createDelegate(this, this._onLoad));

        this._clientTabSelectedDelegate = Function.createDelegate(this, this._clientTabSelectedHandler)
        this._menuTabStrip.add_tabSelected(this._clientTabSelectedDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerBase.callBaseMethod(this, 'dispose');

        if (this._clientTabSelectedDelegate) {
            this._menuTabStrip.remove_tabSelected(this._clientTabSelectedDelegate);
            delete this._clientTabSelectedDelegate;
        }
    },

    /* ----------------------------- public methods ----------------------------- */

    // forces the designer to refresh the UI from the cotnrol Data
    refreshUI: function () {
        if (this._designerViewsMap != null) {
            for (var i in this._designerViewsMap) {
                if ($find(this._designerViewsMap[i]) !== null) {
                    $find(this._designerViewsMap[i]).refreshUI();
                }
            }
        }
    },

    // forces the designer to apply the changes on UI to the cotnrol Data
    applyChanges: function () {
        if (this._designerViewsMap != null) {
            for (var i in this._designerViewsMap) {
                if ($find(this._designerViewsMap[i]) !== null) {
                    $find(this._designerViewsMap[i]).applyChanges();
                }
            }
        }
    },
    ///sets the value of the fieldControl to the control porpety according to the fieldName(propertyPAth)
    setFieldControlValueToControlData: function (fieldControl, controlData) {
        var value = fieldControl.get_value();
        var propertyPath = fieldControl.get_fieldName();
        this.setValueToControlData(propertyPath, value);
    },
    ///sets the passed value to the control porpety according to the passed propertyPath
    setValueToControlData: function (propertyPath, value, controlData) {
        var res = this._setValueByPath(propertyPath, controlData, value);
        if (!res) {
            // Ivan's note: Not sure what this alert does...
            // alert("Error: Property with path + " + propertyPath + "not set to value " + value);
        }
    },

	//Shows the menu tab strip
	showDesignerOptions: function()
	{
		this.get_menuTabStrip().get_element().style.display = "";
	},

	//hides the menu tab strip
	hideDesignerOptions: function()
	{
		this.get_menuTabStrip().get_element().style.display = "none";
	},

	//fired by the child designer views 
	executeCommand: function(argument)
	{
	},

	//selects the view with the passed viewName or index. If the viewName is null it searches for the tab using the index.
	selectView: function(viewName)
	{
		if(viewName)
		{
			var tab = this.get_menuTabStrip().findTabByValue(viewName);
			if (tab) {
			    tab.select();
			    this.set_saveButtonEnabled(true);
			    var view = $find(this._designerViewsMap[viewName])
			    if (typeof view.notifyViewSelected === "function") {
			        view.notifyViewSelected();
			    }
			}
		}
	},

	isViewSelected: function(view) {
		var views = this.get_designerViewsMap();

		return views[this.get_currentViewName()] == view.get_id();
	},


    /* ----------------------------- event handlers ----------------------------- */

    // Called when page has loaded with all of its components. At this moment property
    // editor already has the control data.
    _onLoad: function () {
        if (this._designerViewsMap != null) {
            for (var i in this._designerViewsMap) {
                if ($find(this._designerViewsMap[i]) !== null) {
                    $find(this._designerViewsMap[i]).set_parentDesigner(this);
                }
            }
        }
        Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerBase.callBaseMethod(this, '_onLoad');

		this._hideTabsOfHiddenViews();
        dialogBase.resizeToContent();
    },

    // Sends notification that a view in the designer was selected
    _clientTabSelectedHandler: function (sender, args) {
		var viewName = sender.get_selectedTab().get_value();
        var designerView = $find(this._designerViewsMap[viewName]);
        if (typeof designerView.notifyViewSelected === "function")
            designerView.notifyViewSelected();

		this._processHiddenView(viewName);
		dialogBase.resizeToContent();
    },

	//shows or hides the designerViewOptions based on the hiddenViews array
	_processHiddenView: function(viewName)
	{
		if(Array.indexOf(this.get_hiddenViews(), viewName) < 0)
		{
			this.showDesignerOptions();
		}
		else
		{
			this.hideDesignerOptions();
		}
	},

	_hideTabsOfHiddenViews: function()
	{
		var length = this._hiddenViews.length;
		if(length > 0)
		{
			var menuTabStrip = this.get_menuTabStrip();
			this._processHiddenView(menuTabStrip.get_selectedTab().get_value());
			for (var i = 0; i < length; i++) 
			{
				var tab = menuTabStrip.findTabByValue(this._hiddenViews[i]);
				if(tab) tab.get_element().style.display = "none";
			}
		}
	},

    /* ----------------------------- properties ----------------------------- */

    get_designerViewsMap: function () {
        return this._designerViewsMap;
    },
    set_designerViewsMap: function (value) {
        this._designerViewsMap = value;
    },
    get_menuTabStrip: function () {
        return this._menuTabStrip;
    },
    set_menuTabStrip: function (value) {
        this._menuTabStrip = value;
    },
    get_hiddenViews: function () {
        return this._hiddenViews;
    },
    set_hiddenViews: function (value) {
        this._hiddenViews = value;
    },

	get_currentViewName: function() {
		return this.get_menuTabStrip().get_selectedTab().get_value();
	},

	set_saveButtonText: function(text) {
		jQuery(this.get_saveButton()).find(".sfLinkBtnIn").text(text);
	},

	set_saveButtonEnabled: function(state) {
		jQuery(this.get_saveButton())[state ? "removeClass" : "addClass"]("sfDisabledLinkBtn");
	},

	get_saveButton: function() {
		return this.get_propertyEditor().get_saveButton();
	},

	get_message: function () {
	    return this._message;
	},
	set_message: function (value) {
	    this._message = value;
	},

    /* private methods */
    _pathToChunks: function (path) {

        //convert array indexes  to properties
        path = path.replace(/\[/g, ".");
        path = path.replace(/\]/g, "");
        path = path.replace(/\"|\'/g, "");
        //all properties as array elements
        return path.split(".");
    },

    //sets a property value by given properyt path (like: a.b.c[0].d)
    // obj - is the object, which to set the value to
    _setValueByPath: function (path, obj, value) {
        var pathChunks;
        if (path.constructor != Array) {
            pathChunks = this._pathToChunks(path);
        }
        else {
            pathChunks = path;
        }
        var currentChunk = pathChunks[0];

        var depth = pathChunks.length;
        var ready = false;
        for (var memberName in obj) {
            if (memberName == currentChunk) {
                // names match and we're on the right depth
                if (depth == 1) {
                    //we are on the right member
                    obj[memberName] = value;
                    ready = true;
                }
                else {
                    //we go deeper, just remove the current element from the path
                    ready = this._setValueByPath(pathChunks.slice(1), obj[memberName], value);
                }
            }
            if (ready) {
                return true;
            }
        }
        return false;
    }

}

Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerBase.registerClass('Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerBase', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
