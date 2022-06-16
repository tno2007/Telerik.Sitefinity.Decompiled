// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Cryptography.HmacBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Security.Cryptography;

namespace Telerik.Sitefinity.Security.Cryptography
{
  internal abstract class HmacBase : KeyedHashAlgorithm
  {
    private int blockSizeValue = 64;
    internal HashAlgorithm m_hash1;
    internal HashAlgorithm m_hash2;
    private bool m_hashing;
    internal string m_hashName;
    private byte[] m_inner;
    private byte[] m_outer;

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.m_hash1 != null)
          this.m_hash1.Dispose();
        if (this.m_hash2 != null)
          this.m_hash2.Dispose();
        if (this.m_inner != null)
          Array.Clear((Array) this.m_inner, 0, this.m_inner.Length);
        if (this.m_outer != null)
          Array.Clear((Array) this.m_outer, 0, this.m_outer.Length);
      }
      base.Dispose(disposing);
    }

    protected override void HashCore(byte[] rgb, int ib, int cb)
    {
      if (!this.m_hashing)
      {
        this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
        this.m_hashing = true;
      }
      this.m_hash1.TransformBlock(rgb, ib, cb, rgb, ib);
    }

    protected override byte[] HashFinal()
    {
      if (!this.m_hashing)
      {
        this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
        this.m_hashing = true;
      }
      this.m_hash1.TransformFinalBlock(new byte[0], 0, 0);
      byte[] hash = this.m_hash1.Hash;
      this.m_hash2.TransformBlock(this.m_outer, 0, this.m_outer.Length, this.m_outer, 0);
      this.m_hash2.TransformBlock(hash, 0, hash.Length, hash, 0);
      this.m_hashing = false;
      this.m_hash2.TransformFinalBlock(new byte[0], 0, 0);
      return this.m_hash2.Hash;
    }

    public override void Initialize()
    {
      this.m_hash1.Initialize();
      this.m_hash2.Initialize();
      this.m_hashing = false;
    }

    internal void InitializeKey(byte[] key)
    {
      this.m_inner = (byte[]) null;
      this.m_outer = (byte[]) null;
      if (key.Length > this.BlockSizeValue)
        this.KeyValue = this.m_hash1.ComputeHash(key);
      else
        this.KeyValue = (byte[]) key.Clone();
      this.UpdateIOPadBuffers();
    }

    private void UpdateIOPadBuffers()
    {
      if (this.m_inner == null)
        this.m_inner = new byte[this.BlockSizeValue];
      if (this.m_outer == null)
        this.m_outer = new byte[this.BlockSizeValue];
      for (int index = 0; index < this.BlockSizeValue; ++index)
      {
        this.m_inner[index] = (byte) 54;
        this.m_outer[index] = (byte) 92;
      }
      for (int index = 0; index < this.KeyValue.Length; ++index)
      {
        this.m_inner[index] = (byte) ((uint) this.m_inner[index] ^ (uint) this.KeyValue[index]);
        this.m_outer[index] = (byte) ((uint) this.m_outer[index] ^ (uint) this.KeyValue[index]);
      }
    }

    protected int BlockSizeValue
    {
      get => this.blockSizeValue;
      set => this.blockSizeValue = value;
    }

    public string HashName => this.m_hashName;

    public override byte[] Key => (byte[]) this.KeyValue.Clone();
  }
}
