Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.GoalsField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.GoalsField.initializeBase(this, [element]);

    this._element = element;
    this._goals = new kendo.data.ObservableArray([]);
    this._goalSelector = null;
    this._culture = null;
    this._onCloseDelegate = null;
    this._pageId = null;
    this._clientLabelManager = null;
}
Telerik.Sitefinity.Web.UI.Fields.GoalsField.prototype =
{
    /* -------------------- set up and tear down ----------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.GoalsField.callBaseMethod(this, "initialize");
        var that = this;
        this._onCloseDelegate = Function.createDelegate(this, this._onClose);
        if (this.get_goalSelector()) {
            this.get_goalSelector().add_close(this._onCloseDelegate);
            this.get_goalSelector()._validateSelectedPage = function (pageId, onSuccess, onError) {
                that.validateSelectedPage(pageId, onSuccess, onError);
            };
        }

        this._bindList();
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.GoalsField.callBaseMethod(this, "dispose");

        if (this._onCloseDelegate) {
            delete this._onCloseDelegate;
        }
    },
    /* --------- Public Methods ------------ */
    reset: function () {
        Telerik.Sitefinity.Web.UI.Fields.GoalsField.callBaseMethod(this, "reset");
        this._goals = new kendo.data.ObservableArray([]);
        this._bindList();
    },

    _bindList: function () {
        var that = this;

        this._viewModel = kendo.observable({
            goals: this._goals,
            hasGoals: this._goals.length > 0,
            removeGoal: function (e) {
                var isPrimary = e.data.get("isPrimary");
                this.goals.remove(e.data);

                if (isPrimary && this.goals.length > 0) {
                    this.goals[0].set("isPrimary", true);
                }

                this.set("hasGoals", this.goals.length > 0);
            },
            addGoal: function (e) {
                var transferData =
                    {
                        culture: that.get_culture(),
                        pageId: that.get_pageId()
                    };
                that.get_goalSelector().show(transferData);
            },
            setPrimary: function (e) {
                for (var i = 0; i < this.goals.length; i++) {
                    this.goals[i].set("isPrimary", false);
                }

                e.data.set("isPrimary", true);
            }
        });

        kendo.bind(this.get_kendoViewContainer(), this._viewModel);
        this._viewModel.bind("change", function (e) {
            that._valueChangedHandler();
        });
    },

    _onClose: function (sender, e) {
        var createdGoal = e._data;
        if (createdGoal) {
            var maxOrdinal = 0;
            if (this._viewModel.goals.length > 0) {
                maxOrdinal = this._viewModel.goals.reduce(function (a, b) {
                    return Math.max(a, b.ordinal)
                }, 0);
            }
            createdGoal.isPrimary = !maxOrdinal;
            createdGoal.ordinal = maxOrdinal + 1;

            // Set goal name from resources
            createdGoal.goalName = this._get_goalName(createdGoal.goalNameResourceKey)

            this._viewModel.goals.push(createdGoal);
            this._viewModel.set("hasGoals", this._viewModel.goals.length > 0);
        }
    },

    validateSelectedPage: function (pageId, onSuccess, onError) {
    },

    /* ---------- Properties ------- */
    get_element: function () {
        return this._element;
    },

    get_kendoViewContainer: function () {
        return jQuery(this.get_element()).find('#kendoViewContainer');
    },

    // Gets the value of the field control.
    get_value: function () {
        return this._goals.map(function (x) {
            return {
                Id: x.id,
                GoalType: x.goalType,
                GoalNameResourceKey: x.goalNameResourceKey,
                PredicateOperator: x.predicateOperator,
                ObjectId: x.objectId,
                ObjectCulture: x.objectCulture,
                ObjectName: x.objectName,
                IsPrimary: x.isPrimary,
                Ordinal: x.ordinal
            };
        });
    },

    // Sets the value of the field control.
    set_value: function (value) {
        var that = this;
        if (value && value.length) {
            value = value.sort(function (a, b) {
                return a.Ordinal - b.Ordinal;
            });

            this._goals = new kendo.data.ObservableArray([]);
            for (var i = 0; i < value.length; i++) {
                this._goals.push({
                    id: value[i].Id,
                    goalType: value[i].GoalType,
                    goalNameResourceKey: value[i].GoalNameResourceKey,
                    goalName: that._get_goalName(value[i].GoalNameResourceKey),
                    predicateOperator: value[i].PredicateOperator,
                    objectId: value[i].ObjectId,
                    objectCulture: value[i].ObjectCulture,
                    objectName: value[i].ObjectName,
                    isPrimary: value[i].IsPrimary,
                    ordinal: value[i].Ordinal,
                    objectNameWrapper: value[i].PredicateOperator === 'contains' ? '"' : ''
                });
            }

            this._bindList();
        }
    },

    _get_goalName: function (goalNameResourceKey) {
        var goalName = this.get_clientLabelManager().getLabel("Labels", goalNameResourceKey)
        if (!goalName) {
            goalName = goalNameResourceKey;
        }

        return goalName;
    },

    get_goalSelector: function () {
        return this._goalSelector;
    },
    set_goalSelector: function (value) {
        this._goalSelector = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },

    get_culture: function () {
        return this._culture;
    },
    set_culture: function (value) {
        this._culture = value
    },

    get_pageId: function () {
        return this._pageId;
    },
    set_pageId: function (value) {
        this._pageId = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.GoalsField.registerClass("Telerik.Sitefinity.Web.UI.Fields.GoalsField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);
