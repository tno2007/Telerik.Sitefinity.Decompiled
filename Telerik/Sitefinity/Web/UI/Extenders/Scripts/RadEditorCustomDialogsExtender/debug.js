Type.registerNamespace("Telerik.Sitefinity.Web.UI.Extenders");

Telerik.Sitefinity.Web.UI.Extenders.RadEditorCustomDialogsExtender = function (element) {
    this._editor = null;
    this._imageManagerDialogUrl = null;
    this._documentManagerDialogUrl = null;
    this._mediaManagerDialogUrl = null;
    this._linkManagerDialogUrl = null;
    this._isToOverrideDialogs = null;
    this._uiCulture = null;

    this._radEditorExternalDialogClosedDelegate = null;
    this._editorCommandExecutingDelegate = null;
    this._editorCommandExecutedDelegate = null;
    this._openImageManagerDelegate = null;
    this._openLinkManagerDelegate = null;
    this._openDocumentManagerDelegate = null;
    this._openMediaManagerDelegate = null;
    this._loadDelegate = null;

    Telerik.Sitefinity.Web.UI.Extenders.RadEditorCustomDialogsExtender.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Extenders.RadEditorCustomDialogsExtender.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Extenders.RadEditorCustomDialogsExtender.callBaseMethod(this, "initialize");
        this._radEditorExternalDialogClosedDelegate = Function.createDelegate(this, this._radEditorExternalDialogClosed);
        this._editorCommandExecutingDelegate = Function.createDelegate(this, this._editorCommandExecutingHandler);
        this._editorCommandExecutedDelegate = Function.createDelegate(this, this._editorCommandExecutedHandler);
        this._loadDelegate = Function.createDelegate(this, this._load);
        Sys.Application.add_load(this._loadDelegate);
    },

    dispose: function () {
        if (this._radEditorExternalDialogClosedDelegate) {
            delete this._radEditorExternalDialogClosedDelegate;
        }

        if (this._editorCommandExecutingDelegate) {
            delete this._editorCommandExecutingDelegate;
        }

        if (this._editorCommandExecutedDelegate) {
            delete this._editorCommandExecutedDelegate;
        }

        Telerik.Sitefinity.Web.UI.Extenders.RadEditorCustomDialogsExtender.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    /* -------------------- private methods ----------- */

    _load: function () {
        this._initializeEditorCommandList();
    },

    _initializeEditorCommandList: function () {
        if (this._isToOverrideDialogs) {
            this.get_editor().add_commandExecuting(this._editorCommandExecutingDelegate);
            this.get_editor().add_commandExecuted(this._editorCommandExecutedDelegate);
        }
    },

    _editorCommandExecutingHandler: function (sender, args) {
        if ($telerik.isIE) {
            sender.setActive();
        }
        var commandName = args.get_commandName();

        // Some links are links to documents and DocumentManager needs to be used.
        if (commandName == "SetLinkProperties") {
            var argument = Telerik.Web.UI.Editor.CommandList._getLinkArgument(this.get_editor());
            var sfref = argument.get_value().attributes["sfref"];
            if (sfref && sfref.value && sfref.value.indexOf("[documents|") == 0) {
                commandName = "DocumentManager";
            }
        }

        switch (commandName) {
            case "SetImageProperties":
            case "ImageManager":
                {
                    args.set_cancel(this.openImageManager());
                    break;
                }
            case "DocumentManager":
                {
                    args.set_cancel(this.openDocumentManager());
                    break;
                }
            case "MediaManager":
                {
                    args.set_cancel(this.openMediaManager());
                    break;
                }
            case "LinkManager":
            case "SetLinkProperties":
                {
                    args.set_cancel(this.openLinkManager());
                    break;
                }
            default:
                break;

        }
    },

    _editorCommandExecutedHandler: function (sender, args) {
        var commandName = args.get_commandName();
        if (commandName === "ImageMapDialog") {
            // This is the image map dialog. We need to resize it to be bigger - not confined by the PropertyEditorDialog, because we resize it afterwards
            var imageMapDialog = this.get_editor()._dialogOpener._dialogContainers["ImageMapDialog"];

            this._resizePropertyEditorDialog();
        }
    },

    _resizePropertyEditorDialog: function () {
        var propertyEditorDialog = this.get_editor()._contentWindow.top.$find("PropertyEditorDialog");
        if (!propertyEditorDialog)
            return;

        setTimeout(function () {
            propertyEditorDialog.AjaxDialog.resizeToContent();
        }, 500);
    },

    openImageManager: function () {
        var dialogUrl = this.get_imageManagerDialogUrl();
        if (dialogUrl) {
            var domElement = this._getSelectedOrCreatedImageDomElement();
            //TODO: localize
            var dialogTitle = 'Insert an image';

            this._openManager(dialogUrl, domElement, dialogTitle);
        }
        return !!dialogUrl;
    },

    openDocumentManager: function () {
        var dialogUrl = this.get_documentManagerDialogUrl();
        if (dialogUrl) {
            var argument = Telerik.Web.UI.Editor.CommandList._getLinkArgument(this.get_editor());
            var domElement = argument.get_value();
            //TODO: localize
            var dialogTitle = 'Insert a document';

            this._openManager(dialogUrl, domElement, dialogTitle);
        }
        return !!dialogUrl;
    },

    openMediaManager: function () {
        var dialogUrl = this.get_mediaManagerDialogUrl();
        if (dialogUrl) {
            var domElement = this._getSelectedOrCreatedObjectElement();
            //TODO: localize
            var dialogTitle = 'Insert a video';

            this._openManager(dialogUrl, domElement, dialogTitle);
        }
        return !!dialogUrl;
    },

    openLinkManager: function () {
        var dialogUrl = this.get_linkManagerDialogUrl();
        if (dialogUrl) {
            var argument = Telerik.Web.UI.Editor.CommandList._getLinkArgument(this.get_editor());
            var domElement = argument.get_value();
            //TODO: localize
            var dialogTitle = 'Insert a link';

            this._openManager(dialogUrl, domElement, dialogTitle);
        }
        return !!dialogUrl;
    },

    _openManager: function (dialogUrl, domElement, dialogTitle) {
        this.get_editor().showExternalDialog(
            dialogUrl,
            domElement,
            395,
            250,
            this._radEditorExternalDialogClosedDelegate,
            null,
            dialogTitle,
            true,
            Telerik.Web.UI.WindowBehaviors.Close,
            false,
            true);
    },

    _radEditorExternalDialogClosed: function (sender, args) {
        var commandName = args.get_commandName();
        var argument = args.get_commandArgument();

        switch (commandName) {
            case "pasteLink":
                this.get_editor().pasteHyperLink(argument);
                break;
            case "pasteHtml":
                var html = this._applyFiltersForEmbedContent(argument);
                this.get_editor().pasteHtml(html);
                break;
        }
    },

    _getSelectedOrCreatedImageDomElement: function () {
        var domElement = this.get_editor().getSelectedElement(); //returns the selected element.

        if (domElement && domElement.tagName == "IMG") {
            this.get_editor().selectElement(domElement);
            return domElement;
        }
        else {
            domElement = this.get_editor().get_document().createElement("IMG");
            return domElement;
        }
    },

    _getSelectedOrCreatedLinkDomElement: function () {
        var domElement = this.get_editor().getSelectedElement(); //returns the selected element.
        //if there is a link around the selected element, preserve it.
        if (domElement && domElement.parentNode && domElement.parentNode.tagName == "A") {
            domElement = domElement.parentNode;
        }

        if (domElement && domElement.tagName == "A") {
            this.get_editor().selectElement(domElement);
            return domElement;
        }
        else {
            //remove links if present from the current selection - because of JS error thrown in IE
            this.get_editor().fire("Unlink");

            //remove Unlink command from the undo/redo list
            var commandsManager = this.get_editor().get_commandsManager();
            var commandIndex = commandsManager.getCommandsToUndo().length - 1;
            commandsManager.removeCommandAt(commandIndex);

            var link = this.get_editor().get_document().createElement("A");
            var tagName = (domElement && domElement.tagName) ? domElement.tagName : "";
            var tagNamesThatAreNotOK = [
                "BODY", "TD", "H1", "H2", "H3", "H4", "H5", "TABLE", "P", "LI", "UL", "OL", "DIV",
                "SPAN", "STRONG", "EM", "FONT"
            ];
            if (domElement && !Array.contains(tagNamesThatAreNotOK, tagName)) {
                this.get_editor().selectElement(domElement);
                // appending child to prevent the IE from resolving the relative src and hrefs when we use innerHTML.
                // we also clone the node because so that the node is not removed from the editor's document.
                jQuery(link).append(jQuery(domElement).clone());
            }
            else {
                var content = this.get_editor().getSelectionHtml();
                link.innerHTML = content;
            }

            return link;
        }
    },

    _getSelectedOrCreatedObjectElement: function () {
        var domElement = this.get_editor().getSelectedElement(); //returns the selected element.
        var tempDomElement = this.get_editor().get_document().createElement("DIV");

        if (domElement && (domElement.tagName === "OBJECT" || domElement.tagName === "EMBED" || domElement.tagName === "VIDEO")) {
            tempDomElement.innerHTML = $telerik.getOuterHtml(domElement);
        }
        else if (domElement && domElement.tagName === "IMG") {
            var originalcode = domElement.getAttribute("originalcode");
            if (originalcode) {
                tempDomElement.innerHTML = unescape(originalcode);
            }
        }
        else {
            // do nothing
        }

        return tempDomElement;
    },

    _applyFiltersForEmbedContent: function (html) {
        //optional filters for object/embed content
        var filterIE = this._editor.get_filtersManager().getFilterByName("IEKeepObjectParamsFilter");
        var filterMoz = this._editor.get_filtersManager().getFilterByName("MozillaKeepFlash");
        if (filterIE) html = filterIE.getDesignContent(html);
        if (filterMoz) {
            var tempDiv = this._editor.get_document().createElement("div");
            Telerik.Web.UI.Editor.Utils.setElementInnerHtml(tempDiv, html);
            tempDiv = filterMoz.getDesignContent(tempDiv);
            html = tempDiv.innerHTML;
        }

        return html;
    },

    _applyUiCulture: function (url) {
        if (this.get_uiCulture()) {
            var urlBuilder = new Sys.Uri(url);
            urlBuilder.get_query().uiCulture = this.get_uiCulture();
            return urlBuilder.toString();
        }
        return url;
    },

    /* -------------------- properties ---------------- */

    // Gets the RadEditorElement
    get_editor: function () {
        return this._editor;
    },
    // Sets the RadEditorElement
    set_editor: function (value) {
        this._editor = value;
    },

    get_imageManagerDialogUrl: function () {
        this._imageManagerDialogUrl = this._applyUiCulture(this._imageManagerDialogUrl);
        return this._imageManagerDialogUrl;
    },
    set_imageManagerDialogUrl: function (value) {
        this._imageManagerDialogUrl = value;
    },

    get_documentManagerDialogUrl: function () {
        this._documentManagerDialogUrl = this._applyUiCulture(this._documentManagerDialogUrl);
        return this._documentManagerDialogUrl;
    },
    set_documentManagerDialogUrl: function (value) {
        this._documentManagerDialogUrl = value;
    },

    get_mediaManagerDialogUrl: function () {
        this._mediaManagerDialogUrl = this._applyUiCulture(this._mediaManagerDialogUrl);
        return this._mediaManagerDialogUrl;
    },
    set_mediaManagerDialogUrl: function (value) {
        this._mediaManagerDialogUrl = value;
    },

    get_linkManagerDialogUrl: function () {
        this._linkManagerDialogUrl = this._applyUiCulture(this._linkManagerDialogUrl);
        return this._linkManagerDialogUrl;
    },
    set_linkManagerDialogUrl: function (value) {
        this._linkManagerDialogUrl = value;
    },

    get_isToOverrideDialogs: function () {
        return this._isToOverrideDialogs;
    },
    set_isToOverrideDialogs: function (value) {
        this._isToOverrideDialogs = value;
    },

    // Specifies the culture that will be used on the server as UICulture when processing the request
    set_uiCulture: function (culture) {
        this._uiCulture = culture;
    },
    // Gets the culture that will be used on the server as UICulture when processing the request
    get_uiCulture: function () {
        return this._uiCulture;
    }
};

Telerik.Sitefinity.Web.UI.Extenders.RadEditorCustomDialogsExtender.registerClass("Telerik.Sitefinity.Web.UI.Extenders.RadEditorCustomDialogsExtender", Sys.UI.Behavior);
