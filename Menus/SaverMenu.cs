using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Rabid.SaverSetter
{
    public class SaverMenu : UserControl, Rabid.SaverSetter.ISaverMenu
    {
        #region Properties

        private RegistryKey __RegKey;
        public RegistryKey RegKey
        { get { return __RegKey; } protected set { __RegKey = value; } }

        private String __SaverName;
        public String SaverName
        { get { return __SaverName; } protected set { __SaverName = value; } }

        private String __SaverFile;
        public String SaverFile
        { get { return __SaverFile; } protected set { __SaverFile = value; } }

        public StringBuilder RegistryScript
        {
            get { return (StringBuilder)GetValue(RegistryScriptProperty); }
            private set { SetValue(RegistryScriptProperty, value); }
        }
        public static readonly DependencyProperty RegistryScriptProperty =
            DependencyProperty.Register("RegistryScript", typeof(StringBuilder), typeof(SaverMenu));

        public Boolean Changes
        {
            get { return (Boolean)GetValue(ChangesProperty); }
            set { SetValue(ChangesProperty, value); }
        }
        public static readonly DependencyProperty ChangesProperty =
            DependencyProperty.Register("Changes", typeof(Boolean), typeof(SaverMenu), new UIPropertyMetadata(false));

        protected static void SaverPropertyChanged(DependencyObject pObject, DependencyPropertyChangedEventArgs pArgs)
        {
            SaverMenu qMenu = pObject as SaverMenu;
            if (qMenu != null)
            {
                if (!qMenu.Changes.Equals(true))
                { qMenu.Changes = true; }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<DependencyPropertyDescriptor> MenuOptions
        {
            get
            {
                //      Use BindingFlags enumeration to select Reflection.PropertyInfo for each
                //      public property declared on the instance level, by Name
                IEnumerable<String> qNames = from PropertyInfo lPI in this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                                             select lPI.Name;
                //      From those Properties get ComponentModel.PropertyDescriptors for each 
                IEnumerable<PropertyDescriptor> qProperties = TypeDescriptor.GetProperties(this, new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.All) }).OfType<PropertyDescriptor>().Where(lPD => qNames.Contains(lPD.Name));
                List<DependencyPropertyDescriptor> qDependencyProperties = new List<DependencyPropertyDescriptor>();
                foreach (PropertyDescriptor zPD in qProperties)
                {
                    DependencyPropertyDescriptor zDPD = DependencyPropertyDescriptor.FromProperty(zPD);
                    if (zDPD != null)
                    { qDependencyProperties.Add(zDPD); }
                }
                return (IEnumerable<DependencyPropertyDescriptor>)qDependencyProperties;
            }
        }

        #endregion

        #region RegistryUpdated Event

        public static readonly RoutedEvent RegistryUpdatedEvent = EventManager.RegisterRoutedEvent(
            "RegistryUpdated", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SaverMenu));
        public event RoutedEventHandler RegistryUpdated
        {
            add { AddHandler(RegistryUpdatedEvent, value); }
            remove { RemoveHandler(RegistryUpdatedEvent, value); }
        }
        void RaiseRegistryUpdatedEvent()
        {
            RoutedEventArgs qArgs = new RoutedEventArgs(SaverMenu.RegistryUpdatedEvent, this);
            RaiseEvent(qArgs); 
        }

        #endregion
        
        #region Registry Access Methods

        public void UnsetSaverSettings()
        {
            foreach (String zName in this.RegKey.GetValueNames())
            {
                this.RegKey.DeleteValue(zName);
            }
            this.Changes = false;
            this.RaiseRegistryUpdatedEvent();
        }
        public void WriteSaverSettings()
        {
            foreach (DependencyPropertyDescriptor zDPD in this.MenuOptions)
            {
                if (zDPD.PropertyType.Equals(typeof(Boolean)))
                { this.RegKey.SetValue(zDPD.Name, (Boolean)zDPD.GetValue(this) ? 1 : 0); }
                else if (zDPD.PropertyType.Equals(typeof(Int32)))
                { this.RegKey.SetValue(zDPD.Name, zDPD.GetValue(this)); }
            }
            this.Changes = false;
            this.RaiseRegistryUpdatedEvent();
        }
        public void DefaultSaverSettings()
        {
            foreach (DependencyPropertyDescriptor zDPD in this.MenuOptions)
            {
                zDPD.SetValue(this, zDPD.Metadata.DefaultValue);
            }
            WriteSaverSettings();
        }
        public void LoadSaverSettings()
        {
            //if (this.RegKey.ValueCount == 0) { return; }
            foreach (DependencyPropertyDescriptor zDPD in this.MenuOptions)
            {
                if (!this.RegKey.GetValueNames().Contains(zDPD.Name))
                { continue; }
                else if (zDPD.PropertyType.Equals(typeof(Boolean)))
                { zDPD.SetValue(this, ((Int32)this.RegKey.GetValue(zDPD.Name) == 1) ? true : false); }
                else if (zDPD.PropertyType.Equals(typeof(Int32)))
                { zDPD.SetValue(this, this.RegKey.GetValue(zDPD.Name) ?? zDPD.Metadata.DefaultValue); }
            }
            this.Changes = false;
        }

        #endregion
    }
}
