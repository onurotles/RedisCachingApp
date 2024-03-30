using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RedisCachingApp.Domain.Common
{
    public class BaseEntity
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
