using System.Collections.Generic;
using System;

namespace Model {
    //This class manages users.
    public class User : Entity {
        public string url {get; set;}
        public string external_id {get; set;}
        public string name {get; set;}
        public string alias {get; set;}
        public DateTime created_at {get; set;}
        public bool active {get; set;}
        public bool verified {get; set;}
        public bool shared {get; set;}
        public string locale {get; set;}
        public string timezone {get; set;}
        public DateTime last_login_at {get; set;}
        public string email {get; set;}
        public string phone {get; set;}
        public string signature {get; set;}
        public string organization_id {get; set;}
        public List<string> tags {get; set;}
        public bool suspended {get; set;}
        public string role {get; set;}
    }
}