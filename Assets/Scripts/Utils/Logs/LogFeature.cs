using System;
using UnityEngine;

namespace TinyBytes.Utils.Logs
{
    [Serializable]
    public class LogFeature
    {
        public LogType LogType;
        public bool Enabled;
        public Color Color;
    }
}