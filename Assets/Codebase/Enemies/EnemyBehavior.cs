using System.Collections;
using Codebase.Controllers;
using Codebase.EmtyComponents;
using Codebase.Spawners;
using UnityEngine;

namespace Codebase.Behaviors
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField] private Rigidbody _enemy;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private float _speed = 7f;

        private float _radius = 30f;
        private bool _isMoving = true;
        private bool _isDestroyable = false;
    
        private void Start()
        {
            StartCoroutine(WaitForDestroy());
        }

        private IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(6f);
            _isDestroyable = true;
        }

        private void FixedUpdate()
        {
            if (_isMoving)
                _enemy.MovePosition(_enemy.position + transform.forward * _speed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out EnemyDestroyerMovement destroyer) && _isDestroyable)
                gameObject.SetActive(false);

            if (other.gameObject.TryGetComponent(out Crash crash))
            {
                _isMoving = false;
                _isDestroyable = true;

                if (!Physics.CheckSphere(transform.position, _radius, _mask))
                {
                    gameObject.SetActive(false);
                }
            }
        }


        private void OnEnable()
        {
            _isDestroyable = false;
            _isMoving = true;
            StartCoroutine(WaitForDestroy());
        }
    
    }
}
