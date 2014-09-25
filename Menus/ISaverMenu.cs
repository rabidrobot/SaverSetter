using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.Win32;
namespace Rabid.SaverSetter
{
    interface ISaverMenu
    {
        IEnumerable<DependencyPropertyDescriptor> MenuOptions { get; }
        RegistryKey RegKey { get; }
        String SaverName { get; }
        String SaverFile { get; }
        StringBuilder RegistryScript { get; }
        Boolean Changes { get; set; }
        void LoadSaverSettings();
        void WriteSaverSettings();
        void UnsetSaverSettings();
        void DefaultSaverSettings();
    }
}
