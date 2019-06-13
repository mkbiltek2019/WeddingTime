using System.Collections.Generic;

namespace AIT.WebUIComponent.Models.Ballroom
{
    public class BallroomGroupModel
    {
        public string Name { get; set; }
        public Dictionary<string, PersonModel> Persons { get; set; }        // if key is string (its person id) it gives me dictionar out of the box and I am able to serialized it to json
    }
}