Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Telerik.Sitefinity.Web.UI.TaxonSelectorResultView=function(element){this._webServiceUrl=null;
this._taxonomyProvider=null;
this._allowMultipleSelection=null;
this._selectedTaxa=[];
this._selectedIds=[];
Telerik.Sitefinity.Web.UI.TaxonSelectorResultView.initializeBase(this,[element]);
};
Telerik.Sitefinity.Web.UI.TaxonSelectorResultView.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.TaxonSelectorResultView.callBaseMethod(this,"initialize");
},dispose:function(){Telerik.Sitefinity.Web.UI.TaxonSelectorResultView.callBaseMethod(this,"dispose");
},get_selectedItems:function(){if(this._selectedTaxa.length==0||this._allowMultipleSelection){return this._selectedTaxa;
}else{return[this._selectedTaxa[0]];
}},set_selectedItems:function(value){this._selectedTaxa=[];
this._selectedIds=[];
if(value){if(this._allowMultipleSelection){this._selectedTaxa=this._selectedTaxa.concat(value);
for(var i=0,l=this._selectedTaxa.length;
i<l;
i++){this._selectedIds.push(this._selectedTaxa[i].Id);
}}else{this._selectedTaxa.push(value[0]);
this._selectedIds.push(value[0].Id);
}}this.refreshUI();
},get_selectedValues:function(){if(this._selectedIds.length==0||this._allowMultipleSelection){return this._selectedIds;
}else{return[this._selectedIds[0]];
}},set_selectedValues:function(value){if(!value){this._selectedIds=[];
}else{if(this._allowMultipleSelection){this._selectedIds=value;
}else{this._selectedIds=[value[0]];
}}this.refreshUI();
},refreshUI:function(){},get_allowMultipleSelection:function(){return this._allowMultipleSelection;
},set_allowMultipleSelection:function(value){this._allowMultipleSelection=value;
},get_taxonomyProvider:function(){return this._taxonomyProvider;
},set_taxonomyProvider:function(value){this._taxonomyProvider=value;
},get_webServiceUrl:function(){return this._webServiceUrl;
},set_webServiceUrl:function(value){this._webServiceUrl=value;
}};
Telerik.Sitefinity.Web.UI.TaxonSelectorResultView.registerClass("Telerik.Sitefinity.Web.UI.TaxonSelectorResultView",Telerik.Sitefinity.Web.UI.SelectorResultView);
