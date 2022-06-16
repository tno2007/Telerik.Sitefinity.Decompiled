// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.WcfHelpers.AllowPageDynamicFieldsAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Telerik.Sitefinity.Data.WcfHelpers
{
  /// <summary>
  /// Applied to WCF service classes that want to expose types that have dynamic fields
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
  public sealed class AllowPageDynamicFieldsAttribute : 
    Attribute,
    IContractBehavior,
    IOperationBehavior,
    IWsdlExportExtension
  {
    /// <summary>
    /// Configures any binding elements to support the contract behavior.
    /// </summary>
    /// <param name="contractDescription">The contract description to modify.</param>
    /// <param name="endpoint">The endpoint to modify.</param>
    /// <param name="bindingParameters">The objects that binding elements require to support the behavior.</param>
    public void AddBindingParameters(
      ContractDescription contractDescription,
      ServiceEndpoint endpoint,
      BindingParameterCollection bindingParameters)
    {
    }

    /// <summary>
    /// Implements a modification or extension of the client across a contract.
    /// </summary>
    /// <param name="contractDescription">The contract description for which the extension is intended.</param>
    /// <param name="endpoint">The endpoint.</param>
    /// <param name="clientRuntime">The client runtime.</param>
    public void ApplyClientBehavior(
      ContractDescription contractDescription,
      ServiceEndpoint endpoint,
      ClientRuntime clientRuntime)
    {
      foreach (OperationDescription operation in (Collection<OperationDescription>) contractDescription.Operations)
        AllowPageDynamicFieldsAttribute.ApplyDataContractSurrogate(operation);
    }

    /// <summary>
    /// Implements a modification or extension of the client across a contract.
    /// </summary>
    /// <param name="contractDescription">The contract description to be modified.</param>
    /// <param name="endpoint">The endpoint that exposes the contract.</param>
    /// <param name="dispatchRuntime">The dispatch runtime that controls service execution.</param>
    public void ApplyDispatchBehavior(
      ContractDescription contractDescription,
      ServiceEndpoint endpoint,
      DispatchRuntime dispatchRuntime)
    {
      foreach (OperationDescription operation in (Collection<OperationDescription>) contractDescription.Operations)
        AllowPageDynamicFieldsAttribute.ApplyDataContractSurrogate(operation);
    }

    /// <summary>
    /// Implement to confirm that the contract and endpoint can support the contract
    ///     behavior.
    /// </summary>
    /// <param name="contractDescription">The contract to validate.</param>
    /// <param name="endpoint">The endpoint to validate.</param>
    public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
    {
    }

    /// <summary>
    /// Implement to pass data at runtime to bindings to support custom behavior.
    /// </summary>
    /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation
    /// description is modified, the results are undefined.</param>
    /// <param name="bindingParameters"> The collection of objects that binding elements require to support the behavior.</param>
    public void AddBindingParameters(
      OperationDescription operationDescription,
      BindingParameterCollection bindingParameters)
    {
    }

    /// <summary>
    /// Implements a modification or extension of the client across an operation.
    /// </summary>
    /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation
    /// description is modified, the results are undefined.</param>
    /// <param name="clientOperation">The run-time object that exposes customization properties for the operation
    /// described by operationDescription.</param>
    public void ApplyClientBehavior(
      OperationDescription operationDescription,
      ClientOperation clientOperation)
    {
      AllowPageDynamicFieldsAttribute.ApplyDataContractSurrogate(operationDescription);
    }

    /// <summary>
    /// Implements a modification or extension of the service across an operation.
    /// </summary>
    /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation
    /// description is modified, the results are undefined.</param>
    /// <param name="dispatchOperation">The run-time object that exposes customization properties for the operation
    /// described by operationDescription.</param>
    public void ApplyDispatchBehavior(
      OperationDescription operationDescription,
      DispatchOperation dispatchOperation)
    {
      AllowPageDynamicFieldsAttribute.ApplyDataContractSurrogate(operationDescription);
    }

    /// <summary>
    ///  Implement to confirm that the operation meets some intended criteria.
    /// </summary>
    /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation
    /// description is modified, the results are undefined.</param>
    public void Validate(OperationDescription operationDescription)
    {
    }

    /// <summary>
    /// Writes custom Web Services Description Language (WSDL) elements into the
    ///     generated WSDL for a contract.
    /// </summary>
    /// <param name="exporter">The System.ServiceModel.Description.WsdlExporter that exports the contract
    /// information.</param>
    /// <param name="context">Provides mappings from exported WSDL elements to the contract description.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="exporter" /> is <c>null</c>.</exception>
    public void ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
    {
      if (exporter == null)
        throw new ArgumentNullException(nameof (exporter));
      object obj;
      XsdDataContractExporter contractExporter;
      if (!exporter.State.TryGetValue((object) typeof (XsdDataContractExporter), out obj))
      {
        contractExporter = new XsdDataContractExporter(exporter.GeneratedXmlSchemas);
        exporter.State.Add((object) typeof (XsdDataContractExporter), (object) contractExporter);
      }
      else
        contractExporter = (XsdDataContractExporter) obj;
      if (contractExporter.Options == null)
        contractExporter.Options = new ExportOptions();
      if (contractExporter.Options.DataContractSurrogate != null)
        return;
      contractExporter.Options.DataContractSurrogate = (IDataContractSurrogate) new PageDynamicFieldsDataContractSurrogate();
    }

    /// <summary>
    /// Writes custom Web Services Description Language (WSDL) elements into the
    ///     generated WSDL for an endpoint.
    /// </summary>
    /// <param name="exporter">The System.ServiceModel.Description.WsdlExporter that exports the endpoint
    /// information.</param>
    /// <param name="context">rovides mappings from exported WSDL elements to the endpoint description.</param>
    public void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
    {
    }

    /// <summary>
    /// Applies the dynamic fields surrogate to different wcf service operations
    /// </summary>
    /// <param name="description">Operation to apply our surrogate to</param>
    private static void ApplyDataContractSurrogate(OperationDescription description)
    {
      DataContractSerializerOperationBehavior operationBehavior = description.Behaviors.Find<DataContractSerializerOperationBehavior>();
      if (operationBehavior == null || operationBehavior.DataContractSurrogate != null)
        return;
      operationBehavior.DataContractSurrogate = (IDataContractSurrogate) new PageDynamicFieldsDataContractSurrogate();
    }
  }
}
