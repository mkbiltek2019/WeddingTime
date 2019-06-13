using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIT.WebUIComponent.Models.Ballroom
{
    public class PersonModel
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
        //public string FullName
        //{
        //    get { return string.Format("{0} {1}", Name, Surname).TrimEnd(); }
        //}
    }
}