define(function(){function FieldTypeEditorFactory(){}FieldTypeEditorFactory.prototype={createField:function(fieldType,options,callback){var field={};
if(fieldType){require([fieldType],function(FieldEditor){field=new FieldEditor(options);
if(typeof callback==="function"){callback(field);
}});
}return field;
}};
return new FieldTypeEditorFactory();
});
