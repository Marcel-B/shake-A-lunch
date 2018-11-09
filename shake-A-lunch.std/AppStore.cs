using System;
using System.Collections.Generic;

namespace shake_A_lunch.std
{
    public class AppStore
    {
        protected AppStore()
        {
            Locations = new List<string>();
            Rnd = new Random(DateTime.Now.Millisecond);
        }

        static readonly Lazy<AppStore> instance = new Lazy<AppStore>(() => new AppStore());
        public static AppStore Instance => instance.Value;
        public IList<string> Locations { get; set; }
        public Random Rnd { get; set; }
    }
}
