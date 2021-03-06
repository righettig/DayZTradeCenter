﻿using System.IO;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DayZTradeCenter.UI.Web.Helpers
{
    // http://blog.mariusschulz.com/2014/02/05/passing-net-server-side-data-to-javascript
    public static class JavaScriptConvert
    {
        public static IHtmlString SerializeObject(object value)
        {
            using (var stringWriter = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                var serializer = new JsonSerializer
                {
                    // Let's use camelCasing as is common practice in JavaScript
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                // We don't want quotes around object names
                jsonWriter.QuoteName = false;
                serializer.Serialize(jsonWriter, value);

                return new HtmlString(stringWriter.ToString());
            }
        }
    }
}