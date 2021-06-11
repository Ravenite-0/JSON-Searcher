using System.Collections.Generic;
using System;

namespace Model {
    //This class manages users.
    public abstract class User : BaseClass {
        string url {get; set;}
        string external_id {get; set;}
        string name {get; set;}
        string alias {get; set;}
        DateTime created_at {get; set;}
        bool active {get; set;}
        bool verified {get; set;}
        bool shared {get; set;}
        string locale {get; set;}
        string timezone {get; set;}
        DateTime last_login_at {get; set;}
        string email {get; set;}
        string phone {get; set;}
        string signature {get; set;}
        string organization_id {get; set;}
        List<string> tags {get; set;}
        bool suspended {get; set;}
        string role {get; set;}

    }
}