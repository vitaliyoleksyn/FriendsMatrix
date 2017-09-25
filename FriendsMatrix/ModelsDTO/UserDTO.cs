using System;
using System.Collections.Generic;

namespace FriendsMatrix.ModelsDTO
{
    [Serializable]
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<RelationDTO> Relations { get; set; }
        public int? Level { get; set; }
    }
}