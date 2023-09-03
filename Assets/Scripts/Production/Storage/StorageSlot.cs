/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using UnityEngine;

namespace TinyBytes.Idle.Production.Storage
{
	public class StorageSlot : MonoBehaviour
	{
        #region Events



        #endregion

        #region Inspector properties



        #endregion

        #region Private properties



        #endregion

        #region Public properties

        public int Row { get; private set; }
        public int Col { get; private set; }

        public StorageSlotState State { get; private set; }

        public bool IsAvailable => State == StorageSlotState.Available;

        public GameObject StoredResource { get; private set; }

        #endregion

        #region Unity Events

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;

            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }

        #endregion

        #region Private methods



        #endregion

        #region Public methods

        public void Init(int row, int col, float size, float offsetX, float offsetY)
        {
            this.Row = row;
            this.Col = col;

            transform.localPosition = new Vector3(0, col * size * offsetY, row * size * offsetX);
            transform.localScale = Vector3.one * size;
            State = StorageSlotState.Available;
        }

        public void Fill(GameObject resource)
        {
            resource.transform.parent = transform;
            resource.transform.position = Vector3.zero;
            StoredResource = resource;
            State = StorageSlotState.Full;
        }

        public GameObject Free()
        {
            StoredResource.transform.parent = null;
            var resource = StoredResource;
            StoredResource = null;
            State = StorageSlotState.Available;

            return resource;
        }

        #endregion
    }
}