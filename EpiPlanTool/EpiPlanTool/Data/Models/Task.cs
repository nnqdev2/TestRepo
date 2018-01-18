using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpiPlanTool.Models {

  [Table("EPT_SCHEDULE_TASK")]
  public class Task {

    public Task() {
      TaskType = "T";
      StartWorkcell = 0;
      EndWorkcell = 12;
    }

    [Key]
    public long TaskID { get; set; }
    public long? MasterTaskID { get; set; }
    [Required, Column("START_TIME")]
    public DateTime Start { get; set; }
    [Required, Column("END_TIME")]
    public DateTime End { get; set; }
    public string Description { get; set; }
    public string OrderID { get; set; }
    [Required]
    public string TaskType { get; set; }
    public long TaskIndex { get; set; }
    public DateTime? DateCreated { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string UpdatedBy { get; set; }
    [MaxLength(9), MinLength(0)]
    public string Color { get; set; }
    public bool IsPinned { get; set; }
    [Column("FROM_WC")]
    public int StartWorkcell { get; set; }
    [Column("TO_WC")]
    public int EndWorkcell { get; set; }
    public bool IsDeleted { get; set; }
    public long? BookedOrderID { get; set; }
    public long ReactorScheduleID { get; set; }
  }
}
