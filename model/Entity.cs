using System;

namespace Model {
    public abstract class Entity {
        public string _id {get; set;}
        protected void ToConsoleString() {
            foreach(var property in this.GetType().GetProperties()) {
                Console.WriteLine($"{property.Name.ToString()}  ->  {property.GetValue(this).ToString()}");
            }
            Console.WriteLine("==================================================");
        }
    }
}