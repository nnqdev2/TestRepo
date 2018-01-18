namespace EpiPlanTool.Models {
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using DGO = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

  [Table("EPT_REACTOR")]  
  public class Reactor {
    public long ReactorID { get; set; }
    [Column("REACTOR_NAME")]
    public string Caption { get; set; }
    public short ReactorNumber { get; set; }
    [Column("REACTOR_TYPE")]
    public string ReactType { get; set; }
  }
}
