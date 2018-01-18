using System;
using PropertyChanged;

namespace EpiPlanTool.Services {

  [ImplementPropertyChanged]
  public class StatusMessageService {
    public StatusMessageService() { }

    public string Message { get; set; }
  }

}
