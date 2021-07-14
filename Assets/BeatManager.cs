using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public float BPM = 120;
    public float BPS = 0;
    public float BeatLength = 2;

    public float Threshold = 250;

    public bool Beat;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BPS = (BPM / 60);
        BeatLength = (float)((1 / BPS) * 1000);
        Threshold = (float)(BeatLength / 2);
        Beat = Threshold > ((AudioManager.Instance.BGM.time * 1000)  % BeatLength);
    }
}
