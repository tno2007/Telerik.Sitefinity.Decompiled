/**
* @name Sitefinity Templates
*
* @description
* This class provides templates for commonly used user interface
* elements in Sitefinity.
* 
*/

sitefinityTemplates = function() {
    // do nothing in the constructor for now
}

sitefinityTemplates.prototype = {

    /* columns object provides methods for generating templates
    * for common grid columns in Sitefinity grids
    */
    columns:
    {
        /* generates the row edit column */
        editColumn: function (settings) {
            return {
                title: settings.title,
                template: '<a href="#" data-command="edit" data-id="${ ' + settings.key + ' }">${ ' + settings.text + ' }</a>'
            };
        },

        /* generates the row delete column */
        deleteColumn: function (settings) {
            return {
                title: " ",
                template: '<a href="#" data-command="delete" data-id="${ ' + settings.key + ' }">Delete</a>'
            };
        },

        /* generates the actions menu column */
        actionsMenuColumn: function (actions) {

            var template = '<ul class="sfGridActionsMenu"><li class="sfActionMenu"><a href="#">Actions</a><ul>';
            for (var i = 0; i < actions.length; i++) {
                template += '<li>';
                template += '<a href="#" command-name="' + actions[i].command + '">' + actions[i].title + '</a>';
                template += '</li>';
            }

            template += '</li></ul></ul>'

            return {
                title: " ",
                template: template
            };
        }
    }
};

sf.templates = new sitefinityTemplates();