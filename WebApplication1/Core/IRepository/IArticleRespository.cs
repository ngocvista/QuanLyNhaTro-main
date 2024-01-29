using MotelManagement.Data.Models;

namespace MotelManagement.Core.IRepository
{
    public interface IArticleRespository : IGenericRepository<ArticleRoom>
    {
        public Task CreateArticle(ArticleRoom room);
        public Task<List<ArticleRoom>> GetArticleRooms();

        public Task<bool> DeleteArticle(int id);
    }
}
