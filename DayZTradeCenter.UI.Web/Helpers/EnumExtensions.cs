using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DayZTradeCenter.UI.Web.Helpers
{
    public static class EnumExtensions
    {
        public static MvcHtmlString LocalizedEnumDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes)
        {
            var values = Enum.GetValues(typeof(TProperty))
                .Cast<Enum>(); // NB: 'Enum' instead of TProperty so that the ext method 'GetDescription' can be invoked.

            var items = from value in values
                        select new SelectListItem
                        {
                            Text = value.GetDescription(), // generalization of 'ToString'
                            Value = value.ToString()
                        };

            return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
        }

        public static string GetDescription(this Enum enumValue)
        {
            var fi = enumValue
                .GetType()
                .GetField(enumValue.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            return attributes.Length > 0
                ? attributes[0].Description
                : enumValue.ToString();
        }
    }
}