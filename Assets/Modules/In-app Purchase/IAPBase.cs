using System;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace IAP
{
    public class IAPBase : SingletonMono<IAPBase>, IDetailedStoreListener
    {
        protected IStoreController m_StoreController;          // The Unity Purchasing system.
        protected IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
        protected Action<bool, Product> purchaseAction;
        protected string _currentBundleId;

        protected override async void Awake()
        {
            base.Awake();
            await UnityServices.InitializeAsync();
            if (!IsInitialized())
            {
                InitializePurchasing();
            }
        }

        protected virtual void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            UnityPurchasing.Initialize(this, builder);
        }

        private bool IsInitialized()
        {
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }

        public static string GetProductPriceFromStore(string id, string defaultPrice = "")
        {
            if (Instance.m_StoreController != null && Instance.m_StoreController.products != null)
                return Instance.m_StoreController.products.WithID(id).metadata.localizedPriceString;
            else
                return defaultPrice;
        }

        public static void PurchaseProduct(string bundleID, Action<bool, Product> action)
        {
            Instance.purchaseAction = action;
            Instance._currentBundleId = bundleID;
            if (Instance.IsInitialized())
            {
                Product product = Instance.m_StoreController.products.WithID(bundleID);
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    Instance.m_StoreController.InitiatePurchase(product);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Product is not found or unavailable :" + bundleID);
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }
        public static void ConfirmPendingPurchase()
        {
            if (Instance.IsInitialized())
            {
                Product product = Instance.m_StoreController.products.WithID(Instance._currentBundleId);
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchased product successfully: '{0}'", product.definition.id));
                    Instance.m_StoreController.ConfirmPendingPurchase(product);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Product is not found or unavailable :" + Instance._currentBundleId);
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }
        // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
        // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
        public void RestorePurchases()
        {
            // If Purchasing has not yet been set up ...
            if (!IsInitialized())
            {
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                return;
            }

            // If we are running on an Apple device ... 
            if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                Debug.Log("RestorePurchases started ...");
                // Fetch the Apple store-specific subsystem.
                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                apple.RestoreTransactions((result, message) =>
                {
                    if (!result)
                    {
                        Debug.Log("RestorePurchases continuing: " + message + ". If no further messages, no purchases available to restore.");
                    }
                    else
                    {
                        Debug.Log("RestorePurchases Successfully");
                    }
                });
            }
            else
            {
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
        }

        // IStoreListener
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            m_StoreController = controller;
            m_StoreExtensionProvider = extensions;
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (String.Equals(args.purchasedProduct.definition.id, _currentBundleId, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                purchaseAction?.Invoke(true, args.purchasedProduct);
            }
            else
            {
                purchaseAction?.Invoke(false, args.purchasedProduct);
                Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            }
            return PurchaseProcessingResult.Pending;
        }
        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.Log(string.Format("Purchase Failed: Product: '{0}', {1}", error.ToString(), message));
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.Log(string.Format("Purchase Failed: Product: '{0}', {1}", product.definition.storeSpecificId, failureDescription));
            purchaseAction?.Invoke(false, product);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log(string.Format("Purchase Failed: Product: '{0}', {1}", product.definition.storeSpecificId, failureReason));
            purchaseAction?.Invoke(false, product);
        }
    }

}
