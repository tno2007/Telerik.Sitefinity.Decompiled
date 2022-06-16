// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.ZoneEditorOperationValidationAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>
  /// Attribute which interecepts the service method call and applies specific validation for the zone editor service
  /// Checks if the user is authenticated and if the content is locked by another user
  /// </summary>
  public class ZoneEditorOperationValidationAttribute : 
    Attribute,
    IOperationBehavior,
    IParameterInspector
  {
    private int parameterOrdinalToInspect;
    private DesignMediaType? mediaType;
    private bool inspectDraftContentId;
    private string operationName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.ZoneEditorOperationValidationAttribute" /> class.
    /// </summary>
    public ZoneEditorOperationValidationAttribute()
      : this(false, new DesignMediaType?(), 0)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.ZoneEditorOperationValidationAttribute" /> class.
    /// </summary>
    public ZoneEditorOperationValidationAttribute(
      bool inspectDraftContentId,
      DesignMediaType mediaType)
      : this(inspectDraftContentId, mediaType, 0)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.ZoneEditorOperationValidationAttribute" /> class.
    /// </summary>
    /// <param name="inspectDraftContentId">if set to <c>true</c> will verify if the content is locked by somebody else</param>
    /// <param name="mediaType">Type of the media.</param>
    /// <param name="parameterOrdinalToInspect">The parameter ordinal to inspect.</param>
    public ZoneEditorOperationValidationAttribute(
      bool inspectDraftContentId,
      DesignMediaType mediaType,
      int parameterOrdinalToInspect)
      : this(inspectDraftContentId, new DesignMediaType?(mediaType), parameterOrdinalToInspect)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.ZoneEditorOperationValidationAttribute" /> class.
    /// </summary>
    /// <param name="inspectDraftContentId">if set to <c>true</c> will verify if the content is locked by somebody else</param>
    /// <param name="mediaType">Type of the media.</param>
    /// <param name="parameterOrdinalToInspect">The parameter ordinal to inspect.</param>
    public ZoneEditorOperationValidationAttribute(
      bool inspectDraftContentId,
      DesignMediaType? mediaType,
      int parameterOrdinalToInspect)
    {
      this.parameterOrdinalToInspect = parameterOrdinalToInspect;
      this.mediaType = mediaType;
      this.inspectDraftContentId = inspectDraftContentId;
    }

    /// <summary>Implement to confirm that the operation meets some intended criteria.
    /// </summary>
    /// <param name="operationDescription">The operation being examined. Use for examination
    /// only. If the operation description is modified, the results are undefined.</param>
    public void Validate(OperationDescription operationDescription)
    {
    }

    /// <summary>Implements a modification or extension of the service across an operation.
    /// </summary>
    /// <param name="operationDescription">The operation being examined. Use for examination
    /// only. If the operation description is modified, the results are undefined.</param>
    /// <param name="dispatchOperation">The run-time object that exposes customization
    /// properties for the operation described by <paramref name="operationDescription" />.
    /// </param>
    public void ApplyDispatchBehavior(
      OperationDescription operationDescription,
      DispatchOperation dispatchOperation)
    {
      this.operationName = operationDescription.Name;
      dispatchOperation.ParameterInspectors.Add((IParameterInspector) this);
    }

    /// <summary>Implements a modification or extension of the client across an operation.
    /// </summary>
    /// <param name="operationDescription">The operation being examined. Use for examination
    /// only. If the operation description is modified, the results are undefined.</param>
    /// <param name="clientOperation">The run-time object that exposes customization
    /// properties for the operation described by <paramref name="operationDescription" />.
    /// </param>
    public void ApplyClientBehavior(
      OperationDescription operationDescription,
      ClientOperation clientOperation)
    {
      throw new NotImplementedException();
    }

    /// <summary>Implement to pass data at runtime to bindings to support custom behavior.
    /// </summary>
    /// <param name="operationDescription">The operation being examined. Use for examination
    /// only. If the operation description is modified, the results are undefined.</param>
    /// <param name="bindingParameters">The collection of objects that binding elements
    /// require to support the behavior.</param>
    public void AddBindingParameters(
      OperationDescription operationDescription,
      BindingParameterCollection bindingParameters)
    {
    }

    /// <summary>Called before client calls are sent and after service responses are
    /// returned.</summary>
    /// <returns>The correlation state that is returned as the <paramref name="correlationState" />
    /// parameter in <see cref="M:System.ServiceModel.Dispatcher.IParameterInspector.AfterCall(System.String,System.Object[],System.Object,System.Object)" />.
    /// Return null if you do not intend to use correlation state.</returns>
    /// <param name="operationName">The name of the operation.</param>
    /// <param name="inputs">The objects passed to the method by the client.</param>
    public object BeforeCall(string operationName, object[] inputs)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      if (!this.inspectDraftContentId)
      {
        foreach (object input in inputs)
        {
          if (input is ZoneEditorWebServiceArgs)
            (input as ZoneEditorWebServiceArgs).ValidateChange(operationName);
        }
      }
      else if (inputs.Length > this.parameterOrdinalToInspect)
      {
        string input = inputs[this.parameterOrdinalToInspect] as string;
        if (!ControlUtilities.IsGuid(input))
          throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid ID format.", (Exception) new ArgumentException());
        ZoneEditorValidationExtensions.ValidateChange(new Guid(input), this.mediaType.Value, operationName);
      }
      return (object) null;
    }

    /// <summary>Called after client calls are returned and before service responses
    /// are sent.</summary>
    /// <param name="operationName">The name of the invoked operation.</param>
    /// <param name="outputs">Any output objects.</param>
    /// <param name="returnValue">The return value of the operation.</param>
    /// <param name="correlationState">Any correlation state returned from the <see cref="M:System.ServiceModel.Dispatcher.IParameterInspector.BeforeCall(System.String,System.Object[])" />
    /// method, or null. </param>
    public void AfterCall(
      string operationName,
      object[] outputs,
      object returnValue,
      object correlationState)
    {
    }
  }
}
