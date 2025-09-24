using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.Main.Utils
{
    internal static class GeneralUtils
    {

        public static TTarget CopyTo<TTarget>(this object source) where TTarget : new()
        {
            var target = new TTarget();
            var sourceProps = source.GetType().GetProperties();
            var targetProps = typeof(TTarget).GetProperties();

            foreach (var sp in sourceProps)
            {
                var tp = targetProps.FirstOrDefault(p => p.Name == sp.Name && p.PropertyType == sp.PropertyType);
                if (tp != null && tp.CanWrite)
                    tp.SetValue(target, sp.GetValue(source));
            }

            return target;
        }

        public static object ConvertToType(this object source, Type targetType)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            // Create a new instance of the target type
            var target = Activator.CreateInstance(targetType);

            // Copy properties
            var sourceProps = source.GetType().GetProperties();
            var targetProps = targetType.GetProperties();

            foreach (var sp in sourceProps)
            {
                var tp = targetProps.FirstOrDefault(p => p.Name == sp.Name &&
                                                         p.PropertyType == sp.PropertyType &&
                                                         p.CanWrite);
                if (tp != null)
                {
                    var value = sp.GetValue(source);
                    tp.SetValue(target, value);
                }
            }

            // Copy fields too (optional)
            var sourceFields = source.GetType().GetFields();
            var targetFields = targetType.GetFields();

            foreach (var sf in sourceFields)
            {
                var tf = targetFields.FirstOrDefault(f => f.Name == sf.Name &&
                                                          f.FieldType == sf.FieldType);
                if (tf != null)
                {
                    var value = sf.GetValue(source);
                    tf.SetValue(target, value);
                }
            }

            return target;
        }

    }
}
