using System;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Services.Undo.DTO.Guests
{
    [Serializable]
    public class GroupUndo
    {               
        public int Index { get; set; }                              // to remember group position on the page
        public int? OrderNo { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }

        public List<PersonUndo> Persons { get; set; }
        public List<InnerGroupUndo> InnerGroups { get; set; }
    }
}