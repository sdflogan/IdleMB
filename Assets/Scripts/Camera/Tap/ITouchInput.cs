
using UnityEngine;

namespace TinyBytes.Idle.GameCamera.Tap
{
    public interface ITouchInput
    {
        public bool IsStartTouching();

        public bool IsEndTouching();

        public Vector2 GetTouchPosition();
    }
}