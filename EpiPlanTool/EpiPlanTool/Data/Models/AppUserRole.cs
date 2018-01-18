using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpiPlanTool.Models {

  [Flags]
  public enum RoleFlags {
    None    = 0,
    Planner = 1,
    Admin   = 8
  }
    
  [Table("EPT_USERS")]
  public class AppUserRole {
  	[Key]
  	public string UserName { get; set; }
    public RoleFlags Roles { get; set; }
  }
}
