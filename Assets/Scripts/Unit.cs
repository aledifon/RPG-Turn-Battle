using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Info Unit")]
    [SerializeField] private string _unitName;
    public string UnitName 
    { 
        get { return _unitName; } 
        private set { _unitName = value; } 
    }
    [SerializeField] private float _unitTime;    // Time will take the player to attack
    public float UnitTime
    {
        get { return _unitTime; }
        private set { _unitTime = value; }
    }

    [Header("HP")]
    [SerializeField] private int maxHP;
    private int currentHP;
    public int CurrentHP 
    {
        get { return currentHP; }       
        private set { currentHP = Mathf.Clamp(value,0, maxHP); } 
    }

    Vector3 startedPosition;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        startedPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    // This method returns a boolean and tells me if the character is dead or not.
    // Will be called from the script which controls the battle
    public bool TakeDamage(int damage)
    {
        CurrentHP -= damage;

        if (CurrentHP <= 0) return true;
        else return false;
    }
    public void Heal(int amount)
    {
        CurrentHP += amount;
        
    }
}
