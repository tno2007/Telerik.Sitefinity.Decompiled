Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Type._registerScript("PropertyGrid.js", ["IControlDesigner.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");

// ------------------------------------------------------------------------
// PropertyGrid class
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.PropertyGrid = function (element) {
    this._breadcrumbId = null;
    this._breadcrumb = null;
    this._screensBreadcrumb = null;
    this._propertyBinder = null;
    this._alphabeticalView = null;
    this._lastCategoryName = null;
    this._currentScreenId = 0;
    this._currentPropertyName = '';
    this._currentPropertyPath = '';
    this._currentLevel = 0;
    this._properties = null;
    this._propertyEditor = null;
    this._propertyViewsToolbar = null;
    this._expandAllButtonId = null;
    this._collapseAllButtonId = null;
    this._proxyPropertyName = null;
    this._propertyPaths = [];
    Telerik.Sitefinity.Web.UI.PropertyGrid.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.PropertyGrid.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {
        this._pageLoadedDelegate = Function.createDelegate(this, this._pageLoaded);
        this._breadcrumbClickedDelegate = Function.createDelegate(this, this._breadcrumbClicked);

        this._itemDataBoundDelegate = Function.createDelegate(this, this._propertyGridItemDataBound);
        this._propertyBinder.add_onItemDataBound(this._itemDataBoundDelegate);

        this._dataBoundDelegate = Function.createDelegate(this, this._propertyGridDataBound);
        this._propertyBinder.add_onDataBound(this._dataBoundDelegate);

        this._itemSelectedDelegate = Function.createDelegate(this, this._propertyGridItemSelectCommand);
        this._propertyBinder.add_onItemSelectCommand(this._itemSelectedDelegate);

        this._toolbarModeChangedDelegate = Function.createDelegate(this, this._toolbarModeChanged)
        this._propertyViewsToolbar.add_buttonClicking(this._toolbarModeChangedDelegate);

        this._levelFilterDelegate = Function.createDelegate(this, this._levelFilter);

        this._expandAllDelegate = Function.createDelegate(this, this._expandAll);
        $addHandler($get(this._expandAllButtonId), 'click', this._expandAllDelegate);

        this._collapseAllDelegate = Function.createDelegate(this, this._collapseAll);
        $addHandler($get(this._collapseAllButtonId), 'click', this._collapseAllDelegate);

        Sys.Application.add_load(this._pageLoadedDelegate);

        Telerik.Sitefinity.Web.UI.PropertyGrid.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        this._pageLoadedDelegate = null;
        this._breadcrumbClickedDelegate = null;
        this._propertyBinder.remove_onItemDataBound(this._itemDataBoundDelegate);
        this._itemDataBoundDelegate = null;
        this._propertyBinder.remove_onDataBound(this, this._dataBoundDelegate);
        this._dataBoundDelegate = null;
        this._propertyBinder.remove_onItemSelectCommand(this._itemSelectedDelegate);
        this._itemSelectedDelegate = null;
        this._propertyViewsToolbar.remove_buttonClicking(this._toolbarModeChangedDelegate);
        this._toolbarModeChangedDelegate = null;
        this._expandAllDelegate = null;
        this._collapseAllDelegate = null;
        this._levelFilterDelegate = null;
        Telerik.Sitefinity.Web.UI.PropertyGrid.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */


    /* ************************* events ************************* */


    /* ************************* event handlers ************************* */
    _pageLoaded: function () {
        // attach the event handler for the breadcrumb control
        this._breadcrumb = $find(this._breadcrumbId);
        this._breadcrumb.add_click(this._breadcrumbClickedDelegate);
        dialogBase.resizeToContent();
        // set the default view to the alphabetical
        // this._changeView(true);
    },

    _toolbarModeChanged: function (sender, args) {
        var button = args.get_item();
        if (args.get_item().get_value() == 'Alphabetically') {
            this._changeView(true);
        }
        else if (args.get_item().get_value() == 'Categorized') {
            this._changeView(false);
        }
    },

    // handles the click event of the breadcrumb component
    _breadcrumbClicked: function (sender, args) {
        if (typeof args !== 'undefined' && args !== null) {
            this._navigateToScreen(args.text, args.value);
        }
    },

    _propertyGridItemDataBound: function (sender, args) {
        var dataItem = args.get_dataItem();

        var categoryName = dataItem.CategoryName;
        var el = args.get_itemElement();

        $(el).find('.sf_categoryElement').each(function (i, element) {
            if (alphabeticalView || lastCategoryName == dataItem.CategoryName) {
                $(element).hide();
            } else {
                $(element).show();
            }
        });

        if (dataItem.NeedsEditor && dataItem.NeedsEditor.toString().toLowerCase() != 'false') {
            $(el).find('.sf_complexProperty').each(function (i, element) {
                $(element).show();
            });
            $(el).find('.sf_propertyInput').each(function (i, element) {
                $(element).hide();
            });
        }

        var self = this;
        // Ivan's note: jQuery has issues with change event in this particular scenarion, therefore
        // a workaround with the click event
        $(el).find('input[type="text"]').keyup(function () {
            self._changeProperty(dataItem.PropertyPath, dataItem.PropertyId, $(this).val());
        });

        lastCategoryName = dataItem.CategoryName;
    },

    _propertyGridDataBound: function (sender, args) {
        var self = this;
        $('#screens').find('.sf_categoryLink').click(function () {
            var itemSelector = 'sf_item' + $(this).text().replace(" ", "");
            $('#propertyGridLevel' + self._currentScreenId).find('li.' + itemSelector + ' > div.sf_propertyElement').each(function () {
                $(this).toggle();
            });
        });
    },

    _propertyGridItemSelectCommand: function (sender, args) {
        var dataItem = args.get_dataItem();

        var numberOfScreens = $('#screens > .screen').length;
        var propertyName = dataItem.PropertyName;

        // clone the level0 list item and clear the ul element inside of it
        $('#screens > #level0').clone().attr('id', 'level' + numberOfScreens).insertAfter('#screens > #level0').find('#propertyGridLevel0').attr('id', 'propertyGridLevel' + numberOfScreens).empty();

        this._currentScreenId = numberOfScreens;
        this._currentPropertyName = propertyName;
        this._propertyPaths[this._currentScreenId] = dataItem.PropertyPath;
        this._currentPropertyPath = dataItem.PropertyPath;
        this._currentLevel = dataItem.PropertyPath.split('/').length - 1;

        this._bindProperties();
        this._navigateToScreen(propertyName, this._currentScreenId);
    },

    /* ************************* event firing ************************* */


    /* ************************* private methods ************************* */
    _changeView: function (toAlphabetical) {
        var expandCollapsePanel = $('#expandCollapsePanel');
        var screensList = $("#screens");
        lastCategoryName = '';
        if (toAlphabetical) {
            this._propertyBinder.set_sortExpression('Name');
            alphabeticalView = true;
            $(expandCollapsePanel).hide();
            screensList.removeClass("sfCategorizedList");
        } else {
            this._propertyBinder.set_sortExpression('Category');
            alphabeticalView = false;
            $(expandCollapsePanel).show();
            screensList.addClass("sfCategorizedList");
        }
        this._bindProperties();
    },

    _setBreadcrumb: function (propertyName, screenId) {
        if (this._screensBreadcrumb == null) {
            this._screensBreadcrumb = [{ text: 'Home', value: 0}];
        }

        // if screenId does not exist in the screensBreadcrumb, assume we are moving forward
        // otherwise cut everything behind the current screen, assuming we are moving backwards
        var screenPosition = -1;
        for (screenIter = 0; screenIter < this._screensBreadcrumb.length; screenIter++) {
            if (this._screensBreadcrumb[screenIter].value == screenId) {
                screenPosition = screenIter;
                break;
            }
        }

        if (screenPosition == -1) {
            this._screensBreadcrumb.push({ text: propertyName, value: this._screensBreadcrumb.length });
        } else {
            this._screensBreadcrumb = this._screensBreadcrumb.slice(0, screenPosition + 1);
        }

        this._currentPropertyPath = '';
        // skip the home node
        for (nodeIter = 1; nodeIter < this._screensBreadcrumb.length; nodeIter++) {
            this._currentPropertyPath += this._screensBreadcrumb[nodeIter].text + '/';
        }
        this._currentPropertyPath = this._currentPropertyPath.substr(0, this._currentPropertyPath.length - 1);

        this._breadcrumb.set_list(this._screensBreadcrumb);
        if (this._screensBreadcrumb.length == 1) {
            $('#breadcrumbDiv').hide();
            $('#displayModesPanel').show();
        } else {
            $('#breadcrumbDiv').show();
            $('#displayModesPanel').hide();
        }
    },

    _navigateToScreen: function (propertyName, screenId) {
        // TODO: Katia the screenId is the id of the LI element representing the screen that 
        // should be displayed now. The animation should be added here.

        // hide all screens
        $('.screen').hide();

        // show the screen that should be displayed
        $('#level' + screenId).show();

        this._currentScreenId = screenId;
        this._currentPropertyName = propertyName;

        this._setBreadcrumb(propertyName, screenId);
    },

    _changeProperty: function (propertyPath, propertyId, propertyValue) {
        this.get_propertyEditor().changePropertyValue(propertyPath, propertyValue);
    },

    _collapseAll: function () {
        $('.screen').find('li > div.sf_propertyElement').each(function (i, element) {
            $(element).hide();
        });
        $('.screen').find('li > div.sf_categoryElement').each(function (i, element) {
            $(element).addClass("sfCollapsedCategory");
        });
    },

    _expandAll: function () {
        $('.screen').find('li > div.sf_propertyElement').each(function (i, element) {
            $(element).show();
        });
        $('.screen').find('li > div.sf_categoryElement').each(function (i, element) {
            $(element).removeClass("sfCollapsedCategory");
        });
    },

    _bindProperties: function () {
        var newTarget = $get('propertyGridLevel' + this._currentScreenId);
        this._propertyBinder.set_target(newTarget);

        this._propertyBinder.ClearTarget();
        var items = this._propertyEditor.get_propertyBag();

        // remove proxy, move down there

        if (this._propertyBinder.get_sortExpression() == 'Name') {
            items.sort(this._sortPropertiesForAlphabeticalView);
        } else {
            items.sort(this._sortPropertiesForCategoryView);
        }

        // check if proxy property exists
        var proxyPropertyExists = false;
        var propsLength = items.length;
        while (propsLength--) {
            if (items[propsLength].IsProxy) {
                proxyPropertyExists = true;
                this._proxyPropertyName = items[propsLength].PropertyName;
                break;
            }
        }

        var proxyFilterDelegate = Function.createDelegate(this, this._levelFilterForProxy);

        var data;
        if (proxyPropertyExists) {
            data = { 'Items': items.filter(proxyFilterDelegate), 'IsGeneric': false };
        } else {
            data = { 'Items': items.filter(this._levelFilterDelegate), 'IsGeneric': false };
        }
        this._propertyBinder.BindCollection(data);
    },

    _sortPropertiesForCategoryView: function (a, b) {
        if (a.CategoryName < b.CategoryName) {
            return -1;
        } else if (a.CategoryName > b.CategoryName) {
            return 1;
        } else {
            return 0;
        }
    },

    _sortPropertiesForAlphabeticalView: function (a, b) {
        if (a.Name < b.Name) {
            return -1;
        } else if (a.Name > b.Name) {
            return 1;
        } else {
            return 0;
        }
    },

    _levelFilter: function (property) {
        var level = property.PropertyPath.split('/').length - 2;
        if (this._currentPropertyName == 'Home') {
            if (level > 0) {
                return false;
            } else {
                return true;
            }
        } else {
            if (property.PropertyPath.startsWith(this._propertyPaths[this._currentScreenId])) {
                return this._currentLevel == level;
            } else {
                return false;
            }
        }
    },

    _levelFilterForProxy: function (property) {
        var isProxy = property.IsProxy;

        if (isProxy) {
            return false;
        }
        else {
            if (!this._currentLevel)
                this._currentLevel = 1;

            return this._levelFilter(property);
        }
    },

    /* ************************* properties ************************* */
    // gets the reference to the property binder
    get_propertyBinder: function () {
        return this._propertyBinder;
    },
    // sets the reference to the property binder
    set_propertyBinder: function (value) {
        this._propertyBinder = value;
    },
    // gets the reference to the propertyEditor control
    get_propertyEditor: function () {
        return this._propertyEditor;
    },
    // sets the reference for the propertyEditor control
    set_propertyEditor: function (value) {
        this._propertyEditor = value;
    },
    // gets the reference to the property view toolbar component
    get_propertyViewsToolbar: function () {
        return this._propertyViewsToolbar;
    },
    // sets the reference to the property views toolbar component
    set_propertyViewsToolbar: function (value) {
        this._propertyViewsToolbar = value;
    }
};
Telerik.Sitefinity.Web.UI.PropertyGrid.registerClass('Telerik.Sitefinity.Web.UI.PropertyGrid', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IControlDesigner);

if (!Array.prototype.filter)
{
  Array.prototype.filter = function(fun /*, thisp*/)
  {
    var len = this.length;
    if (typeof fun != "function")
      throw new TypeError();

    var res = new Array();
    var thisp = arguments[1];
    for (var i = 0; i < len; i++)
    {
      if (i in this)
      {
        var val = this[i];
        if (fun.call(thisp, val, i, this))
          res.push(val);
      }
    }

    return res;
  };
}

String.prototype.startsWith = function(str) 
{return (this.match("^"+str)==str)}