using System.Collections.Generic;
using System;

namespace Model {
    //This class manages tickets.
    public abstract class Ticket : BaseClass {
        string url {get; set;}
        string external_id {get; set;}
        DateTime created_at {get; set;}
        string type {get; set;}
        string subject {get; set;}
        string description {get; set;}
        string priority {get; set;}
        string status {get; set;}
        string submitter_id {get; set;}
        string assignee_id {get; set;}
        string organization_id {get; set;}
        List<string> tags {get; set;}
        bool has_incidents {get; set;}
        DateTime due_at {get; set;}
        string via {get; set;}
    }
}