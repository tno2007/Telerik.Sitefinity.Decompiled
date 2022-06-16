Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework");

/* SitefinityWizard class */

Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizardStepControl = function (element) {
    this._autoSaved = true;
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizardStepControl.initializeBase(this, [element]);
}
Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizardStepControl.prototype = {

    // set up 
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizardStepControl.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizardStepControl.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */
    isValid: function () {
        alert('This method must be implemented in the concrete implementation of the SitefinityWizardStepControl abstract class.');
    },

    show: function () {

    },

    reset: function() {

    },

    /* *************************** private methods *************************** */

    /* *************************** properties *************************** */
    get_autoSaved: function () {
        return this._autoSaved;
    },
    set_autoSaved: function (value) {
        this._autoSaved = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizardStepControl.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizardStepControl', Sys.UI.Control);