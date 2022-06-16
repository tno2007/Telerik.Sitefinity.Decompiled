
Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI");

Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateExplorer = function (element) {
    Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateExplorer.initializeBase(this, [element]);
    this._sitefinityLocalization = null;
    var clickedDataItem = null;

    //Localization
    this._templateExplorerLocalization = null;
    this._deserializedLocalizationKeys = false;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateExplorer.prototype =
    {
        initialize: function () {
            Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateExplorer.callBaseMethod(this, "initialize");

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

        get_templateExplorerLocalization: function () {
            if (!this._deserializedLocalizationKeys && this._templateExplorerLocalization) {
                this._deserializedLocalizationKeys = true;
                this._templateExplorerLocalization = Sys.Serialization.JavaScriptSerializer.deserialize(this._templateExplorerLocalization);
            }
            return this._templateExplorerLocalization;
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
                window.setTimeout(function () { oThis.refresh(); }, 100);

                var idx = argument.indexOf("***");
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
            if (target && target.className == "sfteTemplatePages") {
                var manager = this.get_windowManager();
                var url = this.get_templatePagesDialogUrl();
                if (this.get_templatePagesDialogUrl().indexOf("?") > -1) {
                    url += "&id=" + attributes["Id"];
                }
                else {
                    url += "?id=" + attributes["Id"];
                }

                window.setTimeout(function () {
                    var wnd = manager.open(
                    url,
                    manager.get_id() + "_templatePagesDialog");
                    wnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                    wnd.set_modal(false);
                    wnd.setSize(370, 295);
                    var popupElement = wnd.get_popupElement();
                    if (popupElement) {
                        popupElement.className += " sfSimpleSelector";
                    }
                    wnd.set_visibleTitlebar(true);
                }, 10);
            }

            if (target && target.id.indexOf("ClientSelectColumnSelectCheckBox") > -1) {
                // do nothing if the target is the checkbox
            }
            else {
                //If a row was not arleady selected, cancel the event
                //Else, do not cancel the event
                var isSelected = currentItem.get_selected();
                if (isSelecting && !isSelected) args.set_cancel(true);
                else if (!isSelecting && isSelected) args.set_cancel(true);
            }
        },

        _showWindow: function (wndUrl, wndId, dialogMode, id, optionalAdditionalQueryStringParams) {
            var manager = this.get_windowManager();

            window.setTimeout(function () {
                var url = wndUrl;
                if (wndUrl.indexOf("?") > -1) {
                    url += "&dialogMode=" + dialogMode
                }
                else {
                    url += "?dialogMode=" + dialogMode;
                }

                if (id) {
                    url = url + "&id=" + id;
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

                        if (i == "TemplatePropertiesCommand") {
                            menu.get_items().add(separator);
                            textSeparator.set_text(this.get_templateExplorerLocalization()["Edit"]);
                            menu.get_items().add(textSeparator);
                        }

                        menu.get_items().add(menuItem);

                        if (i == "OwnerCommand") {
                            menu.get_items().add(separator);
                            textSeparator.set_text(this.get_templateExplorerLocalization()["SelectAnother"]);
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
                case "TemplateProperties":
                    this._showWindow(this.get_dialogUrl(), "templPropertiesDialog", "TemplateProperties", id);
                    break;
                case "ParentTemplate":
                    this._showWindow(this.get_dialogUrl(), "parentTemplDialog", "ChangeParentTemplate", id);
                    break;
                case "Permissions":
                    var additionalParams =
                        "&" + "securedObjectID=" + id +
                        "&" + "managerClassName=" + this.get_pageManagerType() +
                        "&" + "title=" + this.clickedDataItem.Name +
                        "&" + "securedObjectTypeName=" + this.get_pageDataSecuredObjectType() +
                        "&" + "showPermissionSetNameTitle=true";
                    this._showWindow(this.get_permissionsDialogUrl(), "permissionsDialog", "Template", id, additionalParams);
                    break;
            }

            this.clickedDataItem = null;
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
            var selectedItems = grid.get_selectedItems();
            var enable = (selectedItems.length > 0);

            this._setToolbarButtonEnabledState("Delete", enable);
            this._setToolbarButtonEnabledState("TemplateProperties", enable);
            this._setToolbarButtonEnabledState("Owner", enable);
            this._setToolbarButtonEnabledState("ParentTemplate", enable);
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
            Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateExplorer.callBaseMethod(this, "toolbarClicked", [sender, args]);

            var id = null;
            if (this.clickedDataItem) {
                id = this.clickedDataItem.Attributes["Id"];
                this.clickedDataItem = null;
            }

            var buttonValue = args.get_item().get_value();

            switch (buttonValue) {
                case "CreateTemplate":
                    this._showWindow(this.get_dialogUrl(), "templPropertiesDialog", "TemplateProperties");
                    break;
                case "TemplateProperties":
                    if (id != null) {
                        this._showWindow(this.get_dialogUrl(), "templPropertiesDialog", "TemplateProperties", id);
                    }
                    break;
                case "Owner":
                    alert("In process of implementation.");
                    break;
                case "ParentTemplate":
                    if (id != null) {
                        this._showWindow(this.get_dialogUrl(), "parentTemplDialog", "ChangeParentTemplate", id);
                    }
                    break;
            }
        },

        _onGridRowDataBound: function (grid, args) {
            Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateExplorer.callBaseMethod(this, "_onGridRowDataBound", [grid, args]);

            var tr = args.get_item("Name").get_element();
            var dataItem = args.get_dataItem();
            var attributes = dataItem.Attributes;

            if (tr) {
                var html = null;

                var cell = tr.insertCell(0);
                var checkbox = document.createElement("input");
                checkbox.type = "checkbox";
                checkbox.setAttribute("id", "ClientSelectColumnSelectCheckBox");
                checkbox.setAttribute("name", "ClientSelectColumnSelectCheckBox");
                cell.appendChild(checkbox);

                var cell1 = tr.cells[2];
                if (cell1) {
                    cell1.className = "sfpePage";
                }

                var cell2 = tr.cells[3];
                if (cell2) {
                    cell2.innerHTML = "";
                    cell2.className = "sfpeActions";

                    var actions = document.createElement("span");
                    actions.innerHTML = this.get_templateExplorerLocalization()["Actions"];
                    actions.className = "sfPageBrowserActions";
                    cell2.appendChild(actions);
                }

                var cell4 = tr.cells[4];
                if (cell4) {
                    cell4.className = "sfpeTemplate";
                    var parentTemplateUrl = dataItem.Attributes["ParentTemplateUrl"];
                    if (parentTemplateUrl) {
                        html = cell4.innerHTML;
                        cell4.innerHTML = "";

                        var editTemplate = document.createElement("a");
                        editTemplate.innerHTML = html;
                        editTemplate.setAttribute("href", parentTemplateUrl);
                        cell4.appendChild(editTemplate);
                    }
                    else {
                        var templateTitle = dataItem.Attributes["ParentTemplate"];
                        if (templateTitle) {
                            cell4.innerHTML = templateTitle;
                        }
                        else {
                            cell4.innerHTML = this.get_templateExplorerLocalization()["NotBasedOnOtherTemplate"];
                        }
                    }
                }

                var cell5 = tr.cells[5];
                if (cell5) {
                    cell5.className = "sfpePageCount";
                    html = cell5.innerHTML;
                    if (html.match("^0") != "0") {
                        cell5.innerHTML = "";

                        var templatePages = document.createElement("span");
                        templatePages.setAttribute("title", this.get_templateExplorerLocalization()["ClickToViewWhichPagesUseThisTemplate"]);
                        templatePages.innerHTML = html;
                        templatePages.className = "sfteTemplatePages";
                        cell5.appendChild(templatePages);
                    }
                }

                var cell6 = tr.cells[6];
                if (cell6) {
                    cell6.className = "sfpeAuthor";
                }

                var cell7 = tr.cells[7];
                if (cell7) {
                    cell7.className = "sfpeDate";
                }
            }

            // Deletes empty cells.
            if (tr.cells.length > 2) {
                tr.deleteCell(1);
            }
        },

        set_currentDirectory: function (value, addToStack) {
            Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateExplorer.callBaseMethod(this, "set_currentDirectory", [value, addToStack]);
            var grid = this.get_grid();
            if (grid) {
                this._setToolbarButtonsEnabledState(grid);
            }
        }
    }

    $telerik.$.registerControlProperties(Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateExplorer, {
        pagesContextMenu: null,
        breadcrumb: null,
        message: null,
        dialogUrl: null,
        templatePagesDialogUrl: null,
        splitter: null,
        //for permissions management:
        permissionsDialogUrl: null,
        pageManagerType: null,
        pageDataSecuredObjectType: null        
    });

    Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateExplorer.registerClass("Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateExplorer", Telerik.Web.UI.RadFileExplorer);
  