using Framework;
using UnityEngine;

public class AudioTracker : CacheMonoBehaviour
{
    public SoundType type;
    [SerializeField] private int activeSound; public int ActiveSound
    {
        get
        {
            int activeSound = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    activeSound++;
                }
            }
            return activeSound;
        }
    }
    void Start()
    {
        transform.GetChildren();
    }

    public bool IsFullActiveSound()
    {
        return ActiveSound == AudioConfig.SoundConfigs[type].maxActiveSound;
    }
}
