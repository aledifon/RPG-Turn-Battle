using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

    [Header("Attack Variables")]
    [SerializeField] float timeAnimationAttack;     // Time which takes the attack animation to execute
    [SerializeField] float speed;                   // Moving speed of the player when he's attacking
    [SerializeField] float offset;                  // Offset Max. Distance to keep between the player and its target

    public int damage;
    public int healAmount;

    [Header("Limit Unit")]
    [SerializeField] private float _unitMaxLimit;    // max limit value of the player
    public float UnitMaxLimit
    {
        get { return _unitMaxLimit; }
        private set { _unitMaxLimit = value; }
    }
    private float _currentLimit;                 // current acumulated limit value of the player
    public float CurrentLimit
    {
        get { return _currentLimit; }
        private set { _currentLimit = Mathf.Clamp(value,0,_unitMaxLimit); }
    }

    [Header("HP")]
    [SerializeField] private int _maxHP;
    public int MaxHP
    {
        get { return _maxHP; }
        private set { _maxHP = value; }
    }
    [SerializeField] private int _currentHP;
    public int CurrentHP 
    {
        get { return _currentHP; }       
        private set { _currentHP = Mathf.Clamp(value,0, _maxHP); } 
    }

    [Header("MP")]
    [SerializeField] private int _maxMP;
    public int MaxMP
    {
        get { return _maxMP; }
        private set { _maxMP = value; }
    }
    [SerializeField] private int _currentMP;
    public int CurrentMP
    {
        get { return _currentMP; }
        private set { _currentMP = Mathf.Clamp(value, 0, _maxMP); }
    }

    Vector3 startedPosition;
    Animator anim;

    #region Unity API
    private void Awake()
    {
        CurrentHP = MaxHP;
        CurrentMP = MaxMP;
        CurrentLimit = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        startedPosition = transform.position;        
    }

    // This method will move the player towards its target and will execute the attack
    // point represents the enemy's position
    public IEnumerator Attacking(Vector3 point)
    {
        anim.SetBool("Moving", true);
        while (Vector3.Distance(transform.position, point) >= offset) 
        {
            transform.position = Vector3.MoveTowards(transform.position,point,speed * Time.deltaTime);
            yield return null;
        }

        // Once thehe player reaches its target then the Attack Animation
        // is played and we wait for it to be completed
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(timeAnimationAttack);   
    }
    public IEnumerator MovingToInitPosition()
    {
        while (Vector3.Distance(transform.position, startedPosition) >= 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startedPosition, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = startedPosition;
        anim.SetBool("Moving", false);
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
        else
        {
            IncreaseLimitBar(damage);
            return false;
        }
    }
    public void Heal(int amount)
    {
        CurrentHP += amount;        
    }
    public void HealMP(int amount)
    {
        CurrentMP += amount;        
    }
    public void IncreaseLimitBar(int amount)
    {        
        CurrentLimit += Mathf.Clamp((amount/_maxHP), 0, 1) * UnitMaxLimit;
    }
    public void ResetLimitBar()
    {
        CurrentLimit = 0;
    }
}
