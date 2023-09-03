using System;
using System.Collections;
using System.Collections.Generic;
using TinyBytes.Utils.Logs;
using UnityEngine;

namespace TinyBytes.Idle.Player.Wallet
{
    public class WalletService
    {
        #region Public properties

        public Dictionary<ResourceType, long> Resources { get; private set; }

        #endregion

        #region Private methods

        private void LoadPlayerResources()
        {
            Resources = new Dictionary<ResourceType, long>();

            Resources[ResourceType.HC] = 0;
            Resources[ResourceType.SC] = 0;
            Resources[ResourceType.Special] = 0;

            WalletEvents.OnResourceUpdated?.Invoke(ResourceType.HC, Resources[ResourceType.HC]);
            WalletEvents.OnResourceUpdated?.Invoke(ResourceType.SC, Resources[ResourceType.SC]);
            WalletEvents.OnResourceUpdated?.Invoke(ResourceType.Special, Resources[ResourceType.Special]);
        }

        #endregion

        #region Public methods

        public void Start()
        {
            LoadPlayerResources();
            WalletEvents.OnServiceLoaded?.Invoke();

            LogService.Log("Wallet Service Loaded", Utils.Logs.LogType.Wallet);
        }

        public void Earn(ResourceType resource, long amount)
        {
            Resources[resource] += amount;

            WalletEvents.OnResourceUpdated?.Invoke(resource, Resources[resource]);

            LogService.Log($"Earned {amount} of {resource}", Utils.Logs.LogType.Wallet);
        }

        public void Spend(ResourceType resource, long amount)
        {
            Resources[resource] -= amount;

            if (Resources[resource] <= 0)
            {
                Resources[resource] = 0;
            }

            WalletEvents.OnResourceUpdated?.Invoke(resource, Resources[resource]);

            LogService.Log($"Spent {amount} of {resource}", Utils.Logs.LogType.Wallet);
        }

        public bool Contains(ResourceType resource, long amount)
        {
            return Resources[resource] - amount >= 0;
        }

        #endregion
    }
}