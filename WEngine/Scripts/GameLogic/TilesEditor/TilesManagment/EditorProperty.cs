using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.TilesEditor.TilesManagment
{
    internal class EditorProperty
    {
        private readonly PropertyInfo _propertyInfo;

        public string Name { get; private set; }


        public EditorProperty(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
            Name = propertyInfo.Name;
        }


        public EditorPropertyUI ShowUI(object target)
        {
            var value = _propertyInfo.GetValue(target);

            switch (value)
            {
                case int:
                    return new EditorIntPropertyUI(target, this);
            }

            return null;
        }

        public T GetValue<T>(object target)
        {
            return (T) _propertyInfo.GetValue(target);
        }

        public void SetValue(object target, object value)
        {
            _propertyInfo.SetValue(target, value);
        }
    }
}

