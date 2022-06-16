var detailFormView = null;

// called by the DetailFormView when it is loaded
function OnDetailViewLoaded(sender, args) {

    // the sender here is DetailFormView
    detailFormView = sender;
    resetForm();

    // wire up the behavior field
    var behaviorField = findFieldControl("BehaviorField");
    behaviorField.add_valueChanged(function () {
        if (behaviorField.get_value() == "TransformLayout") {
            switchToLayoutTransformMode();
        } else {
            switchToMiniSiteMode();
        }
    });
}

function resetForm() {
    switchToLayoutTransformMode();    
}

function switchToLayoutTransformMode() {
    // hide the SiteSelection section
    var siteSelectionSection = findSection("SiteSelection");
    $(siteSelectionSection.get_element()).hide();
    // show css selection section
    var cssSelectorSection = findSection("CssSelector");
    $(cssSelectorSection.get_element()).show();
    // show layout transformation section
    var layoutTransformationSection = findSection("LayoutTransformationSection");
    $(layoutTransformationSection.get_element()).show();
}

function switchToMiniSiteMode() {
    // show the SiteSelection section
    var siteSelectionSection = findSection("SiteSelection");
    $(siteSelectionSection.get_element()).show();
    // hide css selection section
    var cssSelectorSection = findSection("CssSelector");
    $(cssSelectorSection.get_element()).hide();
    // hide layout transformation section
    var layoutTransformationSection = findSection("LayoutTransformationSection");
    $(layoutTransformationSection.get_element()).hide();
}

function findSection(sectionName) {
    var section = null;
    var sectionIds = detailFormView.get_sectionIds();
    for (var s = 0; s < sectionIds.length; s++) {
        section = $find(sectionIds[s]);
        if (section && section.get_name() == sectionName) {
            return section;
        }
    }
    return section;
}

function findFieldControl(fieldControlName) {
    var fieldControl = null;
    var fieldControlsIds = detailFormView.get_fieldControlIds();
    for (var f = 0; f < fieldControlsIds.length; f++) {
        fieldControl = $find(fieldControlsIds[f]);
        if (fieldControl && fieldControl.get_fieldName() == fieldControlName) {
            return fieldControl;
        }
    }
    return fieldControl;
}