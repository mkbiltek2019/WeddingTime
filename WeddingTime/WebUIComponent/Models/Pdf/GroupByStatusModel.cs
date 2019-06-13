using System.Collections.Generic;

namespace AIT.WebUIComponent.Models.Pdf
{
    public class GroupByStatusModel
    {
        public string Name { get; set; }
        public List<PersonModel> PersonsAccepted { get; set; }          // all three properties are filled by automapper
        public List<PersonModel> PersonsDeclined { get; set; }
        public List<PersonModel> PersonsUnconfirmed { get; set; }
    }
}