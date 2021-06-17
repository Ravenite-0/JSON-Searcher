using System.Reflection;
using System.Collections.Generic;
using System;

///<summary>Manages methods that involves managing different property and object types.</summary>
namespace Utils {
  public static class SysUtils {
    public static bool IsObjectStringList(PropertyInfo p) =>
      (typeof(List<string>).IsAssignableFrom(p.PropertyType));
    
    public static bool IsObjectDateTime(PropertyInfo p) =>
      (typeof(DateTime).IsAssignableFrom(p.PropertyType));
    
    public static PropertyInfo GetPropertyFromEntity(dynamic entity, string name) =>
      entity.GetType().GetProperty(name);
  }
}