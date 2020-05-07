using System;
using System.ComponentModel;
using System.Linq;

public static class AutoMapperExtensions
{
    /// <summary>
    /// Usage:
    /// NewModel newObject = originalObject.CopyTo<NewModel>();
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static T CopyTo<T>(this object source) where T : new()
    {
        T targetObj = new T();
        var targetProperties = TypeDescriptor.GetProperties(targetObj).Cast<PropertyDescriptor>().ToArray();
        var sourceProperties = TypeDescriptor.GetProperties(source).Cast<PropertyDescriptor>().ToArray();

        foreach (var sourceProp in sourceProperties)
        {
            object objSourceVal = sourceProp.GetValue(source);
            int targetIndex = Array.IndexOf(targetProperties, sourceProp);
            
            if (targetIndex >= 0)
            {
                var targetProp = targetProperties[targetIndex];
                targetProp.SetValue(targetObj, objSourceVal);
            }
        }

        return targetObj;
    }
}