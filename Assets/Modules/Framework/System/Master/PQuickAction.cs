#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Framework
{
    public class PQuickAction : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneTransitionHelper.Reload(false);
            }
        }

        [MenuItem("PFramework/Clear Data")]
        public static void ClearData()
        {
            PGameMaster.ClearData();
        }
    }
}

#endif