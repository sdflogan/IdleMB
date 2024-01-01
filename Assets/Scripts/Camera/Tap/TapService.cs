using TinyBytes.Idle.Interactable;
using UnityEngine;

namespace TinyBytes.Idle.GameCamera.Tap
{
    public class TapService : MonoBehaviour
    {
        #region Inspector properties

        [SerializeField] private float _tapDurationThreshold = 0.2f;
        [SerializeField] private float _tapDistanceThreshold = 50f;
        [SerializeField] private LayerMask _interactableLayers;

        #endregion

        #region Private properties

        private float _touchStartTime;
        private Vector2 _touchStartPosition;

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
        }

        private void DetectUserTap()
        {
            if (_touchInput.IsStartTouching())
            {
                _touchStartTime = Time.time;
                _touchStartPosition = _touchInput.GetTouchPosition();
            }
            else if (_touchInput.IsEndTouching())
            {
                var touchDuration = Time.time - _touchStartTime;
                var touchDistance = Vector3.Distance(_touchInput.GetTouchPosition(), _touchStartPosition);
                
                if (touchDuration < _tapDurationThreshold || touchDistance < _tapDistanceThreshold)
                {
                    OnTapDone();
                }
            }
        }

        private void OnTapDone()
        {
            Ray ray = GameplayCameraService.Instance.GameCamera.ScreenPointToRay(_touchStartPosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 500f, _interactableLayers))
            {
                var interactableComponent = hit.collider.gameObject.GetComponent<IInteractable>();

                interactableComponent?.Interact();
            }
        }

        #endregion
    }
}