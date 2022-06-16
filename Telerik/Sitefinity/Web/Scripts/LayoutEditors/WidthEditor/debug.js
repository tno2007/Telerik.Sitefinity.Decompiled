﻿//================================ Width editor ===========================================================================//
Telerik.Sitefinity.Web.UI.WidthEditor = function (layoutRoot, visualEditorContainer, outerColumns, innerColumns, controlPanelInfo) {
    Telerik.Sitefinity.Web.UI.WidthEditor.initializeBase(this, [visualEditorContainer, outerColumns, innerColumns]);

    //The root element which contains the layout control and its zones
    this._visualEditorContainer = visualEditorContainer;
    this._layoutRoot = layoutRoot;
    this._outerColumns = outerColumns;

    // Control panel 
    this._controlPanelInfo = controlPanelInfo;
    this._widthsContainer = $get(controlPanelInfo.ColumnWidthsContainerId);
    this._isSettingAutoSizedColumn = false;

    // possible: pixels or percentages
    this._sizesInPixelsRadio = $get(controlPanelInfo.SizesInPixelsRadioId);
    this._sizesInPercentagesRadio = $get(controlPanelInfo.SizesInPercentagesRadioId);

    this._unitMode = (this._sizesInPixelsRadio.checked) ? this._PIXELS : this._PERCENTAGES;
    this._controlPanelInitialized = false;
    this._widthTextBoxes = [];
    this._columnItems = [];
    this._currentAutoSizedIndex = null;

    this._minimumPercentageColumnWidth = 2;
    this._minimumPixelColumnWidth = 25;

    this._widthsInPixels = [];
    this._widthsInPercentages = [];

    // original values
    this._hasOriginalStyleWidths = false;
    this._defaultCalculatedWidths = [];

    // visual editor
    this._sliders = [];
    this._slidersInitialLeft = [];
    this._sliderBounds = [];

    // calculations
    this._originalWidthsPixels = [];

}

Telerik.Sitefinity.Web.UI.WidthEditor.prototype = {

    //Set width to the splitter panes and record ratios that help convert
    initialize: function () {

        Telerik.Sitefinity.Web.UI.WidthEditor.callBaseMethod(this, 'initialize');

        this._calculateWidths();
        this._updateVisuals(true);

        var outerCols = this._outerColumns;

        this._changeAutoSizedColumnDelegate = Function.createDelegate(this, this._changeAutoSizedColumn);
        this._numericValueChangedDelegate = Function.createDelegate(this, this._numericValueChanged);

        // auto sized columns - hide this button if there is only one column
        this._autoSizedColumnButton = $get(this._controlPanelInfo.AutoSizedColumnButtonId);
        this._autoSizedColumnButton.innerHTML = this._controlPanelInfo.ChangeAutoSizedColumnLabel;
        this._autoSizedColumnButtonClickDelegate = Function.createDelegate(this, this._autoSizedColumnButtonClick);
        $addHandler(this._autoSizedColumnButton, 'click', this._autoSizedColumnButtonClickDelegate);

        // create control panel
        this._generateTextboxes();

        // by default, set the last column to be autosized
        // unless, there is only one column
        if (outerCols.length > 1) {
            this._setAutosizedColumn(this._outerColumns.length - 1);
        }

        // units
        this._unitsChangedDelegate = Function.createDelegate(this, this._unitsChanged);
        $addHandler(this._sizesInPixelsRadio, 'click', this._unitsChangedDelegate);
        $addHandler(this._sizesInPercentagesRadio, 'click', this._unitsChangedDelegate);
    },


    dispose: function () {

        $removeHandler(this._sizesInPixelsRadio, 'click', this._unitsChangedDelegate);
        $removeHandler(this._sizesInPercentagesRadio, 'click', this._unitsChangedDelegate);
        $removeHandler(this._autoSizedColumnButton, 'click', this._autoSizedColumnButtonClickDelegate);

        //References
        this._layoutRoot = null;
        this._outerColumns = [];
        this._sliders = [];
        this._slidersInitialLeft = [];
        this._sliderBounds = [];
        this._widthsPixels = [];
        this._widthsPercentages = [];

        $(this._widthsContainer).empty();

        this._columnItems = [];
        var txtCount = this._widthTextBoxes.length;
        for (txtIter = 0; txtIter < txtCount; txtIter++) {
            $removeHandler(this._widthTextBoxes[txtIter], 'change', this._numericValueChangedDelegate);
        }
        this._widthTextBoxes = [];

        // remove the visual elements
        $(this._visualEditorContainer).empty();

        Telerik.Sitefinity.Web.UI.WidthEditor.callBaseMethod(this, 'dispose');
    },

    SetVisualEditor: function () {
        this.ClearUI();

        if (this._outerColumns.length > 1) {
            $(this._autoSizedColumnButton).show();
        } else {
            $(this._autoSizedColumnButton).hide();
        }

        this.GenerateLabels(this._generateLabelStrings());
        this._generateSliders();
    },

    WidthsAreChanged: function () {
        // in case there were no with in the element sytle
        // check if the calculated are different 
        if (this._hasOriginalStyleWidths) {
            return true;
        }
        else {
            for (var i = 0, len = this._outerColumns.length; i < len; i++) {
                if (this._outerColumns[i].style.width != this._defaultCalculatedWidths[i]) {
                    return true;
                }
            }
        }
        return false;
    },

    /* ********************************* visual editor ********************************* */

    _generateSliders: function () {
        var columnCount = this._outerColumns.length;
        if (columnCount == 1) {
            return; // do not generate sliders, if there is only one column
        }
        var slidersCount = columnCount - 1; // we don't need sliders at the beginning and at the end
        for (i = 0; i < slidersCount; i++) {
            var sliderIndex = i;
            var slider = document.createElement('div');
            //slider.setAttribute('class', 'sfLayoutWidthSlider');
            $(slider).addClass('sfLayoutWidthSlider');
            //var style = this._generateInitialSliderStyle(i);
            //slider.setAttribute('style', style);
            this._generateAndSetInitialSliderStyle(i, slider);

            slider.setAttribute('id', 'sfSlider' + i);

            // insert the styling div inside <div class="sfLayoutSliderArrow"/>
            var sliderStylingDiv = document.createElement('div');
            //sliderStylingDiv.setAttribute('class', 'sfLayoutSliderArrow');
            $(sliderStylingDiv).addClass('sfLayoutSliderArrow');
            slider.appendChild(sliderStylingDiv);

            this._visualEditorContainer.appendChild(slider);
            this._slidersInitialLeft[i] = this.GetColumnRightInPixels(i);
            var widthEditor = this;
            $(slider).draggable({
                'axis': 'x',
                'drag': function (event, ui) {
                    widthEditor._resize(event.target);
                },
                'stop': function (event, ui) {
                    // bounds of all sliders need to be recalculated after the dragging has stopped
                    var slidersCount = widthEditor._sliders.length;
                    for (var sliderIter = 0; sliderIter < slidersCount; sliderIter++) {
                        widthEditor._calculateBounds(sliderIter);
                    }
                    widthEditor._updateVisuals();
                }
            });
            this._sliders[i] = slider;
            this._calculateBounds(sliderIndex);
        }
    },

    _calculateBounds: function (sliderIndex) {
        var x1 = this._getLeftDraggingBound(sliderIndex);
        var y1 = 0;
        var x2 = this._getRightDraggingBound(sliderIndex);
        var y2 = 5000;
        this._sliderBounds[sliderIndex] = [x1, y1, x2, y2];
        $(this._sliders[sliderIndex]).draggable('option', 'containment', this._sliderBounds[sliderIndex]);
    },

    _generateInitialSliderStyle: function (sliderIndex) {
        var leftColIndex = sliderIndex;
        var rightColIndex = sliderIndex + 1;
        var width = this._getSliderWidth(leftColIndex, rightColIndex) + 'px';
        var height = this.GetOuterColumnHeight(leftColIndex) + 'px';
        var left = this.GetColumnRightInPixels(leftColIndex) + 'px';
        var top = '0px';
        return 'position:absolute; left:' + left + ';top:' + top + ';width:' + width + ';height:' + height;
    },
    _generateAndSetInitialSliderStyle: function (sliderIndex, elm) {
        var leftColIndex = sliderIndex;
        var rightColIndex = sliderIndex + 1;
        var sliderWidth = this._getSliderWidth(leftColIndex, rightColIndex);
        if (sliderWidth < 0) {
            sliderWidth = 0;
        }
        var width = sliderWidth + 'px';
        var height = this.GetOuterColumnHeight(leftColIndex) + 'px';
        var left = this.GetColumnRightInPixels(leftColIndex) + 'px';
        var top = '0px';

        elm.style.position = 'absolute';
        elm.style.left = left;
        elm.style.top = top;
        elm.style.width = width;
        elm.style.height = height;
    },

    _getSliderWidth: function (leftColIndex, rightColIndex) {
        var left = this.GetColumnRightInPixels(leftColIndex);
        var right = this.GetColumnLeftInPixels(rightColIndex);
        var leftMargin = this.GetColumnMarginInPixels(rightColIndex, this._LEFT);
        var rightMargin = this.GetColumnMarginInPixels(leftColIndex, this._RIGHT);
        return right - left + leftMargin + rightMargin;
    },

    _resize: function (slider) {
        var sliderIndex = Number(slider.id.replace('sfSlider', ''));
        if (isNaN(sliderIndex)) {
            return false;
        }
        var diff = this._slidersInitialLeft[sliderIndex] - this._getElementLeftInPixels(slider);
        var leftWidth = this._widthsInPixels[sliderIndex];
        var rightWidth = this._widthsInPixels[sliderIndex + 1];

        leftWidth = leftWidth - diff;
        rightWidth = rightWidth + diff;

        // The size of the slider is 12px, so two sliders with 12px width + 1px between them
        if (leftWidth <= 25) {
            return false;
        }
        else if (rightWidth <= 25) {
            return false;
        }

        // update the slider position
        this._slidersInitialLeft[sliderIndex] = this._getElementLeftInPixels(slider);

        //update the widths
        var leftColPixels = leftWidth;
        var rightColPixels = this._ensureLayoutIntegrity(leftWidth, sliderIndex, rightWidth, sliderIndex + 1, this._PIXELS);
        var leftColPercentages = this._convertToPercentages(leftWidth);
        var rightColPercentages = this._ensureLayoutIntegrity(this._convertToPercentages(leftWidth), sliderIndex, this._convertToPercentages(rightWidth), sliderIndex + 1, this._PERCENTAGES);

        var objChange = {
            'LeftColumnIndex': sliderIndex,
            'RightColumnIndex': sliderIndex + 1,
            'LeftColumnWidthPixels': leftColPixels,
            'RightColumnWidthPixels': rightColPixels,
            'LeftColumnWidthPercentages': leftColPercentages,
            'RightColumnWidthPercentages': rightColPercentages
        };

        this._updateWidths(objChange);
        this._updateTextboxValues();
        this._updateVisuals();
        this.UpdateLabels(this._generateLabelStrings());
    },

    _updateVisuals: function (isInitializing) {
        var colCount = this._outerColumns.length;
        //keep the original style widths
        if (isInitializing) {
            for (var i = 0; i < colCount; i++) {
                if (this._outerColumns[i].style.width != "") {
                    this._hasOriginalStyleWidths = true;
                }
            }
        }

        if (this._unitMode == this._PIXELS) {
            for (var i = 0; i < colCount; i++) {
                this._outerColumns[i].style.width = this._widthsInPixels[i] + 'px';
            }
        } else if (this._unitMode == this._PERCENTAGES) {
            for (var i = 0; i < colCount; i++) {
                this._outerColumns[i].style.width = this._widthsInPercentages[i] + '%';
            }
        }

        //keep the initial caclulations
        if (isInitializing) {
            for (var i = 0; i < colCount; i++) {
                this._defaultCalculatedWidths[i] = this._outerColumns[i].style.width;
            }
        }

        // update sliders
        var sliderCount = this._sliders.length;
        for (var sliderIter = 0; sliderIter < sliderCount; sliderIter++) {
            var style = this._generateInitialSliderStyle(sliderIter);
            //this._sliders[sliderIter].setAttribute('style', style);
            this._generateAndSetInitialSliderStyle(sliderIter, this._sliders[sliderIter]);
        }
    },



    /* ********************************* numeric calculations ********************************* */

    _numericValueChanged: function (sender) {
        var colIndex = this._getColumnIndex(sender.target.id);
        var newValue = sender.target.value;
        if (this._unitMode == this._PIXELS) {
            this._recalculateInPixels(colIndex, newValue);
        } else if (this._unitMode == this._PERCENTAGES) {
            this._recalculateInPercentages(colIndex, newValue);
        }
        this.UpdateLabels(this._generateLabelStrings());
    },

    _getColumnIndex: function (colId) {
        return colId.replace('columnWidth', '');
    },

    _recalculateInPercentages: function (colIndex, newValue) {
        if (this._isPercentageValueValid(newValue, colIndex)) {
            // calculate the new value for the auto sized column and set it
            var oldCurrentValue = this._widthsInPercentages[colIndex];
            var autoSizedValue = this._widthsInPercentages[this._currentAutoSizedIndex];
            var difference = oldCurrentValue - newValue;
            var newAutoSizedValue = autoSizedValue + difference;

            // update the widths
            var objChange = {
                'LeftColumnIndex': colIndex,
                'RightColumnIndex': this._currentAutoSizedIndex,
                'LeftColumnWidthPixels': this._convertToPixels(newValue),
                'RightColumnWidthPixels': this._ensureLayoutIntegrity(this._convertToPixels(newValue), colIndex, this._convertToPixels(newAutoSizedValue), this._currentAutoSizedIndex, this._PIXELS),
                'LeftColumnWidthPercentages': newValue,
                'RightColumnWidthPercentages': this._ensureLayoutIntegrity(newValue, colIndex, newAutoSizedValue, this._currentAutoSizedIndex, this._PERCENTAGES)
            };
            this._updateWidths(objChange);
            this._updateTextboxValues();
            this._updateVisuals();
        } else {
            jQuery(this._columnItems[colIndex]).find(".sfTxt").attr("value", this._widthsInPercentages[colIndex]);
        }
    },

    _recalculateInPixels: function (colIndex, newValue) {
        if (this._isPixelValueValid(newValue, colIndex)) {
            // calculate the new value for the auto sized column and set it
            var oldCurrentValue = this._widthsInPixels[colIndex];
            var autoSizedValue = this._widthsInPixels[this._currentAutoSizedIndex];
            var difference = oldCurrentValue - newValue;
            var newAutoSizedValue = autoSizedValue + difference;

            // update the widths
            var objChange = {
                'LeftColumnIndex': colIndex,
                'RightColumnIndex': this._currentAutoSizedIndex,
                'LeftColumnWidthPixels': newValue,
                'RightColumnWidthPixels': this._ensureLayoutIntegrity(newValue, colIndex, newAutoSizedValue, this._currentAutoSizedIndex, this._PIXELS),
                'LeftColumnWidthPercentages': this._convertToPercentages(newValue),
                'RightColumnWidthPercentages': this._ensureLayoutIntegrity(this._convertToPercentages(newValue), colIndex, this._convertToPercentages(newAutoSizedValue), this._currentAutoSizedIndex, this._PERCENTAGES)
            };
            this._updateWidths(objChange);
            this._updateTextboxValues();
            this._updateVisuals();
        } else {
            jQuery(this._columnItems[colIndex]).find(".sfTxt").attr("value", this._widthsInPixels[colIndex]);
        }
    },

    _isPercentageValueValid: function (value, colIndex) {
        if (!jQuery.isNumeric(value)) {
            alert("Please enter a numeric value");
            return false;
        }
        var minValue = this._minimumPercentageColumnWidth;
        var maxValue = this._getMaxColumnWidth(colIndex);
        if (value < minValue || value > maxValue) {
            var message = String.format(this._controlPanelInfo.AValueBetweenLabel, minValue, maxValue);
            alert(message);
            return false;
        }
        return true;
    },

    _isPixelValueValid: function (value, colIndex) {
        if (!jQuery.isNumeric(value)) {
            alert("Please enter a numeric value");
            return false;
        }
        var minValue = this._minimumPixelColumnWidth;
        var maxValue = this._getMaxColumnWidth(colIndex);
        if (value < minValue || value > maxValue) {
            var message = String.format(this._controlPanelInfo.AValueBetweenLabel, minValue, maxValue);
            alert(message);
            return false;
        }
        return true;
    },

    _getMaxColumnWidth: function (colIndex) {
        // the maximum percentage a column can occupy is following:
        // current percentage of the auto-sized column + current percentage of the column we are resizing - minimum percentage width
        // of the column, since we cannot resize the auto sized column to less than that
        // LOGIC: if we are increasing the width of the current column, we are decreasing the auto-sized column
        if (this._unitMode == this._PIXELS) {
            return parseInt(this._widthsInPixels[this._currentAutoSizedIndex]) + parseInt(this._widthsInPixels[colIndex]) - parseInt(this._minimumPixelColumnWidth);
        } else if (this._unitMode == this._PERCENTAGES) {
            return parseInt(this._widthsInPercentages[this._currentAutoSizedIndex]) + parseInt(this._widthsInPercentages[colIndex]) - parseInt(this._minimumPercentageColumnWidth);
        }
    },

    /* ********************************* width textboxes ********************************* */

    _generateTextboxes: function () {
        // create a list
        var columnList = document.createElement('ul');
        columnList.setAttribute('id', 'columnList');
        var columnCount = this._outerColumns.length;

        for (colIter = 0; colIter < columnCount; colIter++) {

            var textId = 'columnWidth' + colIter;

            // create a new list item
            var columnItem = document.createElement('li');

            // create a column label
            var columnLabel = document.createElement('label');
            columnLabel.setAttribute('for', textId);
            // adjust for zero based index
            columnLabel.innerHTML = this._controlPanelInfo.ColumnLabel + ' ' + (colIter + 1);
            columnItem.appendChild(columnLabel);

            // remove this later
            //var brTemp = document.createElement('br');
            //columnItem.appendChild(brTemp);

            // create a width textbox
            var widthVal = 0;
            if (this._unitMode == this._PIXELS) {
                widthVal = this._widthsInPixels[colIter];
            } else if (this._unitMode == this._PERCENTAGES) {
                widthVal = this._widthsInPercentages[colIter];
            }

            var columnWidth = document.createElement('input');
            columnWidth.setAttribute('type', 'input');
            columnWidth.setAttribute('id', textId);
            columnWidth.setAttribute('value', widthVal);
            //columnWidth.setAttribute('class', 'sfTxt');
            $(columnWidth).addClass('sfTxt');
            columnItem.appendChild(columnWidth);

            this._widthTextBoxes.push(columnWidth);

            // add the unit label
            var unitLabel = document.createElement('span');
            //unitLabel.setAttribute('class', 'unitLabels');
            $(unitLabel).addClass('unitLabels');
            unitLabel.innerHTML = this.GetUnitString(this._unitMode);
            columnItem.appendChild(unitLabel);

            // by default, last column is auto-sized, so when generating the textboxes,
            // don't add the command to the last one
            var autoSizedPanel = this._generateAutoSizedCommand(colIter);
            columnItem.appendChild(autoSizedPanel);

            columnList.appendChild(columnItem);

            $addHandler(columnWidth, 'change', this._numericValueChangedDelegate);

            this._columnItems.push(columnItem);
            this._originalWidthsPixels.push(this._widthsInPixels[colIter]);
        }

        this._widthsContainer.appendChild(columnList);
        this._controlPanelInitialized = true;
    },

    _generateAutoSizedCommand: function (colIndex, visible) {
        var columnAutoSizedOptions = document.createElement('span');
        //columnAutoSizedOptions.setAttribute('class', 'sfColumnAutoSizedOptions');
        $(columnAutoSizedOptions).addClass('sfColumnAutoSizedOptions');
        if (visible == null || visible == false) {
            //columnAutoSizedOptions.setAttribute('style', 'display:none;');
            columnAutoSizedOptions.style.display = 'none';
        }

        var makeAutoSizedButton = document.createElement('a');
        //makeAutoSizedButton.setAttribute('class', 'sfMakeAutoSized');
        $(makeAutoSizedButton).addClass('sfMakeAutoSized');
        makeAutoSizedButton.setAttribute('href', '#');
        makeAutoSizedButton.setAttribute('rel', colIndex);
        makeAutoSizedButton.innerHTML = this._controlPanelInfo.MakeThisAutoSizedLabel;
        columnAutoSizedOptions.appendChild(makeAutoSizedButton);

        $addHandler(makeAutoSizedButton, 'click', this._changeAutoSizedColumnDelegate);

        return columnAutoSizedOptions;
    },

    _updateTextboxValues: function () {
        var colCount = this._outerColumns.length;
        if (!this._controlPanelInitialized) {
            return;
        }
        for (colIter = 0; colIter < colCount; colIter++) {
            var widthVal = 0;
            if (this._unitMode == this._PIXELS) {
                widthVal = this._widthsInPixels[colIter];
            } else if (this._unitMode = this._PERCENTAGES) {
                widthVal = this._widthsInPercentages[colIter];
            }
            this._widthTextBoxes[colIter].value = widthVal;
        }
    },

    /* ********************************* autosized columns ********************************* */

    _changeAutoSizedColumn: function (sender, args) {
        this._setAutosizedColumn(sender.target.rel);
    },

    _setAutosizedColumn: function (colIndex) {

        // replace the old auto sized column with a command
        if (this._currentAutoSizedIndex != null) {
            var oldColumnItem = this._columnItems[this._currentAutoSizedIndex];
            var autoSizedCommandPanel = this._generateAutoSizedCommand(this._currentAutoSizedIndex, true);
            oldColumnItem.appendChild(autoSizedCommandPanel);
            $(oldColumnItem).find('#autosizePanel').each(function () {
                $(this).remove();
            });
            // enable the checkbox
            $(oldColumnItem).find('input.sfTxt').prop('disabled', false);
        }

        var columnItem = this._columnItems[colIndex];
        // remove the command for making the column auto-sized
        $(columnItem).find('.sfMakeAutoSized').each(function () {
            $(this).remove()
        });

        // create the autosized controls and labels
        var autoSizedPanel = document.createElement('span');
        autoSizedPanel.setAttribute('id', 'autosizePanel');

        var autoSizedLabel = document.createElement('span');
        autoSizedLabel.innerHTML = this._controlPanelInfo.AutoSizedLabel;
        autoSizedPanel.appendChild(autoSizedLabel);

        columnItem.appendChild(autoSizedPanel);

        // disable the input
        $(columnItem).find('input.sfTxt').prop('disabled', true);

        this._currentAutoSizedIndex = colIndex;
    },

    _autoSizedColumnButtonClick: function () {

        this._isSettingAutoSizedColumn = !this._isSettingAutoSizedColumn;

        if (this._isSettingAutoSizedColumn) {
            this._autoSizedColumnButton.innerHTML = this._controlPanelInfo.CancelChangeAutoSizedColumnLabel;
            $('.sfColumnAutoSizedOptions').each(function () {
                $(this).show();
            });
        } else {
            this._autoSizedColumnButton.innerHTML = this._controlPanelInfo.ChangeAutoSizedColumnLabel;
            $('.sfColumnAutoSizedOptions').each(function () {
                $(this).hide();
            });
        }
    },

    /* ********************************* units ********************************* */

    _unitsChanged: function (sender, args) {
        this._changeUnits();
    },

    _changeUnits: function (mode) {
        this._unitMode = (this._sizesInPixelsRadio.checked) ? this._PIXELS : this._PERCENTAGES;
        var unitLabel = this.GetUnitString(this._unitMode);

        $('#columnList').find('.unitLabels').each(function () {
            $(this).text(unitLabel);
        });

        this._updateTextboxValues();
        this.UpdateLabels(this._generateLabelStrings());
    },

    /* ********************************* calculations ********************************* */

    _calculateWidths: function () {
        var colCount = this._outerColumns.length;

        var _totalWidthPixels = jQuery(this._layoutRoot).width();
        var _totalWidthPercentages = 100;

        // calculate widths in pixels	    
        // and assure pixel width integrity - adjust auto-sized column if needed to ensure layout does not break
        var screenTotal = 0;
        for (var i = 0; i < colCount; i++) {
            this._widthsInPixels[i] = this._outerColumns[i].clientWidth;
            // add the width of the column
            screenTotal += this._widthsInPixels[i];
        }

        if (screenTotal > _totalWidthPixels) {
            var adjustIndex = (this._currentAutoSizedIndex == null) ? (colCount - 1) : this._currentAutoSizedIndex;
            this._widthsInPixels[adjustIndex] -= screenTotal - _totalWidthPixels;
        }
        else if (screenTotal < _totalWidthPixels) {
            var adjustIndex = (this._currentAutoSizedIndex == null) ? (colCount - 1) : this._currentAutoSizedIndex;
            this._widthsInPixels[adjustIndex] += _totalWidthPixels - screenTotal;
        }

        // calculate widths in percentages
        // and assure percentage integrity - adjust auto-sized column if needed to ensure layout does not break
        screenTotal = 0;
        for (var i = 0; i < colCount; i++) {
            this._widthsInPercentages[i] = Math.round((parseInt(this._widthsInPixels[i]) / parseInt(_totalWidthPixels)) * 100);
            screenTotal += this._widthsInPercentages[i];
        }

        if (screenTotal > _totalWidthPercentages) {
            var adjustIndex = (this._currentAutoSizedIndex == null) ? (colCount - 1) : this._currentAutoSizedIndex;
            this._widthsInPercentages[adjustIndex] -= screenTotal - _totalWidthPercentages;
        }
    },

    _updateWidths: function (objChange) {
        this._widthsInPixels[objChange.LeftColumnIndex] = parseInt(objChange.LeftColumnWidthPixels);
        this._widthsInPixels[objChange.RightColumnIndex] = parseInt(objChange.RightColumnWidthPixels);
        this._widthsInPercentages[objChange.LeftColumnIndex] = parseInt(objChange.LeftColumnWidthPercentages);
        this._widthsInPercentages[objChange.RightColumnIndex] = parseInt(objChange.RightColumnWidthPercentages);
    },

    _getTotalHorizontalMarginsInPixels: function () {
        var marginsTotal = 0;
        var colCount = this._outerColumns.length;
        for (var i = 0; i < colCount; i++) {
            marginsTotal += this.GetColumnMarginInPixels(i, this._LEFT) + this.GetColumnMarginInPixels(i, this._RIGHT);
        }
        return marginsTotal;
    },

    _convertToPercentages: function (pixelVal) {
        var _totalWidthPixels = this._layoutRoot.clientWidth;
        return Math.round((pixelVal / _totalWidthPixels) * 100);
    },

    _convertToPixels: function (percentageVal) {
        var _totalWidthPixels = this._layoutRoot.clientWidth;
        return Math.round((percentageVal / 100) * _totalWidthPixels);
    },

    _ensureLayoutIntegrity: function (fixedVal, fixedColIndex, adjustableVal, adjustableColIndex, unit) {
        var originalSum;
        fixedVal = Number(fixedVal);
        adjustableVal = Number(adjustableVal);
        if (unit == this._PIXELS) {
            originalSum = this._widthsInPixels[fixedColIndex] + this._widthsInPixels[adjustableColIndex];
            if (originalSum < (fixedVal + adjustableVal)) {
                return adjustableVal - ((fixedVal + adjustableVal) - originalSum);
            }
        } else if (unit == this._PERCENTAGES) {
            originalSum = this._widthsInPercentages[fixedColIndex] + this._widthsInPercentages[adjustableColIndex];
            if (originalSum < (fixedVal + adjustableVal)) {
                var adjustVal = adjustableVal - ((fixedVal + adjustableVal) - originalSum);
                return adjustVal;
            }
        }
        return adjustableVal;
    },

    _getLeftDraggingBound: function (sliderIndex) {
        var sliderInitLeft = this.GetColumnRightInPixels(sliderIndex, true)
        var leftColumnWidth = this._widthsInPixels[sliderIndex];
        return sliderInitLeft - leftColumnWidth + this._minimumPixelColumnWidth;
    },

    _getRightDraggingBound: function (sliderIndex) {
        var sliderInitLeft = this.GetColumnLeftInPixels(sliderIndex + 1, true)
        var rightColumnWidth = this._widthsInPixels[sliderIndex + 1];
        return sliderInitLeft + rightColumnWidth - this._minimumPixelColumnWidth;
    },

    /* ********************************* utility ********************************* */
    _getMiminumColumnWidth: function () {
        if (this._unitMode == this._PIXELS) {
            return this._minimumPixelColumnWidth;
        } else if (this._unitMode == this._PERCENTAGES) {
            return this._minimumPercentageColumnWidth;
        }
    },

    _getColumnInternalUnitMode: function (widthString) {
        if (widthString.indexOf('%') > -1) {
            return this._PERCENTAGES; ;
        }
        if (widthString.indexOf('px') > -1) {
            return this._PIXELS;
        }
    },

    _getColumnInfos: function () {
        var colInfos = [];

        var colCount = this._outerColumns.length;
        for (var i = 0; i < colCount; i++) {
            var width = (this._unitMode == this._PIXELS) ? this._widthsInPixels[i] : this._widthsInPercentages[i];

            var colInfo = {
                name: this._controlPanelInfo.ColumnLabel + ' ' + (i + 1),
                width: width + this.GetUnitString(this._unitMode)
            };

            colInfos.push(colInfo);
        }

        return colInfos;
    },

    _generateLabelStrings: function () {
        var labelStrings = [];

        var colInfos = this._getColumnInfos();
        for (var i = 0; i < colInfos.length; i++) {
            var label = '<strong>' + colInfos[i].name + '</strong><br />' + colInfos[i].width;
            labelStrings.push(label);
        }

        return labelStrings;
    }
};

Telerik.Sitefinity.Web.UI.WidthEditor.registerClass('Telerik.Sitefinity.Web.UI.WidthEditor', Telerik.Sitefinity.Web.UI.EditorBase);