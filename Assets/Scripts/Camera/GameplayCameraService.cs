/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using TinyBytes.Idle.GameCamera.PlatformInput;
using TinyBytes.Utils;
using UnityEngine;

namespace TinyBytes.Idle.GameCamera
{
	public class GameplayCameraService : Singleton<GameplayCameraService>
	{
        #region Events

        public static System.Action<Vector3> OnInstantMovement;

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
        [SerializeField] private float _followAutomaticSpeed = 4;

        [Header("Zoom")]

        [SerializeField] private float _zoomSpeed = 1f;
        [SerializeField] private float _pcZoomSpeedMultiplier = 5;
        [SerializeField] private float _mobileZoomSpeedMultiplier = 0.05f;

        #endregion

        #region Private properties

        private ICameraInput _cameraInput;
        private bool _automaticMovementEnabled = false;
        private float _automaticMovementThreshold = 0.1f;

        #endregion

        #region Public properties

        public Camera GameCamera => _camera;
        public bool TestMobileInput => _testMobileInput;

        #endregion

        #region Unity Events

        private void Awake()
        {
            OnInstantMovement += StartAutomaticMovement;

            InitCameraInput();
        }

		private void OnDestroy()
		{
            OnInstantMovement -= StartAutomaticMovement;
        }

		private void Update()
        {
            if (_automaticMovementEnabled) return;

            TouchHandler();
        }

        private void LateUpdate()
        {
            if (_automaticMovementEnabled)
			{
                FollowAutomaticMovement();

                return;
			}

            FollowTarget();
        }

        #endregion

        #region Private methods

        private void FollowTarget()
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _target.position, _followSmoothSpeed * Time.deltaTime);
        }

        private void FollowAutomaticMovement()
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _target.position, _followAutomaticSpeed * Time.deltaTime);

            // Check end movement
            var distance = (_camera.transform.position - _target.position).magnitude;

            if (distance > _automaticMovementThreshold) return;

            StopAutomaticMovement();
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

        private void StartAutomaticMovement(Vector3 position)
		{
            position.y = _target.position.y;

            _target.position = position;

            var maxZoomOut = _zoomRange.y;

            _camera.orthographicSize = maxZoomOut;

            _automaticMovementEnabled = true;
		}

        private void StopAutomaticMovement()
		{
            _automaticMovementEnabled = false;
		}

        #endregion

        #region Public methods



        #endregion
    }
}