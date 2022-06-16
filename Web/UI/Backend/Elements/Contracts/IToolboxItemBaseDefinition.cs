// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IToolboxItemBaseDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  public interface IToolboxItemBaseDefinition
  {
    string ContainerId { get; set; }

    string CssClass { get; set; }

    string ItemTemplatePath { get; set; }

    bool Visible { get; set; }

    string WrapperTagCssClass { get; set; }

    string WrapperTagId { get; set; }

    string WrapperTagName { get; set; }
  }
}
