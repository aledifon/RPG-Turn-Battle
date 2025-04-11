using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI namePlayer;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Slider timeSlider;
    [SerializeField] GameObject panelButtons;

    public void SetHUD(Unit unit)
    {
        namePlayer.text = unit.UnitName;
        timeSlider.maxValue = unit.UnitTime;
        timeSlider.value = 0;
    }

    // Show the player's life on the screen
    public void SetHP(int currentHP, int maxHP)
    {
        hpText.text = currentHP.ToString() + " / " + maxHP.ToString();
    }
}
