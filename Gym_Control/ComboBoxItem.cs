using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Control
{
    class ComboBoxItem
    {
        public string Text
        {
            get;set;
        }
        public object value
        {
            get;set;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
