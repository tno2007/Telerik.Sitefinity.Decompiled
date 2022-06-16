Type.registerNamespace("Telerik.Sitefinity.Modules.Files.Web.UI");

Telerik.Sitefinity.Modules.Files.Web.UI.FileManager = function (element) {
    Telerik.Sitefinity.Modules.Files.Web.UI.FileManager.initializeBase(this, [element]);

    this._control = null;
    this._gridSelectedFiles = [];

    this._selectFileListDelegate = null;
    this._bindToServerDataDelegate = null;

    this._treeContextClickedDelegate = null;
}

Telerik.Sitefinity.Modules.Files.Web.UI.FileManager.prototype =
    {
        initialize: function () {
            //Hack for RadFileExplorer. replace with event subscription if they provide it
            var gridFileListPrototype = Telerik.Web.UI.FileExplorer.GridFileList.prototype;
            gridFileListPrototype._onGridRowSelectedBaseMethod = gridFileListPrototype._onSelectItem;
            gridFileListPrototype._onSelectItem = this._onGridRowSelectedOverride;
            gridFileListPrototype._onGridRowDataBoundBaseMethod = gridFileListPrototype._onRowDataBound;
            gridFileListPrototype._onRowDataBound = this._onGridRowDataBoundOverride;
            gridFileListPrototype._onRowDblClick = this._onGridRowDblClick;
            gridFileListPrototype.selectDefaultItem = this._selectDefaultItem;
            gridFileListPrototype.SitefinityFileManager = this;
            //-------------------------------------------------------------------------------

            Telerik.Sitefinity.Modules.Files.Web.UI.FileManager.callBaseMethod(this, "initialize");

            this._treeContextClickedDelegate = Function.createDelegate(this, this._onTreeContextMenuClicked);

            var tree = this.get_tree()
            if (tree != null) {
                tree.add_contextMenuItemClicked(this._treeContextClickedDelegate);
            }

            var grid = this.get_grid();

            if (grid != null) {
                grid.add_rowClick(Function.createDelegate(this, this._onGridRowClick));
                grid.add_rowDeselected(Function.createDelegate(this, this._onGridRowDeselected));
                grid.add_rowSelecting(Function.createDelegate(this, this._onGridRowSelecting));
                grid.add_rowDeselecting(Function.createDelegate(this, this._onGridRowDeselecting));
            }

            this._setToolbarButtonsEnabledState();

            this._selectFileListDelegate = Function.createDelegate(this, this._selectFileList);
            this._bindToServerDataDelegate = Function.createDelegate(this, this._bindToServerData);

            this.get_fileList().selectFileList = this._selectFileListDelegate;
            this.get_fileList().bindToServerData = this._bindToServerDataDelegate;

            this.clearGridItems();
        },

        dispose: function () {
            Telerik.Sitefinity.Modules.Files.Web.UI.FileManager.callBaseMethod(this, "dispose");

            if (this._selectFileListDelegate) {
                delete this._selectFileListDelegate;
            }
            if (this._bindToServerDataDelegate) {
                delete this._bindToServerDataDelegate;
            }
            if (this._treeContextClickedDelegate) {
                delete this._treeContextClickedDelegate;
            }
        },

        /* Override from RadFileList.js: fixing when you create a folder, the top folder checkbox is marked */

        _selectFileList: function () {
            this.clearGridItems();
        },

        //this override should be removed with Q3 2012 of Telerik.Web.UI as it will not select the default item on focus any more

        _selectDefaultItem: function () {   
        },

        _bindToServerData: function (serverData, pageIndex) {
            if (serverData && serverData.data && !isNaN(serverData.count)) {
                this.get_fileList()._listControl.bindToServerData(serverData, pageIndex);
            }

            // Dirty hack - we need to readd the handler in order to be able to download freshly created folders.
            var tree = this.get_tree();
            if (tree != null) {
                tree.remove_contextMenuItemClicked(this._treeContextClickedDelegate);
                tree.add_contextMenuItemClicked(this._treeContextClickedDelegate);
            }

            this.clearGridItems();
        },

        /*end of override*/

        _createFolder: function (path, newName) {
            Telerik.Sitefinity.Modules.Files.Web.UI.FileManager.callBaseMethod(this, "_createFolder", [path, newName]);

            this.clearGridItems();
        },

        clearGridItems: function () {
            this.get_fileList().clearSelection();
        },

        _onTreeContextMenuShowing: function (sender, args) {

            Telerik.Sitefinity.Modules.Files.Web.UI.FileManager.callBaseMethod(this, "_onTreeContextMenuShowing", [sender, args]);

            this._control = "tree";
            var node = args.get_node();
            if (node) {
                var toEnableDelete = node.get_parent() != node.get_treeView();
                this._setItemEnabledState(args.get_menu(), "Delete", toEnableDelete);

                var toEnableDownload = this.hasFiles(node);
                this._setItemEnabledState(args.get_menu(), "Download", toEnableDownload);

                var selectedItem = this.getFileExplorerItemFromNode(node);
                this._setToolbarButtonsEnabledState(selectedItem);
            }
        },

        _onTreeContextMenuClicked: function (sender, args) {
            var buttonValue = args.get_menuItem().get_value();

            if (buttonValue == "Download") {
                var node = args.get_node();

                if (node) {
                    var selectedItems = [];
                    Array.add(selectedItems, this.getFileExplorerItemFromNode(node));
                    this.download(selectedItems);
                }
            }
        },

        _onGridRowDeselected: function (grid, args) {
            this._setToolbarButtonsEnabledState();
        },

        _onGridRowSelecting: function (grid, args) {
            this._gridRowHandler(grid, args, true);
        },

        _onGridRowDeselecting: function (grid, args) {
            this._gridRowHandler(grid, args, false);
        },

        _onGridRowClick: function (grid, args) {
            var dataItem = args.get_gridDataItem().get_dataItem();
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
            this.clearGridItems();
        },

        hasFiles: function (node) {

            var attTag = node.get_attributes().getAttribute("Tag");
            if (attTag) {
                var tags = attTag.split(';');
                if (tags) {
                    var tagValue = tags[0].split('=')[1];
                    if (tagValue) {
                        return tagValue.toUpperCase() == "TRUE";
                    }
                }
            }
            return false;
        },

        folderUp: function () {

            var curDirectory = this.get_currentDirectory();
            if (curDirectory) {
                var separator = this.get_pathSeparator();
                var lastIdx = curDirectory.lastIndexOf(separator);
                var parentDirPath = this.get_initialPath();

                if (lastIdx > 0) {
                    parentDirPath = curDirectory.substring(0, lastIdx);
                }

                this.loadFolder(parentDirPath, false);
            }
            else {
                return;
            }
        },

        download: function (selectedItems) {
            var path = "";

            if (selectedItems == null) {
                selectedItems = this.get_selectedItems();
            }
            if (null != selectedItems) {
                var len = selectedItems.length;
                for (var i = 0; i < len; i++) {
                    var item = selectedItems[i];
                    if (item) {
                        path += "&path" + (i + 1) + "=" + encodeURIComponent(item.get_path());
                    }
                }
            }
            if (path != "") {
                document.location.href = this.get_initialPath() + "Telerik.Sitefinity.FilesDownloadHandler.ashx?" + path.substring(1);
            }
        },

        rename: function (selectedItem) {
            if (this._control == null || this._control == "grid") {
                var curFolderName = selectedItem.get_name();
                var message = this.get_localization()["Rename"];

                function renamePropmptCallback(arg) {
                    if (arg) {
                        this._renameItem(selectedItem, arg);
                    }
                }

                var oThis = this;
                var manager = this.get_windowManager();
                //small timeout is added to allow the context menu, which called this function to close properly.
                window.setTimeout(function () {
                    manager.radprompt(message, Function.createDelegate(oThis, renamePropmptCallback), 330, 100, null, message, curFolderName);
                }, 10);
            }
            else if (this._control == "tree") {
                var tree = this.get_tree();
                if (tree) {
                    var node = tree.get_selectedNode();
                    if (node) {
                        node.startEdit();
                    }
                }
            }
        },


        _setToolbarButtonsEnabledState: function (selectedItem) {

            if (selectedItem == null) {
                selectedItem = this.get_selectedItem();
            }
            var rootNode = this.get_tree();
            var rootPath = rootNode.get_nodeData()[0].attributes["Path"]; ;

            var toEnable = (selectedItem != null && selectedItem.get_path() != rootPath);
            var toolbar = this.get_toolbar();

            this._setItemEnabledState(toolbar, "FolderUp", toEnable);
            this._setItemEnabledState(toolbar, "Download", toEnable);
            this._setItemEnabledState(toolbar, "Delete", toEnable);
            this._setItemEnabledState(toolbar, "Rename", toEnable);
            this._setItemEnabledState(toolbar, "Copy", toEnable);
            this._setItemEnabledState(toolbar, "Paste", toEnable);
        },

        _setItemEnabledState: function (container, value, toEnable) {
            if (container == null || container.get_items().get_count() == 0) {
                return;
            }
            var item = container.findItemByValue(value);
            if (item) {
                item.set_enabled(toEnable);
            }
        },

        _gridRowHandler: function (grid, args, isSelecting) {
            var e = args.get_domEvent();
            var target = e.target ? e.target : e.srcElement;

            if (!target) {
                return;
            }

            // do nothing if the target is the checkbox
            if (target.id.indexOf("ClientSelectColumnSelectCheckBox") == -1) {
                var index = args.get_itemIndexHierarchical();
                var tableView = grid.get_masterTableView();
                if (tableView.get_dataItems().length > index) {
                    var currentItem = tableView.get_dataItems()[index];
                    var isSelected = currentItem.get_selected();

                    if (isSelecting !== isSelected) {
                        args.set_cancel(true);
                    }
                }
            }
        },

        /* ======================================= Overridden Methods ======================================= */

        _onGridRowDataBoundOverride: function (grid, args) {
            this._onGridRowDataBoundBaseMethod(grid, args);

            this.SitefinityFileManager._onGridRowDataBound(grid, args);
        },

        _onGridRowDataBound: function (grid, args) {
            var cell = null;
            var tr = args.get_item("Name").get_element();

            if (tr) {
                cell = tr.insertCell(0);
                var checkbox = document.createElement("input");
                checkbox.type = "checkbox";
                checkbox.setAttribute("id", "ClientSelectColumnSelectCheckBox");
                checkbox.setAttribute("name", "ClientSelectColumnSelectCheckBox");
                cell.appendChild(checkbox);

                // Gets the Size cell.
                cell = tr.cells[3];
                if (cell && cell.innerHTML != "&nbsp;") {
                    var bytes = cell.innerHTML;
                    cell.innerHTML = this.readablizeBytes(bytes);
                }

                // Deletes the empty cell.
                if (tr.cells.length > 2) {
                    tr.deleteCell(1);
                }
            }
        },

        readablizeBytes: function (bytes) {
            var result = "";
            if (bytes == "0")
                result = "0 KB";

            else {
                var s = ['bytes', 'KB', 'MB', 'GB', 'TB', 'PB'];
                var range = Math.floor(Math.log(bytes) / Math.log(1024));

                //when range is in bytes present in KB and round to nearest integer.
                if (range == 0) {
                    range++;
                    result = Math.ceil((bytes / Math.pow(1024, Math.floor(range)))) + " " + s[range];
                }
                else {
                    result = (bytes / Math.pow(1024, Math.floor(range))).toFixed(2) + " " + s[range];
                }
            }
            return result;
        },

        toolbarClicked: function (sender, args) {
            Telerik.Sitefinity.Modules.Files.Web.UI.FileManager.callBaseMethod(this, "toolbarClicked", [sender, args]);

            var buttonValue = args.get_item().get_value();
            var selectedItem = this.get_selectedItem();

            switch (buttonValue) {
                case "FolderUp":
                    this.folderUp();
                    break;
                case "Download":
                    this.download(null);
                    break;
                case "Rename":
                    if (selectedItem) {
                        this.rename(selectedItem);
                    }
                    break;
                case "Copy":
                    if (this._control == null) {
                        this._control = "grid";
                    }
                    this._copy(this._control);
                    break;
                case "Paste":
                    var pasteItem = selectedItem;

                    if (this._control == null || this._control == "grid") {
                        if (pasteItem == null || !pasteItem.isDirectory()) {
                            //file or nothing selected - paste into current folder
                            pasteItem = this._getCurrentFolderItem()
                        }
                    }

                    this._paste(pasteItem);
                    break;
            }
        },

        _updateToolbar: function (sender, args) {
            Telerik.Sitefinity.Modules.Files.Web.UI.FileManager.callBaseMethod(this, "_updateToolbar", [sender, args]);
            this._setToolbarButtonsEnabledState();
        },

        _onGridRowSelectedOverride: function (grid, args) {
            this._onGridRowSelectedBaseMethod(grid, args);

            this.SitefinityFileManager._onGridRowSelected(grid, args);
        },

        _onGridRowSelected: function (grid, args) {
            this._control = "grid";
            this._setToolbarButtonsEnabledState();
        },

        _onTreeNodeClicked: function (sender, args) {
            Telerik.Sitefinity.Modules.Files.Web.UI.FileManager.callBaseMethod(this, "_onTreeNodeClicked", [sender, args]);

            this._control = "tree";
            this._setToolbarButtonsEnabledState();
        },

        _onGridRowDblClick: function (grid, args) {
            // Disable double click in the grid
            return false;
        }
    }

$telerik.$.registerControlProperties(Telerik.Sitefinity.Modules.Files.Web.UI.FileManager, {
    initialPath: null
});

Telerik.Sitefinity.Modules.Files.Web.UI.FileManager.registerClass("Telerik.Sitefinity.Modules.Files.Web.UI.FileManager", Telerik.Web.UI.RadFileExplorer);
