using System.Collections.Generic;
using System;

namespace Model {
    //This class manages organizations.
    public class Organization {
        string _id;
        string url;
        string external_id;
        string name;
        List<string> domain_names;
        DateTime created_at;
        string details;
        bool shared_tickets;
        List<string> tags;
    }
}