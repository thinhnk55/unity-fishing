using Framework;
using UnityEngine;

public class VolumnInfo : IDataUnit<VolumnInfo>
{
    public int Index { get; set; }
    public float Volumn;
}
public class VolumnCard : CardBase<VolumnInfo>
{
    [SerializeField] RectTransform rect;
    float initHeight;
    private void Awake()
    {
        initHeight = rect.sizeDelta.y;
    }

    public override void BuildView(VolumnInfo info)
    {
        base.BuildView(info);
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, initHeight * info.Volumn);
    }
}
