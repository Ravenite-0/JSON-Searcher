using static System.Reflection.Assembly;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Model;
using System;
using System.Collections.Generic;
using static Newtonsoft.Json.Linq.JToken;

namespace Utils {
    //This class uses inheritance to instantiate a custom JSON reader that converts JSON files to various classes
    public class JsonUtils<T> : JsonConverter {
        public override bool CanConvert(Type objectType) =>
            typeof(T).IsAssignableFrom(objectType);
    
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            (reader.TokenType == JsonToken.StartArray) ?
                JArray.Load(reader).ToObject<T>() :
                new List<T> { JObject.Load(reader).ToObject<T>() };
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            throw new NotImplementedException();
    }
}