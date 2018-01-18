using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpiPlanTool.Models {
  partial class ReactorSchedule {
    public String Label { get { return this.Reactor.ReactorName; } }
  }

}
