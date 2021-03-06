/* 
 This extension allows simpler data binding, when the binder's ContentItemId property is set by the back-end code.
 In this case the front-end code required would be just:

 <code>
    binder.set_fieldControlIds([ '<%= FirstName.ClientID %>', ... ]);
    binder.DataBind();
 </code>
*/

var FieldControlsBinder = Telerik.Sitefinity.Web.UI.FieldControls.FieldControlsBinder;

// the original DataBind function
var FieldControlsBinder_DataBind = FieldControlsBinder.prototype.DataBind;

FieldControlsBinder.prototype.DataBindContentItemId = function (id) {
    id = id || this.contentItemId;
    var success = Function.createDelegate(this, this._dataBindContentItemIdSuccess);
    var failure = Function.createDelegate(this, this._dataBindContentItemIdFailure);

    this._keys = { Id: id };
    this.get_manager().InvokeGet(this.get_serviceBaseUrl(), null, this._keys, success, failure, this);
}

FieldControlsBinder.prototype._dataBindContentItemIdSuccess = function (sender, result) {
    var dataItem = { Item: result };

    //this.DataBind(dataItem, this._keys);
    FieldControlsBinder_DataBind.call(this, dataItem, this._keys);
}

FieldControlsBinder.prototype._dataBindContentItemIdFailure = function (sender, result) {
    alert(result.Detail);
}

FieldControlsBinder.prototype.DataBind = function (dataItem, keys) {
    if (this.contentItemId || this.contentItemId === 0) {
        this.DataBindContentItemId();
    } else {
        FieldControlsBinder_DataBind.call(this, dataItem, keys);
    }
}