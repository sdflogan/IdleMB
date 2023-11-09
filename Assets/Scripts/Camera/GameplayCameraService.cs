/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using TinyBytes.Idle.GameCamera.PlatformInput;
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

        [Space]

        [SerializeField] private bool _testMobileInput = false;

        [Header("Config")]

        [SerializeField] private float _followSmoothSpeed;
        [SerializeField] private Vector2 _zoomRange;

        [Header("Movement")]

        [SerializeField] private float _baseMoveSpeed = 1f;
        [SerializeField] private float _pcMoveSpeedMultiplier = 50;
        [SerializeField] private float _mobileMoveSpeedMultiplier = 0.5f;

        [Header("Zoom")]

        [SerializeField] private float _zoomSpeed = 1f;
        [SerializeField] private float _pcZoomSpeedMultiplier = 5;
        [SerializeField] private float _mobileZoomSpeedMultiplier = 0.05f;

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
                _cameraInput = (_testMobileInput ? 
                    new CameraInputMobile(_mobileMoveSpeedMultiplier, _mobileZoomSpeedMultiplier) : 
                    new CameraInputPC(_pcMoveSpeedMultiplier, _pcZoomSpeedMultiplier));
            }
            else
            {
                _cameraInput = new CameraInputMobile(_mobileMoveSpeedMultiplier, _mobileZoomSpeedMultiplier);
            }
        }

        private void TouchHandler()
        {
            HandleCameraZoom();
            HandleCameraMovement();
        }

        private void HandleCameraMovement()
        {
            if (_cameraInput.IsMoving())
            {
                var axis = _cameraInput.GetAxisXY();
                var currentZoomSpeedMultiplier = (_camera.orthographicSize / _zoomRange.x);
                var moveSpeed = _cameraInput.CalculateMoveSpeed(_baseMoveSpeed, currentZoomSpeedMultiplier);

                // Move camera
                Vector3 moveVector = new Vector3(axis.x, 0, axis.y) * moveSpeed * Time.deltaTime;

                // Camera could rotated, so we need to properly translate those inputs
                moveVector = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * moveVector;

                // Move target. Camera will follow it
                _target.Translate(moveVector, Space.World);
            }
        }

        private void HandleCameraZoom()
        {
            if (_cameraInput.IsZooming())
            {
                var zoomMagnitude = _cameraInput.CalculateZoomMagnitude();
                var zoomSize = _cameraInput.CalculateZoom(zoomMagnitude, _zoomSpeed, _camera.orthographicSize, _zoomRange);
                _camera.orthographicSize = zoomSize;
            }
        }

        #endregion

        #region Public methods



        #endregion
    }
}