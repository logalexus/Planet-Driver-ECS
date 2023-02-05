using Codebase.Animation;
using Codebase.Controllers;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Codebase.Behaviors
{
    public class CoinBehavior : MonoBehaviour
    {
        [SerializeField] private CoinAnim _coinAnim;

        private GameObject parent;
        private PlayerService _playerService;
        
        [Inject]
        public void Init(PlayerService playerService)
        {
            _playerService = playerService;
        }
        
        private void Start()
        {
            parent = transform.parent.gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out PlayerService cb))
            {
                _playerService.CoinCollecting();
                _coinAnim.DoCollect().OnComplete(() => 
                {
                    _coinAnim.DoDefault();
                    parent.SetActive(false);
                });
            }
        }
    

    }
}
