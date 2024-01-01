
using UnityEngine;

namespace TinyBytes.Idle.GameCamera.Touch
{
    public interface ITouchInput
    {
        public bool IsStartTouching();

        public bool IsEndTouching();

        public Vector2 GetTouchPosition();
    }
}