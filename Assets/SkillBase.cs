using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillBase : MonoBehaviour
{
    public bool UserIsPlayerOrFoe;
    public bool TargetIsPlayerOrFoe;
    public int UserID;
    public int TargetID;

    // Start is called before the first frame update
    public virtual void Start()
    {

    }


    public virtual IEnumerator UseSkill(int entityID, bool UserIsPlayerOrFoe, int targetID, bool TargetIsPlayerOrFoe)
    {
        yield return null;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
