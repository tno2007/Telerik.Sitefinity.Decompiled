// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Dynamic.ClassFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace Telerik.Sitefinity.Data.Linq.Dynamic
{
  internal class ClassFactory
  {
    public static readonly ClassFactory Instance = new ClassFactory();
    private ModuleBuilder module;
    private Dictionary<Signature, Type> classes;
    private int classCount;
    private ReaderWriterLock rwLock;

    private ClassFactory()
    {
      this.module = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("DynamicClasses"), AssemblyBuilderAccess.Run).DefineDynamicModule("Module");
      this.classes = new Dictionary<Signature, Type>();
      this.rwLock = new ReaderWriterLock();
    }

    public Type GetDynamicClass(IEnumerable<DynamicProperty> properties)
    {
      this.rwLock.AcquireReaderLock(-1);
      try
      {
        Signature key = new Signature(properties);
        Type dynamicClass;
        if (!this.classes.TryGetValue(key, out dynamicClass))
        {
          dynamicClass = this.CreateDynamicClass(key.properties);
          this.classes.Add(key, dynamicClass);
        }
        return dynamicClass;
      }
      finally
      {
        this.rwLock.ReleaseReaderLock();
      }
    }

    private Type CreateDynamicClass(DynamicProperty[] properties)
    {
      LockCookie writerLock = this.rwLock.UpgradeToWriterLock(-1);
      try
      {
        TypeBuilder tb = this.module.DefineType("DynamicClass" + (object) (this.classCount + 1), TypeAttributes.Public, typeof (DynamicClass));
        FieldInfo[] properties1 = this.GenerateProperties(tb, properties);
        this.GenerateEquals(tb, properties1);
        this.GenerateGetHashCode(tb, properties1);
        Type type = tb.CreateType();
        ++this.classCount;
        return type;
      }
      finally
      {
        this.rwLock.DowngradeFromWriterLock(ref writerLock);
      }
    }

    private FieldInfo[] GenerateProperties(TypeBuilder tb, DynamicProperty[] properties)
    {
      FieldInfo[] properties1 = (FieldInfo[]) new FieldBuilder[properties.Length];
      for (int index = 0; index < properties.Length; ++index)
      {
        DynamicProperty property = properties[index];
        FieldBuilder field = tb.DefineField("_" + property.Name, property.Type, FieldAttributes.Private);
        PropertyBuilder propertyBuilder = tb.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.Type, (Type[]) null);
        MethodBuilder mdBuilder1 = tb.DefineMethod("get_" + property.Name, MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName, property.Type, Type.EmptyTypes);
        ILGenerator ilGenerator1 = mdBuilder1.GetILGenerator();
        ilGenerator1.Emit(OpCodes.Ldarg_0);
        ilGenerator1.Emit(OpCodes.Ldfld, (FieldInfo) field);
        ilGenerator1.Emit(OpCodes.Ret);
        MethodBuilder mdBuilder2 = tb.DefineMethod("set_" + property.Name, MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName, (Type) null, new Type[1]
        {
          property.Type
        });
        ILGenerator ilGenerator2 = mdBuilder2.GetILGenerator();
        ilGenerator2.Emit(OpCodes.Ldarg_0);
        ilGenerator2.Emit(OpCodes.Ldarg_1);
        ilGenerator2.Emit(OpCodes.Stfld, (FieldInfo) field);
        ilGenerator2.Emit(OpCodes.Ret);
        propertyBuilder.SetGetMethod(mdBuilder1);
        propertyBuilder.SetSetMethod(mdBuilder2);
        properties1[index] = (FieldInfo) field;
      }
      return properties1;
    }

    private void GenerateEquals(TypeBuilder tb, FieldInfo[] fields)
    {
      ILGenerator ilGenerator = tb.DefineMethod("Equals", MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof (bool), new Type[1]
      {
        typeof (object)
      }).GetILGenerator();
      LocalBuilder local = ilGenerator.DeclareLocal((Type) tb);
      Label label1 = ilGenerator.DefineLabel();
      ilGenerator.Emit(OpCodes.Ldarg_1);
      ilGenerator.Emit(OpCodes.Isinst, (Type) tb);
      ilGenerator.Emit(OpCodes.Stloc, local);
      ilGenerator.Emit(OpCodes.Ldloc, local);
      ilGenerator.Emit(OpCodes.Brtrue_S, label1);
      ilGenerator.Emit(OpCodes.Ldc_I4_0);
      ilGenerator.Emit(OpCodes.Ret);
      ilGenerator.MarkLabel(label1);
      foreach (FieldInfo field in fields)
      {
        Type fieldType = field.FieldType;
        Type type = typeof (EqualityComparer<>).MakeGenericType(fieldType);
        Label label2 = ilGenerator.DefineLabel();
        ilGenerator.EmitCall(OpCodes.Call, type.GetMethod("get_Default"), (Type[]) null);
        ilGenerator.Emit(OpCodes.Ldarg_0);
        ilGenerator.Emit(OpCodes.Ldfld, field);
        ilGenerator.Emit(OpCodes.Ldloc, local);
        ilGenerator.Emit(OpCodes.Ldfld, field);
        ilGenerator.EmitCall(OpCodes.Callvirt, type.GetMethod("Equals", new Type[2]
        {
          fieldType,
          fieldType
        }), (Type[]) null);
        ilGenerator.Emit(OpCodes.Brtrue_S, label2);
        ilGenerator.Emit(OpCodes.Ldc_I4_0);
        ilGenerator.Emit(OpCodes.Ret);
        ilGenerator.MarkLabel(label2);
      }
      ilGenerator.Emit(OpCodes.Ldc_I4_1);
      ilGenerator.Emit(OpCodes.Ret);
    }

    private void GenerateGetHashCode(TypeBuilder tb, FieldInfo[] fields)
    {
      ILGenerator ilGenerator = tb.DefineMethod("GetHashCode", MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof (int), Type.EmptyTypes).GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldc_I4_0);
      foreach (FieldInfo field in fields)
      {
        Type fieldType = field.FieldType;
        Type type = typeof (EqualityComparer<>).MakeGenericType(fieldType);
        ilGenerator.EmitCall(OpCodes.Call, type.GetMethod("get_Default"), (Type[]) null);
        ilGenerator.Emit(OpCodes.Ldarg_0);
        ilGenerator.Emit(OpCodes.Ldfld, field);
        ilGenerator.EmitCall(OpCodes.Callvirt, type.GetMethod("GetHashCode", new Type[1]
        {
          fieldType
        }), (Type[]) null);
        ilGenerator.Emit(OpCodes.Xor);
      }
      ilGenerator.Emit(OpCodes.Ret);
    }
  }
}
