using System;
using System.Collections.Generic;
using shake_A_lunch.std.Models;

namespace shake_A_lunch.std
{
    public class AppStore
    {
        protected AppStore()
        {
            Locations = new List<Location>();
            Rnd = new Random(DateTime.Now.Millisecond);
        }

        static readonly Lazy<AppStore> instance = new Lazy<AppStore>(() => new AppStore());
        public static AppStore Instance => instance.Value;
        public List<Location> Locations { get; set; }
        public Random Rnd { get; set; }
    }
}
