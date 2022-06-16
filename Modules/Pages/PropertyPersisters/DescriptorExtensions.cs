// Decompiled with JetBrains decompiler
// Type: System.ComponentModel.DescriptorExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Security;

namespace System.ComponentModel
{
  /// <summary>Extension methods for type descriptors</summary>
  public static class DescriptorExtensions
  {
    /// <summary>
    /// Gets the type converter for a type.
    /// This method returns an instance of NullConverter if retrieving of the actual converter fails.
    /// </summary>
    /// <param name="descriptor">The descriptor.</param>
    /// <returns></returns>
    public static TypeConverter GetConverter(this PropertyDescriptor descriptor)
    {
      try
      {
        return descriptor.Converter;
      }
      catch (SecurityException ex)
      {
        return (TypeConverter) new DescriptorExtensions.NullConverter();
      }
      catch (MemberAccessException ex)
      {
        return (TypeConverter) new DescriptorExtensions.NullConverter();
      }
    }

    private class NullConverter : TypeConverter
    {
    }
  }
}
