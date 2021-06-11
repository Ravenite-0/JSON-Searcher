using System.Collections.Generic;
using System;

namespace Model {
    //Since all JSON provided have an _id, inheriting them from a base class makes the code cleaner.
    public abstract class Organization : BaseClass {
        string url {get; set;}
        string external_id {get; set;}
        string name {get; set;}
        List<string> domain_names {get; set;}
        DateTime created_at {get; set;}
        string details {get; set;}
        bool shared_tickets {get; set;}
        List<string> tags {get; set;}
    }
}