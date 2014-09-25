using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Rabid.Controls
{
    public partial class NumericUpDown : UserControl
    {
        public static readonly DependencyProperty ValueProperty;
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty;
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty;
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty BigChangeProperty;
        public double BigChange
        {
            get { return (double)GetValue(BigChangeProperty); }
            set { SetValue(BigChangeProperty, value); }
        }

        public static readonly DependencyProperty SmallChangeProperty;
        public double SmallChange
        {
            get { return (double)GetValue(SmallChangeProperty); }
            set { SetValue(SmallChangeProperty, value); }
        }

        static NumericUpDown()
        {
            ValueProperty = DependencyProperty.Register(
                "Value"
                , typeof(double)
                , typeof(NumericUpDown)
                );
            MaximumProperty = DependencyProperty.Register(
                "Maximum"
                , typeof(double)
                , typeof(NumericUpDown)
                , new PropertyMetadata(
                    SystemParameters.VirtualScreenWidth)
                );
            MinimumProperty = DependencyProperty.Register(
                "Minimum"
                , typeof(double)
                , typeof(NumericUpDown)
                , new PropertyMetadata(0d)
                );
            SmallChangeProperty = DependencyProperty.Register(
                "SmallChange"
                , typeof(double)
                , typeof(NumericUpDown)
                , new PropertyMetadata(1d)
                );
            BigChangeProperty = DependencyProperty.Register(
                "BigChange"
                , typeof(double)
                , typeof(NumericUpDown)
                , new PropertyMetadata(10d)
                );
        }

        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
                "ValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Double>), typeof(NumericUpDown));
        public event RoutedPropertyChangedEventHandler<Double> ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }
        void RaiseValueChangedEvent(Object pSender, RoutedPropertyChangedEventArgs<Double> pArgs)
        {
            RoutedPropertyChangedEventArgs<Double> qArgs = new RoutedPropertyChangedEventArgs<Double>((Double)pArgs.OldValue, (Double)pArgs.NewValue, NumericUpDown.ValueChangedEvent);
            qArgs.Source = this;
            RaiseEvent(qArgs);
        }

        public NumericUpDown()
        {
            this.InitializeComponent();
            this.Loaded += new RoutedEventHandler(NumericUpDown_Loaded);
        }

        void NumericUpDown_Loaded(object sender, RoutedEventArgs e)
        {
            //      Expose ValueChanged from ScrollBar
            ScrollBar qBar = (ScrollBar)this.Template.FindName("_scrollBar", this);
            qBar.ValueChanged += new RoutedPropertyChangedEventHandler<double>(RaiseValueChangedEvent);
        }

        private void SelectTextBoxText(object sender, RoutedEventArgs e)
        {
            TextBox qBox = (TextBox)this.Template.FindName("_textBox", this);
            if (!qBox.IsFocused) qBox.Focus();
            qBox.SelectAll();
        }
    }
}