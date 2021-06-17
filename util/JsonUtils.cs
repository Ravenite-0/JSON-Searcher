using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using static Newtonsoft.Json.JsonToken;
using static Newtonsoft.Json.JsonConvert;
using static Utils.ConsoleUtils;

namespace Utils {
  ///<summary>A custom JSON converter based on the Newtonsoft.Json library.</summary>
  public class CustomJsonConverter<T> : JsonConverter {
    public override bool CanConvert(Type objectType) =>
      typeof(T).IsAssignableFrom(objectType);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
      try {
        return (reader.TokenType == StartArray) ?
          JArray.Load(reader).ToObject<T>() :
          throw new FormatException();
      } catch (Exception e) {
        if(e is JsonReaderException) {
          OutputExceptionToConsole(e, "Error: Invalid JSON schema found:");
        } else if (e is FormatException) {
          OutputExceptionToConsole(e, "Something went wrong with the JSON schema formatting. (Did you forget to add [] to your JSON objects?)", false);
        } else {
          OutputExceptionToConsole(e, "Oops! Something went wrong with JSON file parsing:");
        }
        return false;
      }
    }
      
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
      throw new NotImplementedException();
  }

  ///<summary>JsonUtils manages methods that relates to JSON content.</summary>
  public class JsonUtils {
    public static T DeserializeJson<T>(string fileContent) =>
      DeserializeObject<T>(fileContent, new CustomJsonConverter<T>());
    
    //Allows JSON deserialization based on a specific typ
    public static dynamic ParseJsonToTable(Type tableType, string fileContent) =>
      typeof(JsonUtils)
        .GetMethod("DeserializeJson")
        .MakeGenericMethod(tableType)
        .Invoke(new JsonUtils(), new object[] { fileContent });
  }
}