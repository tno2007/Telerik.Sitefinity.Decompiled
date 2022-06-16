// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration.ResponsiveDesignConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Data;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.ResponsiveDesign.Model;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration
{
  /// <summary>
  /// Represents the configuration section for Responsive Design module.
  /// </summary>
  [ObjectInfo(typeof (ResponsiveDesignResources), Description = "ResponsiveDesignConfigDescription", Title = "ResponsiveDesignConfigCaption")]
  public class ResponsiveDesignConfig : ContentModuleConfigBase
  {
    /// <summary>
    /// Gets or sets the value which indicates weather the responsive design module
    /// is enabled.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "EnabledConfig")]
    [ConfigurationProperty("enabled")]
    public bool Enabled
    {
      get
      {
        object obj = this["enabled"];
        return obj != null && (bool) obj;
      }
      set
      {
        bool? nullable = this["enabled"] as bool?;
        if (nullable.HasValue && nullable.Value == value)
          return;
        this["enabled"] = (object) value;
        CacheDependency.Notify((IList<CacheDependencyKey>) new CacheDependencyKey[1]
        {
          new CacheDependencyKey()
          {
            Key = (string) null,
            Type = typeof (CacheDependencyPageDataObject)
          }
        });
      }
    }

    /// <summary>
    /// Gets a collection of device types supported by the responsive design module.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "DeviceTypesConfig")]
    [ConfigurationProperty("deviceTypes")]
    public virtual ConfigElementDictionary<string, DeviceTypeElement> DeviceTypes => (ConfigElementDictionary<string, DeviceTypeElement>) this["deviceTypes"];

    /// <summary>
    /// Gets a collection of layout elements that can be transformed through
    /// responsive design module.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "LayoutElementsConfig")]
    [ConfigurationProperty("layoutElements")]
    public virtual ConfigElementDictionary<string, OriginalLayoutElement> LayoutElements => (ConfigElementDictionary<string, OriginalLayoutElement>) this["layoutElements"];

    /// <summary>
    /// Gets a collection of preview devices that can be used to preview the page.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "PreviewDevicesConfig")]
    [ConfigurationProperty("previewDevices")]
    public virtual ConfigElementDictionary<string, PreviewDeviceElement> PreviewDevices => (ConfigElementDictionary<string, PreviewDeviceElement>) this["previewDevices"];

    [DescriptionResource(typeof (ResponsiveDesignResources), "HiddenLayoutCss")]
    [ConfigurationProperty("hiddenLayoutsCss")]
    public virtual string HiddenLayoutsCss
    {
      get => (string) this["hiddenLayoutsCss"];
      set => this["hiddenLayoutsCss"] = (object) value;
    }

    /// <summary>
    /// Gets a collection of control transformations that can be applied.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "ControlTransformationsConfig")]
    [ConfigurationProperty("controlTransformationElements")]
    public virtual ConfigElementDictionary<string, NavigationTransformationElement> NavigationTransformations => (ConfigElementDictionary<string, NavigationTransformationElement>) this["controlTransformationElements"];

    /// <summary>Initializes the properties.</summary>
    protected override void InitializeProperties()
    {
      base.InitializeProperties();
      if (this.DeviceTypes.Count == 0)
      {
        DeviceTypeElement element1 = new DeviceTypeElement((ConfigElement) this.DeviceTypes)
        {
          Name = "Smartphones",
          Title = "Smartphones",
          ResourceClassName = typeof (ResponsiveDesignResources).Name
        };
        this.DeviceTypes.Add(element1);
        element1.DefaultRules.Add(new MediaQueryRuleElement((ConfigElement) element1.DefaultRules)
        {
          Description = "High PPI devices - like iPhone5, HTC One, Samsung Galaxy S4, LG G2, Nokia Lumia 1020, Sony Xperia Z etc.   ",
          MediaQueryRule = "@media only screen and (-moz-min-device-pixel-ratio: 2), only screen and (-o-min-device-pixel-ratio: 2/1), only screen and (-webkit-min-device-pixel-ratio: 2), only screen and (min-device-pixel-ratio: 2)"
        });
        element1.DefaultRules.Add(new MediaQueryRuleElement((ConfigElement) element1.DefaultRules)
        {
          Description = "Small-screen phones (width: 240-320px) - like HTC Wildfire, BlackBerry Curve etc.",
          WidthType = SizeType.Range,
          MinWidth = new int?(240),
          MaxWidth = new int?(320),
          MediaQueryRule = "@media only screen and (min-width: 240px) and (max-width: 320px)"
        });
        element1.DefaultRules.Add(new MediaQueryRuleElement((ConfigElement) element1.DefaultRules)
        {
          Description = "Mid-screen phones (width: 320-480px) - like iPhone 2G-4S, LG Optimus, HTC Hero, BlackBerry Storm 2 9520 etc.",
          WidthType = SizeType.Range,
          MinWidth = new int?(320),
          MaxWidth = new int?(480),
          MediaQueryRule = "@media only screen and (min-width: 320px) and (max-width: 480px)"
        });
        element1.DefaultRules.Add(new MediaQueryRuleElement((ConfigElement) element1.DefaultRules)
        {
          Description = "Big-screen phones (width: 480-960px) - like HTC (Vivid, Amaze, HD2), BlackBerry Bold 9900, Samsung Galaxy Note 3 etc.",
          WidthType = SizeType.Range,
          MinWidth = new int?(480),
          MaxWidth = new int?(960),
          MediaQueryRule = "@media only screen and (min-width: 480px) and (max-width : 960px)"
        });
        DeviceTypeElement element2 = new DeviceTypeElement((ConfigElement) this.DeviceTypes)
        {
          Name = "Tablets",
          Title = "TabletsAndSmallScreens",
          ResourceClassName = typeof (ResponsiveDesignResources).Name
        };
        this.DeviceTypes.Add(element2);
        element2.DefaultRules.Add(new MediaQueryRuleElement((ConfigElement) element2.DefaultRules)
        {
          Description = "Tablets (width: 600-1024px) - like iPad, Samsung Galaxy Tab, Kindle Fire etc.",
          WidthType = SizeType.Range,
          MinWidth = new int?(600),
          MaxWidth = new int?(1024),
          MediaQueryRule = "@media only screen and (min-width: 600px) and (max-width : 1024px)"
        });
        element2.DefaultRules.Add(new MediaQueryRuleElement((ConfigElement) element2.DefaultRules)
        {
          Description = "Bigger tablets (width: 768-1366px) - like Samsung Galaxy Note 8, Kindle Fire HD 8.9\", Samsung Galaxy Tab, Microsoft Surface Pro etc.",
          WidthType = SizeType.Range,
          MinWidth = new int?(700),
          MaxWidth = new int?(2000),
          MediaQueryRule = "@media only screen and (min-width: 768px) and (max-width : 1366px)"
        });
        DeviceTypeElement element3 = new DeviceTypeElement((ConfigElement) this.DeviceTypes)
        {
          Name = "LargeScreens",
          Title = "LargeScreens",
          ResourceClassName = typeof (ResponsiveDesignResources).Name
        };
        this.DeviceTypes.Add(element3);
        element3.DefaultRules.Add(new MediaQueryRuleElement((ConfigElement) element3.DefaultRules)
        {
          Description = "Notebooks and desktop screens (width: 1024px and higher)",
          WidthType = SizeType.Range,
          MinWidth = new int?(1024),
          MediaQueryRule = "@media only screen and (min-width : 1024px)"
        });
      }
      if (this.LayoutElements.Count == 0)
      {
        OriginalLayoutElement originalLayoutElement1 = new OriginalLayoutElement((ConfigElement) this.LayoutElements);
        originalLayoutElement1.Name = "two-columns";
        originalLayoutElement1.GroupName = "two-columns";
        originalLayoutElement1.Title = "<span class='sfOrg2'></span>2 columns";
        OriginalLayoutElement element4 = originalLayoutElement1;
        element4.AlternateLayouts.Add(new LayoutElement((ConfigElement) element4.AlternateLayouts)
        {
          Name = "two-rows",
          GroupName = "two-columns",
          Title = "<span class='sfTrans2To_1_1'></span>2 rows",
          LayoutCss = "/* \r\n                                  25+75, 33+67, 50+50, 67+33, 75+25 \r\n                                  Transformation in one column with two rows\r\n                                  */\r\n                                \r\n                                body { min-width: 0 !important; }\r\n                                .sfPublicWrapper { width: auto !important; }\r\n\r\n                                .sf_colsOut.sf_2cols_1_33,\r\n                                .sf_colsOut.sf_2cols_2_67,\r\n                                .sf_colsOut.sf_2cols_1_67,\r\n                                .sf_colsOut.sf_2cols_2_33,\r\n                                .sf_colsOut.sf_2cols_1_50,\r\n                                .sf_colsOut.sf_2cols_2_50,\r\n                                .sf_colsOut.sf_2cols_1_25,\r\n                                .sf_colsOut.sf_2cols_2_75,\r\n                                .sf_colsOut.sf_2cols_1_75,\r\n                                .sf_colsOut.sf_2cols_2_25 {\r\n\t                                width: 100% !important;\r\n                                }\r\n\r\n                                .sf_colsOut.sf_2cols_2_67 .sf_colsIn,\r\n                                .sf_colsOut.sf_2cols_2_33 .sf_colsIn,\r\n                                .sf_colsOut.sf_2cols_2_50 .sf_colsIn,\r\n                                .sf_colsOut.sf_2cols_2_75 .sf_colsIn,\r\n                                .sf_colsOut.sf_2cols_2_25 .sf_colsIn {\r\n\t                                margin-left: 0 !important;\r\n\t                                margin-right: 0 !important;\r\n                                }\r\n\r\n                                .sf_colsOut.sf_2cols_1_67 .sf_colsIn,\r\n                                .sf_colsOut.sf_2cols_1_33 .sf_colsIn,\r\n                                .sf_colsOut.sf_2cols_1_50 .sf_colsIn,\r\n                                .sf_colsOut.sf_2cols_1_75 .sf_colsIn,\r\n                                .sf_colsOut.sf_2cols_1_25 .sf_colsIn {\r\n\t                                margin-left: 0 !important;\r\n\t                                margin-right: 0 !important;\r\n                                }\r\n\r\n                                /*------------------- 3 columns layout, 1 hidden, 2 left --------------- */\r\n                                .sf_3cols_hide_3 .sf_colsOut.sf_3cols_1_33,\r\n                                .sf_3cols_hide_3 .sf_colsOut.sf_3cols_2_34,\r\n                                .sf_3cols_hide_1 .sf_colsOut.sf_3cols_2_34,\r\n                                .sf_3cols_hide_1 .sf_colsOut.sf_3cols_3_33,\r\n                                .sf_3cols_hide_2 .sf_colsOut.sf_3cols_1_33, \r\n                                .sf_3cols_hide_2 .sf_colsOut.sf_3cols_3_33,\r\n                                .sf_3cols_hide_1 .sf_colsOut.sf_3cols_2_50, \r\n                                .sf_3cols_hide_1 .sf_colsOut.sf_3cols_3_25,\r\n                                .sf_3cols_hide_2 .sf_colsOut.sf_3cols_1_25, \r\n                                .sf_3cols_hide_2 .sf_colsOut.sf_3cols_3_25,\r\n                                .sf_3cols_hide_3 .sf_colsOut.sf_3cols_1_25,\r\n                                .sf_3cols_hide_3 .sf_colsOut.sf_3cols_2_50 {width: 100% !important;}\r\n\r\n                                .sf_3cols_hide_1 .sf_colsOut.sf_3cols_3_33 .sf_3cols_3in_33,\r\n                                .sf_3cols_hide_3 .sf_colsOut.sf_3cols_2_34 .sf_3cols_2in_34,\r\n                                .sf_3cols_hide_2 .sf_colsOut.sf_3cols_3_33 .sf_3cols_3in_33,\r\n                                .sf_3cols_hide_1 .sf_colsOut.sf_3cols_3_25 .sf_3cols_3in_25,\r\n                                .sf_3cols_hide_2 .sf_colsOut.sf_3cols_3_25 .sf_3cols_3in_25,\r\n                                .sf_3cols_hide_3 .sf_colsOut.sf_3cols_2_50 .sf_3cols_2in_50 {margin-left: 0 !important;}\r\n\r\n                                /*------------------- 4 columns layout, 2 hidden, 2 left --------------- */\r\n                                .sf_4cols_hide_1.sf_4cols_hide_2 .sf_colsOut.sf_4cols_3_25,\r\n                                .sf_4cols_hide_1.sf_4cols_hide_2 .sf_colsOut.sf_4cols_4_25,\r\n                                .sf_4cols_hide_1.sf_4cols_hide_3 .sf_colsOut.sf_4cols_2_25,\r\n                                .sf_4cols_hide_1.sf_4cols_hide_3 .sf_colsOut.sf_4cols_4_25,\r\n                                .sf_4cols_hide_1.sf_4cols_hide_4 .sf_colsOut.sf_4cols_2_25,\r\n                                .sf_4cols_hide_1.sf_4cols_hide_4 .sf_colsOut.sf_4cols_3_25,\r\n                                .sf_4cols_hide_2.sf_4cols_hide_4 .sf_colsOut.sf_4cols_1_25,\r\n                                .sf_4cols_hide_2.sf_4cols_hide_4 .sf_colsOut.sf_4cols_3_25,\r\n                                .sf_4cols_hide_3.sf_4cols_hide_4 .sf_colsOut.sf_4cols_1_25,\r\n                                .sf_4cols_hide_3.sf_4cols_hide_4 .sf_colsOut.sf_4cols_2_25,\r\n                                .sf_4cols_hide_2.sf_4cols_hide_3 .sf_colsOut.sf_4cols_1_25,\r\n                                .sf_4cols_hide_2.sf_4cols_hide_3 .sf_colsOut.sf_4cols_4_25 {width: 100% !important;}\r\n\r\n                                /*------------------- 5 columns layout, 3 hidden, 2 left --------------- */\r\n                                .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_3 .sf_colsOut.sf_5cols_4_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_3 .sf_colsOut.sf_5cols_5_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_4 .sf_colsOut.sf_5cols_3_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20,\r\n                                .sf_5cols_hide_3.sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20,\r\n                                .sf_5cols_hide_3.sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_4 .sf_colsOut.sf_5cols_2_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_4 .sf_colsOut.sf_5cols_1_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20 {width: 100% !important;}"
        });
        this.LayoutElements.Add(element4);
        OriginalLayoutElement originalLayoutElement2 = new OriginalLayoutElement((ConfigElement) this.LayoutElements);
        originalLayoutElement2.Name = "three-columns";
        originalLayoutElement2.GroupName = "three-columns";
        originalLayoutElement2.Title = "<span class='sfOrg3'></span>3 columns";
        OriginalLayoutElement element5 = originalLayoutElement2;
        element5.AlternateLayouts.Add(new LayoutElement((ConfigElement) element5.AlternateLayouts)
        {
          Name = "three-row",
          GroupName = "three-columns",
          Title = "<span class='sfTrans3To_1_1_1'></span>3 rows",
          LayoutCss = "/* \r\n                                    33+34+33, 25+50+25\r\n                                    Transformation in one column with three rows\r\n                                   */\r\n                    \r\n                                body { min-width: 0 !important; }\r\n                                .sfPublicWrapper { width: auto !important; }\r\n\r\n                                .sf_colsOut.sf_3cols_1_33,\r\n                                .sf_colsOut.sf_3cols_2_34,\r\n                                .sf_colsOut.sf_3cols_3_33,\r\n                                .sf_colsOut.sf_3cols_1_25,\r\n                                .sf_colsOut.sf_3cols_2_50,\r\n                                .sf_colsOut.sf_3cols_3_25 {width: 100%  !important}\r\n\r\n                                .sf_colsOut.sf_3cols_1_33 .sf_colsIn,\r\n                                .sf_colsOut.sf_3cols_1_25 .sf_colsIn,\r\n                                .sf_colsOut.sf_3cols_2_34 .sf_colsIn,\r\n                                .sf_colsOut.sf_3cols_3_33 .sf_colsIn,\r\n                                .sf_colsOut.sf_3cols_2_50 .sf_colsIn,\r\n                                .sf_colsOut.sf_3cols_3_25 .sf_colsIn\r\n                                {\r\n\t                                margin-left: 0 !important;\r\n\t                                margin-right: 0 !important;\r\n                                }\r\n\r\n                                /*------------------- 4 columns layout, 1 hidden, 3 left --------------- */\r\n                                .sf_4cols_hide_1 .sf_4cols_2sf_colsOut._25, \r\n                                .sf_4cols_hide_1 .sf_colsOut.sf_4cols_3_25, \r\n                                .sf_4cols_hide_1 .sf_colsOut.sf_4cols_4_25,\r\n                                .sf_4cols_hide_2 .sf_colsOut.sf_4cols_1_25, \r\n                                .sf_4cols_hide_2 .sf_colsOut.sf_4cols_3_25, \r\n                                .sf_4cols_hide_2 .sf_colsOut.sf_4cols_4_25,\r\n                                .sf_4cols_hide_3 .sf_colsOut.sf_4cols_1_25, \r\n                                .sf_4cols_hide_3 .sf_colsOut.sf_4cols_2_25, \r\n                                .sf_4cols_hide_3 .sf_colsOut.sf_4cols_4_25,\r\n                                .sf_4cols_hide_4 .sf_colsOut.sf_4cols_1_25, \r\n                                .sf_4cols_hide_4 .sf_colsOut.sf_4cols_2_25, \r\n                                .sf_4cols_hide_4 .sf_colsOut.sf_4cols_3_25 {width: 100% !important;}\r\n\r\n                                .sf_4cols_hide_2 .sf_colsOut.sf_4cols_1_25 .sf_4cols_1in_25, \r\n                                .sf_4cols_hide_2 .sf_colsOut.sf_4cols_3_25 .sf_4cols_3in_25, \r\n                                .sf_4cols_hide_2 .sf_colsOut.sf_4cols_4_25 .sf_4cols_4in_25,\r\n                                .sf_4cols_hide_3 .sf_colsOut.sf_4cols_2_25 .sf_4cols_2in_25, \r\n                                .sf_4cols_hide_3 .sf_colsOut.sf_4cols_4_25 .sf_4cols_4in_25,\r\n                                .sf_4cols_hide_4 .sf_colsOut.sf_4cols_2_25 .sf_4cols_2in_25, \r\n                                .sf_4cols_hide_4 .sf_colsOut.sf_4cols_3_25 .sf_4cols_3in_25 {margin-left: 0 !important;}\r\n\r\n                                /*------------------- 5 columns layout, 2 hidden, 3 left --------------- */\r\n                                .sf_5cols_hide_1.sf_5cols_hide_2 .sf_colsOut.sf_5cols_3_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_2 .sf_colsOut.sf_5cols_4_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_2 .sf_colsOut.sf_5cols_5_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_3 .sf_colsOut.sf_5cols_2_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_3 .sf_colsOut.sf_5cols_4_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_3 .sf_colsOut.sf_5cols_5_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_4 .sf_colsOut.sf_5cols_2_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_4 .sf_colsOut.sf_5cols_3_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20,\r\n                                .sf_5cols_hide_1.sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20,\r\n                                .sf_5cols_hide_3.sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20,\r\n                                .sf_5cols_hide_3.sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20,\r\n                                .sf_5cols_hide_3.sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20,\r\n                                .sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20,\r\n                                .sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20,\r\n                                .sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_3 .sf_colsOut.sf_5cols_1_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_3 .sf_colsOut.sf_5cols_4_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_3 .sf_colsOut.sf_5cols_5_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_4 .sf_colsOut.sf_5cols_1_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_4 .sf_colsOut.sf_5cols_3_20,\r\n                                .sf_5cols_hide_2.sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20,\r\n                                .sf_5cols_hide_3.sf_5cols_hide_4 .sf_colsOut.sf_5cols_1_20,\r\n                                .sf_5cols_hide_3.sf_5cols_hide_4 .sf_colsOut.sf_5cols_2_20,\r\n                                .sf_5cols_hide_3.sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20 {width: 100% !important;}"
        });
        element5.AlternateLayouts.Add(new LayoutElement((ConfigElement) element5.AlternateLayouts)
        {
          Name = "one-row",
          GroupName = "three-columns",
          Title = "<span class='sfTrans3To_1_2'></span>2 rows<br />(1+2 columns)",
          LayoutCss = "/* \r\n                                    33+34+33, 25+50+25\r\n                                    Transformation in one column on the first row and two on the second\r\n                                    */\r\n\r\n                                    body { min-width: 0 !important; }\r\n                                    .sfPublicWrapper { width: auto !important; }\r\n\r\n                                    .sf_colsOut.sf_3cols_1_33,\r\n                                    .sf_colsOut.sf_3cols_1_25 {\r\n\t                                    width: 100% !important;\r\n                                    }\r\n                                    .sf_colsOut.sf_3cols_2_34,\r\n                                    .sf_colsOut.sf_3cols_3_33,\r\n                                    .sf_colsOut.sf_3cols_2_50,\r\n                                    .sf_colsOut.sf_3cols_3_25 {\r\n\t                                    width: 50% !important;\r\n                                    }\r\n                                    .sf_colsOut.sf_3cols_3_33 .sf_colsIn,\r\n                                    .sf_colsOut.sf_3cols_2_50 .sf_colsIn {\r\n\t                                    margin-right: 0 !important;\r\n                                    }\r\n                                    .sf_colsOut.sf_3cols_1_33 .sf_colsIn,\r\n                                    .sf_colsOut.sf_3cols_1_25 .sf_colsIn,\r\n                                    .sf_colsOut.sf_3cols_2_34 .sf_colsIn,\r\n                                    .sf_colsOut.sf_3cols_2_50 .sf_colsIn\r\n                                    {\r\n\t                                    margin-left: 0 !important;\r\n\t                                    margin-right: 0 !important;\r\n                                    }\r\n\r\n                                    /*------------------- 4 columns layout, 1 hidden, 3 left --------------- */\r\n                                    .sf_4cols_hide_1 .sf_colsOut.sf_4cols_2_25,\r\n                                    .sf_4cols_hide_2 .sf_colsOut.sf_4cols_1_25,\r\n                                    .sf_4cols_hide_3 .sf_colsOut.sf_4cols_1_25,\r\n                                    .sf_4cols_hide_4 .sf_colsOut.sf_4cols_1_25 {width: 100% !important;} \r\n\r\n                                    .sf_4cols_hide_1 .sf_colsOut.sf_4cols_3_25, \r\n                                    .sf_4cols_hide_1 .sf_colsOut.sf_4cols_4_25,\r\n                                    .sf_4cols_hide_2 .sf_colsOut.sf_4cols_3_25, \r\n                                    .sf_4cols_hide_2 .sf_colsOut.sf_4cols_4_25,\r\n                                    .sf_4cols_hide_3 .sf_colsOut.sf_4cols_2_25, \r\n                                    .sf_4cols_hide_3 .sf_colsOut.sf_4cols_4_25,\r\n                                    .sf_4cols_hide_4 .sf_colsOut.sf_4cols_2_25, \r\n                                    .sf_4cols_hide_4 .sf_colsOut.sf_4cols_3_25 {width: 50% !important; clear: none;}\r\n\r\n                                    .sf_4cols_hide_1 .sf_colsOut.sf_4cols_3_25 .sf_4cols_3in_25,\r\n                                    .sf_4cols_hide_2 .sf_colsOut.sf_4cols_3_25 .sf_4cols_3in_25,\r\n                                    .sf_4cols_hide_3 .sf_colsOut.sf_4cols_2_25 .sf_4cols_2in_25,\r\n                                    .sf_4cols_hide_4 .sf_colsOut.sf_4cols_2_25 .sf_4cols_2in_25 {margin-left: 0 !important;}\r\n\r\n                                    /*------------------- 5 columns layout, 2 hidden, 3 left --------------- */\r\n                                   \r\n                                    .sf_5cols_hide_1.sf_5cols_hide_2 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_2 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_3 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_3 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_4 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_3.sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20,\r\n                                    .sf_5cols_hide_3.sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20,\r\n                                    .sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_3 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_3 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_4 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_3.sf_5cols_hide_4 .sf_colsOut.sf_5cols_2_20,\r\n                                    .sf_5cols_hide_3.sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20 {width: 50% !important;clear: none;}\r\n\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_2 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_3 .sf_colsOut.sf_5cols_2_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_4 .sf_colsOut.sf_5cols_2_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20,\r\n                                    .sf_5cols_hide_3.sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20,\r\n                                    .sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_3 .sf_colsOut.sf_5cols_1_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_4 .sf_colsOut.sf_5cols_1_20,\r\n                                    .sf_5cols_hide_3.sf_5cols_hide_4 .sf_colsOut.sf_5cols_1_20 {width: 100% !important;}"
        });
        element5.AlternateLayouts.Add(new LayoutElement((ConfigElement) element5.AlternateLayouts)
        {
          Name = "two-rows",
          GroupName = "three-columns",
          Title = "<span class='sfTrans3To_2_1'></span>2 rows<br />(2+1 columns)",
          LayoutCss = "/* \r\n                                    33+34+33, 25+50+25\r\n                                    Transformation in two columns on the first row and one on the second\r\n                                   */\r\n\r\n                                    body { min-width: 0 !important; }\r\n                                    .sfPublicWrapper { width: auto !important; }\r\n\r\n                                    .sf_colsOut.sf_3cols_1_33,\r\n                                    .sf_colsOut.sf_3cols_2_34,\r\n                                    .sf_colsOut.sf_3cols_1_25,\r\n                                    .sf_colsOut.sf_3cols_2_50 {\r\n\t                                    width: 50%  !important;\r\n                                    }\r\n                                    .sf_colsOut.sf_3cols_2_34 .sf_colsIn,\r\n                                    .sf_colsOut.sf_3cols_2_50 .sf_colsIn{\r\n\t                                    margin-right: 0 !important;\r\n                                    }\r\n                                    .sf_colsOut.sf_3cols_3_33,\r\n                                    .sf_colsOut.sf_3cols_3_25 {\r\n\t                                    width: 100%  !important;\r\n                                    }\r\n                                    .sf_colsOut.sf_3cols_1_33 .sf_colsIn,\r\n                                    .sf_colsOut.sf_3cols_1_25 .sf_colsIn,\r\n                                    .sf_colsOut.sf_3cols_3_33 .sf_colsIn,\r\n                                    .sf_colsOut.sf_3cols_3_25 .sf_colsIn {\r\n\t                                    margin-left: 0 !important;\r\n\t                                    margin-right: 0 !important;\r\n                                    }\r\n\r\n                                    /*------------------- 4 columns layout, 1 hidden, 3 left --------------- */\r\n                                    .sf_4cols_hide_1 .sf_colsOut.sf_4cols_4_25,\r\n                                    .sf_4cols_hide_4 .sf_colsOut.sf_4cols_3_25,\r\n                                    .sf_4cols_hide_2 .sf_colsOut.sf_4cols_4_25,\r\n                                    .sf_4cols_hide_3 .sf_colsOut.sf_4cols_4_25 {width: 100% !important;} \r\n\r\n                                    .sf_4cols_hide_1 .sf_colsOut.sf_4cols_2_25, \r\n                                    .sf_4cols_hide_1 .sf_colsOut.sf_4cols_3_25,\r\n                                    .sf_4cols_hide_4 .sf_colsOut.sf_4cols_1_25, \r\n                                    .sf_4cols_hide_4 .sf_colsOut.sf_4cols_2_25,\r\n                                    .sf_4cols_hide_2 .sf_colsOut.sf_4cols_1_25, \r\n                                    .sf_4cols_hide_2 .sf_colsOut.sf_4cols_3_25,\r\n                                    .sf_4cols_hide_3 .sf_colsOut.sf_4cols_1_25, \r\n                                    .sf_4cols_hide_3 .sf_colsOut.sf_4cols_2_25 {width: 50% !important; clear: none;}\r\n\r\n                                    .sf_4cols_hide_1 .sf_colsOut.sf_4cols_4_25 .sf_4cols_4in_25,\r\n                                    .sf_4cols_hide_2 .sf_colsOut.sf_4cols_4_25 .sf_4cols_4in_25,\r\n                                    .sf_4cols_hide_3 .sf_colsOut.sf_4cols_4_25 .sf_4cols_4in_25,\r\n                                    .sf_4cols_hide_4 .sf_colsOut.sf_4cols_3_25 .sf_4cols_3in_25 {margin-left: 0 !important;}\r\n\r\n                                    /*------------------- 5 columns layout, 2 hidden, 3 left --------------- */\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_2 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_2 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_3 .sf_colsOut.sf_5cols_2_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_3 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_4 .sf_colsOut.sf_5cols_2_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_4 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_3.sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20,\r\n                                    .sf_5cols_hide_3.sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20,\r\n                                    .sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20,\r\n                                    .sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_3 .sf_colsOut.sf_5cols_1_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_3 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_4 .sf_colsOut.sf_5cols_1_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_4 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_3.sf_5cols_hide_4 .sf_colsOut.sf_5cols_1_20,\r\n                                    .sf_5cols_hide_3.sf_5cols_hide_4 .sf_colsOut.sf_5cols_2_20 {width: 50% !important; clear: none;}\r\n\r\n                                    .sf_5cols_hide_3.sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_3 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_3 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_1.sf_5cols_hide_2 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_4.sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_3.sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_2.sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20 {width: 100% !important;}"
        });
        this.LayoutElements.Add(element5);
        OriginalLayoutElement originalLayoutElement3 = new OriginalLayoutElement((ConfigElement) this.LayoutElements);
        originalLayoutElement3.Name = "four-columns";
        originalLayoutElement3.GroupName = "four-columns";
        originalLayoutElement3.Title = "<span class='sfOrg4'></span>4 columns";
        OriginalLayoutElement element6 = originalLayoutElement3;
        element6.AlternateLayouts.Add(new LayoutElement((ConfigElement) element6.AlternateLayouts)
        {
          Name = "four-rows",
          GroupName = "four-columns",
          Title = "<span class='sfTrans4To_1_1_1_1'></span>4 rows",
          LayoutCss = "/* \r\n                                    25+25+25+25\r\n                                    Transformation in four rows, one column\r\n                                    */\r\n\r\n                                    body { min-width: 0 !important; }\r\n                                    .sfPublicWrapper { width: auto !important; }\r\n\r\n                                    .sf_colsOut.sf_4cols_1_25,\r\n                                    .sf_colsOut.sf_4cols_2_25,\r\n                                    .sf_colsOut.sf_4cols_3_25,\r\n                                    .sf_colsOut.sf_4cols_4_25 {\r\n\t                                    width: 100% !important;\r\n                                    }\r\n                                    .sf_colsOut.sf_4cols_1_25 .sf_colsIn,\r\n                                    .sf_colsOut.sf_4cols_2_25 .sf_colsIn,\r\n                                    .sf_colsOut.sf_4cols_3_25 .sf_colsIn,\r\n                                    .sf_colsOut.sf_4cols_4_25 .sf_colsIn {\r\n\t                                    margin-left: 0 !important;\r\n\t                                    margin-right: 0 !important;\r\n                                    }\r\n\r\n                                    /*------------------- 5 columns layout, 1 hidden, 4 left --------------- */\r\n                                    .sf_5cols_hide_1 .sf_colsOut.sf_5cols_2_20, \r\n                                    .sf_5cols_hide_1 .sf_colsOut.sf_5cols_3_20, \r\n                                    .sf_5cols_hide_1 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_1 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_2 .sf_colsOut.sf_5cols_1_20, \r\n                                    .sf_5cols_hide_2 .sf_colsOut.sf_5cols_3_20, \r\n                                    .sf_5cols_hide_2 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_2 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_3 .sf_colsOut.sf_5cols_1_20, \r\n                                    .sf_5cols_hide_3 .sf_colsOut.sf_5cols_2_20, \r\n                                    .sf_5cols_hide_3 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_3 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_4 .sf_colsOut.sf_5cols_1_20, \r\n                                    .sf_5cols_hide_4 .sf_colsOut.sf_5cols_2_20, \r\n                                    .sf_5cols_hide_4 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20, \r\n                                    .sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20, \r\n                                    .sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20 {width: 100% !important;}\r\n\r\n                                    .sf_5cols_hide_1 .sf_colsOut.sf_5cols_2_20 .sf_5cols_2in_20, \r\n                                    .sf_5cols_hide_1 .sf_colsOut.sf_5cols_3_20 .sf_5cols_3in_20, \r\n                                    .sf_5cols_hide_1 .sf_colsOut.sf_5cols_4_20 .sf_5cols_4in_20,\r\n                                    .sf_5cols_hide_1 .sf_colsOut.sf_5cols_5_20 .sf_5cols_5in_20,\r\n                                    .sf_5cols_hide_2 .sf_colsOut.sf_5cols_3_20 .sf_5cols_3in_20, \r\n                                    .sf_5cols_hide_2 .sf_colsOut.sf_5cols_4_20 .sf_5cols_4in_20,\r\n                                    .sf_5cols_hide_2 .sf_colsOut.sf_5cols_5_20 .sf_5cols_5in_20,\r\n                                    .sf_5cols_hide_3 .sf_colsOut.sf_5cols_2_20 .sf_5cols_2in_20, \r\n                                    .sf_5cols_hide_3 .sf_colsOut.sf_5cols_4_20 .sf_5cols_4in_20,\r\n                                    .sf_5cols_hide_3 .sf_colsOut.sf_5cols_5_20 .sf_5cols_5in_20,\r\n                                    .sf_5cols_hide_4 .sf_colsOut.sf_5cols_2_20 .sf_5cols_2in_20, \r\n                                    .sf_5cols_hide_4 .sf_colsOut.sf_5cols_3_20 .sf_5cols_3in_20,\r\n                                    .sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20 .sf_5cols_5in_20,\r\n                                    .sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20 .sf_5cols_2in_20, \r\n                                    .sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20 .sf_5cols_3in_20,\r\n                                    .sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20 .sf_5cols_4in_20 {margin: 0 !important;}"
        });
        element6.AlternateLayouts.Add(new LayoutElement((ConfigElement) element6.AlternateLayouts)
        {
          Name = "two-rows",
          GroupName = "four-columns",
          Title = "<span class='sfTrans4To_2_2'></span>2 rows<br />(2+2 columns)",
          LayoutCss = "/* \r\n                                    25+25+25+25\r\n                                    Transformation in two rows with two columns on each row\r\n                                    */\r\n\r\n                                    body { min-width: 0 !important; }\r\n                                    .sfPublicWrapper { width: auto !important; }\r\n\r\n                                    .sf_colsOut.sf_4cols_1_25,\r\n                                    .sf_colsOut.sf_4cols_2_25,\r\n                                    .sf_colsOut.sf_4cols_3_25,\r\n                                    .sf_colsOut.sf_4cols_4_25 {\r\n\t                                    width: 50% !important;\r\n                                    }\r\n\r\n                                    .sf_colsOut.sf_4cols_2_25 .sf_colsIn,\r\n                                    .sf_colsOut.sf_4cols_4_25 .sf_colsIn {\r\n                                        margin-right: 0 !important;\r\n                                    }\r\n\r\n                                    .sf_colsOut.sf_4cols_3_25 {\r\n                                        clear: left;\r\n                                    }\r\n                                    .sf_colsOut.sf_4cols_1_25 .sf_colsIn,\r\n                                    .sf_colsOut.sf_4cols_3_25 .sf_colsIn {\r\n\t                                    margin-left: 0 !important;\r\n\t                                    margin-right: 0 !important;\r\n                                    }\r\n\r\n                                    /*------------------- 5 columns layout, 1 hidden, 4 left --------------- */\r\n                                    .sf_5cols_hide_1 .sf_colsOut.sf_5cols_2_20, \r\n                                    .sf_5cols_hide_1 .sf_colsOut.sf_5cols_3_20, \r\n                                    .sf_5cols_hide_1 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_1 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_3 .sf_colsOut.sf_5cols_1_20, \r\n                                    .sf_5cols_hide_3 .sf_colsOut.sf_5cols_2_20, \r\n                                    .sf_5cols_hide_3 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_3 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_4 .sf_colsOut.sf_5cols_1_20, \r\n                                    .sf_5cols_hide_4 .sf_colsOut.sf_5cols_2_20, \r\n                                    .sf_5cols_hide_4 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_4 .sf_colsOut.sf_5cols_5_20,\r\n                                    .sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20, \r\n                                    .sf_5cols_hide_5 .sf_colsOut.sf_5cols_2_20, \r\n                                    .sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_5cols_hide_5 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_2 .sf_colsOut.sf_5cols_1_20, \r\n                                    .sf_5cols_hide_2 .sf_colsOut.sf_5cols_3_20, \r\n                                    .sf_5cols_hide_2 .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_5cols_hide_2 .sf_5cols_5_20 {width: 50% !important;clear: none;}\r\n\r\n                                    .sf_5cols_hide_1 .sf_colsOut.sf_5cols_2_20 .sf_5cols_2in_20, \r\n                                    .sf_5cols_hide_1 .sf_colsOut.sf_5cols_4_20 .sf_5cols_4in_20,\r\n                                    .sf_5cols_hide_2 .sf_colsOut.sf_5cols_1_20 .sf_5cols_1in_20, \r\n                                    .sf_5cols_hide_2 .sf_colsOut.sf_5cols_4_20 .sf_5cols_4in_20,\r\n                                    .sf_5cols_hide_3 .sf_colsOut.sf_5cols_1_20 .sf_5cols_1in_20, \r\n                                    .sf_5cols_hide_3 .sf_colsOut.sf_5cols_4_20 .sf_5cols_4in_20,\r\n                                    .sf_5cols_hide_4 .sf_colsOut.sf_5cols_1_20 .sf_5cols_1in_20, \r\n                                    .sf_5cols_hide_4 .sf_colsOut.sf_5cols_3_20 .sf_5cols_3in_20,\r\n                                    .sf_5cols_hide_5 .sf_colsOut.sf_5cols_1_20 .sf_5cols_1in_20, \r\n                                    .sf_5cols_hide_5 .sf_colsOut.sf_5cols_3_20 .sf_5cols_3in_20 {margin: 0 !important;}"
        });
        this.LayoutElements.Add(element6);
        OriginalLayoutElement originalLayoutElement4 = new OriginalLayoutElement((ConfigElement) this.LayoutElements);
        originalLayoutElement4.Name = "five-columns";
        originalLayoutElement4.GroupName = "five-columns";
        originalLayoutElement4.Title = "<span class='sfOrg5'></span>5 columns";
        OriginalLayoutElement element7 = originalLayoutElement4;
        element7.AlternateLayouts.Add(new LayoutElement((ConfigElement) element7.AlternateLayouts)
        {
          Name = "five-rows",
          GroupName = "five-columns",
          Title = "<span class='sfTrans5To_1_1_1_1_1'></span>5 rows",
          LayoutCss = "/* \r\n                                    20+20+20+20+20\r\n                                    Transformation in five rows, one column\r\n                                    */\r\n\r\n                                    body { min-width: 0 !important; }\r\n                                    .sfPublicWrapper { width: auto !important; }\r\n\r\n                                    .sf_colsOut.sf_5cols_1_20,\r\n                                    .sf_colsOut.sf_5cols_2_20,\r\n                                    .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_colsOut.sf_5cols_5_20 {\r\n\t                                    width: 100% !important;\r\n                                    }\r\n                                    .sf_colsOut.sf_5cols_1_20 .sf_colsIn,\r\n                                    .sf_colsOut.sf_5cols_2_20 .sf_colsIn,\r\n                                    .sf_colsOut.sf_5cols_3_20 .sf_colsIn,\r\n                                    .sf_colsOut.sf_5cols_4_20 .sf_colsIn,\r\n                                    .sf_colsOut.sf_5cols_5_20 .sf_colsIn {\r\n\t                                    margin-left: 0 !important;\r\n\t                                    margin-right: 0 !important;\r\n                                    }"
        });
        element7.AlternateLayouts.Add(new LayoutElement((ConfigElement) element7.AlternateLayouts)
        {
          Name = "three-rows-2-2-1",
          GroupName = "five-columns",
          Title = "<span class='sfTrans5To_2_2_1'></span>3 rows<br />(2+2+1 columns)",
          LayoutCss = "/* \r\n                                    20+20+20+20+20\r\n                                    Transformation in two by two columns a and one row\r\n                                    */\r\n\r\n                                    body { min-width: 0 !important; }\r\n                                    .sfPublicWrapper { width: auto !important; }\r\n\r\n                                    .sf_colsOut.sf_5cols_1_20,\r\n                                    .sf_colsOut.sf_5cols_2_20,\r\n                                    .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_colsOut.sf_5cols_4_20\r\n                                     {\r\n\t                                    width: 50% !important;\r\n                                    }\r\n                                    .sf_colsOut.sf_5cols_2_20 .sf_colsIn,\r\n                                    .sf_colsOut.sf_5cols_4_20 .sf_colsIn {\r\n                                       margin-right: 0 !important;\r\n                                    }\r\n                                    .sf_colsOut.sf_5cols_3_20\r\n                                    {\r\n                                        clear: left;\r\n                                    }\r\n                                    .sf_colsOut.sf_5cols_5_20 {\r\n\t                                    width: 100% !important;\r\n                                    }\r\n                                    .sf_colsOut.sf_5cols_1_20 .sf_colsIn,\r\n                                    .sf_colsOut.sf_5cols_3_20 .sf_colsIn,\r\n                                    .sf_colsOut.sf_5cols_5_20 .sf_colsIn\r\n                                    {\r\n                                        margin-left: 0 !important;\r\n\t                                    margin-right: 0 !important;\r\n                                    }"
        });
        element7.AlternateLayouts.Add(new LayoutElement((ConfigElement) element7.AlternateLayouts)
        {
          Name = "three-rows-1-2-2",
          GroupName = "five-columns",
          Title = "<span class='sfTrans5To_1_2_2'></span>3 rows<br />(1+2+2 columns)",
          LayoutCss = "/* \r\n                                    20+20+20+20+20\r\n                                    Transformation in one row and two by two columns\r\n                                    */\r\n\r\n                                    body { min-width: 0 !important; }\r\n                                    .sfPublicWrapper { width: auto !important; }\r\n\r\n                                    .sf_colsOut.sf_5cols_1_20\r\n                                    {\r\n\t                                    width: 100% !important;\r\n                                    }\r\n                                    .sf_colsOut.sf_5cols_2_20,\r\n                                    .sf_colsOut.sf_5cols_3_20,\r\n                                    .sf_colsOut.sf_5cols_4_20,\r\n                                    .sf_colsOut.sf_5cols_5_20 {\r\n\t                                    width: 50% !important;\r\n                                    }\r\n                                    .sf_colsOut.sf_5cols_3_20 .sf_colsIn,\r\n                                    .sf_colsOut.sf_5cols_5_20 .sf_colsIn {\r\n                                        margin-right: 0 !important;\r\n                                    }\r\n                                    .sf_colsOut.sf_5cols_4_20\r\n                                    {\r\n                                        clear: left;\r\n                                    }\r\n                                    .sf_colsOut.sf_5cols_1_20 .sf_colsIn,\r\n                                    .sf_colsOut.sf_5cols_2_20 .sf_colsIn,\r\n                                    .sf_colsOut.sf_5cols_4_20 .sf_colsIn\r\n                                    {\r\n                                        margin-left: 0 !important;\r\n\t                                    margin-right: 0 !important;\r\n                                    }"
        });
        this.LayoutElements.Add(element7);
      }
      if (this.PreviewDevices.Count == 0)
      {
        this.PreviewDevices.Add(new PreviewDeviceElement((ConfigElement) this.PreviewDevices)
        {
          Name = "i_phone_5",
          Title = "iPhone 5/SE",
          CssClass = "sfIPhone5_SE",
          DeviceWidth = 373,
          DeviceHeight = 782,
          ViewportWidth = 315,
          ViewportHeight = 558,
          OffsetX = 31,
          OffsetY = 116,
          OffsetXLandscape = 116,
          OffsetYLandscape = 27,
          DeviceCategory = "Phone"
        });
        this.PreviewDevices.Add(new PreviewDeviceElement((ConfigElement) this.PreviewDevices)
        {
          Name = "i_phone_6",
          Title = "iPhone 6/7/8",
          CssClass = "sfIPhone6_7_8",
          DeviceWidth = 436,
          DeviceHeight = 889,
          ViewportWidth = 375,
          ViewportHeight = 667,
          OffsetX = 31,
          OffsetY = 114,
          OffsetXLandscape = 114,
          OffsetYLandscape = 31,
          DeviceCategory = "Phone"
        });
        this.PreviewDevices.Add(new PreviewDeviceElement((ConfigElement) this.PreviewDevices)
        {
          Name = "i_phone_6_plus",
          Title = "iPhone 6/7/8 Plus",
          CssClass = "sfIPhone6_7_8_Plus",
          DeviceWidth = 480,
          DeviceHeight = 980,
          ViewportWidth = 414,
          ViewportHeight = 736,
          OffsetX = 34,
          OffsetY = 126,
          OffsetXLandscape = 126,
          OffsetYLandscape = 34,
          DeviceCategory = "Phone"
        });
        this.PreviewDevices.Add(new PreviewDeviceElement((ConfigElement) this.PreviewDevices)
        {
          Name = "i_phone_x",
          Title = "iPhone X/XS",
          CssClass = "sfIPhoneX",
          DeviceWidth = 418,
          DeviceHeight = 865,
          ViewportWidth = 364,
          ViewportHeight = 814,
          OffsetX = 27,
          OffsetY = 25,
          OffsetXLandscape = 24,
          OffsetYLandscape = 27,
          DeviceCategory = "Phone"
        });
        this.PreviewDevices.Add(new PreviewDeviceElement((ConfigElement) this.PreviewDevices)
        {
          Name = "huawei_p8",
          Title = "Huawei P8/P9 Lite",
          CssClass = "sfHuaweiP8",
          DeviceWidth = 397,
          DeviceHeight = 792,
          ViewportWidth = 357,
          ViewportHeight = 634,
          OffsetX = 20,
          OffsetY = 76,
          OffsetXLandscape = 76,
          OffsetYLandscape = 20,
          DeviceCategory = "Phone"
        });
        this.PreviewDevices.Add(new PreviewDeviceElement((ConfigElement) this.PreviewDevices)
        {
          Name = "samsung_galaxy_S6",
          Title = "Samsung Galaxy S6",
          CssClass = "sfSmsgGalaxyS6",
          DeviceWidth = 481,
          DeviceHeight = 875,
          ViewportWidth = 360,
          ViewportHeight = 640,
          OffsetX = 59,
          OffsetY = 117,
          OffsetXLandscape = 117,
          OffsetYLandscape = 62,
          DeviceCategory = "Phone"
        });
        this.PreviewDevices.Add(new PreviewDeviceElement((ConfigElement) this.PreviewDevices)
        {
          Name = "samsung_galaxy_j2",
          Title = "Samsung Galaxy J2",
          CssClass = "sfSmsgGalaxyJ2",
          DeviceWidth = 419,
          DeviceHeight = 821,
          ViewportWidth = 354,
          ViewportHeight = 632,
          OffsetX = 32,
          OffsetY = 98,
          OffsetXLandscape = 98,
          OffsetYLandscape = 32,
          DeviceCategory = "Phone"
        });
        this.PreviewDevices.Add(new PreviewDeviceElement((ConfigElement) this.PreviewDevices)
        {
          Name = "samsung_galaxy_s7",
          Title = "Samsung Galaxy S7",
          CssClass = "sfSmsgGalaxyS7",
          DeviceWidth = 396,
          DeviceHeight = 800,
          ViewportWidth = 360,
          ViewportHeight = 639,
          OffsetX = 18,
          OffsetY = 84,
          OffsetXLandscape = 84,
          OffsetYLandscape = 18,
          DeviceCategory = "Phone"
        });
        this.PreviewDevices.Add(new PreviewDeviceElement((ConfigElement) this.PreviewDevices)
        {
          Name = "samsung_galaxy_s8_9",
          Title = "Samsung Galaxy S8/S9",
          CssClass = "sfSmsgGalaxyS8_9",
          DeviceWidth = 382,
          DeviceHeight = 822,
          ViewportWidth = 358,
          ViewportHeight = 735,
          OffsetX = 11,
          OffsetY = 47,
          OffsetXLandscape = 42,
          OffsetYLandscape = 11,
          DeviceCategory = "Phone"
        });
        this.PreviewDevices.Add(new PreviewDeviceElement((ConfigElement) this.PreviewDevices)
        {
          Name = "samsung_galaxy_s8_9_Plus",
          Title = "Samsung Galaxy S8/S9 Plus",
          CssClass = "sfSmsgGalaxyS8_9_Plus",
          DeviceWidth = 380,
          DeviceHeight = 808,
          ViewportWidth = 358,
          ViewportHeight = 735,
          OffsetX = 10,
          OffsetY = 39,
          OffsetXLandscape = 39,
          OffsetYLandscape = 11,
          DeviceCategory = "Phone"
        });
        this.PreviewDevices.Add(new PreviewDeviceElement((ConfigElement) this.PreviewDevices)
        {
          Name = "google_pixel_2",
          Title = "Google Pixel 2",
          CssClass = "sfGooglePixel2",
          DeviceWidth = 440,
          DeviceHeight = 912,
          ViewportWidth = 394,
          ViewportHeight = 698,
          OffsetX = 22,
          OffsetY = 107,
          OffsetXLandscape = 105,
          OffsetYLandscape = 26,
          DeviceCategory = "Phone"
        });
        this.PreviewDevices.Add(new PreviewDeviceElement((ConfigElement) this.PreviewDevices)
        {
          Name = "google_pixel_3",
          Title = "Google Pixel 3",
          CssClass = "sfGooglePixel3",
          DeviceWidth = 450,
          DeviceHeight = 953,
          ViewportWidth = 406,
          ViewportHeight = 810,
          OffsetX = 20,
          OffsetY = 75,
          OffsetXLandscape = 73,
          OffsetYLandscape = 24,
          DeviceCategory = "Phone"
        });
        this.PreviewDevices.Add(new PreviewDeviceElement((ConfigElement) this.PreviewDevices)
        {
          Name = "i_pad_air",
          Title = "iPad Air",
          CssClass = "sfIPadAir",
          DeviceWidth = 868,
          DeviceHeight = 1288,
          ViewportWidth = 768,
          ViewportHeight = 1024,
          OffsetX = 50,
          OffsetY = 131,
          OffsetXLandscape = 131,
          OffsetYLandscape = 50,
          DeviceCategory = "Tablet"
        });
        this.PreviewDevices.Add(new PreviewDeviceElement((ConfigElement) this.PreviewDevices)
        {
          Name = "i_pad_pro",
          Title = "iPad Pro",
          CssClass = "sfIPadPro",
          DeviceWidth = 704,
          DeviceHeight = 975,
          ViewportWidth = 630,
          ViewportHeight = 902,
          OffsetX = 36,
          OffsetY = 37,
          OffsetXLandscape = 37,
          OffsetYLandscape = 37,
          DeviceCategory = "Tablet"
        });
      }
      if (string.IsNullOrEmpty(this.HiddenLayoutsCss))
        this.HiddenLayoutsCss = "/*------------------------------------------------\r\n                                               1 of 1 is hidden (default transformations)\r\n                                            ------------------------------------------------*/\r\n                                            .sf_1cols_hide_1 .sf_1col_1_100 {display: none}\r\n\r\n                                            /*------------------------------------------------\r\n                                               1 of 2 is hidden (default transformations)\r\n                                            ------------------------------------------------*/\r\n                                            .sf_2cols_hide_1 .sf_2cols_1_25,\r\n                                            .sf_2cols_hide_1 .sf_2cols_1_33,\r\n                                            .sf_2cols_hide_1 .sf_2cols_1_50,\r\n                                            .sf_2cols_hide_1 .sf_2cols_1_67,\r\n                                            .sf_2cols_hide_1 .sf_2cols_1_75,\r\n                                            .sf_2cols_hide_2 .sf_2cols_2_25,\r\n                                            .sf_2cols_hide_2 .sf_2cols_2_33,\r\n                                            .sf_2cols_hide_2 .sf_2cols_2_50,\r\n                                            .sf_2cols_hide_2 .sf_2cols_2_67,\r\n                                            .sf_2cols_hide_2 .sf_2cols_2_75 {display: none}\r\n\r\n                                            .sf_2cols_hide_1 .sf_2cols_2_25,\r\n                                            .sf_2cols_hide_2 .sf_2cols_1_25,\r\n                                            .sf_2cols_hide_1 .sf_2cols_2_33,\r\n                                            .sf_2cols_hide_2 .sf_2cols_1_33,\r\n                                            .sf_2cols_hide_1 .sf_2cols_2_50,\r\n                                            .sf_2cols_hide_2 .sf_2cols_1_50,\r\n                                            .sf_2cols_hide_1 .sf_2cols_2_67,\r\n                                            .sf_2cols_hide_2 .sf_2cols_1_67,\r\n                                            .sf_2cols_hide_1 .sf_2cols_2_75,\r\n                                            .sf_2cols_hide_2 .sf_2cols_1_75 {width: 100% !important}\r\n                                            \r\n                                            .sf_2cols_hide_1 .sf_2cols_2_25 .sf_2cols_2in_25,\r\n                                            .sf_2cols_hide_1 .sf_2cols_2_33 .sf_2cols_2in_33,\r\n                                            .sf_2cols_hide_1 .sf_2cols_2_50 .sf_2cols_2in_50,\r\n                                            .sf_2cols_hide_1 .sf_2cols_2_67 .sf_2cols_2in_67,\r\n                                            .sf_2cols_hide_1 .sf_2cols_2_75 .sf_2cols_2in_75 {margin-left: 0}\r\n\r\n                                            /*------------------------------------------------\r\n\t\t                                       1 of 3 is hidden (default transformations)\r\n                                            --------------------------------------------------*/\r\n                                            .sf_3cols_hide_1 .sf_3cols_1_33,\r\n                                            .sf_3cols_hide_2 .sf_3cols_2_34,\r\n                                            .sf_3cols_hide_3 .sf_3cols_3_33,\r\n                                            .sf_3cols_hide_1 .sf_3cols_1_25,\r\n                                            .sf_3cols_hide_2 .sf_3cols_2_50,\r\n                                            .sf_3cols_hide_3 .sf_3cols_3_25 {display: none}\r\n\r\n                                            .sf_3cols_hide_3 .sf_3cols_1_33,\r\n                                            .sf_3cols_hide_3 .sf_3cols_2_34, \r\n                                            .sf_3cols_hide_2 .sf_3cols_1_33,\r\n                                            .sf_3cols_hide_2 .sf_3cols_3_33,\r\n                                            .sf_3cols_hide_1 .sf_3cols_2_34,\r\n                                            .sf_3cols_hide_1 .sf_3cols_3_33,\r\n                                            .sf_3cols_hide_2 .sf_3cols_1_25, \r\n                                            .sf_3cols_hide_2 .sf_3cols_3_25 {width: 50%}\r\n\r\n                                            .sf_3cols_hide_1 .sf_3cols_2_50,\r\n                                            .sf_3cols_hide_3 .sf_3cols_2_50 {width: 67%} \r\n\r\n                                            .sf_3cols_hide_1 .sf_3cols_3_25,\r\n                                            .sf_3cols_hide_3 .sf_3cols_1_25 {width: 33%}\r\n\r\n                                            .sf_3cols_hide_1 .sf_3cols_2_50 .sf_3cols_2in_50,\r\n                                            .sf_3cols_hide_1 .sf_3cols_2_34 .sf_3cols_2in_34 {margin-left: 0}\r\n\r\n                                            /*------------------------------------------------\r\n                                                2 of 3 are hidden (default transformations)\r\n                                            ------------------------------------------------*/\r\n                                            .sf_3cols_hide_1.sf_3cols_hide_2 .sf_3cols_3_33,\r\n                                            .sf_3cols_hide_1.sf_3cols_hide_3 .sf_3cols_2_34,\r\n                                            .sf_3cols_hide_2.sf_3cols_hide_3 .sf_3cols_1_33,\r\n                                            .sf_3cols_hide_1.sf_3cols_hide_2 .sf_3cols_3_25,\r\n                                            .sf_3cols_hide_1.sf_3cols_hide_3 .sf_3cols_2_50,\r\n                                            .sf_3cols_hide_2.sf_3cols_hide_3 .sf_3cols_1_25 {width: 100% !important;}\r\n\r\n                                             /*------------------------------------------------\r\n                                                1 of 4 is hidden (default transformations)\r\n                                            ------------------------------------------------*/\r\n                                            .sf_4cols_hide_1 .sf_4cols_1_25,\r\n                                            .sf_4cols_hide_2 .sf_4cols_2_25,\r\n                                            .sf_4cols_hide_3 .sf_4cols_3_25,\r\n                                            .sf_4cols_hide_4 .sf_4cols_4_25 {display: none}\r\n\r\n                                            .sf_4cols_hide_1 .sf_4cols_2_25, \r\n                                            .sf_4cols_hide_1 .sf_4cols_4_25,\r\n                                            .sf_4cols_hide_3 .sf_4cols_1_25, \r\n                                            .sf_4cols_hide_3 .sf_4cols_4_25,\r\n                                            .sf_4cols_hide_2 .sf_4cols_1_25, \r\n                                            .sf_4cols_hide_2 .sf_4cols_4_25 {width: 33%} \r\n\r\n                                            .sf_4cols_hide_1 .sf_4cols_3_25,\r\n                                            .sf_4cols_hide_2 .sf_4cols_3_25,\r\n                                            .sf_4cols_hide_3 .sf_4cols_2_25 {width: 34%}\r\n\r\n                                            .sf_4cols_hide_1 .sf_4cols_2_25 .sf_4cols_2in_25,\r\n                                            .sf_4cols_hide_1 .sf_4cols_2_25 .sf_4cols_2in_25, \r\n                                            .sf_4cols_hide_1 .sf_4cols_3_25 .sf_4cols_3in_25, \r\n                                            .sf_4cols_hide_1 .sf_4cols_4_25 .sf_4cols_4in_25 {margin-left: 0;}\r\n\r\n                                            /*------------------------------------------------\r\n                                                2 of 4 are hidden (default transformations)\r\n                                            ------------------------------------------------*/\r\n\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_3 .sf_4cols_1_25,\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_3 .sf_4cols_3_25,\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_2 .sf_4cols_1_25,\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_2 .sf_4cols_2_25,\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_4 .sf_4cols_1_25,\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_4 .sf_4cols_4_25,\r\n                                            .sf_4cols_hide_2.sf_4cols_hide_4 .sf_4cols_2_25,\r\n                                            .sf_4cols_hide_2.sf_4cols_hide_4 .sf_4cols_4_25,\r\n                                            .sf_4cols_hide_3.sf_4cols_hide_4 .sf_4cols_3_25,\r\n                                            .sf_4cols_hide_3.sf_4cols_hide_4 .sf_4cols_4_25,\r\n                                            .sf_4cols_hide_2.sf_4cols_hide_3 .sf_4cols_2_25,\r\n                                            .sf_4cols_hide_2.sf_4cols_hide_3 .sf_4cols_3_25 {display: none}\r\n\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_3 .sf_4cols_2_25,\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_3 .sf_4cols_4_25,\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_2 .sf_4cols_3_25,\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_2 .sf_4cols_4_25,\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_4 .sf_4cols_2_25,\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_4 .sf_4cols_3_25,\r\n                                            .sf_4cols_hide_2.sf_4cols_hide_4 .sf_4cols_1_25,\r\n                                            .sf_4cols_hide_2.sf_4cols_hide_4 .sf_4cols_3_25,\r\n                                            .sf_4cols_hide_3.sf_4cols_hide_4 .sf_4cols_1_25,\r\n                                            .sf_4cols_hide_3.sf_4cols_hide_4 .sf_4cols_2_25,\r\n                                            .sf_4cols_hide_2.sf_4cols_hide_3 .sf_4cols_1_25,\r\n                                            .sf_4cols_hide_2.sf_4cols_hide_3 .sf_4cols_4_25 {width: 50% !important;}\r\n\r\n                                            /*------------------------------------------------\r\n                                                3 of 4 are hidden (default transformations)\r\n                                            ------------------------------------------------*/\r\n                                            .sf_4cols_hide_2.sf_4cols_hide_3.sf_4cols_hide_4 .sf_4cols_1_25,\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_3.sf_4cols_hide_4 .sf_4cols_2_25,\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_2.sf_4cols_hide_4 .sf_4cols_3_25,\r\n                                            .sf_4cols_hide_1.sf_4cols_hide_2.sf_4cols_hide_3 .sf_4cols_4_25 {width: 100%}\r\n\r\n                                            /*------------------------------------------------\r\n                                                1 of 5 is hidden (default transformations)\r\n                                            ------------------------------------------------*/\r\n                                            .sf_5cols_hide_5 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_4 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_1 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_2 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_3 .sf_5cols_3_20 {display: none}\r\n\r\n                                            .sf_5cols_hide_1 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_1 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_1 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_1 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_2 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_2 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_2 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_2 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_3 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_3 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_3 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_3 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_4 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_4 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_4 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_4 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_5 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_5 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_5 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_5 .sf_5cols_4_20 {width: 25%}\r\n\r\n                                            /*------------------------------------------------\r\n                                                2 of 5 are hidden (default transformations)\r\n                                            ------------------------------------------------*/\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2 .sf_5cols_1_20, \r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3 .sf_5cols_1_20, \r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_4 .sf_5cols_1_20, \r\n                                            .sf_5cols_hide_1.sf_5cols_hide_4 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_5 .sf_5cols_1_20, \r\n                                            .sf_5cols_hide_1.sf_5cols_hide_5 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_5 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_5 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_4 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_4 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_4_20 {display: none}\r\n\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3 .sf_5cols_2_20, \r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_4 .sf_5cols_2_20, \r\n                                            .sf_5cols_hide_1.sf_5cols_hide_4 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2 .sf_5cols_3_20, \r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_5 .sf_5cols_2_20, \r\n                                            .sf_5cols_hide_1.sf_5cols_hide_5 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_5 .sf_5cols_1_20, \r\n                                            .sf_5cols_hide_2.sf_5cols_hide_5 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_1_20, \r\n                                            .sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_1_20, \r\n                                            .sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3 .sf_5cols_1_20, \r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_4 .sf_5cols_1_20, \r\n                                            .sf_5cols_hide_2.sf_5cols_hide_4 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_1_20, \r\n                                            .sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_5_20 {width: 33%}\r\n\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_4 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_5 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_5 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_4 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_2_20 {width: 34%}\r\n\r\n                                            /*------------------------------------------------\r\n                                                3 of 5 are hidden (default transformations)\r\n                                            ------------------------------------------------*/\r\n\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_5 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_5 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_5 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_4 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_4 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_4 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_3 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_3 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_3 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_3.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_3.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_3.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_5_20 {display: none}\r\n\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_3 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_3 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_4 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_4 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_5 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_5 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_3.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_3.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_5_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_3_20 {width: 50%}\r\n\r\n                                            /*------------------------------------------------\r\n                                                4 of 5 are hidden (default transformations)\r\n                                            ------------------------------------------------*/\r\n                                            .sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_1_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_3.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_2_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_4.sf_5cols_hide_5 .sf_5cols_3_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_5 .sf_5cols_4_20,\r\n                                            .sf_5cols_hide_1.sf_5cols_hide_2.sf_5cols_hide_3.sf_5cols_hide_4 .sf_5cols_5_20 { width: 100%}";
      if (this.NavigationTransformations.Count == 0)
        this.NavigationTransformations.Add(new NavigationTransformationElement()
        {
          Name = "ToToggleMenu",
          Title = "ToToggleMenu",
          ResourceClassId = typeof (ResponsiveDesignResources).Name,
          IsActive = true,
          TransformationCss = "{{selector}} .sfNavToggle\r\n{\r\n        display: inline-block;\r\n}\r\n{{selector}} .sfNavList, {{selector}} .sfNavSelect, {{selector}} .k-plus, {{selector}} .k-minus\r\n{\r\n        display: none;\r\n}\r\n{{selector}} .sfNavList.sfShown\r\n{\r\n        display: block;\r\n}\r\n{{selector}} li\r\n{\r\n        margin-left: 0;\r\n        float: none !important;\r\n}\r\n{{selector}} ul.sfNavHorizontalSiteMap > li, {{selector}} .sfLevel1\r\n{\r\n        margin-bottom: 10px;\r\n}\r\n{{selector}} .k-animation-container, {{selector}} .k-menu .k-group, {{selector}} .k-treeview .k-group .k-group\r\n{\r\n        position: static !important;\r\n        display: block !important;\r\n        transform: none !important;\r\n}\r\n{{selector}} .k-group\r\n{\r\n        margin-left: 10px;\r\n}\r\n"
        });
      this.NavigationTransformations.Add(new NavigationTransformationElement()
      {
        Name = "ToDropDown",
        Title = "ToDropDown",
        ResourceClassId = typeof (ResponsiveDesignResources).Name,
        IsActive = true,
        TransformationCss = "{{selector}} .sfNavSelect\r\n{\r\n       display: block;\r\n}\r\n{{selector}} .sfNavList,{{selector}} .sfNavList.sfShown, {{selector}} .sfNavToggle\r\n{\r\n       display: none;\r\n}\r\n"
      });
      this.NavigationTransformations.Add(new NavigationTransformationElement()
      {
        Name = "HideNavigation",
        Title = "HideNavigation",
        ResourceClassId = typeof (ResponsiveDesignResources).Name,
        IsActive = true,
        TransformationCss = "{{selector}}\r\n{\r\n       display: none;\r\n}\r\n"
      });
    }

    /// <summary>Initializes the default views.</summary>
    protected override void InitializeDefaultViews(
      ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
    {
      contentViewControls.Add("RulesGroupBackend", ResponsiveDesignDefinitions.DefineMediaQueriesBackend((ConfigElement) contentViewControls));
    }

    /// <summary>Initializes the default providers.</summary>
    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "OpenAccessDataProvider",
        Description = "A provider that stores Responsive Design module data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessResponsiveDesignProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "/ResponsiveDesign"
          }
        }
      });
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Properties
    {
      public const string Enabled = "enabled";
      public const string DeviceTypes = "deviceTypes";
      public const string LayoutElements = "layoutElements";
      public const string PreviewDevices = "previewDevices";
      public const string HiddenLayoutsCss = "hiddenLayoutsCss";
      public const string ControlTransformations = "controlTransformationElements";
    }
  }
}
