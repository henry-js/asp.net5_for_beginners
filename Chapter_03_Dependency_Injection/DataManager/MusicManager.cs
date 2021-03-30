using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chapter_03_Dependency_Injection.Models;

namespace Chapter_03_Dependency_Injection.DataManager
{
    public interface IMusicManager
    {
        Guid RequestId { get; set; }
        INotifier Notify { get; set; }
        List<SongModel> GetAllMusic();
        List<SongModel> GetAllMusicThenNotify();
    }

    public class MusicManager : IMusicManager
    {
        public INotifier Notify { get; set; }
        public Guid RequestId { get; set; }

        public MusicManager() : this(Guid.NewGuid()) { }

        public MusicManager(Guid requestId)
        {
            RequestId = requestId;
        }

        public List<SongModel> GetAllMusic()
        {
            return new List<SongModel>
            {
                new SongModel { Id = 1, Title = "InterstateLove Song", Artist = "STP",Genre = "Hard Rock" },
                new SongModel { Id = 2, Title = "Man In TheBox", Artist = "Alice In Chains",Genre = "Grunge" },
                new SongModel { Id = 3, Title = "Blind", Artist= "Lifehouse", Genre = "Alternative" },
                new SongModel { Id = 4, Title = "Hey Jude",Artist = "The Beatles", Genre = "Rock n Roll" }
            };
        }

        public List<SongModel> GetAllMusicThenNotify()
        {
            if (Notify != default)
            {
                // invoke notifier method
                Notify.SendMessage("User viewed the music page.");
            }

            // return the list of music
            return GetAllMusic();
        }

        public async Task<int> GetMusicCount()
        {
            return await Task.FromResult(GetAllMusic().Count);
        }
    }
    public class InstrumentalMusicManager
    {
        private readonly IMusicManager _musicManager;

        public Guid RequestId { get; set; }

        public InstrumentalMusicManager(IMusicManager musicManager)
        {
            RequestId = musicManager.RequestId;
        }
    }
}