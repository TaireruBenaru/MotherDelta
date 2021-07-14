using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public enum BattleMenuIndex { Bash, Shoot, Swing, PSI, Impulse, Goods, Defend, Run };

    [System.Serializable]
    public struct CharData
    {
        public string Name;
        public int Level;
        public int EXPTotal;
        public int CurrentEXP;
        public int MaxHP;
        public int TargetHP;
        public int CurrentHP;
        public int MaxPP;
        public int TargetPP;
        public int CurrentPP;
        public int BaseOffense;
        public int CurrentOffense;
        public int BaseDefense;
        public int CurrentDefense;
        public int BaseSpeed;
        public int CurrentSpeed;
        public int BaseGuts;
        public int CurrentGuts;
        public int BaseLuck;
        public int CurrentLuck;
        public int Vitality;
        public int IQ;
        public bool HasPP;
        public int BattleMenuArrangement; 
    }

    [System.Serializable]
    public struct BattleMenuArrangementData
    {
        public BattleMenuIndex[] BattleMenuIndices;
    }

    public CharData[] Char;
    public int[] CurrentParty = new int[6];
    public int PartySize = 1;
    public BattleMenuArrangementData[] BattleMenuArrangementTable;

    public static CharacterManager Instance = null;

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
