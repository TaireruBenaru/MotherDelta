using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public enum BattleActionIndex
    {
        WasteTurn = 0,
    };
    
   
    [System.Serializable]
    public struct BattleActionData
    {
        public GameObject ActionObject;
        public string UseText;
        public int PPCost;
    }

    public BattleActionData[] BattleActionTable;

    public static ActionManager Instance = null;

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
