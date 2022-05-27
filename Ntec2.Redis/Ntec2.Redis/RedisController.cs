using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntec2.Redis
{
    public class RedisController
    {
        private RedisClient redisClient;

        public RedisController()
        {
            this.redisClient = new RedisClient();
        }

        public string Get(string key)
        {
            return this.redisClient.Get<string>(key);
        }

        public void Set(string key, string value)
        {
             this.redisClient.Set<string>(key,value);
        }

        public void Del(string key)
        {
            this.redisClient.Delete<string>(key);
        }

    }
}
