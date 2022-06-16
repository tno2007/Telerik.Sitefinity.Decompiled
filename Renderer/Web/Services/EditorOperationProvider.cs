// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Web.Services.EditorOperationProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Renderer.Editor;
using Telerik.Sitefinity.Renderer.Web.Services.Dto;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;

namespace Telerik.Sitefinity.Renderer.Web.Services
{
  internal class EditorOperationProvider : IOperationProvider
  {
    private IEditorAdaptor editorAdaptor;

    public EditorOperationProvider(IEditorAdaptor editorAdaptor) => this.editorAdaptor = editorAdaptor;

    public IEnumerable<OperationData> GetOperations(Type clrType)
    {
      if (!((IEnumerable<Type>) new Type[2]
      {
        typeof (PageNode),
        typeof (PageTemplate)
      }).Contains<Type>(clrType))
        return Enumerable.Empty<OperationData>();
      OperationData operationData1 = OperationData.Create<string, IList<ItemOperation>>(new Func<OperationContext, string, IList<ItemOperation>>(this.GetWidgetOperations));
      operationData1.OperationType = OperationType.PerItem;
      OperationData operationData2 = OperationData.Create<DefaultExecuteOperationContext, WidgetOperationResult>(new Func<OperationContext, DefaultExecuteOperationContext, WidgetOperationResult>(this.ExecuteWidgetOperation));
      operationData2.OperationType = OperationType.PerItem;
      operationData2.IsRead = false;
      OperationData operationData3 = OperationData.Create<LockContext, EditorState>(new Func<OperationContext, LockContext, EditorState>(this.Lock));
      operationData3.OperationType = OperationType.PerItem;
      operationData3.IsRead = false;
      OperationData operationData4 = OperationData.Create(new Action<OperationContext>(this.Unlock));
      operationData4.OperationType = OperationType.PerItem;
      operationData4.IsRead = false;
      OperationData operationData5 = OperationData.Create<DefaultAddWidgetContext, WidgetState>(new Func<OperationContext, DefaultAddWidgetContext, WidgetState>(this.AddWidget));
      operationData5.OperationType = OperationType.PerItem;
      operationData5.IsRead = false;
      OperationData operationData6 = OperationData.Create<DefaultAddWidgetContext>(new Action<OperationContext, DefaultAddWidgetContext>(this.MoveWidget));
      operationData6.OperationType = OperationType.PerItem;
      operationData6.IsRead = false;
      OperationData operationData7 = OperationData.Create<EditorState>(new Func<OperationContext, EditorState>(this.GetState));
      operationData7.OperationType = OperationType.PerItem;
      OperationData operationData8 = OperationData.Create<string, PropertyValueGroupContainer>(new Func<OperationContext, string, PropertyValueGroupContainer>(this.GetPropertyValues));
      operationData8.OperationType = OperationType.PerItem;
      OperationData operationData9 = OperationData.Create<string, ComponentDto>(new Func<OperationContext, string, ComponentDto>(this.HierarchicalWidgetModel));
      operationData9.OperationType = OperationType.PerItem;
      OperationData operationData10 = OperationData.Create<PropertyValueGroupContainer>(new Action<OperationContext, PropertyValueGroupContainer>(this.SetProperties));
      operationData10.OperationType = OperationType.PerItem;
      operationData10.IsRead = false;
      return (IEnumerable<OperationData>) new OperationData[10]
      {
        operationData9,
        operationData1,
        operationData2,
        operationData3,
        operationData4,
        operationData5,
        operationData6,
        operationData7,
        operationData8,
        operationData10
      };
    }

    private void SetProperties(
      OperationContext context,
      PropertyValueGroupContainer propertyValueGroup)
    {
      DefaultEditPropertiesWidgetContext propertiesWidgetContext = new DefaultEditPropertiesWidgetContext()
      {
        PropertyGroup = propertyValueGroup
      };
      this.EnhanceContext(context, (DefaultEditorContext) propertiesWidgetContext);
      this.editorAdaptor.EditProperties((IEditWidgetPropertiesContext) propertiesWidgetContext);
    }

    private ComponentDto HierarchicalWidgetModel(
      OperationContext context,
      string componentId)
    {
      DefaultGetWidgetModelContext widgetModelContext = new DefaultGetWidgetModelContext()
      {
        Id = componentId
      };
      this.EnhanceContext(context, (DefaultEditorContext) widgetModelContext);
      return this.editorAdaptor.HierarchicalWidgetModel((IGetWidgetModelContext) widgetModelContext);
    }

    private PropertyValueGroupContainer GetPropertyValues(
      OperationContext context,
      string componentId)
    {
      DefaultGetWidgetModelContext widgetModelContext = new DefaultGetWidgetModelContext()
      {
        Id = componentId
      };
      this.EnhanceContext(context, (DefaultEditorContext) widgetModelContext);
      return this.editorAdaptor.GetPropertyValues((IGetWidgetModelContext) widgetModelContext);
    }

    private WidgetState AddWidget(
      OperationContext context,
      DefaultAddWidgetContext widget)
    {
      this.EnhanceContext(context, (DefaultEditorContext) widget);
      WidgetState widgetState = this.editorAdaptor.AddWidget((IAddWidgetContext) widget);
      string localizationMode = this.editorAdaptor.GetPropertyValueLocalizationMode((IEditorContext) widget);
      this.SetProperties(context, new PropertyValueGroupContainer()
      {
        ComponentId = widgetState.Key,
        Properties = widget.Properties,
        PropertyLocalizationMode = localizationMode
      });
      return widgetState;
    }

    private void MoveWidget(OperationContext context, DefaultAddWidgetContext widget)
    {
      this.EnhanceContext(context, (DefaultEditorContext) widget);
      this.editorAdaptor.MoveWidget((IAddWidgetContext) widget);
    }

    private EditorState Lock(OperationContext context, LockContext state)
    {
      this.EnhanceContext(context, (DefaultEditorContext) state);
      return this.editorAdaptor.Lock((ILockContext) state);
    }

    private void Unlock(OperationContext context)
    {
      DefaultEditorContext defaultEditorContext = new DefaultEditorContext();
      this.EnhanceContext(context, defaultEditorContext);
      this.editorAdaptor.Unlock((IEditorContext) defaultEditorContext);
    }

    private WidgetOperationResult ExecuteWidgetOperation(
      OperationContext context,
      DefaultExecuteOperationContext operation)
    {
      this.EnhanceContext(context, (DefaultEditorContext) operation);
      return this.editorAdaptor.ExecuteWidgetOperation((IExecuteOperationContext) operation);
    }

    private IList<ItemOperation> GetWidgetOperations(
      OperationContext context,
      string widgetKey)
    {
      DefaultOperationsContext operationsContext = new DefaultOperationsContext()
      {
        WidgetKey = widgetKey
      };
      this.EnhanceContext(context, (DefaultEditorContext) operationsContext);
      return this.editorAdaptor.GetWidgetOperations((IGetOperationsContext) operationsContext);
    }

    private EditorState GetState(OperationContext context)
    {
      DefaultEditorContext defaultEditorContext = new DefaultEditorContext();
      this.EnhanceContext(context, defaultEditorContext);
      return this.editorAdaptor.GetState((IEditorContext) defaultEditorContext);
    }

    private void EnhanceContext(OperationContext context, DefaultEditorContext editorContext)
    {
      editorContext.Key = context.GetKey();
      editorContext.EditedType = context.GetClrType();
    }
  }
}
