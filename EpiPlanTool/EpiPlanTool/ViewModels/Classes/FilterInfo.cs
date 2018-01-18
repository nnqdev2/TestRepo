using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Reflection;
using System.Text;
using DynamicExpresso;

namespace EpiPlanTool.ViewModels {
  public enum CompareFlags {
    CompareNone,
    CompareEquals,
    CompareAsEqualString,
    CompareAsContainsString,
    CompareAsExpression,
    CompareNotBlank
  }

  public class FilterInfo {
    private object _source;
    private object _value;
    private CompareFlags _compareFlag;
    private PropertyInfo _property;
    //private DataGridBoundColumn _boundColumn;
    private Interpreter interpreter;
    private string expr;

    public PropertyInfo Property { get { return _property; } }
    //public DataGridBoundColumn BoundColumn { get { return _boundColumn; } }
    public object Source { get { return _source; } }
    public CompareFlags CompareAs {
      get { return _compareFlag; }
      set {
        _compareFlag = value;
        if (value == CompareFlags.CompareAsExpression) {
          interpreter = new Interpreter(InterpreterOptions.CaseInsensitive);
        }
      }
    }
    public object Value {
      get { return _value; }
      set {
            _value = value;
            //todo -- will not work for reactor expression which 
            // is not being used now
            this.expr = "value " + _value;
          }
    }

    public bool Compare(object obj) {
      bool result = false;
      dynamic value;
      switch (CompareAs) {
        case CompareFlags.CompareNotBlank:
          value = GetItemValue(obj);
          if (IsList(value)) {
             var reactors = value as IList<string>; 
             result = reactors.Count() > 0;
          }
          else {
             result = value != null;
          }
          break;
        case CompareFlags.CompareAsContainsString:
          value = GetItemValue(obj);
          if (value == null) {
             result = false;
          } 
          else if (IsList(value)) {
             var reactors = value as IList<string>;
             var matchCnt = reactors.Where(s => s.ToString().Equals(Value));
             result = (matchCnt.Count()>0);
          }
          else {
             result = value.ToString().IndexOf(Value.ToString(), StringComparison.OrdinalIgnoreCase) > -1;
          }
          break;
        case CompareFlags.CompareAsExpression:
          value = GetItemValue(obj);
          if (IsList(value)) {
             var reactors = value as IList<string>;
             foreach (var reactor in reactors) {
                interpreter.SetVariable("value", reactor);
                result = (bool)interpreter.Eval(expr);
                if (result) break;
             }
          }
          else {
             interpreter.SetVariable("value", GetItemValue(obj));
             result = (bool)interpreter.Eval(expr);
          }
          break;
        case CompareFlags.CompareAsEqualString:
          value = GetItemValue(obj);
          if (IsList(value)) {
             var reactors = value as IList<string>;
             var filterValues = Value as IList<string>;
             int matchCnt = 0;
             foreach (var filterValue in filterValues) {
                var found = reactors.Where(s => s.ToString().Equals(filterValue));
                if (found.Count() > 0) matchCnt++;
             }
             result = (filterValues.Count() == matchCnt);
          }
          else {
             result = value.ToString().Equals(Value);
          }
          break;
        case CompareFlags.CompareEquals:
          value = GetItemValue(obj);
          if (value == null) {
             result = Value == null;
          }
          else if (IsList(value))   {
             var reactors = value as IList<string>;
             var filterValues = Value as IList<string>;
             int matchCnt = 0;
             foreach (var filterValue in filterValues) {
                var found = reactors.Where(s => s.ToString().Equals(filterValue));
                if (found.Count() > 0) matchCnt++;
             }
             result = (filterValues.Count() == matchCnt);
          } else {
              result = value.Equals(Value); 
          }
          break;
      }
      return result;
    }

    public FilterInfo(object obj, string propertyName) {
    //public FilterInfo(object obj, string propertyName, DataGridBoundColumn col) {
      this._source = obj;
      this._property = _source.GetType().GetProperty(propertyName);
      //this._boundColumn = col;
      this.interpreter = new Interpreter(InterpreterOptions.CaseInsensitive | InterpreterOptions.CommonTypes);
    }

    public object GetSourceValue() {
      return _property.GetValue(_source);
    }

    public object GetItemValue(object item) {
      return _property.GetValue(item);
    }

    public bool IsList(object o) {
       if (o == null) return false;
       return o is IList<string> ;
    }
  }
}
