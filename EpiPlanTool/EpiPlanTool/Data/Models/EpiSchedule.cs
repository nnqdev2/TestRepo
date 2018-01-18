using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DGO = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace EpiPlanTool.Models {
 
  [Table("EPT_SCHEDULE")] 
  public class EpiSchedule {
    public EpiSchedule() {
      this.Status = "A";
      this.ReactorSchedules = new List<ReactorSchedule>();
    }
    
    [Key]
    public long ScheduleID { get; set; }
    [Required]
    public string Status { get; set; }
    [Required]
    public string SchedCode { get; set; }
    public DateTime? DateCreated { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? DatePublished { get; set; }
    public string PublishedBy { get; set; }
    
    public List<ReactorSchedule> ReactorSchedules { get; set; }
  }
}
