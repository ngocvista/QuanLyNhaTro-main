using MotelManagement.Data.Models;

namespace MotelManagement.Business.IService
{
    public interface IArticleService
    {
         Task CreateArticle(ArticleRoom room);
         Task<List<ArticleRoom>> GetListArticleRooms();

         Task<bool> DeleteArticle(int id);
         Task<ArticleRoom> GetDetail(int id);
    }
}
