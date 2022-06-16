// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IExternalPageFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// Defines a contract for ExternalPageField definition and config element.
  /// </summary>
  public interface IExternalPageFieldDefinition : IFieldDefinition, IDefinition
  {
    /// <summary>Gets the is-external-page flag.</summary>
    /// <value>The is-external-page choice field definition.</value>
    IChoiceFieldDefinition IsExternalPageChoiceFieldDefinition { get; }

    /// <summary>The guid of the site page to be redirected to.</summary>
    Guid InternalPageId { get; }

    /// <summary>The url of the external page to be redirected to.</summary>
    ITextFieldDefinition ExternalPageUrlFieldDefinition { get; }

    /// <summary>If set the page will be open in a new browser window.</summary>
    IChoiceFieldDefinition OpenInNewWindowChoiceFieldDefinition { get; }
  }
}
