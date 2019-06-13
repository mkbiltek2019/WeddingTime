using AIT.WebUIComponent.Models.Shared.Enums;

namespace AIT.WebUIComponent.Models.Shared
{
    public class UpdateOrderNoBaseModel
    {
        public int? BaseItemId { get; set; }
        public UpdateOrderNoType UpdateType { get; set; }               //I could have just int and in java script the same model as in guest domain?
    }
}