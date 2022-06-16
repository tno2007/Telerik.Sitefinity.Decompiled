// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Report.FormsModuleReporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.Services.Operations;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Forms.Report
{
  /// <summary>Forms module usage tracking reporter</summary>
  internal class FormsModuleReporter
  {
    private const string ControllerNamePropertyName = "ControllerName";
    private const string IdPropertyName = "ID";
    private const string HubSpotSyncedForm = "HubSpotFormName";
    private const string EloquaSyncedForm = "EloquaFormName";
    private const string SyncFormFieldsToLeadFields = "SyncFormFieldsToLeadFields";
    private const string ChangeMarketoLeadScore = "ChangeMarketoLeadScore";
    private const string DoSpecificWebCalls = "DoSpecificWebCalls";
    private ConcurrentDictionary<string, int> formResponsesCount = new ConcurrentDictionary<string, int>();

    /// <summary>Gets the report.</summary>
    /// <returns>Forms module report</returns>
    public FormsModuleReport GetReport()
    {
      List<FormsModuleReporter.FormDescriptionObject> descriptionObjects = this.GetFormDescriptionObjects();
      List<FormsModuleReporter.FormDescriptionObject> list = descriptionObjects.Where<FormsModuleReporter.FormDescriptionObject>((Func<FormsModuleReporter.FormDescriptionObject, bool>) (p => p.Rules != null)).ToList<FormsModuleReporter.FormDescriptionObject>();
      FormsModuleReport report = new FormsModuleReport();
      report.ModuleName = "Forms";
      report.MvcBasedFormsCount = descriptionObjects.Count<FormsModuleReporter.FormDescriptionObject>((Func<FormsModuleReporter.FormDescriptionObject, bool>) (p => p.Framework == FormFramework.Mvc));
      report.WebFormsBasedFormsCount = descriptionObjects.Count<FormsModuleReporter.FormDescriptionObject>((Func<FormsModuleReporter.FormDescriptionObject, bool>) (p => p.Framework == FormFramework.WebForms));
      report.FormFields = this.GetFormFieldsTracking(descriptionObjects);
      report.FormRules = new FormRulesTracking()
      {
        RuleBasedFormsCount = list.Count<FormsModuleReporter.FormDescriptionObject>(),
        TotalRulesCount = this.GetTotalRulesCount(list),
        Actions = this.GetRuleActionsTracking(list),
        ConditionFields = this.GetRuleConditionsFieldsTracking(list),
        RuleOperators = this.GetRuleOperatorsTracking(list)
      };
      report.ConnectorTracking = this.GenerateConnectorsTracking(descriptionObjects);
      this.formResponsesCount = new ConcurrentDictionary<string, int>();
      return report;
    }

    private IEnumerable<ConnectorTracking> GenerateConnectorsTracking(
      List<FormsModuleReporter.FormDescriptionObject> forms)
    {
      return (IEnumerable<ConnectorTracking>) new List<ConnectorTracking>()
      {
        this.GetTrackingReportFor(forms, "Marketo", (Func<string, string, bool>) ((connectorKey, isSetingEnabled) => (connectorKey == "SyncFormFieldsToLeadFields" || connectorKey == "DoSpecificWebCalls" || connectorKey == "ChangeMarketoLeadScore") && isSetingEnabled == "true")),
        this.GetTrackingReportFor(forms, "Eloqua", (Func<string, string, bool>) ((connectorKey, connectedFormName) => connectorKey == "EloquaFormName" && !string.IsNullOrEmpty(connectedFormName))),
        this.GetTrackingReportFor(forms, "HubSpot", (Func<string, string, bool>) ((connectorKey, connectedFormName) => connectorKey == "HubSpotFormName" && !string.IsNullOrEmpty(connectedFormName)))
      };
    }

    private ConnectorTracking GetTrackingReportFor(
      List<FormsModuleReporter.FormDescriptionObject> forms,
      string connectorName,
      Func<string, string, bool> enabledFormsCondition)
    {
      IEnumerable<string> source = forms.SelectMany((Func<FormsModuleReporter.FormDescriptionObject, IEnumerable<FormsModuleReporter.FormAttribute>>) (form => (IEnumerable<FormsModuleReporter.FormAttribute>) form.FormAttributes), (form, attribute) => new
      {
        form = form,
        attribute = attribute
      }).Where(_param1 => enabledFormsCondition(_param1.attribute.MapKey, _param1.attribute.Val)).Select(_param1 => _param1.form.FormName).Distinct<string>();
      FormsManager manager = FormsManager.GetManager();
      int num1 = 0;
      foreach (string str in source)
      {
        if (!this.formResponsesCount.ContainsKey(str))
        {
          int num2 = manager.GetFormEntries(new FormDescription(str)).Count<FormEntry>();
          num1 += num2;
          this.formResponsesCount.TryAdd(str, num2);
        }
        else
          num1 += this.formResponsesCount[str];
      }
      return new ConnectorTracking()
      {
        FormResponsesCount = num1,
        FormsCount = source.Count<string>(),
        ConnectorName = connectorName
      };
    }

    private List<FormsModuleReporter.FormDescriptionObject> GetFormDescriptionObjects()
    {
      FormsManager manager = FormsManager.GetManager();
      IQueryable<FormDescription> queryable1 = manager.GetForms().Where<FormDescription>((Expression<Func<FormDescription, bool>>) (f => (int) f.Status == 2));
      IQueryable<FormControl> queryable2 = manager.GetControls<FormControl>().Join<FormControl, FormDescription, Guid, FormControl>((IEnumerable<FormDescription>) queryable1, (Expression<Func<FormControl, Guid>>) (fc => fc.ContainerId), (Expression<Func<FormDescription, Guid>>) (fd => fd.Id), (Expression<Func<FormControl, FormDescription, FormControl>>) ((fc, fd) => fc));
      IQueryable<ControlProperty> source = manager.GetProperties().Where<ControlProperty>((Expression<Func<ControlProperty, bool>>) (p => p.Name == "ID" || p.Name == "ControllerName")).Join<ControlProperty, FormControl, Guid, ControlProperty>((IEnumerable<FormControl>) queryable2, (Expression<Func<ControlProperty, Guid>>) (cp => cp.Control.Id), (Expression<Func<FormControl, Guid>>) (fc => fc.Id), (Expression<Func<ControlProperty, FormControl, ControlProperty>>) ((cp, fc) => cp));
      ParameterExpression parameterExpression1;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      List<FormsModuleReporter.FormDescriptionObject> list = queryable1.Select<FormDescription, FormsModuleReporter.FormDescriptionObject>(Expression.Lambda<Func<FormDescription, FormsModuleReporter.FormDescriptionObject>>((Expression) Expression.MemberInit(Expression.New(typeof (FormsModuleReporter.FormDescriptionObject)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (FormsModuleReporter.FormDescriptionObject.set_Id)), )))); //unable to render the statement
      ParameterExpression parameterExpression2;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      List<FormsModuleReporter.FormControlObject> formControlsObjects = queryable2.Select<FormControl, FormsModuleReporter.FormControlObject>(Expression.Lambda<Func<FormControl, FormsModuleReporter.FormControlObject>>((Expression) Expression.MemberInit(Expression.New(typeof (FormsModuleReporter.FormControlObject)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (FormsModuleReporter.FormControlObject.set_Id)), )))); //unable to render the statement
      ParameterExpression parameterExpression3;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      List<FormsModuleReporter.ControlPropertyObject> controlProperties = source.Select<ControlProperty, FormsModuleReporter.ControlPropertyObject>(Expression.Lambda<Func<ControlProperty, FormsModuleReporter.ControlPropertyObject>>((Expression) Expression.MemberInit(Expression.New(typeof (FormsModuleReporter.ControlPropertyObject)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (FormsModuleReporter.ControlPropertyObject.set_ControlId)), )))); //unable to render the statement
      formControlsObjects.ForEach((Action<FormsModuleReporter.FormControlObject>) (c => c.Properties = (IEnumerable<FormsModuleReporter.ControlPropertyObject>) controlProperties.Where<FormsModuleReporter.ControlPropertyObject>((Func<FormsModuleReporter.ControlPropertyObject, bool>) (p => p.ControlId == c.Id)).ToList<FormsModuleReporter.ControlPropertyObject>()));
      list.ForEach((Action<FormsModuleReporter.FormDescriptionObject>) (f => f.Controls = (IEnumerable<FormsModuleReporter.FormControlObject>) formControlsObjects.Where<FormsModuleReporter.FormControlObject>((Func<FormsModuleReporter.FormControlObject, bool>) (c => c.FormId == f.Id)).ToList<FormsModuleReporter.FormControlObject>()));
      IList<FormsModuleReporter.FormAttribute> formAttributes = this.GetFormAttributes(manager);
      list.ForEach((Action<FormsModuleReporter.FormDescriptionObject>) (f => f.FormAttributes = (IList<FormsModuleReporter.FormAttribute>) formAttributes.Where<FormsModuleReporter.FormAttribute>((Func<FormsModuleReporter.FormAttribute, bool>) (c => c.Content_id == f.Id)).ToList<FormsModuleReporter.FormAttribute>()));
      return list;
    }

    private IList<FormsModuleReporter.FormAttribute> GetFormAttributes(
      FormsManager formsManager)
    {
      SitefinityOAContext transaction = formsManager.Provider.GetTransaction() as SitefinityOAContext;
      IList<FormsModuleReporter.FormAttribute> formAttributes = (IList<FormsModuleReporter.FormAttribute>) new List<FormsModuleReporter.FormAttribute>();
      if (transaction != null)
      {
        string sqlStatement = this.GetSqlStatement(transaction.OpenAccessConnection.DbType);
        try
        {
          formAttributes = transaction.ExecuteQuery<FormsModuleReporter.FormAttribute>(sqlStatement);
        }
        catch (Exception ex)
        {
          Log.Write((object) "Retrieving form attributes failed");
        }
      }
      return formAttributes;
    }

    private string GetSqlStatement(DatabaseType databaseType)
    {
      if (databaseType == DatabaseType.MySQL)
        return "select * from `sf_form_description_attrbutes` where `mapkey` in ('HubSpotFormName', 'EloquaFormName', 'SyncFormFieldsToLeadFields', 'ChangeMarketoLeadScore', 'DoSpecificWebCalls')";
      return databaseType == DatabaseType.Oracle ? "select * from \"sf_form_description_attrbutes\" where \"mapkey\" in ('HubSpotFormName', 'EloquaFormName', 'SyncFormFieldsToLeadFields', 'ChangeMarketoLeadScore', 'DoSpecificWebCalls')" : "select * from [sf_form_description_attrbutes] where [mapkey] in ('HubSpotFormName', 'EloquaFormName', 'SyncFormFieldsToLeadFields', 'ChangeMarketoLeadScore', 'DoSpecificWebCalls')";
    }

    private int GetTotalRulesCount(
      List<FormsModuleReporter.FormDescriptionObject> forms)
    {
      return forms.Select<FormsModuleReporter.FormDescriptionObject, int>((Func<FormsModuleReporter.FormDescriptionObject, int>) (form => form.Rules.Count<FormRule>())).Sum();
    }

    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Not needed.")]
    private IEnumerable<FieldTracking> GetFormFieldsTracking(
      List<FormsModuleReporter.FormDescriptionObject> forms)
    {
      return forms.Where<FormsModuleReporter.FormDescriptionObject>((Func<FormsModuleReporter.FormDescriptionObject, bool>) (form => form.Framework == FormFramework.Mvc)).SelectMany((Func<FormsModuleReporter.FormDescriptionObject, IEnumerable<FormsModuleReporter.FormControlObject>>) (form => form.Controls), (form, control) => new
      {
        form = form,
        control = control
      }).SelectMany(_param1 => _param1.control.Properties, (_param1, properties) => new
      {
        \u003C\u003Eh__TransparentIdentifier0 = _param1,
        properties = properties
      }).Where(_param1 => _param1.properties.Name == "ControllerName").GroupBy(_param1 => _param1.properties.Value, _param1 => _param1.properties).Select<IGrouping<string, FormsModuleReporter.ControlPropertyObject>, FieldTracking>((Func<IGrouping<string, FormsModuleReporter.ControlPropertyObject>, FieldTracking>) (fieldTypes => new FieldTracking()
      {
        FieldType = fieldTypes.Key,
        Count = fieldTypes.Count<FormsModuleReporter.ControlPropertyObject>()
      })).Concat<FieldTracking>(forms.Where<FormsModuleReporter.FormDescriptionObject>((Func<FormsModuleReporter.FormDescriptionObject, bool>) (form => form.Framework == FormFramework.WebForms)).SelectMany((Func<FormsModuleReporter.FormDescriptionObject, IEnumerable<FormsModuleReporter.FormControlObject>>) (form => form.Controls), (form, control) => new
      {
        form = form,
        control = control
      }).GroupBy(_param1 => _param1.control.ObjectType, _param1 => _param1.control).Select<IGrouping<string, FormsModuleReporter.FormControlObject>, FieldTracking>((Func<IGrouping<string, FormsModuleReporter.FormControlObject>, FieldTracking>) (fieldTypes => new FieldTracking()
      {
        FieldType = fieldTypes.Key,
        Count = fieldTypes.Count<FormsModuleReporter.FormControlObject>()
      })));
    }

    private IEnumerable<RuleActionTracking> GetRuleActionsTracking(
      List<FormsModuleReporter.FormDescriptionObject> forms)
    {
      return forms.SelectMany((Func<FormsModuleReporter.FormDescriptionObject, IEnumerable<FormRule>>) (form => (IEnumerable<FormRule>) form.Rules), (form, formRule) => new
      {
        form = form,
        formRule = formRule
      }).SelectMany(_param1 => (IEnumerable<RuleAction>) _param1.formRule.Actions, (_param1, action) => new
      {
        \u003C\u003Eh__TransparentIdentifier0 = _param1,
        action = action
      }).GroupBy(_param1 => _param1.action.Action, _param1 => _param1.action).Select<IGrouping<FormRuleAction, RuleAction>, RuleActionTracking>((Func<IGrouping<FormRuleAction, RuleAction>, RuleActionTracking>) (actionGroup => new RuleActionTracking()
      {
        ActionType = actionGroup.Key,
        Count = actionGroup.Count<RuleAction>()
      }));
    }

    private IEnumerable<RulesOperatorTracking> GetRuleOperatorsTracking(
      List<FormsModuleReporter.FormDescriptionObject> forms)
    {
      return forms.SelectMany((Func<FormsModuleReporter.FormDescriptionObject, IEnumerable<FormRule>>) (form => (IEnumerable<FormRule>) form.Rules), (form, formRule) => new
      {
        form = form,
        formRule = formRule
      }).GroupBy(_param1 => _param1.formRule.Operator, _param1 => _param1.formRule).Select<IGrouping<LogicalOperator, FormRule>, RulesOperatorTracking>((Func<IGrouping<LogicalOperator, FormRule>, RulesOperatorTracking>) (formRuleOperators => new RulesOperatorTracking()
      {
        OperatorType = formRuleOperators.Key,
        Count = formRuleOperators.Count<FormRule>()
      }));
    }

    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Not needed.")]
    private IEnumerable<FieldTracking> GetRuleConditionsFieldsTracking(
      List<FormsModuleReporter.FormDescriptionObject> forms)
    {
      IEnumerable<\u003C\u003Ef__AnonymousType5<Guid, string, string>> inner = forms.SelectMany((Func<FormsModuleReporter.FormDescriptionObject, IEnumerable<FormsModuleReporter.FormControlObject>>) (form => form.Controls), (form, control) => new
      {
        form = form,
        control = control
      }).Where(_param1 => _param1.control.Properties.Any<FormsModuleReporter.ControlPropertyObject>((Func<FormsModuleReporter.ControlPropertyObject, bool>) (p => p.Name == "ID" && !string.IsNullOrWhiteSpace(p.Value))) && _param1.control.Properties.Any<FormsModuleReporter.ControlPropertyObject>((Func<FormsModuleReporter.ControlPropertyObject, bool>) (p => p.Name == "ControllerName" && !string.IsNullOrWhiteSpace(p.Value)))).Select(_param1 => new
      {
        FormId = _param1.form.Id,
        Id = _param1.control.Properties.First<FormsModuleReporter.ControlPropertyObject>((Func<FormsModuleReporter.ControlPropertyObject, bool>) (p => p.Name == "ID")).Value,
        FieldType = _param1.control.Properties.First<FormsModuleReporter.ControlPropertyObject>((Func<FormsModuleReporter.ControlPropertyObject, bool>) (p => p.Name == "ControllerName")).Value
      });
      return forms.SelectMany((Func<FormsModuleReporter.FormDescriptionObject, IEnumerable<FormRule>>) (form => (IEnumerable<FormRule>) form.Rules), (form, formRule) => new
      {
        form = form,
        formRule = formRule
      }).SelectMany(_param1 => (IEnumerable<RuleCondition>) _param1.formRule.Conditions, (_param1, condition) => new
      {
        \u003C\u003Eh__TransparentIdentifier0 = _param1,
        condition = condition
      }).GroupJoin(inner, _param1 => _param1.condition.Id, formFieldType => formFieldType.Id, (_param1, conditionFields) => new
      {
        \u003C\u003Eh__TransparentIdentifier1 = _param1,
        conditionFields = conditionFields
      }).SelectMany(_param1 => _param1.conditionFields, (_param1, conditionField) => new
      {
        \u003C\u003Eh__TransparentIdentifier2 = _param1,
        conditionField = conditionField
      }).Where(_param1 => _param1.conditionField.FormId == _param1.\u003C\u003Eh__TransparentIdentifier2.\u003C\u003Eh__TransparentIdentifier1.\u003C\u003Eh__TransparentIdentifier0.form.Id).GroupBy(_param1 => _param1.conditionField.FieldType, _param1 => _param1.conditionField).Select<IGrouping<string, \u003C\u003Ef__AnonymousType5<Guid, string, string>>, FieldTracking>(conditionFieldsGroup => new FieldTracking()
      {
        FieldType = conditionFieldsGroup.Key,
        Count = conditionFieldsGroup.Count()
      });
    }

    internal class FormDescriptionObject
    {
      private List<FormRule> rules;

      public Guid Id { get; set; }

      public FormFramework Framework { get; set; }

      public IEnumerable<FormsModuleReporter.FormControlObject> Controls { get; set; }

      public string RawRules { get; set; }

      public string FormName { get; set; }

      public List<FormRule> Rules
      {
        get
        {
          if (this.rules == null && !string.IsNullOrWhiteSpace(this.RawRules))
            this.rules = JsonConvert.DeserializeObject<List<FormRule>>(this.RawRules);
          return this.rules;
        }
      }

      public IList<FormsModuleReporter.FormAttribute> FormAttributes { get; set; }
    }

    internal class FormControlObject
    {
      public Guid Id { get; set; }

      public Guid FormId { get; set; }

      public IEnumerable<FormsModuleReporter.ControlPropertyObject> Properties { get; set; }

      public string ObjectType { get; set; }
    }

    internal class ControlPropertyObject
    {
      public Guid ControlId { get; set; }

      public string Name { get; set; }

      public string Value { get; set; }
    }

    internal class FormAttribute
    {
      public Guid Content_id { get; set; }

      public string MapKey { get; set; }

      public string Val { get; set; }
    }
  }
}
