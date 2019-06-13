using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Services.RenewMembership.DTO;
using AIT.UtilitiesComponents.Chains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIT.GuestDomain.Services.RenewMembership.Links
{
    public class RenewGroupAndMembershipLink : ChainLink<RenewSpecification, Boolean>
    {
        public override bool CanHandle(RenewSpecification specification)
        {
            var renewItem = specification.RenewItem;
            var group = specification.Group;

            //jest to spelnione gdy usunelismy z grupy wszystkich oprocz jednego...teraz przy pierwszym elemencie, ktory ma wpisany relatedperson groupmemebership
            //tworzymy taka grupe (i tu moze byc problem przy nastepnym przebiegu...) jesli w grupie byly trzy osoby a usunelismy dwie dla kolejnej juz tej grupy nie
            //stworzymy, poniewaz zostala ona stworzona w kroku poprzednim, dla undo elementu, ktory byl rowniez w tej grupie
            return renewItem.IsInnerGroupMember &&
                   renewItem.RenewRelatedPersonGroupMembership &&
                   group.InnerGroups.All(n => n.GroupKey != renewItem.InnerGroupKey);
        }

        public override bool Handle(RenewSpecification specification)
        {
            if (CanHandle(specification))
            {
                var group = specification.Group;

                var innerGroupKey = specification.RenewItem.InnerGroupKey.Value;
                var relatedPersonId = specification.RenewItem.RelatedPersonId.Value;

                if (!group.CheckGroupMember(relatedPersonId))
                    throw new Exception();

                group.AddInnerGroup(innerGroupKey);
                group.CreateInnerGroupMembership(innerGroupKey, relatedPersonId);

                return true;
            }

            return Successor.Handle(specification);
        }
    }
}