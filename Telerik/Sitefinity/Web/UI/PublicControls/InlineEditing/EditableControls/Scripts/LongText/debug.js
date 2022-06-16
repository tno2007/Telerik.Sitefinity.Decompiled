define(["EditableField", "ImageSelector", "ImageEditor", "HyperLinkManager", "DialogBase"], function (EditableField, ImageSelector, ImageEditor, HyperLinkManager, DialogBase) {
    
    function EditableLongText(options) {
        // Call the super constructor.
        EditableField.call(this, options);
        this.imageSelector = new ImageSelector({ siteBaseUrl: this.siteBaseUrl, culture: options.contentRepository.culture });
        this.imageEditorDialog = new DialogBase();
        this.imageEditor = new ImageEditor({ parentElement: this.imageEditorDialog.getContentPlaceHolder(), siteBaseUrl: this.siteBaseUrl, culture: options.contentRepository.culture });
        this.editorWindow = null;
        this.editor = null;
        this.imgTag = null;
        this.insertImageTag = false;
        this.hyperLinkManager = new HyperLinkManager({ siteBaseUrl: options.siteBaseUrl, culture: options.contentRepository.culture });
        this.editorRange = null;
        this.editorFormatter = null;
        var wrapped = jQuery('<div>').html(this.originalValue);
        wrapped.find('[data-sf-ignore]').remove();
        this.originalValue = jQuery.trim(wrapped.html());
        this.value = this.originalValue;
        this.editorMarker = null;
        this.editorRestoreSelectionFunc = null;

        // Return this object reference.
        return (this);
    }

    EditableLongText.prototype = {

        showEditMode: function () {
            if (!this.isInEdit) {
                var that = this;
                var isMoved = false;
                this.element.attr('contenteditable', true);

                this.editor = this.element.kendoEditor({
                    tools: [
                        "formatting",
                        "bold",
                        "italic",
                        "underline",
                        "justifyLeft",
                        "justifyCenter",
                        "justifyRight",
                        "justifyFull",
                        "insertOrderedList",
                        "insertUnorderedList",
                        "indent",
                        "outdent",
                        {
                            name: "insertImage",
                            tooltip: "Insert image",
                            exec: function (e) {
                                e.preventDefault();
                                e.stopPropagation();
                                var editor = $(this).data("kendoEditor");
                                that.editorRange = editor.getRange();
                                that.imgTag = $(editor.selectedHtml());
                                if (that.imgTag.is('img')) {
                                    //open image editor
                                    that.imageEditor.refresh(that.imgTag);
                                    that.imageEditorDialog.open();
                                } else {
                                    //select an image
                                    that.imageSelector.show();
                                }
                            }
                        },
                        {
                            name: "createLink",
                            tooltip: "Select link",
                            exec: function (e) {
                                e.preventDefault();
                                e.stopPropagation();
                                var editor = that.element.data("kendoEditor");
                                that.editorRange = editor.getRange();
                                that.editorFormatter = new kendo.ui.editor.LinkFormatter();

                                that.editorMarker = new kendo.ui.editor.Marker();
                                that.editorRange = that.editorMarker.add(that.editorRange, true);

                                that.editorRestoreSelectionFunc = that.editor.restoreSelection;
                                that.editor.restoreSelection = function () { }

                                var nodes = kendo.ui.editor.RangeUtils.textNodes(that.editorRange);
                                var selectedNode = nodes.length ? that.editorFormatter.finder.findSuitable(nodes[0]) : null;
                                that.hyperLinkManager.show(selectedNode, nodes);
                                $(that.hyperLinkManager).one("linkSelected", jQuery.proxy(that.linkManagerLinkSelected, that));
                            }
                        },
                        "unlink",
                        "createTable",
                        "addRowAbove",
                        "addRowBelow",
                        "addColumnLeft",
                        "addColumnRight",
                        "deleteRow",
                        "deleteColumn",
                        "viewHtml"                        
                    ],
                    execute: function (e) {
                        if (e.name === 'viewhtml') {
                            $(that.editor.element).children().filter(function (index, item) { return $(item).attr('data-sf-ignore') !== undefined }).remove();
                        }
                    }
                }).data('kendoEditor');

                this.editorWindow = this.editor.toolbar.element.parent().data("kendoWindow");
                this.editorWindow.setOptions({
                    animation: {
                        close: false,
                        open: false
                    }
                });
                    
                this.editorWindow.bind('close', function (e) {
                    $(document.body).removeClass("sfEditorToolbarShown");
                    if (isMoved) {
                        $(document.body).animate({ 'margin-top': '-=45' }, 200);
                        isMoved = false;
                    }
                });

                this.editorWindow.bind('open', function (e) {
                    that.editorWindow.element.parent().css({
                        top: 43,
                        position: "fixed",
                        width: "100%", padding: 0, border: 0, left: 0,
                        display: ''
                    })
                    $(document.body).addClass("sfEditorToolbarShown");
                    if (that.element.position().top < 89) {
                        isMoved = true;
                        $(document.body).animate({ 'margin-top': '+=45' }, 200);
                    }
                    else {
                        isMoved = false;
                    }
                });
                
                
                $(this.editorWindow.wrapper).addClass("sfEditorToolbarWrp");

                //Drag functionality cannot be disabled for kendoEditor yet, so destroy and remove the button.
                var draggableElement = this.editorWindow.wrapper.data("kendoDraggable");
                if (draggableElement !== undefined) {
                    draggableElement.destroy();
                }
                var dragButton = this.editorWindow.wrapper.find('button.k-button.k-bare.k-editortoolbar-dragHandle');
                if (dragButton) {
                    dragButton.remove();
                }

                this.isInEdit = true;
                
                $(this.imageSelector).on("doneSelected", jQuery.proxy(this.onImageSelected, this));
                $(this.imageSelector.dialog).on("closeSelected", jQuery.proxy(this.returnFocus, this));
                $(this.hyperLinkManager.dialog).on("closeSelected", jQuery.proxy(this.returnFocus, this));
                $(this.hyperLinkManager.dialog).on("doneSelected", jQuery.proxy(this.returnFocus, this));
                $(this.imageEditorDialog).on("doneSelected", jQuery.proxy(this.onImageEditorSelected, this));
                $(this.imageEditorDialog).on("closeSelected", function (event, sender) {
                    sender.close();
                });

                if (this.editor) {
                    $(this.editor.body).focusin();
                }
            }
        },

        unselectField: function () {
            $(this.element).removeClass('sfFieldEditMode').addClass('sfFieldEditable');
            if (this.editor) {
                $(this.editor.body).focusout();
            }
        },

        linkManagerLinkSelected: function (e, HyperLinkManager) {
            var linkAttributes = HyperLinkManager.getLinkAttributes();
            var kendoUIDomAttrFunc = kendo.ui.editor.Dom.attr;
            var sfDomAttrFunc = function (element, attributes) {
                var extendedElement = kendoUIDomAttrFunc(element, attributes);
                if (attributes.sfref && !extendedElement.attributes.sfref) {
                    extendedElement.setAttribute("sfref", attributes.sfref);
                }
                return extendedElement;
            }

            kendo.ui.editor.Dom.attr = sfDomAttrFunc;
            this.editorFormatter.apply(this.editorRange, linkAttributes);
            kendo.ui.editor.Dom.attr = kendoUIDomAttrFunc;
            this.editor.restoreSelection = this.editorRestoreSelectionFunc;
        },
        returnFocus: function(event, sender) {
            this.editor.selectRange(this.editorRange);
        },
        disableEditing: function () {
            this.element.removeAttr('contenteditable');
            if (this.editorWindow) {
                this.editorWindow.unbind('open');
                this.editorWindow.unbind('close');
            }
        },

        isChanged: function () {

            if (this.isInEdit) {
                this.value = this.element.html();
                var container = $('<div>').html(this.value);
                if (container.find('[data-sf-ignore]').remove().length > 0) {
                    this.value = container.html();
                }
                this.value = $.trim(this.value);
            }
            return this.value !== this.originalValue;
        },

        onImageSelected: function (event, selectedImages) {
            var selectedImage = selectedImages[0] || selectedImages;
            this.imgTag = this.imageEditor.formatImgTag($(document.createElement('img')), selectedImage);
            this.imageEditor.refresh(this.imgTag);
            this.imageEditorDialog.open();
            this.insertImageTag = true;
        },

        onImageEditorSelected: function (event, sender) {
            var selectedImage = this.imageEditor.getSelectedImage();
            this.updateImgTag(selectedImage);
            sender.close();
        },

        updateImgTag: function (imageItem) {
            var oldHtmlSfref = this.imgTag.attr('sfref');
            this.imgTag = this.imageEditor.formatImgTag(this.imgTag, imageItem);

            if (this.insertImageTag) {
                this.editor.exec("inserthtml", { value: this.imgTag[0].outerHTML });
            } else {
                
                var formattedImgTag = this.imageEditor.formatImgTag(this.imgTag, imageItem);
                var attributeSelector = String.format('[sfref="{0}"]', oldHtmlSfref);
                var oldTag = $(this.editor.body).find(attributeSelector);
                $(oldTag).replaceWith(formattedImgTag);
            }
        }
    };
    EditableLongText.prototype = $.extend(Object.create(EditableField.prototype), EditableLongText.prototype);
    return (EditableLongText);
});