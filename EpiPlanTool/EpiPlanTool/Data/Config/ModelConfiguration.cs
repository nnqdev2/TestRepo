using System.Data.Entity;
using Oracle.ManagedDataAccess.EntityFramework;

namespace EpiPlanTool.Context {

  public class ModelConfiguration : DbConfiguration {
    public ModelConfiguration() {
      SetProviderServices("Oracle.ManagedDataAccess.Client", EFOracleProviderServices.Instance);
    }
  }

}