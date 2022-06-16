// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Cmis.RestAtom.Const
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Services.Cmis.RestAtom
{
  internal static class Const
  {
    /// <summary>Defines namespace for Atom protocol.</summary>
    public const string XmlnsAtom = "http://www.w3.org/2005/Atom";
    /// <summary>Defines namespace for AtomPub protocol.</summary>
    public const string XmlnsAtomPub = "http://www.w3.org/2007/app";
    /// <summary>Defines namespace for CMIS core protocol.</summary>
    public const string XmlnsCmisCore = "http://docs.oasis-open.org/ns/cmis/core/200908/";
    /// <summary>Defines namespace for CMIS REST/AtomPub protocol.</summary>
    public const string XmlnsCmisRestAtom = "http://docs.oasis-open.org/ns/cmis/restatom/200908/";
    /// <summary>Defines namespace for Sitefinity specific elements.</summary>
    public const string XmlnsSitefinity = "urn:telerik:sitefinity:cmis";
    /// <summary>Defines the URL of the CMIS AtomPub endpoint.</summary>
    public const string EndPointUrl = "Sitefinity/CMIS/RestAtom/";
    /// <summary>
    /// The prefix used for all Sitefinity specific HTTP headers.
    /// </summary>
    public const string SitefinityHeaderPrefix = "SF-";
    /// <summary>
    /// The prefix used for HTTP headers used to transfer SiteSyncObject properties..
    /// </summary>
    public const string SitefinityObjectPropertyHeaderPrefix = "SF-Prop-";
    public const char SitefinityObjectPropertyHeaderTypeSeparator = ':';
    public static readonly ISet<string> CmisObjectProperyTypes = (ISet<string>) new HashSet<string>()
    {
      "propertyId",
      "propertyString",
      "propertyBoolean",
      "propertyInteger",
      "propertyDecimal",
      "propertyDouble",
      "propertyDateTime",
      "propertyUri",
      "propertyHtml"
    };
    public static readonly ISet<string> CmisPropertyDefinitionIds = (ISet<string>) new HashSet<string>()
    {
      "name",
      "objectId",
      "baseTypeId",
      "objectTypeId",
      "createdBy",
      "creationDate",
      "lastModifiedBy",
      "lastModificationDate",
      "changeToken",
      "isImmutable",
      "isLatestVersion",
      "isMajorVersion",
      "isLatestMajorVersion",
      "versionLabel",
      "versionSeriesId",
      "isVersionSeriesCheckedOut",
      "versionSeriesCheckedOutBy",
      "versionSeriesCheckedOutId",
      "checkinComment",
      "contentStreamLength",
      "contentStreamMimeType",
      "contentStreamFileName",
      "contentStreamId"
    };
    public const string SnapInType = "snapInType";
    public const string CollectionTypeAttr = "collectionType";
    public const string EnumerableCollectionType = "IEnumerable";
    public const string DictionaryCollectionType = "IDictionary";
    public const string DictionaryKeyAttr = "key";
    public const string LangAttr = "lang";
    public const string InvariantValueAttr = "invariantValue";
    /// <summary>The name of the blob stream wrapper object property.</summary>
    public const string BlobStream = "BlobStream";
    /// <summary>
    /// The name of the blob stream total size wrapper object property.
    /// </summary>
    public const string BlobTotalSize = "BlobTotalSize";
  }
}
