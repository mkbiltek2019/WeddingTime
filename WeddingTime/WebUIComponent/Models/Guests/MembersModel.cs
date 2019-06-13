using System;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Models.Guests
{
    public class MembersModel
    {
        private Guid? _previousKey;

        public int? UniqueId { get; set; }
        public List<PersonModel> Members { get; set; }

        public bool OpenTag(PersonModel person)
        {
            if (!person.IsInnerGroupMember)
                return true;

            if (HasDifferentKey(person))
                return true;

            return false;
        }

        public bool CloseTag(PersonModel person)
        {
            if (!person.IsInnerGroupMember)
                return true;

            var index = Members.IndexOf(person);
            if (index < Members.Count - 1 && Members[index + 1].InnerGroupKey != _previousKey)
                return true;

            // means that that was the last element on Members list, it belongs to inner group 
            // so we have to close the tag because there is no any element to compere
            if (index == Members.Count - 1)
                return true;

            return false;
        }

        private bool HasDifferentKey(PersonModel person)
        {
            if (person.InnerGroupMember.InnerGroupKey == _previousKey)
                return false;

            _previousKey = person.InnerGroupMember.InnerGroupKey;
            return true;
        }
    }
}