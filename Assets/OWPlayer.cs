using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OWPlayer : MonoBehaviour
{
    public Vector2 Direction;
    public Rigidbody2D rigidBody;
    
    private bool UpButton;
    private bool LeftButton;
    private bool DownButton;
    private bool RightButton;
    private bool ConfirmButton;
    private bool BackButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpButton = Input.GetKey(KeyCode.W);
        LeftButton = Input.GetKey(KeyCode.A);
        DownButton = Input.GetKey(KeyCode.S);
        RightButton = Input.GetKey(KeyCode.D);

        ConfirmButton = Input.GetKey(KeyCode.Z);
        BackButton = Input.GetKey(KeyCode.X);

        Direction = new Vector2(Convert.ToInt32(RightButton) - Convert.ToInt32(LeftButton), -(Convert.ToInt32(DownButton) - Convert.ToInt32(UpButton)));

    }

    void FixedUpdate()
    {
        if(Direction != Vector2.zero)
        {
            rigidBody.MovePosition(rigidBody.position + Direction * 2 * Time.fixedDeltaTime);
        }
    }
}
