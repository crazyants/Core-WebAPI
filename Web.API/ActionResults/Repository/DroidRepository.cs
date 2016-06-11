﻿using System.Collections.Generic;
using System.Linq;

namespace ActionResults.Repository
{
    public class DroidRepository : IDroidRepository
    {
        private static Dictionary<string, Droid> repo { get; set; }
        private static int id = 0;
        public DroidRepository()
        {
            repo = new Dictionary<string, Droid>();

            var ig88 = new Droid
            {
                Id = id++,
                Name = "IG-88",
                ProductSeries = "IG-86",
                Armaments = new List<string> { "Vibroblades", "Heavy pulse cannon" }
            };

            repo.Add(ig88.Name, ig88);

        }

        //public bool Exists(string name)
        //{
        //    return repo.ContainsKey(name);
        //}

        public bool Delete(string name)
        {
            return repo.Remove(name);
        }

        public Droid Get(string name)
        {
            Droid droid;
            repo.TryGetValue(name, out droid);
            return droid;
        }

        public bool Put(Droid newDroid)
        {
            Droid droid = null;
            repo.TryGetValue(newDroid.Name, out droid);
            if (droid != null)
            {
                return false;
            }

            newDroid.Id = id++;
            repo.Add(newDroid.Name, newDroid);

            return true;
        }

        public Droid Update(Droid droid)
        {
            if (repo.ContainsKey(droid.Name))
            {
                repo[droid.Name] = droid;
                return droid;
            }
            return null;
        }
    }

}
