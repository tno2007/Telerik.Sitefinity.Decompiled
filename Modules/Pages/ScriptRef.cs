// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.ScriptRef
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>Defines script reference files.</summary>
  [Flags]
  public enum ScriptRef : ulong
  {
    /// <summary>No reference</summary>
    Empty = 0,
    /// <summary>Microsoft AJAX</summary>
    MicrosoftAjax = 1,
    /// <summary>Microsoft AJAX ADO.Net</summary>
    [Obsolete("No more supported by Microsoft")] MicrosoftAjaxAdoNet = 2,
    /// <summary>Microsoft AJAX Application Services</summary>
    MicrosoftAjaxApplicationServices = 4,
    /// <summary>Microsoft AJAX Component Model</summary>
    MicrosoftAjaxComponentModel = 8,
    /// <summary>Microsoft AJAX Core</summary>
    MicrosoftAjaxCore = 16, // 0x0000000000000010
    /// <summary>Microsoft AJAX Data Context</summary>
    [Obsolete("No more supported by Microsoft")] MicrosoftAjaxDataContext = 32, // 0x0000000000000020
    /// <summary>Microsoft AJAX Globalization</summary>
    MicrosoftAjaxGlobalization = 64, // 0x0000000000000040
    /// <summary>Microsoft AJAX</summary>
    MicrosoftAjaxHistory = 128, // 0x0000000000000080
    /// <summary>Microsoft AJAX Network</summary>
    MicrosoftAjaxNetwork = 256, // 0x0000000000000100
    /// <summary>Microsoft AJAX Serialization</summary>
    MicrosoftAjaxSerialization = 512, // 0x0000000000000200
    /// <summary>Microsoft AJAX Templates</summary>
    MicrosoftAjaxTemplates = 1024, // 0x0000000000000400
    /// <summary>Microsoft AJAX Timer</summary>
    MicrosoftAjaxTimer = 2048, // 0x0000000000000800
    /// <summary>Microsoft AJAX Web Forms</summary>
    MicrosoftAjaxWebForms = 4096, // 0x0000000000001000
    /// <summary>Microsoft AJAX Web Services</summary>
    MicrosoftAjaxWebServices = 8192, // 0x0000000000002000
    /// <summary>JQuery</summary>
    JQuery = 16384, // 0x0000000000004000
    /// <summary>Kendo.all</summary>
    KendoAll = 32768, // 0x0000000000008000
    /// <summary>JQuery Validate</summary>
    JQueryValidate = 65536, // 0x0000000000010000
    /// <summary>Mootools</summary>
    Mootools = 131072, // 0x0000000000020000
    /// <summary>Prototype</summary>
    Prototype = 262144, // 0x0000000000040000
    /// <summary>Sitefinity's DialogManager</summary>
    DialogManager = 524288, // 0x0000000000080000
    /// <summary>Telerik.Sitefinity javascript framework</summary>
    TelerikSitefinity = 1048576, // 0x0000000000100000
    /// <summary>JQuery UI</summary>
    JQueryUI = 2097152, // 0x0000000000200000
    /// <summary>JQuery FancyBox</summary>
    JQueryFancyBox = 4194304, // 0x0000000000400000
    /// <summary>JQuery FancyBox</summary>
    JQueryCookie = 8388608, // 0x0000000000800000
    /// <summary>Querystring</summary>
    QueryString = 16777216, // 0x0000000001000000
    /// <summary>Kendo.web. This script is included in Kendo.All.</summary>
    KendoWeb = 33554432, // 0x0000000002000000
    /// <summary>The Sitefinity Insight javascript client</summary>
    DecJsClient = 134217728, // 0x0000000008000000
    /// <summary>Kendo Timezones</summary>
    KendoTimezones = 268435456, // 0x0000000010000000
    /// <summary>AngularJS</summary>
    AngularJS = 536870912, // 0x0000000020000000
  }
}
