using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyData
    {
        public string Name;
        public int Level;
        public int EXP;
        public int Reward;
        public string EncounterText;
        public int HP;
        public int PP;
        public int Offense;
        public int Defense;
        public int Speed;
        public int Guts;
        public int Luck;
        public int Vitality;
        public int IQ;

        public string[] ActionText;
        public ActionManager.BattleActionIndex[] BattleActions;
    }

    [System.Serializable]
    public struct EnemyGroupData
    {
        public EnemyIndex[] Enemy;
    }

    public enum EnemyIndex
    {
        RamblinEvilMushroom
    };

    public EnemyData[] Enemy;
    public EnemyGroupData[] EnemyGroup;

    public static EnemyManager Instance = null;

    void Awake()
    {
        if (Instance != null) // meaning there's already an instance
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
