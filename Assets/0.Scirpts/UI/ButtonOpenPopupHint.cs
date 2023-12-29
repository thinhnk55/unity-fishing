using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenPopupHint : ButtonBase
{
    [SerializeField] int idButton;
    [SerializeField] GameObject popupHint;


    protected override void Button_OnClicked()
    {
        PopupHint popup = PopupHelper.Create(popupHint).GetComponent<PopupHint>();
        popup.idWord = FishingManager.Instance.itemsCorrect[idButton];
    }
}
