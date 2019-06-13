using AIT.GuestDomain.Model.Enums;
using AIT.WebUtilities.DTO;
using AIT.WebUtilities.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AIT.WebUIComponent.Models.Guests
{
    public class PersonModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Surname { get; set; }

        [Display(Name = "Telefon:")]
        public string Phone { get; set; }
        
        [RegularExpression(@"[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "Adres:")]
        public string Address { get; set; }

        public bool IsAddressDefined
        {
            get { return !string.IsNullOrEmpty(Address); }
        }

        public bool IsPhoneDefined
        {
            get { return !string.IsNullOrEmpty(Phone); }
        }

        public bool IsEmailDefined
        {
            get { return !string.IsNullOrEmpty(Email); }
        }

        [Required]
        [Display(Name = "Status:")]
        public int Status { get; set; }

        [Required]
        public int Genre { get; set; }

        public List<DropDownItem> GenreCollection
        {
            get { return DropDownCollectionHelpers.CreateDropDownCollection<PersonGenre>(); }
        }

        public List<DropDownItem> StatusCollection
        {
            get { return DropDownCollectionHelpers.CreateDropDownCollection<ConfirmationStatus>(); }
        }

        public string GenreText
        {
            get { return GenreCollection[Genre].Text; }
        }

        public string StatusText
        {
            get { return StatusCollection[Status].Text; }
        }

        public string StatusValue
        {
            get { return Enum.GetName(typeof(ConfirmationStatus), Status).ToLower(); }
        }

        public InnerGroupMemberModel InnerGroupMember { get; set; }

        public bool IsInnerGroupMember
        {
            get { return InnerGroupMember != null; }
        }

        public Guid? InnerGroupKey
        {
            get { return IsInnerGroupMember ? InnerGroupMember.InnerGroupKey : (Guid?)null; }
        }
    }
}