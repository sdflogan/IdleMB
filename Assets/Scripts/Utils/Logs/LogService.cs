using System;
using System.Collections.Generic;
using UnityEngine;

namespace TinyBytes.Utils.Logs
{
    public class LogService : Singleton<LogService>
    {
        #region Inspector properties

        [SerializeField] private Dictionary<LogType, LogFeature> _features;

        /// <summary>
        /// We need this property until odin is installed (serializing dictionaries)
        /// </summary>
        [SerializeField] private List<LogFeature> _featuresData;

        #endregion

        #region Unity Events

        private void Awake()
        {
            _features = new Dictionary<LogType, LogFeature>();

            foreach(var feature in _featuresData)
            {
                _features[feature.LogType] = feature;
            }
        }

        #endregion

        #region Private methods

        private LogFeature GetFeatureInfo(LogType type)
        {
            if (_features != null)
            {
                return (_features.ContainsKey(type) ? _features[type] : null);
            }

            return null;
        }

        private void LogConsole(string text, LogType type, bool isError = false)
        {
            var featureInfo = GetFeatureInfo(type);

            if (featureInfo != null)
            {
                Log(text, type, featureInfo, isError);
            }
        }

        private void LogConsole(string text, Color color)
        {
            Debug.Log(ColorString(text, color));
        }

        private void Log(string text, LogType type, LogFeature feature, bool isError)
        {
            if (feature.Enabled)
            {
                string header = BoldString(ColorString("[" + type.ToString() + "]", feature.Color));

                if (isError)
                {
                    Debug.LogError(header + text);
                }
                else
                {
                    Debug.Log(header + text);
                }
            }
        }

        private string ColorString(string text, Color color)
        {
            string hexColor = ToHex(color);
            return ("<color=" + hexColor + ">" + text + "</color> ");
        }

        private string BoldString(string text)
        {
            return ("<b>" + text + "</b>");
        }

        public static string ToHex(Color color)
        {
            var col = (Color32)color;
            return "#" + col.r.ToString("x2") + col.g.ToString("x2") + col.b.ToString("x2") + col.a.ToString("x2");
        }

        #endregion

        #region Public methods

        public static void Log(string text, Color color)
        {
#if !RELEASE
            Instance?.LogConsole(text, color);
#endif
        }

        public static void Log(string text, LogType type = LogType.Undefined)
        {
#if !RELEASE
            Instance?.LogConsole(text, type);
#endif
        }

        public static void LogWarning(string text, UnityEngine.Object context = null)
        {
#if !RELEASE
            Debug.LogWarning(text, context);
#endif
        }

        public static void LogError(string text, LogType type = LogType.Undefined)
        {
            Instance?.LogConsole(text, type, true);
        }

        public void DisableLogs()
        {
            foreach (var feature in _features.Values)
            {
                feature.Enabled = false;
            }
        }

        public void EnableLogs(params LogType[] features)
        {
            foreach (var feature in features)
            {
                if (_features.ContainsKey(feature))
                {
                    _features[feature].Enabled = true;
                }
            }
        }

        #endregion
    }
}