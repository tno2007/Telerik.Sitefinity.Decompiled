﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework");

/* SitefinityWizard class */

Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizard = function (element) {

    this._topPreviousButton = null;
    this._topNextButton = null;
    this._bottomPreviousButton = null;
    this._bottomNextButton = null;
    this._steps = null;
    this._currentStep = null;

    this._movePreviousDelegate = null;
    this._moveNextDelegate = null;

    Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizard.initializeBase(this, [element]);
}
Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizard.prototype = {

    // set up 
    initialize: function () {

        if (this._movePreviousDelegate === null) {
            this._movePreviousDelegate = Function.createDelegate(this, this._movePrevious);
        }

        if (this._moveNextDelegate == null) {
            this._moveNextDelegate = Function.createDelegate(this, this._moveNext);
        }

        if (this._pageChangedDelegate === null) {
            this._pageChangedDelegate = Function.createDelegate(this, this._pageChangedHandler);
        }

        if (this._topPreviousButton !== null) {
            $addHandler(this._topPreviousButton, 'click', this._movePreviousDelegate);
        }

        if (this._bottomPreviousButton !== null) {
            $addHandler(this._bottomPreviousButton, 'click', this._movePreviousDelegate);
        }

        if (this._topNextButton !== null) {
            $addHandler(this._topNextButton, 'click', this._moveNextDelegate);
        }

        if (this._bottomNextButton !== null) {
            $addHandler(this._bottomNextButton, 'click', this._moveNextDelegate);
        }

        this._prepareUI();
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizard.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {

        if (this._movePreviousDelegate) {
            delete this._movePreviousDelegate;
        }

        if (this._moveNextDelegate) {
            delete this._moveNextDelegate;
        }

        if (this._pageChangedDelegate) {
            delete this._pageChangedDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizard.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    add_pageChanged: function (delegate) {
        this.get_events().addHandler('pageChanged', delegate);
    },
    remove_pageChanged: function (delegate) {
        this.get_events().removeHandler('pageChanged', delegate);
    },

    goToFirstStep: function() {
        this._currentStep = this._steps[0];
        this._prepareUI();
    },

    /* *************************** private methods *************************** */

    _prepareUI: function () {

        if (this._currentStep === null) {
            this._currentStep = this._steps[0];
        }

        for (stepsIter = 0; stepsIter < this._steps.length; stepsIter++) {
            if (this._steps[stepsIter] != this._currentStep) {
                $('#' + this._steps[stepsIter]).hide();
            } else {
                $('#' + this._steps[stepsIter]).show();
            }
        }

        this._updatePager();
    },

    _updatePager: function () {
        if (this._topPreviousButton !== null) {
            $(this._topPreviousButton).hide();
        }

        if (this._topNextButton !== null) {
            $(this._topNextButton).hide();
        }

        if (this._bottomPreviousButton !== null) {
            $(this._bottomPreviousButton).hide();
        }

        if (this._bottomNextButton !== null) {
            $(this._bottomNextButton).hide();
        }

        if (this._steps.indexOf(this._currentStep) == 0) {
            if (this._topNextButton !== null) {
                $(this._topNextButton).show();
            }
            if (this._bottomNextButton !== null) {
                $(this._bottomNextButton).show();
            }
        } else if (this._steps.indexOf(this._currentStep) == this._steps.length - 1) {
            if (this._topPreviousButton !== null) {
                $(this._topPreviousButton).show();
            }
            if (this._bottomPreviousButton !== null) {
                $(this._bottomPreviousButton).show();
            }
        } else {
            if (this._topPreviousButton !== null) {
                $(this._topPreviousButton).show();
            }
            if (this._topNextButton !== null) {
                $(this._topNextButton).show();
            }
            if (this._bottomPreviousButton !== null) {
                $(this._bottomPreviousButton).show();
            }
            if (this._bottomNextButton !== null) {
                $(this._bottomNextButton).show();
            }
        }
    },

    _movePrevious: function () {
        var currentStepIndex = this._steps.indexOf(this._currentStep);
        if (currentStepIndex > 0) {
            currentStepIndex = currentStepIndex - 1;
            this._currentStep = this._steps[currentStepIndex];
            this._prepareUI();
            if (currentStepIndex >= 0) {
                this._pageChangedHandler(this._currentStep, this._steps[currentStepIndex + 1]);
            } else {
                this._pageChangedHandler(this._currentStep, null);
            }
        }
    },

    _moveNext: function () {
        var currentStepComponent = $find(this.get_currentStep());
        if (currentStepComponent.isValid()) {
            var currentStepIndex = this._steps.indexOf(this._currentStep);
            if (currentStepIndex < this._steps.length - 1) {
                currentStepIndex = currentStepIndex + 1;
                this._currentStep = this._steps[currentStepIndex];
                this._prepareUI();
                this._pageChangedHandler(this._currentStep, this._steps[currentStepIndex - 1]);
            }
        }
    },

    _pageChangedHandler: function (currentStepId, previousStepId) {
        var pageChangedArgs =
        {
            'CurrentStepId': currentStepId,
            'PreviousStepId': previousStepId
        };
        var h = this.get_events().getHandler('pageChanged');
        if (h) h(this, pageChangedArgs);
        return pageChangedArgs;
    },

    /* *************************** properties *************************** */
    get_topPreviousButton: function () {
        return this._topPreviousButton;
    },
    set_topPreviousButton: function (value) {
        this._topPreviousButton = value;
    },

    get_topNextButton: function () {
        return this._topNextButton;
    },
    set_topNextButton: function (value) {
        this._topNextButton = value;
    },

    get_bottomPreviousButton: function () {
        return this._bottomPreviousButton;
    },
    set_bottomPreviousButton: function (value) {
        this._bottomPreviousButton = value;
    },

    get_bottomNextButton: function () {
        return this._bottomNextButton;
    },
    set_bottomNextButton: function (value) {
        this._bottomNextButton = value;
    },
    get_steps: function () {
        return this._steps;
    },
    set_steps: function (value) {
        this._steps = value;
    },
    get_currentStep: function () {
        if (this._currentStep === null) {
            this._currentStep = this.get_steps()[0];
        }
        return this._currentStep;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizard.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizard', Sys.UI.Control);