using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using FriendsMatrix.Models;
using FriendsMatrix.ModelsDTO;
using System.Threading.Tasks;

namespace FriendsMatrix.Controllers
{
	// I didn't create business and db layer
	// and didn't use dependency injection
	//Just push all logic into controller to speed up with this task.
	//That's why I have doubt whether it was well done
	
	
    public class UserController : ApiController
    {
        //GET: api/User
        public IEnumerable<UserDTO> Get()
        {
            using (DbEntities context = new DbEntities())
            {
                IQueryable<UserDTO> userQuery = context.Users.Select(u => new UserDTO()
                {
                    Id = u.Id,
                    Name = u.Name
                });
                return userQuery.ToList();
            }
        }

        // GET: api/User/5
        [Route("api/User/{id}")]
        public UserDTO Get(int id)
        {
            using (DbEntities context = new DbEntities())
            {
                IQueryable<UserDTO> userQuery = context.Users.Include("Relation")
                                                             .Where(u => u.Id == id).Select(u => new UserDTO()
                                                             {
                                                                 Id = u.Id,
                                                                 Name = u.Name,
                                                                 Relations = u.Relations.Select(r => new RelationDTO() { Id = r.Id, UserID = r.UserId, FriendID = r.FriendId })
                                                             });

                return userQuery.FirstOrDefault();
            }
        }

        // GET: api/User/5/7
        [Route("api/User/{id}/{level}")]
        public async Task<List<UserDTO>> Get(int id, int level)
        {
            using (DbEntities context = new DbEntities())
            {
				//This may take some time because of 
				//sql procedure was written recursively (SqlScripts/CreateAndInitDataBase.sql)
				//This is not good, but to accelerate the task
				return await Task.Run(() =>
                {
                    return context.GetFriendsLevels(level, id).Select(i => new UserDTO() { Id = i.Id, Level = i.Level, Name = i.Name }).ToList();
                });
            }
        }

        // POST: api/User
        public void Post([FromBody]User user)
        {
            bool isValid = user != null && (user.Name != null || user.Name != string.Empty);

            if (isValid)
            {
                using (DbEntities context = new DbEntities())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
        }
    }
}
