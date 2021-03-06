/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Versioning.Web.UI");

Telerik.Sitefinity.Versioning.Web.UI.VersionNoteControl = function (element) {
    Telerik.Sitefinity.Versioning.Web.UI.VersionNoteControl.initializeBase(this, [element]);
    this._promptDialogComponent = null;
    this._editButton = null;
    this._deleteButton = null;
    this._noteViewLabel = null;
    this._versionInfoLabel = null;
    this._dataItemContext = null;
    this._editButtonClickDelegate = null;
    this._deleteButtonClickDelegate = null;
    this._noteChangedDelegate = null;
    this._noteUpdatedDelegate = null;
    this._noteUpdateFailureDelegate = null;
    this._serviceBaseUrl = null;
    this._manager = null;
    this._buttonAreaPanelId = null;
    this._buttonAreaPanel = null;
    this._clientLabelManager = null;
}

Telerik.Sitefinity.Versioning.Web.UI.VersionNoteControl.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        this._editButtonClickDelegate = Function.createDelegate(this, this._editButtonClickHandler);
        $addHandler(this._editButton, "click", this._editButtonClickDelegate);

        this._deleteButtonClickDelegate = Function.createDelegate(this, this._deleteButtonClickHandler);
        $addHandler(this._deleteButton, "click", this._deleteButtonClickDelegate);

        this._noteChangedDelegate = Function.createDelegate(this, this._noteChangedHandler);
        this._noteUpdatedDelegate = Function.createDelegate(this, this._noteUpdatedHandler);
        this._noteUpdateFailureDelegate = Function.createDelegate(this, this._noteUpdateFailureHandler);
        this._promptDialogComponent.add_command(this._noteChangedDelegate);
        this._buttonAreaPanel = $get(this._buttonAreaPanelId);
        Telerik.Sitefinity.Versioning.Web.UI.VersionNoteControl.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        if (this._editButtonClickDelegate) {
            if (this._editButton) {
                $removeHandler(this._editButton, "click", this._editButtonClickDelegate);
            }
            delete this._editButtonClickDelegate;
        }

        if (this._deleteButtonClickDelegate) {
            if (this._deleteButton) {
                $removeHandler(this._deleteButton, "click", this._deleteButtonClickDelegate);
            }
            delete this._deleteButtonClickDelegate;
        }

        if (this._noteChangedDelegate) {
            if (this._promptDialogComponent) {
                this._promptDialogComponent.remove_command(this._noteChangedDelegate);
            }
            delete this._noteChangedDelegate;
        }
        this._noteUpdatedDelegate = null;
        this._noteUpdateFailureDelegate = null;

        Telerik.Sitefinity.Versioning.Web.UI.VersionNoteControl.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */
    reset: function () {
        jQuery(this.get_element()).hide();
        this._noteViewLabel.innerHTML = "";
        this._dataItemContext = null;
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    // Handles the click on the edit button, public
    editButtonClickHandler: function (e) {
        this._editButtonClickHandler(e);
    },

    // Handles the click on the edit button
    _editButtonClickHandler: function (e) {
        if (this._dataItemContext == null) {
            return;
        }
        var oPDialog = this.get_promptDialogComponent();
        var textValue = this._dataItemContext.VersionInfo.Comment;
        var preTextFieldTitle = this.get_clientLabelManager().getLabel("VersionResources", "EditNoteForVersion") + " ";
        if ((textValue == '') || (textValue == ' ') || (textValue == '&nbsp;')) {
            preTextFieldTitle = this.get_clientLabelManager().getLabel("VersionResources", "WriteNoteForVersion") + " "; //WriteNoteForVersion
        }
        oPDialog.set_textFieldTitle(preTextFieldTitle + this._dataItemContext.VersionInfo.Version);
        oPDialog.set_defaultInputText(textValue);
        oPDialog.show_prompt();
        // radprompt("Edit note for version " + this._dataItemContext.VersionInfo.Version, this._noteChangedDelegate, 330, 160, null, "", this._dataItemContext.VersionInfo.Comment);
    },

    // Handles the click on the delete button
    _deleteButtonClickHandler: function (e) {
        this._updateNoteWithService(" ");
        this._dataItemContext.VersionInfo.Comment = " ";
        this.get_noteViewLabel().innerHTML = "";
        this._updateEditButtonLabel();
    },

    _noteChangedHandler: function (sender, args) {
        if (args._commandName == "cancel") return;
        if (sender != null) {
            var argsText = sender.get_inputText();
            if (!argsText) {
                argsText = " ";
            }
            this._updateNoteWithService(argsText);
            this.get_noteViewLabel().innerHTML = argsText.htmlEncode();
            this._dataItemContext.VersionInfo.Comment = argsText.htmlEncode();
            this._updateEditButtonLabel();
        }
    },

    _updateNoteWithService: function (note) {
        var manager = this.getManager();
        var keys = [];
        keys.push(this._dataItemContext.VersionInfo.Id);
        manager.InvokePut(this._serviceBaseUrl, [], keys, note, this._noteUpdatedDelegate, this._noteUpdateFailureDelegate, this, null, null, null);
    },

    _noteUpdatedHandler: function (sender, result) {
        //alert('Note updated, successfully');

    },
    _noteUpdateFailureHandler: function (sender, result) {
        alert(this.get_clientLabelManager().getLabel("VersionResources", "ErrorWhileUpdatingNote"));
    },

    _updateEditButtonLabel: function () {
        if (this.get_noteViewLabel().innerHTML == " " || this.get_noteViewLabel().innerHTML == "&nbsp;" || this.get_noteViewLabel().innerHTML == "") {
            this.get_editButton().getElementsByTagName('span')[0].innerHTML = this.get_clientLabelManager().getLabel("VersionResources", "Write");
            var expandBtn = document.getElementById('versionNoteHolderExpand');
            if (expandBtn && expandBtn.innerHTML != this.get_clientLabelManager().getLabel("VersionResources", "HideNote"))
                expandBtn.innerHTML = this.get_clientLabelManager().getLabel("VersionResources", "WriteNoteLabel");
        } else {
            this.get_editButton().getElementsByTagName('span')[0].innerHTML = this.get_clientLabelManager().getLabel("VersionResources", "Edit");
            //if (document.getElementById('versionNoteHolderExpand')) if (document.getElementById('versionNoteHolderExpand').innerHTML != 'Hide note') document.getElementById('versionNoteHolderExpand').innerHTML = 'Show note for this version';

            var versionNote = this.get_element();
            versionNote.style.display = 'block';
            versionNote.className = 'sfPageVersionNoteWrp';
            jQuery("#versionNoteHolderExpand").html(this.get_clientLabelManager().getLabel("VersionResources", "HideNote"));

        }
    },

    getManager: function () {
        if (this._manager === null) {
            this._manager = new Telerik.Sitefinity.Data.ClientManager();
        }
        return this._manager;
    },
    // Gets the component of the PromptDialog control
    get_promptDialogComponent: function () { return this._promptDialogComponent; },
    // Sets the component of the PromptDialog control
    set_promptDialogComponent: function (value) { this._promptDialogComponent = value; },

    // Gets the link button (DOM element) that starts the note editing
    get_editButton: function () { return this._editButton; },
    // sets the link button (DOM element) that starts the note editing
    set_editButton: function (value) { this._editButton = value; },

    // Gets the link button (DOM element) that deletes the note
    get_deleteButton: function () { return this._deleteButton; },
    // sets the link button (DOM element) that deletes the note
    set_deleteButton: function (value) { this._deleteButton = value; },

    //gets the notes label
    get_noteViewLabel: function () { return this._noteViewLabel; },
    //sets the notes label
    set_noteViewLabel: function (value) { this._noteViewLabel = value; },
    //gets the version info label
    get_versionInfoLabel: function () { return this._versionInfoLabel; },
    //sets the version info label
    set_versionInfoLabel: function (value) { this._versionInfoLabel = value; },

    //get the button area panel
    get_buttonArea: function () { return this._buttonAreaPanel; },

    get_dataFieldName: function () { return null; },
    set_dataFieldName: function (value) { return; },

    // inherited from IRequiresDataItemContext
    set_dataItemContext: function (value) {
        this.reset();
        this._dataItemContext = value;
        if (this._dataItemContext.VersionInfo) {
            jQuery(this.get_element()).show();
            var info = this._dataItemContext.VersionInfo;
            this.get_noteViewLabel().innerHTML = info.Comment.htmlEncode();
            var txt = this.get_clientLabelManager().getLabel("VersionResources", "ModifiedAtDateByUserName");
            txt = String.format(txt, info.LastModified.sitefinityLocaleFormat('dd MMM, yyyy'), info.CreatedByUserName);
            this.get_versionInfoLabel().innerHTML = txt;
        }
        else {
            jQuery(this.get_element()).hide();
        }

        this._updateEditButtonLabel();
    },

    get_clientLabelManager: function () { return this._clientLabelManager; },
    set_clientLabelManager: function (val) { this._clientLabelManager = val; }
};

Telerik.Sitefinity.Versioning.Web.UI.VersionNoteControl.registerClass("Telerik.Sitefinity.Versioning.Web.UI.VersionNoteControl", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext);