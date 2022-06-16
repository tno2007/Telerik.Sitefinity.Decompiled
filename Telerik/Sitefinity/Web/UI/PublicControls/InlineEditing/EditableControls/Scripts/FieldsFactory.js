define(function(){function FieldsFactory(){}FieldsFactory.prototype={createField:function(fieldType,options,callback){var field={};
if(fieldType){require([fieldType],function(EditableField){field=new EditableField(options);
if(typeof callback==="function"){callback(field);
}});
}return field;
}};
return new FieldsFactory();
});
