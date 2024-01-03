
using System;
using System.Collections.Generic;
using TinyBytes.Utils;
using UnityEngine;

namespace TinyBytes.Idle.UI.Popup
{
    public class PopupService : Singleton<PopupService>
    {
        #region Events

        public static event Action OnPopupOpen;
        public static event Action OnPopupClosed;
        public static event Action OnAllPopupsClosed;

        #endregion

        #region Inspector properties

        [SerializeField] private List<PopupUI> _popupPrefabs;
        [SerializeField] private Transform _content;

        #endregion

        #region Private properties

        private List<PopupUI> _instancedPopups;

        #endregion

        #region Unity events

        private void Awake()
        {
            _instancedPopups = new List<PopupUI>();
        }

        #endregion

        #region Public methods

        public T Display<T>(Type popupType) where T : PopupUI
        {
            var popup = _instancedPopups.Find(popup => popup.PopupType == popupType);

            if (popup != null)
            {
                popup.Display();
                return (T) popup;
            }

            var prefab = _popupPrefabs.Find(popup => popup.PopupType == popupType);

            if (prefab != null)
            {
                var generatedPopup = Instantiate(prefab, _content);
                generatedPopup.Display();

                _instancedPopups.Add(generatedPopup);

                return (T) generatedPopup;
            }

            Debug.LogError($"[Popup]: {popupType} not found.");
            return null;
        }

        #endregion
    }
}