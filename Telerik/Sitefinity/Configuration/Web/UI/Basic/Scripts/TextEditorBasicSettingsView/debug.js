﻿Type.registerNamespace("Telerik.Sitefinity.Configuration.Web.UI.Basic");
Telerik.Sitefinity.Configuration.Web.UI.Basic.TextEditorBasicSettingsView = function (element)
{
	Telerik.Sitefinity.Configuration.Web.UI.Basic.TextEditorBasicSettingsView.initializeBase(this, [element]);
	this._customToolSetsItemsGrid = null;
	this._defaultToolSetsItemsGrid = null;
	this._createToolSetButton = null;
	this._createToolSetButtonClickDelegate = null;
	this._uploadToolSetButton = null;
	this._uploadToolSetButtonClickDelegate = null;
}

Telerik.Sitefinity.Configuration.Web.UI.Basic.TextEditorBasicSettingsView.prototype =
{
	/* --------------------  set up and tear down ----------- */
	initialize: function ()
	{
		Telerik.Sitefinity.Configuration.Web.UI.Basic.TextEditorBasicSettingsView.callBaseMethod(this, "initialize");

		this.set_createToolSetButton($get("openCreateToolsSetButton"));
		this._createToolSetButtonClickDelegate = Function.createDelegate(this, this._createToolSetButtonClickHandler);
		$addHandler(this.get_createToolSetButton(), "click", this._createToolSetButtonClickDelegate);

		this.set_uploadToolSetButton($get("openUploadToolsSetFileButton"));
		this._uploadToolSetButtonClickDelegate = Function.createDelegate(this, this._uploadToolSetButtonClickHandler);
		$addHandler(this.get_uploadToolSetButton(), "click", this._uploadToolSetButtonClickDelegate);

		var defaultToolSetsItemsGrid = this.get_defaultToolSetsItemsGrid();
		this._defaultToolSetsItemsGridDataBoundDelegate = Function.createDelegate(this, this._defaultToolSetsItemsGridDataBound);
		defaultToolSetsItemsGrid.add_dataBound(this._defaultToolSetsItemsGridDataBoundDelegate);
	},

	dispose: function ()
	{
		Telerik.Sitefinity.Configuration.Web.UI.Basic.TextEditorBasicSettingsView.callBaseMethod(this, "dispose");
		$removeHandler(this.get_createToolSetButton(), "click", this._createToolSetButtonClickDelegate);
		delete this._createToolSetButtonClickDelegate;
		$removeHandler(this.get_uploadToolSetButton(), "click", this._uploadToolSetButtonClickDelegate);
		delete this._uploadToolSetButtonClickDelegate;
	},

	/* --------------------  public methods ----------- */

	saveChanges: function ()
	{
		Telerik.Sitefinity.Configuration.Web.UI.Basic.TextEditorBasicSettingsView.callBaseMethod(this, "saveChanges");
	},

	/* --------------------  events handlers ----------- */
	_createToolSetButtonClickHandler: function (sender, args)
	{
		var grid = this.get_customToolSetsItemsGrid();
		grid.openDialog("edit", { ToolSetName: "", ToolSetXml: "" });
	},

	_uploadToolSetButtonClickHandler: function (sender, args)
	{
		var grid = this.get_customToolSetsItemsGrid();
		grid.openDialog("upload", { ToolSetName: "", ToolSetXml: "" });
	},

	_defaultToolSetsItemsGridDataBound: function (sender, args)
	{
		var clientLabelManager = this._clientLabelManager;
		$('span.sfItemTitle', sender.get_element()).each(function ()
		{
			var html = this.innerHTML;
			if (html == $('#standardEditorConfigurationKey').attr('value'))
			{

				$('<div class="sfNote">' + clientLabelManager.getLabel('Labels', 'UsedInAllBackendContentTypes') + '</div>').insertAfter(this);
			}
			else if (html == $('#minimalEditorConfigurationKey').attr('value'))
			{
				$('<div class="sfNote">' + clientLabelManager.getLabel('Labels', 'UsedInThePublicSiteWhereThereAreComments') + '</div>').insertAfter(this);
            }
            else if (html == $('#forumsEditorConfigurationKey').attr('value')) {
                $('<div class="sfNote">' + clientLabelManager.getLabel('Labels', 'UsedInThePublicSiteWhereThereAreForums') + '</div>').insertAfter(this);
            }
		});
	},
	/* -------------------- events -------------------- */

	/* --------------------  private methods ----------- */

	/* -------------------- properties ----------- */
	get_customToolSetsItemsGrid: function ()
	{
		return this._customToolSetsItemsGrid;
	},

	set_customToolSetsItemsGrid: function (value)
	{
		this._customToolSetsItemsGrid = value;
	},

	get_defaultToolSetsItemsGrid: function ()
	{
		return this._defaultToolSetsItemsGrid;
	},

	set_defaultToolSetsItemsGrid: function (value)
	{
		this._defaultToolSetsItemsGrid = value;
	},

	get_createToolSetButton: function ()
	{
		return this._createToolSetButton;
	},

	set_createToolSetButton: function (value)
	{
		this._createToolSetButton = value;
	},

	get_uploadToolSetButton: function ()
	{
		return this._uploadToolSetButton;
	},

	set_uploadToolSetButton: function (value)
	{
		this._uploadToolSetButton = value;
	}
}

Telerik.Sitefinity.Configuration.Web.UI.Basic.TextEditorBasicSettingsView.registerClass("Telerik.Sitefinity.Configuration.Web.UI.Basic.TextEditorBasicSettingsView", Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsView);