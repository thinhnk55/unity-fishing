using DG.Tweening;
using Framework;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DebugFunction : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float timeFly;
    [SerializeField] Ease easeFly;
    [SerializeField] Ease easeScale;

    [Button("Fly")]
    private void FlyToTarget()
    {
        //GameObject itemFly = Instantiate(PrefabFactory.ItemFly, this.transform.position, Quaternion.identity, target.parent);
        //itemFly.GetComponent<Image>().sprite = ;
        this.transform.DOMove(target.position, timeFly).SetEase(easeFly).OnComplete(() =>
        {
            this.transform.DOScale(0, 0.5f).SetEase(easeScale).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            }).Rewind();
        });
    }
}
