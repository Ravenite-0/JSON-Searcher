using System.Collections.Generic;
using System;

namespace Model {
    //This class manages users.
    public class User {
        string _id;
        string url;
        string external_id;
        string name;
        string alias;
        DateTime created_at;
        bool active;
        bool verified;
        bool shared;
        string locale;
        string timezone;
        DateTime last_login_at;
        string email;
        string phone;
        string signature;
        string organization_id;
        List<string> tags;
        bool suspended;
        string role;
    }
}