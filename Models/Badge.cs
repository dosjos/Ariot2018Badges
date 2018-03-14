using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Badge
    {
        public int BadgeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public string ImageUrl { get; set; }
        public bool Achieved { get; set; }
    }
}
