using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshPro Textfield;
    public MeshRenderer Renderer;
    //public GameObject AdvancePrompt;

    public bool DoneWithText;

    public bool ConfirmButton;
    public int Index;
    public int MaxIndex;
    float CharWaitTime = 0.025f;

    public static DialogueManager Instance = null;

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

    
    public IEnumerator DisplayText(WindowManager.WindowIndex Window, string Text, float PauseTime)
    {
        WindowManager.Instance.CreateWindow(Window);
        Textfield.GetComponent<RectTransform>().position = WindowManager.Instance.WindowTable[(int)Window].TextPosition;
        Textfield.GetComponent<RectTransform>().sizeDelta = WindowManager.Instance.WindowTable[(int)Window].TextWH;

        Renderer.sortingOrder = WindowManager.Instance.WindowTable[(int)Window].SortingOrder + 1;
        Renderer.sortingLayerName = WindowManager.Instance.WindowTable[(int)Window].SortingLayer;

        DoneWithText = false;
        Renderer.enabled = false;
        Textfield.text = Text;
        Textfield.ForceMeshUpdate();
        MaxIndex = Textfield.textInfo.characterCount+1;
        Index = 0;
        Renderer.enabled = true;

        while (Index < MaxIndex)
        {
            Textfield.maxVisibleCharacters = Index;
            Index++;
            yield return new WaitForSeconds(CharWaitTime);
        }
        // AdvancePrompt.transform.DOScale(1, 0.2f);
        // yield return new WaitUntil(() => ConfirmButton == true);
        // AdvancePrompt.transform.DOScale(0, 0.5f);
        yield return new WaitForSeconds(PauseTime);
        WindowManager.Instance.DeleteWindow(Window);
        Renderer.enabled = false;
        DoneWithText = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
