Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Fields");

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCheckboxes = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCheckboxes.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCheckboxes.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCheckboxes.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCheckboxes.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    //ChoiceField override to suuport making the multiple choices as coma separated values instead of list of values
    _get_selectedItemsValues: function () {
        var result = "";
        var tempStringArray = [];
        var selected = this._get_selectedListItemsElements();
        if (selected && selected.length > 0) {
            for (var i = 0, length = selected.length; i < length; i++) {
                tempStringArray.push(jQuery(selected[i]).attr("value"));
            }
            result = tempStringArray.join(',');
        }

        return result;
    },

    //ChoiceField override to support setting of comma separated values
    _get_listItemByValue: function (value) {
        var selector = this._get_listItemSelector();
        if (value.indexOf(",") != -1) {
            var values = value.split(",");
            var valuesLength = values.length;
            while (valuesLength--) {
                selector += "[value='" + this._cssEscape(values[valuesLength]) + "']"
                if (valuesLength > 0) {
                    selector += ",";
                }
            }
        }
        else {
            selector += "[value='" + this._cssEscape(value) + "']"
        }
        return jQuery(this._choiceElement).find(selector);
    },

    //added check for CSS.escape, since it's not supported in IE and Edge
    _cssEscape: function(value) {
		var result = '';
        if (window.CSS && window.CSS.escape) {
            result = CSS.escape(value);
        } else {
	        if (arguments.length == 0) {
			    throw new TypeError('`CSS.escape` requires an argument.');
		    }
		    var string = String(value);
		    var length = string.length;
		    var index = -1;
		    var codeUnit;
		    var firstCodeUnit = string.charCodeAt(0);
		    while (++index < length) {
			    codeUnit = string.charCodeAt(index);
			    // Note: there’s no need to special-case astral symbols, surrogate
			    // pairs, or lone surrogates.

			    // If the character is NULL (U+0000), then the REPLACEMENT CHARACTER
			    // (U+FFFD).
			    if (codeUnit == 0x0000) {
				    result += '\uFFFD';
				    continue;
			    }

			    if (
				    // If the character is in the range [\1-\1F] (U+0001 to U+001F) or is
				    // U+007F, […]
				    (codeUnit >= 0x0001 && codeUnit <= 0x001F) || codeUnit == 0x007F ||
				    // If the character is the first character and is in the range [0-9]
				    // (U+0030 to U+0039), […]
				    (index == 0 && codeUnit >= 0x0030 && codeUnit <= 0x0039) ||
				    // If the character is the second character and is in the range [0-9]
				    // (U+0030 to U+0039) and the first character is a `-` (U+002D), […]
				    (
					    index == 1 &&
					    codeUnit >= 0x0030 && codeUnit <= 0x0039 &&
					    firstCodeUnit == 0x002D
				    )
			    ) {
				    // https://drafts.csswg.org/cssom/#escape-a-character-as-code-point
				    result += '\\' + codeUnit.toString(16) + ' ';
				    continue;
			    }

			    if (
				    // If the character is the first character and is a `-` (U+002D), and
				    // there is no second character, […]
				    index == 0 &&
				    length == 1 &&
				    codeUnit == 0x002D
			    ) {
				    result += '\\' + string.charAt(index);
				    continue;
			    }

			    // If the character is not handled by one of the above rules and is
			    // greater than or equal to U+0080, is `-` (U+002D) or `_` (U+005F), or
			    // is in one of the ranges [0-9] (U+0030 to U+0039), [A-Z] (U+0041 to
			    // U+005A), or [a-z] (U+0061 to U+007A), […]
			    if (
				    codeUnit >= 0x0080 ||
				    codeUnit == 0x002D ||
				    codeUnit == 0x005F ||
				    codeUnit >= 0x0030 && codeUnit <= 0x0039 ||
				    codeUnit >= 0x0041 && codeUnit <= 0x005A ||
				    codeUnit >= 0x0061 && codeUnit <= 0x007A
			    ) {
				    // the character itself
				    result += string.charAt(index);
				    continue;
			    }

			    // Otherwise, the escaped character.
			    // https://drafts.csswg.org/cssom/#escape-a-character
			    result += '\\' + string.charAt(index);

		    }
       }
		    return result;
	}

    /* --------------------------------- properties -------------------------------------- */
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCheckboxes.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCheckboxes', Telerik.Sitefinity.Web.UI.Fields.ChoiceField);
