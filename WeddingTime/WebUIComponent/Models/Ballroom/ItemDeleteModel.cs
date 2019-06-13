using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIT.WebUIComponent.Models.Ballroom
{
    public class ItemDeleteModel
    {
        public bool TableSpecific
        {
            get { return !string.IsNullOrEmpty(UndoView); }
        }

        public string UndoView { get; set; }
    }
}