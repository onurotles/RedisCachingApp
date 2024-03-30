using RedisCachingApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCachingApp.Domain.Entities
{
    public class Driver : BaseEntity
    {
        public string Name { get; set; }
        public int DriveNo { get; set; }
    }
}
