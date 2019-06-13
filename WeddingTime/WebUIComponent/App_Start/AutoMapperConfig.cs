using AIT.WebUIComponent.AutoMapping;

namespace AIT.WebUIComponent
{
    public class AutoMapperConfig
    {
        public static void RegisterMapping()
        {
            GuestMap.CreateMap();
            ExpenseMap.CreateMap();
            BallroomMap.CreateMap();
            PdfMap.CreateMap();
            TasksMap.CreateMap();
        }
    }
}