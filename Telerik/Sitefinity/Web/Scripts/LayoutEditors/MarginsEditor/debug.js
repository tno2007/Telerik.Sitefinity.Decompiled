//================================ Margin editor ==========================================================================//
Telerik.Sitefinity.Web.UI.MarginEditor = function(layoutRoot, visualEditorContainer, outerColumns, innerColumns, controlPanelInfo) {
    Telerik.Sitefinity.Web.UI.MarginEditor.initializeBase(this, [visualEditorContainer, outerColumns, innerColumns]);

    this._layoutRoot = layoutRoot;
    this._visualEditorContainer = visualEditorContainer;
    this._innerColumns = innerColumns;

    this._advancedTextBoxes = [];

    this._SIMPLE = 0;
    this._ADVANCED = 1;

    this._controlPanelInfo = controlPanelInfo;
    this._currentMode = null; // possible: simple, advanced
    this._unitMode = this._PIXELS; // possible: this._PIXELS, this._PERCENTAGES
    this._horizontalSpaceText = $get(this._controlPanelInfo.HorizontalSpaceColumnsId);
    this._verticalSpaceText = $get(this._controlPanelInfo.VerticalSpaceColumnsId);
    this._simpleSpacesContainer = $get(this._controlPanelInfo.SimpleSpacesContainerId);
    this._advancedSpacesContainer = $get(this._controlPanelInfo.AdvancedSpacesContainerId);
    this._advancedMarginsContainer = $get(this._controlPanelInfo.AdvancedMarginsContainerId);

    this._spaceBetweenInPixels = 0;
    this._spaceAboveAndBelowInPixels = 0;
    this._spaceBetweenInPercentages = 0;
    this._spaceAboveAndBelowInPercentages = 0;

    this._advancedMarginsInPixels = [];
    this._advancedMarginsInPercentages = [];
}

Telerik.Sitefinity.Web.UI.MarginEditor.prototype = {
    initialize: function () {

        Telerik.Sitefinity.Web.UI.MarginEditor.callBaseMethod(this, 'initialize');

        this._simpleMarginChangedDelegate = Function.createDelegate(this, this._simpleMarginChanged);
        this._advancedMarginChangedDelegate = Function.createDelegate(this, this._advancedMarginChanged);

        // Populate the equal spaces boxes - simple mode
        this._horizontalSpaceText.value = this._getSpaceBetweenColumnsInPixels();
        $addHandler(this._horizontalSpaceText, 'change', this._simpleMarginChangedDelegate);
        this._verticalSpaceText.value = this._getSpaceAboveAndBeloweColumnsInPixels();
        $addHandler(this._verticalSpaceText, 'change', this._simpleMarginChangedDelegate);

        this._changeModeDelegate = Function.createDelegate(this, this._modeChanged);
        // modes are available only if there is more than one column
        this._spacesSideBySideButton = $get(this._controlPanelInfo.SpacesSideBySideButtonId);
        this._equalSpacesButton = $get(this._controlPanelInfo.EqualSpacesButtonId);
        $addHandler(this._spacesSideBySideButton, 'click', this._changeModeDelegate);
        $addHandler(this._equalSpacesButton, 'click', this._changeModeDelegate);

        this._generateAdvancedModeTextBoxes();

        this._changeUnitModeDelegate = Function.createDelegate(this, this._unitModeChanged);
        this._spacesInPixels = $get(this._controlPanelInfo.SpacesInPixelsRadioId);
        this._spacesInPercentages = $get(this._controlPanelInfo.SpacesInPercentagesRadioId);
        $addHandler(this._spacesInPixels, 'click', this._changeUnitModeDelegate);
        $addHandler(this._spacesInPercentages, 'click', this._changeUnitModeDelegate);
    },

    dispose: function () {
        $removeHandler(this._spacesSideBySideButton, 'click', this._changeModeDelegate);
        $removeHandler(this._equalSpacesButton, 'click', this._changeModeDelegate);
        $removeHandler(this._horizontalSpaceText, 'change', this._simpleMarginChangedDelegate);
        $removeHandler(this._verticalSpaceText, 'change', this._simpleMarginChangedDelegate);
        $removeHandler(this._spacesInPixels, 'click', this._changeUnitModeDelegate);
        $removeHandler(this._spacesInPercentages, 'click', this._changeUnitModeDelegate);
        $(this._advancedMarginsContainer).empty();

        this._advancedTextBoxes = [];
        this._innerColumns = [];

        Telerik.Sitefinity.Web.UI.MarginEditor.callBaseMethod(this, 'dispose');
    },

    SetVisualEditor: function () {
        this.ClearUI();

        // NOTE: Simple mode temporarily disabled

        // set the mode to be simple by default. However, if there is only one column,
        // only advanced mode is possible - so set the advanced to be default
        //        if (this._innerColumns.length > 1) {
        //            this._changeMode(this._SIMPLE);
        //            $('#simpleModeCommand').show();
        //            $('#advancedModeCommand').show();
        //        } else {
        this._changeMode(this._ADVANCED);
        $('#simpleModeCommand').hide();
        $('#advancedModeCommand').hide();
        //        }

        // make sure that for the new editor, simple is always selected first
        this._changeUnitMode(); // make sure that for the new editor, default units are always set

        this.GenerateLabels(this._generateLabelStrings());
    },

    _updateVisuals: function () {
        var colCount = this._innerColumns.length;
        if (this._currentMode == this._SIMPLE) {
            for (var colIter = 0; colIter < colCount; colIter++) {
                if (this._unitMode == this._PIXELS) {
                    this._innerColumns[colIter].style.marginTop = this.GetStyleString(this._spaceAboveAndBelowInPixels, this._unitMode);
                    this._innerColumns[colIter].style.marginBottom = this.GetStyleString(this._spaceAboveAndBelowInPixels, this._unitMode);
                    if (colIter > 0) { // don't apply margin to the first column
                        var test = this.GetStyleString(this._spaceBetweenInPixels, this._unitMode);
                        this._innerColumns[colIter].style.marginLeft = test;
                    }
                } else if (this._unitMode == this._PERCENTAGES) {
                    this._innerColumns[colIter].style.marginTop = this.GetStyleString(this._spaceAboveAndBelowInPercentages, this._unitMode);
                    this._innerColumns[colIter].style.marginBottom = this.GetStyleString(this._spaceAboveAndBelowInPercentages, this._unitMode);
                    if (colIter > 0 && colIter < (colCount)) {
                        this._innerColumns[colIter].style.marginLeft = this.GetStyleString(this._spaceBetweenInPercentages, this._unitMode);
                    }
                }
            }
        } else if (this._currentMode == this._ADVANCED) {
            for (var colIter = 0; colIter < colCount; colIter++) {
                if (this._unitMode == this._PIXELS) {
                    this._innerColumns[colIter].style.marginTop = this.GetStyleString(this._advancedMarginsInPixels[colIter][0], this._unitMode);
                    this._innerColumns[colIter].style.marginRight = this.GetStyleString(this._advancedMarginsInPixels[colIter][1], this._unitMode);
                    this._innerColumns[colIter].style.marginBottom = this.GetStyleString(this._advancedMarginsInPixels[colIter][2], this._unitMode);
                    this._innerColumns[colIter].style.marginLeft = this.GetStyleString(this._advancedMarginsInPixels[colIter][3], this._unitMode);
                } else if (this._unitMode == this._PERCENTAGES) {
                    this._innerColumns[colIter].style.marginTop = this.GetStyleString(this._advancedMarginsInPercentages[colIter][0], this._unitMode);
                    this._innerColumns[colIter].style.marginRight = this.GetStyleString(this._advancedMarginsInPercentages[colIter][1], this._unitMode);
                    this._innerColumns[colIter].style.marginBottom = this.GetStyleString(this._advancedMarginsInPercentages[colIter][2], this._unitMode);
                    this._innerColumns[colIter].style.marginLeft = this.GetStyleString(this._advancedMarginsInPercentages[colIter][3], this._unitMode);
                }
            }
        }
        this.UpdateLabels(this._generateLabelStrings());
    },

    _changeMode: function (mode) {
        var _mode = mode;
        if (_mode == null) {
            _mode = (this._currentMode == this._SIMPLE) ? this._ADVANCED : this._SIMPLE; // toggling logic
        }

        if (_mode == this._ADVANCED) {
            $(this._simpleSpacesContainer).hide();
            $(this._advancedSpacesContainer).show();
        } else {
            $(this._simpleSpacesContainer).show();
            $(this._advancedSpacesContainer).hide();
        }

        this._currentMode = _mode;
    },

    _modeChanged: function () {
        this._changeMode();
        this._updateVisuals();
    },

    _unitModeChanged: function () {
        if (this._spacesInPixels.checked) {
            this._unitMode = this._PIXELS;
        } else if (this._spacesInPercentages.checked) {
            this._unitMode = this._PERCENTAGES;
        }
        this._changeUnitMode();
        this._updateVisuals();
    },

    _changeUnitMode: function () {
        var editor = this;
        $('.sfSpaceUnit').each(function () {
            $(this).html(editor.GetUnitString(editor._unitMode));
        });
        this.GenerateLabels(this._generateLabelStrings());
    },

    _generateAdvancedModeTextBoxes: function () {
        var columnList = document.createElement('ul');

        var colCount = this._innerColumns.length;
        for (colIter = 0; colIter < colCount; colIter++) {

            var colItem = document.createElement('li');

            // create a column label
            var columnLabel = document.createElement('h2');
            // adjust for the zero based index
            columnLabel.innerHTML = this._controlPanelInfo.ColumnLabel + (colIter + 1);
            colItem.appendChild(columnLabel);

            colItem.appendChild(this._generateColumnMarginTextBoxes(colIter));

            columnList.appendChild(colItem);
        }

        this._advancedMarginsContainer.appendChild(columnList);
    },

    _generateColumnMarginTextBoxes: function (colIndex) {

        var sidesList = document.createElement('ol');

        var sides = ['top', 'right', 'bottom', 'left'];
        var labels = [];
        labels['top'] = this._controlPanelInfo.TopLabel;
        labels['right'] = this._controlPanelInfo.RightLabel;
        labels['bottom'] = this._controlPanelInfo.BottomLabel;
        labels['left'] = this._controlPanelInfo.LeftLabel;

        var colTextBoxes = [];
        var marginsPixels = [];
        var marginsPercentages = [];
        for (sidesIter = 0; sidesIter < 4; sidesIter++) {
            var _side = sides[sidesIter];
            var _label = labels[_side];

            var boxId = _side + 'MarginCol' + colIndex;
            var item = document.createElement('li');

            var label = document.createElement('label');
            label.innerHTML = _label;
            label.setAttribute('for', boxId);
            item.appendChild(label);

            var marginText = document.createElement('input');
            marginText.setAttribute('type', 'text');
            marginText.setAttribute('id', boxId);
            marginText.setAttribute('class', 'sfTxt');
            if (this._unitMode == this._PIXELS) {
                marginText.value = this._getColumnMarginInPixels(colIndex, _side);
            } else if (this._unitMode == this._PERCENTAGES) {
                marginText.value = this._convertPixelsToPercentages(this._getColumnMarginInPixels(colIndex, _side));
            }
            item.appendChild(marginText);

            colTextBoxes[_side] = marginText;

            marginsPixels[sidesIter] = this._getColumnMarginInPixels(colIndex, _side);
            marginsPercentages[sidesIter] = this._convertPixelsToPercentages(this._getColumnMarginInPixels(colIndex, _side));

            // TODO: remove this handlers somewhere
            $addHandler(marginText, 'change', this._advancedMarginChangedDelegate);

            if (sidesIter == 3) {
                var unitLabel = document.createElement('span');
                unitLabel.setAttribute('class', 'sfSpaceUnit');
                unitLabel.innerHTML = this.GetUnitString(this._unitMode);
                item.appendChild(unitLabel);
            }

            sidesList.appendChild(item);
        }

        this._advancedMarginsInPixels[colIndex] = marginsPixels;
        this._advancedMarginsInPercentages[colIndex] = marginsPercentages;

        this._advancedTextBoxes.push(colTextBoxes);

        return sidesList;
    },

    _simpleMarginChanged: function (sender) {
        // horizontal space changed
        if (sender.target.id == this._controlPanelInfo.HorizontalSpaceColumnsId) {
            if (this._unitMode == this._PIXELS) {
                this._spaceBetweenInPixels = sender.target.value;
                this._spaceBetweenInPercentages = this._convertPixelsToPercentages(sender.target.value);
            } else if (this._unitMode == this._PERCENTAGES) {
                this._spaceBetweenInPercentages = sender.target.value;
                this._spaceBetweenInPixels = this._convertPercentagesToPixels(sender.target.value);
            }
        } else { // vertical space changed
            if (this._unitMode == this._PIXELS) {
                this._spaceAboveAndBelowInPixels = sender.target.value;
                this._spaceAboveAndBelowInPercentages = this._convertPixelsToPercentages(sender.target.value);
            } else if (this._unitMode == this._PERCENTAGES) {
                this._spaceAboveAndBelowInPercentages = sender.target.value;
                this._spaceAboveAndBelowInPixels = this._convertPercentagesToPixels(sender.target.value);
            }
        }
        this._updateVisuals();
    },

    _advancedMarginChanged: function (sender) {
        var newMargin = this.GetStyleString(sender.target.value, this._unitMode);
        var side = this._getSideFromAdvancedTextId(sender.target.id);
        var colIndex = this._getColumnIndexFromAdvancedTextId(sender.target.id);

        var top = this._advancedTextBoxes[colIndex][this._TOP].value;
        var right = this._advancedTextBoxes[colIndex][this._RIGHT].value;
        var bottom = this._advancedTextBoxes[colIndex][this._BOTTOM].value;
        var left = this._advancedTextBoxes[colIndex][this._LEFT].value;

        var topPixels = (this._unitMode == this._PIXELS) ? top : this._convertPercentagesToPixels(top);
        var rightPixels = (this._unitMode == this._PIXELS) ? right : this._convertPercentagesToPixels(right);
        var bottomPixels = (this._unitMode == this._PIXELS) ? bottom : this._convertPercentagesToPixels(bottom);
        var leftPixels = (this._unitMode == this._PIXELS) ? left : this._convertPercentagesToPixels(left);

        var topPercentages = (this._unitMode == this._PERCENTAGES) ? top : this._convertPixelsToPercentages(top);
        var rightPercentages = (this._unitMode == this._PERCENTAGES) ? right : this._convertPixelsToPercentages(right);
        var bottomPercentages = (this._unitMode == this._PERCENTAGES) ? bottom : this._convertPixelsToPercentages(bottom);
        var leftPercentages = (this._unitMode == this._PERCENTAGES) ? left : this._convertPixelsToPercentages(left);

        this._advancedMarginsInPixels[colIndex] = [topPixels, rightPixels, bottomPixels, leftPixels];
        this._advancedMarginsInPercentages[colIndex] = [topPercentages, rightPercentages, bottomPercentages, leftPercentages];

        this._updateVisuals();
    },

    _getSideFromAdvancedTextId: function (id) {
        return id.split('MarginCol')[0];
    },

    _getColumnIndexFromAdvancedTextId: function (id) {
        return id.split('MarginCol')[1];
    },

    /* ******************************** calculations ******************************** */
    _getSpaceBetweenColumnsInPixels: function () {
        return 0; // TODO: implement this
    },

    _getSpaceAboveAndBeloweColumnsInPixels: function () {
        return 0; // TODO: implement this
    },

    _getSpaceBetweenColumnsInPercentages: function () {
        return 0; // TODO: implement this
    },

    _getSpaceAboveAndBeloweColumnsInPercentages: function () {
        return 0; // TODO: implement this
    },

    _getColumnMarginInPixels: function (colIndex, side) {
        var col = this._innerColumns[colIndex];
        var cssProperty = "";
        if (side == this._TOP) {
            cssProperty = "marginTop";
        } else if (side == this._RIGHT) {
            cssProperty = "marginRight";
        } else if (side == this._BOTTOM) {
            cssProperty = "marginBottom";
        } else if (side == this._LEFT) {
            cssProperty = "marginLeft";
        } else {
            alert('No such side as "' + side + '"');
        }

        var margin = jQuery(col).css(cssProperty);

        if (this._unitMode == this._PIXELS) {
            return this.GetStyleNumber(margin);
        } else if (this._unitMode == this._PERCENTAGES) {
            return this._convertPercentagesToPixels(this.GetStyleNumber(margin));
        }
    },

    /* ******************************** utility ******************************** */
    _generateLabelStrings: function () {
        var labelStrings = [];
        var unitString = this.GetUnitString(this._unitMode);
        var colCount = this._innerColumns.length;
        for (i = 0; i < colCount; i++) {
            var marginsString = '';
            if (this._currentMode == this._ADVANCED) {
                marginsString = '<br />' + this._advancedTextBoxes[i][this._TOP].value + ' ' + unitString + ' ' +
								this._advancedTextBoxes[i][this._RIGHT].value + ' ' + unitString + ' ' +
								this._advancedTextBoxes[i][this._BOTTOM].value + ' ' + unitString + ' ' +
								this._advancedTextBoxes[i][this._LEFT].value + ' ' + unitString + ' ';
            }
            var label = '<strong>' + this._controlPanelInfo.ColumnLabel + ' ' + (i + 1) + '</strong>' + marginsString;
            labelStrings.push(label);
        }
        return labelStrings;
    },

    _convertPixelsToPercentages: function (pixelVal) {
        var containerWidth = this._layoutRoot.clientWidth; // per W3C standards, width is always used as a base
        return Math.round(((pixelVal / containerWidth) * 100));
    },

    _convertPercentagesToPixels: function (percentageVal) {
        var containerWidth = this._layoutRoot.clientWidth; // per W3C standards, width is always used as a base
        return Math.round(containerWidth * (percentageVal / 100));
    }

};

Telerik.Sitefinity.Web.UI.MarginEditor.registerClass('Telerik.Sitefinity.Web.UI.MarginEditor', Telerik.Sitefinity.Web.UI.EditorBase);
