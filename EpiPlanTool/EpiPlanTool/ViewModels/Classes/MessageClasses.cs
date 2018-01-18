using System;
using LightMessageBus.Interfaces;
using EpiPlanTool.ViewModels;

namespace EpiPlanTool.Messages {

  public abstract class MessageBase : IMessage {
    public object Source { get; set; }
    protected MessageBase(object source) {
      Source = source;
    }
  }

  public class StatusDisplayMessage : MessageBase {
    public String Message { get; set; }
    public StatusDisplayMessage(object source, String message)
      : base(source) {
      this.Message = message;
    }
  }

  public class CloseOrdersContextMenuMessage : MessageBase {
    public CloseOrdersContextMenuMessage(object source) : base(source) { }
  }

  public class SetOrdersContextMenuSearchBox : MessageBase {
    public SetOrdersContextMenuSearchBox(object source) : base(source) { }
  }

  public class TaskMessageBase : MessageBase {
    public TaskViewModel Task { get { return Source as TaskViewModel; } }
    public TaskMessageBase(TaskViewModel task) : base(task) { }
  }

  public class EditTaskMessage : TaskMessageBase {
    public EditTaskMessage(TaskViewModel task) : base(task) { }
  }

  public class TaskSelectedMessage : TaskMessageBase {
    public TaskSelectedMessage(TaskViewModel task) : base(task) { }
  }

  public class UserAuthenticatedMessage : MessageBase {
    public String UserID { get; set; }
    public bool AllowEdits { get; set; }
    public UserAuthenticatedMessage(object source, String userId, bool allowEdits) : base(source) {
      this.UserID = userId;
      this.AllowEdits = allowEdits;
    }
  }


}