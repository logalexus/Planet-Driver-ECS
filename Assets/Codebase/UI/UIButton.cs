using Codebase.Controllers;
using UnityEngine.UI;
using Zenject;

namespace Codebase.UI
{
    public class UIButton : Button
    {
        public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (IsActive() && IsInteractable())
                AudioService.Instance.PlaySFX(AudioService.Instance.Sounds.Click);
            base.OnPointerDown(eventData);
        }
    }
}
