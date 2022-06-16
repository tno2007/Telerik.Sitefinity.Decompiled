// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.CursorCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  public class CursorCollection : KeyedCollection<string, PlaceHolderCursor>
  {
    public const string NamespacesPlaceHolderName = "sf_Namespaces";
    private CursorCollection.RegistrationCollection namespaces = new CursorCollection.RegistrationCollection();

    public CursorCollection()
      : base((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
    }

    protected override string GetKeyForItem(PlaceHolderCursor item) => item.Name;

    public bool TryGetValue(string key, out PlaceHolderCursor value)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      if (this.Dictionary != null)
        return this.Dictionary.TryGetValue(key, out value);
      foreach (PlaceHolderCursor placeHolderCursor in (IEnumerable<PlaceHolderCursor>) this.Items)
      {
        if (this.Comparer.Equals(this.GetKeyForItem(placeHolderCursor), key))
        {
          value = placeHolderCursor;
          return true;
        }
      }
      value = (PlaceHolderCursor) null;
      return false;
    }

    public void CreateNamspacePlaceHolder(int position) => this.Add(new PlaceHolderCursor("sf_Namespaces", position));

    public void RegisterNamespace(string namespc, string assembly, string tagPrefix, bool include)
    {
      if (this.namespaces.Contains(namespc + assembly + tagPrefix))
        return;
      this.namespaces.Add(new CursorCollection.Registration(namespc, assembly, tagPrefix, include));
    }

    public void RegisterUserControl(string src, string tagName, string tagPrefix, bool include)
    {
      if (this.namespaces.Contains(src))
        return;
      this.namespaces.Add(new CursorCollection.Registration(include, src, tagName, tagPrefix));
    }

    public string GetTagPrefix(string namespc, string assembly, string defaultTagPrefix)
    {
      CursorCollection.Registration registration = this.namespaces.FirstOrDefault<CursorCollection.Registration>((Func<CursorCollection.Registration, bool>) (r => r.Namespace != null && r.Namespace.Equals(namespc, StringComparison.OrdinalIgnoreCase) && r.Assembly != null && r.Assembly.Equals(assembly, StringComparison.OrdinalIgnoreCase)));
      if (registration != null)
        return registration.TagPrefix;
      this.RegisterNamespace(namespc, assembly, defaultTagPrefix, true);
      return defaultTagPrefix;
    }

    public string GetTagName(string src, string tagName, string tagPrefix)
    {
      if (this.namespaces.Contains(src))
      {
        CursorCollection.Registration registration = this.namespaces[src];
        return registration.TagPrefix + ":" + registration.TagName;
      }
      this.RegisterUserControl(src, tagName, tagPrefix, true);
      return tagPrefix + ":" + tagName;
    }

    public void WriteRegistrations()
    {
      StringBuilder output = this["sf_Namespaces"].Output;
      output.Clear();
      foreach (CursorCollection.Registration registration in (Collection<CursorCollection.Registration>) this.namespaces)
      {
        if (registration.Include)
        {
          if (string.IsNullOrEmpty(registration.Assembly))
            output.Append("<%@ Register TagPrefix=\"").Append(registration.TagPrefix).Append("\" TagName=\"").Append(registration.TagName).Append("\" Src=\"").Append(registration.Src).AppendLine("\" %>");
          else
            output.Append("<%@ Register TagPrefix=\"").Append(registration.TagPrefix).Append("\" Namespace=\"").Append(registration.Namespace).Append("\" Assembly=\"").Append(registration.Assembly).AppendLine("\" %>");
        }
      }
    }

    private class RegistrationCollection : KeyedCollection<string, CursorCollection.Registration>
    {
      public RegistrationCollection()
        : base((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
      {
      }

      protected override string GetKeyForItem(CursorCollection.Registration item) => item.Key;
    }

    private class Registration
    {
      public Registration(string namespc, string assembly, string tagPrefix, bool include)
      {
        this.Namespace = namespc;
        this.Assembly = assembly;
        this.TagPrefix = tagPrefix;
        this.Include = include;
        this.Key = namespc + assembly + tagPrefix;
      }

      public Registration(bool include, string src, string tagName, string tagPrefix)
      {
        this.Src = src;
        this.TagName = tagName;
        this.TagPrefix = tagPrefix;
        this.Include = include;
        this.Key = src;
      }

      public string Key { get; private set; }

      public string Namespace { get; private set; }

      public string Assembly { get; private set; }

      public string TagPrefix { get; private set; }

      public bool Include { get; private set; }

      public string TagName { get; private set; }

      public string Src { get; private set; }
    }
  }
}
