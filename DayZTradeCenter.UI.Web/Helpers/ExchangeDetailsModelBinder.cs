using System;
using System.ComponentModel;
using System.Web.Mvc;

namespace DayZTradeCenter.UI.Web.Helpers
{
    /// <summary>
    /// Fixes the issue with dates like 10/20/2014 that are wrongly interpreted when run outside "en" culture.
    /// </summary>
    public class ExchangeDetailsModelBinder : DefaultModelBinder
    {
        protected override void BindProperty(
            ControllerContext controllerContext, 
            ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.Name == "Time")
            {
                var value = controllerContext.HttpContext.Request.Form["Details.Time"];

                var date =
                    DateTime.ParseExact(value, "MM/dd/yyyy h:mm tt", System.Globalization.CultureInfo.InvariantCulture);

                propertyDescriptor.SetValue(bindingContext.Model, date);
            }
            else
            {
                base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
            }
        }
    }
}