using ECS.Code.Planets;
using UnityEngine;

namespace Codebase.Content.Maps
{
    [System.Serializable]
    public class PlanetModel : Content
    {
        public PlanetProvider Prefab;
        public Material SkyBox;
        public int TargetLevel;
    

    }
}