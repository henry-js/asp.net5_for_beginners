using System.Threading.Tasks;
using EFCore_CodeFirst.DTO;
using EFCore_CodeFirst.DTO.Players;

namespace EFCore_CodeFirst.Interfaces
{
    public interface IPlayerService
    {
        Task CreatePlayerAsync(CreatePlayerRequest playerRequest);
        Task<PagedResponse<GetPlayerResponse>> GetPlayersAsync(UrlQueryParameters urlQueryParameters);
        
    }
}