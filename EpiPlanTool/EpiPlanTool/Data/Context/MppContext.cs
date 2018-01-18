using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using NLog;
using EpiPlanTool.Models;

namespace EpiPlanTool.Context {

  public class MppContext : DbContext {
    public MppContext() : base() {
      Database.SetInitializer<MppContext>(null);
      this.Configuration.LazyLoadingEnabled = false;
      this.Configuration.ProxyCreationEnabled = false;
      //DbInterception.Add(new NLogCommandInterceptor());
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      modelBuilder.HasDefaultSchema("MPP_OWNER");
    }

    public virtual DbSet<ReactorStatus> ReactorStatuses { get; set; }

    public List<ReactorStatus> GetReactorStatuses(){
      string sql = EpiPlanTool.Properties.Resources.GET_REACTOR_STATUSES;
      return ReactorStatuses.SqlQuery(sql).ToList<ReactorStatus>();
    }

  }

}
