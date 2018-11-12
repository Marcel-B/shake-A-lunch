using System;
namespace shake_A_lunch.std.Models
{
    public class Location : IComparable
    {

        public string Name { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public short Stars { get; set; }

        public long Count { get; set; }

        public long Accepted { get; set; }

        public bool Active { get; set; }

        public DateTimeOffset Created { get; set; }

        public int CompareTo(object obj)
        {
            var s = (Location)obj;
            if (s.Accepted > Accepted) return 1;
            else if (s.Accepted < Accepted) return -1;
            return 0;
        }
    }
}
