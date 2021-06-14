using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Newtonsoft.Json.JsonToken;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using static Newtonsoft.Json.JsonConvert;
using Model;
namespace Utils {
    ///<summary>JsonUtils is a custom Json converter class based on the imported json library.</summary>
    public class CustomJsonConverter<T> : JsonConverter {
        public override bool CanConvert(Type objectType) =>
            typeof(T).IsAssignableFrom(objectType);
    
        [Description("A custom Json parser that checks if the Json input is a single item or an array."),Category("Json")]
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            (reader.TokenType == StartArray) ?
                JArray.Load(reader).ToObject<T>() :
                new List<T> { JObject.Load(reader).ToObject<T>() };
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            throw new NotImplementedException();
    }

    public class JsonUtils {
        public static T ParseJsonToTable<T>(T data, string fileContent) =>
            DeserializeObject<T>(fileContent, new CustomJsonConverter<T>());
    }
}