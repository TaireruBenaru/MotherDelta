using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattler : MonoBehaviour
{
        public string Name;
        public string EncounterText;
        public int Level;
        public int EXP;
        public int Reward;
        public int MaxHP;
        public int CurrentHP;
        public int MaxPP;
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

        public string[] ActionText;
        public ActionManager.BattleActionIndex[] BattleActions;

    public int Action;
    public int Target;
    public bool PlayerOrFoe;

    public SkillBase ActionObject;


    private BattleRoutine battleRoutine;

    void Start()
    {
       battleRoutine = GameObject.Find("BattleRoutine").GetComponent<BattleRoutine>();
    }

    public IEnumerator StartTurn()
    {
        yield return StartCoroutine(DialogueManager.Instance.DisplayText(WindowManager.WindowIndex.BattleMessage, ActionManager.Instance.BattleActionTable[Action].UseText, 0.8f));
        ActionObject = Instantiate(ActionManager.Instance.BattleActionTable[Action].ActionObject, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<SkillBase>();
        yield return StartCoroutine(ActionObject.UseSkill(EnemyNumber(), false, Target, PlayerOrFoe));
    }

    public void DecisionAction()
    {
        Action = 0;
    }

    public int EnemyNumber()
    {
        return battleRoutine.Enemy.IndexOf(this);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
