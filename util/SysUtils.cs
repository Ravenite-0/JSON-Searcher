using System.Reflection;
using System.Collections.Generic;
using System;

///<summary>Manages methods that involves managing different property and object types.</summary>
namespace Utils {
  public static class SysUtils {
    public static bool IsObjectList(PropertyInfo p) =>
      (typeof(ICollection<>).IsAssignableFrom(p.PropertyType));
    
    public static bool IsObjectDateTime(PropertyInfo p) =>
      (typeof(DateTime).IsAssignableFrom(p.PropertyType));
  }
}