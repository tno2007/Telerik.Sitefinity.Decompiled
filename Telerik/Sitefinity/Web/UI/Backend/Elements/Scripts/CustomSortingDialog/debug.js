Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements");
// ------------------------------------------------------------------------
// Class CustomSortingDialog
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.Backend.Elements.CustomSortingDialog = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.CustomSortingDialog.initializeBase(this, [element]);
    /// <summary>Creates a new instance of CustomSortingDialog</summary>
    this._commandBar = null;
    this._commandBarCommandDelegate = null;
    this._containerId = null;
    this._sortExpression = null;
    this._typeProperties = null;
    this._removeAllLink = null;

    this._sortConditionControl = null;
    this._removeAllSortConditionDelegate = null;
    this._onLoadDelegate = null;
    
}

Telerik.Sitefinity.Web.UI.Backend.Elements.CustomSortingDialog.prototype = {
    // ------------------------------------------------------------------------
    // initialization and clean-up
    // ------------------------------------------------------------------------
    initialize: function () {
        // delegates
        if (this._commandBarCommandDelegate == null)
            this._commandBarCommandDelegate = Function.createDelegate(this, this.commandBar_Command);

        if (this._commandBar != null)
            this._commandBar.add_command(this._commandBarCommandDelegate);

        if (this._removeAllSortConditionDelegate === null)
            this._removeAllSortConditionDelegate = Function.createDelegate(this, this._removeAllSortConditionHandler);

        if (this._removeAllLink != null)
            $addHandler($get(this._removeAllLink), 'click', this._removeAllSortConditionDelegate);

        this._typeProperties = Sys.Serialization.JavaScriptSerializer.deserialize(this._typeProperties);
        this.buildSortConditions();

        this._prepareDialogDelegate = Function.createDelegate(this, this._prepareDialogHandler);
        this.get_radWindow().add_show(this._prepareDialogDelegate);
        
        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);

    },

    dispose: function () {
        var element = this.get_element();
        $clearHandlers(element);

        if (this._commandBarCommandDelegate != null)
            this._commandBar.remove_command(this._commandBarCommandDelegate);

        if (this._removeAllSortConditionDelegate != null)
            delete this._removeAllSortConditionDelegate;

        this._sortConditionControl.remove_addAnotherSortCondition(this._resizeToContent);
        this._sortConditionControl.remove_removeSortCondition(this._resizeToContent);
   
        this._commandBar = null;
        this._commandBarCommandDelegate = null;
        this._containerId = null;
        this._sortExpression = null;
        this._typeProperties = null;
        this._removeAllLink = null;
        this._sortConditionControl = null;

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }
    },

    /* -------------------------- Public Methods ---------------------------------------- */
    buildSortConditions: function () {
        /// <summary>Intialize custom sorting condition componet and build sorting conditions based on sorting expression.</summary>
        this._sortConditionControl = new Telerik.Sitefinity.Web.UI.Backend.Elements.SortCondition(
        this._containerId, this._sortExpression, this._typeProperties, this._removeAllLink);
    
        this._sortConditionControl.initialize();

        //subscribe for 'addAnotherSortCondition' event
        this._sortConditionControl.add_addAnotherSortCondition(this._resizeToContent);

        //subscribe for 'removeAnotherSortCondition' event
        this._sortConditionControl.add_removeSortCondition(this._resizeToContent);

    },

    /* -------------------- Events -------------------- */
    _onLoad: function () {
        jQuery("body").addClass("sfSelectorDialog");
        if ((jQuery.browser.safari || jQuery.browser.chrome) && !dialogBase._dialog.isMaximized()) 
        {
            jQuery("body").addClass("sfOverflowHiddenX");
        }
        dialogBase.resizeToContent();
    },
    /* -------------------- Event handlers ------------ */

    commandBar_Command: function (sender, args) {
        switch (args.get_commandName()) {
            case "save":
                this._saveSortExpression();
                break;
            case "cancel":
                this.close();
                break;
        }
    },

    _prepareDialogHandler: function () {
        if (this._sortConditionControl)
            this._sortConditionControl.buildUI();
    },

    _removeAllSortConditionHandler: function () {
        if (this._sortConditionControl)
            this._sortConditionControl.reset();

        dialogBase.resizeToContent();
    },

    _resizeToContent: function () {
        dialogBase.resizeToContent();
    },

    /* -------------------------- Private functions ---------------------------------- */
    _saveSortExpression: function () {
        var sortExpression = '';
        if (this._sortConditionControl) {
            sortExpression = this._sortConditionControl.getSortExpression();
        }
        this.close(sortExpression);
    },

    /* -------------------------- Properties ---------------------------------------- */
    get_commandBar: function () {
        return this._commandBar;
    },
    set_commandBar: function (value) {
        this._commandBar = value;
    },

    get_containerId: function () {
        return this._containerId;
    },
    set_containerId: function (value) {
        this._containerId = value;
    },
    get_typeProperties: function () {
        return this._typeProperties;
    },
    set_typeProperties: function (value) {
        this._typeProperties = value;
    },
    get_removeAllLink: function () {
        return this._removeAllLink;
    },
    set_removeAllLink: function (value) {
        this._removeAllLink = value;
    }
}

Telerik.Sitefinity.Web.UI.Backend.Elements.CustomSortingDialog.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.CustomSortingDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);
