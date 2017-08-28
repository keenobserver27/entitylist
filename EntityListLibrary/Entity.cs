using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityListLibrary
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
