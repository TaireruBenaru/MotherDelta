using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleIcon : MonoBehaviour
{
    public CharacterManager.BattleMenuIndex Index;
    public bool IsSelected = false;
    private SpriteRenderer spriteRenderer;
    public Sprite[] BattleIconSpritesPlain;
    public Sprite[] BattleIconSpritesSelected;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsSelected)
        {
            spriteRenderer.sprite = BattleIconSpritesSelected[(int)Index];
        }
        else
        {
            spriteRenderer.sprite = BattleIconSpritesPlain[(int)Index];
        }
    }
}
