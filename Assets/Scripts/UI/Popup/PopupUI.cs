
using System;
using UnityEngine;

namespace TinyBytes.Idle.UI.Popup
{
    public class PopupUI : MonoBehaviour
    {
        #region Inspector properties

        [SerializeField] private Type _type;
        [SerializeField] private float _maxLifetimeSeconds;

        #endregion

        #region Private properties

        private float _currentLifetimeSeconds;

        private event Action _onCloseCallback;

        #endregion

        #region Public properties

        public Type PopupType => _type;

        #endregion

        #region Public methods

        public void Display()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _onCloseCallback?.Invoke();
        }

        #endregion
    }
}