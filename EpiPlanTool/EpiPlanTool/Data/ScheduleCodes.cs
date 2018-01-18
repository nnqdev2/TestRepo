using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace EpiPlanTool.Models {

  public class ScheduleCodes {

#if DEBUG
    private const String MASTER_SCHED_CODE = "_M";
    private const String MASTER_SCHED_NAME = "Master";
    private const String PUB_SCHED_CODE = "_P";
    private const String PUB_SCHED_NAME = "Published";
#else
    private const String MASTER_SCHED_CODE = "M";
    private const String MASTER_SCHED_NAME = "Master";
    private const String PUB_SCHED_CODE = "P";
    private const String PUB_SCHED_NAME = "Published";
#endif

    private static StringDictionary SchedCodes;
    private static String _defaultMasterCode;
    private static String _defaultPublishCode;

    static ScheduleCodes() {
      SchedCodes = new StringDictionary(){
        {MASTER_SCHED_CODE, MASTER_SCHED_NAME},
        {PUB_SCHED_CODE, PUB_SCHED_NAME}
      };
      _defaultMasterCode = MASTER_SCHED_CODE;
      _defaultPublishCode = PUB_SCHED_CODE;
    }

    public static String Name(String code){
      return SchedCodes[code]; 
    }

    public static String MasterSchedCode{ get { return _defaultMasterCode; }}
    public static String PublishSchedCode{ get { return _defaultPublishCode; }}

  }
}
