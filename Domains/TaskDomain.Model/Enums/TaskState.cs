using System.ComponentModel;

namespace AIT.TaskDomain.Model.Enums
{
    public enum TaskState
    {
        [Description("Nowe zadanie")]
        NewTask = 0,
        [Description("Trwające zadanie")]
        OngoingTask = 1,
        [Description("Zakończone zadanie")]
        DoneTask = 2
    }
}
