namespace AIT.WebUIComponent.Models.Tasks
{
    public class TaskInfoModel
    {
        public int Id { get; set; }
        public int State { get; set; }                  // corresponds to TaskState enum from task domain, used mainly by undo action
        public string Title { get; set; }
        public string ReminderDate { get; set; }
    }
}