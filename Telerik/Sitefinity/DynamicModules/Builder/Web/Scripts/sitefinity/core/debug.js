var sf = {};

/* This type represents the single entry namespace to Sitefinity jQuery extensions */
(function ($) {

    $.fn.sf = function () {

        /* sitefinity form plugin */
        this.form = function (options) {
            var sfForm = this.data("sfForm");
            if(!sfForm) {
                sfForm = new sitefinityForm(this, options);
                sfForm.initialize();
                this.data("sfForm", sfForm);
            }
            return sfForm;
        };

        return this;
    };

})(jQuery);

/* Extensions to JavaScript */

/* Array extensions */

/**
* @name Array.findByKey(keyProp, keyValue)
*
* This method extends JavaScript array by finding the first item 
* that has a property with specified value. If no object is found, returns null.
* This method is useful for working with objects that have unique keys.
*  
* @exampleTitle: How to find the first object in array with given property
* @example
* var people = [ 
*     { 
*         "FirstName" : "Ivan",
*         "LastName" : "Osmak"
*     },
*     {
*         "FirstName" : "Boyan",
*         "LastName" : "Rabchev"
*     },
*     {
*         "FirstName" : "Ivan",
*         "LastName" : "Pelovski"
*     }
*  ];
*
*  // returns Ivan Osmak, as this is the first object where property name "FirstName" equals to "Ivan"
*  var person = people.findByKey("FirstName", "Ivan");
*/

Array.prototype.findByKey = function (keyProp, keyValue) {
    var count = this.length;
    while (count--) {
        if (this[count][keyProp] == keyValue) {
            return this[count];
        }
    }

    return null;
};

// Firefox's Array.prototype.find - https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/find
if (Array.prototype.find == null) {
    Array.prototype.find = function (callback, thisObject) {
        for (var i = 0, len = this.length; i < len; i++) {
            if (callback.call(thisObject, this[i], i, this)) {
                return this[i];
            }
        }

        return undefined;
    };
}

/**
* @name Array.remove(property, value)
*
* This method extends JavaScript array by removing the first item 
* that has a property with specified value. If no object is found, no objects are removed.
* This method is useful for working with objects that have unique keys.
* This method affects the array!
*  
* @exampleTitle: How to remove the first object in array with given property
* @example
* var people = [ 
*     { 
*         "FirstName" : "Ivan",
*         "LastName" : "Osmak"
*     },
*     {
*         "FirstName" : "Boyan",
*         "LastName" : "Rabchev"
*     },
*     {
*         "FirstName" : "Ivan",
*         "LastName" : "Pelovski"
*     }
*  ];
*
*  // removes "Ivan Osmak" from the array
*  people.remove("FirstName", "Ivan");
*  
*/
if (Array.prototype.remove == null) {
    Array.prototype.remove = function (property, value) {

        var count = this.length;
        while (count--) {
            if (this[count][property] == value) {
                this.splice(count, 1);
            }
        }
    }
}