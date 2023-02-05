using Codebase.Controllers;

namespace Codebase.Data
{
    public class DataService
    {
        private GameData _data;
        private Storage _storage;
        private PlayerService _playerService;

        public GameData Data 
        {
            get
            {
                if (_data == null)
                    Load();
                
                return _data;
            }
        }
        
        public void Load()
        {
            _storage = new Storage();
            _data = _storage.Load();
        }

        public void Save()
        {
            _storage.Save(_data);
        }

    }
}
