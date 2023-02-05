using Codebase.ZyphUI.Base;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public class PopupInfo : BaseScreen
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Button okButton;


        public void Init(PopupService popupService, string message, string title)
        {
            titleText.text = title;
            messageText.text = message;
            
            okButton.onClick.AddListener(popupService.ClosePopup);
        }

    }
}