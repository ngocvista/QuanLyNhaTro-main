using DocumentFormat.OpenXml.Wordprocessing;
using MotelManagement.Business.IService;
using MotelManagement.Core.IRepository;
using MotelManagement.Data.Models;

namespace MotelManagement.Business.Service
{
    public class ArticleService : IArticleService
    {

        private readonly IUnitOfWork _unitOfWork;
        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateArticle(ArticleRoom room)
        {
            await _unitOfWork.articleRespository.AddAsync(room);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteArticle(int id)
        {
            await _unitOfWork.articleRespository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<ArticleRoom> GetDetail(int id)
        {
            return await _unitOfWork.articleRespository.GetByIdAsync(id);
        } 

        public async Task<List<ArticleRoom>> GetListArticleRooms()
        {
           return await _unitOfWork.articleRespository.GetAllAsync();
        }
    }
}
