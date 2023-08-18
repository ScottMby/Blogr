using System.ComponentModel.DataAnnotations;

namespace Blogr.Server.Models
{
    public class Image
    {
        [Key]
        public int i_Id { get; set; }

        public string i_Path { get; set; } = null!;

    }
}
