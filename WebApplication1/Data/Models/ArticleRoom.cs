using System.ComponentModel.DataAnnotations;

namespace MotelManagement.Data.Models
{
    public class ArticleRoom
    {
        [Key]
        public int Articelid {  get; set; }
        public string ?header { get; set; }
        public string ?ImageArticle { get; set; }
        public string ?Content { get; set; }

    }
}
