﻿// ------------------ Define form rule classes - START ----------------------

var FormRulesSettings = (function () {
    // private members
    var ConditionEvaluators = [];
    var InputTypeParsers = [];
    var RuleValueParsers = [];
    var FieldSelectors = [];
    var ActionExecutors = [];

    function ConditionEvaluator(name, conditionEvaluator) {
        this.name = name;
        this.conditionEvaluator = conditionEvaluator;
    }

    ConditionEvaluator.prototype.canProcess = function (name) {
        return name === this.name;
    };

    ConditionEvaluator.prototype.process = function (currentValue, ruleValue, inputType) {
        if (!InputTypeParsers || InputTypeParsers.length === 0) return false;

        var inputTypeParser;
        for (var inputValueIndex = 0; inputValueIndex < InputTypeParsers.length; inputValueIndex++) {
            if (inputValueIndex === 0 || InputTypeParsers[inputValueIndex].canParse(inputType)) {
                inputTypeParser = InputTypeParsers[inputValueIndex];
            }
        }

        var ruleValueParsers;
        for (var ruleValueIndex = 0; ruleValueIndex < RuleValueParsers.length; ruleValueIndex++) {
            if (ruleValueIndex === 0 || RuleValueParsers[ruleValueIndex].canParse(inputType)) {
                ruleValueParsers = RuleValueParsers[ruleValueIndex];
            }
        }

        var parsedCurrentValue = inputTypeParser ? inputTypeParser.parse(currentValue) : currentValue;
        var parsedRuleValue = ruleValueParsers ? ruleValueParsers.parse(ruleValue) : ruleValue;

        return this.conditionEvaluator(parsedCurrentValue, parsedRuleValue);
    };

    function ValueParser(inputType, parser, escape, escapeRegEx) {
        this.inputType = inputType;
        this.parser = parser;
        this.escape = escape;
        this.escapeRegEx = escapeRegEx ? escapeRegEx : /[\-\[\]{}()*+?.,\\\^$|#\s]/g;
    }

    ValueParser.prototype.canParse = function (inputType) {
        return this.inputType === inputType;
    };

    ValueParser.prototype.parse = function (value) {
        var parsedValue = this.parser(value);
        if (this.escape === true && typeof parsedValue === "string")
            return parsedValue.replace(this.escapeRegEx, '\\$&');

        return parsedValue;
    };

    function FieldSelector(fieldContainerDataSfRole, elementSelector, additionalFilter) {
        this.fieldContainerDataSfRole = fieldContainerDataSfRole;
        this.elementSelector = elementSelector;
        this.additionalFilter = additionalFilter;
    }

    FieldSelector.prototype.getFieldContainerDataSfRole = function () {
        return this.fieldContainerDataSfRole;
    };

    FieldSelector.prototype.getFieldValues = function (fieldContainer) {
        if (this.additionalFilter)
            return $(fieldContainer).find(this.elementSelector).filter(this.additionalFilter).map(function (i, data) { return $(data).val().replace(/^\s+|\s+$/g, ""); }).get();
        else
            return $(fieldContainer).find(this.elementSelector).map(function (i, data) { return $(data).val().replace(/^\s+|\s+$/g, ""); }).get();
    };

    FieldSelector.prototype.getFieldValueElements = function (fieldContainer) {
        return $(fieldContainer).find(this.elementSelector);
    };

    FieldSelector.prototype.canGetValues = function (fieldContainer) {
        return this.fieldContainerDataSfRole === fieldContainer.attr("data-sf-role");
    };

    // public members
    return {
        addConditionEvaluator: function (name, conditionEvaluator) {
            ConditionEvaluators.push(new ConditionEvaluator(name, conditionEvaluator));
        },
        removeConditionEvaluator: function (name) {
            for (var i = 0; i < ConditionEvaluators.length; i++) {
                if (ConditionEvaluators[i].name === name) {
                    ConditionEvaluators.splice(i, 1);
                    break;
                }
            }
        },
        processConditionEvaluator: function (name, inputType, currentValue, ruleValue) {
            for (var i = 0; i < ConditionEvaluators.length; i++) {
                if (ConditionEvaluators[i].canProcess(name)) {
                    return ConditionEvaluators[i].process(currentValue, ruleValue, inputType);
                }
            }

            return false;
        },
        getConditionEvaluator: function (name) {
            for (var i = 0; i < ConditionEvaluators.length; i++) {
                if (ConditionEvaluators[i].canProcess(name)) {
                    return ConditionEvaluators[i];
                }
            }

            return null;
        },

        addActionExecutor: function (actionName, actionExecutor) {
            ActionExecutors.push({ actionName: actionName, actionExecutor: actionExecutor });
        },
        removeActionExecutor: function (actionName) {
            ActionExecutors = ActionExecutors.filter(function (a) { return a.actionName !== actionName; });
        },
        getActionExecutor: function (actionName) {
            var entry = ActionExecutors.filter(function (a) { return a.actionName === actionName; })[0];
            if (entry) {
                return entry.actionExecutor;
            }

            return null;
        },

        addInputTypeParser: function (inputType, parser, escape, escapeRegEx) {
            InputTypeParsers.push(new ValueParser(inputType, parser, escape, escapeRegEx));
        },
        removeInputTypeParser: function (inputType) {
            for (var i = 0; i < InputTypeParsers.length; i++) {
                if (InputTypeParsers[i].inputType === inputType) {
                    InputTypeParsers.splice(i, 1);
                    break;
                }
            }
        },
        addRuleValueParser: function (inputType, parser, escape, escapeRegEx) {
            RuleValueParsers.push(new ValueParser(inputType, parser, escape, escapeRegEx));
        },
        removeRuleValueParser: function (inputType) {
            for (var i = 0; i < RuleValueParsers.length; i++) {
                if (RuleValueParsers[i].inputType === inputType) {
                    RuleValueParsers.splice(i, 1);
                    break;
                }
            }
        },
        addFieldSelector: function (fieldContainerDataSfRole, elementSelector, additionalFilter) {
            var element = FieldSelectors.map(function (e) { return e.fieldContainerDataSfRole; }).indexOf(fieldContainerDataSfRole);
            if (element > -1)
                throw "Container with attribute [data-sf-role='" + fieldContainerDataSfRole + "'] have been registered already.";
            else
                FieldSelectors.push(new FieldSelector(fieldContainerDataSfRole, elementSelector, additionalFilter));
        },
        removeFieldSelector: function (fieldContainerDataSfRole) {
            for (var i = 0; i < FieldSelectors.length; i++) {
                if (FieldSelectors[i].fieldContainerDataSfRole === fieldContainerDataSfRole) {
                    FieldSelectors.splice(i, 1);
                    break;
                }
            }
        },
        getFieldValues: function (fieldContainer) {
            for (var i = 0; i < FieldSelectors.length; i++) {
                if (FieldSelectors[i].canGetValues(fieldContainer)) {
                    return FieldSelectors[i].getFieldValues(fieldContainer);
                }
            }

            return [];
        },
        getFieldValueElements: function (fieldContainer) {
            for (var i = 0; i < FieldSelectors.length; i++) {
                if (FieldSelectors[i].canGetValues(fieldContainer)) {
                    return FieldSelectors[i].getFieldValueElements(fieldContainer);
                }
            }

            return null;
        },
        getFieldsContainerNames: function () {
            var containers = [];
            for (var i = 0; i < FieldSelectors.length; i++) {
                containers.push(FieldSelectors[i].getFieldContainerDataSfRole());
            }

            return containers;
        }
    };
})();

var FormRuleConstants = {
    Actions: {
        Show: "Show",
        Hide: "Hide",
        Skip: "Skip",
        ShowMessage: "ShowMessage",
        GoTo: "GoTo",
        SendNotification: "SendNotification"
    }
};

function FormRuleActionExecutorBase() { }

FormRuleActionExecutorBase.prototype.applyState = function (context, actionData) {
    throw new Error('applyState() function not implemented');
};

FormRuleActionExecutorBase.prototype.updateState = function (context, actionData) {
    throw new Error('updateState() function not implemented');
};

FormRuleActionExecutorBase.prototype.undoUpdateState = function (context, actionData) {
    throw new Error('undoUpdateState() function not implemented');
};

FormRuleActionExecutorBase.prototype.isConflict = function (actionData, otherActionData) {
    return false;
};

FormRuleActionExecutorBase.prototype.getActionFieldIds = function (actionData) {
    return [];
};

function HideShowFieldFormRuleActionExecutor(actionName) {
    FormRuleActionExecutorBase.call(this);
    if (actionName === FormRuleConstants.Actions.Show || actionName === FormRuleConstants.Actions.Hide) {
        this.actionName = actionName;
    } else {
        throw new Error("Invalid action name! Only " + FormRuleConstants.Actions.Show + " and " + FormRuleConstants.Actions.Hide + " action names are allowed");
    }
}

HideShowFieldFormRuleActionExecutor.prototype = Object.create(FormRuleActionExecutorBase.prototype);
HideShowFieldFormRuleActionExecutor.prototype.constructor = HideShowFieldFormRuleActionExecutor;

HideShowFieldFormRuleActionExecutor.prototype.applyState = function (context, actionData) {
    var fieldIndex = context.helper.fieldIndexOf(context.fields, actionData.target);
    var fieldControlId = context.fields[fieldIndex].FieldControlId;
    if (context.fields[fieldIndex].Visible) {
        context.helper.showField(context, fieldControlId);
    } else {
        context.helper.hideField(context, fieldControlId);
    }
};

HideShowFieldFormRuleActionExecutor.prototype.updateState = function (context, actionData) {
    var updated = false;
    var fieldIndex = context.helper.fieldIndexOf(context.fields, actionData.target);
    if (this.actionName === FormRuleConstants.Actions.Show && !context.fields[fieldIndex].Visible) {
        context.fields[fieldIndex].Visible = true;
        updated = true;
    } else if (this.actionName === FormRuleConstants.Actions.Hide && context.fields[fieldIndex].Visible) {
        context.fields[fieldIndex].Visible = false;
        updated = true;
    }

    return updated;
};

HideShowFieldFormRuleActionExecutor.prototype.undoUpdateState = function (context, actionData) {
    var fieldIndex = context.helper.fieldIndexOf(context.fields, actionData.target);
    if (this.actionName === FormRuleConstants.Actions.Show) {
        context.fields[fieldIndex].Visible = false;
    } else {
        context.fields[fieldIndex].Visible = true;
    }
};

HideShowFieldFormRuleActionExecutor.prototype.isConflict = function (actionData, otherActionData) {
    return (otherActionData.name === FormRuleConstants.Actions.Show || otherActionData.name === FormRuleConstants.Actions.Hide) && actionData.target === otherActionData.target;
};

HideShowFieldFormRuleActionExecutor.prototype.getActionFieldIds = function (actionData) {
    return [actionData.target];
};

function SkipToPageFormRuleActionExecutor() {
    FormRuleActionExecutorBase.call(this);
}

SkipToPageFormRuleActionExecutor.prototype = Object.create(FormRuleActionExecutorBase.prototype);
SkipToPageFormRuleActionExecutor.prototype.constructor = SkipToPageFormRuleActionExecutor;

SkipToPageFormRuleActionExecutor.prototype.applyState = function (context, actionData) {
    if (context.skipToPageCollection) {
        context.formContainer.trigger("form-page-skip", [context.skipToPageCollection]);
    }
};

SkipToPageFormRuleActionExecutor.prototype.updateState = function (context, actionData) {
    if (!context.skipToPageCollection) {
        context.skipToPageCollection = [];
    }

    if (actionData.pageIndex < parseInt(actionData.target)) {
        context.skipToPageCollection.push({ SkipFromPage: actionData.pageIndex, SkipToPage: parseInt(actionData.target) });
        return true;
    }

    return false;
};

SkipToPageFormRuleActionExecutor.prototype.undoUpdateState = function (context, actionData) {
    if (context.skipToPageCollection) {
        context.skipToPageCollection = context.skipToPageCollection.filter(function (p) { return p.SkipFromPage !== actionData.pageIndex && p.SkipToPage !== actionData.target; });
    }
};

SkipToPageFormRuleActionExecutor.prototype.isConflict = function (actionData, otherActionData) {
    return actionData.name === otherActionData.name && actionData.pageIndex === otherActionData.pageIndex; // same action, same current page
};


function ShowMessageRuleActionExecutor() {
    FormRuleActionExecutorBase.call(this);
}

ShowMessageRuleActionExecutor.prototype = Object.create(FormRuleActionExecutorBase.prototype);
ShowMessageRuleActionExecutor.prototype.constructor = ShowMessageRuleActionExecutor;

ShowMessageRuleActionExecutor.prototype.applyState = function (context, actionData) {
    var inputSelector = '[data-sf-role="form-rules-message"]';
    var inputElement = $(context.formContainer).find(inputSelector);
    if (this.execute) {
        inputElement.val(actionData.target);
    } else {
        var currentValue = inputElement.val();
        if (currentValue === actionData.target) {
            inputElement.val("");
        }
    }
};

ShowMessageRuleActionExecutor.prototype.updateState = function (context, actionData) {
    this.execute = true;
    return true;
};

ShowMessageRuleActionExecutor.prototype.undoUpdateState = function (context, actionData) {
    this.execute = false;
};

ShowMessageRuleActionExecutor.prototype.isConflict = function (actionData, otherActionData) {
    return otherActionData.name === FormRuleConstants.Actions.ShowMessage || otherActionData.name === FormRuleConstants.Actions.GoTo;
};


function GoToPageRuleActionExecutor() {
    FormRuleActionExecutorBase.call(this);
}

GoToPageRuleActionExecutor.prototype = Object.create(FormRuleActionExecutorBase.prototype);
GoToPageRuleActionExecutor.prototype.constructor = GoToPageRuleActionExecutor;

GoToPageRuleActionExecutor.prototype.applyState = function (context, actionData) {
    var inputSelector = '[data-sf-role="form-rules-redirect-page"]';
    var inputElement = $(context.formContainer).find(inputSelector);
    if (this.execute) {
        inputElement.val(actionData.target);
    } else {
        var currentValue = inputElement.val();
        if (currentValue === actionData.target) {
            inputElement.val("");
        }
    }
};

GoToPageRuleActionExecutor.prototype.updateState = function (context, actionData) {
    this.execute = true;
    return true;
};

GoToPageRuleActionExecutor.prototype.undoUpdateState = function (context, actionData) {
    this.execute = false;
};

GoToPageRuleActionExecutor.prototype.isConflict = function (actionData, otherActionData) {
    return otherActionData.name === FormRuleConstants.Actions.ShowMessage || otherActionData.name === FormRuleConstants.Actions.GoTo;
};


function SendNotificationRuleActionExecutor() {
    FormRuleActionExecutorBase.call(this);
}

SendNotificationRuleActionExecutor.prototype = Object.create(FormRuleActionExecutorBase.prototype);
SendNotificationRuleActionExecutor.prototype.constructor = SendNotificationRuleActionExecutor;

SendNotificationRuleActionExecutor.prototype.applyState = function (context, actionData) {
    var inputSelector = '[data-sf-role="form-rules-notification-emails"]';
    var inputElement = $(context.formContainer).find(inputSelector);
    if (context.notificationEmails) {
        inputElement.val(context.notificationEmails.join(","));
    } else {
        inputElement.val("");
    }
};

SendNotificationRuleActionExecutor.prototype.updateState = function (context, actionData) {
    if (!context.notificationEmails) {
        context.notificationEmails = [];
    }

    if (context.notificationEmails.indexOf(actionData.target) === -1) {
        context.notificationEmails.push(actionData.target);
    }

    return true;
};

SendNotificationRuleActionExecutor.prototype.undoUpdateState = function (context, actionData) {
    if (context.notificationEmails) {
        var index = context.notificationEmails.indexOf(actionData.target);
        if (index !== -1) {
            context.notificationEmails.splice(index, 0);
        }
    }
};

SendNotificationRuleActionExecutor.prototype.isConflict = function (actionData, otherActionData) {
    return actionData.name === otherActionData.name && actionData.target === otherActionData.target;
};


// ------------------ Define form rule classes - END ----------------------

// ------------------ Add rule objects to FormRuleSettings - START ----------------------

(function addConditionEvaluators() {
    FormRulesSettings.addConditionEvaluator('Equal', function (currentValue, ruleValue) {
        if (typeof currentValue === "string") {
            return currentValue.search(new RegExp("^" + ruleValue + "$", "i")) === 0;
        }

        return currentValue === ruleValue;
    });
    FormRulesSettings.addConditionEvaluator('NotEqual', function (currentValue, ruleValue) {
        if (typeof currentValue === "string") {
            return currentValue.search(new RegExp("^" + ruleValue + "$", "i")) === -1;
        }

        return currentValue !== ruleValue;
    });
    FormRulesSettings.addConditionEvaluator('Contains', function (currentValue, ruleValue) { return currentValue.search(new RegExp(ruleValue, "i")) > -1; });
    FormRulesSettings.addConditionEvaluator('NotContains', function (currentValue, ruleValue) { return currentValue.search(new RegExp(ruleValue, "i")) === -1; });

    var isFilledFunction = function (currentValue) {
        // Check if currentValue is NaN
        if (typeof currentValue === "number" && currentValue !== currentValue) {
            return false;
        }

        return currentValue && currentValue.toString().length > 0;
    };

    FormRulesSettings.addConditionEvaluator('IsFilled', isFilledFunction);
    FormRulesSettings.addConditionEvaluator('IsNotFilled', function (currentValue) { return !isFilledFunction(currentValue); });
    FormRulesSettings.addConditionEvaluator('FileSelected', function (currentValue) { return currentValue && currentValue.length > 0; });
    FormRulesSettings.addConditionEvaluator('FileNotSelected', function (currentValue) { return !currentValue || currentValue.length === 0; });
    FormRulesSettings.addConditionEvaluator('IsGreaterThan', function (currentValue, ruleValue) { return currentValue > ruleValue; });
    FormRulesSettings.addConditionEvaluator('IsLessThan', function (currentValue, ruleValue) { return currentValue < ruleValue; });
})();

(function addValueParsers() {
    var dateParserFunction = function (value) {
        var dateTime = new Date(value);
        var date = new Date(dateTime.getFullYear(), dateTime.getMonth(), dateTime.getDate());

        return date.getTime();
    };

    var monthParserFunction = function (value, timezoneOffset) {
        var dateTime = new Date(value);

        if (timezoneOffset) {
            dateTime.setTime(dateTime.getTime() + timezoneOffset);
        }

        var date = new Date(dateTime.getFullYear(), dateTime.getMonth());

        return date.getTime();
    };

    var weekParserFunction = function (value) {
        var date = new Date(parseInt(value.split(['-W'])[0]), 0, 1 + (parseInt(value.split(['-W'])[1]) - 1) * 7);
        date.setDate(date.getDate() - date.getDay() + (date.getDate() <= 4 ? 1 : 8));
        return date.getTime();
    };

    var dateTimeParserFunction = function (value, timezoneOffset) {
        var dateTime = new Date(value);
        var date = new Date(dateTime.getFullYear(), dateTime.getMonth(), dateTime.getDate(), dateTime.getHours(), dateTime.getMinutes());

        if (timezoneOffset) {
            date = new Date(date.getTime() + timezoneOffset);
        }

        return date.getTime();
    };

    var timeParserFunction = function (value, timezoneOffset) {
        var dateTime = new Date(value);

        if (timezoneOffset) {
            dateTime = new Date(dateTime.getTime() + timezoneOffset);
        }

        return dateTime.getHours() * 60 + dateTime.getMinutes();
    };

    FormRulesSettings.addInputTypeParser("text", function (value) { return value; });
    FormRulesSettings.addInputTypeParser("month", function (value) { return monthParserFunction(value, new Date(value).getTimezoneOffset() * 60000); });
    FormRulesSettings.addInputTypeParser("number", function (value) { return parseFloat(value) && Number(value); });
    FormRulesSettings.addInputTypeParser("datetime-local", dateTimeParserFunction);
    FormRulesSettings.addInputTypeParser("date", dateParserFunction);
    FormRulesSettings.addInputTypeParser("time", function (value) { return parseInt(value.split([':'])[0]) * 60 + parseInt(value.split([':'])[1]); });
    FormRulesSettings.addInputTypeParser("week", weekParserFunction);

    FormRulesSettings.addRuleValueParser("text", function (value) { return value; }, true);
    FormRulesSettings.addRuleValueParser("month", function (value) { return monthParserFunction(value, new Date(value).getTimezoneOffset() * 60000); });
    FormRulesSettings.addRuleValueParser("number", function (value) { return parseFloat(value) && Number(value); });
    FormRulesSettings.addRuleValueParser("datetime-local", function (value) { return dateTimeParserFunction(value, new Date(value).getTimezoneOffset() * 60000); });
    FormRulesSettings.addRuleValueParser("date", dateParserFunction);
    FormRulesSettings.addRuleValueParser("time", function (value) { return timeParserFunction(value, new Date(value).getTimezoneOffset() * 60000); });
    FormRulesSettings.addRuleValueParser("week", weekParserFunction);
})();

(function addFieldSelectors() {
    FormRulesSettings.addFieldSelector("text-field-container", "[data-sf-role='text-field-input']");
    FormRulesSettings.addFieldSelector("email-text-field-container", "[data-sf-role='email-text-field-input']");
    FormRulesSettings.addFieldSelector("multiple-choice-field-container", "[data-sf-role='multiple-choice-field-input']", ":checked");
    FormRulesSettings.addFieldSelector("checkboxes-field-container", "[data-sf-role='checkboxes-field-input']", ":checked");
    FormRulesSettings.addFieldSelector("paragraph-text-field-container", "[data-sf-role='paragraph-text-field-textarea']");
    FormRulesSettings.addFieldSelector("dropdown-list-field-container", "[data-sf-role='dropdown-list-field-select']");
    FormRulesSettings.addFieldSelector("file-field-container", "[data-sf-role='single-file-input'] input[type='file']");
})();

(function addActionExecutors() {
    var hideRuleAction = new HideShowFieldFormRuleActionExecutor(FormRuleConstants.Actions.Hide);
    FormRulesSettings.addActionExecutor(FormRuleConstants.Actions.Hide, hideRuleAction);

    var showRuleAction = new HideShowFieldFormRuleActionExecutor(FormRuleConstants.Actions.Show);
    FormRulesSettings.addActionExecutor(FormRuleConstants.Actions.Show, showRuleAction);

    var skipRuleActionExecutor = new SkipToPageFormRuleActionExecutor();
    FormRulesSettings.addActionExecutor(FormRuleConstants.Actions.Skip, skipRuleActionExecutor);

    var showMessageRuleActionExecutor = new ShowMessageRuleActionExecutor();
    FormRulesSettings.addActionExecutor(FormRuleConstants.Actions.ShowMessage, showMessageRuleActionExecutor);

    var goToPageRuleActionExecutor = new GoToPageRuleActionExecutor();
    FormRulesSettings.addActionExecutor(FormRuleConstants.Actions.GoTo, goToPageRuleActionExecutor);

    var sendNotificationRuleActionExecutor = new SendNotificationRuleActionExecutor();
    FormRulesSettings.addActionExecutor(FormRuleConstants.Actions.SendNotification, sendNotificationRuleActionExecutor);
})();

// ------------------ Add rule objects to FormRuleSettings - END ----------------------

// ------------------ Form rules execution - START ----------------------
(function ($) {
    'use strict';

    var FormRulesExecutor = function (container) {
        this._init(container);
    };

    FormRulesExecutor.prototype = {

        _init: function (target) {
            this.fieldContainerSelector = '[data-sf-role$="field-container"]';
            this.separatorSelector = '[data-sf-role="separator"]';
            this.formContainerSelector = getFormContainerSelector();
            this.skipFieldsSelector = 'input[type="hidden"][data-sf-role="form-rules-skip-fields"]';
            this.hiddenFieldsSelector = 'input[type="hidden"][data-sf-role="form-rules-hidden-fields"]';
            this.formContainer = $(target).closest(this.formContainerSelector);

            var separator = $(target).closest(this.separatorSelector);
            this.fieldsContainer = separator.length > 0 ? separator : this.formContainer;
            this.pages = $(this.formContainer).find(this.separatorSelector);

            this._initializeFormRules();

            this.iterationsMaxCount = 50;

            var that = this;
            this.formContainer.on("form-page-changed", function (e, nextIndex, previousIndex) {
                that._updateSkipPages(previousIndex, nextIndex);
            });
        },

        process: function () {
            if (!this._hasRules()) return;

            this.hiddenFields = this._getHiddenFields();
            this.skipFields = this._getSkipFields();

            var context = this._contextInitialization();
            var updatedContext = this._evaluateFormRules(context);

            this._applyActionsState(updatedContext);

            this.hiddenFields = updatedContext.hiddenFields;
            this._setHiddenFields(this.hiddenFields);
            this._setExecutedActions(updatedContext.activeActions);
        },

        _hasRules: function () {
            return this.formRules && this.formRules.length !== 0;
        },

        _evaluateFormRules: function (context) {
            var currentFieldsVisibility = $.map(context.fields, function (field) { return field.Visible; });
            var updatedContext = this._updateContext(context);
            var updatedFieldsVisibility = $.map(updatedContext.fields, function (field) { return field.Visible; });

            var noChanges = this._compareArrays(currentFieldsVisibility, updatedFieldsVisibility);
            if (context.iterationsCounter > this.iterationsMaxCount || noChanges) {
                return updatedContext;
            }

            context.iterationsCounter++;

            return this._evaluateFormRules(updatedContext);
        },

        _updateContext: function (context) {
            for (var ruleIndex = 0; ruleIndex < this.formRules.length; ruleIndex++) {
                var rule = this.formRules[ruleIndex];
                var applyRule = this._evaluateConditions(context, rule);
                if (applyRule) {
                    // Apply actions
                    for (var applyActionIndex = 0; applyActionIndex < rule.actions.length; applyActionIndex++) {
                        var action = rule.actions[applyActionIndex];
                        var conflictingActiveActions = context.activeActions.filter(function (a) { return action.executor.isConflict(action.data, a.data); });
                        for (var conflictingActionIndex = 0; conflictingActionIndex < conflictingActiveActions.length; conflictingActionIndex++) {
                            var conflictingAction = conflictingActiveActions[conflictingActionIndex];
                            conflictingAction.executor.undoUpdateState(context, conflictingAction.data);
                            context.activeActions = context.activeActions.filter(function (a) { return a !== conflictingAction; });
                        }

                        var updated = action.executor.updateState(context, action.data);
                        if (updated) {
                            context.activeActions.push(action);
                        }
                    }
                } else {
                    // Undo actions if previously applied
                    for (var undoActionIndex = 0; undoActionIndex < rule.actions.length; undoActionIndex++) {
                        var undoAction = rule.actions[undoActionIndex];
                        var activeActionIndex = context.helper.actionItemIndexOf(context.activeActions, undoAction.data);
                        if (activeActionIndex !== -1) {
                            undoAction.executor.undoUpdateState(context, undoAction.data);
                            context.activeActions.splice(activeActionIndex, 1);
                        }
                    }
                }
            }

            return context;
        },

        _applyActionsState: function (context) {
            for (var actionIndex = 0; actionIndex < context.activeActions.length; actionIndex++) {
                var action = context.activeActions[actionIndex];
                action.executor.applyState(context, action.data);
            }

            var deactivatedActions = context.executedActions.filter(function (a) { return context.activeActions.indexOf(a) === -1; });
            for (var deactivatedActionIndex = 0; deactivatedActionIndex < deactivatedActions.length; deactivatedActionIndex++) {
                var deactivatedAction = deactivatedActions[deactivatedActionIndex];
                deactivatedAction.executor.applyState(context, deactivatedAction.data);
            }
        },

        _evaluateConditions: function (context, rule) {
            var executeStatus = [];
            for (var conditionIndex = 0; conditionIndex < rule.conditions.length; conditionIndex++) {
                var condition = rule.conditions[conditionIndex];
                var field = this._getContextField(context, condition.data.id);
                var fieldType = this._getFieldType(condition.data.id);

                var applyRule = false;
                if (field.Visible === true && condition.evaluator) {
                    if (field.Values && field.Values.length > 0) {
                        for (var h = 0; h < field.Values.length; h++) {
                            applyRule = condition.evaluator.process(field.Values[h], condition.data.value, fieldType);
                            if (applyRule) break;
                        }
                    } else {
                        applyRule = condition.evaluator.process(null, condition.data.value, fieldType);
                    }
                }

                executeStatus.push(applyRule);
            }

            var execute;
            if (rule.operator === "And") {
                execute = this._every(executeStatus, function (e) { return e; });
            } else {
                // Operator "Or"
                execute = this._some(executeStatus, function (e) { return e; });
            }

            return execute;
        },

        _initializeFormRules: function () {
            this.formRules = [];
            var formRulesElement = this.formContainer.find('input[data-sf-role="form-rules"]');
            var deserializedFormRules = formRulesElement.length > 0 && formRulesElement.val().length > 0 ? JSON.parse(formRulesElement.val()) : null;
            if (deserializedFormRules) {
                for (var ruleIndex = 0; ruleIndex < deserializedFormRules.length; ruleIndex++) {
                    var rule = deserializedFormRules[ruleIndex];
                    var newRule = {
                        operator: rule.Operator,
                        conditions: [],
                        actions: []
                    };

                    var rulePageIndex = 0;
                    for (var conditionIndex = 0; conditionIndex < rule.Conditions.length; conditionIndex++) {
                        var condition = rule.Conditions[conditionIndex];
                        var conditionEvaluator = FormRulesSettings.getConditionEvaluator(condition.Operator);
                        newRule.conditions.push({
                            data: {
                                id: condition.Id,
                                value: condition.Value
                            },
                            evaluator: conditionEvaluator
                        });

                        var conditionTargetPageIndex = this._getFieldPageContainerIndex(condition.Id);
                        rulePageIndex = conditionTargetPageIndex && conditionTargetPageIndex > rulePageIndex ? conditionTargetPageIndex : rulePageIndex;
                    }

                    for (var actionIndex = 0; actionIndex < rule.Actions.length; actionIndex++) {
                        var action = rule.Actions[actionIndex];
                        var actionExecutor = FormRulesSettings.getActionExecutor(action.Action);
                        if (actionExecutor) {
                            newRule.actions.push({
                                data: {
                                    target: action.Target,
                                    name: action.Action,
                                    pageIndex: rulePageIndex
                                },
                                executor: actionExecutor
                            });
                        }
                    }

                    newRule.actions = this._filterConflictingRuleActions(newRule.actions);

                    this.formRules.push(newRule);
                }
            }
        },

        _filterConflictingRuleActions: function (ruleActions) {
            var filteredActions = [];

            // iterate backward and get the first rule action of the action list, skip others
            for (var i = ruleActions.length - 1; i >= 0; i--) {
                if (filteredActions.filter(function (a) { return ruleActions[i].executor.isConflict(ruleActions[i].data, a.data); }).length === 0) {
                    filteredActions.push(ruleActions[i]);
                }
            }

            return filteredActions.reverse();
        },

        _contextInitialization: function () {
            var executedActions = this._getExecutedActions();
            return {
                fields: this._fieldsInitialization(),
                executedActions: executedActions.slice(),
                activeActions: executedActions.slice(),
                formContainer: this.formContainer,
                formContainerSelector: this.formContainerSelector,
                iterationsCounter: 0,
                hiddenFields: this.hiddenFields,
                skipToPageCollection: [],
                helper: {
                    showField: this._showField.bind(this),
                    hideField: this._hideField.bind(this),
                    getFieldElement: this._getFieldElement.bind(this),
                    getFieldStartSelector: this._getFieldStartSelector.bind(this),
                    getFieldEndSelector: this._getFieldEndSelector.bind(this),
                    fieldIndexOf: this._fieldIndexOf.bind(this),
                    arrayIndexOf: this._arrayIndexOf.bind(this),
                    actionItemIndexOf: this._actionItemIndexOf.bind(this)
                }
            };
        },

        _fieldsInitialization: function () {
            var fields = [];
            var formRuleFields = this._getFormRulesFields();
            for (var i = 0; i < formRuleFields.length; i++) {
                fields.push({
                    FieldControlId: formRuleFields[i],
                    Values: this._getFieldValues(formRuleFields[i]),
                    Visible: this._arrayIndexOf(this.hiddenFields, formRuleFields[i]) === -1 && this._arrayIndexOf(this.skipFields, formRuleFields[i]) === -1
                });
            }

            return fields;
        },

        _getFormRulesFields: function () {
            var fields = [];

            for (var i = 0; i < this.formRules.length; i++) {
                for (var s = 0; s < this.formRules[i].conditions.length; s++) {
                    var conditionFieldName = this.formRules[i].conditions[s].data.id;
                    if (this._arrayIndexOf(fields, conditionFieldName) === -1) {
                        fields.push(conditionFieldName);
                    }
                }

                for (var j = 0; j < this.formRules[i].actions.length; j++) {
                    var actionFieldIds = this.formRules[i].actions[j].executor.getActionFieldIds(this.formRules[i].actions[j].data);
                    if (actionFieldIds && actionFieldIds.length > 0){
                        for (var k = 0; k < actionFieldIds.length; k++) {
                            if (this._arrayIndexOf(fields, actionFieldIds[k]) === -1) {
                                fields.push(actionFieldIds[k]);
                            }
                        }
                    }
                }
            }

            return fields;
        },

        _updateSkipPages: function (previousIndex, nextIndex) {
            if (previousIndex < nextIndex) {
                // next page - disable fields in skipped pages
                var fieldContainerNames = FormRulesSettings.getFieldsContainerNames();
                for (var skipPage = previousIndex + 1; skipPage < nextIndex; skipPage++) {
                    for (var k = 0; k < fieldContainerNames.length; k++) {
                        var fieldsContainers = this.pages.eq(skipPage).find('[data-sf-role="' + fieldContainerNames[k] + '"]');
                        for (var j = 0; j < fieldsContainers.length; j++) {
                            var skippedField = FormRulesSettings.getFieldValueElements($(fieldsContainers[j]));
                            if (skippedField.length) {
                                var fieldName = skippedField.first().attr("name");
                                var fieldStartWrapper = $(this.formContainer).find("script[data-sf-role-field-name='" + fieldName + "']")[0];
                                if (fieldStartWrapper) {
                                    var dataSfRole = $(fieldStartWrapper).attr("data-sf-role");
                                    if (dataSfRole) {
                                        var fieldControlId = dataSfRole.replace("start_field_", "");
                                        this._skipField(fieldControlId);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else {
                // previous page - iterate through skipped fields and enable them again
                for (var fieldIndex = this.skipFields.length - 1; fieldIndex >= 0; fieldIndex--) {
                    var fieldPageIndex = this._getFieldPageContainerIndex(this.skipFields[fieldIndex]);
                    if (nextIndex < fieldPageIndex && fieldPageIndex < previousIndex) {
                        this._unskipField(this.skipFields[fieldIndex]);
                    }
                }
            }

            this._setSkipFields(this.skipFields);
        },

        _skipField: function (fieldControlId) {
            var index = this._arrayIndexOf(this.skipFields, fieldControlId);
            if (index === -1) {
                this.skipFields.push(fieldControlId);

                var fieldElement = this._getFieldElement(fieldControlId);
                fieldElement.attr('disabled', 'disabled');
            }
        },

        _unskipField: function (fieldControlId) {
            var index = this._arrayIndexOf(this.skipFields, fieldControlId);
            if (index > -1) {
                this.skipFields.splice(index, 1);

                var fieldElement = this._getFieldElement(fieldControlId);
                fieldElement.removeAttr('disabled');
            }
        },

        _actionItemIndexOf: function (actions, actionData) {
            for (var i = 0; i < actions.length; i++) {
                if (actions[i].data.target === actionData.target && actions[i].data.name === actionData.name && actions[i].data.pageIndex === actionData.pageIndex) {
                    return i;
                }
            }

            return -1;
        },

        _getContextField: function (context, fieldControlId) {
            for (var i = 0; i < context.fields.length; i++) {
                if (context.fields[i].FieldControlId === fieldControlId) return context.fields[i];
            }
            return null;
        },

        _getFieldElement: function (fieldControlId) {
            var scriptWrapper = $(this.formContainer).find(this._getFieldStartSelector(fieldControlId));
            if (scriptWrapper && scriptWrapper.length > 0) {
                var fieldAllElements = $(scriptWrapper).nextUntil(this._getFieldEndSelector(fieldControlId));

                // Get field element based on container selector registered in FormRulesSettings
                var fieldContainerNames = FormRulesSettings.getFieldsContainerNames();
                if (fieldContainerNames && fieldContainerNames.length > 0) {
                    for (var i = 0; i < fieldContainerNames.length; i++) {
                        var containerSelector = '[data-sf-role="' + fieldContainerNames[i] + '"]';
                        var fieldContainer = fieldAllElements.filter(containerSelector);

                        // If not found in root elements, try searching in descendants
                        if (!fieldContainer || fieldContainer.length === 0) {
                            fieldContainer = fieldAllElements.find(containerSelector);
                        }

                        if (fieldContainer && fieldContainer.length > 0) {
                            return FormRulesSettings.getFieldValueElements(fieldContainer);
                        }
                    }
                }
            }

            return $();
        },

        _showField: function (context, fieldControlId) {
            var index = this._arrayIndexOf(context.hiddenFields, fieldControlId);
            if (index > -1) {
                context.hiddenFields.splice(index, 1);

                var scriptWrapper = $(context.formContainer).find(this._getFieldStartSelector(fieldControlId));
                if (scriptWrapper && scriptWrapper.length > 0) {
                    var fieldElement = this._getFieldElement(fieldControlId);
                    if (fieldElement && fieldElement.length > 0) {
                        fieldElement.removeAttr('disabled');
                    }

                    $(scriptWrapper).nextUntil(this._getFieldEndSelector(fieldControlId)).each(function (i, element) {
                        $(element).show();
                    });
                }
            }
        },

        _hideField: function (context, fieldControlId) {
            var index = this._arrayIndexOf(context.hiddenFields, fieldControlId);
            if (index === -1) {
                context.hiddenFields.push(fieldControlId);

                var scriptWrapper = $(context.formContainer).find(this._getFieldStartSelector(fieldControlId));
                if (scriptWrapper && scriptWrapper.length > 0) {
                    var fieldElement = this._getFieldElement(fieldControlId);
                    if (fieldElement && fieldElement.length > 0) {
                        fieldElement.attr('disabled', 'disabled');
                    }

                    $(scriptWrapper).nextUntil(this._getFieldEndSelector(fieldControlId)).each(function (i, element) {
                        $(element).hide();
                    });
                }
            }
        },

        _getFieldType: function (fieldControlId) {
            var fieldElement = this._getFieldElement(fieldControlId);
            var fieldType = fieldElement.data("sf-input-type");
            if (!fieldType) {
                fieldType = this._getFieldElement(fieldControlId).attr("type");
            }

            return fieldType;
        },

        _getFieldValues: function (fieldControlId) {
            var fieldContainer = this._getFieldContainer(fieldControlId);
            return FormRulesSettings.getFieldValues(fieldContainer);
        },

        _getFieldContainer: function (fieldControlId) {
            return this._getFieldElement(fieldControlId).closest(this.fieldContainerSelector);
        },

        _getFieldPageContainer: function (fieldControlId) {
            var element = this._getFieldElement(fieldControlId);
            var separator = element.closest(this.separatorSelector);
            return separator.length > 0 ? separator : element.closest(this.formContainerSelector);
        },

        _getFieldPageContainerIndex: function (fieldControlId) {
            var fieldPageContainer = this._getFieldPageContainer(fieldControlId);
            return this.pages.index(fieldPageContainer);
        },

        _fieldIndexOf: function (fields, fieldControlId) {
            for (var i = 0; i < fields.length; i++) {
                if (fields[i].FieldControlId === fieldControlId) return i;
            }

            return -1;
        },

        _getExecutedActions: function () {
            return this.formContainer.data("sfActions") || [];
        },

        _setExecutedActions: function (actions) {
            this.formContainer.data("sfActions", actions);
        },

        _getHiddenFields: function () {
            return this._createArrayFromCsvValue($(this.formContainer).find(this.hiddenFieldsSelector).val());
        },

        _setHiddenFields: function (fields) {
            $(this.formContainer).find(this.hiddenFieldsSelector).val(fields.join(','));
        },

        _getSkipFields: function () {
            return this._createArrayFromCsvValue($(this.formContainer).find(this.skipFieldsSelector).val());
        },

        _setSkipFields: function (fields) {
            $(this.formContainer).find(this.skipFieldsSelector).val(fields.join(','));
        },

        _createArrayFromCsvValue: function (value) {
            var array = (value || '').split(","), newArray = [];
            for (var i = 0; i < array.length; i++) {
                if (array[i] && array[i] !== '') newArray.push(array[i]);
            }

            return newArray;
        },

        _arrayIndexOf: function (array, value) {
            for (var i = 0; i < array.length; i++) {
                if (array[i] === value) return i;
            }

            return -1;
        },

        _compareArrays: function (array1, array2) {
            var i = array1.length;
            if (i !== array2.length) return false;
            while (i--) {
                if (array1[i] !== array2[i]) return false;
            }
            return true;
        },

        _some: function (array, func) {
            for (var i = 0; i < array.length; i++) {
                if (func(array[i])) return true;
            }
            return false;
        },

        _every: function (array, func) {
            for (var i = 0; i < array.length; i++) {
                if (!func(array[i])) return false;
            }
            return true;
        },

        _getFieldStartSelector: function (fieldControlId) {
            return 'script[data-sf-role="start_field_' + fieldControlId + '"]';
        },

        _getFieldEndSelector: function (fieldControlId) {
            return 'script[data-sf-role="end_field_' + fieldControlId + '"]';
        }
    };

    var formRuleExecutorsCache = [];

    $.fn.extend({
        processFormRules: function () {
            var formContainerSelector = getFormContainerSelector();
            var formContainer = $(this).closest(formContainerSelector);
            if (formContainer && formContainer.length > 0) {
                var formRulesExecutor = null;
                var cachedFormRulesExecutorObject = formRuleExecutorsCache.filter(function (e) { return e.formContainer === formContainer[0]; })[0];
                if (cachedFormRulesExecutorObject) {
                    formRulesExecutor = cachedFormRulesExecutorObject.formRulesExecutor;
                } else {
                    formRulesExecutor = new FormRulesExecutor($(this));
                    formRuleExecutorsCache.push({
                        formContainer: formContainer[0],
                        formRulesExecutor: formRulesExecutor
                    });
                }

                formRulesExecutor.process();
            }
        }
    });

    $(document).ready(function () {
        // Use setTimeout to execute this function after all other document.ready functions
        setTimeout(function () {
            var formContainerSelector = getFormContainerSelector();
            var formContainers = $(formContainerSelector);
            formContainers.each(function (i, element) {
                if (typeof $.fn.processFormRules === 'function') {
                    $(element).processFormRules();
                }
            });
        });
    });

    function getFormContainerSelector() {
        var selector = '[data-sf-role="form-container"]';
        if ($(selector).length > 0) {
            return selector;
        }

        return "#PublicWrapper";
    }
}(jQuery));

// ------------------Form rules execution - END ----------------------