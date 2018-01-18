using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PropertyChanged;
using System.Text.RegularExpressions;

namespace EpiPlanTool.ViewModels {

  using EpiPlanTool.ViewModels.Collections;
  using EpiPlanTool.Collections;
  using EpiPlanTool.Services;
  using EpiPlanTool.Models;

  using WipCollection = List<Inventory>;

  public class Inventory {
    public string Name { get; set; }
    public int Index { get; set; }
    public int Qty {get; set;}
  }

  [ImplementPropertyChanged]
  public class BookedOrderViewModel : IEditableObject {

    #region Private Fields
    private IAppRepository _repo;
    private TaskCollection _tasks;
    private WipCollection _wip;
    #endregion

    #region Constructors
    public BookedOrderViewModel() {
      _tasks = new TaskCollection();
      _tasks.CollectionChanged += (s, e) => {
        HasTasks = _tasks.Count > 0;  
      };
      Lots = new List<AllocLot>(40);
    }

    public BookedOrderViewModel(IAppRepository repo) : this() {
      _repo = repo;
    }
    #endregion

    #region Internal Methods
    internal void AttachTask(TaskViewModel task) {
      if(!_tasks.Contains(task)) {
        _tasks.Add(task);
        var sorted = _tasks.OrderBy(t => t.Reactor.ReactorNumber).ToList();
        _tasks.ReplaceWithRange( sorted );
        UpdateProcessTimes();
      }
    }

    internal void DetachTask(TaskViewModel task) {
      _tasks.Remove(task);
      UpdateProcessTimes();
    }
    #endregion

    #region Public Properties
    public long BookedOrderID { get; set; }
    public string OrderID { get; set; }
    public string OrdNum { get; set; }
    public string OrdItem { get; set; }
    public string OrdType { get; set; }
    public string MAPL { get; set; }
    public int BookQty { get; set; }
    public DateTime BookDate { get; set; }
    public DateTime? MasterSchedDate { get; set; }
    public string CW { get; set; }
    public int AllocQty { get; set; }
    public int RemToProd { get; set; }
    public int? ReqForProd { get; set; }
    public int? SHORT { get; set; }
    public int? TRANS { get; set; }
    public int? BIN { get; set; }
    public int? LC { get; set; }
    public int? MWS { get; set; }
    public int? WC02 { get; set; }
    public int? WC03 { get; set; }
    public int? WC04 { get; set; }
    public int? HTQ { get; set; }
    public int? WC05 { get; set; }
    public int? WC06 { get; set; }
    public int? WC07 { get; set; }
    public int? WC08 { get; set; }
    public int? WC09 { get; set; }
    public int? PostQty { get; set; }
    public int? MG { get; set; }
    public int? FG { get; set; }
    public int? CP { get; set; }
    public int? RW { get; set; }
    public int? PP { get; set; }
    public int? ES { get; set; }
    public decimal? AsmWPD { get; set; }
    public string AsmRecipe1 { get; set; }
    public decimal? CenWPD { get; set; }
    public string CenRecipe1 { get; set; }
    public string CustShortName { get; set; }
    public string CustSpecNickName { get; set; }
    public string Dopant { get; set; }
    public string Dopant1 { get; set; }
    public decimal? ResTgt1 { get; set; }
    public decimal? ThickTgt1 { get; set; }
    public string ReactQual { get; set; }
    public string ReactType { get; set; }
    public string TypeSource { get; set; }
    public int? Layers { get; set; }
    public string CV1 { get; set; }
    public string CV2 { get; set; }
    public decimal? CV_FREQ { get; set; }
    public bool DualQual { get; set; }
    public string Product { get; set; }
    public bool IsInEditMode { get; private set; }
    [DependsOn("SHORT")]
    public bool IsShort { get { return SHORT.HasValue && SHORT.Value > 0; } }
    public List<AllocLot> Lots { get; private set; }
    public IAppRepository Repository { get { return _repo; } }
    public TaskCollection Tasks { get { return _tasks; } }
    public bool HasTasks { get; private set; }
    public decimal? Wpd { get { return (ReactType.Equals("ASM") ? (AsmWPD==null ? AsmWPD : ((int)AsmWPD)) : (CenWPD == null ? CenWPD : ((int)CenWPD))); } }
    public string Recipe { get { return (ReactType.Equals("ASM") ? AsmRecipe1 : CenRecipe1); } }
    [DependsOn("FG", "MG", "PostQty", "BookQty")]
    public bool IsDone { get { return ((FG ?? 0 + MG ?? 0 + PostQty ?? 0) >= BookQty); } }
    [DependsOn("FG", "MG", "PostQty", "BookQty", "BIN", "MWS", "HTQ", "WC02", "WC03", "WC04", "WC05", "WC06", "WC07", "WC08", "WC09")]
    public bool IsPlannable {
       get {
          return ((BIN ?? 0 + LC ?? 0 + MWS ?? 0 + HTQ ?? 0 + WC02 ?? 0 + WC03 ?? 0 + WC04 ?? 0 + WC05 ?? 0 + WC06 ?? 0 + WC07 ?? 0 + WC08 ?? 0 + WC09 ?? 0) > 0)
         && ((FG ?? 0 + MG ?? 0 + PostQty ?? 0) < BookQty);
       }
    }
    [DependsOn("Tasks")]
    public IList<string> Reactors {
       get {return Tasks.Select(t => t.Reactor.ReactorNumber.ToString()).ToList();}
    }
    public List<Inventory> WIP {
      get {
        if (_wip == null) {
          var clsType = this.GetType();
          var workcells = StaticRepository.Workcells.ToList();
          _wip = new WipCollection(workcells.Count);
          foreach(var wc in workcells) {
            var prop = clsType.GetProperty(wc.Name);
            _wip.Add(new Inventory {
              Name = wc.Name,
              Index = wc.Index,
              Qty = (int)(prop.GetValue(this) ?? 0)});
          };
        };
        return _wip;
      }
    }

    public string GetMostRecentWC()
    {
    	string retval = String.Empty;

        foreach( var inventory in WIP.OrderByDescending(wc => wc.Name))
        {
        	if( inventory.Qty > 0)
            {
            	retval = inventory.Name;
                break;
            }
        }

        return retval;
    }
    public int DaysDiff {
      get {
        return MasterSchedDate.HasValue ? (BookDate - MasterSchedDate.Value).Days : 0;
      }
    }

    #endregion

    #region Public Methods
    public void BeginEdit() {
      IsInEditMode = true;  
    }

    public void CancelEdit() {
      IsInEditMode = false;
    }

    public void EndEdit() {
      IsInEditMode = false;
    }

    public void UpdateProcessTimes() {
      BeginEdit();
      var taskQty = Tasks.ToDictionary(t=>t, t=>(int)0);

      var groups = (
          from wc in WIP
          from task in Tasks
          where  
            wc.Index >= task.StartWorkcell && 
            wc.Index <= task.EndWorkcell
          group task by wc into inv
          select new { key = inv.Key, tasks = inv}
        ).ToList();

      foreach(var group in groups){
        int count = group.tasks.Count();
        int qty = group.key.Qty / count;
        foreach (var task in group.tasks) {
          taskQty[task] += qty;
        }
      }

      foreach(var task in Tasks){
         double wpd = WPD(task.Reactor.ReactType);
        if (wpd > 0) {
          int qty = taskQty[task];
          double procTime = (qty / 0.91) / wpd;
          task.End = task.Start.AddDays(procTime);
          task.Reactor.RefreshLayout();
        }
      }
      EndEdit();
    }

    public double WPD(string reactType) {
       switch (reactType) {
          case "ASM":
             if (AsmWPD.HasValue) return (double)AsmWPD.Value;
             break;
          case "CENTURA":
             if (CenWPD.HasValue) return (double)CenWPD.Value;
             break;
       }
       return 0;
    }
     
    // public double WPD(TaskViewModel task) {
    //  switch (task.Reactor.ReactType) {
    //    case "ASM":
    //      if (AsmWPD.HasValue) return (double)AsmWPD.Value;
    //      break;
    //    case "CENTURA":
    //      if (CenWPD.HasValue) {
    //         var fullRate = (double)CenWPD.Value;
    //         var cnt = Regex.Matches(task.Reactor.Chambers, "PROD").Count;
    //         if (cnt == 0 || cnt == 3) {
    //            return fullRate;
    //         }
    //         return (cnt == 1 ? fullRate * .33 : fullRate * .66);  
    //      }
    //      break;
    //  }
    //  return 0;
    //}
    #endregion
  }
}
