
Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI");

Telerik.Sitefinity.Modules.Pages.Web.UI.PageExplorer = function(element) {
    Telerik.Sitefinity.Modules.Pages.Web.UI.PageExplorer.initializeBase(this, [element]);

    this._sitefinityLocalization = null;

    var clickedDataItem = null;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.PageExplorer.prototype =
    {
        initialize: function () {
            Telerik.Sitefinity.Modules.Pages.Web.UI.PageExplorer.callBaseMethod(this, "initialize");

            if (this._sitefinityLocalization) {
                this._sitefinityLocalization = Sys.Serialization.JavaScriptSerializer.deserialize(this._sitefinityLocalization);
            }
            var tree = this.get_tree();
            if (tree) {
                tree.add_nodeClicked(Function.createDelegate(this, this._onNodeClicked));
            }

            var grid = this.get_grid();
            if (grid) {
                grid.add_rowSelected(Function.createDelegate(this, this._onGridRowSelected));
                grid.add_rowDeselected(Function.createDelegate(this, this._onGridRowDeselected));
                grid.add_rowSelecting(Function.createDelegate(this, this._onGridRowSelecting));
                grid.add_rowDeselecting(Function.createDelegate(this, this._onGridRowDeselecting));
                grid.add_rowClick(Function.createDelegate(this, this._onGridRowClick));
            }

            var manager = this.get_windowManager();
            if (manager != null) {
                manager.add_close(Function.createDelegate(this, this._close));
            }

            var menu = this.get_pagesContextMenu();
            if (menu != null) {
                menu.add_itemClicked(Function.createDelegate(this, this._onMenuItemClicked));
            }

            //Get a breadcrumb and message objects
            var breadcrumb = this.get_breadcrumb();
            var messageControl = this.get_message();
            var gridEl = grid.get_element();
            if (gridEl) {
                gridEl.parentNode.insertBefore(breadcrumb.get_element(), gridEl);
                gridEl.parentNode.insertBefore(messageControl.get_element(), gridEl);
            }

            //Subscribe to breadcrumb event
            var oThis = this;
            breadcrumb.add_click(function (sender, args) {
                if (args) {
                    var value = args.value;
                    if (value) oThis.loadFolder(value);
                }
            });

            //Hook the breadcrumb control to data
            this.add_folderLoaded(Function.createDelegate(this, this.onClientFolderLoaded));

            //Call the method for the first time as well
            this.onClientFolderLoaded(this);

            var toolbar = this.get_toolbar();
            if (toolbar) {
                var item = toolbar.findItemByValue("SearchPages");
                if (item) {
                    var itemEl = item.get_element();
                    if (itemEl) {
                        itemEl.className += " sfSeparator";
                    }
                }
            }

            var splitter = this.get_splitter();
            if (splitter) {
                splitter.add_loaded(Function.createDelegate(this, this._onSplitterLoaded));
            }

            this._setToolbarButtonsEnabledState(grid);
        },

        _onSplitterLoaded: function (sender, args) {
            sender.get_element().className += " sfpeGridWrapper";
        },

        _onNodeClicked: function (sender, args) {
            $('html').animate({ scrollTop: 0 }, 500);
        },

        _onGridRowSelected: function (grid, args) {
            this._setToolbarButtonsEnabledState(grid);
        },

        _onGridRowDeselected: function (grid, args) {
            this._setToolbarButtonsEnabledState(grid);
        },

        _onGridRowSelecting: function (grid, args) {
            this._gridRowHandler(grid, args, true);
        },

        _onGridRowDeselecting: function (grid, args) {
            this._gridRowHandler(grid, args, false);
        },

        _close: function (sender, args) {
            var wndId = sender.get_id();
            var argument = args.get_argument();
            var arguments = null;

            if (argument == null || typeof (argument) != "string") {
                //event canceled - return
                return false;
            }

            if (argument.indexOf("reload") > -1) {
                var oThis = this;
                var path = null;
                var idx = this.get_initialPath().indexOf("/");

                if (idx > -1) {
                    path = this.get_initialPath();
                }
                else {
                    path = this.get_currentDirectory();
                    idx = path.indexOf("/");
                    if (idx > -1) {
                        path = path.substring(0, idx);
                    }
                }

                window.setTimeout(function () {
                    // TODO: this approach should be optimized, now we always reload the root folder.
                    oThis.clearFolderCache();
                    oThis.loadFolder(path, false);
                }, 100);

                idx = argument.indexOf("***");
                if (idx > -1) {
                    var msg = argument.substring(idx + 3);
                    var messageControl = this.get_message();
                    if (messageControl) {
                        messageControl.showPositiveMessage(msg);
                        // TODO: Most probably the Message control itself should clear the message text
                        messageControl.set_messageText("");
                    }
                }
            }
            else if (argument.indexOf("redirect") > -1) {
                this._showLoadingPanel(this.get_gridPane().get_id());
            }
        },

        _onGridRowClick: function (grid, args) {
            var e = args.get_domEvent();
            var target = e.target ? e.target : e.srcElement;
            var dataItem = args.get_gridDataItem().get_dataItem();

            if (target && target.className.indexOf("rfeFileExtension") > -1) {
                var url = dataItem["Url"];
                if (url) {
                    window.location.href = url;
                }
                else {
                    var itemProperties = {
                        "type": (dataItem["Length"] == null ? Telerik.Web.UI.FileExplorerItemType.Directory : Telerik.Web.UI.FileExplorerItemType.File),
                        "permissions": dataItem["Permissions"],
                        "name": dataItem["Name"],
                        "path": dataItem["Path"],
                        "size": dataItem["Length"],
                        "url": dataItem["Path"]
                    };
                    var gridItem = $create(Telerik.Web.UI.FileExplorerItem, itemProperties, null, null, null);
                    this.open(gridItem);
                }
            }
        },

        _gridRowHandler: function (grid, args, isSelecting) {
            var e = args.get_domEvent();
            var target = e.target ? e.target : e.srcElement;
            var currentItem = args.get_gridDataItem();
            this.clickedDataItem = currentItem.get_dataItem();
            var attributes = this.clickedDataItem.Attributes;

            if (target && target.className == "sfPageBrowserActions") {
                this._actionsClickHandler(target, attributes);
            }

            if (target && target.id.indexOf("ClientSelectColumnSelectCheckBox") > -1) {
                // do nothing if the target is the checkbox
            }
            else {
                var isSelected = currentItem.get_selected();
                if (isSelecting && !isSelected) args.set_cancel(true);
                else if (!isSelecting && isSelected) args.set_cancel(true);
            }
        },

        _showWindow: function (wndUrl, wndId, dialogMode, addCurrDir, addInitPath, id, optionalAdditionalQueryStringParams) {
            var manager = this.get_windowManager();
            var currDir = this.get_currentDirectory();
            var initPath = this.get_initialPath();

            window.setTimeout(function () {
                var url = wndUrl;
                if (wndUrl.indexOf("?") > -1) {
                    url += "&dialogMode=" + dialogMode
                }
                else {
                    url += "?dialogMode=" + dialogMode;
                }

                if (addCurrDir == true) {
                    url += "&currDir=" + currDir;
                }
                if (addInitPath == true) {
                    url += "&initialPath=" + initPath;
                }
                if (id) {
                    url += "&id=" + id;
                }

                if ((typeof (optionalAdditionalQueryStringParams) != "undefined") && (optionalAdditionalQueryStringParams != null) && (optionalAdditionalQueryStringParams != "")) {
                    if (optionalAdditionalQueryStringParams.indexOf("&") != 0)
                        url += "&";
                    url += optionalAdditionalQueryStringParams;
                }

                var wnd = manager.open(
                    url,
                    manager.get_id() + "_" + wndId);
                wnd.maximize();
            }, 10);
        },

        _actionsClickHandler: function (target, attributes) {
            var menu = this.get_pagesContextMenu();

            if (menu != null) {
                menu.get_items().clear();

                for (var i in attributes) {
                    if (i.match("Command")) {
                        var attr = attributes[i];
                        var menuItem = new Telerik.Web.UI.RadMenuItem();
                        var separator = new Telerik.Web.UI.RadMenuItem();
                        separator.set_isSeparator(true);
                        var textSeparator = new Telerik.Web.UI.RadMenuItem();
                        var text = null;

                        if (attr.indexOf(":") > -1) {
                            var arr = attr.split(":");
                            if (arr.length == 2) {
                                text = arr[0];
                                menuItem.set_navigateUrl(arr[1]);
                            }
                        }
                        else {
                            text = attr;
                        }

                        menuItem.set_text(text);
                        menuItem.set_value(i.replace("Command", ""));

                        if (i == "PagePropertiesCommand" || i == "SectionPropertiesCommand") {
                            menu.get_items().add(separator);
                            // TODO: the text should be localizable
                            textSeparator.set_text("Edit...");
                            menu.get_items().add(textSeparator);
                        }

                        menu.get_items().add(menuItem);

                        if (i == "OwnerCommand") {
                            menu.get_items().add(separator);
                            // TODO: the text should be localizable
                            textSeparator.set_text("Select another...");
                            menu.get_items().add(textSeparator);
                        }

                        if (textSeparator.get_element()) {
                            textSeparator.get_element().className += " sfMenuTitle";
                        }
                    }
                }

                var bounds = $telerik.getBounds(target);
                window.setTimeout(function () {
                    menu.showAt(bounds.x, bounds.y + bounds.height);
                }, 10);
            };
        },

        _onMenuItemClicked: function (menu, args) {
            var id = this.clickedDataItem.Attributes["Id"];
            var menuItem = args.get_item().get_value();

            switch (menuItem) {
                case "Delete":
                    // TODO: the use of private methods is not a good approach.
                    this.deleteItem(this._findItemByPath(this._getGridDataItemPath(this.clickedDataItem)));
                    break;
                case "PageProperties":
                    this._showWindow(this.get_dialogUrl(), "pageDialog", "Page", true, true, id);
                    break;
                case "SectionProperties":
                    this._showWindow(this.get_sectionDialogUrl(), "sectionDialog", "Section", true, true, id);
                    break;
                case "Template":
                    this._showWindow(this.get_dialogUrl(), "templateDialog", "Template", true, true, id);
                    break;
                case "Permissions":
                    var isSection = (typeof (this.clickedDataItem.Length) == "undefined");
                    var managerClassName = ((isSection) ? this.get_sectionManagerType() : this.get_pageManagerType());
                    var securedObjectTypeName = ((isSection) ? this.get_sectionSecuredObjectType() : this.get_pageDataSecuredObjectType());

                    var additionalParams =
                        "&" + "securedObjectID=" + id +
                        "&" + "managerClassName=" + managerClassName +
                        "&" + "title=" + this.clickedDataItem.Name +
                        "&" + "securedObjectTypeName=" + securedObjectTypeName +
                        "&" + "showPermissionSetNameTitle=true";
                    this._showWindow(this.get_permissionsDialogUrl(), "permissionsDialog", "Template", false, false, id, additionalParams);
                    break;
                case "SetHomepage":
                    var _urlParams = [];
                    var _keys = [];
                    var clientManager = new Telerik.Sitefinity.Data.ClientManager();
                    clientManager.InvokePut(this.get_pagesServiceUrl() + "HomePage/Set/", _urlParams, _keys, id, this._setHomepageSuccess, this._setHomepageFailure);
                    break;
            }

            this.clickedDataItem = null;
        },

        // function that is called upon successful saving of the properties
        _setHomepageSuccess: function (sender, result) {
            // Do nothing
        },

        // function that is called if properties were not save successfully
        _setHomepageFailure: function (result) {
            alert(result.Detail);
        },

        onClientFolderLoaded: function (sender, args) {

            var breadcrumb = sender.get_breadcrumb();
            //Get selected tree node
            var tree = this.get_tree();
            var node = tree.get_selectedNode();
            if (!node) return;

            if (node.get_level() == 0) {
                breadcrumb.set_visible(false);
                return;
            }

            breadcrumb.set_visible(true);

            //Make a list of nodes
            var array = [];
            while (node != tree) {
                array[array.length] = {
                    text: node.get_text(),
                    value: node.get_value()
                };
                node = node.get_parent();
            }

            //Reverse the array and feed it to the breadcrumb control
            array = array.reverse();
            breadcrumb.set_list(array);
        },

        isPage: function (item) {
            if (item["Length"] != null) {
                return true;
            }

            return false;
        },

        _setToolbarButtonEnabledState: function (buttonValue, toEnable) {
            var toolbar = this.get_toolbar();
            if (toolbar != null) {
                var item = toolbar.findItemByValue(buttonValue);
                if (item) {
                    item.set_enabled(toEnable);
                }
            }
        },

        _setToolbarButtonsEnabledState: function (grid) {
            var selectedItem = this.get_selectedItem();
            var selectedGridItems = grid.get_selectedItems();
            var isPage = true;

            for (var i = 0, len = selectedGridItems.length; i < len; i++) {
                var item = selectedGridItems[i];
                if (this.isPage(item.get_dataItem()) == false) {
                    isPage = false;
                    break;
                }
            }

            var enable = (selectedItem && selectedItem.get_path() != this.get_initialPath());
            var enablePageAction = (selectedGridItems.length > 0 && isPage);

            this._setToolbarButtonEnabledState("Delete", enable);
            this._setToolbarButtonEnabledState("Publish", enablePageAction);
            this._setToolbarButtonEnabledState("ReorderPages", enablePageAction);
            this._setToolbarButtonEnabledState("PageProperties", enablePageAction);
            this._setToolbarButtonEnabledState("Owner", enable);
            this._setToolbarButtonEnabledState("Template", enablePageAction);
            this._setToolbarButtonEnabledState("Parent", enable);
        },

        /* ======================================= Overridden Methods ======================================= */

        // hack file exploere so that
        get_localization: function () {
            if (this._sitefinityLocalization && this._localization) {
                for (var msg in this._sitefinityLocalization) {
                    if (this._sitefinityLocalization.hasOwnProperty(msg) &&
                        this._localization.hasOwnProperty(msg)) {
                        // for overridden messages only
                        if (this._sitefinityLocalization[msg] != this._localization[msg]) {
                            this._localization[msg] = this._sitefinityLocalization[msg];
                        }
                    }
                }
                return this._localization;
            }
            else {
                return Telerik.Sitefinity.Modules.Pages.Web.UI.PageExplorer.callBaseMethod(this, "get_localization");
            }
        },

        toolbarClicked: function (sender, args) {
            Telerik.Sitefinity.Modules.Pages.Web.UI.PageExplorer.callBaseMethod(this, "toolbarClicked", [sender, args]);

            var buttonValue = args.get_item().get_value();

            switch (buttonValue) {
                case "CreatePage":
                    this._showWindow(this.get_dialogUrl(), "pageDialog", "Page", true, true);
                    break;
                case "CreateSection":
                    this._showWindow(this.get_sectionDialogUrl(), "sectionDialog", "Section", true, true);
                    break;
                case "CreateTemplate":
                    this._showWindow(this.get_templateDialogUrl(), "templPropertiesDialog", "TemplateProperties", false, false);
                    break;
                case "Publish":
                case "ReorderPages":
                case "PageProperties":
                case "Owner":
                case "Parent":
                    alert("In process of implementation.");
                    break;
                case "Template":
                    var id = this.clickedDataItem.Attributes["Id"];
                    this._showWindow(this.get_dialogUrl(), "templateDialog", "Template", true, true, id);
                    this.clickedDataItem = null;
                    break;
                case "PagePermissions":
                    var additionalParams =
                        "&" + "securedObjectID=" + this.get_frontendPagesRootId() +
                        "&" + "managerClassName=" + this.get_sectionManagerType() +
                        "&" + "title=" + this.get_permissionsTitleForFrontend() +
                        "&" + "securedObjectTypeName=" + this.get_sectionSecuredObjectType() +
                        "&" + "showPermissionSetNameTitle=true";
                    this._showWindow(this.get_permissionsDialogUrl(), "permissionsDialog", "Template", false, false, this.get_frontendPagesRootId(), additionalParams); break;
            }
        },

        _onGridRowDataBound: function (grid, args) {
            Telerik.Sitefinity.Modules.Pages.Web.UI.PageExplorer.callBaseMethod(this, "_onGridRowDataBound", [grid, args]);

            var tr = args.get_item("Name").get_element();
            var dataItem = args.get_dataItem();
            var attributes = dataItem.Attributes;

            if (tr) {
                var cell = tr.insertCell(0);
                var checkbox = document.createElement("input");
                checkbox.type = "checkbox";
                checkbox.setAttribute("id", "ClientSelectColumnSelectCheckBox");
                checkbox.setAttribute("name", "ClientSelectColumnSelectCheckBox");
                cell.appendChild(checkbox);

                var cell1 = tr.cells[1];
                if (cell1) {
                    var div = document.createElement("div");
                    div.innerHTML = attributes["Info"];
                    div.className = "sfpePageStatus";
                    cell1.firstChild.appendChild(div);

                    var size = dataItem["Length"];
                    var cssClass = null;
                    if (size == null) {
                        cssClass = "sfpePageGroup";
                    }
                    else {
                        if (size == -1) {
                            cssClass = "sfpeHome";
                        }
                        else {
                            cssClass = "sfpePage";
                        }
                    }

                    cell1.className = cssClass;
                }

                var cell2 = tr.cells[2];
                if (cell2) {
                    cell2.innerHTML = "";
                    cell2.className = "sfpeActions";

                    var actions = document.createElement("span");
                    // TODO: the text should be localizable
                    actions.innerHTML = "Actions";
                    actions.className = "sfPageBrowserActions";
                    cell2.appendChild(actions);
                }

                var cell4 = tr.cells[4];
                if (cell4) {
                    cell4.className = "sfpeTemplate";
                    if (this.isPage(dataItem)) {
                        var templateUrl = dataItem.Attributes["TemplateUrl"];
                        if (templateUrl) {
                            var html = cell4.innerHTML;
                            cell4.innerHTML = "";

                            var editTemplate = document.createElement("a");
                            editTemplate.innerHTML = html;
                            editTemplate.setAttribute("href", templateUrl);
                            cell4.appendChild(editTemplate);
                        }
                        else {
                            var templateTitle = dataItem.Attributes["TemplateTitle"];
                            if (templateTitle) {
                                cell4.innerHTML = templateTitle;
                            }
                            else {
                                // TODO: Localization
                                cell4.innerHTML = "No template selected";
                            }
                        }
                    }
                }

                var cell5 = tr.cells[5];
                if (cell5) {
                    cell5.className = "sfpeAuthor";
                }

                var cell6 = tr.cells[6];
                if (cell6) {
                    cell6.className = "sfpeDate";
                }

                var cell7 = tr.cells[7];
                var url = dataItem["Url"];
                if (cell7 && url) {
                    cell7.className = "sfpeView";

                    var view = document.createElement("a");
                    // TODO: the text should be localizable
                    view.innerHTML = "View";
                    var editPattern = new RegExp("/Action/Edit", "i"); //case insensitive pattern
                    view.setAttribute("href", url.replace(editPattern, ""));
                    view.setAttribute("target", "_blank");
                    cell7.appendChild(view);
                }
            }

            // Deletes empty cells.
            if (tr.cells.length > 3) {
                tr.deleteCell(3);
            }
        },

        _onTreeContextMenuItemClicked: function (sender, args) {
            Telerik.Sitefinity.Modules.Pages.Web.UI.PageExplorer.callBaseMethod(this, "_onTreeContextMenuItemClicked", [sender, args]);

            //Get the node for which the context menu was activated
            var treeNode = args.get_node();
            var menuItem = args.get_menuItem().get_value();
            switch (menuItem) {
                case "SectionProperties":
                    //this._showWindow(this.get_sectionDialogUrl(), "sectionDialog", "Section", true, true, id);
                    break;
            }
        },

        _onGridRowDblClick: function (grid, args) {
            // Disable double click in the grid
            return false;
        },

        set_currentDirectory: function (value, addToStack) {
            Telerik.Sitefinity.Modules.Pages.Web.UI.PageExplorer.callBaseMethod(this, "set_currentDirectory", [value, addToStack]);
            var grid = this.get_grid();
            if (grid) {
                this._setToolbarButtonsEnabledState(grid);
            }
        }
    }

$telerik.$.registerControlProperties(Telerik.Sitefinity.Modules.Pages.Web.UI.PageExplorer, {
    pagesContextMenu: null,
    breadcrumb: null,
    message: null,
    initialPath: null,
    dialogUrl: null,
    templateDialogUrl: null, 
    splitter: null,
    sectionDialogUrl: null,
    //for permissions management:
    permissionsDialogUrl: null,
    pageManagerType: null,
    sectionManagerType: null,
    frontendPagesRootId: null,
    permissionsTitleForFrontend:null,
    pageDataSecuredObjectType: null,
    sectionSecuredObjectType: null,
    pagesServiceUrl: null
});

Telerik.Sitefinity.Modules.Pages.Web.UI.PageExplorer.registerClass("Telerik.Sitefinity.Modules.Pages.Web.UI.PageExplorer", Telerik.Web.UI.RadFileExplorer);
  