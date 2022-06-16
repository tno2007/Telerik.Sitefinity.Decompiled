// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Cmis.RestAtom.Writer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Http.Headers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;
using System.Xml;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.SiteSync.Serialization;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Services.Cmis.RestAtom
{
  internal class Writer : IDisposable
  {
    protected XmlWriter writer;

    public void Dispose() => this.DisposeStream();

    public Stream OutputStream
    {
      set
      {
        this.DisposeStream();
        this.writer = XmlWriter.Create(value, new XmlWriterSettings()
        {
          Indent = true,
          CheckCharacters = false
        });
      }
    }

    private void DisposeStream()
    {
      if (this.writer == null)
        return;
      this.writer.Dispose();
      this.writer = (XmlWriter) null;
    }

    /// <summary>
    /// Appends the XML namespaces and prefixes as specified in CMIS v1.0. chapter 3.1.1
    /// </summary>
    protected virtual void AppendXmlNamespaces()
    {
      this.writer.WriteAttributeString("xmlns", "app", (string) null, "http://www.w3.org/2007/app");
      this.writer.WriteAttributeString("xmlns", "atom", (string) null, "http://www.w3.org/2005/Atom");
      this.writer.WriteAttributeString("xmlns", "cmis", (string) null, "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteAttributeString("xmlns", "cmisra", (string) null, "http://docs.oasis-open.org/ns/cmis/restatom/200908/");
      this.writer.WriteAttributeString("xmlns", "sf", (string) null, "urn:telerik:sitefinity:cmis");
      this.writer.WriteAttributeString("xml", "base", (string) null, UrlPath.ResolveAbsoluteUrl("~/Sitefinity/CMIS/RestAtom/"));
    }

    public virtual void WriteTransactionStart()
    {
      this.writer.WriteStartDocument();
      this.writer.WriteStartElement("atom", "entry", "http://www.w3.org/2005/Atom");
      this.AppendXmlNamespaces();
    }

    public virtual void WriteTransactionEnd()
    {
      this.writer.WriteEndElement();
      this.writer.Flush();
    }

    public virtual void WriteObject(object obj)
    {
      string cultureName = (string) null;
      PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(obj).Find("LangId", true);
      if (propertyDescriptor != null)
        cultureName = (string) propertyDescriptor.GetValue(obj);
      using (new CultureRegion(cultureName))
      {
        this.writer.WriteStartElement("object", "http://docs.oasis-open.org/ns/cmis/restatom/200908/");
        this.writer.WriteStartElement("properties", "http://docs.oasis-open.org/ns/cmis/core/200908/");
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj))
          this.WriteProperty(property, obj);
        this.writer.WriteEndElement();
        this.writer.WriteEndElement();
      }
    }

    private void WriteProperty(PropertyDescriptor prop, object instance)
    {
      Type type;
      object obj;
      if (prop.PropertyType == typeof (RelatedItems))
      {
        type = prop.PropertyType;
        obj = new object();
      }
      else
      {
        obj = prop.GetValue(instance);
        type = obj == null ? prop.PropertyType : obj.GetType();
      }
      var settings = new{ Item = instance, Property = prop };
      string invariantValue = (string) null;
      if (prop.PropertyType == typeof (Lstring) && SystemManager.CurrentContext.AppSettings.Multilingual && prop is ILocalizablePropertyDescriptor propertyDescriptor)
        invariantValue = propertyDescriptor.GetValue(instance, CultureInfo.InvariantCulture) as string;
      Type keyType;
      Type valueType;
      this.GetTypes(ref type, out keyType, out valueType);
      string localName = this.GetCmisPropertyType(type);
      string ns = localName != null ? "http://docs.oasis-open.org/ns/cmis/core/200908/" : "urn:telerik:sitefinity:cmis";
      if (localName == null)
        localName = type.FullName;
      this.writer.WriteStartElement(localName, ns);
      this.writer.WriteAttributeString("propertyDefinitionId", string.Format("{0}:{1}", Const.CmisPropertyDefinitionIds.Contains(prop.Name) ? (object) "cmis" : (object) "sf", (object) prop.Name));
      if (obj != null)
      {
        if (keyType != (Type) null)
          this.WriteDictionary((IDictionary) obj, valueType, keyType);
        else if (valueType != (Type) null)
          this.WriteEnumerable((IEnumerable) obj, type, (object) settings);
        else if (invariantValue != null)
          this.WriteLString(obj, type, invariantValue);
        else
          this.WritePropertyValue(obj, type, (object) settings);
      }
      this.writer.WriteEndElement();
    }

    protected virtual void WriteEnumerable(IEnumerable value, Type type, object settings)
    {
      this.writer.WriteAttributeString("collectionType", "urn:telerik:sitefinity:cmis", "IEnumerable");
      foreach (object obj in value)
      {
        // ISSUE: reference to a compiler-generated field
        if (Writer.\u003C\u003Eo__9.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Writer.\u003C\u003Eo__9.\u003C\u003Ep__0 = CallSite<Action<CallSite, Writer, object, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "WritePropertyValue", (IEnumerable<Type>) null, typeof (Writer), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        Writer.\u003C\u003Eo__9.\u003C\u003Ep__0.Target((CallSite) Writer.\u003C\u003Eo__9.\u003C\u003Ep__0, this, obj, type, settings);
      }
    }

    protected virtual void WriteDictionary(IDictionary value, Type valueType, Type keyType)
    {
      this.writer.WriteAttributeString("collectionType", "urn:telerik:sitefinity:cmis", "IDictionary");
      foreach (DictionaryEntry dictionaryEntry in value)
      {
        DictionaryEntry entry = dictionaryEntry;
        this.WritePropertyValue(entry.Value, valueType, (object) null, (Action) (() => this.writer.WriteAttributeString("key", "urn:telerik:sitefinity:cmis", this.SerializeValue(entry.Key, keyType, (object) null))));
      }
    }

    protected virtual void WriteLString(object value, Type valueType, string invariantValue) => this.WritePropertyValue(value, valueType, (object) null, (Action) (() =>
    {
      this.writer.WriteAttributeString("lang", "urn:telerik:sitefinity:cmis", SystemManager.CurrentContext.Culture.Name);
      this.writer.WriteAttributeString(nameof (invariantValue), "urn:telerik:sitefinity:cmis", invariantValue);
    }));

    protected virtual void WritePropertyValue(
      object value,
      Type valueType,
      object settings,
      Action writeAttrs = null)
    {
      if (value == null)
        return;
      this.writer.WriteStartElement(nameof (value), "http://docs.oasis-open.org/ns/cmis/core/200908/");
      if (writeAttrs != null)
        writeAttrs();
      // ISSUE: reference to a compiler-generated field
      if (Writer.\u003C\u003Eo__12.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Writer.\u003C\u003Eo__12.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (Writer)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target = Writer.\u003C\u003Eo__12.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p1 = Writer.\u003C\u003Eo__12.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (Writer.\u003C\u003Eo__12.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Writer.\u003C\u003Eo__12.\u003C\u003Ep__0 = CallSite<Func<CallSite, Writer, object, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "SerializeValue", (IEnumerable<Type>) null, typeof (Writer), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = Writer.\u003C\u003Eo__12.\u003C\u003Ep__0.Target((CallSite) Writer.\u003C\u003Eo__12.\u003C\u003Ep__0, this, value, valueType, settings);
      this.writer.WriteValue(target((CallSite) p1, obj));
      this.writer.WriteEndElement();
    }

    protected virtual string SerializeValue(object value, Type type, object settings)
    {
      string empty = string.Empty;
      switch (this.GetCmisPropertyType(type))
      {
        case "propertyBoolean":
        case "propertyDecimal":
        case "propertyDouble":
        case "propertyFloat":
        case "propertyId":
        case "propertyInteger":
        case "propertyString":
          return this.GetStringValue(value, type);
        case "propertyDateTime":
          return ((DateTime) value).ToString("u");
        case "propertyStream":
          return Convert.ToBase64String(this.DownloadStream((Stream) value));
        default:
          ISiteSyncConverter siteSyncConverter = this.ResolveConverter(type);
          // ISSUE: reference to a compiler-generated field
          if (Writer.\u003C\u003Eo__13.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Writer.\u003C\u003Eo__13.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (Writer)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, string> target = Writer.\u003C\u003Eo__13.\u003C\u003Ep__1.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, string>> p1 = Writer.\u003C\u003Eo__13.\u003C\u003Ep__1;
          // ISSUE: reference to a compiler-generated field
          if (Writer.\u003C\u003Eo__13.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Writer.\u003C\u003Eo__13.\u003C\u003Ep__0 = CallSite<Func<CallSite, ISiteSyncConverter, object, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Serialize", (IEnumerable<Type>) null, typeof (Writer), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = Writer.\u003C\u003Eo__13.\u003C\u003Ep__0.Target((CallSite) Writer.\u003C\u003Eo__13.\u003C\u003Ep__0, siteSyncConverter, value, type, settings);
          return target((CallSite) p1, obj);
      }
    }

    protected virtual ISiteSyncConverter ResolveConverter(
      Type type,
      string prefix = null)
    {
      return SiteSyncSerializer.ResolveConverter(type, true, true, prefix);
    }

    protected virtual string GetStringValue(object value, Type type)
    {
      string stringValue = string.Empty;
      if (type == typeof (string))
      {
        stringValue = (string) value;
      }
      else
      {
        TypeConverter converter = TypeDescriptor.GetConverter(value);
        if (converter != null && converter.CanConvertTo(typeof (string)))
          stringValue = converter.ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, value);
      }
      return stringValue;
    }

    protected virtual byte[] DownloadStream(Stream imageStream)
    {
      byte[] buffer = (byte[]) null;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        buffer = new byte[imageStream.Length];
        imageStream.Position = 0L;
        int count = imageStream.Read(buffer, 0, buffer.Length);
        memoryStream.Write(buffer, 0, count);
      }
      return buffer;
    }

    private void GetTypes(ref Type type, out Type keyType, out Type valueType)
    {
      keyType = (Type) null;
      valueType = (Type) null;
      Type interfaceImplementation1 = type.GetGenericInterfaceImplementation(typeof (IDictionary<,>));
      if (interfaceImplementation1 != (Type) null)
      {
        Type[] genericArguments = interfaceImplementation1.GetGenericArguments();
        keyType = genericArguments[0];
        valueType = genericArguments[1];
      }
      else if (typeof (IDictionary).IsAssignableFrom(type))
        keyType = valueType = typeof (object);
      else if (type != typeof (string) && type != typeof (Lstring))
      {
        Type interfaceImplementation2 = type.GetGenericInterfaceImplementation(typeof (IEnumerable<>));
        if (interfaceImplementation2 != (Type) null)
          valueType = interfaceImplementation2.GetGenericArguments()[0];
        else if (typeof (IEnumerable).IsAssignableFrom(type))
          valueType = typeof (object);
      }
      ref Type local1 = ref type;
      Type type1 = valueType;
      if ((object) type1 == null)
        type1 = type;
      local1 = type1;
      ref Type local2 = ref type;
      Type type2 = Nullable.GetUnderlyingType(type);
      if ((object) type2 == null)
        type2 = type;
      local2 = type2;
    }

    protected virtual string GetCmisPropertyType(Type type)
    {
      if (typeof (Stream).IsAssignableFrom(type))
        return "propertyStream";
      if (type.IsEnum)
        return "propertyString";
      switch (type.Name)
      {
        case "Boolean":
          return "propertyBoolean";
        case "Byte":
        case "Int16":
        case "Int32":
        case "Int64":
        case "UInt32":
          return "propertyInteger";
        case "DateTime":
          return "propertyDateTime";
        case "Decimal":
          return "propertyDecimal";
        case "Double":
          return "propertyDouble";
        case "Guid":
          return "propertyId";
        case "Lstring":
        case "String":
          return "propertyString";
        case "Single":
          return "propertyFloat";
        default:
          return (string) null;
      }
    }

    public virtual void AddMandatoryHeaders(RequestHeaders headers)
    {
      byte incrementalGuidRange = OpenAccessConnection.GetIncrementalGuidRange();
      Writer.AddHeader(headers, "SF-SourceIncrementalGuidRange", incrementalGuidRange.ToString());
    }

    public virtual void AddTransactionHeaders(
      RequestHeaders headers,
      IDictionary<string, string> transactionHeaders)
    {
      foreach (KeyValuePair<string, string> transactionHeader in (IEnumerable<KeyValuePair<string, string>>) transactionHeaders)
      {
        string name = "SF-" + transactionHeader.Key;
        Writer.AddHeader(headers, name, transactionHeader.Value);
      }
    }

    public virtual void AddObjectPropertyHeaders(RequestHeaders headers, object obj)
    {
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj))
        this.AddObjectPropertyHeader(headers, property, obj);
    }

    private void AddObjectPropertyHeader(
      RequestHeaders headers,
      PropertyDescriptor prop,
      object instance)
    {
      if (prop.Name == "BlobStream")
        return;
      object obj = prop.GetValue(instance);
      if (obj == null)
        return;
      Type type = obj == null ? prop.PropertyType : obj.GetType();
      Type valueType;
      this.GetTypes(ref type, out Type _, out valueType);
      if (!(valueType == (Type) null))
        return;
      string cmisPropertyType = this.GetCmisPropertyType(type);
      string name = "SF-Prop-" + prop.Name;
      string str1 = obj.ToString();
      string str2 = cmisPropertyType + ":" + str1;
      Writer.AddHeader(headers, name, str2);
    }

    internal static void AddHeader(RequestHeaders headers, string name, string value) => headers.Add(name, HttpUtility.UrlEncode(value));

    /// <summary>
    /// Writes the service document to the provided output stream.
    /// </summary>
    /// <param name="output">The output stream.</param>
    public virtual void WriteServiceDocument(Stream output)
    {
      Stream output1 = output != null ? output : throw new ArgumentNullException(nameof (output));
      using (XmlWriter xmlWriter = XmlWriter.Create(output1, new XmlWriterSettings()
      {
        Indent = true,
        CheckCharacters = false
      }))
      {
        xmlWriter.WriteStartDocument();
        xmlWriter.WriteStartElement("app", "service", "http://www.w3.org/2007/app");
        this.AppendXmlNamespaces();
        this.AppendRepositories();
        xmlWriter.WriteEndElement();
        xmlWriter.Flush();
      }
    }

    /// <summary>Appends all repositories to the Service Document.</summary>
    protected virtual void AppendRepositories() => this.AppendRepository((IPublishingPoint) new PublishingPoint()
    {
      Id = Guid.Parse("D49CD7BE-8639-4F8B-9B08-E99B5B2B7212"),
      Name = "Main Repository"
    });

    /// <summary>
    /// Appends the specified repository to the Service Document.
    /// </summary>
    protected virtual void AppendRepository(IPublishingPoint publishingPoint)
    {
      this.writer.WriteStartElement("workspace", "http://www.w3.org/2007/app");
      this.writer.WriteStartElement("title", "http://www.w3.org/2005/Atom");
      this.writer.WriteValue(publishingPoint.Id.ToString());
      this.writer.WriteEndElement();
      this.AppendRootCollection(publishingPoint);
      this.AppendTypesCollection(publishingPoint);
      this.AppendQueryCollection(publishingPoint);
      this.AppendRepositoryInfo(publishingPoint);
      this.writer.WriteEndElement();
    }

    /// <summary>Appends the root collection.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    protected virtual void AppendRootCollection(IPublishingPoint publishingPoint)
    {
      this.writer.WriteStartElement("collection", "http://www.w3.org/2007/app");
      this.writer.WriteAttributeString("href", publishingPoint.Id.ToString() + "/children");
      this.writer.WriteStartElement("collectionType", "http://docs.oasis-open.org/ns/cmis/restatom/200908/");
      this.writer.WriteValue("root");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("title", "http://www.w3.org/2005/Atom");
      this.writer.WriteAttributeString("type", "text");
      this.writer.WriteValue("Root Collection");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("accept", "http://www.w3.org/2007/app");
      this.writer.WriteValue("application/atom+xml;type=entry");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("accept", "http://www.w3.org/2007/app");
      this.writer.WriteValue("application/cmisatom+xml;");
      this.writer.WriteEndElement();
      this.writer.WriteEndElement();
    }

    /// <summary>Appends the types collection.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    protected virtual void AppendTypesCollection(IPublishingPoint publishingPoint)
    {
      this.writer.WriteStartElement("collection", "http://www.w3.org/2007/app");
      this.writer.WriteAttributeString("href", publishingPoint.Id.ToString() + "/types");
      this.writer.WriteStartElement("collectionType", "http://docs.oasis-open.org/ns/cmis/restatom/200908/");
      this.writer.WriteValue("types");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("title", "http://www.w3.org/2005/Atom");
      this.writer.WriteAttributeString("type", "text");
      this.writer.WriteValue("Types Collection");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("accept", "http://www.w3.org/2007/app");
      this.writer.WriteEndElement();
      this.writer.WriteEndElement();
    }

    protected virtual void AppendQueryCollection(IPublishingPoint publishingPoint)
    {
      this.writer.WriteStartElement("collection", "http://www.w3.org/2007/app");
      this.writer.WriteAttributeString("href", publishingPoint.Id.ToString() + "/query");
      this.writer.WriteStartElement("collectionType", "http://docs.oasis-open.org/ns/cmis/restatom/200908/");
      this.writer.WriteValue("query");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("title", "http://www.w3.org/2005/Atom");
      this.writer.WriteAttributeString("type", "text");
      this.writer.WriteValue("Query Collection");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("accept", "http://www.w3.org/2007/app");
      this.writer.WriteValue("application/cmisquery+xml");
      this.writer.WriteEndElement();
      this.writer.WriteEndElement();
    }

    /// <summary>Appends the repository info.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    protected virtual void AppendRepositoryInfo(IPublishingPoint publishingPoint)
    {
      this.writer.WriteStartElement("repositoryInfo", "http://docs.oasis-open.org/ns/cmis/restatom/200908/");
      this.writer.WriteStartElement("repositoryId", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue(publishingPoint.Id.ToString());
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("repositoryName", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue(publishingPoint.Name);
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("repositoryDescription", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue((string) publishingPoint.Description);
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("vendorName", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue("Telerik");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("productName", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue("Sitefinity CMS");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("productVersion", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue("4.2");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("rootFolderId", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue(publishingPoint.Id.ToString());
      this.writer.WriteEndElement();
      this.AppendRepositoryCapabilities(publishingPoint);
      this.writer.WriteEndElement();
    }

    /// <summary>Appends the repository capabilities.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    protected virtual void AppendRepositoryCapabilities(IPublishingPoint publishingPoint)
    {
      this.writer.WriteStartElement("capabilities", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteStartElement("capabilityACL", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue("none");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("capabilityAllVersionsSearchable", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue("false");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("capabilityChanges", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue("none");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("capabilityContentStreamUpdatability", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue("none");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("capabilityGetDescendants", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue("true");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("capabilityGetFolderTree", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue("true");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("capabilityMultifiling", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue("false");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("apabilityPWCSearchable", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue("false");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("capabilityPWCUpdatable", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue("true");
      this.writer.WriteEndElement();
      this.writer.WriteStartElement("capabilityQuery", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      this.writer.WriteValue("bothcombined");
      this.writer.WriteEndElement();
      this.writer.WriteEndElement();
    }
  }
}
