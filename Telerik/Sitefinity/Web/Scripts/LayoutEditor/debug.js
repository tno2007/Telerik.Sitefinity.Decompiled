//================================ Layout editor (topmost control consisting of 3 child editors ================================//

Type.registerNamespace("Telerik.Sitefinity.Web.UI");

var zoneEditor = null;

Telerik.Sitefinity.Web.UI.LayoutEditor = function (element) {

    Telerik.Sitefinity.Web.UI.LayoutEditor.initializeBase(this, [element]);

    this._zoneEditorId = null;

    this._tabstrip = null;
    this._tabstripId = null;

    this._visualEditorContainerId = null;
    this._visualEditorContainer = null;

    this._controlPanel = null;
    this._controlPanelId = null;

    this._saveButtonId = null;
    this._saveButton = null;
    this._cancelButtonId = null;
    this._cancelButton = null;

    this._labelManager = null;

    // Child Editor controls
    this._widthEditor = null;
    this._marginEditor = null;
    this._appearanceEditor = null;
    this._columnVisibilityEditor = null;

    // Labels
    this._aValueBetweenLabel = null;
    this._columnLabel = null;

    // Width editor fields
    this._sizesInPercentagesRadioId = null;
    this._sizesInPixelsRadioId = null;
    this._columnWidthsContainerId = null;
    this._autoSizedLabel = null;
    this._makeThisAutoSizedLabel = null;
    this._autoSizedColumnButtonId = null;
    this._changeAutoSizedColumnLabel = null;
    this._cancelChangeAutoSizedColumnLabel = null;

    // Spaces editor controls
    this._spacesInPercentagesRadioId = null;
    this._spacesInPixelsRadioId = null;
    this._simpleSpacesContainerId = null;
    this._advancedSpacesContainerId = null
    this._spacesSideBySideButtonId = null;
    this._equalSpacesButtonId = null;
    this._advancedMarginsContainerId = null;
    this._horizontalSpaceColumnsId = null;
    this._verticalSpaceColumsId = null;
    this._topLabel = null;
    this._rightLabel = null;
    this._bottomLabel = null;
    this._leftLabel = null;

    // Classes editor fields
    this._classesContainerId = null;
    this._placeholdersLabelsContainerId = null;

    // column visibility
    this._changeColumnVisibilityButtonId = null;

    //State and column arrays
    this._outerColumns = [];
    this._innerColumns = [];
    this._cssClasses = [];
    this._originalOuterState = []; //Array to hold the elements' css style (use it to restore original state when cancel is pressed)
    this._originalPlaceholderLabels = [];
    this._originalInnerState = [];
    this._originalCssClasses = [];

    //Layout root
    this._layoutRoot = null;

    this._layoutControlId = null;
    this._layoutControlIsTemplate = 0;

    this._layoutContainer = null;

    this._webServiceUrl = null;

    this._emptyColumnClass = 'zeDockZoneEmpty';
    this._emptyColumnEditClass = 'zeEmptyLayoutBoxEdit';
    this._originalEmptyColumns = [];

    this._dock = null;

    //Mode
    this._mode = ""; //"Sizes, ""Margins", "Appearance"
}

Telerik.Sitefinity.Web.UI.LayoutEditor.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.LayoutEditor.callBaseMethod(this, "initialize");

        this._saveButton = $get(this._saveButtonId);
        this._cancelButton = $get(this._cancelButtonId);

        this._saveDelegate = Function.createDelegate(this, this._saveLayout);
        this._cancelDelegate = Function.createDelegate(this, this._cancelLayout);

        $addHandler(this._saveButton, "click", this._saveDelegate);
        $addHandler(this._cancelButton, "click", this._cancelDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.LayoutEditor.callBaseMethod(this, "dispose");

        this._originalEmptyColumns = [];

        if (this._saveDelegate) {
            if (this._saveButton) {
                $removeHandler(this._saveButton, "click", this._saveDelegate);
            }
            delete this._saveDelegate;
        }
        if (this._cancelDelegate) {
            if (this._cancelButton) {
                $removeHandler(this._cancelButton, "click", this._cancelDelegate);
            }
            delete this._cancelDelegate;
        }
    },

    SetUpEditor: function (layoutRoot, layoutContainer, layoutControlId, mediaType, dock, pageId) {
        this._pageId = pageId;
        this._layoutRoot = layoutRoot;
        this._layoutContainer = layoutContainer;
        this._layoutControlId = layoutControlId;
        this._layoutControlIsTemplate = mediaType;
        this._dock = dock;

        _zoneEditor = $find(this._zoneEditorId);

        this._visualEditorContainer = $get(this._visualEditorContainerId);
        this._tabstrip = $find(this._tabstripId);
        this._controlPanel = $get(this._controlPanelId);

        //Get the outerCols array, ordered properly - the splitter needs them ordered!               
        this._outerColumns = this._getOrderedPlaceholders();
        this._innerColumns = [];

        //Sort out the inner columns
        for (var i = 0; i < this._outerColumns.length; i++) {
            var outerDiv = this._outerColumns[i];
            //Get the innerDivs
            this._innerColumns.push($telerik.getChildByClassName(outerDiv, "sf_colsIn"));
        }

        //Save original state
        this._saveOriginalState();

        //Show the editor UI
        this._showLayoutEditor();

        //Create WidthEditor               
        this._widthEditor = new Telerik.Sitefinity.Web.UI.WidthEditor(this._layoutRoot, this._visualEditorContainer, this._outerColumns, this._innerColumns, this._getSizesControlPanelInfo());
        this._widthEditor.initialize();

        //For margins we use the inner DIV elements, not the outher ones
        this._marginEditor = new Telerik.Sitefinity.Web.UI.MarginEditor(this._layoutRoot, this._visualEditorContainer, this._outerColumns, this._innerColumns, this._getSpacesControlPanelInfo());
        this._marginEditor.initialize();

        //Create AppearanceEditor
        this._appearanceEditor = new Telerik.Sitefinity.Web.UI.AppearanceEditor(this._layoutRoot, this._visualEditorContainer, this._outerColumns, this._innerColumns, this._getAppearanceControlPanelInfo());
        this._appearanceEditor.initialize();

        //Create ColumnVisibilityEditor
        this._columnVisibilityEditor = new Telerik.Sitefinity.Web.UI.ColumnVisibilityEditor(this._layoutRoot, this._changeColumnVisibilityButtonId, this._outerColumns.length);
        this._columnVisibilityEditor.initialize();

        //Set the mode before attaching the event handler to the tabstrip
        this.set_mode("Sizes");

        //Set the label which contains the description of the columns' visibility
        this._setColumnsVisibilityDescription(this);
        this._columnVisibilityEditor.executeOnClose(this, this._setColumnsVisibilityDescription);

        //Add handler to the tabstrip                 
        this._tabSelectedDelegate = Function.createDelegate(this, this._onClientTabSelected);
        this._tabstrip.add_tabSelected(this._tabSelectedDelegate);
    },

    CleanUpEditor: function () {
        //Call dispose for child editor controls
        var editors = [this._widthEditor, this._marginEditor, this._appearanceEditor, this._columnVisibilityEditor];
        for (var i = 0; i < editors.length; i++) {
            if (editors[i]) editors[i].dispose();
        }

        //Dispose all handlers that were attached during the initialize phase and destroy data objects                              
        if (this._tabstrip) this._tabstrip.remove_tabSelected(this._tabSelectedDelegate);

        //Hide the editor
        this._hideLayoutEditor();
    },

    _saveLayout: function () {

        var isLayout = (this._layoutControlIsTemplate.toString() == '0') ? 'false' : 'true';

        //Save the contents of the style attributes of the controls in the hidden field
        var tempWrapper = document.createElement('div');

        $(tempWrapper).html($(this._layoutContainer).html());

        //we check for changes in style widths and if there are no changes we set the original ones
        if (!this._widthEditor.WidthsAreChanged()) {
            $(tempWrapper).find("div.sf_colsOut").each(function () {
                this.style.width = "";
            });
        }

        // remove all the stuff that RadDock has injected inside
        $(tempWrapper).find('.RadDockZone').each(function () {
            $(this).remove();
        });
        // remove all the id attributes from the divs
        $(tempWrapper).find('div').each(function () {
            $(this).removeAttr('id');
        });

        $(tempWrapper).find('.' + this._emptyColumnEditClass).each(function () {
            $(this).removeClass(this._emptyColumnEditClass).addClass(this._emptyColumnClass);
        });

        var data = _zoneEditor._getWebServiceParameters('reload', this._dock);
        data['LayoutHtml'] = tempWrapper.innerHTML.trim().replace(/(<div)/gi, '$1 runat="server" '); // add runat="server" to each div --> fix this better later

        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var serviceUrl = this._webServiceUrl;
        var urlParams = [];
        // keys should be string
        var keys = [this._layoutControlId, this._pageId, isLayout + ""];

        var newTitle = this._getDockTitle();
        data.Title = newTitle;

        clientManager.InvokePut(serviceUrl, urlParams, keys, data, this._saveLayout_Success, this._saveLayout_Failure);

        // Change the title of the editor
        this._dock.set_title(newTitle);

        // Hide & dispose the layout editor
        this.CleanUpEditor();

        return false;
    },

    _cancelLayout: function () {
        //Restore the state
        this.restoreOriginalState();
        //Hide & dispose the layout editor
        this.CleanUpEditor();
        return false;
    },

    _saveLayout_Success: function (sender, result, webRequest) {
        // do nothing here
    },

    _saveLayout_Failure: function (result) {
        alert('fail');
    },

    _setColumnsVisibilityDescription: function (that) {
        $("#columnsVisibilityDescription").html('');

        var hiddenColumnsDictonary = that._columnVisibilityEditor.calculateHiddenColumns();
        var columns = $.map(hiddenColumnsDictonary, function (n, i) { return i; });
        columns.sort();

        if (columns.length > 0) {

            if (that._columnVisibilityEditor.checkWetherAllColumnsAreHidden()) {
                $("#columnsVisibilityDescription").text(that.get_labelManager().getLabel('Labels', 'AllColumnsAreHidden'));
            }
            else {
                for (i = 0; i < columns.length; i++) {
                    var columnDescr = document.createTextNode(that.get_labelManager().getLabel('Labels', 'ColumnIsHidden')
                        .replace("{0}", columns[i])
                        .replace("{1}", hiddenColumnsDictonary[columns[i]].join(",")));
                    $("#columnsVisibilityDescription").append(columnDescr);
                    $("#columnsVisibilityDescription").append("<br/>");
                }
            }
        }
        else {
            $("#columnsVisibilityDescription").text(that.get_labelManager().getLabel('Labels', 'AllColumnsAreVisible'));
        }
    },

    _getDockTitle: function () {
        var dockTitle = ""; // "Customized ";

        var colInfos = this._widthEditor._getColumnInfos();
        for (var i = 0; i < colInfos.length; i++) {
            if (i > 0) {
                dockTitle += " + ";
            }

            dockTitle += colInfos[i].width; // +" (" + colInfos[i].name + ")";
        }

        dockTitle += " (" + this.get_labelManager().getLabel('Labels', 'CustomLowercase') + ")";

        return dockTitle;
    },

    _setOverlayVisible: function (show, bounds) {
        var pane = document.body;

        if (show) {
            var options = {
                hide: false,
                disable: true,
                persistent: true, //!
                handles: false,
                movable: false,

                outerOpacity: 0.5,

                selectionOpacity: 0.3,
                selectionColor: "#9999ff",
                zIndex: 1000,

                classPrefix: "bodyOverlay",

                x1: 0, y1: 0, x2: 0, y2: 0
            };

            //Set selection bounds
            if (bounds) {
                options.x1 = bounds.x - 15;
                options.y1 = bounds.y - 15;
                options.x2 = bounds.x + bounds.width + 15;
                options.y2 = bounds.y + bounds.height + 3;
            }

            //The jQuery object might exist from some previous control initialization
            var existing = $(pane).data("imgAreaSelect");
            if (existing) existing.setOptions(options);
            else $(pane).imgAreaSelect(options);
        }
        else {
            var jObject = $(pane).data("imgAreaSelect");
            if (jObject) jObject.setOptions({ x1: 0, y1: 0, x2: 0, y2: 0 });
            var className = "div.bodyOverlay-outer";
            $(className).hide();
        }
    },

    _showLayoutEditor: function () {
        var layoutRoot = this._layoutRoot;
        // remove the empty column class
        var editor = this;
        $(layoutRoot).find('.' + this._emptyColumnClass).each(function () {
            $(this).removeClass(editor._emptyColumnClass).addClass(editor._emptyColumnEditClass);
            editor._originalEmptyColumns.push(this);
        });

        var bounds = $telerik.getBounds(layoutRoot);

        var topBorder = Number(this._visualEditorContainer.style.borderTopWidth.replace('px', ''));
        var rightBorder = Number(this._visualEditorContainer.style.borderRightWidth.replace('px', ''));
        var bottomBorder = Number(this._visualEditorContainer.style.borderBottomWidth.replace('px', ''));
        var leftBorder = Number(this._visualEditorContainer.style.borderLeftWidth.replace('px', ''));

        var scrollTop = jQuery('#sfPageContainer').scrollTop();

        this._visualEditorContainer.style.top = (this._getY(layoutRoot) - scrollTop - topBorder) + 'px';
        this._visualEditorContainer.style.left = (this._getX(layoutRoot) - leftBorder) + 'px';
        this._visualEditorContainer.style.width = bounds.width + 'px';
        this._visualEditorContainer.style.height = bounds.height + 'px';
        this._visualEditorContainer.style.position = 'fixed';
        this._visualEditorContainer.style.zIndex = 100000;
        this._visualEditorContainer.style.display = '';

        //wnd.moveTo(bounds.x - 18, bounds.y - 15);
        //wnd.setSize(bounds.width + 25, bounds.height + 16);

        //NEW
        this._setOverlayVisible(true, bounds);

        //Show the control panel
        this._controlPanel.style.display = "";
    },

    _hideLayoutEditor: function () {

        // put the empty classes back where needed
        var editor = this;
        for (i = 0; i < this._originalEmptyColumns.length; i++) {
            $(editor._originalEmptyColumns[i]).removeClass(editor._emptyColumnEditClass).addClass(editor._emptyColumnClass);
        }

        // Clear the columns collection, so it should be empty every time the layout editor is shown
        this._originalEmptyColumns = [];

        this._visualEditorContainer.style.display = 'none';
        //Hide the control panel
        this._controlPanel.style.display = "none";
        //Hide the body overlay
        this._setOverlayVisible(false);
    },

    get_mode: function () {
        return this._mode;
    },

    set_mode: function (mode) {
        this._mode = mode;

        if (mode == "Sizes") {
            this._widthEditor.SetVisualEditor();
        }
        else {; }

        if (mode == "Margins") {
            this._marginEditor.SetVisualEditor();
        }
        else {; }

        if (mode == "Appearance") {
            this._appearanceEditor.SetVisualEditor();
        }
        else {; }

        //Configure the tabstrip to show this tab in case it is showing some other tab               
        var tab = this._tabstrip.findTabByValue(mode);
        if (tab && !tab.get_selected()) tab.select();
    },

    _onClientTabSelected: function (sender, args) {
        var value = sender.get_selectedTab().get_value();
        this.set_mode(value);
    },

    _saveOriginalState: function () {
        var outerCols = this._outerColumns;
        var layoutUpdater = LayoutUpdater.getInstance();

        for (var i = 0; i < outerCols.length; i++) {
            this._originalOuterState[i] = outerCols[i].style.cssText;
            this._originalPlaceholderLabels[i] = layoutUpdater.getLabelFromOuterColumn(jQuery(outerCols[i]));
        }

        var innerCols = this._innerColumns;
        for (var i = 0; i < innerCols.length; i++) {
            this._originalInnerState[i] = innerCols[i].style.cssText;
        }

        var cssClasses = this._cssClasses;
        for (var i = 0; i < this._cssClasses.length; i++) {
            this._originalCssClasses[i] = outerCols[i].className;
        }
    },

    //Called when cancel is clicked
    restoreOriginalState: function () {
        var outerCols = this._outerColumns;
        var layoutUpdater = LayoutUpdater.getInstance();

        for (var i = 0; i < outerCols.length; i++) {
            outerCols[i].style.cssText = this._originalOuterState[i];

            layoutUpdater.updatePlaceholderLabels(jQuery(outerCols[i]), this._originalPlaceholderLabels[i]);
        }

        var innerCols = this._innerColumns;
        for (var i = 0; i < innerCols.length; i++) {
            innerCols[i].style.cssText = this._originalInnerState[i];
        }

        var cssClasses = this._cssClasses;
        for (var i = 0; i < this._cssClasses.length; i++) {
            outerCols[i].className = this._originalCssClasses[i];
        }
    },

    //Method returns a collection of the OUTER elements in the order in which they appear in the HTML 
    //(which might not be the order in which they appear visually)
    _getUnorderedPlaceholders: function () {
        return $telerik.getChildrenByClassName(this._layoutRoot, "sf_colsOut");
    },

    _getOrderedPlaceholders: function () {
        //Access the parent page, get a reference to the control, read its columns' widths & current height configure splitter
        var divs = this._getUnorderedPlaceholders();

        //See if the outerCols need to be rearranged - in case they have the sf_col1, sf_col2 etc classes!
        var outerCols = new Array(divs.length);
        for (var i = 0; i < divs.length; i++) {
            //Get the current placeholder
            var div = divs[i];
            //See if it contains a sf_col# classname. First remove the sf_colsOut to prevent wrong indexOf result                                  
            var className = div.className.replace("sf_colsOut", "");
            if (className.indexOf("sf_col") < 0) {
                outerCols[i] = div;
            }
            else {
                //Determine which col it is and place it in the palceholders array accordingly                   
                var index = className.charAt(className.indexOf("sf_col") + "sf_col".length);
                if (!isNaN(index)) outerCols[index - 1] = div; //First col is markes sf_col1, so it has to be at position 0!
                else outerCols[i] = div; //Revert to default behavior...
            }
        }
        //alert(outerCols[0].style.cssText + " " + outerCols[1].style.cssText + " " + outerCols[2].style.cssText);
        return outerCols;
    },

    _getSizesControlPanelInfo: function () {
        var controlPanelInfo = {
            'SizesInPercentagesRadioId': this._sizesInPercentagesRadioId,
            'SizesInPixelsRadioId': this._sizesInPixelsRadioId,
            'ColumnWidthsContainerId': this._columnWidthsContainerId,
            'ColumnLabel': this._columnLabel,
            'AutoSizedLabel': this._autoSizedLabel,
            'MakeThisAutoSizedLabel': this._makeThisAutoSizedLabel,
            'ChangeAutoSizedColumnLabel': this._changeAutoSizedColumnLabel,
            'CancelChangeAutoSizedColumnLabel': this._cancelChangeAutoSizedColumnLabel,
            'AutoSizedColumnButtonId': this._autoSizedColumnButtonId,
            'AValueBetweenLabel': this._aValueBetweenLabel
        };
        return controlPanelInfo;
    },

    _getSpacesControlPanelInfo: function () {
        var controlPanelInfo = {
            'SpacesInPercentagesRadioId': this._spacesInPercentagesRadioId,
            'SpacesInPixelsRadioId': this._spacesInPixelsRadioId,
            'SimpleSpacesContainerId': this._simpleSpacesContainerId,
            'AdvancedSpacesContainerId': this._advancedSpacesContainerId,
            'SpacesSideBySideButtonId': this._spacesSideBySideButtonId,
            'EqualSpacesButtonId': this._equalSpacesButtonId,
            'AdvancedMarginsContainerId': this._advancedMarginsContainerId,
            'HorizontalSpaceColumnsId': this._horizontalSpaceColumnsId,
            'VerticalSpaceColumnsId': this._verticalSpaceColumsId,
            'ColumnLabel': this._columnLabel,
            'TopLabel': this._topLabel,
            'RightLabel': this._rightLabel,
            'BottomLabel': this._bottomLabel,
            'LeftLabel': this._leftLabel,
            'AValueBetweenLabel': this._aValueBetweenLabel
        };
        return controlPanelInfo;
    },

    _getAppearanceControlPanelInfo: function () {
        var controlPanelInfo = {
            'ClassesContainerId': this._classesContainerId,
            'WrapperCssClassTextboxId': this._wrapperCssClassTextboxId,
            'ColumnLabel': this._columnLabel,
            'AValueBetweenLabel': this._aValueBetweenLabel,
            'PlaceholdersLabelsContainerId': this._placeholdersLabelsContainerId
        };
        return controlPanelInfo;
    },



    get_labelManager: function () {
        return this._labelManager;
    },

    set_labelManager: function (value) {
        this._labelManager = value;
    },

    /* ***************************** utility methods ************************************** */
    _getY: function (oElement) {
        var iReturnValue = 0;
        while (oElement != null) {
            iReturnValue += oElement.offsetTop;
            oElement = oElement.offsetParent;
        }
        return iReturnValue;
    },

    _getX: function (oElement) {
        var iReturnValue = 0;
        while (oElement != null) {
            iReturnValue += oElement.offsetLeft;
            oElement = oElement.offsetParent;
        }
        return iReturnValue;
    }

};

Telerik.Sitefinity.Web.UI.LayoutEditor.registerClass('Telerik.Sitefinity.Web.UI.LayoutEditor', Sys.UI.Control);




