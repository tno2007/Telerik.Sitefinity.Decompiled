/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI");

//event args
Telerik.Sitefinity.Web.UI.TagsContainerEventArgs = function(tagText, tagId) {
    if (arguments.length <= 1 || arguments.length > 2) {
        throw Error.parameterCount();
    }

    Telerik.Sitefinity.Web.UI.TagsContainerEventArgs.initializeBase(this);
    this._tagText = tagText;
    this._tagId = tagId;

}

Telerik.Sitefinity.Web.UI.TagsContainerEventArgs.prototype = {
    get_tagText: function() {
        if (arguments.length !== 0) throw Error.parameterCount();
        return this._tagText;
    },
    get_tagId: function() {
        if (arguments.length !== 0) throw Error.parameterCount();
        return this._tagId;
    }
}
Telerik.Sitefinity.Web.UI.TagsContainerEventArgs.registerClass('Telerik.Sitefinity.Web.UI.TagsContainerEventArgs', Sys.EventArgs);

//component
Telerik.Sitefinity.Web.UI.TagsContainerExtender = function(element) {
    Telerik.Sitefinity.Web.UI.TagsContainerExtender.initializeBase(this, [element]);
    this.jQueryControl = {};
    this._categoriesListCSS = null;
    var me = null;
}

Telerik.Sitefinity.Web.UI.TagsContainerExtender.prototype = {
    initialize: function() {
        Telerik.Sitefinity.Web.UI.TagsContainerExtender.callBaseMethod(this, 'initialize');
        me = this.get_element();
        this.jQueryControl = jQuery(me).TagsContainerExtender({ "component": this, "categoriesListCSS": this._categoriesListCSS });
    },

    dispose: function() {
        $clearHandlers(this.get_element());
        Telerik.Sitefinity.Web.UI.TagsContainerExtender.callBaseMethod(this, 'dispose');
    },

    addTag: function(tagText, tagId) {
        this.jQueryControl.addTag(tagText, tagId);
    },

    removeTag: function(tagText, tagId) {
        this.jQueryControl.removeTag(tagText, tagId);
        //the event is raised withing the jquery control
        //this._raiseTagRemovedEvent(removedTagText, removedTagId);
    },

    clear: function() {
        this.jQueryControl.clear();
    },

    add_tagRemoved: function(handler) {
        this.get_events().addHandler("tagRemoved", handler);
    },

    remove_tagRemoved: function(handler) {
        this.get_events().removeHandler('tagRemoved', handler);
    },

    _raiseTagRemovedEvent: function(text, id) {
        var handler = this.get_events().getHandler('tagRemoved');
        if (handler) {
            handler(this, new Telerik.Sitefinity.Web.UI.TagsContainerEventArgs(text, id));
        }
    }
}
Telerik.Sitefinity.Web.UI.TagsContainerExtender.registerClass('Telerik.Sitefinity.Web.UI.TagsContainerExtender', Sys.UI.Behavior);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();


// plugin
(function(jQuery) {

    //plugin imlementation
    jQuery.fn.TagsContainerExtender = function(options) {

        var globalSettings = {
            "tagsContainer": "ul",
            "tagWrap": "li",
            "tagContents": "span",
            "relAttr": "tagValue",
            "closeElement": "&nbsp;<a href='javascript:void(0)' class='sfRemoveBtn'>[x]</a>",
            "component": {},
            "categoriesListCSS": ""
        }

        //combine global and options, keeping the options
        var settings = jQuery.extend(globalSettings, options);
        //members
        var _tagsPanel = jQuery(this);
        var me = this;
        var _saveTagsButton;
        var _tagsElement;
        var _component;

        //methods
        var initialize = function() {
            _component = settings.component;
            //_tagsPanel.hide();
            
            //tries to find already rendered tags
            // and creates an empty
            _tagsPanel.addClass(settings.categoriesListCSS);
            _tagsElement = _tagsPanel.find(settings.tagsContainer);
            if (_tagsElement.length == 0) {
                _tagsElement = jQuery("<" + settings.tagsContainer + "/>");

                if ((t = _tagsPanel.find(":first")).length > 0) {
                    t.append(_tagsElement);
                }
                else {
                    _tagsPanel.append(_tagsElement);
                }
            }
        }

        this.addTag = function(tagText, tagId) {
            if (this.findTagById(tagId)) {

                return;
            }
            if (this.findTagByText(tagText)) {

                return;
            }

            var el = this.getTagElement(tagText, tagId);
            _tagsElement.append(el);
            _tagsPanel.show();
//            _tagsPanel.attr("display", "block");
//            _tagsPanel.parents().show();
//            console.log("_tagsPanel display:" + _tagsPanel.attr('display'));
//            console.log("_tagsPanel visible:" + _tagsPanel.is(':visible'));
//            console.log("_tagsPanel hidden parents:" + _tagsPanel.parents(':hidden').length);
//            _tagsPanel.parents(':hidden').each(function() {
//                console.log("parent: " + this.id);
//            });

        }

        this.setTags = function(categoriesArray) {
            for (var i = 0; i < categoriesArray.length; i++) {
                this.addTag(categoriesArray[i].text, categoriesArray[i].value);
            }
        }

        //returns the current tags
        this.getTagElements = function() {
            return _tagsElement.find(settings.tagWrap + "[rel='" + settings.relAttr + "']");
        }

        this.findTagByText = function(tagText) {
            if (tagText == null) {
                return null;
            }
            var res = null;
            this.getTagElements().each(function() {
                if (jQuery(this).data("tag").text == tagText) {
                    res = jQuery(this);
                }
            });
            return res;
        }

        this.findTagById = function(tagId) {
            if (tagId == null) {
                return null;
            }
            var res = null;
            this.getTagElements().each(function() {
                if (jQuery(this).data("tag").Id == tagId) {
                    res = jQuery(this);
                }
            });
            return res;
        }

        //clears all tags
        this.clear = function() {
            this.getTagElements().remove();
            this.hide();
        }

        //builds the element
        this.getTagElement = function(tagText, tagId) {

            var item = jQuery("<" + settings.tagWrap + "/>");
            if (this.getTagElements().length == 0) {
                item.addClass("first");
            }

            item.attr("rel", settings.relAttr);
            item.data("tag", { "text": tagText, "Id": tagId });
            var close = jQuery(settings.closeElement).click(function(e) {
                me.removeTag(tagText, tagId);
            });
            //close.css("cursor", "pointer");
            var contents = jQuery("<" + settings.tagContents + " />");

            contents.text(tagText);
            contents.append(close); //add  the remove close/button
            return item.append(contents);
        }

        this.removeTag = function(tagText, tagId) {
            var el = this.findTagById(tagId);

            if (el) {
                el.remove();
            }
            else {
                el = this.findTagByText(tagText);
                if (el) {
                    el.remove();
                }
            }
            if (this.getTagElements().length == 0) {
                this.hide();
            }
            _component._raiseTagRemovedEvent(tagText, tagId);
        }


        initialize();
        return this;
    }

})(jQuery)