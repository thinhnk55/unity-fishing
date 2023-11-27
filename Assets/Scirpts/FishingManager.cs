using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public enum TypeFishing
{
    CaHe = 0,
    CaDuoi = 1,
    CaHong = 2,
}

public class FishingManager : MonoBehaviour
{
    public static FishingManager instance;


    public int Score;
    [SerializeField] private Image ImageRequire;
    [SerializeField] Dictionary<TypeFishing, Sprite> mappingSprite = new Dictionary<TypeFishing, Sprite>();
    [SerializeField] Sprite[] imageFish;
    public TypeFishing TypeFishingRequire { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < imageFish.Length; i++)
        {
            mappingSprite.Add((TypeFishing)i, imageFish[i]);
        }

        SetTargetRequire((TypeFishing)UnityEngine.Random.Range(0, mappingSprite.Count + 1));
    }

    public void SetTargetRequire(TypeFishing typeFishing)
    {
        TypeFishingRequire = typeFishing;
        ImageRequire.sprite = mappingSprite[typeFishing];
    }

    public void AddScore(int value)
    {
        Score += value;
        if (Score < 0) Score = 0;
        OnChangeScore(Score);

        CheckWin();
    }

    public bool CheckMatch(TypeFishing typeFishing)
    {
        if (TypeFishingRequire == typeFishing)
        {
            AddScore(1);
            mappingSprite.Remove(typeFishing);
            SetTargetRequire((TypeFishing)UnityEngine.Random.Range(0, mappingSprite.Count + 1));
            return true;
        }
        else
        {
            AddScore(-1);
            return false;
        }
    }

    private void CheckWin()
    {
        if (mappingSprite.Count == 0)
        {
            Debug.Log("Win");
        }
    } 

    public Action<int> OnChangeScore;
}
