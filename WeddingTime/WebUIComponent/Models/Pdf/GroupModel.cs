using System.Collections.Generic;

namespace AIT.WebUIComponent.Models.Pdf
{
    public class GroupModel
    {
        public string Name { get; set; }
        public List<PersonModel> Persons { get; set; }
    }
}