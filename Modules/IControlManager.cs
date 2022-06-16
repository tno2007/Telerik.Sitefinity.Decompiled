// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.IControlManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>Provides API for managing Sitefinity controls.</summary>
  public interface IControlManager : IContentManager, IManager, IDisposable, IProviderResolver
  {
    /// <summary>
    /// Creates a new persistent class for storing serialized control data and serializes the provided web control in to the created instance.
    /// </summary>
    /// <param name="control">The control instance</param>
    /// <param name="placeHolder">The place holder to which the control will be added.</param>
    T CreateControl<T>(Control control, string placeHolder, bool isBackendObject = false) where T : ControlData;

    /// <summary>
    /// Creates a new persistent class for storing serialized control data and serializes the provided user control in to the created instance.
    /// </summary>
    /// <param name="controlPath">The path to the user control</param>
    /// <param name="placeHolder">The place holder to which the control will be added.</param>
    T CreateControl<T>(string controlPath, string placeHolder, bool isBackendObject = false) where T : ControlData;

    /// <summary>
    /// Reads all properties from the component and creates persistent elements for them.
    /// </summary>
    /// <param name="component">The component from which the properties should be read.</param>
    /// <param name="objectData">The persistent object that represents the object data.</param>
    void ReadProperties(object component, ObjectData objectData);

    /// <summary>
    /// Reads all properties from the component and creates persistent elements for them.
    /// </summary>
    /// <param name="component">The component from which the properties should be read.</param>
    /// <param name="objectData">The persistent object that represents the object data.</param>
    /// <param name="language">The language.</param>
    /// <param name="defaultComponentInstance">The default component instance.</param>
    void ReadProperties(
      object component,
      ObjectData objectData,
      CultureInfo language,
      object defaultComponentInstance = null);

    /// <summary>
    /// Reads the properties recursively until the whole object graph has been read.
    /// </summary>
    /// <param name="component">The component on which the properties to be read are declared.</param>
    /// <param name="parentPropertyData">The persistent data of the parent property.</param>
    void ReadProperties(object component, ControlProperty parentPropertyData);

    /// <summary>
    /// Sets a unique control ID for the provided page or template.
    /// </summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="controlData">The control data.</param>
    void SetControlId(IControlsContainer pageData, ObjectData controlData, CultureInfo language = null);

    /// <summary>
    /// Deserializes the provided data and creates a new instance of a custom or user control.
    /// </summary>
    /// <param name="controlData">The control serialized data.</param>
    /// <returns>An instance of the control.</returns>
    Control LoadControl(ObjectData controlData, CultureInfo culture = null);

    /// <summary>
    /// Deserializes the provided data and creates a new instance of the specified object.
    /// </summary>
    /// <param name="controlData">The serialized data.</param>
    /// <returns>An instance of the object.</returns>
    object LoadObject(ObjectData objectData);

    /// <summary>
    /// Makes a deep copy of controls from the source collection to the target list.
    /// </summary>
    /// <typeparam name="SrcT">The source generic type.</typeparam>
    /// <typeparam name="TrgT">The target generic type.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    void CopyControls<SrcT, TrgT>(IEnumerable<SrcT> source, IList<TrgT> target)
      where SrcT : ControlData
      where TrgT : ControlData;

    /// <summary>
    /// Copies all properties of the source control to the target control.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    void CopyControl(ControlData source, ControlData target);

    /// <summary>
    /// Makes a deep copy of the objects from the source collection to the target list.
    /// </summary>
    /// <typeparam name="SrcT">The source generic type.</typeparam>
    /// <typeparam name="TrgT">The target generic type.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    void CopyObjects<SrcT, TrgT>(IEnumerable<SrcT> source, IList<TrgT> target)
      where SrcT : ObjectData
      where TrgT : ObjectData;

    /// <summary>
    /// Copies all properties of the source object to the target object.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    void CopyObject(ObjectData source, ObjectData target);

    /// <summary>
    /// Creates new persistent class for storing object serialized data.
    /// </summary>
    /// <returns></returns>
    ObjectData CreateObjectData(bool isBackendObject = false);

    /// <summary>Creates new control.</summary>
    /// <returns>The new control.</returns>
    T CreateControl<T>(bool isBackendObject = false) where T : ObjectData;

    /// <summary>Creates new control with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new control.</param>
    /// <returns>The new control.</returns>
    T CreateControl<T>(Guid id, bool isBackendObject = false) where T : ObjectData;

    /// <summary>Gets the control with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>Control data persistent object.</returns>
    T GetControl<T>(Guid id) where T : ObjectData;

    /// <summary>Gets a query for controls.</summary>
    /// <returns>The query for controls.</returns>
    IQueryable<T> GetControls<T>() where T : ObjectData;

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    void Delete(ControlData item);

    /// <summary>
    /// Copies the source language properties to the target language for the specified object.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <param name="sourceLanguage">The source language.</param>
    /// <param name="targetLanguage">The target language.</param>
    /// <param name="cleanOldTargetLanguageProperties">Whether to remove all properties for the target language before copying.</param>
    void CopyProperties(
      ObjectData obj,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool cleanOldTargetLanguageProperties = true);

    /// <summary>
    /// Makes a deep copy of properties from the source collection to the target list.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    void CopyProperties(IEnumerable<ControlProperty> source, IList<ControlProperty> target);

    /// <summary>Copies the property.</summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    void CopyProperty(ControlProperty source, ControlProperty target);

    /// <summary>Creates new page template.</summary>
    /// <returns>The new page template.</returns>
    ControlProperty CreateProperty();

    /// <summary>Creates new page template with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new page template.</param>
    /// <returns>The new page template.</returns>
    ControlProperty CreateProperty(Guid id);

    /// <summary>Gets the page template with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>A page template.</returns>
    ControlProperty GetProperty(Guid id);

    /// <summary>Gets a query for page templates.</summary>
    /// <returns>The query for page templates.</returns>
    IQueryable<ControlProperty> GetProperties();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The page template to delete.</param>
    void Delete(ControlProperty item);

    /// <summary>
    /// Makes a deep copy of the presentation data from the source collection to the target list.
    /// </summary>
    /// <typeparam name="SrcT">The source generic type.</typeparam>
    /// <typeparam name="TrgT">The target generic type.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    void CopyPresentation<SrcT, TrgT>(IEnumerable<SrcT> source, IList<TrgT> target)
      where SrcT : PresentationData
      where TrgT : PresentationData;

    /// <summary>
    /// Copies the information from the source presentation item to the target.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    void CopyPresentationItem(PresentationData source, PresentationData target);

    /// <summary>
    /// Creates new object for storing presentation information.
    /// </summary>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    T CreatePresentationItem<T>() where T : PresentationData;

    /// <summary>
    /// Creates new object for storing presentation information with the specified ID.
    /// </summary>
    /// <param name="pageId">The pageId of the new item.</param>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    T CreatePresentationItem<T>(Guid id) where T : PresentationData;

    /// <summary>Gets the item with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    T GetPresentationItem<T>(Guid id) where T : PresentationData;

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> items.
    /// </summary>
    /// <returns>The query for page templates.</returns>
    IQueryable<T> GetPresentationItems<T>() where T : PresentationData;

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    void Delete(PresentationData item);

    /// <summary>
    /// Set default permissions for controls (they do not inherit, but have hard-coded preset ones)
    /// </summary>
    /// <param name="ctrlData">The CTRL data.</param>
    /// <param name="manager">The manager.</param>
    void SetControlDefaultPermissions(ControlData ctrlData);
  }
}
