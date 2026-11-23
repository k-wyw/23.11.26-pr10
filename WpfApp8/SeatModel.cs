using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp8
{
    public class SeatModel
    {
        public int Row { get; set; }
        public int SeatNumber { get; set; }
        public bool IsBooked { get; set; }
        public bool IsSelected { get; set; }

        public Brush ButtonColor
        {
            get
            {
                if (IsBooked) return Brushes.DarkRed;     // Занято
                if (IsSelected) return Brushes.LightGreen; // Выбрано
                return Brushes.LightGray;                 // Свободно
            }
        }
    }
}