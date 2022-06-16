// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.IAnyPropertyEditorFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Fluent.AnyContent
{
  public interface IAnyPropertyEditorFacade<TParentFacade> where TParentFacade : class
  {
    void SetInitialState(TParentFacade parentFacade, Content item, IManager manager);

    IAnyPropertyEditorFacade<TParentFacade> SetValue(
      string propertyName,
      object value);

    IAnyPropertyEditorFacade<TParentFacade> SetValue(
      string propertyName,
      object value,
      bool caseInsensitive);

    IAnyPropertyEditorFacade<TParentFacade> GetValueAndContinue(
      string propertyName,
      out object value);

    IAnyPropertyEditorFacade<TParentFacade> GetValueAndContinue(
      string propertyName,
      out object value,
      bool caseInsensitive);

    object GetValue(string propertyName);

    object GetValue(string propertyName, bool caseInsensitive);

    TParentFacade Done(bool recompileUrls);

    TParentFacade Done();
  }
}
