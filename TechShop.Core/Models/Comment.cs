using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class Comment
    {
        public Guid? Id { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        [Required]
        public string? CommentText { get; set; }
        [Range(0,5)]
        public int? Rate { get; set; }
        public Guid? UserReplyId { get; set; }
        public Comment? UserReply { get; set; }
        public List<Comment>? UserReplies { get ; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
    }
}
