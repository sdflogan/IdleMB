using UnityEngine;

namespace TinyBytes.Idle.GameCamera.Touch
{
    public class TapService : MonoBehaviour
    {
        #region Inspector properties

        [SerializeField] private float _tapDurationThreshold = 0.2f;
        [SerializeField] private float _tapDistanceThreshold = 50f;
        [SerializeField] private LayerMask _interactableLayers;

        #endregion

        #region Private properties

        private float _onTouchStartTime;
        private Vector2 _touchPosition;

        private ITouchInput _touchInput;

        #endregion

        #region Unity events

        private void Start()
        {
            InitTouchInput(GameplayCameraService.Instance.TestMobileInput);
        }

        private void Update()
        {
            DetectUserTap();
        }

        #endregion

        #region Private methods

        private void InitTouchInput(bool forceMobileInput)
        {
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                _touchInput = (forceMobileInput ? new TouchInputMobile() : new TouchInputPC());
            }
            else
            {
                _touchInput = new TouchInputMobile();
            }
        }//

        private void DetectUserTap()
        {
            if (_touchInput.IsStartTouching())
            {
                _onTouchStartTime = Time.time;
                _touchPosition = _touchInput.GetTouchPosition();
            }
            else if (_touchInput.IsEndTouching())
            {
                var touchDuration = Time.time - _onTouchStartTime;
                var touchDistance = Vector3.Distance(_touchInput.GetTouchPosition(), _touchPosition);
                Debug.LogError("First: " + _touchPosition + " - Second: " + _touchInput.GetTouchPosition() + " ---- Distance: " + touchDistance);
                if (touchDuration < _tapDurationThreshold || touchDistance < _tapDistanceThreshold)
                {
                    OnTapDone();
                }
            }
        }

        private void OnTapDone()
        {
            Debug.LogError("Tap Done");

            Ray ray = GameplayCameraService.Instance.GameCamera.ScreenPointToRay(_touchPosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _interactableLayers))
            {
                Debug.LogError("Interactable layer found");
            }
        }

        #endregion
    }
}