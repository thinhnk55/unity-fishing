using Framework;
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

    private bool isGameOver;
    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
        }
    }

    public int Score;
    [SerializeField] Dictionary<TypeFishing, Sprite> mappingSpriteRequire = new Dictionary<TypeFishing, Sprite>();
    public Dictionary<TypeFishing, Sprite> mappingSpriteRequire1 = new Dictionary<TypeFishing, Sprite>();
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
            mappingSpriteRequire.Add((TypeFishing)i, imageFish[i]);
            mappingSpriteRequire1.Add((TypeFishing)i, imageFish[i]);
        }

        SetTargetRequire(GetNextRequire().Value);
    }

    public void SetTargetRequire(TypeFishing typeFishing)
    {
        TypeFishingRequire = typeFishing;
        OnChangeTargetRequire(mappingSpriteRequire[TypeFishingRequire]);
    }

    public void AddScore(int value)
    {
        Score += value;
        if (Score < 0) Score = 0;
        OnChangeScore(Score);
    }

    public bool CheckMatch(TypeFishing typeFishing)
    {
        if (TypeFishingRequire == typeFishing)
        {
            AddScore(1);
            mappingSpriteRequire.Remove(typeFishing);
            TypeFishing? nextTypeFishing = GetNextRequire();
            if (nextTypeFishing != null)
            {
                SetTargetRequire(nextTypeFishing.Value);

            }
            else
            {
                GameOver(true);
            }
            return true;
        }
        else
        {
            AddScore(-1);
            return false;
        }
    }

    public void GameOver(bool isWin)
    {
        if(IsGameOver) { return; }

        Debug.Log("Player Win: " + isWin);
        isGameOver = true;
        if(isWin)
        {
            PopupHelper.Create(PrefabFactory.WinPanel);
        }
        else
        {
            PopupHelper.Create(PrefabFactory.LosePanel);
        }
        OnGameOver?.Invoke(isWin);
    }

    private TypeFishing? GetNextRequire()
    {
        if(mappingSpriteRequire.Count > 0)
        {
            KeyValuePair<TypeFishing, Sprite> KVP = GetRandomElement(mappingSpriteRequire);
            return KVP.Key;
        }
        return null;
    }


    static KeyValuePair<TKey, TValue> GetRandomElement<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        // Use Random to generate a random index
        System.Random random = new System.Random();
        int randomIndex = random.Next(0, dictionary.Count);

        // Access the corresponding element in the dictionary
        KeyValuePair<TKey, TValue> randomElement = dictionary.ElementAt(randomIndex);

        return randomElement;
    }
    public Action<int> OnChangeScore;
    public Action<Sprite> OnChangeTargetRequire;
    public Action<bool> OnGameOver;
}