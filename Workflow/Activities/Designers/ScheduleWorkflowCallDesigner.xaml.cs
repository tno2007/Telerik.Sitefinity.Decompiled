﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.Designers.ScheduleWorkflowCallDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities.Presentation;
using System.Activities.Presentation.View;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Telerik.Sitefinity.Workflow.Activities.Designers
{
  /// <summary>ScheduleWorkflowCallDesigner</summary>
  public partial class ScheduleWorkflowCallDesigner : ActivityDesigner, IComponentConnector
  {
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal TextBox tbOperaionName;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal ExpressionTextBox tbExecuteOn;
    private bool _contentLoaded;

    public ScheduleWorkflowCallDesigner() => this.InitializeComponent();

    /// <summary>InitializeComponent</summary>
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Telerik.Sitefinity;component/workflow/activities/designers/scheduleworkflowcalldesigner.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      if (connectionId != 1)
      {
        if (connectionId == 2)
          this.tbExecuteOn = (ExpressionTextBox) target;
        else
          this._contentLoaded = true;
      }
      else
        this.tbOperaionName = (TextBox) target;
    }
  }
}
