using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurnManager : MonoBehaviour
{
    public BattleIcon[] BattleIcons = new BattleIcon[6];
    public BattleIcon BattleSelection;
    public GameObject BattleIconParent;
    public GameObject BattleIconPrefab;

    public int Selection = 0;
    public int SelectionMax = 0;
    public bool Done;

    private bool UpButton;
    private bool LeftButton;
    private bool DownButton;
    private bool RightButton;
    private bool ConfirmButton;
    private bool BackButton;

    private BattleRoutine battleRoutine;


    // Start is called before the first frame update
    void Start()
    {
        battleRoutine = GameObject.Find("BattleRoutine").GetComponent<BattleRoutine>();
    }

    public IEnumerator ChooseBattleMenuOption(int Character)
    {
        ConstructMenu(Character, CharacterManager.Instance.Char[Character].BattleMenuArrangement);
        BattleIconParent.transform.DOMoveY(5f, 0.2f, false);
        BattleSelection.transform.DOMoveY(5f, 0.2f, false);
        UpdateSelection(Character, true);
        while(!Done)
        {
            if(LeftButton)
            {
                if(Selection != 0)
                {
                    UpdateSelection(Character, false);
                    Selection--;
                    UpdateSelection(Character, true);
                }
                else
                {
                    UpdateSelection(Character, false);
                    Selection = SelectionMax;
                    UpdateSelection(Character, true);
                }
            }
            else if(RightButton)
            {
                if(Selection != SelectionMax)
                {
                    UpdateSelection(Character, false);
                    Selection++;
                    UpdateSelection(Character, true);
                }
                else
                {
                    UpdateSelection(Character, false);
                    Selection = 0;
                    UpdateSelection(Character, true);
                }
            }
            else if(ConfirmButton)
            {
                BattleIconParent.transform.DOMoveY(8f, 0.2f, false);
                BattleSelection.transform.DOMoveY(8f, 0.2f, false);
                yield return StartCoroutine(SelectEnemy(Character));
            }
            
            yield return null;
        }

        yield return null;
    }
    
    public IEnumerator SelectEnemy(int Character)
    {
        //TODO: SELECT ENEMY CODE;
        Done = true;
        battleRoutine.Player[Character].Action = 0;  
        yield return null;
    }

    void ConstructMenu(int Character, int MenuArrangement)
    {
        BattleIconParent.transform.position = new Vector3(-8.2f, 8, 0);

        for (int i = 0; i < 6; i++)
        {
            if(BattleIcons[i] != null)
            {
                Destroy(BattleIcons[i]);
            }
        }

        for (int i = 0; i < CharacterManager.Instance.BattleMenuArrangementTable[MenuArrangement].BattleMenuIndices.Length; i++)
        {
            BattleIcons[i] = Instantiate(BattleIconPrefab, new Vector3(0, 0, 0), Quaternion.identity, BattleIconParent.transform).GetComponent<BattleIcon>();
            BattleIcons[i].Index = CharacterManager.Instance.BattleMenuArrangementTable[MenuArrangement].BattleMenuIndices[i];
            BattleIcons[i].gameObject.transform.localPosition = new Vector3((float)(i^2 + 217 * i - 218) / 200, 0, 0);
        }
       
        SelectionMax = CharacterManager.Instance.BattleMenuArrangementTable[MenuArrangement].BattleMenuIndices.Length-1;
    }

    void UpdateSelection(int Character, bool Mode)
    {
        if(Mode)
        {
            BattleIcons[Selection].IsSelected = true;
            BattleIcons[Selection].gameObject.transform.DOLocalMoveY(-0.5f, 0.2f, false);
        }
        else
        {
            BattleIcons[Selection].gameObject.transform.DOLocalMoveY(0f, 0.2f, false);
            BattleIcons[Selection].IsSelected = false;
        }

        BattleSelection.Index =  CharacterManager.Instance.BattleMenuArrangementTable[Character].BattleMenuIndices[Selection];
    }

    // Update is called once per frame
    void Update()
    {
        UpButton = Input.GetKeyUp(KeyCode.W);
        LeftButton = Input.GetKeyUp(KeyCode.A);
        DownButton = Input.GetKeyUp(KeyCode.S);
        RightButton = Input.GetKeyUp(KeyCode.D);

        ConfirmButton = Input.GetKeyUp(KeyCode.Z);
        BackButton = Input.GetKeyUp(KeyCode.X);

    }
}
