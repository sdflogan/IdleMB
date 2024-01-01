
using UnityEngine;

namespace TinyBytes.Idle.GameCamera.Touch
{
    public class TouchInputPC : ITouchInput
    {
        public Vector2 GetTouchPosition()
        {
            return Input.mousePosition;
        }

        public bool IsStartTouching()
        {
            return Input.GetMouseButtonDown(0);
        }

        public bool IsEndTouching()
        {
            return Input.GetMouseButtonUp(0);
        }
    }
}