using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using EpiPlanTool.Models;

namespace EpiPlanTool.Context {
    
  public class PlanContext : DbContext {
    public PlanContext() : base() {
      Database.SetInitializer<PlanContext>(null);
      this.Configuration.LazyLoadingEnabled = false;
      this.Configuration.ProxyCreationEnabled = false;
      //DbInterception.Add(new NLogCommandInterceptor());
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      modelBuilder.HasDefaultSchema("F2REP_OWNER");
      modelBuilder.Conventions.Add<OracleCaseConvention>();
      base.OnModelCreating(modelBuilder);
    }
    
    public virtual DbSet<Reactor> Reactors { get; set; }
    public virtual DbSet<ReactorSchedule> ReactorSchedules { get; set; }
    public virtual DbSet<EpiSchedule> EpiSchedules { get; set; }
    public virtual DbSet<Task> Tasks { get; set; }
    public virtual DbSet<AppUserRole> AppUserRoles { get; set; }
    public virtual DbSet<BookedOrder> BookedOrders { get; set; }
//    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    public virtual DbSet<AllocLot> AllocLots { get; set; }
    public virtual DbSet<Workcell> Workcells { get; set; }
  }
}
