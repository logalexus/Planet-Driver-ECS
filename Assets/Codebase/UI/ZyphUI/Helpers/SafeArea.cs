using UnityEngine;

namespace Codebase.ZyphUI.Helpers
{
    public class SafeArea
    {
        public static void UpdateSafeArea(RectTransform gameScreen)
        {
            var SafeArea = Screen.safeArea;

            var anchorMin = SafeArea.position;
            var anchorMax = SafeArea.position + SafeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;

            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            gameScreen.anchorMin = anchorMin;
            gameScreen.anchorMax = anchorMax;
        }
    }
}