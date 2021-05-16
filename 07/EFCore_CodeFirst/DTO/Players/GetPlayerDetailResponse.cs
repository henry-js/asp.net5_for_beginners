using System;
using System.Collections.Generic;
using EFCore_CodeFirst.DTO.PlayerInstruments;

namespace EFCore_CodeFirst.DTO.Players
{
    public class GetPlayerDetailResponse
    {
        public string NickName { get; set; }
        public DateTime JoinedDate { get; set; }
        public List<GetPlayerInstrumentResponse> PlayerInstruments { get; set; }
    }
}