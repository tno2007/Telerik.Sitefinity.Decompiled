// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Package.ScanOperation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Packaging.Package
{
  /// <summary>This class defines a scan operation definition</summary>
  internal class ScanOperation
  {
    private ScanType typeOfScan;
    private AddOnInfo addOnInfo;

    /// <summary>
    ///  Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Packaging.Package.ScanOperation" /> class.
    /// </summary>
    public ScanOperation() => this.typeOfScan = ScanType.Import;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Packaging.Package.ScanOperation" /> class.
    /// </summary>
    /// <param name="addOnInfo">AddOn info.</param>
    public ScanOperation(AddOnInfo addOnInfo)
    {
      this.addOnInfo = addOnInfo;
      this.typeOfScan = ScanType.Count;
    }

    /// <summary>Gets the type of the scan</summary>
    public ScanType TypeOfScan => this.typeOfScan;

    /// <summary>
    /// Gets add on information if the type of the scan is count
    /// </summary>
    internal AddOnInfo AddOnInfo => this.addOnInfo;
  }
}
