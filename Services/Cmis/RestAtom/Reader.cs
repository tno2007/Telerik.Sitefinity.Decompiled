// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Cmis.RestAtom.Reader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Xml;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.SiteSync;

namespace Telerik.Sitefinity.Services.Cmis.RestAtom
{
  internal class Reader
  {
    static Reader() => ObjectFactory.Container.RegisterType<Reader>((LifetimeManager) new ContainerControlledLifetimeManager());

    public static Reader GetReader() => ObjectFactory.Resolve<Reader>();

    public void ReadXml(HttpRequestBase request, ISiteSyncImportTransaction transaction)
    {
      this.ReadHeaders(request, transaction);
      Stream stream = (Stream) new GZipStream(request.InputStream, CompressionMode.Decompress);
      XmlReader xmlReader1 = (XmlReader) null;
      XmlReader xmlReader2;
      try
      {
        xmlReader2 = this.CreateXmlReader(stream);
      }
      catch
      {
        xmlReader1?.Dispose();
        stream.Dispose();
        throw;
      }
      transaction.Items = this.ReadObjects(xmlReader2, stream);
    }

    public void ReadBinary(HttpRequestBase request, ISiteSyncImportTransaction transaction)
    {
      this.ReadHeaders(request, transaction);
      WrapperObject wrapperObject = new WrapperObject((object) null);
      this.ReadObjectPropertyHeaders(request, wrapperObject);
      wrapperObject.AddProperty("BlobStream", (object) request.InputStream);
      transaction.Items = (IEnumerable<WrapperObject>) new WrapperObject[1]
      {
        wrapperObject
      };
    }

    protected XmlReader CreateXmlReader(Stream stream)
    {
      XmlReader xmlReader = XmlReader.Create(stream, new XmlReaderSettings()
      {
        CheckCharacters = false
      });
      while (xmlReader.NodeType != XmlNodeType.Element)
        xmlReader.Read();
      if (xmlReader.LocalName != "entry" || xmlReader.NamespaceURI != "http://www.w3.org/2005/Atom")
        throw new FormatException("atom:entry expected as a root element.");
      return xmlReader;
    }

    private void ReadHeaders(HttpRequestBase request, ISiteSyncImportTransaction transaction)
    {
      NameValueCollection headers = request.Headers;
      foreach (string name in ((IEnumerable<string>) headers.AllKeys).Where<string>((Func<string, bool>) (h => h.StartsWith("SF-") && !h.StartsWith("SF-Prop-"))))
      {
        string key = name.Substring("SF-".Length);
        string str = HttpUtility.UrlDecode(headers[name]);
        transaction.Headers.Add(key, str);
      }
    }

    private void ReadObjectPropertyHeaders(HttpRequestBase request, WrapperObject obj)
    {
      NameValueCollection headers = request.Headers;
      foreach (string name1 in ((IEnumerable<string>) headers.AllKeys).Where<string>((Func<string, bool>) (item => item.StartsWith("SF-Prop-"))))
      {
        string name2 = name1.Substring("SF-Prop-".Length);
        string[] strArray = HttpUtility.UrlDecode(headers[name1]).Split(':');
        object obj1 = this.ParseValue(strArray[1], strArray[0]);
        obj.AddProperty(name2, obj1);
      }
    }

    protected virtual IEnumerable<WrapperObject> ReadObjects(
      XmlReader reader,
      Stream stream)
    {
      List<WrapperObject> wrapperObjectList = new List<WrapperObject>();
      using (stream)
      {
        using (reader)
        {
          while (reader.Read())
          {
            bool flag = true;
            while (!(reader.LocalName == "object") || !(reader.NamespaceURI == "http://docs.oasis-open.org/ns/cmis/restatom/200908/"))
            {
              flag = reader.Read();
              if (!flag)
                break;
            }
            if (flag)
            {
              WrapperObject wrapperObject = this.ReadObject(reader);
              wrapperObjectList.Add(wrapperObject);
            }
            else
              break;
          }
        }
      }
      return (IEnumerable<WrapperObject>) wrapperObjectList;
    }

    protected virtual WrapperObject ReadObject(XmlReader reader)
    {
      WrapperObject wrapperObject = new WrapperObject((object) null);
      reader.ReadStartElement("object", "http://docs.oasis-open.org/ns/cmis/restatom/200908/");
      reader.ReadStartElement("properties", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      while (reader.Read())
      {
        if (!(reader.LocalName == "object") || !(reader.NamespaceURI == "http://docs.oasis-open.org/ns/cmis/restatom/200908/"))
        {
          if (reader.NodeType == XmlNodeType.Element)
          {
            string namespaceUri = reader.NamespaceURI;
            if (!(namespaceUri != "http://docs.oasis-open.org/ns/cmis/core/200908/") || !(namespaceUri != "urn:telerik:sitefinity:cmis"))
            {
              string propType = reader.LocalName;
              string name = this.SplitPropertyName(reader.GetAttribute("propertyDefinitionId"));
              if (!string.IsNullOrEmpty(name))
              {
                string attribute = reader.GetAttribute("collectionType", "urn:telerik:sitefinity:cmis");
                if (reader.IsEmptyElement)
                  wrapperObject.AddProperty(name, (object) null);
                else if (attribute == "IDictionary")
                {
                  IDictionary<string, string> dictionary = this.ReadDictionary(reader.ReadSubtree());
                  wrapperObject.AddProperty(name, (object) dictionary);
                }
                else
                {
                  IList<string> source1 = this.ReadValues(reader.ReadSubtree());
                  if (attribute == "IEnumerable")
                  {
                    IEnumerable<object> source2 = source1.Select<string, object>((Func<string, object>) (v => this.ParseValue(v, propType)));
                    IList instance = (IList) Activator.CreateInstance(typeof (List<>).MakeGenericType(source2.Any<object>() ? source2.FirstOrDefault<object>().GetType() : typeof (object)));
                    foreach (object obj in source2)
                      instance.Add(obj);
                    wrapperObject.AddProperty(name, (object) instance);
                  }
                  else
                  {
                    object obj = this.ParseValue(source1.FirstOrDefault<string>(), propType);
                    wrapperObject.AddProperty(name, obj);
                  }
                }
              }
            }
          }
        }
        else
          break;
      }
      return wrapperObject;
    }

    private IList<string> ReadValues(XmlReader reader)
    {
      List<string> values = new List<string>();
      this.ReadValues(reader, (Action<XmlReader>) (r => values.Add(this.ReadString(r))));
      return (IList<string>) values;
    }

    private IDictionary<string, string> ReadDictionary(XmlReader reader)
    {
      Dictionary<string, string> values = new Dictionary<string, string>();
      this.ReadValues(reader, (Action<XmlReader>) (r =>
      {
        string attribute = r.GetAttribute("key", "urn:telerik:sitefinity:cmis");
        if (attribute == null)
          throw new FormatException("Invalid dictionary element with no key.");
        string str = r.ReadElementContentAsString();
        values.Add(attribute, str);
      }));
      return (IDictionary<string, string>) values;
    }

    private void ReadValues(XmlReader reader, Action<XmlReader> readValueElement)
    {
      while (reader.Read())
      {
        if (reader.NodeType == XmlNodeType.Element && reader.Name.Contains("value"))
          readValueElement(reader);
      }
    }

    private string ReadString(XmlReader reader)
    {
      string attribute1 = reader.GetAttribute("lang", "urn:telerik:sitefinity:cmis");
      string attribute2 = reader.GetAttribute("invariantValue", "urn:telerik:sitefinity:cmis");
      string str = reader.ReadElementContentAsString().Replace("\n", "\r\n");
      if (attribute1 != null)
        str += string.Format("\0{0}\0{1}", (object) attribute1, (object) attribute2);
      return str;
    }

    internal static string GetLstringValue(object obj, CultureInfo culture = null)
    {
      if (obj is string lstringValue)
        return lstringValue;
      if (culture == null)
        culture = SystemManager.CurrentContext.AppSettings.Multilingual ? SystemManager.CurrentContext.Culture : CultureInfo.InvariantCulture;
      Lstring lstring = obj as Lstring;
      if (lstring != (Lstring) null)
        return lstring[culture];
      return obj is IDictionary<CultureInfo, string> dictionary && dictionary.ContainsKey(culture) ? dictionary[culture] : (string) null;
    }

    protected virtual object ParseValue(string str, string cmisTypeName)
    {
      if (str == null)
        return (object) null;
      object obj;
      switch (cmisTypeName)
      {
        case "propertyBoolean":
          obj = (object) bool.Parse(str);
          break;
        case "propertyDateTime":
          obj = (object) DateTime.Parse(str).ToUniversalTime();
          break;
        case "propertyDecimal":
          obj = (object) Decimal.Parse(str.Replace(" ", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowCurrencySymbol, (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat);
          break;
        case "propertyDouble":
          obj = (object) double.Parse(str.Replace(" ", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowCurrencySymbol, (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat);
          break;
        case "propertyFloat":
          obj = (object) float.Parse(str.Replace(" ", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowCurrencySymbol, (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat);
          break;
        case "propertyId":
          Guid result;
          obj = !Guid.TryParse(str, out result) ? (object) str : (object) result;
          break;
        case "propertyInteger":
          obj = (object) long.Parse(str);
          break;
        case "propertyString":
          string[] strArray = str.Split(new char[1]);
          if (strArray.Length == 1)
          {
            obj = (object) str;
            break;
          }
          obj = (object) new Dictionary<CultureInfo, string>()
          {
            [CultureInfo.GetCultureInfo(strArray[1])] = strArray[0],
            [CultureInfo.InvariantCulture] = strArray[2]
          };
          break;
        default:
          obj = (object) str;
          break;
      }
      return obj;
    }

    private string SplitPropertyName(string mergedName)
    {
      string empty = string.Empty;
      if (!string.IsNullOrEmpty(mergedName))
      {
        char ch = ':';
        if (mergedName.Contains(ch.ToString()))
        {
          string[] strArray = mergedName.Split(':');
          if (strArray != null && strArray.Length >= 2)
            empty = strArray[1];
        }
      }
      return empty;
    }
  }
}
