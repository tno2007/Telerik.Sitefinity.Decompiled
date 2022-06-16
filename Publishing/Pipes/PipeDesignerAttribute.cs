// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.PipeDesignerAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>
  /// This attribute is used to instruct publishing point editopr that the designer has been implemented for the Pipe
  /// and to specify the type of the control that serves as the actual control designer.
  /// </summary>
  /// <remarks>
  /// Pipe designers are controls which provide simple and straightforward user interface for setting the properties
  /// of a the pipe settings
  /// </remarks>
  public class PipeDesignerAttribute : Attribute
  {
    private string inboundDesignerTypeName;
    private string outboundDesignerTypeName;
    private Type inboundDesignerType;
    private Type outboundDesignerType;

    public PipeDesignerAttribute()
    {
    }

    public PipeDesignerAttribute(string inboundTypeName, string outboundTypeName)
    {
      this.InboundDesignerTypeName = inboundTypeName;
      this.OutboundDesignerTypeName = outboundTypeName;
    }

    public PipeDesignerAttribute(Type inboundType, Type outboundType)
    {
      this.InboundDesignerType = inboundType;
      this.OutboundDesignerType = outboundType;
    }

    /// <summary>
    /// Gets or sets the name/user control path of the inbound designer type.
    /// </summary>
    /// <value>The name of the inbound designer type.</value>
    public string InboundDesignerTypeName
    {
      get => this.inboundDesignerTypeName;
      set => this.inboundDesignerTypeName = value;
    }

    /// <summary>
    /// Gets or sets the name/user control path of the outbound designer type.
    /// </summary>
    /// <value>The name of the outbound designer type.</value>
    public string OutboundDesignerTypeName
    {
      get => this.outboundDesignerTypeName;
      set => this.outboundDesignerTypeName = value;
    }

    /// <summary>
    /// Gets or sets the control type of the inbound designer.
    /// </summary>
    /// <value>The type of the inbound designer.</value>
    public Type InboundDesignerType
    {
      get => this.inboundDesignerType;
      set => this.inboundDesignerType = value;
    }

    /// <summary>
    /// Gets or sets the control type of the outbound designer.
    /// </summary>
    /// <value>The type of the outbound designer.</value>
    public Type OutboundDesignerType
    {
      get => this.outboundDesignerType;
      set => this.outboundDesignerType = value;
    }
  }
}
