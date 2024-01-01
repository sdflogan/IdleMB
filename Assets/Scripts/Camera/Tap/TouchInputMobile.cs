
using UnityEngine;

namespace TinyBytes.Idle.GameCamera.Tap
{
    public class TouchInputMobile : ITouchInput
    {
        private int _moveCount = 0;
        private UnityEngine.Touch _currentTouch;

        public Vector2 GetTouchPosition()
        {
            return (Input.touchCount > 0 ? Input.GetTouch(0).position : Vector2.zero);
        }

        public bool IsStartTouching()
        {
            return (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began);
        }

        public bool IsEndTouching()
        {
            return (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended);
        }
    }
}