namespace Model {
    //Since all JSON provided have an _id, inheriting them from a base class makes the code cleaner.
    public abstract class BaseClass {
        protected string id {get; set;}
    }
}