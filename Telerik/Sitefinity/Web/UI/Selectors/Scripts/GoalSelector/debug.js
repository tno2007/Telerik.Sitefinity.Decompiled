Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.GoalSelector = function (element) {
    Telerik.Sitefinity.Web.UI.GoalSelector.initializeBase(this, [element]);
    this._viewModel = null;
    this._decConversionService = null;
    this._goalNextPage = null;
    this._conversions = null;
    this._forms = null;
    this._formsPageId = null;
    this._formsCulture = null;
    this._clientLabelManager = null;
    this._onPageSelectedDelegate = null;
}

Telerik.Sitefinity.Web.UI.GoalSelector.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.GoalSelector.callBaseMethod(this, "initialize");

        this._onPageSelectedDelegate = Function.createDelegate(this, this._onPageSelected);
        this.get_goalNextPage().add_selectorClosed(this._onPageSelectedDelegate);
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.GoalSelector.callBaseMethod(this, "dispose");

        if (this._onPageSelectedDelegate) {
            if (this.get_goalNextPage()) {
                this.get_goalNextPage().remove_selectorClosed(this._onPageSelectedDelegate);
            }

            delete this._onPageSelectedDelegate;
        }
    },
    show: function (transferedData) {
        Telerik.Sitefinity.Web.UI.GoalSelector.callBaseMethod(this, "show");
        this._bindForm(transferedData);

        if (this.get_goalNextPage()) {
            this.get_goalNextPage().reset();
            if (transferedData && transferedData.culture) {
                this.get_goalNextPage().set_culture(transferedData.culture);
            }
        }
    },

    _bindForm: function (transferedData) {
        var that = this;
        var pageId = transferedData.pageId;
        var culture = transferedData.culture;

        var isPredicateOperator = "is",
           isPredicateOperatorName = that.get_clientLabelManager().getLabel("Labels", "IsGoalPredicate"),
           containsPredicateOperator = "contains",
           containsPredicateOperatorName = that.get_clientLabelManager().getLabel("Labels", "ContainsGoalPredicate"),
           nextPageViewGoalType = that.get_clientLabelManager().getLabel("Labels", "NextPageViewGoalType"),
           submittedFormGoalType = that.get_clientLabelManager().getLabel("Labels", "FormSubmissionGoalType"),
           decConversionGoalType = that.get_clientLabelManager().getLabel("Labels", "DecConversionGoalType");


        this._viewModel = kendo.observable({
            goalTypes: [{ name: nextPageViewGoalType, value: Telerik.Sitefinity.Web.UI.Enums.GoalType.NextPageView },
                { name: submittedFormGoalType, value: Telerik.Sitefinity.Web.UI.Enums.GoalType.FormSubmission },
                { name: decConversionGoalType, value: Telerik.Sitefinity.Web.UI.Enums.GoalType.DecConversion }
            ],
            selectedGoalType: Telerik.Sitefinity.Web.UI.Enums.GoalType.NextPageView,
            predcateOperators: [{ name: isPredicateOperatorName, value: isPredicateOperator },
                { name: containsPredicateOperatorName, value: containsPredicateOperator }
            ],
            selectedPredicateOperator: isPredicateOperator,
            multiplePredicateOperators: true,
            singlePredicateOperator: false,
            conversions: that.get_conversions.call(that),
            selectedConversion: null,
            noConversionsAvailable: false,
            pageUrl: null,
            isPageFieldVisible: true,
            isPageUrlVisible: false,
            isConversionDropdownVisible: false,
            isFormsSubmissionSelected: false,
            errorMessage: "",
            save: function () {
                var result = {};
                if (this.errorMessage && this.errorMessage.length > 0) {
                    return;
                }

                var selectedGoalType = this.get("selectedGoalType");
                var selectedPredicateOperator = this.get("selectedPredicateOperator");
                result.goalType = selectedGoalType;
                result.goalNameResourceKey = that._get_goalNameResourceKey(selectedGoalType, selectedPredicateOperator);
                result.objectName = "";
                result.predicateOperator = "";
                result.objectNameWrapper = "";

                if (selectedGoalType === Telerik.Sitefinity.Web.UI.Enums.GoalType.NextPageView) {
                    result.predicateOperator = selectedPredicateOperator;
                    if (selectedPredicateOperator === isPredicateOperator) {

                        // Next page view with specific page is selected
                        var nextPage = that.get_goalNextPage().get_selectedPage();
                        if (!nextPage) {
                            this.set("errorMessage", that.get_clientLabelManager().getLabel("Labels", "GoalPageRequired"));
                            return;
                        }

                        result.objectId = nextPage.Id;
                        result.objectName = nextPage.Title.PersistedValue ? nextPage.Title.PersistedValue : nextPage.Title;
                        result.objectCulture = that.get_goalNextPage().get_pageSelector().get_uiCulture();
                    } else if (selectedPredicateOperator === containsPredicateOperator) {

                        // Next page view with Url contains is selected
                        var pageUrl = this.get("pageUrl");
                        if (!pageUrl || /^\s*$/.test(pageUrl)) {
                            this.set("errorMessage", that.get_clientLabelManager().getLabel("Labels", "GoalPageUrlRequired"));
                            return;
                        }

                        result.objectName = pageUrl;
                        result.objectNameWrapper = '"';
                    }
                } else if (selectedGoalType === Telerik.Sitefinity.Web.UI.Enums.GoalType.DecConversion) {
                    result.predicateOperator = selectedPredicateOperator;
                    var conversionId = this.get("selectedConversion");
                    if (conversionId) {
                        var selectedConversion = this.conversions.get(conversionId);
                        result.objectId = selectedConversion.Id;
                        result.objectName = selectedConversion.Name;
                    } else {
                        this.set("errorMessage", that.get_clientLabelManager().getLabel("Labels", "GoalConversionRequired"));
                        return;
                    }
                }

                that.close(result);
            },
            cancel: function () {
                that.close();
            }
        });

        var addGoalForm = jQuery(this.get_outerDiv());
        kendo.bind(addGoalForm, this._viewModel);

        this._viewModel.bind("change", function (e) {
            if (e.field === "selectedGoalType") {
                var selectedGoalType = that._viewModel.get("selectedGoalType");

                // Reseting is needed to track changes
                that._viewModel.set("selectedPredicateOperator", null);
                this.set("errorMessage", "");

                var newPredcateOperators = [];
                if (selectedGoalType === Telerik.Sitefinity.Web.UI.Enums.GoalType.NextPageView ||
                    (selectedGoalType === Telerik.Sitefinity.Web.UI.Enums.GoalType.DecConversion && that._viewModel.get("conversions").data().length > 0)) {
                    newPredcateOperators.push({ name: isPredicateOperatorName, value: isPredicateOperator });
                }

                if (selectedGoalType === Telerik.Sitefinity.Web.UI.Enums.GoalType.NextPageView) {
                    newPredcateOperators.push({ name: containsPredicateOperatorName, value: containsPredicateOperator });
                }

                that._viewModel.set("predcateOperators", newPredcateOperators);
                that._viewModel.set("selectedPredicateOperator", newPredcateOperators.length > 0 ? newPredcateOperators[0].value : "");
                that._viewModel.set("multiplePredicateOperators", newPredcateOperators.length > 1);
                that._viewModel.set("singlePredicateOperator", newPredcateOperators.length === 1);
            }

            if (e.field === "selectedPredicateOperator") {
                that._viewModel.set("isPageFieldVisible", false);
                that._viewModel.set("isPageUrlVisible", false);
                that._viewModel.set("isFormsSubmissionSelected", false);
                that._viewModel.set("isConversionDropdownVisible", false);
                that._viewModel.set("noConversionsAvailable", false);
                this.set("errorMessage", "");

                if (that._viewModel.get("selectedGoalType") === Telerik.Sitefinity.Web.UI.Enums.GoalType.NextPageView) {
                    if (that._viewModel.get("selectedPredicateOperator") === isPredicateOperator) {
                        that._viewModel.set("isPageFieldVisible", true);
                    } else {
                        that._viewModel.set("isPageUrlVisible", true);
                    }
                } else if (that._viewModel.get("selectedGoalType") === Telerik.Sitefinity.Web.UI.Enums.GoalType.FormSubmission) {
                    that._viewModel.set("isFormsSubmissionSelected", true);
                } else if (that._viewModel.get("selectedGoalType") === Telerik.Sitefinity.Web.UI.Enums.GoalType.DecConversion) {
                    if (that._viewModel.get("conversions").data().length > 0) {
                        that._viewModel.set("isConversionDropdownVisible", true);
                    }
                    else {
                        that._viewModel.set("noConversionsAvailable", true);
                    }
                }
            }
        });
    },

    get_conversions: function () {
        var that = this;
        if (!this._conversions) {
            this._conversions = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: that.get_decConversionService(),
                        dataType: "json"
                    }
                },
                change: function (e) {
                    var data = this.data();
                    if (data && data.length > 0) {
                        that._viewModel.set("selectedConversion", data[0].Id);
                    }
                },
                schema: {
                    model: { id: "Id" }
                }
            })
        }

        return this._conversions;
    },

    _get_goalNameResourceKey: function (selectedGoalType, selectedPredicateOperator) {
        if (selectedGoalType == Telerik.Sitefinity.Web.UI.Enums.GoalType.NextPageView) {
            if (selectedPredicateOperator == "contains") {
                return "NextPageViewedContainsGoalName";
            } else {
                return "NextPageViewedIsGoalName";
            }
        } else if (selectedGoalType == Telerik.Sitefinity.Web.UI.Enums.GoalType.FormSubmission) {
            return "FormSubmissionGoalName";
        } else if (selectedGoalType == Telerik.Sitefinity.Web.UI.Enums.GoalType.DecConversion) {
            return "DecConversionIsGoalName";
        }

        return "Goal undefined " + selectedPredicateOperator;
    },

    _onPageSelected: function (reset) {
        var pageId = this.get_goalNextPage().get_value();
        var emptyGuid = "00000000-0000-0000-0000-000000000000";
        if (pageId && pageId !== emptyGuid) {
            this._validateSelectedPage(pageId, function () { }, this._onPageError);
        }
    },

    _validateSelectedPage: function (pageId, onSuccess, onError) {
    },

    _onPageError: function (err, that) {
        if (err) {
            var errorMessage = null;
            if (err.responseJSON && err.responseJSON.ResponseStatus && err.responseJSON.ResponseStatus.Message) {
                errorMessage = err.responseJSON.ResponseStatus.Message;
            } else {
                errorMessage = err.responseText;
            }

            if (typeof err === 'string' && !errorMessage) {
                errorMessage = err;
            }
            if (!that) {
                that = this;
            }

            that._viewModel.set("errorMessage", errorMessage);
        }
    },

    get_decConversionService: function () {
        return this._decConversionService;
    },
    set_decConversionService: function (value) {
        this._decConversionService = value;
    },
    get_goalNextPage: function () {
        return this._goalNextPage;
    },
    set_goalNextPage: function (value) {
        this._goalNextPage = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
}

Telerik.Sitefinity.Web.UI.GoalSelector.registerClass('Telerik.Sitefinity.Web.UI.GoalSelector', Telerik.Sitefinity.Web.UI.Kendo.KendoWindow);

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Enums");
Telerik.Sitefinity.Web.UI.Enums.GoalType =
{
    NextPageView: 0,
    FormSubmission: 1,
    DecConversion: 2
}