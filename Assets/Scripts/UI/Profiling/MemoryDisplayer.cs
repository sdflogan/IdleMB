/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace TinyBytes.Idle.UI.Profiling
{
	public class MemoryDisplayer : MonoBehaviour
	{
        #region Inspector properties

        [SerializeField] private TextMeshProUGUI _displayerTxt;

        #endregion

        #region Private properties

        private Coroutine _coroutine = null;

        #endregion

        private void OnEnable()
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(UpdateCurrentMemory());
            }
        }

        private void OnDisable()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }

        private IEnumerator UpdateCurrentMemory()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                _displayerTxt.text = (Profiler.GetTotalAllocatedMemoryLong() / 1048576).ToString();
            }
        }
    }
}