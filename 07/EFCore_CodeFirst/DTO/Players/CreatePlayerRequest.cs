using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EFCore_CodeFirst.DTO.PlayerInstruments;

namespace EFCore_CodeFirst.DTO.Players
{
    public class CreatePlayerRequest
    {
        [Required] public string NickName { get; set; }
        [Required] public List<CreatePlayerInstrumentRequest> PlayerInstruments { get; set; }
    }
}