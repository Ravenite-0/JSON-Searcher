using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using static Newtonsoft.Json.Required;

namespace Model {
  /// <summary>Represents the user JSON schema.</summary>
  public class User {
    [JsonPropertyName("_id"),JsonProperty(Required = Always)] public string _id {get; set;}
    [JsonPropertyName("url")] public string url {get; set;}
    [JsonPropertyName("external_id")] public string external_id {get; set;}
    [JsonPropertyName("name")] public string name {get; set;}
    [JsonPropertyName("alias")] public string alias {get; set;}
    [JsonPropertyName("created_at")] public DateTime created_at {get; set;}
    [JsonPropertyName("active")] public bool active {get; set;}
    [JsonPropertyName("verified")] public bool verified {get; set;}
    [JsonPropertyName("shared")] public bool shared {get; set;}
    [JsonPropertyName("locale")] public string locale {get; set;}
    [JsonPropertyName("timezone")] public string timezone {get; set;}
    [JsonPropertyName("last_login_at")] public DateTime last_login_at {get; set;}
    [JsonPropertyName("email")] public string email {get; set;}
    [JsonPropertyName("phone")] public string phone {get; set;}
    [JsonPropertyName("signature")] public string signature {get; set;}
    [JsonPropertyName("organization_id")] public string organization_id {get; set;}
    [JsonPropertyName("tags")] public List<string> tags {get; set;}
    [JsonPropertyName("suspended")] public bool suspended {get; set;}
    [JsonPropertyName("role")] public string role {get; set;}
  }
}