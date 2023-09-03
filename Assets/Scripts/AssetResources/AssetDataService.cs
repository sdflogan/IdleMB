/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using TinyBytes.Idle.Scriptable.Wallet;
using TinyBytes.Utils;
using UnityEngine;

namespace TinyBytes.Idle
{
	public class AssetDataService : Singleton<AssetDataService>
	{
		#region Events



		#endregion

		#region Inspector properties

		[SerializeField] private WalletResourcesScriptable _walletResourcesData;

		#endregion

		#region Private properties



		#endregion

		#region Public properties

		public WalletResourcesScriptable WalletResourcesData => _walletResourcesData;
		
		#endregion
		
		#region Private methods
		
		
		
		#endregion
		
		#region Public methods
		
		
		
		#endregion
	}
}