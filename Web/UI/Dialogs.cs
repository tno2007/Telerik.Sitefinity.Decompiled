// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Dialogs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Helper class for working with Sitefinity dialogs.</summary>
  public static class Dialogs
  {
    /// <summary>Gets the dialog.</summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public static DialogBase GetDialog(string name) => ObjectFactory.Container.Resolve<DialogBase>(name);

    /// <summary>Registers a dialog class with the type system.</summary>
    /// <typeparam name="TDialog">The type of the dialog to register.</typeparam>
    public static void RegisterDialog<TDialog>() where TDialog : DialogBase, new() => Dialogs.RegisterDialog(typeof (TDialog));

    /// <summary>Registers a dialog class with the type system.</summary>
    /// <param name="dialogType">The type of the dialog to register.</param>
    public static void RegisterDialog(Type dialogType) => ObjectFactory.Container.RegisterType(typeof (DialogBase), dialogType, dialogType.Name);
  }
}
