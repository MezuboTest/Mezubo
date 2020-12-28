using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.roulette.Repository
{
    interface IRedisConnection
    {
        public string Get(string id);
        public bool Set(string id, string value);
        public List<string> GetAllKeys();
    }
}
