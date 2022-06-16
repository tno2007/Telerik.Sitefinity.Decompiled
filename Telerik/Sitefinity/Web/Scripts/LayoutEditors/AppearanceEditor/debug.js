//===================================================Appearance editor===========================================================//
Telerik.Sitefinity.Web.UI.AppearanceEditor = function (layoutRoot, visualEditorContainer, outerColumns, innerColumns, controlPanelInfo) {
    Telerik.Sitefinity.Web.UI.AppearanceEditor.initializeBase(this, [visualEditorContainer, outerColumns, innerColumns]);

    this._visualEditorContainer = visualEditorContainer;
    this._outerColumns = outerColumns;
    this._innerColumns = innerColumns;
    this._layoutRoot = layoutRoot;

    //The current column that is edited
    this._currentColumn = null;

    // Control panel
    this._controlPanelInfo = controlPanelInfo;
    this._classesContainer = $get(this._controlPanelInfo.ClassesContainerId);
    this._wrapperCssTextbox = $get(this._controlPanelInfo.WrapperCssClassTextboxId);
    this._placeholdersLabelsContainer = $get(this._controlPanelInfo.PlaceholdersLabelsContainerId);

    this._classTextBoxes = [];
    this._placeholderLabelTextBoxes = [];
    this._classNameTextBoxIdPrefix = 'sfColumnClass';
    this._placeholderLabelTextBoxIdPrefix = 'sfPlaceholderLabelTextBox';
    this._layoutClass = 'sf_colsOut';
    this._layoutWrapperClass = 'sf_cols';

    this.layoutUpdater = null;
}

Telerik.Sitefinity.Web.UI.AppearanceEditor.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.AppearanceEditor.callBaseMethod(this, 'initialize');

        this.layoutUpdater = LayoutUpdater.getInstance();
        this._classNameChangedDelegate = Function.createDelegate(this, this._classNameChanged);
        this._placeholderLabelChangedDelegate = Function.createDelegate(this, this._placeholderLabelChanged);
        this._wrapperClassNameChangedDelegate = Function.createDelegate(this, this._wrapperClassNameChanged);
        this._generateTextboxes();
        this._initializeWrapperTextbox();
    },

    dispose: function () {
        //TODO: Detach click handlers to the panes
        $(this._classesContainer).empty();
        $(this._placeholdersLabelsContainer).empty();
        Telerik.Sitefinity.Web.UI.AppearanceEditor.callBaseMethod(this, 'dispose');

        $removeHandler(this._wrapperCssTextbox, 'change', this._wrapperClassNameChangedDelegate);

        this._classTextBoxes = [];
        this._placeholderLabelTextBoxes = [];
    },

    SetVisualEditor: function () {
        this.ClearUI();
        this.GenerateLabels(this._generateLabelStrings());
    },

    _initializeWrapperTextbox: function () {
        this._wrapperCssTextbox.value = this._getOriginalWrapperClass();
        $addHandler(this._wrapperCssTextbox, 'change', this._wrapperClassNameChangedDelegate);
    },

    _generateTextboxes: function () {
        var columnsCount = this._innerColumns.length;

        this._generateClassNameTextBoxes(columnsCount);

        this._generatePlaceholderLabelTextBoxes(columnsCount);
    },

    _generateClassNameTextBoxes: function (columnsCount) {
        // generate unordered list that will have list item per class name text box.
        var listElement = document.createElement('ul');

        for (var columnIndex = 0; columnIndex < columnsCount; columnIndex++) {

            var listItemElement = this._createClassNameTextBoxWithLabel(columnIndex);
            listElement.appendChild(listItemElement);
        }

        this._classesContainer.appendChild(listElement);
    },

    _generatePlaceholderLabelTextBoxes: function (columnsCount) {
        // generate unordered list that will contain a list item per every placeholder label text box.
        var listElement = document.createElement('ul');

        for (var columnIndex = 0; columnIndex < columnsCount; columnIndex++) {

            var listItemElement = this._createPlaceholderLabelTextBoxWithLabel(columnIndex);
            listElement.appendChild(listItemElement);
        }

        this._placeholdersLabelsContainer.appendChild(listElement);
    },

    _createClassNameTextBoxWithLabel: function (columnIndex) {
        var listItemElement = document.createElement('li');

        var colTxtId = this._classNameTextBoxIdPrefix + columnIndex;

        //Creating a label element for the text box.
        var labelInnerHtml = this._controlPanelInfo.ColumnLabel + ' ' + (columnIndex + 1);
        var columnLabel = this._createTextBoxLabelElement(colTxtId, labelInnerHtml);
        listItemElement.appendChild(columnLabel);

        //Creating a text box for setting the class name.
        var initialValue = this._getOriginalColumnClass(columnIndex);
        var classTextbox = this._createTextBoxElement(colTxtId, initialValue);
        listItemElement.appendChild(classTextbox);

        this._classTextBoxes.push(classTextbox);

        // TODO: remove these delegates somewhere
        $addHandler(classTextbox, 'change', this._classNameChangedDelegate);
        return listItemElement;
    },

    _createPlaceholderLabelTextBoxWithLabel: function (columnIndex) {
        var listItemElement = document.createElement('li');

        var colTxtId = this._placeholderLabelTextBoxIdPrefix + columnIndex;

        //Creating a label element for the text box.
        //TODO: use a proper label.
        var labelInnerHtml = this._controlPanelInfo.ColumnLabel + ' ' + (columnIndex + 1);
        var columnLabel = this._createTextBoxLabelElement(colTxtId, labelInnerHtml);
        listItemElement.appendChild(columnLabel);

        //Creating a text box for setting the class name.
        var initialValue = this._getOriginalPlaceholderLabel(columnIndex);
        if (!initialValue) {
            initialValue = "";
        }
        var placeholderLabelTextBox = this._createTextBoxElement(colTxtId, initialValue);
        listItemElement.appendChild(placeholderLabelTextBox);

        this._placeholderLabelTextBoxes.push(placeholderLabelTextBox);

        // TODO: remove these delegates somewhere
        $addHandler(placeholderLabelTextBox, 'change', this._placeholderLabelChangedDelegate);
        return listItemElement;
    },

    _createTextBoxLabelElement: function (textBoxId, labelInnerHtml) {
        var columnLabel = document.createElement('label');
        columnLabel.setAttribute('for', textBoxId);
        // adjust for the zero based index
        columnLabel.innerHTML = labelInnerHtml;
        return columnLabel;
    },

    _createTextBoxElement: function (textBoxId, initialValue) {
        var textBoxElement = document.createElement('input');
        textBoxElement.setAttribute('id', textBoxId);
        textBoxElement.setAttribute('type', 'input');
        textBoxElement.setAttribute('class', 'sfTxt');
        textBoxElement.value = initialValue;
        return textBoxElement;
    },

    _classNameChanged: function (sender) {
        // append the layout class
        var newClassName = this._layoutClass;
        if (newClassName) newClassName += ' ';
        newClassName += sender.target.value;
        this._outerColumns[this._getClassNamesColumnIndex(sender.target.id)].className = newClassName;
        this.UpdateLabels(this._generateLabelStrings());
    },

    _placeholderLabelChanged: function (sender) {
        // append the layout class
        var label = sender.target.value;
        var columnElement = this._outerColumns[this._getPlaceholderLabelColumnIndex(sender.target.id)];
        var jColumnElement = jQuery(columnElement);

        this.layoutUpdater.updatePlaceholderLabels(jColumnElement, label);
        this.UpdateLabels(this._generateLabelStrings());
    },

    _wrapperClassNameChanged: function (sender) {
        // append the layout class
        var newClassName = this._layoutWrapperClass
        if (newClassName) newClassName += ' ';
        newClassName += sender.target.value;
        this._layoutRoot.className = newClassName;
    },

    _getOriginalColumnClass: function (colIndex) {
        var classes = this._outerColumns[colIndex].className;
        // remove the layout class
        var result = classes.replace(this._layoutClass, '');
        result = jQuery.trim(result);
        return result;
    },

    _getOriginalPlaceholderLabel: function (colIndex) {
        var columnElement = this._outerColumns[colIndex];
        var jColumnElement = jQuery(columnElement);

        return this.layoutUpdater.getLabelFromOuterColumn(jColumnElement);
    },

    _getOriginalWrapperClass: function () {
        var classes = this._layoutRoot.className;
        // remove the layout wrapper class
        var result = classes.replace(this._layoutWrapperClass, '');
        result = jQuery.trim(result);
        return result;
    },

    _getClassNamesColumnIndex: function (textBoxId) {
        return Number(textBoxId.substring(this._classNameTextBoxIdPrefix.length));
    },

    _getPlaceholderLabelColumnIndex: function (textBoxId) {
        return Number(textBoxId.substring(this._placeholderLabelTextBoxIdPrefix.length));
    },

    _generateLabelStrings: function () {
        var labelStrings = [];
        var colCount = this._outerColumns.length;
        for (var i = 0; i < colCount; i++) {
            var colCaption = this._controlPanelInfo.ColumnLabel + ' ' + (i + 1);
            var label = '<strong>' + colCaption + '</strong><br />' + this._classTextBoxes[i].value;
            labelStrings.push(label);
        }
        return labelStrings;
    }
}

Telerik.Sitefinity.Web.UI.AppearanceEditor.registerClass('Telerik.Sitefinity.Web.UI.AppearanceEditor', Telerik.Sitefinity.Web.UI.EditorBase);