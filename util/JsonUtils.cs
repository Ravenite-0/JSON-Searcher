using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Newtonsoft.Json.JsonToken;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using static Newtonsoft.Json.JsonConvert;
using static System.IO.File;

namespace Utils {
    ///<summary>A custom JSON converter based on the JSON.NET library that accommodates both JArrays and JObjects</summary>
    public class CustomJsonConverter<T> : JsonConverter {
        public override bool CanConvert(Type objectType) =>
            typeof(T).IsAssignableFrom(objectType);
    
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            (reader.TokenType == StartArray) ?
                JArray.Load(reader).ToObject<T>() :
                new List<T> { JObject.Load(reader).ToObject<T>() };
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            throw new NotImplementedException();
    }

    ///<summary>JsonUtils is a custom Json converter class based on the imported json library.</summary>
    public class JsonUtils {
        [Description("Deserialize JSON string into the supplied generic class."),Category("Json")]
        public static T DeserializeJson<T>(string fileContent) =>
            DeserializeObject<T>(fileContent, new CustomJsonConverter<T>());

        [Description("Identifies and parses JSON string into the correct table."),Category("Json")]
        public static dynamic ParseJsonToTable(Type tableType, string filepath) =>
            typeof(JsonUtils)
                .GetMethod("DeserializeJson")
                .MakeGenericMethod(tableType)
                .Invoke(new JsonUtils(), new object[] { ReadAllText(filepath) });
    }
}