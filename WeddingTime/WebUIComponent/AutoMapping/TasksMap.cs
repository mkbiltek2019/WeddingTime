using AIT.TaskDomain.Model.Entities;
using AIT.TaskDomain.Model.Enums;
using AIT.WebUIComponent.Models.Tasks;
using AIT.WebUIComponent.Services.Undo.DTO.Tasks;
using AutoMapper;
using System;
using System.Globalization;
using System.Linq;

namespace AIT.WebUIComponent.AutoMapping
{
    public class TasksMap
    {
        public static void CreateMap()
        {
            const string dateFormat = "dd/MM/yyyy";

            Mapper.CreateMap<TaskModel, Task>()
                .ForMember(n => n.ReminderDate, o => o.ResolveUsing(x => string.IsNullOrWhiteSpace(x.ReminderDate) ? (DateTime?)null : DateTime.ParseExact(x.ReminderDate, dateFormat, CultureInfo.InvariantCulture)));
            Mapper.CreateMap<Task, TaskModel>()
                .ForMember(n => n.ReminderDate, o => o.ResolveUsing(x => x.ReminderDate.HasValue ? x.ReminderDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null));

            Mapper.CreateMap<Task, TaskInfoModel>()
                .ForMember(n => n.ReminderDate, o => o.ResolveUsing(x => x.ReminderDate.HasValue ? x.ReminderDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null));
            
            Mapper.CreateMap<TaskCardModel, TaskCard>();
            Mapper.CreateMap<TaskCardItemModel, TaskCardItem>();

            Mapper.CreateMap<TaskCard, TaskCardModel>()
                .ForMember(n => n.HasPhoneItems, o => o.ResolveUsing(x => x.Items.Any(n => n.Type == ItemType.Phone)))
                .ForMember(n => n.HasContactItems, o => o.ResolveUsing(x => x.Items.Any(n => n.Type == ItemType.ContactPerson)))
                .ForMember(n => n.HasAddressItems, o => o.ResolveUsing(x => x.Items.Any(n => n.Type == ItemType.Address)))
                .ForMember(n => n.HasLinkItems, o => o.ResolveUsing(x => x.Items.Any(n => n.Type == ItemType.Link)))
                .ForMember(n => n.HasEmailItems, o => o.ResolveUsing(x => x.Items.Any(n => n.Type == ItemType.Email)));
            Mapper.CreateMap<TaskCardItem, TaskCardItemModel>();


            Mapper.CreateMap<Task, TaskUndo>();
            Mapper.CreateMap<TaskCard, TaskCardUndo>();
            Mapper.CreateMap<TaskCardItem, TaskCardItemUndo>();

            Mapper.CreateMap<TaskUndo, Task>();
            Mapper.CreateMap<TaskCardUndo, TaskCard>();
            Mapper.CreateMap<TaskCardItemUndo, TaskCardItem>();
        }
    }
}