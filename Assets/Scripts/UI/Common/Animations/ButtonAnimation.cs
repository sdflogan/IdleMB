/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Mauricio Perez (Peche)
    Date: Friday 8th, December 2023
*/

using UnityEngine;
using DG.Tweening;

namespace TinyBytes.Idle.UI
{
    public class ButtonAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration = default;
        [SerializeField] private float _punchScale = default;
        [SerializeField] private int _vibrato = default;
        [SerializeField] private float _elasticity = default;
        [SerializeField] private Ease _ease = default;

        public void Play()
		{
            transform.DOPunchScale(Vector3.one * _punchScale, _duration, _vibrato, _elasticity).SetEase(_ease);
		}
    }
}