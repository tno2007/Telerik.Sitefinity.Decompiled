define(["text!DataTypesTemplate!strip"], function (html) {
    function DataTypes() {
        var that = this;

        this.template = html;
        this.relatedDataServiceUrl = null;
        this.dataSource = null;
        this.dataSourceProviders = new kendo.data.DataSource({});
        this.dataTypesDropdown = null;

        this.viewModel = kendo.observable({
            currentValue: "DataTypes",

            back: function (e, s) {
                e.preventDefault();
                that.back(e, s);
            },
            widgetTypeNameChanged: function (e, s) {
                var selectedValue = $(e.target).find(":selected").val();
                var isCustom = selectedValue !== $(e.target).find(":first").val();
                that.viewModel.set("isWidgetTypeNameVisible", isCustom);
            }
        });

        return (this);
    };

    DataTypes.prototype = {
        initialize: function (siteBaseUrl) {
            var that = this;

            this.relatedDataServiceUrl = siteBaseUrl + "restapi/sitefinity/related-data/data-types";

            this.dataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: this.relatedDataServiceUrl,
                        dataType: "json"
                    }
                }
            });

            kendo.bind($(this.template), this.viewModel);

            this.dataTypesDropdown = $("#dataTypes").kendoDropDownList({
                animation: false,
                height: 150,
                dataTextField: "Name",
                dataValueField: "ClrType",
                dataSource: this.dataSource,
                cascade: function (e) {
                    $('#dataTypesValidationMirrorField').val(e.sender.dataItem().Name);
                },
                open: function () {
                    if (typeof dialogBase != "undefined")
                        dialogBase.resizeToContent();
                },
                close: function () {
                    if (typeof dialogBase != "undefined")
                        dialogBase.resizeToContent();
                }
            }).data("kendoDropDownList");

            if (typeof dialogBase != "undefined")
                dialogBase.resizeToContent();

            var multisiteVal = $('#multisiteVal').val();
            if (multisiteVal) {
                multisiteVal = multisiteVal.toLowerCase();
            }

            var isMultisite = multisiteVal == 'true';
            if (isMultisite) {
                $('#detailsBtn').click(function () {
                    if ($('#detailsCnt').hasClass('sfDisplayNone'))
                        $('#detailsCnt').removeClass('sfDisplayNone');
                    else
                        $('#detailsCnt').addClass('sfDisplayNone');
                    dialogBase.resizeToContent();
                });
            } else {
                $('#detailsWrapper').addClass('sfDisplayNone');
            }
        },
        hideDropDown: function ($dropDownSelector) {
            if ($dropDownSelector && $dropDownSelector.element) {
                $dropDownSelector.element.closest(":has(h2)").find("h2").last().hide();
                $dropDownSelector.element.closest(".k-widget").hide();
            }
        },
        showDropDown: function ($dropDownSelector) {
            if ($dropDownSelector && $dropDownSelector.element) {
                $dropDownSelector.element.closest(":has(h2)").find("h2").last().show();
                $dropDownSelector.element.closest(".k-widget").show();
            }
        }
    };

    return (DataTypes);
});