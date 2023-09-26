/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using UnityEngine;

namespace TinyBytes.Utils.EditorHelpers
{
	public class EditorDrawSphere : MonoBehaviour
	{
        #region Inspector properties

        [SerializeField] private float _radius = 0.1f;
        [SerializeField] private Color _color = Color.yellow;

        #endregion

        #region Unity Events

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        #endregion
    }
}