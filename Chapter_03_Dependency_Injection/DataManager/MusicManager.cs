using System.Collections.Generic;
using Chapter_03_Dependency_Injection.Models;

namespace Chapter_03_Dependency_Injection.DataManager
{
    public interface IMusicManager
    {
        List<SongModel> GetAllMusic();
    }

    public class MusicManager : IMusicManager
    {
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

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}