using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;

namespace MoneyConverter.Models
{
    public class DefaultUnknownEnumConverter : StringEnumConverter
    {
        private readonly int defaultValue;
        public DefaultUnknownEnumConverter()
        { }
        public DefaultUnknownEnumConverter(int defaultValue)
        {
            this.defaultValue = defaultValue;
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch
            {
                return Enum.Parse(objectType, $"{defaultValue}");
            }
        }
        public override bool CanConvert(Type objectType)
        {
            return base.CanConvert(objectType) && objectType.GetTypeInfo().IsEnum && Enum.IsDefined(objectType, defaultValue);
        }
    }
}
