using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    class ClipManager:DependencyObject
    {
        public static string GetCornerDirection(DependencyObject obj)
        {
            return (string)obj.GetValue(CornerDirectionProperty);
        }

        public static void SetCornerDirection(DependencyObject obj, string value)
        {
            obj.SetValue(CornerDirectionProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerDirection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerDirectionProperty =
            DependencyProperty.RegisterAttached("CornerDirection", typeof(string), typeof(ClipManager), 
                new PropertyMetadata("",OnDirectionChanged));

        private static void OnDirectionChanged(DependencyObject obj,DependencyPropertyChangedEventArgs e)
        {
            // optimize this code with ClipGeometry and make it works for all FrameWorkElement
            if(obj is Border border)
            {
                if(e.NewValue.ToString() == "horizon")
                {
                    border.CornerRadius = new CornerRadius(border.Height / 2);
                }
                else if(e.NewValue.ToString() == "vertical")
                {
                    border.CornerRadius = new CornerRadius(border.Width / 2);
                }
            }
        }
    }
}
