using System;
using Codebase.ZyphUI.Base;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public class PopupApprove : BaseScreen
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;


        public void Init(PopupService popupService, string message, string title, Action action)
        {
            titleText.text = title;
            messageText.text = message;

            yesButton.onClick.AddListener(() => action?.Invoke());
            yesButton.onClick.AddListener(popupService.ClosePopup);
            noButton.onClick.AddListener(popupService.ClosePopup);
        }
    }
}