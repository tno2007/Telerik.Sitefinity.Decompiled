// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PassThroughPublishingPoint
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>Pass through publishing point</summary>
  public class PassThroughPublishingPoint : 
    PublishingPointBase,
    IQueryablePublishingPoint,
    IQueryablePullable
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.PassThroughPublishingPoint" /> class.
    /// </summary>
    public PassThroughPublishingPoint()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.PassThroughPublishingPoint" /> class.
    /// </summary>
    /// <param name="model">The model.</param>
    public PassThroughPublishingPoint(PublishingPoint model)
      : base(model)
    {
    }

    /// <summary>Gets the publishing point items.</summary>
    /// <returns></returns>
    public override IQueryable<WrapperObject> GetPublishingPointItems() => this.GetItems(false, (string) null, (string) null, 0, 0);

    IQueryable<WrapperObject> IQueryablePullable.GetItems(
      string filter,
      string order,
      int skip,
      int take)
    {
      return this.GetItems(true, filter, order, skip, take);
    }

    private IQueryable<WrapperObject> GetItems(
      bool queryablePullPipesEnabled,
      string filter,
      string order,
      int skip,
      int take)
    {
      IQueryable<WrapperObject> left = (IQueryable<WrapperObject>) null;
      foreach (IInboundPipe inboundPipe in this.InboundPipes)
      {
        if (inboundPipe is IPullPipe pullPipe)
        {
          IQueryablePullable queryablePullable = pullPipe as IQueryablePullable;
          IQueryable<WrapperObject> right;
          if (queryablePullPipesEnabled && queryablePullable != null)
          {
            right = queryablePullable.GetItems(filter, order, skip, take);
          }
          else
          {
            IPipe pipe = inboundPipe as IPipe;
            int num = -1;
            if (pipe != null)
            {
              num = pipe.PipeSettings.MaxItems;
              ((IPipe) inboundPipe).PipeSettings.MaxItems = Math.Max(take, pipe.PipeSettings.MaxItems);
            }
            try
            {
              right = pullPipe.GetData();
            }
            finally
            {
              if (pipe != null && num > -1)
                ((IPipe) inboundPipe).PipeSettings.MaxItems = num;
            }
          }
          left = left.UnionOrRight<WrapperObject>(right);
        }
      }
      return left;
    }
  }
}
