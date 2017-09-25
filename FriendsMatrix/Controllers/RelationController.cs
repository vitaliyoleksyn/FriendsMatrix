using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using FriendsMatrix.Models;
using FriendsMatrix.ModelsDTO;

namespace FriendsMatrix.Controllers
{
    public class RelationController : ApiController
    {
        // GET: api/Relation
        public IEnumerable<RelationDTO> Get()
        {
            using (DbEntities context = new DbEntities())
            {
                IQueryable<RelationDTO> queryRelation = context.Relations.Select(r => new RelationDTO()
                {
                    Id = r.Id,
                    FriendID = r.FriendId,
                    UserID = r.UserId
                });

                return queryRelation.ToList();
            }
        }

        // POST: api/Relation
        public void Post([FromBody]Relation relation)
        {
            if (relation != null && relation.FriendId != relation.UserId)
            {
                using (DbEntities context = new DbEntities())
                {
                    IQueryable<Relation> queryRelation = context.Relations.Where(r => r.UserId == relation.UserId && r.FriendId == relation.FriendId);

                    var relationNotExists = queryRelation.FirstOrDefault() == null;

                    if (relationNotExists)
                    {
                        context.Relations.AddRange(new List<Relation>(2) { relation, new Relation() { UserId = relation.FriendId, FriendId = relation.UserId } });
                        context.SaveChanges();
                    }
                }
            }
        }

        // DELETE: api/Relation/5/5
        public void Delete(int iD1, int iD2)
        {
            using (DbEntities context = new DbEntities())
            {
                IQueryable<Relation> queryRelation = context.Relations.Where(u => u.UserId == iD1 && u.FriendId == iD2
                || u.UserId == iD2 && u.FriendId == iD1);

                context.Relations.RemoveRange(queryRelation.ToList());
                context.SaveChanges();
            }
        }
    }
}
