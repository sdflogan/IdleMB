/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using TinyBytes.Idle.Player;
using UnityEngine;

namespace TinyBytes.Idle.Services
{
	public class ServicesStarter : MonoBehaviour
	{
		#region Events



		#endregion

		#region Inspector properties

		[SerializeField] private PlayerServices _playerServices;

        #endregion

        #region Private properties



        #endregion

        #region Public properties



        #endregion

        #region UnityEvents

        private void Start()
        {
            _playerServices.StartService();

            MainEvents.OnGameLoaded?.Invoke();
        }

        #endregion

        #region Private methods



        #endregion

        #region Public methods



        #endregion
    }
}