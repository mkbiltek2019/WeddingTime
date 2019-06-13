using AIT.WebUtilities.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AIT.WebUtilities.Helpers
{
    public static class DropDownCollectionHelpers
    {
        public static List<DropDownItem> CreateDropDownCollection<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(n => new DropDownItem
            {
                Text = GetDescription(n),// manager.GetString(string.Format("{0}Desc", n)),
                Value = (((int)(object)n)).ToString()
            }).ToList();
        }

        private static string GetDescription<T>(T e)
        {
            MemberInfo memberInfo = typeof(T).GetMember(e.ToString()).FirstOrDefault();

            if (memberInfo != null)
            {
                DescriptionAttribute attribute = (DescriptionAttribute)memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                return attribute.Description;
            }

            return string.Empty;
        }

        //public static List<DropDownItem> CreateDropDownCollection<T>()
        //{
        //    var manager = new ResourceManager(typeof(Resources));

        //    return Enum.GetValues(typeof(T)).Cast<T>().Select(n => new DropDownItem
        //    {
        //        Text = manager.GetString(string.Format("{0}Desc", n)),
        //        Value = (((int)(object)n)).ToString()
        //    }).ToList();
        //}
    }
}
