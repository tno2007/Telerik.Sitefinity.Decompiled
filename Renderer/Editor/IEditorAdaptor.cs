// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Editor.IEditorAdaptor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Renderer.Web.Services.Dto;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;

namespace Telerik.Sitefinity.Renderer.Editor
{
  internal interface IEditorAdaptor
  {
    WidgetState AddWidget(IAddWidgetContext context);

    ComponentDto HierarchicalWidgetModel(IGetWidgetModelContext context);

    PropertyValueGroupContainer GetPropertyValues(
      IGetWidgetModelContext context);

    void EditProperties(IEditWidgetPropertiesContext context);

    void MoveWidget(IAddWidgetContext context);

    IList<ItemOperation> GetWidgetOperations(IGetOperationsContext context);

    WidgetOperationResult ExecuteWidgetOperation(
      IExecuteOperationContext context);

    EditorState Lock(ILockContext context);

    void Unlock(IEditorContext context);

    EditorState GetState(IEditorContext context);

    string GetPropertyValueLocalizationMode(IEditorContext context);
  }
}
