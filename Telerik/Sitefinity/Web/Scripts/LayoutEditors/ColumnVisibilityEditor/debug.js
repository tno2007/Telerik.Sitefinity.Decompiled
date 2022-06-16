﻿//================================ Width editor ===========================================================================//
Telerik.Sitefinity.Web.UI.ColumnVisibilityEditor = function (layoutRoot, changeButtonId, columnCount) {
    this._changeButtonId = changeButtonId;
    this._columnCount = columnCount;
    this._layoutRoot = layoutRoot;
    this._hiddenColumns = {};
    this._currentColumn = 1;
    this._mediaQueryList = [];
    Telerik.Sitefinity.Web.UI.ColumnVisibilityEditor.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.ColumnVisibilityEditor.prototype = {

    //Set width to the splitter panes and record ratios that help convert
    initialize: function () {

        $("#columnVisibilityDialog").kendoWindow(
          {
              visible: false,
              resizable: false,
              width: "525px",
              height: "auto",
              modal: true,
              animation: false,
              actions: ["Close"]
          });

        var dialog = $("#columnVisibilityDialog").data("kendoWindow");

        var that = this;

        this.hideRelevantClasses();

        if (!$("#column-visibility-tabstrip").data("kendoTabStrip")) {
            $("#column-visibility-tabstrip").kendoTabStrip({
                animation: false
            });
        }

        var onSelect = function (e) {
            that._currentColumn = 1;
            if ($(e.item).hasClass("sfColumn1Tab")) {
                that._currentColumn = 1;
            } else if ($(e.item).hasClass("sfColumn2Tab")) {
                that._currentColumn = 2;
            } else if ($(e.item).hasClass("sfColumn3Tab")) {
                that._currentColumn = 3;
            } else if ($(e.item).hasClass("sfColumn4Tab")) {
                that._currentColumn = 4;
            } else if ($(e.item).hasClass("sfColumn5Tab")) {
                that._currentColumn = 5;
            }
            $("#columnVisibilityCurrentColumn").html(that._currentColumn);
            that.loadVisibilityState.call(that);
        };

        if ($("#column-visibility-tabstrip").length > 0) {
            $("#column-visibility-tabstrip").data("kendoTabStrip").wrapper.addClass("k-sitefinity");

            $("#column-visibility-tabstrip").data("kendoTabStrip").unbind("select");
            $("#column-visibility-tabstrip").data("kendoTabStrip").bind("select", onSelect);
        }

        $('#' + this._changeButtonId).unbind('click').click(function () {
            var window = $("#columnVisibilityDialog").data("kendoWindow");
            window.center();
            window.open();
            that._dialogScrollToTop(window);
            var zIndex = parseInt(jQuery(".sfVisualLayoutEditor:visible").css("z-index"));
            window.wrapper.css("z-index", zIndex + 3);
            jQuery(".k-overlay:visible").css("z-index", zIndex + 2);
        });

        this.prepareTabs(this._columnCount);

        $(".groupOrRulesList input").unbind('click').click(function (e) {

            var mediaQueryName = $(this).parent().attr("class");

            if ($(this).is(":checked")) {
                that.showColumn.call(that, mediaQueryName);
            } else {
                that.hideColumn.call(that, mediaQueryName);
            }
        });

        $("#column-visibility-done").unbind('click').click(function () {

            // remove previously generated classes
            var currentClassAtr = $(that._layoutRoot).attr("class");
            var currentClasses = currentClassAtr.split(" ");

            for (var i = 0; i < currentClasses.length; i++) {
                if (currentClasses[i].indexOf("_hide_") > -1) {
                    $(that._layoutRoot).removeClass(currentClasses[i]);
                }
            }

            var cssClass = that.generateClass();
            $(that._layoutRoot).addClass(cssClass);
            dialog.close();
        });

        $("#column-visibility-cancel").unbind('click').click(function () {
            dialog.close();
        });

        this.loadVisibilityState();

        Telerik.Sitefinity.Web.UI.ColumnVisibilityEditor.callBaseMethod(this, 'initialize');
    },


    dispose: function () {
        Telerik.Sitefinity.Web.UI.ColumnVisibilityEditor.callBaseMethod(this, 'dispose');
    },

    prepareTabs: function (columnCount) {
        $(".sfColumn1Tab, .sfColumn2Tab, .sfColumn3Tab, .sfColumn4Tab, .sfColumn5Tab").show();
        switch (columnCount) {
            case 1:
                $(".sfColumn2Tab, .sfColumn3Tab, .sfColumn4Tab, .sfColumn5Tab").hide();
                break;
            case 2:
                $(".sfColumn3Tab, .sfColumn4Tab, .sfColumn5Tab").hide();
                break;
            case 3:
                $(".sfColumn4Tab, .sfColumn5Tab").hide();
                break;
            case 4:
                $(".sfColumn5Tab").hide();
                break;
        }
    },

    hideColumn: function (mediaQueryName) {
        if (!this._hiddenColumns[mediaQueryName]) {
            this._hiddenColumns[mediaQueryName] = [];
        }

        this._hiddenColumns[mediaQueryName].push(this._currentColumn);
    },

    showColumn: function (mediaQueryName) {
        if (!this._hiddenColumns[mediaQueryName]) {
            return;
        }

        for (i = 0; i < this._hiddenColumns[mediaQueryName].length; i++) {
            if (this._hiddenColumns[mediaQueryName][i] == this._currentColumn) {
                this._hiddenColumns[mediaQueryName].splice(i, 1);
            }
        }

    },

    hideRelevantClasses: function () {
        var that = this;
        var classAtr = $(this._layoutRoot).attr("class");
        var classes = classAtr.split(" ");
        for (var i = 0; i < classes.length; i++) {
            if (this.isHideClass(classes[i])) {
                $(".groupOrRulesList input").each(function (z, val) {
                    var ruleName = $(this).parent().attr("class");
                    if (classes[i].startsWith("sf_" + that.generateMediaQueryShorthand(ruleName))) {
                        if (!that._hiddenColumns[ruleName]) {
                            that._hiddenColumns[ruleName] = [];

                            if (!(ruleName in that._mediaQueryList)) {
                                that._mediaQueryList.push(ruleName);
                            }
                        }
                        that._hiddenColumns[ruleName].push(parseInt(classes[i].substr(classes[i].length - 1, 1)));
                    }
                });
            }
        }
    },

    isHideClass: function (cssClass) {
        var isHideClass = false;
        $(".groupOrRulesList input").each(function (i, val) {
            var ruleName = $(val).parent().attr("class");
            if (ruleName) {
                if (cssClass.startsWith("sf_" + ruleName.toLowerCase())) {
                    isHideClass = true;
                    return false;
                }
            }
        });
        return isHideClass;
    },

    generateMediaQueryShorthand: function (mediaQueryName) {
        return mediaQueryName.replace(" ", "").toLowerCase();
    },

    loadVisibilityState: function () {
        $(".groupOrRulesList input").attr("checked", "checked");
        for (var mediaQueryName in this._hiddenColumns) {
            for (i = 0; i < this._hiddenColumns[mediaQueryName].length; i++) {
                if (this._hiddenColumns[mediaQueryName][i] == this._currentColumn) {
                    $("." + mediaQueryName + " input").prop("checked", false);
                    break;
                }
            }
        }
    },


    generateClass: function () {

        var cssClasses = "";

        for (var mediaQuery in this._hiddenColumns) {
            for (i = 0; i < this._hiddenColumns[mediaQuery].length; i++) {
                var cssClass = "sf_" + this.generateMediaQueryShorthand(mediaQuery) + "_" + this._columnCount + "cols_hide_" + this._hiddenColumns[mediaQuery][i] + " ";
                cssClasses += cssClass;
            }
        }

        return cssClasses;
    },

    _dialogScrollToTop: function (dlg) {
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        dlg.wrapper.css({ "top": scrollTop });
    },

    checkWetherAllColumnsAreHidden: function () {

        var mediaQueryKeys = $.map(this._hiddenColumns, function (n, i) { return i; });

        if (mediaQueryKeys.length == this._mediaQueryList.length) {
            for (var i = 0; i < this._mediaQueryList.length; i++) {

                if (!this._hiddenColumns[this._mediaQueryList[i]] || this._hiddenColumns[this._mediaQueryList[i]].length != this._columnCount) {
                    return false;
                }

                for (var j = 1; j <= this._columnCount; j++) {
                    if ($.inArray(j, this._hiddenColumns[this._mediaQueryList[i]]) == -1) {
                        return false;
                    }
                }

            }
        }
        else {
            return false
        }

        return true;
    },

    calculateHiddenColumns: function () {
        var columnsDict = {};
        var currentCol = null;

        for (var mediaQueryName in this._hiddenColumns) {
            for (i = 0; i < this._hiddenColumns[mediaQueryName].length; i++) {

                currentCol = this._hiddenColumns[mediaQueryName][i];

                if (Object.prototype.toString.call(columnsDict[currentCol]) === '[object Array]') {
                    columnsDict[currentCol].push(mediaQueryName);
                }
                else {
                    columnsDict[currentCol] = new Array();
                    columnsDict[currentCol].push(mediaQueryName);
                }
            }
        }
        return columnsDict;
    },

    executeOnClose: function (that, callBack) {
        if ($("#columnVisibilityDialog").length > 0) {
            $("#columnVisibilityDialog").data("kendoWindow")
                .bind("close", function (e) {
                    callBack(that);
                });
        }
    }
};

Telerik.Sitefinity.Web.UI.ColumnVisibilityEditor.registerClass('Telerik.Sitefinity.Web.UI.ColumnVisibilityEditor', Sys.Component);

if (typeof String.prototype.startsWith != 'function') {
    // see below for better implementation!
    String.prototype.startsWith = function (str) {
        return this.indexOf(str) == 0;
    };
}