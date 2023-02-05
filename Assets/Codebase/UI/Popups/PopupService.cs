using System;
using Codebase.ZyphUI.Base;
using UnityEngine;

namespace UI.Popups
{
    public class PopupService : MonoBehaviour
    {
        [SerializeField] private PopupLoading popupLoadingPrefab;
        [SerializeField] private PopupInfo popupInfoPrefab;
        [SerializeField] private PopupApprove popupApprovePrefab;

        private BaseScreen _activePopup;

        public void ShowLoadingPopup()
        {
            ClosePopup();
            _activePopup = Instantiate(popupLoadingPrefab, transform);
            _activePopup.Open();
        }

        public void ShowInfoPopup(string message, string title = "Warning")
        {
            ClosePopup();
            var popup = Instantiate(popupInfoPrefab, transform);
            popup.Init(this, message, title);

            _activePopup = popup;
            _activePopup.Open();
        }

        public void ShowApprovePopup(string message, string title, Action action)
        {
            ClosePopup();
            var popup = Instantiate(popupApprovePrefab, transform);
            popup.Init(this, message, title, action);

            _activePopup = popup;
            _activePopup.Open();
        }

        public void ClosePopup()
        {
            if (_activePopup == null)
                return;
            
            _activePopup.Close();
            Destroy(_activePopup.gameObject, 1f);
        }
    }
}