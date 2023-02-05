using System.Collections.Generic;
using Codebase.Audio;
using Codebase.Content.Cars;
using Codebase.Content.Maps;
using Codebase.ECS.Code.Coins;
using Codebase.ECS.Code.Enemies;
using Codebase.UI;
using UnityEngine;

namespace Codebase.Assets
{
    public class AssetsProvider : MonoBehaviour
    {
        [SerializeField] public PlanetHolder planetHolder;
        [SerializeField] private CarsHolder carsHolder;
        [SerializeField] private PlanetUI planetUI;
        [SerializeField] private CarUI carUI;
        [SerializeField] private Sounds sounds;
        [SerializeField] private List<CoinProvider> coins;
        [SerializeField] private List<EnemyProvider> enemies;

        public PlanetHolder PlanetHolder => planetHolder;

        public CarsHolder CarsHolder => carsHolder;

        public PlanetUI PlanetUI => planetUI;

        public CarUI CarUI => carUI;

        public Sounds Sounds => sounds;

        public List<CoinProvider> Coins => coins;

        public List<EnemyProvider> Enemies => enemies;
    }
}