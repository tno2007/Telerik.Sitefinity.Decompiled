// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Web.Services.DiagnosticsOperationProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Renderer.Diagnostics;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;

namespace Telerik.Sitefinity.Renderer.Web.Services
{
  internal class DiagnosticsOperationProvider : IOperationProvider
  {
    internal static bool EnableRendererDiagnostics { get; set; }

    public IEnumerable<OperationData> GetOperations(Type clrType)
    {
      bool result;
      if (!DiagnosticsOperationProvider.EnableRendererDiagnostics && (!bool.TryParse(ConfigurationManager.AppSettings["sf:enableRendererDiagnostics"], out result) || !result))
        return (IEnumerable<OperationData>) Array.Empty<OperationData>();
      if (!(clrType == (Type) null))
        return (IEnumerable<OperationData>) Array.Empty<OperationData>();
      List<OperationData> operations = new List<OperationData>()
      {
        OperationData.Create<RendererData, DiagnosticResult>(new Func<OperationContext, RendererData, DiagnosticResult>(this.Diagnostics))
      };
      foreach (OperationData operationData in operations)
      {
        operationData.OperationType = OperationType.Unbound;
        operationData.IsRead = false;
        operationData.IsAllowedUnauthorized = true;
      }
      return (IEnumerable<OperationData>) operations;
    }

    private DiagnosticResult Diagnostics(
      OperationContext context,
      RendererData data)
    {
      IEnumerable<IRendererIntegration> rendererIntegrations = ObjectFactory.Container.ResolveAll(typeof (IRendererIntegration)).OfType<IRendererIntegration>();
      List<DiagnosticsDto> diagnosticsDtoList = new List<DiagnosticsDto>();
      foreach (IRendererIntegration rendererIntegration in rendererIntegrations)
      {
        IEnumerable<DiagnosticsDto> collection = rendererIntegration.Run(data);
        diagnosticsDtoList.AddRange(collection);
      }
      foreach (DiagnosticsDto diagnosticsDto in diagnosticsDtoList)
      {
        if (string.IsNullOrEmpty(diagnosticsDto.Message))
          diagnosticsDto.Success = true;
      }
      return new DiagnosticResult()
      {
        Diagnostics = (IList<DiagnosticsDto>) diagnosticsDtoList
      };
    }
  }
}
