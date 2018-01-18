using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpiPlanTool.Models {

  [Table("EPT_REACTOR_SCHEDULE")]  
  public class ReactorSchedule {
    public ReactorSchedule(){
      this.Tasks = new List<Task>();
    }
    
    [Key] 
    public long ReactorScheduleID { get; set; }

    public EpiSchedule Schedule { get; set; }
    [Required]
    public long ScheduleID { get; set; }

    public Reactor Reactor { get; set; }
    [Required]
    public long ReactorID { get; set; }
    
    public List<Task> Tasks { get; set; }
  }
}
