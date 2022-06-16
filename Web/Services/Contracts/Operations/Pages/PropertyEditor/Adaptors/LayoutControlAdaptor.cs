// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.LayoutControlAdaptor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Utilities.HtmlParsing;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors
{
  internal class LayoutControlAdaptor : ComponentAdaptorBase
  {
    public override bool CanAdaptComponent(ControlData component) => component.ObjectType.Contains("Telerik.Sitefinity.Frontend.GridSystem.GridControl") || component.ObjectType.Contains("Telerik.Sitefinity.Web.UI.LayoutControl");

    public override IList<PropertyValueContainer> AdaptValuesForSerialization(
      IEnumerable<WcfControlProperty> wcfProperties,
      IAdaptValuesContext context)
    {
      List<PropertyValueContainer> propertyValueContainerList = new List<PropertyValueContainer>();
      WcfControlProperty wcfControlProperty = wcfProperties.FirstOrDefault<WcfControlProperty>((Func<WcfControlProperty, bool>) (x => x.PropertyName == "Layout"));
      if (wcfControlProperty != null)
      {
        foreach (LayoutControlAdaptor.ElementData element in (IEnumerable<LayoutControlAdaptor.ElementData>) this.GetViewData(wcfControlProperty.PropertyValue).Elements)
        {
          string name = element.Name;
          propertyValueContainerList.Add(new PropertyValueContainer()
          {
            Name = name + "_Css",
            Value = element.Css
          });
          if (!string.IsNullOrEmpty(element.Label))
            propertyValueContainerList.Add(new PropertyValueContainer()
            {
              Name = name + "_Label",
              Value = element.Label
            });
        }
      }
      return (IList<PropertyValueContainer>) propertyValueContainerList;
    }

    public override ControlMetadata AdaptControlMetadata(IAdaptControlArgs args)
    {
      LayoutControlAdaptor.ViewData viewData = this.GetViewData(args.ControlData);
      string str = string.Format("{0}-{1}", (object) viewData.Columns.Count, viewData.Columns.Count > 1 ? (object) "columns" : (object) "column");
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      string[] placeHolders = args.ControlData.PlaceHolders;
      string[] array = viewData.Columns.Select<LayoutControlAdaptor.ElementData, string>((Func<LayoutControlAdaptor.ElementData, string>) (x => x.Name)).ToArray<string>();
      for (int index = 0; index < placeHolders.Length; ++index)
        dictionary.Add(placeHolders[index], array[index]);
      ControlMetadata controlMetadata = base.AdaptControlMetadata(args);
      LayoutControlMetadata layoutControlMetadata = new LayoutControlMetadata();
      layoutControlMetadata.Name = "Layout";
      layoutControlMetadata.Caption = string.Format("Layout", str != null ? (object) (" " + str) : (object) string.Empty);
      layoutControlMetadata.ViewName = str;
      layoutControlMetadata.PlaceHolderMap = (IDictionary<string, string>) dictionary;
      layoutControlMetadata.PropertyMetadata = controlMetadata.PropertyMetadata;
      layoutControlMetadata.PropertyMetadataFlat = controlMetadata.PropertyMetadataFlat;
      return (ControlMetadata) layoutControlMetadata;
    }

    private LayoutControlAdaptor.ViewData GetViewData(string htmlContent)
    {
      if (htmlContent.StartsWith("~"))
      {
        using (StreamReader streamReader = new StreamReader(VirtualPathManager.OpenFile(htmlContent)))
          htmlContent = streamReader.ReadToEnd();
      }
      LayoutControlAdaptor.ViewData viewData = new LayoutControlAdaptor.ViewData();
      using (HtmlParser htmlParser = new HtmlParser(htmlContent))
      {
        htmlParser.SetChunkHashMode(false);
        htmlParser.AutoExtractBetweenTagsOnly = false;
        htmlParser.CompressWhiteSpaceBeforeTag = false;
        htmlParser.KeepRawHTML = true;
        HtmlChunk next;
        while ((next = htmlParser.ParseNext()) != null)
        {
          if (next.Type == HtmlChunkType.OpenTag)
          {
            string attributeValue1 = this.GetAttributeValue(next, "data-sf-element");
            if (attributeValue1 != null)
            {
              string str1 = attributeValue1.Replace(" ", string.Empty);
              string str2 = this.GetAttributeValue(next, "class");
              bool flag = false;
              if (str2 != null)
              {
                if (str2.Contains("sf_colsIn"))
                {
                  flag = true;
                  str2 = str2.Replace("sf_colsIn", string.Empty);
                }
                str2 = str2.Trim();
              }
              string attributeValue2 = this.GetAttributeValue(next, "data-placeholder-label");
              LayoutControlAdaptor.ElementData elementData = new LayoutControlAdaptor.ElementData()
              {
                Css = str2,
                Label = attributeValue2,
                Name = str1
              };
              if (flag)
                viewData.Columns.Add(elementData);
              viewData.Elements.Add(elementData);
            }
          }
        }
      }
      return viewData;
    }

    private LayoutControlAdaptor.ViewData GetViewData(ControlData controlData)
    {
      LayoutControlAdaptor.ViewData viewData = new LayoutControlAdaptor.ViewData();
      ControlProperty controlProperty = controlData.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (x => x.Name == "Layout"));
      return controlProperty != null && !string.IsNullOrEmpty(controlProperty.Value) ? this.GetViewData(controlProperty.Value) : viewData;
    }

    private string GetAttributeValue(HtmlChunk chunk, string attributeName)
    {
      int index = Array.FindIndex<string>(chunk.Attributes, 0, chunk.ParamsCount, (Predicate<string>) (i => i.Equals(attributeName, StringComparison.OrdinalIgnoreCase)));
      return index != -1 ? chunk.Values[index] : (string) null;
    }

    private class ViewData
    {
      public ViewData()
      {
        this.Elements = (IList<LayoutControlAdaptor.ElementData>) new List<LayoutControlAdaptor.ElementData>();
        this.Columns = (IList<LayoutControlAdaptor.ElementData>) new List<LayoutControlAdaptor.ElementData>();
      }

      public IList<LayoutControlAdaptor.ElementData> Elements { get; }

      public IList<LayoutControlAdaptor.ElementData> Columns { get; }
    }

    private class ElementData
    {
      public string Css { get; set; }

      public string Label { get; set; }

      public string Name { get; set; }
    }
  }
}
