using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpiPlanTool.Models {
  [Table("EPT_WORKCELL")]
  public class Workcell {
    [Column("ID"), Key]
    public int WorkcellID { get; set; }
    [Column("WC")]
    public string Name { get; set; }

    [NotMapped]
    public int Index { get { return WorkcellID; } }
  }
}
