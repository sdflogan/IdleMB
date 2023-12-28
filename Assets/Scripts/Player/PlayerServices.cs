using System.Collections;
using System.Collections.Generic;
using TinyBytes.Idle.Player.Wallet;
using TinyBytes.Utils;
using TinyBytes.Utils.Extension;
using UnityEngine;

namespace TinyBytes.Idle.Player
{
    public class PlayerServices : Singleton<PlayerServices>
    {
        #region Private properties



        #endregion

        #region Public properties

        public WalletService Wallet { get; private set; }

        #endregion

        #region Private methods



        #endregion

        #region Public methods

        public void StartService()
        {
            Wallet = new WalletService();
            Wallet.Start();
        }

        #endregion
    }
}