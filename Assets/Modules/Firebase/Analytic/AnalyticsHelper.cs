#if FIREBASE
using System.Collections.Generic;

namespace FirebaseIntegration
{
    public class AnalyticsHelper
    {
        public static void Login()
        {
            Analytics.Log(Firebase.Analytics.FirebaseAnalytics.EventLogin,
                new List<KeyValuePair<string, object>>()
                {
                });
        }
        public static void UnlockAchievement(int id, int level)
        {
            Analytics.Log(Firebase.Analytics.FirebaseAnalytics.EventUnlockAchievement,
                new List<KeyValuePair<string, object>>()
            {
                    new KeyValuePair<string, object>(Firebase.Analytics.FirebaseAnalytics.ParameterAchievementId, id),
                    new KeyValuePair<string, object>("level", level),
            });
        }
        public static void WatchAds(string format, string type, string source)
        {
            Analytics.Log(Firebase.Analytics.FirebaseAnalytics.EventAdImpression,
                new List<KeyValuePair<string, object>>()
            {
                    new KeyValuePair<string, object>(Firebase.Analytics.FirebaseAnalytics.ParameterAdSource, format),
                    new KeyValuePair<string, object>(Firebase.Analytics.FirebaseAnalytics.ParameterAdUnitName, type),
                    new KeyValuePair<string, object>(Firebase.Analytics.FirebaseAnalytics.ParameterAdSource, source),
            });
        }
        public static void Transaction(string transaction)
        {
            Analytics.Log(Firebase.Analytics.FirebaseAnalytics.EventSpendVirtualCurrency,
                new List<KeyValuePair<string, object>>()
            {
                    new KeyValuePair<string, object>(Firebase.Analytics.FirebaseAnalytics.ParameterTransactionId, transaction),
            });
        }
        public static void SpendVirtualCurrency(string virtualCurrencyName, string source)
        {
            Analytics.Log(Firebase.Analytics.FirebaseAnalytics.EventSpendVirtualCurrency,
                new List<KeyValuePair<string, object>>()
            {
                    new KeyValuePair<string, object>(Firebase.Analytics.FirebaseAnalytics.ParameterVirtualCurrencyName, virtualCurrencyName),
                    new KeyValuePair<string, object>(Firebase.Analytics.FirebaseAnalytics.ParameterSource, source),
            });
        }
        public static void EarnVirtualCurrency(string virtualCurrencyName, string source)
        {
            Analytics.Log(Firebase.Analytics.FirebaseAnalytics.EventEarnVirtualCurrency,
                new List<KeyValuePair<string, object>>()
            {
                    new KeyValuePair<string, object>(Firebase.Analytics.FirebaseAnalytics.ParameterVirtualCurrencyName, virtualCurrencyName),
                    new KeyValuePair<string, object>(Firebase.Analytics.FirebaseAnalytics.ParameterSource, source),
            });
        }
    }
}
#endif