using System.ComponentModel.DataAnnotations;
namespace WebApplication2.Models
{
    public class CreateTodoDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Название задачи обязательно")]
        [StringLength(100, ErrorMessage = "Название не должно быть больше 100 символов")]
        public required string Title { get; set; }
        public bool IsCompleted { get; set; }
    }


    public class UpdateTodoDto
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
