using Domain.Database;
using RepoDb.Attributes;

namespace Domain.Resource.Data
{
    public class News : BaseModel
    {
        [Primary]
        public long Id { get; set; }
        public string NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    } 
}
