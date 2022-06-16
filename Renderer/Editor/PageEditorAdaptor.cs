// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Editor.PageEditorAdaptor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Modules.Strategies.Operation;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Renderer.Editor.Specifics;
using Telerik.Sitefinity.Renderer.Generators;
using Telerik.Sitefinity.Renderer.Web.Services.Dto;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Api.OData.Exceptions;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;

namespace Telerik.Sitefinity.Renderer.Editor
{
  internal class PageEditorAdaptor : IEditorAdaptor
  {
    private const string CaptionPropertyName = "SfCaption";
    private CompositeGenerator generator;

    public PageEditorAdaptor(CompositeGenerator generator) => this.generator = generator;

    public WidgetState AddWidget(IAddWidgetContext context)
    {
      IContainerSpecifics containerSpecifics = this.GetContainerSpecifics((IEditorContext) context);
      IControlManager manager = containerSpecifics.GetManager();
      object itemFromContext = containerSpecifics.GetItemFromContext((IEditorContext) context, manager);
      IControlsContainer controlsContainer = this.CheckOut(containerSpecifics, itemFromContext, true, "Adding a widget, when page is not locked is not supported", manager);
      IList<ControlData> fromAllContainers = new ContainerIterator().GetControlsFromAllContainers(controlsContainer);
      ControlData control = this.AddWidget(context, manager, containerSpecifics, itemFromContext, controlsContainer, fromAllContainers);
      manager.SaveChanges();
      return this.GetWidgetState(control, controlsContainer, true, true);
    }

    public void MoveWidget(IAddWidgetContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      Guid parsedWidgetId;
      IContainerSpecifics specifics = Guid.TryParse(context.Id, out parsedWidgetId) ? this.GetContainerSpecifics((IEditorContext) context) : throw new InvalidOperationException("The provided widget id " + context.Id + " is not a valid Guid");
      IControlManager manager = specifics.GetManager();
      object itemFromContext = specifics.GetItemFromContext((IEditorContext) context, manager);
      IControlsContainer source = this.CheckOut(specifics, itemFromContext, true, "Moving a widget when page is not locked is not supported", manager);
      IList<ControlData> fromAllContainers = new ContainerIterator().GetControlsFromAllContainers(source);
      ControlData control = source.Controls.FirstOrDefault<ControlData>((Func<ControlData, bool>) (x => x.Id == parsedWidgetId));
      if (!control.IsGranted("Controls", "MoveControl"))
        throw new InvalidOperationException("No operation with the name " + context.Name + " was found to execute.");
      Tuple<Guid, Guid> tuple = this.GuardWidgetPosition(context, control, (IEnumerable<ControlData>) fromAllContainers);
      ++source.Version;
      foreach (ControlData controlData in fromAllContainers.Where<ControlData>((Func<ControlData, bool>) (x => x.SiblingId == control.Id)))
        controlData.SiblingId = control.SiblingId;
      ++control.Version;
      control.PlaceHolder = context.PlaceholderName;
      control.SiblingId = tuple.Item2;
      control.ParentId = tuple.Item1;
      foreach (ControlData controlData in fromAllContainers.Where<ControlData>((Func<ControlData, bool>) (x => x.SiblingId == control.SiblingId && x.PlaceHolder == control.PlaceHolder && x.ParentId == control.ParentId && x.Id != control.Id)))
        controlData.SiblingId = control.Id;
      manager.SaveChanges();
    }

    public WidgetOperationResult ExecuteWidgetOperation(
      IExecuteOperationContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      Guid parsedWidgetId;
      IContainerSpecifics specifics = Guid.TryParse(context.WidgetKey, out parsedWidgetId) ? this.GetContainerSpecifics((IEditorContext) context) : throw new InvalidOperationException("The provided widget id " + context.WidgetKey + " is not a valid Guid");
      IControlManager manager = specifics.GetManager();
      object itemFromContext = specifics.GetItemFromContext((IEditorContext) context, manager);
      IControlsContainer draft = this.CheckOut(specifics, itemFromContext, true, "Executing operations for a widget when page is not locked is not supported", manager);
      if (this.GetWidgetOperations((IGetOperationsContext) context, draft).FirstOrDefault<ItemOperation>((Func<ItemOperation, bool>) (x => x.Name == context.Name)) == null)
        throw new InvalidOperationException("No operation with the name " + context.Name + " was found to execute.");
      ++draft.Version;
      ControlData control = draft.Controls.FirstOrDefault<ControlData>((Func<ControlData, bool>) (x => x.Id == parsedWidgetId));
      if (string.Equals(context.Name, "delete", StringComparison.OrdinalIgnoreCase))
        return this.HandleDelete(specifics, draft, control, manager);
      if (string.Equals(context.Name, "duplicate", StringComparison.OrdinalIgnoreCase))
        return this.HandleDuplicate(context, manager, specifics, draft, itemFromContext, control);
      if (string.Equals(context.Name, "copy-widget-settings", StringComparison.OrdinalIgnoreCase))
        return this.HandleCopy(context);
      return context.Name.StartsWith("paste-widget-settings", StringComparison.OrdinalIgnoreCase) ? this.HandlePaste(context) : WidgetOperationResult.FailureResult();
    }

    public IList<ItemOperation> GetWidgetOperations(IGetOperationsContext context)
    {
      IControlsContainer draft = this.GetDraft((IEditorContext) context, "Page is not locked. Widget operations are available when page is locked.");
      return this.GetWidgetOperations(context, draft);
    }

    public void EditProperties(IEditWidgetPropertiesContext context)
    {
      this.GetDraft((IEditorContext) context, "Page is not locked. Editing widgets is supported only when page is locked.");
      SetComponentArgs args = new SetComponentArgs();
      args.ContainerResolver = this.GetContainerResolver((IEditorContext) context);
      args.ComponentId = context.PropertyGroup.ComponentId;
      args.PropertiesGroup = context.PropertyGroup;
      args.IsEdit = true;
      args.CleanPersistedProperites = context.CleanPersistedProperties;
      this.generator.SetComponentPropertyValues((ISetComponentArgs) args);
    }

    public ComponentDto HierarchicalWidgetModel(IGetWidgetModelContext context)
    {
      this.GetDraft((IEditorContext) context, "Page is not locked. Hierarchical model is available only when page is locked.");
      GetComponentArgs args = new GetComponentArgs();
      args.ContainerResolver = this.GetContainerResolver((IEditorContext) context);
      args.ComponentId = context.Id;
      args.PreserveDynamicLinks = true;
      args.IsEdit = true;
      return this.generator.GenerateHierarchicalWidgetModel((IGetComponentArgs) args);
    }

    public PropertyValueGroupContainer GetPropertyValues(
      IGetWidgetModelContext context)
    {
      PropertyValueGroupContainer propertyValues = new PropertyValueGroupContainer();
      propertyValues.ComponentId = context.Id;
      IContainerSpecifics containerSpecifics = this.GetContainerSpecifics((IEditorContext) context);
      IControlManager manager = containerSpecifics.GetManager();
      object itemFromContext = containerSpecifics.GetItemFromContext((IEditorContext) context, manager);
      this.CheckOut(containerSpecifics, itemFromContext, true, "Page is not locked. Property values are available only when page is locked.", manager);
      GetComponentArgs args = new GetComponentArgs();
      args.ContainerResolver = this.GetContainerResolver((IEditorContext) context);
      args.ComponentId = context.Id;
      args.PreserveDynamicLinks = true;
      args.IsEdit = true;
      ComponentDto component = this.generator.GetComponent((IGetComponentArgs) args);
      propertyValues.Properties = (IEnumerable<PropertyValueContainer>) component.PropertiesModel.Properties.Select<KeyValuePair<string, object>, PropertyValueContainer>((Func<KeyValuePair<string, object>, PropertyValueContainer>) (p => new PropertyValueContainer()
      {
        Name = p.Key,
        Value = p.Value != null ? p.Value.ToString() : (string) null
      })).ToList<PropertyValueContainer>();
      propertyValues.Caption = component.Caption;
      propertyValues.PropertyLocalizationMode = this.GetPropertyValueLocalizationMode((IEditorContext) context);
      return propertyValues;
    }

    public void Unlock(IEditorContext context)
    {
      IContainerSpecifics containerSpecifics = this.GetContainerSpecifics(context);
      IControlManager manager = containerSpecifics.GetManager();
      containerSpecifics.Unlock(containerSpecifics.GetItemFromContext(context, manager), manager);
      manager.SaveChanges();
    }

    public EditorState Lock(ILockContext context)
    {
      IContainerSpecifics containerSpecifics = this.GetContainerSpecifics((IEditorContext) context);
      IControlManager manager = containerSpecifics.GetManager();
      object itemFromContext = containerSpecifics.GetItemFromContext((IEditorContext) context, manager);
      IControlsContainer controlsContainer = this.CheckOut(containerSpecifics, itemFromContext, false, (string) null, manager);
      manager.SaveChanges();
      EditorState state = this.GetState(containerSpecifics.GetVersion(controlsContainer, manager), (IEnumerable<ControlData>) new ContainerIterator().GetControlsFromAllContainers(controlsContainer), controlsContainer, true);
      state.HasChanged = context.Version != state.Version;
      return state;
    }

    public EditorState GetState(IEditorContext context)
    {
      IContainerSpecifics containerSpecifics = this.GetContainerSpecifics(context);
      IControlManager manager = containerSpecifics.GetManager();
      object itemFromContext = containerSpecifics.GetItemFromContext(context, manager);
      bool flag = containerSpecifics.CanEdit(itemFromContext, false);
      IControlsContainer container = this.GetContainerResolver(containerSpecifics, itemFromContext, manager).GetContainer(flag, (string) null);
      IList<ControlData> fromAllContainers = new ContainerIterator().GetControlsFromAllContainers(container);
      return this.GetState(containerSpecifics.GetVersion(container, manager), (IEnumerable<ControlData>) fromAllContainers, container, flag);
    }

    public string GetPropertyValueLocalizationMode(IEditorContext context)
    {
      IContainerSpecifics containerSpecifics = this.GetContainerSpecifics(context);
      IControlManager manager = containerSpecifics.GetManager();
      object itemFromContext = containerSpecifics.GetItemFromContext(context, manager);
      return SystemManager.CurrentContext.CurrentSite.Cultures.Length <= 1 || !containerSpecifics.SupportsSaveToAll(itemFromContext, manager) ? Enum.GetName(typeof (PropertyLocalizationType), (object) PropertyLocalizationType.Default) : Enum.GetName(typeof (PropertyLocalizationType), (object) PropertyLocalizationType.AllTranslations);
    }

    private EditorState GetState(
      int version,
      IEnumerable<ControlData> controls,
      IControlsContainer container,
      bool canEdit)
    {
      EditorState state = new EditorState()
      {
        Version = version,
        AddAllowed = canEdit,
        EditAllowed = canEdit
      };
      foreach (ControlData control in controls)
      {
        WidgetState widgetState = this.GetWidgetState(control, container, canEdit, canEdit);
        state.WidgetState.Add(widgetState);
      }
      return state;
    }

    private ControlData AddWidget(
      IAddWidgetContext context,
      IControlManager manager,
      IContainerSpecifics specifics,
      object item,
      IControlsContainer draft,
      IList<ControlData> allControls)
    {
      ControlData controlData = specifics.CreateControl(item, manager);
      Tuple<Guid, Guid> tuple = this.GuardWidgetPosition(context, controlData, (IEnumerable<ControlData>) allControls);
      controlData.PlaceHolder = context.PlaceholderName;
      controlData.SiblingId = tuple.Item2;
      controlData.ParentId = tuple.Item1;
      controlData.Editable = true;
      controlData.IsOverridedControl = false;
      controlData.ObjectType = context.Name;
      controlData.IsLayoutControl = false;
      controlData.SupportedPermissionSets = ControlData.ControlPermissionSets;
      controlData.SetDefaultPermissions(manager);
      controlData.SetPersistanceStrategy();
      ControlData controlData1 = draft.Controls.FirstOrDefault<ControlData>((Func<ControlData, bool>) (x => x.SiblingId == controlData.SiblingId && x.ParentId == controlData.ParentId && string.Equals(x.PlaceHolder, controlData.PlaceHolder, StringComparison.OrdinalIgnoreCase)));
      if (controlData1 != null)
        controlData1.SiblingId = controlData.Id;
      specifics.AddControl(draft, controlData);
      return controlData;
    }

    private ControlData AddWidget(
      IAddWidgetContext context,
      IControlManager manager,
      IContainerSpecifics specifics,
      object item,
      IControlsContainer draft,
      IList<ControlData> allControls,
      ControlData source)
    {
      ControlData controlData = this.AddWidget(context, manager, specifics, item, draft, allControls);
      if (source != null)
      {
        foreach (ControlProperty property1 in (IEnumerable<ControlProperty>) source.Properties)
        {
          ControlProperty property2 = manager.CreateProperty();
          manager.CopyProperty(property1, property2);
          property2.Language = property1.Language;
          controlData.Properties.Add(property2);
        }
        controlData.Strategy = source.Strategy;
        controlData.Caption = source.Caption;
      }
      return controlData;
    }

    private IList<ItemOperation> GetWidgetOperations(
      IGetOperationsContext context,
      IControlsContainer draft)
    {
      ControlData controlData = draft.Controls.FirstOrDefault<ControlData>((Func<ControlData, bool>) (x => x.Id == Guid.Parse(context.WidgetKey)));
      if (controlData == null)
        throw new ItemNotFoundException();
      List<ItemOperation> widgetOperations = new List<ItemOperation>();
      int num1 = 6;
      if (controlData.IsGranted("Controls", "EditControlProperties"))
      {
        widgetOperations.Add(new ItemOperation()
        {
          Title = "Edit",
          Name = "edit",
          Ordinal = -1,
          Category = new OperationCategory("General")
        });
        List<ItemOperation> itemOperationList1 = widgetOperations;
        ItemOperation itemOperation1 = new ItemOperation();
        itemOperation1.Title = "Duplicate";
        itemOperation1.Name = "duplicate";
        int num2 = num1;
        int num3 = num2 + 1;
        itemOperation1.Ordinal = num2;
        itemOperation1.Category = new OperationCategory("General");
        itemOperationList1.Add(itemOperation1);
        List<ItemOperation> itemOperationList2 = widgetOperations;
        ItemOperation itemOperation2 = new ItemOperation();
        itemOperation2.Title = "Copy settings";
        itemOperation2.Name = "copy-widget-settings";
        int num4 = num3;
        int num5 = num4 + 1;
        itemOperation2.Ordinal = num4;
        itemOperation2.Category = new OperationCategory("General");
        itemOperationList2.Add(itemOperation2);
        List<ItemOperation> itemOperationList3 = widgetOperations;
        ItemOperation itemOperation3 = new ItemOperation();
        itemOperation3.Title = "Paste settings";
        itemOperation3.Name = "paste-widget-settings";
        int num6 = num5;
        int num7 = num6 + 1;
        itemOperation3.Ordinal = num6;
        itemOperation3.Category = new OperationCategory("General");
        itemOperationList3.Add(itemOperation3);
        ItemOperation itemOperation4 = new ItemOperation();
        itemOperation4.Title = "Paste settings for all translations";
        itemOperation4.Name = "paste-widget-settings-for-all";
        int num8 = num7;
        num1 = num8 + 1;
        itemOperation4.Ordinal = num8;
        itemOperation4.Category = new OperationCategory("General");
        ItemOperation itemOperation5 = itemOperation4;
        switch (draft)
        {
          case PageDraft pageDraft:
            if (pageDraft.ParentPage.NavigationNode.LocalizationStrategy == LocalizationStrategy.Synced)
            {
              widgetOperations.Add(itemOperation5);
              break;
            }
            break;
          case TemplateDraft _:
            widgetOperations.Add(itemOperation5);
            break;
        }
      }
      if (controlData.IsGranted("Controls", "DeleteControl"))
      {
        ItemOperation itemOperation6 = new ItemOperation();
        itemOperation6.Title = "Delete";
        itemOperation6.Name = "delete";
        ItemOperation itemOperation7 = itemOperation6;
        int num9 = num1;
        int num10 = num9 + 1;
        itemOperation7.Ordinal = num9;
        itemOperation6.DetailedTitle = "Delete this widget?";
        itemOperation6.ExecuteOnServer = true;
        itemOperation6.PerformsDelete = true;
        itemOperation6.RequiresConfirmation = true;
        itemOperation6.Actions = new OperationAction[1]
        {
          new OperationAction()
          {
            Name = "Delete",
            Title = "Delete widget",
            Type = 2
          }
        };
        itemOperation6.Category = new OperationCategory("Delete");
        ItemOperation itemOperation8 = itemOperation6;
        widgetOperations.Add(itemOperation8);
      }
      return (IList<ItemOperation>) widgetOperations;
    }

    private IControlsContainer GetDraft(
      IEditorContext context,
      string lockedMessage)
    {
      IContainerSpecifics containerSpecifics = this.GetContainerSpecifics(context);
      IControlManager manager = containerSpecifics.GetManager();
      object itemFromContext = containerSpecifics.GetItemFromContext(context, manager);
      return this.CheckOut(containerSpecifics, itemFromContext, true, lockedMessage, manager);
    }

    private IControlsContainer CheckOut(
      IContainerSpecifics specifics,
      object item,
      bool ensureAlreadyLocked,
      string lockedMessage,
      IControlManager manager)
    {
      if (!specifics.CanEdit(item, ensureAlreadyLocked))
        throw new ErrorCodeException(lockedMessage, "LockedItem");
      return specifics.CheckOut(item, manager);
    }

    private WidgetState GetWidgetState(
      ControlData control,
      IControlsContainer container,
      bool isLockedByCurrentUser,
      bool addAllowed)
    {
      WidgetState widgetState = new WidgetState()
      {
        Key = control.Id.ToString(),
        Name = control.ObjectType
      };
      widgetState.MoveAllowed = (!(control.IsGranted("Controls", "MoveControl") & isLockedByCurrentUser) ? 0 : (container.Id == control.ContainerId ? 1 : 0)) != 0;
      widgetState.EditAllowed = (!(control.IsGranted("Controls", "EditControlProperties") & isLockedByCurrentUser) ? 0 : (container.Id == control.ContainerId ? 1 : 0)) != 0;
      if (!widgetState.EditAllowed)
        widgetState.LabelTooltip = "INHERITED FROM TEMPLATE";
      widgetState.DeleteAllowed = (!(control.IsGranted("Controls", "DeleteControl") & isLockedByCurrentUser) ? 0 : (container.Id == control.ContainerId ? 1 : 0)) != 0;
      return widgetState;
    }

    private WidgetOperationResult HandleCopy(IExecuteOperationContext context)
    {
      DefaultGetWidgetModelContext context1 = new DefaultGetWidgetModelContext();
      context1.EditedType = context.EditedType;
      context1.Key = context.Key;
      context1.Id = context.WidgetKey;
      PropertyValueGroupContainer propertyValues = this.GetPropertyValues((IGetWidgetModelContext) context1);
      Dictionary<string, PropertyContainer> dictionary1 = context.PropertyMetadata.ToDictionary<PropertyContainer, string, PropertyContainer>((Func<PropertyContainer, string>) (x => x.Name), (Func<PropertyContainer, PropertyContainer>) (y => y));
      Dictionary<string, string> dictionary2 = propertyValues.Properties.ToDictionary<PropertyValueContainer, string, string>((Func<PropertyValueContainer, string>) (x => x.Name), (Func<PropertyValueContainer, string>) (y => y.Value));
      foreach (KeyValuePair<string, PropertyContainer> keyValuePair in dictionary1)
      {
        object obj;
        bool result;
        if (dictionary2.ContainsKey(keyValuePair.Key) && keyValuePair.Value.Properties.Properties.TryGetValue(WidgetMetadataConstants.ExcludeFromCopy, out obj) && obj != null && bool.TryParse(obj.ToString(), out result) && result)
          dictionary2.Remove(keyValuePair.Key);
      }
      propertyValues.Properties = (IEnumerable<PropertyValueContainer>) dictionary2.Select<KeyValuePair<string, string>, PropertyValueContainer>((Func<KeyValuePair<string, string>, PropertyValueContainer>) (x => new PropertyValueContainer()
      {
        Name = x.Key,
        Value = x.Value
      })).ToList<PropertyValueContainer>();
      WidgetOperationResult widgetOperationResult = WidgetOperationResult.SuccessResult();
      widgetOperationResult.Message = Res.Get<PageResources>().SettingsCopiedSuccessfully;
      widgetOperationResult.Type = NotificationType.Success;
      List<PropertyValueContainer> propertyValueContainerList = new List<PropertyValueContainer>(propertyValues.Properties);
      if (!string.IsNullOrEmpty(propertyValues.Caption))
        propertyValueContainerList.Add(new PropertyValueContainer()
        {
          Name = "SfCaption",
          Value = propertyValues.Caption
        });
      widgetOperationResult.Properties = (IEnumerable<PropertyValueContainer>) propertyValueContainerList;
      return widgetOperationResult;
    }

    private WidgetOperationResult HandleDuplicate(
      IExecuteOperationContext context,
      IControlManager manager,
      IContainerSpecifics specifics,
      IControlsContainer draft,
      object item,
      ControlData control)
    {
      DefaultGetWidgetModelContext context1 = new DefaultGetWidgetModelContext();
      context1.EditedType = context.EditedType;
      context1.Key = context.Key;
      context1.Id = control.Id.ToString();
      ComponentDto source = this.HierarchicalWidgetModel((IGetWidgetModelContext) context1);
      string parentId = control.ParentId != Guid.Empty ? control.ParentId.ToString() : (string) null;
      IList<ControlData> fromAllContainers = new ContainerIterator().GetControlsFromAllContainers(draft);
      WidgetState widgetState = this.DuplicateWidgetRecursively(context, manager, specifics, draft, fromAllContainers, item, source, parentId, control.Id.ToString());
      manager.SaveChanges();
      WidgetOperationResult widgetOperationResult = WidgetOperationResult.SuccessResult();
      widgetOperationResult.State = widgetState;
      return widgetOperationResult;
    }

    private WidgetOperationResult HandlePaste(IExecuteOperationContext context)
    {
      if (context.Parameters != null)
      {
        PropertyLocalizationType localizationType = context.Name == "paste-widget-settings-for-all" ? PropertyLocalizationType.AllTranslations : PropertyLocalizationType.Default;
        List<PropertyValueContainer> propertyValueContainerList = new List<PropertyValueContainer>(context.Parameters);
        PropertyValueContainer propertyValueContainer = context.Parameters.FirstOrDefault<PropertyValueContainer>((Func<PropertyValueContainer, bool>) (x => x.Name == "SfCaption"));
        if (propertyValueContainer != null)
          propertyValueContainerList.Remove(propertyValueContainer);
        DefaultEditPropertiesWidgetContext context1 = new DefaultEditPropertiesWidgetContext();
        context1.EditedType = context.EditedType;
        context1.Key = context.Key;
        context1.PropertyGroup = new PropertyValueGroupContainer()
        {
          Caption = propertyValueContainer?.Value,
          ComponentId = context.WidgetKey,
          Properties = (IEnumerable<PropertyValueContainer>) propertyValueContainerList,
          PropertyLocalizationMode = localizationType.ToString()
        };
        context1.CleanPersistedProperties = true;
        this.EditProperties((IEditWidgetPropertiesContext) context1);
      }
      WidgetOperationResult widgetOperationResult = WidgetOperationResult.SuccessResult();
      widgetOperationResult.Message = Res.Get<PageResources>().SettingsPastedSuccessfully;
      widgetOperationResult.Type = NotificationType.Success;
      return widgetOperationResult;
    }

    private WidgetOperationResult HandleDelete(
      IContainerSpecifics specifics,
      IControlsContainer draft,
      ControlData control,
      IControlManager manager)
    {
      foreach (ControlData controlData in (IEnumerable<ControlData>) this.DeleteRecuresively((IList<ControlData>) draft.Controls.ToList<ControlData>(), control))
        manager.Delete(controlData);
      manager.SaveChanges();
      return WidgetOperationResult.SuccessResult();
    }

    private IList<ControlData> DeleteRecuresively(
      IList<ControlData> allControls,
      ControlData control)
    {
      List<ControlData> controlDataList = new List<ControlData>();
      controlDataList.Add(control);
      foreach (ControlData controlData in allControls.Where<ControlData>((Func<ControlData, bool>) (x => x.SiblingId == control.Id)))
        controlData.SiblingId = control.SiblingId;
      foreach (ControlData control1 in allControls.Where<ControlData>((Func<ControlData, bool>) (x => x.ParentId == control.Id)).ToList<ControlData>())
      {
        IList<ControlData> collection = this.DeleteRecuresively(allControls, control1);
        controlDataList.AddRange((IEnumerable<ControlData>) collection);
      }
      return (IList<ControlData>) controlDataList;
    }

    private WidgetState DuplicateWidgetRecursively(
      IExecuteOperationContext context,
      IControlManager manager,
      IContainerSpecifics specifics,
      IControlsContainer draft,
      IList<ControlData> allControls,
      object item,
      ComponentDto source,
      string parentId,
      string siblingKey)
    {
      ControlData control1 = manager.GetControl<ControlData>(source.Id);
      DefaultAddWidgetContext context1 = new DefaultAddWidgetContext();
      context1.EditedType = context.EditedType;
      context1.Key = context.Key;
      context1.ParentPlaceholderKey = parentId;
      context1.PlaceholderName = source.PlaceHolder;
      context1.SiblingKey = siblingKey;
      context1.Name = source.Name;
      ControlData control2 = this.AddWidget((IAddWidgetContext) context1, manager, specifics, item, draft, allControls, control1);
      allControls.Add(control2);
      string siblingKey1 = (string) null;
      foreach (ComponentDto child in (IEnumerable<ComponentDto>) source.Children)
      {
        if (child.SiblingId == Guid.Empty)
          siblingKey1 = (string) null;
        siblingKey1 = this.DuplicateWidgetRecursively(context, manager, specifics, draft, allControls, item, child, control2.Id.ToString(), siblingKey1).Key;
      }
      return this.GetWidgetState(control2, draft, true, true);
    }

    private Tuple<Guid, Guid> GuardWidgetPosition(
      IAddWidgetContext context,
      ControlData controlData,
      IEnumerable<ControlData> allControls)
    {
      Guid parentId;
      if (Guid.TryParse(context.ParentPlaceholderKey, out parentId) && parentId != Guid.Empty && !allControls.Any<ControlData>((Func<ControlData, bool>) (x => x.Id == parentId)))
        throw new InvalidOperationException("The widget that ParentPlaceholderKey points to does not exist");
      Guid siblingId;
      if (Guid.TryParse(context.SiblingKey, out siblingId) && siblingId != Guid.Empty)
      {
        if (!allControls.Any<ControlData>((Func<ControlData, bool>) (x => x.Id == siblingId && x.PlaceHolder == context.PlaceholderName && x.ParentId == parentId)))
          throw new InvalidOperationException(string.Format("The provided sibling with ID: {0} doesn't exist in the expected parent {1} and placeholder {2}.", (object) context.Id, (object) parentId, (object) context.PlaceholderName));
        if (controlData.Id == siblingId)
          throw new InvalidOperationException("The widget with id " + context.Id + " has a SiblingId value that points to itslef, which is not allowed");
      }
      return new Tuple<Guid, Guid>(parentId, siblingId);
    }

    private IContainerSpecifics GetContainerSpecifics(IEditorContext context)
    {
      if (object.Equals((object) typeof (PageNode), (object) context.EditedType))
        return (IContainerSpecifics) new PageSpecifics();
      return object.Equals((object) typeof (PageTemplate), (object) context.EditedType) ? (IContainerSpecifics) new PageTemplateSpecifics() : (IContainerSpecifics) null;
    }

    private IContainerResolver GetContainerResolver(IEditorContext context)
    {
      IContainerSpecifics containerSpecifics = this.GetContainerSpecifics(context);
      IControlManager manager = containerSpecifics.GetManager();
      object itemFromContext = containerSpecifics.GetItemFromContext(context, manager);
      return this.GetContainerResolver(containerSpecifics, itemFromContext, manager);
    }

    private IContainerResolver GetContainerResolver(
      IContainerSpecifics specifics,
      object item,
      IControlManager manager)
    {
      switch (specifics)
      {
        case PageSpecifics _:
          SystemManager.CurrentHttpContext.Request.RequestContext.RouteData.DataTokens["SiteMapNode"] = item;
          return (IContainerResolver) new PageDataContainerResolver(item as PageSiteNode);
        case PageTemplateSpecifics _:
          return (IContainerResolver) new TemplateContainerResolver(item as PageTemplate, manager as PageManager);
        default:
          return (IContainerResolver) null;
      }
    }
  }
}
