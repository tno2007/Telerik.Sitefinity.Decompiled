﻿///<reference path="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.4.js" />
$(document).ready(function () {
    var _settings = {};
    var _crontabExpression;
    var _defaultExpressionString = "0 0 * * * *"; // every day and year at 12:00 AM
    var _isExpressionSet = false;
    var _summary;
    var _schedulerView;
    var _datePickerMaxYear = now().getFullYear() + 50;    

    var selectors = {
        scheduleButton: "#scheduleButton",
        cancelConfiguringScheduleButton: "#cancelConfiguringScheduleButton",
        configureSchedulingButton: "#configureSchedulingButton",
        scheduleContainer: "#scheduleContainer",
        scheduleTimeChoices: "#scheduleTimeChoices",
        selectecScheduleTimeChoice: "#scheduleTimeChoices option:selected",
        scheduleTimePicker: "#scheduleTimePicker",
        scheduleDateTimePicker: "#scheduleDateTimePicker",
        scheduleDatePicker: "#scheduleDatePicker",
        customDatePicker: "#customDatePicker",
        dateTimePickerContainer: "#dateTimePickerContainer",
        scheduleButtonsContainer: "#scheduleButtonsContainer",
        changeScheduleButton: "#changeScheduleButton",
        cancelScheduleButton: "#cancelScheduleButton",
        orCancelScheduleButton: "#orCancelScheduleButton",
        errorMessage: "#errorMessage",
        errorMessageContainer: "#errorMessageContainer",
        jqCronContainer: "#jqCronContainer"
    };

    function now() {
        if (_settings && _settings.now) {
            return new Date(_settings.now.getTime());
        }

        return new Date();
    }

    var methods = {
        // this function initializes the plugin in
        init: function (settings) {
            _schedulerView = this;
            _settings = settings;

            if (settings.defaultExpression) {
                _defaultExpressionString = settings.defaultExpression;
            }            
            _crontabExpression = methods.getExpressionObjectOrDefault(settings.crontabExpression);

            // wire up the Kendo DatePicker
            var datepicker = $(selectors.scheduleDatePicker).kendoDatePicker({
                                value: now(),
                                min: now(),
                                max: new Date(_datePickerMaxYear, 11, 31, 0, 0, 0, 0),
                                animation: false,
                                change: methods.scheduleDatePickerChangeHandler
                            });
            datepicker.data("kendoDatePicker").readonly(false);

            // wire up the Kendo TimePicker
            var timepicker = $(selectors.scheduleTimePicker).kendoTimePicker({
                                format: "hh:mm tt",
                                max: new Date(_datePickerMaxYear, 0, 0, 23, 30, 0, 0),
                                animation: false
            });

            timepicker.data("kendoTimePicker").readonly(false);

            $(selectors.scheduleTimeChoices).change(methods.scheduleTimeChoicesChange);
            $(selectors.configureSchedulingButton).click(methods.configureSchedulingButtonClickHandler);
            $(selectors.cancelConfiguringScheduleButton).click(methods.cancelConfiguringScheduleButtonClickHandler);
            $(selectors.changeScheduleButton).click(methods.changeScheduleButtonClickHandler);
            $(selectors.cancelScheduleButton).click(function () {
                methods.cancelScheduleButtonClickHandler();
            });
            $(selectors.scheduleButton).click(function () {
                methods.scheduleButtonClickHandler();
            });

            $('#cron').cron({
                initial: "0 0 * * *",
                onChange: function () {                    
                    $(selectors.customDatePicker).val($(this).cron("value") + " *");
                },
                useGentleSelect: false // default: false
            });

            // next minute
            //var todayDate = now();
            //var initialCronValue = [
            //    todayDate.getMinutes() + 1,
            //    todayDate.getHours(),
            //    todayDate.getDate(),
            //    todayDate.getMonth(),
            //    todayDate.getDay(), //dow
            //    todayDate.getFullYear()].join(" ");

            var taskId = methods.getParameterByName("taskId");            
            if (taskId) {
                $(selectors.scheduleContainer).show();
                methods.prepareUIForExpression.apply(this, [settings.crontabExpression]);
                $(selectors.scheduleButton).hide();
                methods.prepareAfterScheduleButtons();                                
            }
        },

        getParameterByName: function (name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        },

        getCrontabExpressionString: function () {
            return _crontabExpression.minutesPattern + " " + _crontabExpression.hoursPattern + " " + _crontabExpression.daysPattern +
                " " + _crontabExpression.monthsPattern + " " + _crontabExpression.daysOfWeekPattern + " " + _crontabExpression.yearPattern;
        },
        setSpecificDatePatternValues: function () {
            var datePicker = $(selectors.scheduleDatePicker).data("kendoDatePicker");
            _crontabExpression.daysPattern = datePicker.value().getDate();
            _crontabExpression.monthsPattern = datePicker.value().getMonth() + 1;
            _crontabExpression.yearPattern = datePicker.value().getFullYear();
        },
        setSpecificTimePatternValues: function () {
            if (!_isExpressionSet) {
                var timePicker = $(selectors.scheduleTimePicker).data("kendoTimePicker");
                if (timePicker.value()) {
                    _crontabExpression.hoursPattern = timePicker.value().getHours();
                    _crontabExpression.minutesPattern = timePicker.value().getMinutes();
                }                
            }            
        },
        setCronTabExpression: function () {
            var cronExpression = $(selectors.customDatePicker).val();
            var isValidExpression = methods.isExpressionStringValid(cronExpression);
            if (isValidExpression) {
                methods.parseExpression(cronExpression);
            }
            else {
                alert("There was an error parsing the template.");                
            }
        },
        parseExpression: function(expression) {
            var expressionToSet = expression.split(' ');
            _crontabExpression.minutesPattern = expressionToSet[0];
            _crontabExpression.hoursPattern=expressionToSet[1];
            _crontabExpression.daysPattern = expressionToSet[2];
            _crontabExpression.monthsPattern = expressionToSet[3];
            _crontabExpression.daysOfWeekPattern = expressionToSet[4];
            if (expressionToSet.length > 5) {
                _crontabExpression.yearPattern = expressionToSet[5];
            }
        },
        setScheduleTimeChoicePatterns: function () {
            var selectedTimeValue = $(selectors.scheduleTimeChoices).val();
            switch (selectedTimeValue) {              
                case 'tomorrow':
                    var tomorrowDate = methods.getTomorrowDate();
                    _crontabExpression.daysPattern = tomorrowDate.getDate();
                    _crontabExpression.monthsPattern = tomorrowDate.getMonth() + 1;
                    _crontabExpression.daysOfWeekPattern = "*";
                    _crontabExpression.yearPattern = tomorrowDate.getFullYear();
                    _isExpressionSet = false;
                    break;
                case 'specific-date':
                    {
                        _crontabExpression.daysPattern = "*";
                        _crontabExpression.monthsPattern = "*";
                        _crontabExpression.daysOfWeekPattern = "*";
                        _crontabExpression.yearPattern = "*";
                        methods.setSpecificDatePatternValues();
                        _isExpressionSet = false;
                    }
                    break;
                case 'custom-date':
                    methods.setCronTabExpression();
                    _isExpressionSet = true;
                    break;
                case 'today':
                default:
                    var todayDate = now();
                    _crontabExpression.daysPattern = todayDate.getDate();
                    _crontabExpression.monthsPattern = todayDate.getMonth() + 1;
                    _crontabExpression.daysOfWeekPattern = "*";
                    _crontabExpression.yearPattern = todayDate.getFullYear();
                    _isExpressionSet = false;
                    break;
            }
        },
        getTomorrowDate: function () {
            var oneDayInMilliseconds = 86400000; //1000*3600*24
            return new Date(now().getTime() + oneDayInMilliseconds);
        },
        prepareBeforeScheduleButtons: function () {
            $(selectors.scheduleButtonsContainer).show();
            $(selectors.scheduleButton).hide();
            $(selectors.configureSchedulingButton).show();
            $(selectors.changeScheduleButton).hide();
            $(selectors.cancelScheduleButton).hide();
            $(selectors.cancelConfiguringScheduleButton).hide();
            $(selectors.orCancelScheduleButton).hide();            
        },
        prepareAfterScheduleButtons: function () {
            $(selectors.scheduleContainer).show();
            $(selectors.cancelConfiguringScheduleButton).hide();
            $(selectors.scheduleButtonsContainer).show();
            $(selectors.configureSchedulingButton).hide();
            $(selectors.changeScheduleButton).show();
            $(selectors.cancelScheduleButton).show();
            $(selectors.orCancelScheduleButton).show();            
        },
        _getSummary: function () {
            var resultText;
            var timePickerValue;

            var selectedChoice = $(selectors.scheduleTimeChoices).val();
            if (selectedChoice == 'specific-date') {
                resultText = $(selectors.scheduleDatePicker).val();
                timePickerValue = kendo.format('{0:hh:mm tt}', $(selectors.scheduleTimePicker).data("kendoTimePicker").value());
                resultText += ", " + timePickerValue;
            }
            else if (selectedChoice == 'custom-date') {
                resultText = 'Custom, ' + methods.getCrontabExpressionString();
            }
            else {
                resultText = $(selectors.selectecScheduleTimeChoice).text().trim();
                timePickerValue = kendo.format('{0:hh:mm tt}', $(selectors.scheduleTimePicker).data("kendoTimePicker").value());
                if (timePickerValue != null) {
                    resultText += ", " + timePickerValue;
                }
                else {
                    resultText += ", " + methods.getCrontabExpressionString();
                }                
            }           

            return resultText;
        },
        showScheduleSummary: function (noRefresh) {
            if (typeof _settings.onScheduleSummaryChange != 'function') {
                return;
            }

            if (!noRefresh) {
                _summary = methods._getSummary.apply(this);
            }

            _settings.onScheduleSummaryChange(_summary);
        },
        hideScheduleSummary: function () {
            if (typeof _settings.onScheduleSummaryChange == 'function') {
                _settings.onScheduleSummaryChange(null);
            }
        },
        isExpressionStringValid: function (expressionString) {
            if (typeof expressionString !== "string" || expressionString.length == 0)
                return false;
            var expressionToSet = expressionString.split(' ');
            if (expressionToSet.length != 6)
                return false;

            // check if all patterns are in valid range valid or "*"
            var isMinutesPatternValid = methods.isPatternValid.apply(this, [expressionToSet[0], 0, 59]);
            var isHoursPatternValid = methods.isPatternValid.apply(this, [expressionToSet[1], 0, 23]);
            var isDaysPatternValid = methods.isPatternValid.apply(this, [expressionToSet[2], 0, 31]);
            var isMonthsPatternValid = methods.isPatternValid.apply(this, [expressionToSet[3], 1, 12]);
            var isDaysOfWeekPatternValid = methods.isPatternValid.apply(this, [expressionToSet[4], 0, 6]);

            var currentYear = now().getFullYear();
            var isYearPatternValid = methods.isPatternValid.apply(this, [expressionToSet[5], currentYear, _datePickerMaxYear]);

            return isMinutesPatternValid && isHoursPatternValid && isDaysPatternValid && isMonthsPatternValid && isDaysOfWeekPatternValid && isYearPatternValid;
        },
        isPatternValid: function (pattern, rangeFrom, rangeTo) {
            
            if (pattern === "*") {
                return true;
            }

            var number = pattern;
            if (pattern.length > 1 && 
                (pattern.startsWith("*/") || pattern.startsWith("0/"))) {
                number = pattern.substr(2); //remove the starting part
            }

            var isValid = false;
            if (!isNaN(number)) {
                var patternAsNumber = parseInt(number);
                if (patternAsNumber >= rangeFrom && patternAsNumber <= rangeTo)
                    isValid = true;
            }
            return isValid;
        },
        getExpressionObjectOrDefault: function (expressionString) {
            var useDefault = !expressionString || !methods.isExpressionStringValid.apply(this, [expressionString]);
            if (useDefault) {
                if (expressionString) {
                    // Provided and invalid - show error.
                    methods.logErrorMessage('Invalid schedule expression: ' + expressionString);
                }

                // If expression is not provided or is not valid, show  the UI with the default expression.
                expressionString = _defaultExpressionString;
            }
            var expressionStringArray = expressionString.split(' ');
            var crontabExpressionObject = {
                minutesPattern: expressionStringArray[0],
                hoursPattern: expressionStringArray[1],
                daysPattern: expressionStringArray[2],
                monthsPattern: expressionStringArray[3],
                daysOfWeekPattern: expressionStringArray[4],
                yearPattern: expressionStringArray[5]
            }
            return crontabExpressionObject;
        },

        
        prepareUIForExpression: function (expressionString) {
            if (_crontabExpression) {
                var crontabExpressionToSet = _crontabExpression;
                var timePicker = $(selectors.scheduleTimePicker).data("kendoTimePicker");
                $(selectors.dateTimePickerContainer).hide();
                var scheduleTimeChoices = $(selectors.scheduleTimeChoices);
                var datePicker = $(selectors.scheduleDatePicker).data("kendoDatePicker");

                if (crontabExpressionToSet.daysPattern != "*" && crontabExpressionToSet.monthsPattern != "*" && crontabExpressionToSet.yearPattern != "*") {
                    // check if date is today
                    var todayDate = now();
                    var tomorrowDate = methods.getTomorrowDate();
                    if (todayDate.getDate() == crontabExpressionToSet.daysPattern &&
                        todayDate.getMonth() + 1 == crontabExpressionToSet.monthsPattern &&
                        todayDate.getFullYear() == crontabExpressionToSet.yearPattern) {
                        // value of date is today
                        scheduleTimeChoices.val("today");
                        //methods.setValidTimePickerRange();
                    } else if (tomorrowDate.getDate() == crontabExpressionToSet.daysPattern &&
                        tomorrowDate.getMonth() + 1 == crontabExpressionToSet.monthsPattern &&
                        tomorrowDate.getFullYear() == crontabExpressionToSet.yearPattern) {
                        // value of date is tomorrow
                        scheduleTimeChoices.val("tomorrow");
                        timePicker.value(tomorrowDate);
                    } else {
                        // we have specific date set
                        scheduleTimeChoices.val("specific-date");
                        var dateToSet = now();
                        dateToSet.setDate(crontabExpressionToSet.daysPattern);
                        dateToSet.setMonth(crontabExpressionToSet.monthsPattern - 1);
                        dateToSet.setFullYear(crontabExpressionToSet.yearPattern);
                        datePicker.value(dateToSet);
                        datePicker.trigger("change");
                        $(selectors.dateTimePickerContainer).show();
                    }
                }
                else {
                    scheduleTimeChoices.val("custom-date");                    
                    var cronValue = methods.getCrontabExpressionString().slice(0, methods.getCrontabExpressionString().length - 2);
                    $('#cron').cron("value", cronValue);
                }
                var currentDate = timePicker.value();
                if (crontabExpressionToSet.minutesPattern != "*") {
                    // we have set time in the expression so set the value of the time picker                
                    if (currentDate == null)
                        currentDate = now();
                    currentDate.setMinutes(crontabExpressionToSet.minutesPattern);
                    if (crontabExpressionToSet.hoursPattern != "*") {
                        currentDate.setHours(crontabExpressionToSet.hoursPattern);
                    }
                    else {
                        currentDate.setHours(0);
                    }

                    timePicker.value(currentDate);
                }
                else {

                    if (currentDate == null)
                        currentDate = now();
                    currentDate.setHours(crontabExpressionToSet.hoursPattern);
                    if (crontabExpressionToSet.minutesPattern != "*") {
                        currentDate.setHours(crontabExpressionToSet.minutesPattern);
                    }
                    else {
                        currentDate.setMinutes(0);
                    }

                    timePicker.value(currentDate);
                }
                methods.scheduleTimeChoicesChange();
            }
        },
        prepareInitialUI: function () {
            if (!_settings.crontabExpression || (_settings.crontabExpression == _defaultExpressionString)) {
                methods.prepareUIForExpression.apply(this, [_defaultExpressionString]);            
                $(selectors.scheduleContainer).hide();
                $(selectors.changeScheduleButton).hide();
                $(selectors.cancelConfiguringScheduleButton).hide();
                $(selectors.orCancelScheduleButton).hide();
                $(selectors.cancelScheduleButton).hide;
            }            
        },
        logErrorMessage: function (msg) {
            if (console && console.error) {
                console.error(msg);
            }
        },
        scheduleButtonClickHandler: function () {
            
            methods.setScheduleTimeChoicePatterns();
            methods.setSpecificTimePatternValues();            

            if (typeof _settings.onSchedule == 'function') {
                _settings.onSchedule(_schedulerView);
            }
        },

        insertParam: function(key, value) {
                key = encodeURI(key);
            value = encodeURI(value);
            var keyPair = document.location.search.substr(1).split('&');

            var i = keyPair.length;
            var x;
            while (i--) {
                x = keyPair[i].split('=');
                if (x[0]==key) {
                    x[1]= value;
                    keyPair[i]= x.join('=');
                    break;
                }
                }

            if (i < 0) {
                keyPair[keyPair.length]=[key, value].join('=');
            }
            if(value=="true") {
                document.location.search = keyPair.join('&');
                }
        },

        cancelScheduleButtonClickHandler: function () {            
            methods.insertParam("taskId", "");
            if (typeof _settings.onCancelSchedule == 'function') {
                _settings.onCancelSchedule(_schedulerView, methods.onCancelScheduleSuccess, methods.onCancelScheduleError);
            }
        },
        onCancelScheduleSuccess: function () {
            _isExpressionSet = false;
            methods.insertParam("taskId", "");
            methods.hideScheduleSummary();
            $(selectors.scheduleContainer).hide();
            methods.prepareBeforeScheduleButtons();
            $(selectors.scheduleButtonsContainer).show();
        },
        onCancelScheduleError: function () {
        },
        scheduleTimeChoicesChange: function (e) {
            var selectedValue = $(selectors.scheduleTimeChoices).val();
            if (selectedValue === 'today') {
                var taskId = methods.getParameterByName("taskId");            
                if (!taskId) {
                    methods.setValidTimePickerRange();
                }
            }
            else if (selectedValue === "tomorrow" && e) {
                //check if the argument 'e' is undefinted because when method is called
                //from prepareUIForExpression we do not want to reset time picker
                methods.resetTimePickerRange();
            }

            else if (selectedValue === 'custom-date') {
                $(selectors.customDatePicker).val(methods.getCrontabExpressionString);
            }
            
            $(selectors.scheduleDateTimePicker).toggle(selectedValue !== 'custom-date');
            $(selectors.scheduleTimePicker).toggle(selectedValue !== 'custom-date');
            $(selectors.dateTimePickerContainer).toggle(selectedValue === 'specific-date');
            $(selectors.jqCronContainer).toggle(selectedValue === 'custom-date');            
        },
        // when we have today or specific date today selected we need to validate hours selection
        setValidTimePickerRange: function () {
            if (_settings && _settings.allowPassedTime) {
                return;
            }

            var timePicker = $(selectors.scheduleTimePicker).data("kendoTimePicker");
            var interval = timePicker.options.interval;

            var time = now();

            // Calc the next minute interval.
            var minutes = (Math.floor(time.getMinutes() / interval) + 1) * interval;
            if (minutes < 60) {
                time.setMinutes(minutes);
            } else {
                time.setMinutes(0);
                time.setHours(time.getHours() + 1);
            }

            time.setSeconds(0);
            time.setMilliseconds(0);

            timePicker.min(time);
            timePicker.value(time);
        },
        resetTimePickerRange: function () {
            var timePicker = $(selectors.scheduleTimePicker).data("kendoTimePicker");
            currentDate = new Date();
            currentDate.setHours(0);
            currentDate.setMinutes(0);
            currentDate.setSeconds(0);
            currentDate.setMilliseconds(0);
            timePicker.min(currentDate);
            timePicker.value(currentDate);
        },
        scheduleDatePickerChangeHandler: function (e) {
            var todayDate = now();
            var datePickerCurrentDate = e.sender.value();
            if (todayDate.getDate() == datePickerCurrentDate.getDate() &&
                todayDate.getMonth() == datePickerCurrentDate.getMonth() &&
                todayDate.getFullYear() == datePickerCurrentDate.getFullYear()) {
                // value of date is today
                methods.setValidTimePickerRange();
            } else {
                methods.resetTimePickerRange();
            }
        },
        configureSchedulingButtonClickHandler: function () {            
            methods.setScheduleTimeChoicePatterns();
            methods.setSpecificTimePatternValues();
            methods.prepareUIForExpression.apply(this, [methods.getCrontabExpressionString()]);
            $(selectors.errorMessageContainer).hide();                
            $(selectors.scheduleContainer).show();
            $(selectors.scheduleButton).show();
            $(selectors.cancelConfiguringScheduleButton).show();
            $(selectors.configureSchedulingButton).hide();
            $(selectors.orCancelScheduleButton).hide();
            $("[id$='startSyncBtn']").hide();
        },
        cancelConfiguringScheduleButtonClickHandler: function () {
            methods.prepareBeforeScheduleButtons();
            $(selectors.scheduleContainer).hide();
            $(selectors.cancelConfiguringScheduleButton).hide();
            $("[id$='startSyncBtn']").show();            
        },
        changeScheduleButtonClickHandler: function () {
            methods.setScheduleTimeChoicePatterns();
            methods.setSpecificTimePatternValues();
            methods.prepareUIForExpression.apply(this, [methods.getCrontabExpressionString()]);
            _settings.crontabExpression = methods.getCrontabExpressionString();
            
            if (typeof _settings.onChangeSchedule == 'function') {
                _settings.onChangeSchedule(_schedulerView);
            }
        }
    };

    // scheduler view plugin. This plugin provides functionality for creating a crontab expression
    $.fn.schedulerView = function (method) {
        // Method calling logic
        if (methods[method]) {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return methods.init.apply(this, arguments);
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.schedulerView');
        }
    };
});
