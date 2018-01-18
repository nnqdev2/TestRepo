using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DGO = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace EpiPlanTool.Models {

  public class ReactorStatus {
    [Key]
    public long ReactorID { get; set; }
    public string A { get; set; }
    public string B { get; set; }
    public string C { get; set; }
    public string Dopant { get; set; }
    public string npType { get; set; }
    public string DopantType { get; set; }
  }

}
