// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.LocalResourceReader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.IO;
using System.Resources;
using System.Web;
using System.Web.Hosting;
using System.Xml;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// Provides the base functionality to read data from resource files.
  /// </summary>
  public class LocalResourceReader : IResourceReader, IEnumerable, IDisposable
  {
    private string fielExt;
    private string virtualPath;

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.LocalResourceReader" /> with the provided virtual path.
    /// </summary>
    /// <param name="virtualPath">The virtual path that will be used to retrieve resources.</param>
    /// <param name="resourceFileExtension">Specifies the file extension of the resource file.</param>
    public LocalResourceReader(string virtualPath, string resourceFileExtension)
    {
      this.virtualPath = virtualPath;
      this.fielExt = resourceFileExtension;
    }

    /// <summary>
    /// Releases all resources used by the <see cref="T:System.Resources.ResourceReader" />.
    /// </summary>
    public virtual void Close() => this.Dispose();

    /// <summary>
    /// Returns an enumerator for the current <see cref="T:System.Resources.ResourceReader" /> object.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the current  <see cref="T:System.Resources.ResourceReader" /> object.
    /// </returns>
    public virtual IDictionaryEnumerator GetEnumerator() => (IDictionaryEnumerator) new LocalResourceReader.ResourceEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    /// <summary>Defines a method to release allocated resources.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.Resources.ResourceReader" /> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">
    /// True to release both managed and unmanaged resources; false to release only unmanaged resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
      int num = disposing ? 1 : 0;
    }

    internal sealed class ResourceEnumerator : IDictionaryEnumerator, IEnumerator
    {
      private string key;
      private string value;
      private string culture;
      private XmlReader xmlReader;
      private LocalResourceReader resReader;
      private string[] resourceFiles;
      private int currentFile;

      public ResourceEnumerator(LocalResourceReader resReader)
      {
        this.currentFile = -1;
        this.resReader = resReader;
        if (resReader.virtualPath.EndsWith(resReader.fielExt, StringComparison.OrdinalIgnoreCase))
        {
          string path = HostingEnvironment.MapPath(resReader.virtualPath);
          if (File.Exists(path))
            this.resourceFiles = new string[1]{ path };
          else
            this.resourceFiles = new string[0];
        }
        else
        {
          string fileName = VirtualPathUtility.GetFileName(resReader.virtualPath);
          this.resourceFiles = Directory.GetFiles(HostingEnvironment.MapPath(VirtualPathUtility.GetDirectory(resReader.virtualPath) + "App_LocalResources/"), fileName + "*" + resReader.fielExt);
        }
      }

      public DictionaryEntry Entry => new DictionaryEntry((object) this.key, (object) this.value);

      public object Key => (object) this.key;

      public object Value => (object) this.value;

      public object Current => (object) this.Entry;

      public bool MoveNext()
      {
        if (this.currentFile == -1)
          this.SetNextFile();
        if (this.xmlReader == null)
          return false;
        this.key = (string) null;
        this.value = (string) null;
        while (this.xmlReader.Read())
        {
          if (this.xmlReader.NodeType == XmlNodeType.Element && this.xmlReader.Name == "data")
          {
            this.xmlReader.MoveToAttribute("name");
            this.key = this.xmlReader.Value + this.culture;
            while (this.xmlReader.Read())
            {
              if (this.xmlReader.NodeType == XmlNodeType.Element && this.xmlReader.Name == "value")
              {
                this.xmlReader.Read();
                this.value = this.xmlReader.Value;
              }
              if (this.xmlReader.NodeType == XmlNodeType.EndElement && this.xmlReader.Name == "data")
                break;
            }
            break;
          }
        }
        if (!this.xmlReader.EOF)
          return true;
        this.SetNextFile();
        return this.MoveNext();
      }

      public void Reset()
      {
        this.currentFile = -1;
        if (this.xmlReader == null)
          return;
        this.xmlReader.Close();
        this.xmlReader = (XmlReader) null;
      }

      private void SetNextFile()
      {
        if (this.resourceFiles.Length != 0 && this.resourceFiles.Length > this.currentFile + 1)
        {
          string resourceFile = this.resourceFiles[++this.currentFile];
          if (resourceFile.EndsWith(this.resReader.fielExt, StringComparison.OrdinalIgnoreCase))
          {
            string fileName = VirtualPathUtility.GetFileName(this.resReader.virtualPath);
            string withoutExtension = Path.GetFileNameWithoutExtension(resourceFile);
            this.culture = withoutExtension.Length <= fileName.Length ? string.Empty : withoutExtension.Substring(fileName.Length + 1);
            this.xmlReader = XmlReader.Create(resourceFile);
          }
          else
            this.SetNextFile();
        }
        else
        {
          this.key = (string) null;
          this.value = (string) null;
          this.culture = (string) null;
          if (this.xmlReader == null)
            return;
          this.xmlReader.Close();
          this.xmlReader = (XmlReader) null;
        }
      }

      object IEnumerator.Current => this.Current;

      bool IEnumerator.MoveNext() => this.MoveNext();

      void IEnumerator.Reset() => this.Reset();
    }
  }
}
