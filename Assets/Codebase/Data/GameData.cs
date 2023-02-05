using System;
using System.Collections.Generic;
using Data.Models;

namespace Codebase
{
    [Serializable]
    public class GameData
    {
        public UserData UserData;
        public ProgressData ProgressData;
        public SettingsData SettingsData;
        public List<string> AvailablePlanetsData;
        public List<string> AvailableCarsData;
        public string CurrentPlanet;
        public string CurrentCar;

        public GameData()
        {
            UserData = new UserData();
            ProgressData = new ProgressData();
            SettingsData = new SettingsData();
            AvailablePlanetsData = new List<string>();
            AvailableCarsData = new List<string>();
        }
    }
}
