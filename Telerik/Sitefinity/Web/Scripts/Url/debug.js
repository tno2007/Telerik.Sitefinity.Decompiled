var Url = {

    // public method for url encoding   
    encode: function (string) {
        return this._urlEncode(this._utf8Encode(string));
    },

    // public method for url decoding   
    decode: function (string) {
        return this._utf8Decode(this._urlDecode(string));
    },

    _urlEncode: function (clearString) {
        var output = '';
        var x = 0;
        clearString = clearString.toString();
        var regex = /(^[a-zA-Z0-9_.]*)/;
        while (x < clearString.length) {
            var match = regex.exec(clearString.substr(x));
            if (match != null && match.length > 1 && match[1] != '') {
                output += match[1];
                x += match[1].length;
            } else {
                if (clearString[x] == ' ')
                    output += '+';
                else {
                    var charCode = clearString.charCodeAt(x);
                    var hexVal = charCode.toString(16);
                    output += '%' + (hexVal.length < 2 ? '0' : '') + hexVal.toUpperCase();
                }
                x++;
            }
        }
        return output;
    },

    _urlDecode: function (encodedString) {
        var output = encodedString;
        var binVal, thisString;
        var myregexp = /(%[^%]{2})/;
        while ((match = myregexp.exec(output)) != null
                 && match.length > 1
                 && match[1] != '') {
            binVal = parseInt(match[1].substr(1), 16);
            thisString = String.fromCharCode(binVal);
            output = output.replace(match[1], thisString);
        }
        return output;
    },

    // private method for UTF-8 encoding   
    _utf8Encode: function (string) {
        var utftext = "";
        if (string) {
            string = string.replace(/\r\n/g, "\n");

            for (var n = 0; n < string.length; n++) {

                var c = string.charCodeAt(n);

                if (c < 128) {
                    utftext += String.fromCharCode(c);
                }
                else if ((c > 127) && (c < 2048)) {
                    utftext += String.fromCharCode((c >> 6) | 192);
                    utftext += String.fromCharCode((c & 63) | 128);
                }
                else {
                    utftext += String.fromCharCode((c >> 12) | 224);
                    utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                    utftext += String.fromCharCode((c & 63) | 128);
                }
            }
        }
        return utftext;
    },

    // private method for UTF-8 decoding   
    _utf8Decode: function (utftext) {
        var string = "";
        var i = 0;
        var c = c1 = c2 = 0;

        if (utftext) {
            while (i < utftext.length) {

                c = utftext.charCodeAt(i);

                if (c < 128) {
                    string += String.fromCharCode(c);
                    i++;
                }
                else if ((c > 191) && (c < 224)) {
                    c2 = utftext.charCodeAt(i + 1);
                    string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                    i += 2;
                }
                else {
                    c2 = utftext.charCodeAt(i + 1);
                    c3 = utftext.charCodeAt(i + 2);
                    string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                    i += 3;
                }
            }
        }
        return string;
    }
};