/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using UnityEngine;

namespace TinyBytes.Idle.GameCamera
{
	public class BillboardBehaviour : MonoBehaviour
	{
        #region Events



        #endregion

        #region Inspector properties

        [SerializeField] private Canvas _canvas;

		#endregion

		#region Private properties

		private Camera _playerCamera;

        #endregion

        #region Public properties



        #endregion

        #region Unity Events

        private void Awake()
        {
            _playerCamera = Camera.main;
            _canvas.worldCamera = _playerCamera;
        }

        private void Update()
        {
            if (_playerCamera != null)
            {
                transform.LookAt(_playerCamera.transform);
            }
        }

        #endregion

        #region Private methods



        #endregion

        #region Public methods



        #endregion
    }
}