using System;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace EpiPlanTool.Context {

  public class OracleCaseConvention : Convention {
    public OracleCaseConvention() {
      Properties()
        .Configure(config => config.HasColumnName(GetColumnName(config.ClrPropertyInfo)));
    }

    private static string GetColumnName(PropertyInfo property) {
      string result = property.Name;
      if (property.Name.ToUpper() != property.Name) {
        result = Regex.Replace(
          property.Name,
          ".[A-Z]",
          (m) => {
            return m.Value[0] + "_" + m.Value[1];
          }
        ).ToUpper();
      }
      return result;
    }

  }

}