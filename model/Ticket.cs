using System.Collections.Generic;
using System;

namespace Model {
    //This class manages tickets.
    public class Ticket {
        string _id;
        string url;
        string external_id;
        DateTime created_at;
        string type;
        string subject;
        string description;
        string priority;
        string status;
        string submitter_id;
        string assignee_id;
        string organization_id;
        List<string> tags;
        bool has_incidents;
        DateTime due_at;
        string via;
    }
}