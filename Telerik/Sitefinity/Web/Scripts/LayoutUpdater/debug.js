var LayoutUpdater = function () {
    this._placeholderLabelsAttributeName = "data-placeholder-label";
    this._labelClass = "zeDockZoneLabel";
    this._placeholderHasLabel = "zeDockZoneHasLabel";
    this._draggingTextClass = "emptyZoneDraggingText";

    if (LayoutUpdater._instance) {
        //this allows the constructor to be called multiple times
        //and refer to the same instance. Another option is to
        //throw an error.
        return LayoutUpdater._instance;
    }

    LayoutUpdater._instance = this;
};

LayoutUpdater.getInstance = function () {
    return LayoutUpdater._instance || new LayoutUpdater();
}

LayoutUpdater.prototype = {
    getLabelFromOuterColumn: function (outerColumn) {
        return outerColumn.attr(this._placeholderLabelsAttributeName);
    },

    updatePlaceholderLabels: function (outerColumn, label) {
        var zone = outerColumn.find('.RadDockZone').first();
        var hiddenPlaceholder = zone.children(".RadDock.rdPlaceHolder:first");
        this._updateOuterColumnAttributeAndLabelDiv(zone, outerColumn, label, hiddenPlaceholder);
    },

    initializeZoneUI: function (zone, zoneEditor) {
        var hiddenPlaceholder = zone.children(".RadDock.rdPlaceHolder:first");

        hiddenPlaceholder.empty();

        var emptyTemplateHtml = '<div class="emptyZoneDragWidget">' + zoneEditor._getLocalizedMessage("ZoneEditorEmptyZoneContentDragMessage") + '</div>' +
             '<div class="emptyZoneDragLayout">' + '<p>' + zoneEditor._getLocalizedMessage("ZoneEditorEmptyZoneLayoutCaption") + '</p>' + zoneEditor._getLocalizedMessage("ZoneEditorEmptyZoneLayoutDragMessage") + '</div>';

        var dragginTextDiv = '<div class="' + this._draggingTextClass + '">' + zoneEditor._getLocalizedMessage("ZoneEditorEmptyZoneDraggingText") + '</div>';

        // Add control in a reverse order - first add the empty template labels that should be at the bottom
        zone.prepend(emptyTemplateHtml);

        var outerColumn = zone.closest("[" + this._placeholderLabelsAttributeName + "]");
        //Check if jQuery found a label element and use it for the label displaying the name of the placeholder.
        if (outerColumn.length > 0) {
            var label = this.getLabelFromOuterColumn(outerColumn);
            this._addLabel(zone, hiddenPlaceholder, label);

        } else {  // If there is no label attribute, there must be a placeholderId to use.
            var outerColumnByClass = zone.closest(".sf_colsOut");
            // If there is no outer column - we are in a master page file
            if (outerColumnByClass.length === 0) {
                var placeholderLabel = zone.attr(this._placeholderLabelsAttributeName);
                if (!placeholderLabel) {
                    // if no label is set, placeholderId is used
                    placeholderLabel = zone.attr("placeholderid");
                }
                if (placeholderLabel) {
                    //if label or id was found - show it
                    this._addLabel(zone, hiddenPlaceholder, placeholderLabel);
                }
            }
        }

        // 'Drop here' label should be above the placeholder label (if any)
        // It is added at 2 places - inside the zone and there is a hidden placeholder when there is already a widget added
        zone.prepend(dragginTextDiv);
        hiddenPlaceholder.prepend(dragginTextDiv);
    },

    _updateOuterColumnAttributeAndLabelDiv: function (zone, outerColumn, label, hiddenPlaceholder) {
        // Set the custom attribute with the label value (attribute is always set, regardless of the label value)
        outerColumn.attr(this._placeholderLabelsAttributeName, label);

        this._updateLabelDiv(zone, label, hiddenPlaceholder);
    },

    _addLabel: function (zone, hiddenPlaceholder, placeholderLabel) {
        if (placeholderLabel) {
            // Makes sure that the label is not interpreted by the browser as HTML
            placeholderLabel = this._htmlEncode(placeholderLabel);

            zone.prepend(this._getPlaceholderLabelHtml(placeholderLabel));
            hiddenPlaceholder.prepend(this._getPlaceholderLabelHtml(placeholderLabel));
            zone[0].className += " " + this._placeholderHasLabel;
        }
    },

    _updateLabelDiv: function (zone, placeholderLabel, hiddenPlaceholder) {
        // Update the div with the label only if it has a value
        if (placeholderLabel) {
            // Makes sure that the label is not interpreted by the browser as HTML
            placeholderLabel = this._htmlEncode(placeholderLabel);

            // Select the dragging text elements 'Drop here'. 
            // There should be 2 elements with this class - one inside the zone and one inside the div with rdPlaceHolder class
            var zoneDraggingTextElement = jQuery("#" + zone.attr("id") + " > ." + this._draggingTextClass + ":first");
            var hiddenPlaceholderDraggingElement = jQuery("#" + zone.attr("id") + " > .rdPlaceHolder > ." + this._draggingTextClass + ":first");

            // Change label html if exists
            if (zone.children("." + this._labelClass + ":first").length > 0) {
                zone.children("." + this._labelClass + ":first").html('<b>' + placeholderLabel + '</b>');
                hiddenPlaceholder.children("." + this._labelClass + ":first").html('<b>' + placeholderLabel + '</b>');
            } else {
                var placeholderLabelHtml = '<div class="' + this._labelClass + '"><b>' + placeholderLabel + '</b>';

                if (zoneDraggingTextElement) {
                    zoneDraggingTextElement.after(placeholderLabelHtml);
                }

                if (hiddenPlaceholderDraggingElement) {
                    hiddenPlaceholderDraggingElement.after(placeholderLabelHtml);
                }
            }

            zone[0].className += " " + this._placeholderHasLabel;
        } else {
            jQuery(zone[0]).removeClass(this._placeholderHasLabel);
            zone.children("." + this._labelClass + ":first").remove();
            hiddenPlaceholder.children("." + this._labelClass + ":first").remove();
        }
    },

    _htmlEncode: function (input) {
        return String(input)
        .replace(/&/g, '&amp;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;');
    },

    _getPlaceholderLabelHtml: function (label) {
        return '<div class="' + this._labelClass + '"><b>' + label + '</b>';
    }
}