using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WEngine.Scripts.GameLogic.TilesEditor.TilesManagment
{
    internal class EditorStringProperty
    {
        private readonly PropertyInfo _propertyInfo;

        public override string Name => _propertyInfo.Name;


        public EditorStringProperty(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }
    }
}
