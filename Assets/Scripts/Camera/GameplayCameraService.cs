/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using TinyBytes.Idle.GameCamera.Input;
using UnityEngine;

namespace TinyBytes.Idle.GameCamera
{
	public class GameplayCameraService : MonoBehaviour
	{
        #region Events



        #endregion

        #region Inspector properties

        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _target;

        [SerializeField] private float _followSmoothSpeed;

        [SerializeField] private bool _testMobileInput = false;

        #endregion

        #region Private properties

        private ICameraInput _cameraInput;

        #endregion

        #region Public properties



        #endregion

        #region Unity Events

        private void Awake()
        {
            InitCameraInput();
        }

        private void Update()
        {
            TouchHandler();
        }

        private void LateUpdate()
        {
            FollowTarget();
        }

        #endregion

        #region Private methods

        private void FollowTarget()
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _target.position, _followSmoothSpeed * Time.deltaTime);
        }

        private void InitCameraInput()
        {
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                _cameraInput = (_testMobileInput ? new CameraInputMobile() : new CameraInputPC());
            }
            else
            {
                _cameraInput = new CameraInputMobile();
            }
        }

        private void TouchHandler()
        {

        }

        #endregion

        #region Public methods



        #endregion
    }
}