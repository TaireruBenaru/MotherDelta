using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public enum WindowIndex
    {
        BattleMessage,
        Length
    };
    
    [System.Serializable]
    public struct WindowTableData
    {
        public Vector3 Position;
        public Vector3 TextPosition;
        public Vector2 WH;
        public Vector2 TextWH;
        public int SortingOrder;
        public string SortingLayer;
    }

    public GameObject WindowPrefab;
    public WindowTableData[] WindowTable;
    public GameObject[] Windows = new GameObject[(int)WindowIndex.Length];

    public static WindowManager Instance = null;

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

    public void CreateWindow(WindowIndex WindowToCreate)
    {
        Windows[(int)WindowToCreate] = Instantiate(WindowPrefab, WindowTable[(int)WindowToCreate].Position, Quaternion.identity);
        Windows[(int)WindowToCreate].GetComponent<SpriteRenderer>().size = WindowTable[(int)WindowToCreate].WH;
        Windows[(int)WindowToCreate].GetComponent<SpriteRenderer>().sortingOrder = WindowTable[(int)WindowToCreate].SortingOrder;
        Windows[(int)WindowToCreate].GetComponent<SpriteRenderer>().sortingLayerName = WindowTable[(int)WindowToCreate].SortingLayer;
    }
    public void DeleteWindow(WindowIndex WindowToDelete)
    {
        Destroy(Windows[(int)WindowToDelete]);
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
