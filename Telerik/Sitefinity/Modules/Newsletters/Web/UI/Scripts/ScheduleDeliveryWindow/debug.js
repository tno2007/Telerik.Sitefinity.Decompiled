Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.ScheduleDeliveryWindow = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.ScheduleDeliveryWindow.initializeBase(this, [element]);

    this._cancelButtonClickDelegate = null;
    this._scheduleButtonClickDelegate = null;

    this._cancelButton = null;
    this._scheduleButton = null;
    this._clientLabelManager = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.ScheduleDeliveryWindow.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.ScheduleDeliveryWindow.callBaseMethod(this, "initialize");

        this._cancelButtonClickDelegate = Function.createDelegate(this, this._cancelButtonClick);
        this._scheduleButtonClickDelegate = Function.createDelegate(this, this._scheduleButtonClick);

        $addHandler(this.get_cancelButton(), 'click', this._cancelButtonClickDelegate);
        $addHandler(this.get_scheduleButton(), 'click', this._scheduleButtonClickDelegate);
    },

    dispose: function () {
        if (this._cancelButtonClickDelegate) {
            if (this.get_cancelButton()) {
                $removeHandler(this.get_cancelButton(), 'click', this._cancelButtonClickDelegate);
            }
            delete this._cancelButtonClickDelegate;
        }

        if (this._scheduleButtonClickDelegate) {
            if (this.get_scheduleButton()) {
                $removeHandler(this.get_scheduleButton(), 'click', this._scheduleButtonClickDelegate);
            }
            delete this._scheduleButtonClickDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.ScheduleDeliveryWindow.callBaseMethod(this, "dispose");
    },

    reset: function () {
        var currentTime = new Date();
        this.get_scheduleDeliveryDate().set_value(currentTime);
    },

    _cancelButtonClick: function (sender, args) {
        this.close();
    },

    _scheduleButtonClick: function (sender, args) {
        var deliveryDate = this.get_scheduleDeliveryDate().get_value();
        if (deliveryDate) {
            this.close(deliveryDate);
        }
    },

    get_cancelButton: function () {
        return this._cancelButton;
    },
    set_cancelButton: function (value) {
        this._cancelButton = value;
    },
    get_scheduleButton: function () {
        return this._scheduleButton;
    },
    set_scheduleButton: function (value) {
        this._scheduleButton = value;
    },
    get_scheduleDeliveryDate: function () {
        return this._scheduleDeliveryDate;
    },
    set_scheduleDeliveryDate: function (value) {
        this._scheduleDeliveryDate = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.ScheduleDeliveryWindow.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.ScheduleDeliveryWindow', 
    Telerik.Sitefinity.Web.UI.Kendo.KendoWindow);