using System;
using System.Windows;
using System.Windows.Controls;

namespace Rabid
{
    /// <summary>
    /// Interaction logic for AnnotatedScaledSlider.xaml
    /// 
    /// </summary>
    public partial class AnnotatedScaledSlider : UserControl
    {

        #region Properties

        public Double Value
        {
            get { return (Double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Double), typeof(AnnotatedScaledSlider), new UIPropertyMetadata(0.0));

        public Double Maximum
        {
            get { return (Double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(Double), typeof(AnnotatedScaledSlider), new UIPropertyMetadata(0.0));

        public Double Minimum
        {
            get { return (Double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(Double), typeof(AnnotatedScaledSlider), new UIPropertyMetadata(0.0));

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(String), typeof(AnnotatedScaledSlider), new UIPropertyMetadata(String.Empty));

        #endregion
        
        public AnnotatedScaledSlider()
        {
            InitializeComponent();
        }
    }
}
