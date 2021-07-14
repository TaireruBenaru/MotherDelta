using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using System;

public class BattleRoutine : MonoBehaviour
{
    public TurnManager turnManager;

    public List<EnemyBattler> Enemy = new List<EnemyBattler>();
    public List<PlayerBattler> Player = new List<PlayerBattler>();

    public List<GameObject> TurnOrder;
    IDictionary<GameObject, int> turnOrder = new Dictionary<GameObject, int>();

    public GameObject[] Letterbox = new GameObject[2];

    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;

    public Vector3[] OnePlayerPosition; 
    public Vector3[] TwoPlayerPosition; 
    public Vector3[] ThreePlayerPosition; 
    public Vector3[] FourPlayerPosition; 










    // Start is called before the first frame update
    IEnumerator Start()
    {
        InitVars();
        InitParty();
        //InitNPC();
        InitEnemy();

        yield return new WaitForSeconds(1f);

        InitLetterbox();
        
        yield return StartCoroutine(DialogueManager.Instance.DisplayText(WindowManager.WindowIndex.BattleMessage, Enemy[0].EncounterText, 0.8f));
        CalculateTurnOrder();
        yield return StartCoroutine(SelectAction());
    }

    IEnumerator SelectAction()
    {
        for (int i = 0; i < CharacterManager.Instance.PartySize; i++)
        {
            yield return StartCoroutine(turnManager.ChooseBattleMenuOption(Player[i].PlayerNumber));
        }
        yield return StartCoroutine(Turn());
    }
    IEnumerator Turn()
    {
        if(TurnOrder.Any())
        {
            if(TurnOrder[0].GetComponent<PlayerBattler>() != null)
            {
                Debug.Log("Player Turn");
                yield return StartCoroutine(TurnOrder[0].GetComponent<PlayerBattler>().StartTurn());
            }
            else if(TurnOrder[0].GetComponent<EnemyBattler>() != null)
            {
                Debug.Log("Enemy Turn");
                yield return StartCoroutine(TurnOrder[0].GetComponent<EnemyBattler>().StartTurn());
            }
            else
            {
                Debug.LogError("Whatever this is, it's not a player or enemy");
            }

            TurnOrder.RemoveAt(0);
            yield return StartCoroutine(Turn());
        }
        else
        {
            //TODO: END TURN;
            yield return null;
        }
    }

    void CalculateTurnOrder()
    {
        //TODO: Don't add to turn order under certain circomstances.
        for (int i = 0; i < CharacterManager.Instance.PartySize; i++)
        {
            turnOrder.Add(Player[i].gameObject, CharacterManager.Instance.Char[Player[i].PlayerNumber].CurrentSpeed);
        }
        
        for (int i = 0; i < Enemy.Count; i++)
        {
            turnOrder.Add(Enemy[i].gameObject, Enemy[i].CurrentSpeed);
        }

        foreach(KeyValuePair<GameObject, int> Battler in turnOrder.OrderByDescending(key => key.Value))
        {
            TurnOrder.Add(Battler.Key);
        }

    }

    void InitVars()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }
    void InitParty()
    {
        switch(CharacterManager.Instance.PartySize)
        {
           case 1:
                Player.Add(Instantiate(PlayerPrefab, OnePlayerPosition[0], Quaternion.identity).GetComponent<PlayerBattler>());
                SetupCharacter(0);
            break; 
            case 2:
                Player.Add(Instantiate(PlayerPrefab, TwoPlayerPosition[0], Quaternion.identity).GetComponent<PlayerBattler>());
                SetupCharacter(0);
                Player.Add(Instantiate(PlayerPrefab, TwoPlayerPosition[1], Quaternion.identity).GetComponent<PlayerBattler>());
                SetupCharacter(1);
            break;
            case 3:
                Player.Add(Instantiate(PlayerPrefab, ThreePlayerPosition[0], Quaternion.identity).GetComponent<PlayerBattler>());
                SetupCharacter(0);
                Player.Add(Instantiate(PlayerPrefab, ThreePlayerPosition[1], Quaternion.identity).GetComponent<PlayerBattler>());
                SetupCharacter(1);
                Player.Add(Instantiate(PlayerPrefab, ThreePlayerPosition[2], Quaternion.identity).GetComponent<PlayerBattler>());
                SetupCharacter(2);
            break;
            case 4:
                Player.Add(Instantiate(PlayerPrefab, FourPlayerPosition[0], Quaternion.identity).GetComponent<PlayerBattler>());
                SetupCharacter(0);
                Player.Add(Instantiate(PlayerPrefab, FourPlayerPosition[1], Quaternion.identity).GetComponent<PlayerBattler>());
                SetupCharacter(1);
                Player.Add(Instantiate(PlayerPrefab, FourPlayerPosition[2], Quaternion.identity).GetComponent<PlayerBattler>());
                SetupCharacter(2);
                Player.Add(Instantiate(PlayerPrefab, FourPlayerPosition[3], Quaternion.identity).GetComponent<PlayerBattler>());
                SetupCharacter(3);
            break;
        }
    }


    void InitEnemy()
    {
        for (int i = 0; i < EnemyManager.Instance.EnemyGroup[GameManager.Instance.CurrentEnemyGroup].Enemy.Length; i++)
        {
            Enemy.Add(Instantiate(EnemyPrefab, new Vector3(0, 1, 0), Quaternion.identity).GetComponent<EnemyBattler>());
            SetupEnemy(i, (int)EnemyManager.Instance.EnemyGroup[GameManager.Instance.CurrentEnemyGroup].Enemy[i]);
            float Number = i;
            float Y = (float)(1.5f / EnemyManager.Instance.EnemyGroup[GameManager.Instance.CurrentEnemyGroup].Enemy.Length);
            float X = (float)(Y * Number + 0.32);
            Enemy[i].transform.position = new Vector3(X, Y, 0);
        }
    }



    void SetupCharacter(int PartyIndex)
    {
        Player[PartyIndex].PlayerNumber = CharacterManager.Instance.CurrentParty[PartyIndex];
    }

     void SetupEnemy(int EnemyIndex, int GlobalEnemyIndex)
    {
        Enemy[EnemyIndex].Name = EnemyManager.Instance.Enemy[GlobalEnemyIndex].Name;
        Enemy[EnemyIndex].EXP = EnemyManager.Instance.Enemy[GlobalEnemyIndex].EXP;
        Enemy[EnemyIndex].Reward = EnemyManager.Instance.Enemy[GlobalEnemyIndex].Reward;
        Enemy[EnemyIndex].EncounterText = EnemyManager.Instance.Enemy[GlobalEnemyIndex].EncounterText;
        
        Enemy[EnemyIndex].MaxHP = EnemyManager.Instance.Enemy[GlobalEnemyIndex].HP;
        Enemy[EnemyIndex].CurrentHP = EnemyManager.Instance.Enemy[GlobalEnemyIndex].HP;
        Enemy[EnemyIndex].MaxPP = EnemyManager.Instance.Enemy[GlobalEnemyIndex].PP;
        Enemy[EnemyIndex].CurrentPP = EnemyManager.Instance.Enemy[GlobalEnemyIndex].PP;
        Enemy[EnemyIndex].BaseOffense = EnemyManager.Instance.Enemy[GlobalEnemyIndex].Offense;
        Enemy[EnemyIndex].CurrentOffense = EnemyManager.Instance.Enemy[GlobalEnemyIndex].Offense;
        Enemy[EnemyIndex].BaseDefense = EnemyManager.Instance.Enemy[GlobalEnemyIndex].Defense;
        Enemy[EnemyIndex].CurrentDefense = EnemyManager.Instance.Enemy[GlobalEnemyIndex].Defense;
        Enemy[EnemyIndex].BaseSpeed = EnemyManager.Instance.Enemy[GlobalEnemyIndex].Speed;
        Enemy[EnemyIndex].CurrentSpeed = EnemyManager.Instance.Enemy[GlobalEnemyIndex].Speed;
        Enemy[EnemyIndex].BaseGuts = EnemyManager.Instance.Enemy[GlobalEnemyIndex].Guts;
        Enemy[EnemyIndex].CurrentGuts = EnemyManager.Instance.Enemy[GlobalEnemyIndex].Guts;
        Enemy[EnemyIndex].BaseLuck = EnemyManager.Instance.Enemy[GlobalEnemyIndex].Luck;
        Enemy[EnemyIndex].CurrentLuck = EnemyManager.Instance.Enemy[GlobalEnemyIndex].Luck;
        Enemy[EnemyIndex].Vitality = EnemyManager.Instance.Enemy[GlobalEnemyIndex].Vitality;
        Enemy[EnemyIndex].IQ = EnemyManager.Instance.Enemy[GlobalEnemyIndex].IQ;
    }

    void InitLetterbox()
    {
        Letterbox[0].transform.DOMoveY(7.5f, 0.4f, false);
        Letterbox[1].transform.DOMoveY(-7f, 0.4f, false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
