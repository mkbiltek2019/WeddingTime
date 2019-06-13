namespace AIT.WebUIComponent.Services.Undo.DTO.Tasks
{
    public class TaskCardItemUndo
    {
        public string Value { get; set; }
        public int Type { get; set; }           // corresponds to ItemType enum in card domain
    }
}
