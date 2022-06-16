// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ContentServiceBase`3
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>
  /// The WCF web service that is used to work with all types that inherit from base <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />
  /// class.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public abstract class ContentServiceBase<TContent, TContentViewModel, TContentManager> : 
    ContentServiceBase<TContent, TContent, TContentViewModel, TContentViewModel, TContentManager>
    where TContent : Content
    where TContentViewModel : ContentViewModelBase
    where TContentManager : class, IContentManager, IContentLifecycleManager<TContent>
  {
  }
}
