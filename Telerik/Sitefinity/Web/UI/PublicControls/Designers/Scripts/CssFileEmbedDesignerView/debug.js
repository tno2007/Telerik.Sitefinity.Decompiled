/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type._registerScript("CssFileEmbedDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views");

Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssFileEmbedDesignerView = function (element) {
    Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssFileEmbedDesignerView.initializeBase(this, [element]);

    this._parentDesigner = null;
    this._urlTextField = null;
    this._mediaChoiceField = null;
    this._mediaTypesChoiceField = null;
    this._selectFileButton = null;
    this._doneSelectButton = null;
    this._cancelSelectButton = null;
    this._fileExplorerPanel = null;
    this._fileExplorer = null;
    this._domain = "http://" + document.domain;
    this._allowedExtension = "css";

    // keys is enum name, value is its int representation
    this._mediaTypesMap = {};

    this._mediaChoiceFieldChangedDelegate = null;
    this._selectFileButtonClickDelegate = null;
    this._doneSelectButtonClickDelegate = null;
    this._cancelSelectButtonClickDelegate = null;
    this._nodePopulatedDelegate = null;
    this._fileExplorerLoadDelegate = null;
    this._loadDelegate = null;
};

Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssFileEmbedDesignerView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssFileEmbedDesignerView.callBaseMethod(this, 'initialize');

        this._mediaChoiceFieldChangedDelegate = Function.createDelegate(this, this._mediaChangeHandler);
        this._mediaChoiceField.add_valueChanged(this._mediaChoiceFieldChangedDelegate);

        this._selectFileButtonClickDelegate = Function.createDelegate(this, this._selectFileClickHandler);
        $addHandler(this._selectFileButton, "click", this._selectFileButtonClickDelegate);

        this._doneSelectButtonClickDelegate = Function.createDelegate(this, this._doneSelectButtonClickHandler);
        $addHandler(this._doneSelectButton, "click", this._doneSelectButtonClickDelegate);

        this._cancelSelectButtonClickDelegate = Function.createDelegate(this, this._cancelSelectButtonClickHandler);
        $addHandler(this._cancelSelectButton, "click", this._cancelSelectButtonClickDelegate);

        this._nodePopulatedDelegate = Function.createDelegate(this, this._nodePopulatedHandler);

        this._fileExplorerLoadDelegate = Function.createDelegate(this, this._fileExplorerLoadHandler);
        this._fileExplorer.add_load(this._fileExplorerLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssFileEmbedDesignerView.callBaseMethod(this, 'dispose');
        if (this._mediaChoiceFieldChangedDelegate) {
            if (this._mediaChoiceField) {
                this._mediaChoiceField.remove_valueChanged(this._mediaChoiceFieldChangedDelegate);
            }
            delete this._mediaChoiceFieldChangedDelegate;
        }

        if (this._selectFileButtonClickDelegate) {
            $removeHandler(this._selectFileButton, "click", this._selectFileButtonClickDelegate);
            delete this._selectFileButtonClickDelegate;
        }

        if (this._doneSelectButtonClickDelegate) {
            $removeHandler(this._doneSelectButton, "click", this._doneSelectButtonClickDelegate);
            delete this._doneSelectButtonClickDelegate;
        }

        if (this._cancelSelectButtonClickDelegate) {
            $removeHandler(this._cancelSelectButton, "click", this._cancelSelectButtonClickDelegate);
            delete this._cancelSelectButtonClickDelegate;
        }

        if (this._fileExplorerLoadDelegate) {
            if (this._fileExplorer && this._fileExplorer.remove_load) {
                this._fileExplorer.remove_load(this._fileExplorerLoadDelegate);
            }
            delete this._fileExplorerLoadDelegate;
        }

        if (this._nodePopulatedDelegate) {
            delete this._nodePopulatedDelegate;
        }

    },

    /* --------------------------------- public methods --------------------------------- */
    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();

        if (controlData.Url) {
            var parentDesigner = this.get_parentDesigner();
            var tab = parentDesigner.get_menuTabStrip().findTabByValue("CssFileEmbedDesignerView");
            tab.select();
            this.get_urlTextField().set_value(controlData.Url);
            if (controlData.MediaType == "all") {
                this.get_mediaChoiceField().set_value("all");
            }
            else {
                this.get_mediaChoiceField().set_value("selectMediaTypes");
                var selectedTypes = [];
                var typesArray = controlData.MediaType.split(",");
                for (var i = 0, length = typesArray.length; i < length; i++) {
                    var enumValue = this._mediaTypesMap[typesArray[i].trim()];
                    if (enumValue) {
                        selectedTypes.push(enumValue);
                    }
                }
                this.get_mediaTypesChoiceField().set_value(selectedTypes);
            }
        }
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var parentDesigner = this.get_parentDesigner();
        var selectedTab = parentDesigner.get_menuTabStrip().get_selectedTab();
        var tabValue = selectedTab.get_value();

        // process only of the current view is the selected one
        if (tabValue == "CssFileEmbedDesignerView") {
            var controlData = this.get_controlData();

            controlData.Url = this.get_urlTextField().get_value();
            controlData.CustomCssCode = "";
            if (this.get_mediaChoiceField().get_value() == "all") {
                controlData.MediaType = 1; //all
            }
            else {
                var mediaType = this.get_mediaTypesChoiceField().get_value();
                if (Array.prototype.isPrototypeOf(mediaType)) {
                    // if multiple types are selected we convert it to a bit mask
                    var mask = 0;
                    for (var i = 0, length = mediaType.length; i < length; i++) {
                        var flag = parseInt(mediaType[i]);
                        mask = mask | flag;
                    }
                    controlData.MediaType = mask;
                }
                else {
                    controlData.MediaType = mediaType;
                }
            }
        }

    },
    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers --------------------------------- */
    // Handles selection change in the media choice field
    _mediaChangeHandler: function (sender, args) {
        var value = sender.get_value();
        if (value == "all") {
            jQuery(this._mediaTypesChoiceField.get_element()).hide();
        }
        else {
            jQuery(this._mediaTypesChoiceField.get_element()).show();
        }
        dialogBase.resizeToContent();
    },

    // Handles the click on the select button
    _selectFileClickHandler: function (e) {
        jQuery(this._fileExplorerPanel).show();
        dialogBase.resizeToContent();
    },

    // Handles the click on the done button
    _doneSelectButtonClickHandler: function (e) {
        var selectedItem = this._fileExplorer.get_selectedItem();
        var selectedFile = null;
        if (selectedItem.get_type() == Telerik.Web.UI.FileExplorerItemType.File) {
            selectedFile = selectedItem.get_path();
            selectedFile = selectedFile.replace("//", "/");
            //selectedFile = this._domain + selectedFile; // this._domain should not be attached to the file path, so the styles can work when the domain is changed (for example after SiteSync)
            this._urlTextField.set_value(selectedFile);
        }

        jQuery(this._fileExplorerPanel).hide();
        dialogBase.resizeToContent();
    },

    // Handles the click on the cancels selection button
    _cancelSelectButtonClickHandler: function (e) {
        jQuery(this._fileExplorerPanel).hide();

    },

    _nodePopulatedHandler: function (sender, args) {
        var currentNode = args.get_node();
        var nodes = currentNode.get_nodes();
        for (var i = 0; i < nodes.get_count(); i++) {
            var node = nodes.getNode(i);
            this._disableNode(node);
        }
    },

    _fileExplorerLoadHandler: function (sender, args) {
        this._overrideExplorerSetCurrentDirectory();
        this._overrideRadSplitterDispose();
        var tree = sender.get_tree();
        if (tree != null) {
            tree.add_nodePopulated(this._nodePopulatedDelegate);
            var nodes = tree.get_allNodes();
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                this._disableNode(node);
            }
        }

    },

    /* --------------------------------- private methods --------------------------------- */
    //HACK: FileExplorerItem set_currentDirectory throws exception because of the $get() implementation of the MicrosoftAjax script that we are using.
    // The call that causes the exception is var 'd=$get(this.get_addressBox());'. The this.get_addressBox() evaluates to "" (we are not showing the addressBox)
    // The $get implementation of the ajax script that we are using appends '#' to the result ("") and calls 'jQuery('#')' which throws exception.
    _overrideExplorerSetCurrentDirectory: function () {
        Telerik.Web.UI.RadFileExplorer.prototype.set_currentDirectory = function (value, addToStack) {
            //add to back/forward stack
            if ((null != this._actionsManager) &&
                (value != this.get_currentDirectory()) &&
                (false != addToStack)
            )
                this._updateBackForward(value); // Is true

            $get(this._currentDirectoryInputID).value = value;
            var tree = this.get_tree();
            node = tree.findNodeByValue(value);
            if (node)
                node.select();

            if (this.get_addressBox() && this.get_addressBox() !== "") {
                $get(this.get_addressBox()).value = value;
            }

            //update toolbar
            this._updateToolbar();
        };
    },

    //HACK to fix File explorer bug on dispose. Remove after upgrade to Q2 2010 of RadControls.
    _overrideRadSplitterDispose: function () {

        var $T = Telerik.Web.UI;
        $T.RadPane.prototype.dispose = function () {
            this._attachScrollHandler(false);
            this._prevSplitBar = null;
            this._nextSplitBar = null;
            this._contentElement = null;

            $T.RadSplitterController.getInstance()._removePane(this.get_id());
            $T.RadPane.callBaseMethod(this, 'dispose');
        };

        $T.SplitterPaneBase.prototype._attachScrollHandler = function (toRemove) {
            var contentElement = this._getContentElement();
            if (contentElement) {
                var scrollHandler = this._scrollHandler;
                if (scrollHandler) {
                    if (toRemove) {
                        $telerik.removeExternalHandler(contentElement, "scroll", scrollHandler);
                        this._scrollHandler = null;
                    }
                }
                else if (!toRemove) {
                    this._scrollHandler = Function.createDelegate(this, this._onScroll);
                    $telerik.addExternalHandler(contentElement, "scroll", this._scrollHandler);
                }
            }
        };
    },

    _disableNode: function (node) {
        if (node != null) {
            var fileItem = this._fileExplorer.getFileExplorerItemFromNode(node);
            if (fileItem != null &&
                !fileItem.isDirectory() &&
                this._getExtension(fileItem.get_name()).toLowerCase() != this._allowedExtension) {
                node.disable();
            }
        }
    },

    _getExtension: function (name) {
        var idx = name.lastIndexOf(".");
        var extension = "";

        if (idx > -1) {
            extension = name.substring(idx + 1);
        }
        return extension;
    },

    /* --------------------------------- properties --------------------------------- */

    // IDesignerViewControl: gets the reference fo the propertyEditor control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },
    // IDesignerViewControl: sets the reference fo the propertyEditor control
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
    },
    get_propertyEditor: function () {
        if (this.get_parentDesigner()) {
            return this.get_parentDesigner().get_propertyEditor();
        }
        return null;
    },

    // Gets the text field that holds the Url for the style sheet
    get_urlTextField: function () {
        return this._urlTextField;
    },
    // Sets the text field that holds the Url for the style sheet
    set_urlTextField: function (value) {
        this._urlTextField = value;
    },

    // Gets the choices between all media types and specific ones
    get_mediaChoiceField: function () {
        return this._mediaChoiceField;
    },
    // Sets the choices between all media types and specific ones
    set_mediaChoiceField: function (value) {
        this._mediaChoiceField = value;
    },

    // Gets the media type choices
    get_mediaTypesChoiceField: function () {
        return this._mediaTypesChoiceField;
    },
    // Sets the media type choices
    set_mediaTypesChoiceField: function (value) {
        this._mediaTypesChoiceField = value;
    },

    // Gets the file explorer component
    get_fileExplorer: function () {
        return this._fileExplorer;
    },
    // Sets the file explorer component
    set_fileExplorer: function (value) {
        this._fileExplorer = value;
    },

    // Gets the select button element
    get_selectFileButton: function () {
        return this._selectFileButton;
    },
    // Sets the select button element
    set_selectFileButton: function (value) {
        this._selectFileButton = value;
    },

    // Gets the panel that cointains the file explorer
    get_fileExplorerPanel: function () {
        return this._fileExplorerPanel;
    },
    // Sets the panel that cointains the file explorer
    set_fileExplorerPanel: function (value) {
        this._fileExplorerPanel = value;
    },

    // Gets the done selection button element
    get_doneSelectButton: function () {
        return this._doneSelectButton;
    },
    // Sets the done selection button element
    set_doneSelectButton: function (value) {
        this._doneSelectButton = value;
    },

    // Gets the cancel selection button element
    get_cancelSelectButton: function () {
        return this._cancelSelectButton;
    },
    // Sets the cancel selection button element
    set_cancelSelectButton: function (value) {
        this._cancelSelectButton = value;
    }

};

Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssFileEmbedDesignerView.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssFileEmbedDesignerView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();