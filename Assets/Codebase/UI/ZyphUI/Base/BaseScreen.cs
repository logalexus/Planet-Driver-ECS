using UnityEngine;
using Zenject;

namespace Codebase.ZyphUI.Base
{
    [RequireComponent(typeof(UnityEngine.Animation))]
    public abstract class BaseScreen : MonoBehaviour, IInitializable, IScreen
    {
        [Header("Animation")]
        [SerializeField] protected bool isAnimated = false;
        [SerializeField] protected AnimationClip openClip;
        [SerializeField] protected AnimationClip closeClip;
        [SerializeField] protected bool isTopLayer = false;

        protected UnityEngine.Animation _animation;

        public bool IsTopLayer => isTopLayer;


        public virtual void Initialize()
        {
            if (isAnimated)
            {
                _animation = GetComponent<UnityEngine.Animation>();
                _animation.AddClip(openClip, openClip.name);
                _animation.AddClip(closeClip, closeClip.name);
                openClip.legacy = true;
                closeClip.legacy = true;
            }

            Close();
        }   

        public virtual void Open()
        {
            if(isAnimated)
                _animation.Play(openClip.name);
            else
                gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            if (isAnimated)
                _animation.Play(closeClip.name);
            else
                gameObject.SetActive(false);
        }
    }
}