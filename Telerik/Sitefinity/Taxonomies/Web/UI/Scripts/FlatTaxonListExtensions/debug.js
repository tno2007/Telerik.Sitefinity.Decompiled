function FlatTaxonListExtensions_ViewLoaded(masterGridView) {
    this.itemsGrid = masterGridView.get_itemsGrid();
    this.toolbar = masterGridView.get_toolbar();
    this.customParams = masterGridView.get_customParameters();
    this.binder = this.itemsGrid.getBinder();
    this.clientLabelManager = masterGridView.get_clientLabelManager(); 

    this.letters = [];

    this.taxonomyId = null;
    this.singleTaxonName = null;
    this.taxonomyName = null;
    this.taxonomyTitle = null;

    this._itemsGridDataBoundDelegate = null;
    this._itemsGridDataBindingDelegate = null;

    this.initialize = function () {
        this._itemsGridDataBoundDelegate = Function.createDelegate(this, this.onItemsGridDataBound);
        this.binder.add_onItemDataBound(this._itemsGridDataBoundDelegate);

        this._itemsGridDataBindingDelegate = Function.createDelegate(this, this.onItemsGridDataBinding);
        this.itemsGrid.add_dataBinding(this._itemsGridDataBindingDelegate);

        this.taxonomyId = this.customParams.taxonomyId;
        this.singleTaxonName = this.customParams.singleTaxonName;
        this.taxonomyName = this.customParams.taxonomyName;
        this.taxonomyTitle = this.customParams.taxonomyTitle;

        var createNewWidget = this.toolbar.getWidgetByName("CreateFlatTaxon");
        if (createNewWidget) {
            var createText = createNewWidget.get_buttonText();
            createText = this.GetFormattedString(createText, this.singleTaxonName.toLowerCase());
            createNewWidget.set_buttonText(createText);
        }
    }

    this.onItemsGridDataBound = function (sender, args) {
        var dataItem = args.get_dataItem();
        var title = dataItem["Title"];
        var firstLetter = title.substring(0, 1);
        var row = args.get_itemElement();

        if (row) {
            var cell = row.cells[0];
            if (cell) {
                var span = jQuery(cell).find("span").get(0);

                if (this.letters.indexOf(firstLetter.toUpperCase()) == -1) {
                    Array.add(this.letters, firstLetter.toUpperCase());
                    if (span == null) {
                        span = document.createElement("span");
                        span.className = "sfLetterMarks";
                        cell.insertBefore(span, cell.firstChild);
                    }

                    span.innerHTML = firstLetter.toUpperCase();
                }
                else {
                    if (span != null) {
                        cell.removeChild(span);
                    }
                }
            }

            cell = row.cells[row.cells.length - 1];
            if (cell) {
                var itemsCount = dataItem["ItemsCount"];
                var html = null;
                if (itemsCount == 0) {
                    html = itemsCount + " " + this.clientLabelManager.getLabel("Labels", "Items");
                    jQuery(cell).find("a.sf_binderCommand_viewMarkedItems").parent().html(html);
                }
                else if (itemsCount == 1) {
                    html = itemsCount + " " + this.clientLabelManager.getLabel("Labels", "Item");
                    jQuery(cell).find("a.sf_binderCommand_viewMarkedItems").html(html);
                }
            }
        }

        if (sender._itemsBoundCount == sender._itemsToBindCount) {
            this.letters = [];
        }
    }

    this.onItemsGridDataBinding = function (sender, args) {
        // clear the letters array before binding
        this.letters = [];
    }

    /* **************** helper methods **************** */

    this.GetFormattedString = function (str, strToFormat) {
        if (str.indexOf('{0}') != -1) {
            return String.format(str, strToFormat);
        }
        return str;
    };

    this.initialize();
}