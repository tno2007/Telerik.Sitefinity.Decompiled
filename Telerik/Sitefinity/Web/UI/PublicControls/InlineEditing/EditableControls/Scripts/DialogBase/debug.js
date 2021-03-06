define(["text!DialogBaseTemplate!strip"], function (html) {
    function DialogBase(customOptions) {
        var that = this;
        this.dialogTemplate = html;
        this.kendoWindow = null;
        this.initialized = false;
        this.windowOptions =
            customOptions ?
            customOptions :
            {
                animation: false,
                width: "400px",
                modal: true,
                actions: {},
                title: false,
                viewable: false
            }

        this.kendoWindow = $('<div>')
           .append(this.dialogTemplate)
           .kendoWindow(this.windowOptions)
           .data("kendoWindow");
        
        $(this.kendoWindow.wrapper).addClass("sfInlineEditDlgWrp");
        $(this.kendoWindow.wrapper).addClass("sfPreventClickOutside");

        this.viewModel = kendo.observable({
            done: function (e) {
                e.preventDefault();
                $(that).trigger("doneSelected", that);
            },

            close: function (e) {
                e.preventDefault();
                $(that).trigger("closeSelected", that);
            },
            doneButtonText: "",
            titleText: ""
        });
        return (this);
    }

    DialogBase.prototype = {

        init: function() {
            if (!this.initialized) {
                kendo.bind(this.getButtonArea(), this.viewModel);
                kendo.bind(this.getTitleArea(), this.viewModel);
                this.initialized = true;
            }
        },

        open: function () {
            this.init();
            this.kendoWindow.center().open();
        },

        close: function () {
            this.kendoWindow.close();
        },

        center: function() {
            this.kendoWindow.center();
        },

        getKendoWindow: function () {
            return this.kendoWindow;
        },

        getButtonArea: function() {
            return $(this.kendoWindow.wrapper).find('#buttonArea');
        },

        getTitleArea: function() {
            return $(this.kendoWindow.wrapper).find('#titleArea');
        },

        getContentPlaceHolder: function () {
            return $(this.kendoWindow.wrapper).find('#contentPlaceHolder');
        }
    };

    return (DialogBase);
});