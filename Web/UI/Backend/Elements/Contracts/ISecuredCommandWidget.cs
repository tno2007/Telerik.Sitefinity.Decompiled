// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.ISecuredCommandWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  public interface ISecuredCommandWidget
  {
    /// <summary>
    /// Gets or sets the permission set related to the security action which represents this widget's command.
    /// </summary>
    /// <value>The permission set related to the security action which represents this widget's command.</value>
    string PermissionSet { get; set; }

    /// <summary>
    /// Gets or sets the name of the action which represents this widget's command.
    /// </summary>
    /// <value>The name of the action which represents this widget's command.</value>
    string ActionName { get; set; }

    /// <summary>The type of the secured object related to the widget</summary>
    string RelatedSecuredObjectTypeName { get; set; }

    /// <summary>The Id of the secured object related to the widget</summary>
    string RelatedSecuredObjectId { get; set; }

    /// <summary>
    /// (Optional) The provider name of the secured object related to the widget.
    /// If no provider is given, the default one is used.
    /// </summary>
    string RelatedSecuredObjectProviderName { get; set; }

    /// <summary>
    /// (Optional) The type name of the manager of the secured object related to the widget.
    /// If no manager is given, the configured mapped manager is used.
    /// </summary>
    string RelatedSecuredObjectManagerTypeName { get; set; }

    /// <summary>
    /// Determines whether this widget's command is allowed according to its permission set, action and secured object.
    /// </summary>
    /// <param name="relatedSecuredObject">The related secured object.</param>
    /// <returns>
    /// 	<c>true</c> if this widget's command is allowed according to the set permission set, action and secured object; otherwise, <c>false</c>
    /// </returns>
    bool IsAllowed(ISecuredObject relatedSecuredObject);
  }
}
