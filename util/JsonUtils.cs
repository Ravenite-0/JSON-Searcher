using static System.Reflection.Assembly;
using static System.IO.Directory;
using static System.IO.Path;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Model;
using System;

namespace Utils {
    //This class uses inheritance to instantiate a custom JSON reader that converts JSON files to various classes
    public class JsonUtils : JsonConverter {
        public override bool CanConvert(Type objectType) =>
            typeof(BaseClass).IsAssignableFrom(objectType);
    
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var token = reader.TokenType;
            if(token == JsonToken.StartArray) {
                
            }
            return existingValue;
        }
        

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
        throw new NotImplementedException();
    }
}