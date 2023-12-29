using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenPopupHint : ButtonBase
{
    [SerializeField] Rod rod;
    [SerializeField] int idButton;
    [SerializeField] GameObject popupHint;


    protected override void Button_OnClicked()
    {
        if (rod.DiggerState != DiggerState.NONE && rod.DiggerState != DiggerState.SWINGING)
        {
            return;
        }

        PopupHint popup = PopupHelper.Create(popupHint).GetComponent<PopupHint>();
        popup.idWord = FishingManager.Instance.itemsCorrect[idButton];
    }
}
