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

        [SerializeField] private float _followSmoothSpeed;

        [SerializeField] private bool _testMobileInput = false;

        [SerializeField] private float _baseMoveSpeed = 25f;
        [SerializeField] private float _zoomSpeed = 5f;
        [SerializeField] private Vector2 _zoomRange;

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
            // TODO: Split this logic inside cameraInput Interfaces
            // I want to understand differences between pc and mobile before I abstract it
            if (Input.GetMouseButton(0))
            {
                HandleCameraZoomPC();
                HandleCameraMovementPC();
            }
        }

        private void HandleCameraMovementPC()
        {
            var xAxis = -Input.GetAxis("Mouse X");
            var yAxis = -Input.GetAxis("Mouse Y");

            // We need to consider current zoom. When we make a zoom, camera should move slowly.
            var adjustedMoveSpeed = _baseMoveSpeed * (_camera.orthographicSize / _zoomRange.x); // Adjust speed based on zoom

            // Move camera
            Vector3 moveVector = new Vector3(xAxis, 0, yAxis) * adjustedMoveSpeed * Time.deltaTime;

            // Camera could rotated, so we need to properly translate those inputs
            moveVector = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * moveVector;

            // Move target. Camera will follow it
            _target.Translate(moveVector, Space.World);
        }

        private void HandleCameraZoomPC()
        {
            var zoom = Input.GetAxis("Mouse ScrollWheel");

            float newSize = Mathf.Clamp(_camera.orthographicSize - zoom * _zoomSpeed, _zoomRange.x, _zoomRange.y);

            _camera.orthographicSize = newSize;
        }

        private void HandleCameraMovementMobile()
        {
            if (Input.touchCount == 1)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    float xAxis = touch.deltaPosition.x; //* 0.01f;
                    float yAxis = touch.deltaPosition.y; //* 0.01f;

                    // We need to consider current zoom. When we make a zoom, camera should move slowly.
                    var adjustedMoveSpeed = _baseMoveSpeed * (_camera.orthographicSize / _zoomRange.x); // Adjust speed based on zoom

                    // Move camera
                    Vector3 moveVector = new Vector3(xAxis, 0, yAxis) * adjustedMoveSpeed * Time.deltaTime;

                    // Camera could rotated, so we need to properly translate those inputs
                    moveVector = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * moveVector;

                    // Move target. Camera will follow it
                    _target.Translate(moveVector, Space.World);
                }
            }
        }

        private void HandleCameraZoomMobile()
        {
            if (Input.touchCount == 2)
            {
                var touch0 = Input.GetTouch(0);
                var touch1 = Input.GetTouch(1);

                if (touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved)
                {
                    Vector2 prevTouch0Pos = touch0.position - touch0.deltaPosition;
                    Vector2 prevTouch1Pos = touch1.position - touch1.deltaPosition;

                    float prevTouchDeltaMag = (prevTouch0Pos - prevTouch1Pos).magnitude;
                    float touchDeltaMag = (touch0.position - touch1.position).magnitude;

                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                    float newSize = Mathf.Clamp(_camera.orthographicSize + deltaMagnitudeDiff * _zoomSpeed, _zoomRange.x, _zoomRange.y); // *0.01f
                    _camera.orthographicSize = newSize;
                }
            }
        }

        #endregion

        #region Public methods



        #endregion
    }
}