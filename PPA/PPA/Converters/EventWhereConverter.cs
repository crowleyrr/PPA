using PPA.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace PPA.Converters
{
    /*
     * https://github.com/ME-MarvinE/XCalendar
     * 
     * MIT License

        Copyright (c) 2022 MarvinE

        Permission is hereby granted, free of charge, to any person obtaining a copy
        of this software and associated documentation files (the "Software"), to deal
        in the Software without restriction, including without limitation the rights
        to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
        copies of the Software, and to permit persons to whom the Software is
        furnished to do so, subject to the following conditions:

        The above copyright notice and this permission notice shall be included in all
        copies or substantial portions of the Software.

        THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
        IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
        FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
        AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
        LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
        OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
        SOFTWARE.
     * 
     */
    public class EventWhereConverter : BindableObject, IValueConverter
    {
        public IEnumerable<Event> Items
        {
            get { return (IEnumerable<Event>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
        public bool? WhiteList
        {
            get { return (bool?)GetValue(WhiteListProperty); }
            set { SetValue(WhiteListProperty, value); }
        }
        public bool UseDateComponent
        {
            get { return (bool)GetValue(UseDateComponentProperty); }
            set { SetValue(UseDateComponentProperty, value); }
        }
        public bool UseTimeComponent
        {
            get { return (bool)GetValue(UseTimeComponentProperty); }
            set { SetValue(UseTimeComponentProperty, value); }
        }
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(IEnumerable<Event>), typeof(EventWhereConverter));
        public static readonly BindableProperty WhiteListProperty = BindableProperty.Create(nameof(WhiteList), typeof(bool?), typeof(EventWhereConverter), null);
        public static readonly BindableProperty UseDateComponentProperty = BindableProperty.Create(nameof(UseDateComponent), typeof(bool), typeof(EventWhereConverter), true);
        public static readonly BindableProperty UseTimeComponentProperty = BindableProperty.Create(nameof(UseTimeComponent), typeof(bool), typeof(EventWhereConverter), true);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? BindingValue = (DateTime?)value;

            if (Items == null) { return Items; }

            List<Event> ItemsList = new List<Event>(Items);

            if (WhiteList == true)
            {
                if (UseDateComponent && UseTimeComponent) { return ItemsList.Where(x => x.DateTime == BindingValue); }
                else if (UseDateComponent) { return ItemsList.Where(x => x?.DateTime.Date == BindingValue?.Date); }
                else if (UseTimeComponent) { return ItemsList.Where(x => x?.DateTime.TimeOfDay == BindingValue?.TimeOfDay); }
            }
            else if (WhiteList == false)
            {
                if (UseDateComponent && UseTimeComponent) { return ItemsList.Where(x => x.DateTime != BindingValue); }
                else if (UseDateComponent) { return ItemsList.Where(x => x?.DateTime.Date != BindingValue?.Date); }
                else if (UseTimeComponent) { return ItemsList.Where(x => x?.DateTime.TimeOfDay != BindingValue?.TimeOfDay); }
            }
            else
            {
                if (UseDateComponent && UseTimeComponent) { return ItemsList; }
                else if (UseDateComponent) { return ItemsList.Select(x => x?.DateTime.Date); }
                else if (UseTimeComponent) { return ItemsList.Select(x => x?.DateTime.TimeOfDay); }
            }
            return new List<DateTime>();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }
        
    }
}
