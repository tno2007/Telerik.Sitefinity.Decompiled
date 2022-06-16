Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

(function () {

    var $ = $telerik.$;
    var $T = Telerik.Sitefinity.Web.UI.Fields;
    var $DateTime = function () { };
    var minDate = new Date("1970/01/01");
    var maxDate = new Date("9000/01/01");
    var maxInt = 2147483647;
    var timePerMinute = 60000;
    var newLine = "\r\n";

    //-------- DateTime class ----------------

    var $DateTime = $T.DateTime = function (date) {
        // date can be null, Date or $DateTime
        if (!date) {
            this._date = new Date();
            return;
        }

        if (date.getTime)
            this._date = new Date(date.getTime());
        else
            this._date = new Date(date);
    };

    $DateTime._addTicks = function (date, ticks) {
        var resultDate = new Date(date.getTime() + ticks);
        return resultDate;
    };

    $DateTime.add = function (date, valueToAdd) {
        // valueToAdd can be a $TimeSpan or ticks
        var ticks = valueToAdd.get_ticks ? valueToAdd.get_ticks() : valueToAdd;
        return $DateTime._addTicks(date, ticks);
    };

    $DateTime.subtract = function (date, dateToSubtract) {
        dateToSubtract = new $DateTime(dateToSubtract).toDate();
        var diff = date.getTime() - dateToSubtract;
        var tzOffsetDiff = date.getTimezoneOffset() - dateToSubtract.getTimezoneOffset();
        return diff - (tzOffsetDiff * timePerMinute);
    };

    $DateTime.getDate = function (date) {
        return new Date(date.getFullYear(), date.getMonth(), date.getDate());
    };

    $DateTime.getTimeOfDay = function (date) {
        return $DateTime.subtract(date, $DateTime.getDate(date));
    };

    $T.DateTime.prototype =
    {
        get_date: function () {
            return new $DateTime($DateTime.getDate(this._date));
        },

        get_timeOfDay: function () {
            return $DateTime.getTimeOfDay(this._date);
        },

        add: function (valueToAdd) {
            return new $DateTime($DateTime.add(this._date, valueToAdd));
        },

        subtract: function (dateToSubtract) {
            return $DateTime.subtract(this._date, dateToSubtract);
        },

        toDate: function () {
            return this._date;
        }
    };

    // ---------- RecurrenceDay Enum ----------
    $T.RecurrenceDay = function () { };

    $T.RecurrenceDay.prototype =
    {
        None: 0,
        Sunday: 1,
        Monday: 1 << 1,
        Tuesday: 1 << 2,
        Wednesday: 1 << 3,
        Thursday: 1 << 4,
        Friday: 1 << 5,
        Saturday: 1 << 6
    };

    var rday = $T.RecurrenceDay.prototype;
    rday.EveryDay = rday.Monday | rday.Tuesday | rday.Wednesday | rday.Thursday | rday.Friday | rday.Saturday | rday.Sunday;
    rday.WeekDays = rday.Monday | rday.Tuesday | rday.Wednesday | rday.Thursday | rday.Friday;
    rday.WeekendDays = rday.Saturday | rday.Sunday;

    $T.RecurrenceDay.registerEnum("Telerik.Sitefinity.Web.UI.Fields.RecurrenceDay", true);


    // ---------- RecurrenceFrequency Enum ----------
    $T.RecurrenceFrequency = function () { };

    $T.RecurrenceFrequency.prototype =
    {
        None: 0,
        Hourly: 1,
        Daily: 2,
        Weekly: 3,
        Monthly: 4,
        Yearly: 5
    };

    $T.RecurrenceFrequency.registerEnum("Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency");


    // ---------- RecurrenceMonth Enum ----------
    $T.RecurrenceMonth = function () { };

    $T.RecurrenceMonth.prototype =
    {
        None: 0,
        January: 1,
        February: 2,
        March: 3,
        April: 4,
        May: 5,
        June: 6,
        July: 7,
        August: 8,
        September: 9,
        October: 10,
        November: 11,
        December: 12
    };

    $T.RecurrenceMonth.registerEnum("Telerik.Sitefinity.Web.UI.Fields.RecurrenceMonth");


    // ---------- RecurrencePattern Class ----------
    $T.RecurrencePattern = function () {
        this._frequency = $T.RecurrenceFrequency.None;
        this._interval = 0;
        this._daysOfWeekMask = $T.RecurrenceDay.None;
        this._dayOfMonth = 0;
        this._dayOrdinal = 0;
        this._month = $T.RecurrenceMonth.None;
        this._firstDayOfWeek = $T.RecurrenceDay.Sunday;
    };

    $T.RecurrencePattern.prototype =
    {
        get_frequency: function () {
            return this._frequency;
        },

        set_frequency: function (value) {
            this._frequency = value;
        },

        get_interval: function () {
            return this._interval;
        },

        set_interval: function (value) {
            this._interval = value;
        },

        get_daysOfWeekMask: function () {
            return this._daysOfWeekMask;
        },

        set_daysOfWeekMask: function (value) {
            this._daysOfWeekMask = value;
        },

        get_dayOfMonth: function () {
            return this._dayOfMonth;
        },

        set_dayOfMonth: function (value) {
            this._dayOfMonth = value;
        },

        get_dayOrdinal: function () {
            return this._dayOrdinal;
        },

        set_dayOrdinal: function (value) {
            this._dayOrdinal = value;
        },

        get_month: function () {
            return this._month;
        },

        set_month: function (value) {
            this._month = value;
        },

        get_firstDayOfWeek: function () {
            return this._firstDayOfWeek;
        },

        set_firstDayOfWeek: function (value) {
            this._firstDayOfWeek = value;
        }
    };

    $T.RecurrencePattern.registerClass("Telerik.Sitefinity.Web.UI.Fields.RecurrencePattern");

    // ---------- RecurrenceRange Class ----------
    $T.RecurrenceRange = function (start, duration, recursUntil, maxOccurrences, timeZoneId) {
        this._start = start || minDate;
        this._eventDuration = duration || 0;
        this._recursUntil = recursUntil || maxDate;
        this._maxOccurrences = maxOccurrences || maxInt;
        this._timeZoneId = timeZoneId;
    };

    $T.RecurrenceRange.prototype =
    {
        get_start: function () {
            return this._start;
        },

        set_start: function (value) {
            this._start = value;
        },

        get_eventDuration: function () {
            return this._eventDuration;
        },

        set_eventDuration: function (value) {
            this._eventDuration = value;
        },

        get_recursUntil: function () {
            return this._recursUntil;
        },

        set_recursUntil: function (value) {
            this._recursUntil = value;
        },

        get_maxOccurrences: function () {
            return this._maxOccurrences;
        },

        set_maxOccurrences: function (value) {
            this._maxOccurrences = value;
        },

        get_timeZoneId: function () {
            return this._timeZoneId;
        },

        set_timeZoneId: function (value) {
            this._timeZoneId = value;
        },
    };

    $T.RecurrenceRange.registerClass("Telerik.Sitefinity.Web.UI.Fields.RecurrenceRange");


    // ---------- RecurrenceRule Class ----------
    $T.RecurrenceRule = function () {
        this._exceptions = [];

        var dayAbbrev = {};
        dayAbbrev[$T.RecurrenceDay.Monday] = "MO";
        dayAbbrev[$T.RecurrenceDay.Tuesday] = "TU";
        dayAbbrev[$T.RecurrenceDay.Wednesday] = "WE";
        dayAbbrev[$T.RecurrenceDay.Thursday] = "TH";
        dayAbbrev[$T.RecurrenceDay.Friday] = "FR";
        dayAbbrev[$T.RecurrenceDay.Saturday] = "SA";
        dayAbbrev[$T.RecurrenceDay.Sunday] = "SU";

        this._dayAbbrev = dayAbbrev;
    };

    $T.RecurrenceRule.parse = function (input) {
        if (!input)
            return null;

        var range = new $T.RecurrenceRange();
        var pattern = new $T.RecurrencePattern();
        var exceptions = [];

        var start = null;
        var end = null;
        var tzId = null;

        var inputLines = input.split("\n");
        $.each(inputLines, function () {
            var line = this.trim();
            var dateHeaderMatch = line.match(/^(DTSTART|DTEND)(;TZID=\"(.*)\")?:(.*)/i);
            if (dateHeaderMatch) {
                var parsedDate = $T.RecurrenceRule._parseDateTime(dateHeaderMatch[4]);
                if (dateHeaderMatch[2]) {
                    tzId = dateHeaderMatch[3];
                }
                else {
                    tzId = "UTC";
                }
                if (dateHeaderMatch[1] == "DTSTART") {
                    start = parsedDate;
                }
                else {
                    end = parsedDate;
                }
            }

            $T.RecurrenceRule._parseRRule(line, range, pattern);
            $T.RecurrenceRule._parseExceptions(line, exceptions);
        });

        var rrule = null;
        if (start && end) {
            range.set_start(start);
            range.set_timeZoneId(tzId);
            range.set_eventDuration($DateTime.subtract(end, start));
            rrule = $T.RecurrenceRule.fromPatternAndRange(pattern, range);
            Array.addRange(rrule.get_exceptions(), exceptions);
        }

        return rrule;
    };

    $T.RecurrenceRule._parseDateTime = function (input) {
        var date = null;
        var dateMatch = input.match(/^(\d{4})(\d{2})(\d{2})T(\d{2})(\d{2})(\d{2})(Z?)(.*)$/i);
        if (dateMatch) {
            var year = parseInt(dateMatch[1], 10);
            var month = parseInt(dateMatch[2], 10);
            var day = parseInt(dateMatch[3], 10);
            var hour = parseInt(dateMatch[4], 10);
            var minute = parseInt(dateMatch[5], 10);
            var second = parseInt(dateMatch[6], 10);

            var valid = true;
            valid = valid && 1900 <= year && year <= 2900;
            valid = valid && 1 <= month && month <= 12;
            valid = valid && 1 <= day && day <= 31;
            valid = valid && 0 <= hour && hour <= 23;
            valid = valid && 0 <= minute && minute <= 59;
            valid = valid && 0 <= second && second <= 59;

            if (valid)
                date = new Date(year, month - 1, day, hour, minute, second);
        }

        return date;
    };

    $T.RecurrenceRule._parseRRule = function (line, range, pattern) {
        var rruleHeaderMatch = line.match(/^(RRULE:)(.*)$/i);
        if (rruleHeaderMatch) {
            var rules = rruleHeaderMatch[2];

            var frequencyMatch = rules.match(/FREQ=(HOURLY|DAILY|WEEKLY|MONTHLY|YEARLY)/i);
            if (frequencyMatch)
                pattern.set_frequency($T.RecurrenceFrequency.parse(frequencyMatch[1], true));

            var maxOccurrencesMatch = rules.match(/COUNT=(\d{1,4})/i);
            if (maxOccurrencesMatch)
                range.set_maxOccurrences(parseInt(maxOccurrencesMatch[1], 10));

            var recursUntilMatch = rules.match(/UNTIL=([\w\d]*)/i);
            if (recursUntilMatch) {
                var parsedDate = $T.RecurrenceRule._parseDateTime(recursUntilMatch[1]);
                if (parsedDate)
                    range.set_recursUntil(parsedDate);
            }

            var intervalMatch = rules.match(/INTERVAL=(\d{1,})/i);
            if (intervalMatch)
                pattern.set_interval(parseInt(intervalMatch[1], 10));

            var dayOrdinalMatch = rules.match(/BYSETPOS=(-?\d{1})/i);
            if (dayOrdinalMatch)
                pattern.set_dayOrdinal(parseInt(dayOrdinalMatch[1], 10));

            var dayOfMonthMatch = rules.match(/BYMONTHDAY=(\d{1,2})/i);
            if (dayOfMonthMatch)
                pattern.set_dayOfMonth(parseInt(dayOfMonthMatch[1], 10));

            var dayOfWeekMaskMatch = rules.match(/BYDAY=(-?\d{1})?([\w,]*)/i);
            if (dayOfWeekMaskMatch) {
                // The BYDAY rule might also contain a day ordinal value.
                if (dayOfWeekMaskMatch[1])
                    pattern.set_dayOrdinal(parseInt(dayOfWeekMaskMatch[1], 10));

                var mask = $T.RecurrenceRule._parseDaysOfWeekMask(dayOfWeekMaskMatch[2]);
                if (mask)
                    pattern.set_daysOfWeekMask(mask);
            }

            var monthMatch = rules.match(/BYMONTH=(\d{1,2})/i);
            if (monthMatch)
                pattern.set_month(parseInt(monthMatch[1], 10));

            var weekStartMatch = rules.match(/WKST=([\w,]*)/i);
            if (weekStartMatch)
                pattern.set_firstDayOfWeek($T.RecurrenceRule._parseDayOfWeek(weekStartMatch[1]));
        }
    };

    $T.RecurrenceRule._parseDaysOfWeekMask = function (input) {
        var mask = $T.RecurrenceDay.None;
        $.each(input.split(","), function () {
            var day = $T.RecurrenceRule._getRecurrenceDayFromAbbrev(this);
            if (day)
                mask |= day;
        });

        return mask;
    };

    $T.RecurrenceRule._parseDayOfWeek = function (input) {
        return $T.RecurrenceRule._getRecurrenceDayFromAbbrev(input) || $T.RecurrenceDay.Sunday;
    };

    $T.RecurrenceRule._getRecurrenceDayFromAbbrev = function (abbrev) {
        switch (abbrev.toUpperCase()) {
            case "MO": return $T.RecurrenceDay.Monday;
            case "TU": return $T.RecurrenceDay.Tuesday;
            case "WE": return $T.RecurrenceDay.Wednesday;
            case "TH": return $T.RecurrenceDay.Thursday;
            case "FR": return $T.RecurrenceDay.Friday;
            case "SA": return $T.RecurrenceDay.Saturday;
            case "SU": return $T.RecurrenceDay.Sunday;
            default: return null;
        }
    };

    $T.RecurrenceRule._parseExceptions = function (line, exceptions) {
        var exDateHeadersMatch = line.match(/^(EXDATE):(.*)$/i);
        if (exDateHeadersMatch) {
            $.each(exDateHeadersMatch[2].split(","), function () {
                var parsedDate = $T.RecurrenceRule._parseDateTime(this);
                if (parsedDate)
                    Array.add(exceptions, parsedDate);
            });
        }
    };

    $T.RecurrenceRule.fromPatternAndRange = function (pattern, range) {
        if (!pattern || !range)
            return null;

        var rrule = new $T.RecurrenceRule();
        rrule._pattern = pattern;
        rrule._range = range;

        return rrule;
    };

    $T.RecurrenceRule.prototype =
    {
        get_pattern: function () {
            return this._pattern;
        },

        get_range: function () {
            return this._range;
        },

        get_exceptions: function () {
            return this._exceptions;
        },

        toString: function () {
            var range = this.get_range();

            var start = this._toLocalDate(range.get_start());

            var end = $DateTime.add(start, range.get_eventDuration());

            var sb = new Telerik.Web.StringBuilder();

            var startStr = 'DTSTART;TZID="' + range.get_timeZoneId() + '":';
            var endStr = 'DTEND;TZID="' + range.get_timeZoneId() + '":';

            sb.append(startStr)
              .append(this._formatDate(start, true))
              .append(newLine);

            sb.append(endStr)
              .append(this._formatDate(end, true))
              .append(newLine);

            sb.append("RRULE:")
              .append(this._formatRRule())
              .append(newLine);

            sb.append(this._formatExceptions())
              .append(newLine);

            return sb.toString();
        },

        _formatRRule: function () {
            var sb = new Telerik.Web.StringBuilder();

            var pattern = this.get_pattern();
            var range = this.get_range();

            sb.append("FREQ=")
              .append($T.RecurrenceFrequency.toString(pattern.get_frequency()).toUpperCase())
              .append(";");

            if ((0 < range.get_maxOccurrences()) && (range.get_maxOccurrences() < maxInt)) {
                sb.append("COUNT=")
                  .append(range.get_maxOccurrences())
                  .append(";");
            }
            else
                if ((minDate < range.get_recursUntil()) && (range.get_recursUntil() < maxDate)) {
                    var date = this._toLocalDate(range.get_recursUntil());

                    sb.append("UNTIL=")
                      .append(this._formatDate(date, true))
                      .append(";");
                }

            if (0 < pattern.get_interval())
                sb.append("INTERVAL=")
                  .append(pattern.get_interval())
                  .append(";");

            if (pattern.get_dayOrdinal() != 0)
                sb.append("BYSETPOS=")
                  .append(pattern.get_dayOrdinal())
                  .append(";");

            if (0 < pattern.get_dayOfMonth())
                sb.append("BYMONTHDAY=")
                  .append(pattern.get_dayOfMonth())
                  .append(";");

            var daysOfWeekMask = pattern.get_daysOfWeekMask();
            if (daysOfWeekMask != $T.RecurrenceDay.None) {
                var days = [];
                for (var day in this._dayAbbrev) {
                    if ((daysOfWeekMask & day) == day)
                        Array.add(days, this._dayAbbrev[day]);
                }

                sb.append("BYDAY=")
                  .append(days.join(","))
                  .append(";");
            }

            if (pattern.get_month() != $T.RecurrenceMonth.None)
                sb.append("BYMONTH=")
                  .append(pattern.get_month())
                  .append(";");

            if (pattern.get_firstDayOfWeek() != $T.RecurrenceDay.Sunday)
                sb.append("WKST=")
                  .append(this._dayAbbrev[pattern.get_firstDayOfWeek()])
                  .append(";");

            var result = sb.toString();
            if (result.endsWith(";"))
                result = result.substring(0, result.length - 1);

            return result;
        },

        _formatExceptions: function () {
            if (this.get_exceptions().length == 0)
                return "";

            var rrule = this;
            var exStrings = $.map(this.get_exceptions(), function (exDate) {
                return rrule._formatDate(exDate, true);
            });

            var sb = new Telerik.Web.StringBuilder();
            sb.append("EXDATE:")
              .append(exStrings.join(","))
              .append(newLine);

            return sb.toString();
        },

        _formatDate: function (date, containsTime) {
            //var utcDate = new Date(date.setMinutes(date.getMinutes() + date.getTimezoneOffset()));
            var utcDate = date;
            var sb = new Telerik.Web.StringBuilder();
            sb.append(this._formatNum(utcDate.getFullYear()))
              .append(this._formatNum(utcDate.getMonth() + 1))
              .append(this._formatNum(utcDate.getDate()));

            if (containsTime) {
                sb.append("T")
                  .append(this._formatNum(utcDate.getHours()))
                  .append(this._formatNum(utcDate.getMinutes()))
                  .append(this._formatNum(utcDate.getSeconds()));
            }
            return sb.toString();
        },

        _formatNum: function (num, digits) {
            digits = digits || 2;
            var numStr = num.toString();

            var zeroesToAdd = digits - numStr.length;
            if (zeroesToAdd <= 0)
                return numStr;

            var sb = new Telerik.Web.StringBuilder();
            for (var i = 0; i < zeroesToAdd; i++)
                sb.append("0");

            sb.append(numStr);

            return sb.toString();
        },

        _toLocalDate: function (date) {
            var currentTimeOffset = new Date().getTimezoneOffset();
            var offsetDiff = currentTimeOffset - date.getTimezoneOffset();
            var originalDate = new Date(date);
            originalDate = new Date(originalDate.setMinutes(originalDate.getMinutes() - offsetDiff));

            var _date = new Date(date);
            var tempDate = new Date(_date.setMinutes(_date.getMinutes() + _date.getTimezoneOffset()));

            var dateCopy = new Date(originalDate);
            dateCopy.setMinutes(tempDate.getMinutes());
            dateCopy.setHours(tempDate.getHours());

            return dateCopy;
        }
    };

    $T.RecurrenceRule.registerClass("Telerik.Sitefinity.Web.UI.Fields.RecurrenceRule");
})();
