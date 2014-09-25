using System.ComponentModel;
using System.Windows.Data;

namespace Rabid.SaverSetter
{
    class RegistryScriptBinding : MultiBinding
    {
        private RegistryScriptBinding() { }

        /// <summary>
        /// Create the special multibinding for SaverMenu registry script
        /// </summary>
        /// <param name="pMenu"></param>
        public static void CreateRegistryScriptBinding(SaverMenu pMenu)
        {
            RegistryScriptBinding qMulti = new RegistryScriptBinding();
            foreach (DependencyPropertyDescriptor zDPD in pMenu.MenuOptions)
            {
                //      New Binding with path from property name
                Binding zBinding = new Binding(zDPD.Name);
                //      Source will be the instance
                zBinding.Source = pMenu;
                //      Add to collection
                qMulti.Bindings.Add(zBinding);
            }
            qMulti.Converter = new RegistryScriptConverter();
            qMulti.ConverterParameter = pMenu;
            pMenu.SetBinding(SaverMenu.RegistryScriptProperty, qMulti);
        }
    }
}
