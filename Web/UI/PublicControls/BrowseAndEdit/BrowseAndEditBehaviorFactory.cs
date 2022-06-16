// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditBehaviorFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit
{
  public static class BrowseAndEditBehaviorFactory
  {
    public static IBrowseAndEditStrategy CreateConfigurationStrategy(
      string unityMappingName = null)
    {
      return string.IsNullOrEmpty(unityMappingName) ? ObjectFactory.Resolve<IBrowseAndEditStrategy>() : ObjectFactory.Resolve<IBrowseAndEditStrategy>(unityMappingName);
    }
  }
}
