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

        public string Name { get; set; }


        public EditorProperty(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }


        public EditorPropertyUI ShowUI(object target)
        {
            var value = _propertyInfo.GetValue(target);

            switch (value)
            {
                case int:
                    return new EditorIntPropertyUI();
                    

                case string strValue:
                    Console.WriteLine($"[String Field] {Name} = \"{strValue}\"");
                    // render textbox here
                    break;

                case bool boolValue:
                    Console.WriteLine($"[Checkbox] {Name} = {boolValue}");
                    // render checkbox here
                    break;

                default:
                    Console.WriteLine($"[Unsupported] {Name} ({typeof(T).Name})");
                    break;
            }
        }
    }
}

