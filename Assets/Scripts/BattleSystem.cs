using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{    
    public enum BattleState 
    {   
        Start, 
        PlayerTurn, 
        EnemyTurn, 
        Won, 
        Lost 
    };
    public BattleState battleState;

    [SerializeField] List<GameObject> playerPrefab;
    [SerializeField] List<GameObject> enemyPrefab;

    [SerializeField] List<Transform> playerInitPos;
    [SerializeField] List<Transform> enemyInitPos;

    [SerializeField] BattleHUD battleHUD;

    List<Unit> playerUnit = new List<Unit>();
    List<Unit> enemyUnit = new List<Unit>();

    #region Unity API
    void Start() 
    {
        battleState = BattleState.Start;
        StartCoroutine(nameof(SetUpBattle));
    }
    #endregion
    IEnumerator SetUpBattle()
    {
        List<GameObject> players = new List<GameObject>();
        for(int i = 0; i < playerPrefab.Count; i++)
        {
            players.Add(Instantiate(playerPrefab[i], playerInitPos[i].position, new Quaternion()));
            playerUnit.Add(players[i].GetComponent<Unit>());
        }
        
        List<GameObject> enemys = new List<GameObject>();
        for(int i = 0; i < enemyPrefab.Count; i++)
        {
            enemys.Add(Instantiate(enemyPrefab[i], enemyInitPos[i].position, new Quaternion()));
            enemyUnit.Add(enemys[i].GetComponent<Unit>());
        }

        for (int i = 0; i < playerPrefab.Count; i++)
        {
            battleHUD.SetHUD(playerUnit[i], i);
            battleHUD.SetHP(playerUnit[i].CurrentHP, playerUnit[i].MaxHP, i);
            battleHUD.SetMP(playerUnit[i].CurrentMP, playerUnit[i].MaxMP, i);

            battleHUD.SetMaxLimitBar(playerUnit[i].UnitMaxLimit, i);
            battleHUD.SetLimitBar(playerUnit[i].CurrentLimit, playerUnit[i].UnitMaxLimit,i);                   
        }

        // Something else could be added to be executed before the slider loading

        // Call the coroutine and does not continue reading till if finishes 
        yield return StartCoroutine(PlayerTime(0));
        //for (int i = 0; i < playerPrefab.Count; i++)        
        //    yield return StartCoroutine(PlayerTime(i));        

        // Something else could be added to be executed after the coroutine
    }
    
    #region Buttons Methods
    public void OnAttackButton()
    {
        if(battleState != BattleState.PlayerTurn)
            return;
        StartCoroutine(nameof(PlayerAttack));
    }
    public void OnHealButton()
    {
        if (battleState != BattleState.PlayerTurn)
            return;
        StartCoroutine(nameof(PlayerHeal));
    }
    #endregion

    #region Player Actions
    // Will load the attacking Time (Slider)
    IEnumerator PlayerTime(int playerIdx)
    {
        float timePlayer = 0;
        while (timePlayer < playerUnit[playerIdx].UnitTime)
        {
            timePlayer += Time.deltaTime;
            //battleHUD.timeSlider.
            battleHUD.SetTimeBar(timePlayer, playerUnit[playerIdx].UnitTime, playerIdx);
            yield return null;
        }
        battleState = BattleState.PlayerTurn;
        PlayerTurn();
    }
    //void AllPlayersTime()
    //{
    //    for (int i = 0; i < playerPrefab.Count; i++)
    //        StartCoroutine(PlayerTime(i));
    //}
    void PlayerTurn()
    {
        Debug.Log("Player's Turn");
        battleHUD.panelButtons.SetActive(true);
    }
    IEnumerator PlayerHeal()
    {
        ResetAttackPlayer();
        playerUnit[0].Heal(playerUnit[0].healAmount);

        // Update HP values on the UI
        battleHUD.SetHP(playerUnit[0].CurrentHP, playerUnit[0].MaxHP,0);

        // Add Visual Feedback during the Waiting Time (2s)
        // Ex.:
        // - Heal animation? Particle System?
        // - HP Value Increasing animation?
        // - Heal amount Text on Green?
        // - Heal Sound

        yield return new WaitForSeconds(2);        

        battleState = BattleState.EnemyTurn;
        Debug.Log("Enemy's Turn");
    }
    IEnumerator PlayerAttack() 
    {
        ResetAttackPlayer();

        // The Player Attacks its rival
        yield return StartCoroutine(playerUnit[0].Attacking(enemyUnit[0].transform.position));

        // The player hurts the rival and is checked if he's dead
        bool isDead = enemyUnit[0].TakeDamage(playerUnit[0].damage);
        StartCoroutine(battleHUD.ShowTextDamage(playerUnit[0].damage, 0));

        yield return StartCoroutine(playerUnit[0].MovingToInitPosition());

        Debug.Log("The player has attacked: " + enemyUnit[0].CurrentHP);
        if (isDead)
        {
            battleState = BattleState.Won;
            Debug.Log("Battle won");
        }
        else
        {
            battleState = BattleState.EnemyTurn;
            Debug.Log("Enemy's Turn");
            StartCoroutine(nameof(EnemyAttack));
        }            
    } 
    void ResetAttackPlayer()
    {
        battleHUD.panelButtons.SetActive(false);        
        battleHUD.ResetTimeBar(0);
    }
    #endregion

    #region Enemy Actions
    IEnumerator EnemyAttack()
    {        
        // The Player Attacks its rival
        yield return StartCoroutine(enemyUnit[0].Attacking(playerUnit[0].transform.position));

        // The player hurts the rival and is checked if he's dead
        bool isDead = playerUnit[0].TakeDamage(enemyUnit[0].damage);
        battleHUD.SetHP(playerUnit[0].CurrentHP, playerUnit[0].MaxHP,0);

        yield return StartCoroutine(enemyUnit[0].MovingToInitPosition());

        Debug.Log("The enemy has attacked: " + playerUnit[0].CurrentHP);
        if (isDead)
        {
            battleState = BattleState.Lost;
            Debug.Log("Battle lost...");
        }
        else
        {
            battleState = BattleState.PlayerTurn;
            Debug.Log("Player's Turn");
            StartCoroutine(PlayerTime(0));            
        }
    }
    #endregion
}
