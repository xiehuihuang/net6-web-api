
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project.Framework.CustomEnum
{
    public static class CustomEnumExtend
    {
        public static string? GetRemark(this Enum value)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());
            if (field != null && field.IsDefined(typeof(RemarkAttribute), true))
            {
                RemarkAttribute? remarkAttribute = field.GetCustomAttribute<RemarkAttribute>();
                return remarkAttribute?.GetRemark();
            }
            return string.Empty;
        }

        /// <summary>
        /// 枚举转换成IList<SelectListItem>
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IList<SelectListItem> ToSelectListByEnum(Type enumType, string selectdValue = "", string text = "请选择", string value = "")
        {
            IList<SelectListItem> listItem = new List<SelectListItem>();
            if (enumType.IsEnum)
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    listItem.Add(new SelectListItem { Text = text, Value = value });
                }
                foreach (string enumText in Enum.GetNames(enumType))
                {
                    FieldInfo? field = enumType.GetField(enumText);
                    string remark = string.Empty;
                    if (field != null && field.IsDefined(typeof(RemarkAttribute), true))
                    {
                        object[] arr = field.GetCustomAttributes(typeof(RemarkAttribute), true);
                        remark = arr != null && arr.Length > 0 ? ((RemarkAttribute)arr[0]).GetRemark() : enumText;
                        SelectListItem selectListItem = new SelectListItem()
                        {
                            Value = ((int)Enum.Parse(enumType, enumText)).ToString(),
                            Text = remark,
                            Selected = false
                        };
                        if (selectListItem.Value.Equals(selectdValue))
                        {
                            selectListItem.Selected = true;
                        }
                        listItem.Add(selectListItem);
                    }
                }
            }
            else
            {
                throw new ArgumentException("请传入正确的枚举！");
            }
            return listItem;
        }
    }

    public class RemarkAttribute : Attribute
    {
        private string _Rmark;
        public RemarkAttribute(string remark)
        {
            _Rmark = remark;
        }

        public string GetRemark() => _Rmark;
    }
}
