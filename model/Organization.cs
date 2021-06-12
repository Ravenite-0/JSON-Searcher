using System.Collections.Generic;
using System;

namespace Model {
    //This class manages organizations.
    public class Organization : BaseClass {
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