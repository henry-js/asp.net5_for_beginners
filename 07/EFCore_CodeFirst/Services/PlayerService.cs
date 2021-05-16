using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore_CodeFirst.Db;
using EFCore_CodeFirst.Db.Models;
using EFCore_CodeFirst.DTO;
using EFCore_CodeFirst.DTO.PlayerInstruments;
using EFCore_CodeFirst.DTO.Players;
using EFCore_CodeFirst.Extensions;
using EFCore_CodeFirst.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore_CodeFirst.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly CodeFirstDemoContext _dbContext;
        public PlayerService(CodeFirstDemoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreatePlayerAsync(CreatePlayerRequest playerRequest)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var player = new Player
                {
                NickName = playerRequest.NickName,
                JoinedDate = DateTime.Now,
                };

                await _dbContext.Players.AddAsync(player);
                await _dbContext.SaveChangesAsync();

                var playerId = player.PlayerId;
                var playerInstruments = new List<PlayerInstrument>();
                var instrumentTypes = _dbContext.InstrumentTypes.Select(i => i.InstrumentTypeId);

                foreach (var instrument in playerRequest.PlayerInstruments)
                {
                    playerInstruments.Add(new PlayerInstrument
                    {
                        PlayerId = playerId,
                        InstrumentTypeId = instrument.InstrumentTypeId,
                        ModelName = instrument.ModelName,
                        Level = instrument.Level
                    });
                }

                _dbContext.PlayerInstruments.AddRange(playerInstruments);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PagedResponse<GetPlayerResponse>> GetPlayersAsync(UrlQueryParameters parameters)
        {
            var query = await _dbContext.Players
                                .AsNoTracking()
                                .Include(p => p.Instruments)
                                .PaginateAsync(parameters.PageNumber, parameters.PageSize);
            
            return new PagedResponse<GetPlayerResponse>
            {
                PageCount = query.PageCount,
                CurrentPageNumber = query.CurrentPageNumber,
                PageSize = query.PageSize,
                TotalRecordCount = query.TotalRecordCount,
                Result = query.Result.Select(p => new GetPlayerResponse
                {
                    PlayerId = p.PlayerId,
                    NickName = p.NickName,
                    JoinedDate = p.JoinedDate,
                    InstrumentSubmittedCount = p.Instruments.Count
                }).ToList()
            };
        }

        public async Task<GetPlayerDetailResponse> GetPlayerDetailAsync(int id)
        {
            var player = await _dbContext.Players.FindAsync(id);
            // implement null validation check

            var instruments = await
                (from pi in _dbContext.PlayerInstruments
                 join it in _dbContext.InstrumentTypes
                    on pi.InstrumentTypeId equals
                    it.InstrumentTypeId
                where pi.PlayerId.Equals(id)
                select new GetPlayerInstrumentResponse
                {
                    InstrumentTypeName = it.Name,
                    ModelName = pi.ModelName,
                    Level = pi.Level
                }).ToListAsync();
            
            return new GetPlayerDetailResponse
            {
                NickName = player.NickName,
                JoinedDate = player.JoinedDate,
                PlayerInstruments = instruments
            };
        }

        public async Task<bool> UpdatePlayerAsync(int id, UpdatePlayerRequest playerRequest)
        {
            
        }
    }
}