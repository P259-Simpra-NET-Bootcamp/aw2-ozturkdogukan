using System.Reflection;

namespace aw2_ozturkdogukan.Core.Mapper
{
    public class ObjectMapper
    {
        /// <summary>
        /// Verilen kaynak nesneyi hedef nesnenin özelliklerine set eder.
        /// </summary>
        /// <param name="target">Hedef</param>
        /// <param name="source">Kaynak</param>
        public static void Map(object target, object source)
        {
            if (source != null && target != null)
            {
                PropertyInfo[] targetProperties = target.GetType().GetProperties();
                foreach (var sourceProperty in source.GetType().GetProperties())
                {
                    PropertyInfo targetProperty = targetProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);
                    if (targetProperty == null)
                        continue;

                    MapProperty(sourceProperty, source, target, targetProperty);
                }
            }
        }

        private static void MapProperty(PropertyInfo sourceProperty, object source, object target, PropertyInfo targetProperty)
        {
            object sourceValue = sourceProperty.GetValue(source);
            if (targetProperty.PropertyType == sourceProperty.PropertyType && sourceValue != null)
                targetProperty.SetValue(target, sourceProperty.GetValue(source));

        }
    }
}
