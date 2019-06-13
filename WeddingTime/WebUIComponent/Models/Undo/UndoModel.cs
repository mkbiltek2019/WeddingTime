using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIT.WebUIComponent.Models.Undo
{
    public class UndoModel
    {
        public Guid Key { get; set; }
        public string Description { get; set; }
    }
}