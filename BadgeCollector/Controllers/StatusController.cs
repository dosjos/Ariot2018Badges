using Models;
using System.Linq;
using System.Web.Http;

namespace BadgeCollector.Controllers
{
    public class StatusController : ApiController
    {
        BadgeContext db = new BadgeContext();

        // GET api/<controller>
        [Route("api/Badges")]
        public int GetBadges()
        {
            return db.Badges.Count();
        }

        // GET api/<controller>/5
        [Route("api/Achieved")]
        public int GetAchieved()
        {
            return db.Badges.Count(x => x.Achieved);
        }

        [Route("api/Points")]
        public int GetPoints()
        {
            return db.Badges.Where(x => x.Achieved).Sum(x => x.Points);
        }


    }
}