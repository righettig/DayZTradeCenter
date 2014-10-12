using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DayZTradeCenter.UI.Web.Helpers
{
    public static class EnumExtensions
    {
        public static MvcHtmlString LocalizedEnumDropDownListFor<TModel, TEnum>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression,
            object htmlAttributes) where TEnum : struct
        {
            var values = Values<TEnum>();

            var items = from value in values
                select new SelectListItem
                {
                    Text = value.GetDescription(), // generalization of 'ToString'
                    Value = value.ToString()
                };

            return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
        }

        public static MvcHtmlString LocalizedEnumDisplayFor<TModel, TEnum>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression) where TEnum : struct
        {
            var values = Values<TEnum>();

            var result = values
                .First(
                    x =>
                        x.ToString() == ModelMetadata
                            .FromLambdaExpression(expression, htmlHelper.ViewData)
                            .SimpleDisplayText)
                .GetDescription(); // generalization of 'ToString'

            return new MvcHtmlString(result);
        }

        public static string GetDescription(this Enum enumValue)
        {
            var fi = enumValue
                .GetType()
                .GetField(enumValue.ToString());

            var attributes =
                (DescriptionAttribute[]) fi.GetCustomAttributes(
                    typeof (DescriptionAttribute),
                    false);

            return attributes.Length > 0
                ? attributes[0].Description
                : enumValue.ToString();
        }

        /// <summary>
        /// Gets all the values for the specified enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the property.</typeparam>
        /// <returns>The values for the specified enum.</returns>
        /// <exception cref="System.ArgumentException">T must be an enumerated type.</exception>
        private static IEnumerable<Enum> Values<TEnum>() where TEnum : struct
        {
            if (!typeof (TEnum).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type.");
            }

            return Enum // NB: 'Enum' instead of TEnum so that the ext method 'GetDescription' can be invoked.
                .GetValues(typeof (TEnum))
                .Cast<Enum>();
        }
    }
}