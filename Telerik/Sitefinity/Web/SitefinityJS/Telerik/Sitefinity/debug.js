Type.registerNamespace("Telerik.Sitefinity");

// ------------------------------------------------------------------------
//                          Global functions
// ------------------------------------------------------------------------

$sitefinity = Telerik.Sitefinity;

Telerik.Sitefinity.cloneObject = function (jsObject) {
    /// <summary>Try to perform a deep copy of a javascript object</summary>
    /// <param name="jsObject">Object to copy</param>
    /// <returns>Deep copy of <paramref name="jsObject" /></returns>

    if (typeof jsObject == "undefined" || jsObject == null)
        return jsObject;

    if (jsObject instanceof Array || typeof jsObject.length == "number") {
        var clonedArray = [];
        var elementCopy = null;
        for (var i = 0, len = jsObject.length; i < len; i++) {
            elementCopy = jsObject[i];
            elementCopy = Telerik.Sitefinity.cloneObject(elementCopy);
            clonedArray.push(elementCopy);
        }
        return clonedArray;
    }   

    var clone = {};
    for (var memberName in jsObject) {
        var memberValue = jsObject[memberName];
        clone[memberName] = memberValue instanceof Array ? Array.clone(memberValue) : memberValue;
    }
    return clone;
}

Telerik.Sitefinity.compareObjects = function (obj1, obj2) {
    for (var p in obj1) {
        if (obj1.hasOwnProperty(p) !== obj2.hasOwnProperty(p)) return false;

        if (obj1[p] === null && obj2[p] !== null) return false;
        if (obj2[p] === null && obj1[p] !== null) return false;

        switch (typeof (obj1[p])) {
            case 'object':
                if (!Telerik.Sitefinity.compareObjects(obj1[p], obj2[p])) return false;
                break;
            case 'function':
                if (typeof (obj2[p]) == 'undefined' || (p != 'compare' && obj1[p].toString() != obj2[p].toString())) return false;
                break;
            default:
                if (obj1[p] === '' && obj2[p] !== '') return false;
                if (obj2[p] === '' && obj1[p] !== '') return false;
                if (obj1[p] != obj2[p]) return false;
        }
    }

    //Check object 2 for any extra properties
    for (var p in obj2) {
        if (typeof (obj1[p]) == 'undefined') return false;
    }

    return true;
}

Telerik.Sitefinity.find = function (arr, callback) {
    for (var i = 0; i < arr.length; i++) {
        var match = callback(arr[i]);
        if (match) {
            return arr[i];
            break;
        }
    }
}

function Telerik$Sitefinity$isBrokenArray(obj) {
    if (obj != null && typeof obj == "object" && !(obj instanceof Array)) {
        var isBrokenArray = typeof obj.length == "number" && typeof obj.concat == "function" && typeof obj.join == "function"
            && typeof obj.pop == "function" && typeof obj.push == "function" && typeof obj.reverse == "function"
            && typeof obj.shift == "function" && typeof obj.slice == "function" && typeof obj.splice == "function"
            && typeof obj.sort == "function" && typeof obj.toString == "function" && typeof obj.unshift == "function"
            && typeof obj.valueOf == "function";
        return isBrokenArray;
    }
    else {
        return false;
    }
}
Telerik.Sitefinity.isBrokenArray = Telerik$Sitefinity$isBrokenArray;

function Telerik$Sitefinity$fixArray(jsArray) {
    if (typeof jsArray != "undefined" && jsArray != null) {
        if (jsArray instanceof Array || Array.prototype.isPrototypeOf(jsArray) || jsArray.constructor == Array) {
            return jsArray;
        }
        else if (typeof jsArray.length == "number") {
            // this can happen when the browser performs a copy of a JS object; e.g. passing values between window and iframe
            try {
                var copy = [];
                var len = jsArray.length;
                for (var i = 0; i < len; i++) {
                    copy.push(jsArray[i]);
                }
                return copy;
            }
            catch (castError) {
                return null;
            }
        }
        else {
            // this can happen when we serialzie and deserialize an array with Sys.Serialization.JavaScriptSerialzier
            try {
                var copy = [];
                for (var propName in jsArray) {
                    if (!isNaN(parseInt(propName))) {
                        copy.push(jsArray[propName]);
                    }
                }
                return copy;
            }
            catch (e) {
                return null;
            }
        }
    }
    return null;
}
Telerik.Sitefinity.fixArray = Telerik$Sitefinity$fixArray;

function Telerik$Sitefinity$fixObjectForSerialization(obj) {
    var objType = typeof obj;
    if (obj == null || objType != "object") {
        return obj;
    }
    else {
        if (Telerik$Sitefinity$isBrokenArray(obj)) {
            obj = Telerik$Sitefinity$fixArray(obj);
        }
        for (var propName in obj) {
            var val = obj[propName];
            var fixed = Telerik$Sitefinity$fixObjectForSerialization(val);
            obj[propName] = fixed;
        }
        return obj;
    }
}
Telerik.Sitefinity.fixObjectForSerialization = Telerik$Sitefinity$fixObjectForSerialization;



Telerik.Sitefinity.getEmptyGuid = function () {
    /// <summary> Get an empty GUID </summary>
    /// <returns>string</returns>
    return '00000000-0000-0000-0000-000000000000';
}

Telerik.Sitefinity.encodeWcfString = function (wcfString) {
    /// <summary> Encode a wcf string (Type.AssemblyQualifiedName) so that it doesn't use special characters </summary>
    /// <param name="wcfString">string to encode</param>
    /// <returns>string</returns>
    if (typeof (wcfString) == typeof ("") && wcfString != null && wcfString.length > 0) {
        var encoded = wcfString;
        var encodeMap = Telerik.Sitefinity._Implementation.get_encodeWcfMap();
        for (var codeSymbol in encodeMap) {
            encoded = encoded.replace(codeSymbol, encodeMap[codeSymbol], "g");
        }
        return encoded;
    }
    else {
        return "";
    }
}

Telerik.Sitefinity.decodeWcfString = function (wcfString) {
    /// <summary> Decodes a previously encoded string (Type.AssemblyQualifiedName) </summary>
    /// <param name="wcfString">string to decode (returning special characters)</param>
    /// <returns>string</returns>
    if (typeof (wcfString) == typeof ("") && wcfString != null && wcfString.length > 0) {
        var decoded = wcfString;
        var decodeMap = Telerik.Sitefinity._Implementation.get_decodeWcfMap();
        for (var decodeSymbol in decodeMap) {
            decoded = decoded.replace(decodeSymbol, decodeMap[decodeSymbol], "g");
        }
        return decoded;
    }
    else {
        return "";
    }
}

Telerik.Sitefinity.stripHtml = function (text) {
    /// <summary>Strips all HTML from a text string and returns only the text</summary>
    /// <param name="text">String to strip from HTML</param>
    /// <returns>The original string without any HTML tags</returns>

    var div = document.createElement("div");
    div.innerHTML = "<b>txt</b><br/>  another word";
    var txt = div.textContent;
    delete div;

    return txt;
}

Telerik.Sitefinity.cleanjQueryData = function (domElement) {
    /// <summary>
    /// Cleans the element and it's child element from the jQuery system attribute usually something like jQuery321313131
    /// This is useful when populating templates in order not to clone the jQuery behavior or data
    /// </summary>
    domElement.innerHTML = templateElement.innerHTML.replace(/ jQuery\d+="(?:\d+|null)"/g, "");
    domElement.instantiateIn(itemContainer, null, dataItem);
}

Telerik.Sitefinity.centerWindowHorizontally = function (radWindow, topOffset) {
    /// <summary>
    /// Positions the provided element in the center of the view port horizontally
    /// and vertically in a given offset from the top of the view port.
    /// </summary>

    var viewPortWidth = $(window).width();
    var viewPortScrollTop = $(window).scrollTop();
    var viewPortScrollLeft = $(window).scrollLeft();
    var windowWidth = radWindow.get_width();

    var y = parseInt(topOffset || 50 + viewPortScrollTop);
    var x = parseInt((viewPortWidth - windowWidth) / 2) + viewPortScrollLeft;

    radWindow.moveTo(x, y);
}

Telerik.Sitefinity.setUrlParameter = function (url, parameterName, parameterValue) {
    /// <summary>
    /// Adds/Replaces URL parameters
    /// </summary>

    if ((url == null) || (url.length == 0)) {
        url = document.location.href;
    }

    var urlParts = url.split("?");
    var newQueryString = "";
    var parameterNameLower = parameterName.toLowerCase();
    if (urlParts.length > 1) {
        var parameters = urlParts[1].split("&");
        for (var i = 0, length = parameters.length; i < length; i++) {
            var parameterParts = parameters[i].split("=");
            if (parameterParts[0].toLowerCase() != parameterNameLower) {
                if (newQueryString == "") {
                    newQueryString = "?";
                }
                else {
                    newQueryString += "&";
                }
                newQueryString += parameterParts[0] + "=" + parameterParts[1];
            }
        }
    }
    if (newQueryString == "") {
        newQueryString = "?";
    }
    else {
        newQueryString += "&";
    }
    newQueryString += parameterName + "=" + parameterValue;

    return urlParts[0] + newQueryString;
}

//***********************************Cookie methods*******************************//
Telerik.Sitefinity.setPropertyValueInCookie = function (cookieKey, property, value) {
    if (jQuery && typeof (jQuery.cookie) === 'function') {
        var cookieValue = jQuery.cookie(cookieKey);
        var properties = [];
        var hasPropertyValue = false;

        if (cookieValue != null) { // the cookie is initialized           
            properties = Telerik.Sitefinity.JSON.parse(cookieValue);

            for (var i = 0; i < properties.length; i++) {
                if (properties[i].Key == property) { //the property is set in the cookie
                    properties[i].Value = value;
                    hasPropertyValue = true;
                }
            }
        }

        if (!hasPropertyValue) { //the property is not set in the cookie
            var propertyObject = {};
            propertyObject.Key = property;
            propertyObject.Value = value;

            properties.push(propertyObject);
        }
        // this is large enough expiration date, some browsers have problems with dates past 2038
        jQuery.cookie(cookieKey, Telerik.Sitefinity.JSON.stringify(properties), { expires: 2020 });
    }
    else {
        throw new Error('jQuery.cookie library is not loaded!');
    }
}

/**
 * JSON parser and serializer. Original code address: http://www.json.org/json2.js
 * Usage: 
 *  Telerik.Sitefinity.JSON.stringify(obj)
 *  Telerik.Sitefinity.JSON.parse(txt)
**/
if (!this.Telerik.Sitefinity.JSON) { this.Telerik.Sitefinity.JSON = {}; }
(function () {
    function f(n) { return n < 10 ? '0' + n : n; }
    if (typeof Date.prototype.toJSON !== 'function') {
        Date.prototype.toJSON = function (key) {
            return isFinite(this.valueOf()) ? this.getUTCFullYear() + '-' +
f(this.getUTCMonth() + 1) + '-' +
f(this.getUTCDate()) + 'T' +
f(this.getUTCHours()) + ':' +
f(this.getUTCMinutes()) + ':' +
f(this.getUTCSeconds()) + 'Z' : null;
        }; String.prototype.toJSON = Number.prototype.toJSON = Boolean.prototype.toJSON = function (key) { return this.valueOf(); };
    }
    var cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, escapable = /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, gap, indent, meta = { '\b': '\\b', '\t': '\\t', '\n': '\\n', '\f': '\\f', '\r': '\\r', '"': '\\"', '\\': '\\\\' }, rep; function quote(string) { escapable.lastIndex = 0; return escapable.test(string) ? '"' + string.replace(escapable, function (a) { var c = meta[a]; return typeof c === 'string' ? c : '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4); }) + '"' : '"' + string + '"'; }
    function str(key, holder) {
        var i, k, v, length, mind = gap, partial, value = holder[key]; if (value && typeof value === 'object' && typeof value.toJSON === 'function') { value = value.toJSON(key); }
        if (typeof rep === 'function') { value = rep.call(holder, key, value); }
        switch (typeof value) {
            case 'string': return quote(value); case 'number': return isFinite(value) ? String(value) : 'null'; case 'boolean': case 'null': return String(value); case 'object': if (!value) { return 'null'; }
                gap += indent; partial = []; if (Object.prototype.toString.apply(value) === '[object Array]') {
                    length = value.length; for (i = 0; i < length; i += 1) { partial[i] = str(i, value) || 'null'; }
                    v = partial.length === 0 ? '[]' : gap ? '[\n' + gap +
partial.join(',\n' + gap) + '\n' +
mind + ']' : '[' + partial.join(',') + ']'; gap = mind; return v;
                }
                if (rep && typeof rep === 'object') { length = rep.length; for (i = 0; i < length; i += 1) { k = rep[i]; if (typeof k === 'string') { v = str(k, value); if (v) { partial.push(quote(k) + (gap ? ': ' : ':') + v); } } } } else { for (k in value) { if (Object.hasOwnProperty.call(value, k)) { v = str(k, value); if (v) { partial.push(quote(k) + (gap ? ': ' : ':') + v); } } } }
                v = partial.length === 0 ? '{}' : gap ? '{\n' + gap + partial.join(',\n' + gap) + '\n' +
mind + '}' : '{' + partial.join(',') + '}'; gap = mind; return v;
        }
    }
    if (typeof Telerik.Sitefinity.JSON.stringify !== 'function') {
        Telerik.Sitefinity.JSON.stringify = function (value, replacer, space) {
            var i; gap = ''; indent = ''; if (typeof space === 'number') { for (i = 0; i < space; i += 1) { indent += ' '; } } else if (typeof space === 'string') { indent = space; }
            rep = replacer; if (replacer && typeof replacer !== 'function' && (typeof replacer !== 'object' || typeof replacer.length !== 'number')) { throw new Error('JSON.stringify'); }
            return str('', { '': value });
        };
    }
    if (typeof Telerik.Sitefinity.JSON.parse !== 'function') {
        Telerik.Sitefinity.JSON.parse = function (text, reviver) {
            var j; function walk(holder, key) {
                var k, v, value = holder[key]; if (value && typeof value === 'object') { for (k in value) { if (Object.hasOwnProperty.call(value, k)) { v = walk(value, k); if (v !== undefined) { value[k] = v; } else { delete value[k]; } } } }
                return reviver.call(holder, key, value);
            }
            text = String(text); cx.lastIndex = 0; if (cx.test(text)) {
                text = text.replace(cx, function (a) {
                    return '\\u' +
('0000' + a.charCodeAt(0).toString(16)).slice(-4);
                });
            }
            if (/^[\],:{}\s]*$/.test(text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, '@').replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) { j = eval('(' + text + ')'); return typeof reviver === 'function' ? walk({ '': j }, '') : j; }
            throw new SyntaxError('JSON.parse');
        };
    }
} ());

// ------------------------------------------------------------------------
//                          Event agrument classes
// ------------------------------------------------------------------------

// ************************** Command event args **************************
Telerik.Sitefinity.CommandEventArgs = function (commandName, commandArgument, contextBag) {
    this._commandName = commandName;
    this._commandArgument = commandArgument;
    this._contextBag = contextBag;
    this._cancel = null;
    Telerik.Sitefinity.CommandEventArgs.initializeBase(this);
}

Telerik.Sitefinity.CommandEventArgs.prototype = {

    // ************************** Set up and tear down **************************
    initialize: function () {
        Telerik.Sitefinity.CommandEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.CommandEventArgs.callBaseMethod(this, 'dispose');
    },
    // ************************** Properties **************************
    get_commandName: function () {
        return this._commandName;
    },
    get_commandArgument: function () {
        return this._commandArgument;
    },
    get_contextBag: function () {
        return this._contextBag;
    },
    get_cancel: function () {
        return this._cancel;
    },
    set_cancel: function (value) {
        this._cancel = value;
    }
};
Telerik.Sitefinity.CommandEventArgs.registerClass('Telerik.Sitefinity.CommandEventArgs', Sys.CancelEventArgs);

// ************************** Workflow dialog closed event args **************************
Telerik.Sitefinity.WorkflowDialogClosedEventArgs = function (commandName, operationName, contextBag, executeCommandOnClose) {
    this._commandName = commandName;
    this._operationName = operationName;
    this._contextBag = contextBag;
    this._executeCommandOnClose = executeCommandOnClose;
    this._cancel = null;
 

    Telerik.Sitefinity.WorkflowDialogClosedEventArgs.initializeBase(this);
}

Telerik.Sitefinity.WorkflowDialogClosedEventArgs.prototype = {

    // ************************** Set up and tear down **************************
    initialize: function () {
        Telerik.Sitefinity.WorkflowDialogClosedEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.WorkflowDialogClosedEventArgs.callBaseMethod(this, 'dispose');
    },
    // ************************** Properties **************************
    get_commandName: function () {
        return this._commandName;
    },
    get_operationName: function () {
        return this._operationName;
    },
    get_contextBag: function () {
        return this._contextBag;
    },
    //Gets a flag that specifies explicitly if command should be executed when worklfow dialog is closed with WorkflowDialogClosedEventArgs.
    get_executeCommandOnClose: function(){
        return this._executeCommandOnClose;
    },
    get_cancel: function () {
        return this._cancel;
    },
    set_cancel: function (value) {
        this._cancel = value;
    }
};
Telerik.Sitefinity.WorkflowDialogClosedEventArgs.registerClass('Telerik.Sitefinity.WorkflowDialogClosedEventArgs', Sys.CancelEventArgs);


// ************************** Prevent Command event args **************************
Telerik.Sitefinity.PreventCommandEventArgs = function (commandName, commandArgument) {
    Telerik.Sitefinity.PreventCommandEventArgs.initializeBase(this);
    this._cancel = false;
}
Telerik.Sitefinity.PreventCommandEventArgs.prototype = {
    get_cancel: function () { return this._cancel; },
    set_cancel: function (value) { this._cancel = value; }
}
Telerik.Sitefinity.PreventCommandEventArgs.registerClass("Telerik.Sitefinity.PreventCommandEventArgs", Telerik.Sitefinity.CommandEventArgs);

// ************************** Dialog closed event args **************************
Telerik.Sitefinity.DialogClosedEventArgs = function (isCreated, isUpdated, dataItem, context) {
    this._isCreated = isCreated;
    this._isUpdated = isUpdated;
    this._dataItem = dataItem;
    this._context = null;
    if (context) {
        this._context = context;
    }
    Telerik.Sitefinity.DialogClosedEventArgs.initializeBase(this);
}

Telerik.Sitefinity.DialogClosedEventArgs.prototype = {

    // ************************** Set up and tear down **************************
    initialize: function () {
        Telerik.Sitefinity.DialogClosedEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.DialogClosedEventArgs.callBaseMethod(this, 'dispose');
    },
    // ************************** Properties **************************
    get_isCreated: function () {
        return this._isCreated;
    },
    get_isUpdated: function () {
        return this._isUpdated;
    },
    get_dataItem: function () {
        return this._dataItem;
    },
    get_context: function () {
        return this._context;
    }
};

Telerik.Sitefinity.DialogClosedEventArgs.registerClass('Telerik.Sitefinity.DialogClosedEventArgs', Sys.CancelEventArgs);

// ************************** Form Created event args **************************
Telerik.Sitefinity.FormCreatedEventArgs = function (isNew, dataKey, dataItem, commandName, params, context, commandArgument) {
    this._isNew = isNew;
    this._dataKey = dataKey;
    this._dataItem = dataItem;
    this._commandName = commandName;
    this._params = params;
    this._context = null;
    if (context) {
        this._context = context;
    }
    this._commandArgument = commandArgument;
    Telerik.Sitefinity.FormCreatedEventArgs.initializeBase(this);
}

Telerik.Sitefinity.FormCreatedEventArgs.prototype = {

    // ************************** Set up and tear down **************************
    initialize: function () {
        Telerik.Sitefinity.FormCreatedEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.FormCreatedEventArgs.callBaseMethod(this, 'dispose');
    },
    // ************************** Properties **************************
    get_isNew: function () {
        return this._isNew;
    },
    get_dataKey: function () {
        return this._dataKey;
    },
    get_dataItem: function () {
        return this._dataItem;
    },
    get_commandName: function () {
        return this._commandName;
    },
    get_params: function () {
        return this._params;
    },
    get_context: function () {
        return this._context;
    },
    get_commandArgument: function () {
        return this._commandArgument;
    }
};
Telerik.Sitefinity.FormCreatedEventArgs.registerClass('Telerik.Sitefinity.FormCreatedEventArgs', Sys.EventArgs);


// ************************** Form closing event args - fired when a form dialog is getting closed, cancelable **************************
Telerik.Sitefinity.FormClosingEventArgs = function (isNew, isDirty, dataItem, context, commandArgument, commandName) {
    this._isNew = isNew;
    this._isDirty = isDirty;
    this._dataItem = dataItem;
    this._context = context;
    this._commandArgument = commandArgument;
    this._commandName = commandName;
    Telerik.Sitefinity.FormClosingEventArgs.initializeBase(this);
}

Telerik.Sitefinity.FormClosingEventArgs.prototype = {

    // ************************** Set up and tear down **************************
    initialize: function () {
        Telerik.Sitefinity.FormClosingEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.FormClosingEventArgs.callBaseMethod(this, 'dispose');
    },
    // ************************** Properties **************************
    get_isNew: function () {
        return this._isNew;
    },
    get_isDirty: function () {
        return this._isDirty;
    },
    get_dataItem: function () {
        return this._dataItem;
    },
    get_context: function () {
        return this._context;
    },
    get_commandArgument: function () {
        return this._commandArgument;
    },
    get_commandName: function () {
        return this._commandName;
    }
};
Telerik.Sitefinity.FormClosingEventArgs.registerClass('Telerik.Sitefinity.FormClosingEventArgs', Sys.CancelEventArgs);


String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/g, "");
}
String.prototype.ltrim = function () {
    return this.replace(/^\s+/, "");
}
String.prototype.rtrim = function () {
    return this.replace(/\s+$/, "");
}

String.prototype.utf8Encode = function () {
    return unescape(encodeURIComponent(this));
}

String.prototype.utf8Decode = function () {
    var res = "";
    try {
        res = decodeURIComponent(escape(this));
    }
    catch (err) {
        res = decodeURIComponent(this);
    }
    return res;
}

String.prototype.base64Encode = function () {
    var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    var output = "";
    var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
    var i = 0;

    input = this.utf8Encode();

    while (i < this.length) {
        chr1 = this.charCodeAt(i++);
        chr2 = this.charCodeAt(i++);
        chr3 = this.charCodeAt(i++);

        enc1 = chr1 >> 2;
        enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
        enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
        enc4 = chr3 & 63;

        if (isNaN(chr2)) {
            enc3 = enc4 = 64;
        } else if (isNaN(chr3)) {
            enc4 = 64;
        }

        output = output +
            keyStr.charAt(enc1) + keyStr.charAt(enc2) +
            keyStr.charAt(enc3) + keyStr.charAt(enc4);
    }

    return output;
}

String.prototype.htmlEncode = function () {
    var encoded = '',
        div = document.createElement('div');

    div.innerText = this;
    encoded = div.innerHTML;
    delete div;

    return encoded;
}

// ------------------------------------------------------------------------
//                          Internal functions
// ------------------------------------------------------------------------

Type.registerNamespace("Telerik.Sitefinity._Implementation");

Telerik.Sitefinity._Implementation.get_encodeWcfMap = function () {
    var encodeMap = new Array();
    encodeMap["."] = "__dot__"; // dot
    encodeMap["]"] = "__rsb__"; // right square bracket
    encodeMap["["] = "__lsb__"; // left square bracket
    encodeMap["}"] = "__rcb__"; // right curly bracket
    encodeMap["{"] = "__lcb__"; // left curly bracket
    encodeMap["`"] = "__gr__";  // grave
    encodeMap[","] = "__cm__";  // comma
    encodeMap[" "] = "__sp__";  // space
    encodeMap["="] = "__eq__";  // equals 
    encodeMap["#"] = "__pd__";  // pound
    encodeMap["?"] = "__qm__";  // question mark
    encodeMap[":"] = "__cl__";  // colon
    return encodeMap;
}

Telerik.Sitefinity._Implementation.get_decodeWcfMap = function () {
    var encodeMap = get_encodeWcfMap();
    var decodeMap = new Array();
    for (var codeEntry in encodeMap) {
        decodeMap[encodeMap[codeEntry]] = codeEntry;
    }
    return decodeMap;
};

if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (elt /*, from*/) {
        var len = this.length;

        var from = Number(arguments[1]) || 0;
        from = (from < 0) ? Math.ceil(from) : Math.floor(from);
        if (from < 0)
            from += len;

        for (; from < len; from++) {
            if (from in this &&
                        this[from] === elt)
                return from;
        }
        return -1;
    };
}

//fixes an issue with RadWindowManager since TWU Q2.SP1
if (typeof jQuery !== 'undefined') {
    jQuery(document).ready(function () {
        if (Telerik && Telerik.Web && Telerik.Web.UI && Telerik.Web.UI.RadWindowManager) {
            Telerik.Web.UI.RadWindowManager.prototype._ensureFirstManagerIsRegistered = function () { };
        }
    });
}

if (typeof jQuery !== 'undefined') {
    jQuery(document).ready(function () {
        if (typeof (jQuery.cookie) === 'function') {
            jQuery.cookie('sf_timezoneoffset', new Date().getTimezoneOffset(), { path: '/' });
        }
    });
}
