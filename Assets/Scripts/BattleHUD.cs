using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{

    [SerializeField] List<TextMeshProUGUI> namePlayer;
    [SerializeField] List<TextMeshProUGUI> hpText;
    [SerializeField] List<TextMeshProUGUI> textDamage;
    [SerializeField] List<TextMeshProUGUI> mpText;
    [SerializeField] List<Slider> limitSlider;
    [SerializeField] List<Slider> timeSlider;    
    public GameObject panelButtons;

    public void SetHUD(Unit unit, int playerIdx)
    {        
        namePlayer[playerIdx].text = unit.UnitName;
        timeSlider[playerIdx].maxValue = unit.UnitTime;
        timeSlider[playerIdx].value = 0;                        
    }

    // Show the player's life on the screen
    public void SetHP(int currentHP, int maxHP, int playerIdx)
    {
        hpText[playerIdx].text = currentHP.ToString() + " / " + maxHP.ToString();
    }
    public IEnumerator ShowTextDamage(int damage, int playerIdx)
    {
        textDamage[playerIdx].gameObject.SetActive(true);
        textDamage[playerIdx].text = damage.ToString();

        float timer = 1;
        while(timer >= 0)
        {
            timer -= Time.deltaTime;
            // Move slowly the Damage Amount Upwards
            textDamage[playerIdx].transform.localPosition = 
                new Vector3(textDamage[playerIdx].transform.localPosition.x,
                            textDamage[playerIdx].transform.localPosition.y + Time.deltaTime,
                            textDamage[playerIdx].transform.localPosition.z);
            yield return null;
        }
        textDamage[playerIdx].gameObject.SetActive(false);
    }

    public void SetMP(int currentMP, int maxMP, int playerIdx)
    {
        mpText[playerIdx].text = currentMP.ToString() + " / " + maxMP.ToString();
    }
    public void SetMaxLimitBar(float maxLimit, int playerIdx)
    {        
        limitSlider[playerIdx].maxValue = maxLimit;
    }
    public void SetLimitBar(float currentLimit, float maxLimit, int playerIdx)
    {        
        limitSlider[playerIdx].value = Mathf.Clamp(currentLimit,0,maxLimit);
    }
    public void SetTimeBar(float currentTime, float maxTime, int playerIdx)
    {
        timeSlider[playerIdx].value = Mathf.Clamp(currentTime, 0, maxTime);
    }
    public void ResetTimeBar(int playerIdx)
    {
        timeSlider[playerIdx].value = 0;
    }    
}
