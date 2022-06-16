Telerik.Sitefinity.Web.UI.EditorBase = function(visualEditorContainer, outerColumns, innerColumns) {
    this._PIXELS = 0;
    this._PERCENTAGES = 1;

    this._TOP = 'top';
    this._RIGHT = 'right';
    this._BOTTOM = 'bottom';
    this._LEFT = 'left';
	
	this._visualEditorContainer = visualEditorContainer;
	this._outerColumns = outerColumns;
	this._innerColumns = innerColumns;
	
	this._labelClass = 'sfColumnLabel';
	this._labelStrings = [];
}

Telerik.Sitefinity.Web.UI.EditorBase.prototype = {
    // set up and tear down
    initialize: function () {
        Telerik.Sitefinity.Web.UI.EditorBase.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.EditorBase.callBaseMethod(this, 'dispose');
    },

    // functions
    GetUnitString: function (unit) {
        if (unit == this._PIXELS) {
            return 'px';
        } else if (unit == this._PERCENTAGES) {
            return '%';
        }
        return 'N/A';
    },

    GetStyleNumber: function (styleValue) {
        if (styleValue == null || styleValue.length == 0) {
            return 0;
        }
        // remove pixel or percentage characters
        styleValue = styleValue.replace('%', '');
        styleValue = styleValue.replace('px', '');
        styleValue = styleValue.replace('pt', '');
        styleValue = styleValue.replace(' ', '');
        return Number(styleValue);
    },

    GetStyleString: function (value, unit) {
        if (unit == this._PIXELS) {
            return value + 'px';
        } else if (unit == this._PERCENTAGES) {
            return value + '%';
        }
        alert('Unit "' + unit + '" is not supported.');
    },

    GenerateLabels: function (labelStrings) {
        if (labelStrings != null) {
            this._labelStrings = labelStrings;
        }
        if (!this._labelsCreated) {
            var colCount = this._innerColumns.length;
            for (i = 0; i < colCount; i++) {

                var col = this._innerColumns[i];
                var labelTop = col.offsetTop + 5;
                var labelLeft = col.offsetLeft + 5;
                if ($.browser && $.browser.msie && $.browser.version < 8) {
                    if (col.parentNode) {
                        labelLeft += col.parentNode.offsetLeft;
                    }
                }

                var labelStyle = 'position: absolute; top:' + labelTop + 'px; left: ' + labelLeft + 'px';

                var label = document.createElement('span');
                //label.setAttribute('class', this._labelClass);
                //label.setAttribute('style', labelStyle);
                $(label).addClass(this._labelClass);
                label.style.position = 'absolute';
                label.style.top = labelTop + 'px';
                label.style.left = labelLeft + 'px';
                
                label.innerHTML = this._labelStrings[i];
                this._visualEditorContainer.appendChild(label);
            }
        }
        this._labelsCreated = true;
    },

    ClearLabels: function () {
        $(this._visualEditorContainer).find('.' + this._labelClass).each(function () {
            $(this).remove();
        });
        this._labelsCreated = false;
    },

    UpdateLabels: function (labelStrings) {
        this.ClearLabels();
        this.GenerateLabels(labelStrings);
    },

    ClearUI: function () {
        this.ClearLabels();
        $('.sfLayoutWidthSlider').remove();
    },

    /* **************************** calculations **************************** */
    GetOuterColumnHeight: function (colIndex) {
        return this._outerColumns[colIndex].clientHeight;
    },

    GetColumnLeftInPixels: function (colIndex, absolute) {
        return this._getElementLeftInPixels(this._outerColumns[colIndex], absolute);
    },

    GetColumnRightInPixels: function (colIndex, absolute) {
        return this._getElementLeftInPixels(this._outerColumns[colIndex], absolute) + this._getElementWidth(this._outerColumns[colIndex]);
    },

    // NOTE: this implementaion won't work for margins in percentages
    GetColumnMarginInPixels: function (colIndex, side) {
        return this._getElementMargin(this._innerColumns[colIndex], side);
    },

    /* **************************** utility left **************************** */
    _getElementLeftInPixels: function (el, absolute) {
        if (absolute == null || absolute == false) {
            return el.offsetLeft;
        }

        var left = 0;
        while (el != null) {
            left += el.offsetLeft;
            el = el.offsetParent;
        }
        return left;
    },

    _getElementWidth: function (el) {
        return el.clientWidth;
    },

    _getElementMargin: function (el, side) {
        var margin;
        switch (side) {
            case this._TOP:
                margin = el.style.marginTop;
                break;
            case this._RIGHT:
                margin = el.style.marginRight;
                break;
            case this._BOTTOM:
                margin = el.style.marginBottom;
                break;
            case this._LEFT:
                margin = el.style.marginLeft;
                break;
        }

        return this.GetStyleNumber(margin);
    }
};

Telerik.Sitefinity.Web.UI.EditorBase.registerClass('Telerik.Sitefinity.Web.UI.EditorBase', Sys.Component);