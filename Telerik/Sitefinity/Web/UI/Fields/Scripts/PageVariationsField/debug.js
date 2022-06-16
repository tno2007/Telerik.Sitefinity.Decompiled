﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.PageVariationsField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.PageVariationsField.initializeBase(this, [element]);

    this._element = element;
    this._viewModel = null;
    this._clientLabelManager = null;
    this._variations = this.get_defaultVariations();
    this._minVariationsCount = 2;
    this._maxVariationsCount = 10;
    this._maxVariationNameLength = 255;
    this._variationSequenceNumber = 0;
    this._variationNameBase = "Variation #";
}
Telerik.Sitefinity.Web.UI.Fields.PageVariationsField.prototype =
{
    /* -------------------- set up and tear down ----------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.PageVariationsField.callBaseMethod(this, "initialize"); 
        this._bindList();
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.PageVariationsField.callBaseMethod(this, "dispose");
    },
    /* --------- Public Methods ------------ */
    reset: function () {
        Telerik.Sitefinity.Web.UI.Fields.PageVariationsField.callBaseMethod(this, "reset");
        this._variations = this.get_defaultVariations();
        this._bindList();
    },

    validate: function () {
        if (this._validator && this._isToValidate()) {
            var isValid = true;
            var sumOfDistributions = 0;
            this._validator.set_violationMessages([]);
            var value = this.get_value();

            if (value.length < this._minVariationsCount || value.length > this._maxVariationsCount) {
                isValid = false;
                var countMsgFormat = this.get_clientLabelManager().getLabel("Labels", "VariationsCountValidationMessage");
                var countMsg = String.format(countMsgFormat, this._minVariationsCount, this._maxVariationsCount);
                this._validator.get_violationMessages().push(countMsg);
            }

            if (isValid) {
                for (var i = 0; i < value.length; i++) {
                    sumOfDistributions += value[i].Distribution;
                    if (!value[i].Name || /^\s*$/.test(value[i].Name)) {
                        isValid = false;
                        var requiredMsg = this.get_clientLabelManager().getLabel("Labels", "VariationNamesRequiredValidationMessage");
                        this._validator.get_violationMessages().push(requiredMsg);
                        break;
                    }

                    if (value[i].Name.length > this._maxVariationNameLength) {
                        isValid = false;
                        var requiredMsg = this.get_clientLabelManager().getLabel("Labels", "VariationNamesLengthValidationMessage");
                        this._validator.get_violationMessages().push(requiredMsg);
                        break;
                    }

                    var variationsWithSameNameCount = jQuery.grep(value, function (v) { return v.Name === value[i].Name && v !== value[i]; }).length;
                    if (variationsWithSameNameCount > 0) {
                        isValid = false;
                        var duplicateMsgFormat = this.get_clientLabelManager().getLabel("Labels", "DuplicateVariationName");
                        var duplicateMsg = String.format(duplicateMsgFormat, value[i].Name);
                        this._validator.get_violationMessages().push(duplicateMsg);
                        break;
                    }

                    if (value[i].Distribution < 1 || value[i].Distribution > 99) {
                        isValid = false;
                        var rangeMessage = this.get_clientLabelManager().getLabel("Labels", "VariationDistributionRangeValidationMessage");
                        this._validator.get_violationMessages().push(rangeMessage);
                        break;
                    }
                }
            }

            if (isValid && sumOfDistributions !== 100) {
                isValid = false;
                var sumMsg = this.get_clientLabelManager().getLabel("Labels", "VariationDistributionsSumValidationMessage");
                this._validator.get_violationMessages().push(sumMsg);
            }

            this._refreshViolationMessage(isValid);

            return isValid;
        }

        return true;
    }, 

    _bindList: function () {
        var that = this;
        this._setVariationSequenceNumber();
        this._viewModel = kendo.observable({
            variations: this._variations,
            canAdd: this._variations.length < that._maxVariationsCount,
            removeVariation: function (e) {
                var isEqualDistribution = that._isEqualDistribution(this.variations);
                this.variations.remove(e.data);
                if (isEqualDistribution) {
                    that._setEqualDistribution(this.variations);
                }

                that._setCanDelete(this.variations);
                that._setCanAdd(this.variations);
            },
            addVariation: function (e) { 
                var isEqualDistribution = that._isEqualDistribution(this.variations);
                var maxOrdinal = 0;
                if (this.variations.length > 0) {
                    maxOrdinal = this.variations.reduce(function (a, b) {
                        return Math.max(a, b.ordinal);
                    }, 0);
                }
                
                this.variations.push({
                    name: that._variationNameBase + that._variationSequenceNumber,
                    distribution: 0,
                    isOriginal: false,
                    isWinner: false,
                    ordinal: maxOrdinal + 1
                });

                that._variationSequenceNumber++;

                if (isEqualDistribution) {
                    that._setEqualDistribution(this.variations);
                }

                that._setCanDelete(this.variations);
                that._setCanAdd(this.variations);
            }
        });

        kendo.bind(this.get_kendoViewContainer(), this._viewModel);
        this._viewModel.bind("change", function (e) {
            that._valueChangedHandler();
        });
    },
     
    _isEqualDistribution: function (variations) {
        var variationsCount = variations.length;
        var distribution = this._getEqualDistribution(variationsCount);
        for (var i = 0; i < variations.length; i++) {
            if (variations[i].get("distribution") !== distribution[i])
                return false;
        }

        return true;
    },

    _setEqualDistribution: function (variations) {
        var variationsCount = variations.length;
        var distribution = this._getEqualDistribution(variationsCount);
        for (var i = 0; i < variations.length; i++) {
            variations[i].set("distribution", distribution[i]);
        }
    },
     
    _setVariationSequenceNumber: function () { 
        for (var i = 0; i < this._variations.length; i++) {
            var variationName = this._variations[i].name;

            if (variationName.startsWith(this._variationNameBase)) {
                var number = parseInt(variationName.split(this._variationNameBase)[1]);

                if (this._variationSequenceNumber <= number) {
                    this._variationSequenceNumber = number + 1;
                }
            }
        }
    },

    _setCanDelete: function (variations) {
        if (variations) {
            var canDelete = variations.length > this._minVariationsCount;
            for (var i = 0; i < variations.length; i++) {
                variations[i].set("canDelete", canDelete);
            }
        }
    },

    _setCanAdd: function (variations) {
        if (variations) {
            var canAdd = variations.length < this._maxVariationsCount;
            this._viewModel.set("canAdd", canAdd);
        }
    },

    _getEqualDistribution: function (variationsCount) {
        var result = [];
        if (variationsCount > 0) {
            var distributionPerVariation = Math.floor(100 / variationsCount);
            var distributedSoFar = 0;
            for (var i = 0; i < variationsCount; i++) {
                if ((100 - distributedSoFar) >= 2 * distributionPerVariation && i !== variationsCount - 1) {
                    result.push(distributionPerVariation);
                    distributedSoFar += distributionPerVariation;
                } else {
                    result.push(100 - distributedSoFar);
                    distributedSoFar = 100;
                }
            }
        }

        // Assign distribution in reverse order, so that higher distribution is on top
        return result.reverse();
    },

    /* ---------- Properties ------- */
    get_element: function () {
        return this._element;
    },

    get_kendoViewContainer: function () {
        return jQuery(this.get_element()).find('#kendoViewContainer');
    },

    get_defaultVariations: function () {
        return new kendo.data.ObservableArray([{
            name: "Original",
            distribution: 50,
            isOriginal: true,
            ordinal: 0,
            isWinner: false,
            canDelete: false
        }, {
            name: "Variation #1",
            distribution: 50,
            isOriginal: false,
            ordinal: 1,
            isWinner: false,
            canDelete: false
        }]);
    },

    // Gets the value of the field control.
    get_value: function () {
        return this._variations.map(function (x) {
            return {
                Id: x.id,
                Name: x.name,
                Distribution: x.distribution,
                IsOriginal: x.isOriginal,
                IsWinner: x.isWinner,
                Ordinal: x.ordinal
            };
        });
    },
    // Sets the value of the field control.
    set_value: function (value) {
        if (value && value.length) {
            value = value.sort(function (a, b) {
                return a.Ordinal - b.Ordinal;
            });

            this._variations = new kendo.data.ObservableArray([]);
            var canDelete = value.length > this._minVariationsCount;
            for (var i = 0; i < value.length; i++) {
                this._variations.push({
                    id: value[i].Id,
                    name: value[i].Name,
                    distribution: value[i].Distribution,
                    isOriginal: value[i].IsOriginal,
                    ordinal: value[i].Ordinal,
                    isWinner: value[i].IsWinner,
                    canDelete: canDelete
                });
            }

            this._bindList();
        }
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.PageVariationsField.registerClass("Telerik.Sitefinity.Web.UI.Fields.PageVariationsField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);
