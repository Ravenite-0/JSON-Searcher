using System.Collections.Generic;
using System;

namespace Model {
    //This class manages tickets.
    public class Ticket {
        public string _id {get; set;}
        public string url {get; set;}
        public string external_id {get; set;}
        public DateTime created_at {get; set;}
        public string type {get; set;}
        public string subject {get; set;}
        public string description {get; set;}
        public string priority {get; set;}
        public string status {get; set;}
        public string submitter_id {get; set;}
        public string assignee_id {get; set;}
        public string organization_id {get; set;}
        public List<string> tags {get; set;}
        public bool has_incidents {get; set;}
        public DateTime due_at {get; set;}
        public string via {get; set;}
    }
}