using System.Management;

namespace WmiProcessTracker
{
    internal static class ManagementBaseObjectExtension
    {
        public static T Get<T>(this ManagementBaseObject e, params string[] paths)
        {
            object obj = e;
            foreach (string pathItem in paths)
            {
                if(obj != null && obj is ManagementBaseObject mbo) obj = mbo?[pathItem];
            }
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }
}