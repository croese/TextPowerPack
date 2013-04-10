using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TextPowerPack.FlatText.Common
{
  public static class ExtensionMethods
  {
    /// <summary>
    /// Checks to see if <paramref name="mi"/> has an attribute of type <typeparamref name="TAttrib"/>
    /// </summary>
    /// <typeparam name="TAttrib">The type of the attribute to search for</typeparam>
    /// <param name="mi">The member that will be searched</param>
    /// <param name="inherit">True if the inheritance chain of <paramref name="mi"/> 
    /// should be searched for an appropriate attribute type; false otherwise</param>
    /// <returns>True if an attribute of type <typeparamref name="TAttrib"/> is found; 
    /// false otherwise</returns>
    public static bool HasCustomAttribute<TAttrib>(this MemberInfo mi, bool inherit = false)
      where TAttrib : Attribute
    {
      return HasCustomAttribute(mi, typeof(TAttrib), inherit);
    }

    /// <summary>
    /// Checks to see if <paramref name="mi"/> has an attribute of type 
    /// <paramref name="attribType"/>
    /// </summary>
    /// <param name="mi">The member that will be searched</param>
    /// <param name="attribType">The type of the attribute to search for</param>
    /// <param name="inherit">True if the inheritance chain of <paramref name="mi"/> 
    /// should be searched for an appropriate attribute type; false otherwise</param>
    /// <returns>True if an attribute of type <typeparamref name="TAttrib"/> is found; 
    /// false otherwise</returns>
    public static bool HasCustomAttribute(this MemberInfo mi, Type attribType, bool inherit = false)
    {
      return mi.GetCustomAttributes(attribType, inherit).Any();
    }
  }
}
