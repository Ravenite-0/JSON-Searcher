using System.Reflection;
using System.Collections.Generic;
using System;
using static System.String;
using System.Linq;

///<summary>Manages methods that involves managing different property and object types.</summary>
namespace Utils {
  public static class SysUtils {
    public static bool IsObjectStringList(PropertyInfo p) =>
      (typeof(List<string>).IsAssignableFrom(p.PropertyType));
    
    public static bool IsObjectDateTime(PropertyInfo p) =>
      (typeof(DateTime).IsAssignableFrom(p.PropertyType));
  }
}