// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Generators.SetComponentArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;

namespace Telerik.Sitefinity.Renderer.Generators
{
  internal class SetComponentArgs : GeneratorArgs, ISetComponentArgs, IGeneratorArgs
  {
    public string ComponentId { get; set; }

    public PropertyValueGroupContainer PropertiesGroup { get; set; }

    public bool CleanPersistedProperites { get; set; }
  }
}
