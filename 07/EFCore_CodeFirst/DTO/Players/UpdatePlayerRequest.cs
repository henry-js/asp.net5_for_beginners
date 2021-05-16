using System.ComponentModel.DataAnnotations;

namespace EFCore_CodeFirst.DTO.Players
{
    public class UpdatePlayerRequest
    {
        [Required]
        public string NickName { get; set; }
    }
}