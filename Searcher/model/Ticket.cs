using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using static Newtonsoft.Json.Required;
namespace Model {
  /// <summary>Represents the ticket JSON schema.</summary>
  public class Ticket {
    [JsonPropertyName("_id"),JsonProperty(Required = Always)] public string _id {get; set;}
    [JsonPropertyName("url")] public string url {get; set;}
    [JsonPropertyName("external_id")] public string external_id {get; set;}
    [JsonPropertyName("created_at")] public DateTime created_at {get; set;}
    [JsonPropertyName("type")] public string type {get; set;}
    [JsonPropertyName("subject")] public string subject {get; set;}
    [JsonPropertyName("description")] public string description {get; set;}
    [JsonPropertyName("priority")] public string priority {get; set;}
    [JsonPropertyName("status")] public string status {get; set;}
    [JsonPropertyName("submitted_id")] public string submitter_id {get; set;}
    [JsonPropertyName("assignee_id")] public string assignee_id {get; set;}
    [JsonPropertyName("organization_id")] public string organization_id {get; set;}
    [JsonPropertyName("tags")] public List<string> tags {get; set;}
    [JsonPropertyName("has_incidents")] public bool has_incidents {get; set;}
    [JsonPropertyName("due_at")] public DateTime due_at {get; set;}
    [JsonPropertyName("via")] public string via {get; set;}
  }
}