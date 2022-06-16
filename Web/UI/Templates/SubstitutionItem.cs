// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.SubstitutionItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  internal class SubstitutionItem
  {
    private int index;
    private string name;
    private string classId;
    private string format;
    private DataType type;
    private SubstitutionItem.Member[] members;
    private string args;
    private bool isDataResolver;

    internal SubstitutionItem(
      int index,
      string name,
      DataType type,
      string format,
      string args,
      bool isDataResolver)
    {
      this.index = index;
      this.name = name;
      this.type = type;
      this.format = format;
      this.args = args;
      this.isDataResolver = isDataResolver;
      if (this.type == DataType.Resource)
      {
        string[] strArray = name.Split(new char[1]{ ',' }, StringSplitOptions.RemoveEmptyEntries);
        this.classId = strArray.Length == 2 ? strArray[0].Trim() : throw new TemplateException(Res.Get<ErrorMessages>().LocalResourcesNotSupported);
        this.name = strArray[1].Trim();
      }
      else
      {
        if (this.type != DataType.Control)
          return;
        int memIdx = 0;
        int parIdx = 0;
        bool flag = false;
        bool isMethod = false;
        int num = 0;
        char[] memBuff = new char[256];
        char[] parBuff = new char[256];
        char[] charArray = this.name.ToCharArray();
        List<SubstitutionItem.Member> memberList = new List<SubstitutionItem.Member>();
        for (int index1 = 0; index1 < charArray.Length; ++index1)
        {
          char ch = charArray[index1];
          switch (ch)
          {
            case '(':
              if (index1 == 0 || num > 0)
              {
                ++num;
                break;
              }
              isMethod = true;
              flag = true;
              break;
            case ')':
              if (num > 0)
              {
                --num;
                break;
              }
              flag = false;
              break;
            case '.':
              if (num < 2)
              {
                if (flag)
                {
                  parBuff[parIdx++] = ch;
                  break;
                }
                memberList.Add(SubstitutionItem.GetMember(memBuff, ref memIdx, parBuff, ref parIdx, ref isMethod));
                break;
              }
              break;
            default:
              if (num < 2)
              {
                if (flag)
                {
                  parBuff[parIdx++] = ch;
                  break;
                }
                memBuff[memIdx++] = ch;
                break;
              }
              break;
          }
        }
        memberList.Add(SubstitutionItem.GetMember(memBuff, ref memIdx, parBuff, ref parIdx, ref isMethod));
        this.members = memberList.ToArray();
      }
    }

    private static SubstitutionItem.Member GetMember(
      char[] memBuff,
      ref int memIdx,
      char[] parBuff,
      ref int parIdx,
      ref bool isMethod)
    {
      SubstitutionItem.Member member = new SubstitutionItem.Member();
      member.Name = new string(memBuff, 0, memIdx);
      member.IsProperty = !isMethod;
      if (parIdx > 0)
        member.SetParameters(new string(parBuff, 0, parIdx));
      isMethod = false;
      memIdx = 0;
      parIdx = 0;
      return member;
    }

    internal int Index => this.index;

    internal string Name => this.name;

    internal string ClassId => this.classId;

    internal string Format => this.format;

    internal string Args => this.args;

    internal bool IsDataResolver => this.isDataResolver;

    internal DataType Type => this.type;

    internal object GetValue(object component)
    {
      object component1 = component;
      for (int index = 0; index < this.members.Length; ++index)
        component1 = this.members[index].GetValue(component1);
      return component1;
    }

    private class Member
    {
      public string Name;
      public bool IsProperty;
      public object[] Parameters;
      private bool isControl;
      private PropertyDescriptor propDesc;
      private MethodInfo methodInfo;

      public void SetParameters(string pars)
      {
        if (string.IsNullOrEmpty(pars))
          return;
        string[] strArray = pars.Trim().Split(',');
        this.Parameters = new object[strArray.Length];
        for (int index = 0; index < strArray.Length; ++index)
          this.Parameters[index] = (object) strArray[index].Trim('"', ' ');
      }

      public object GetValue(object component)
      {
        if (this.IsProperty)
        {
          if (this.isControl)
          {
            Control control = ((Control) component).FindControl(this.Name);
            if (control != null)
              return (object) control;
            this.Throw();
            return (object) null;
          }
          if (this.propDesc == null)
          {
            this.propDesc = TypeDescriptor.GetProperties(component).Find(this.Name, true);
            if (this.propDesc == null)
            {
              if (component is Control)
              {
                this.isControl = true;
                return this.GetValue(component);
              }
              this.Throw();
            }
          }
          return this.propDesc.GetValue(component);
        }
        if (this.methodInfo == (MethodInfo) null)
        {
          this.methodInfo = component.GetType().GetMethod(this.Name);
          if (this.methodInfo == (MethodInfo) null)
            this.Throw();
        }
        return this.methodInfo.Invoke(component, this.Parameters);
      }

      private void Throw() => throw new ArgumentException(string.Format("Invalid member name \"{0}\".", (object) this.Name));
    }
  }
}
