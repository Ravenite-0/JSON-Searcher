using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using static Newtonsoft.Json.Required;

namespace Model {
  /// <summary>Represents the organization JSON schema.</summary>
  public class Organization {
    [JsonPropertyName("_id"),JsonProperty(Required = Always)] public string _id {get; set;}
    [JsonPropertyName("url")] public string url {get; set;}
    [JsonPropertyName("external_id")] public string external_id {get; set;}
    [JsonPropertyName("name")] public string name {get; set;}
    [JsonPropertyName("domain_names")] public List<string> domain_names {get; set;}
    [JsonPropertyName("created_at")] public DateTime created_at {get; set;}
    [JsonPropertyName("details")] public string details {get; set;}
    [JsonPropertyName("shared_tickets")] public bool shared_tickets {get; set;}
    [JsonPropertyName("tags")] public List<string> tags {get; set;}
  }
}