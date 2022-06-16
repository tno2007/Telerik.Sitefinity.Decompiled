function get_encodeWcfMap() {
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
    encodeMap["%"] = "__pct__";  // percent
    return encodeMap;
}

function get_decodeWcfMap() {
    var encodeMap = get_encodeWcfMap();
    var decodeMap = new Array();
    for (var codeEntry in encodeMap) {
        decodeMap[encodeMap[codeEntry]] = codeEntry;
    }
    return decodeMap;
}

function encodeWcfString(wcfString) {
    if (typeof (wcfString) == typeof ("") && wcfString != null && wcfString.length > 0) {
        var encoded = wcfString;
        var encodeMap = get_encodeWcfMap();
        for (var codeSymbol in encodeMap) {
            encoded = encoded.replace(codeSymbol, encodeMap[codeSymbol], "g");
        }
        return encoded;
    }
    else {
        return "";
    }
}

function decodeWcfString(wcfString) {
    if (typeof (wcfString) == typeof ("") && wcfString != null && wcfString.length > 0) {
        var decoded = wcfString;
        var decodeMap = get_decodeWcfMap();
        for (var decodeSymbol in decodeMap) {
            decoded = decoded.replace(decodeSymbol, decodeMap[decodeSymbol], "g");
        }
        return decoded;
    }
    else {
        return "";
    }
}