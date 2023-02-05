using DG.Tweening;
using UnityEngine;

namespace Codebase.ZyphUI.Helpers
{
    public class ElementScaller : MonoBehaviour
    {
        [SerializeField] private Vector3 _endVector;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        public void PlayScalle()
        {
            transform.DOScale(_endVector, _duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(_ease)
                .SetUpdate(true);
        }

        public void StopScalle()
        {
            transform.DOKill();
            transform.DOScale(Vector3.one, _duration)
                .SetEase(_ease)
                .SetUpdate(true); ;

        }
    }
}