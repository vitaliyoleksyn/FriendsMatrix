using System;

namespace FriendsMatrix.ModelsDTO
{
    [Serializable]
    public class RelationDTO
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int FriendID { get; set; }
    }
}