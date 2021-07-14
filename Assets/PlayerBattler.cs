using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBattler : MonoBehaviour
{
    public int PlayerNumber = 0;
    public int Action;
    public bool PlayerOrFoe;
    public int Target = 0;

    public SkillBase ActionObject;

    public float[] Position;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator StartTurn()
    {
        yield return StartCoroutine(ChangePosition(1));
        yield return StartCoroutine(DialogueManager.Instance.DisplayText(WindowManager.WindowIndex.BattleMessage, ActionManager.Instance.BattleActionTable[Action].UseText, 0.8f));
        ActionObject = Instantiate(ActionManager.Instance.BattleActionTable[Action].ActionObject, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<SkillBase>();
        yield return StartCoroutine(ActionObject.UseSkill(PlayerNumber, true, Target, PlayerOrFoe));
        yield return StartCoroutine(ChangePosition(0));
    }


    IEnumerator ChangePosition(int Stance)
    {
        gameObject.transform.DOMoveY(Position[Stance], 0.12f);
        yield return new WaitForSeconds(0.12f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
