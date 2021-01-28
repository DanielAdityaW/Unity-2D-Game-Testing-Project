using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public CharacterController2D kontrol;
    private float gerakHorizontal = 0f;
    public float kecepatanGerak;
    private bool lompat = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gerakHorizontal = Input.GetAxisRaw("Horizontal") * kecepatanGerak;
        if (Input.GetKey(KeyCode.LeftShift)){
            kecepatanGerak = 75f;
        } else {
            kecepatanGerak = 30f;
        }
        if (Input.GetButtonDown("Jump")){
            lompat = true;
        } 
    }

    void FixedUpdate() 
    {
        kontrol.Move(gerakHorizontal * Time.fixedDeltaTime , false, lompat);
        lompat = false;
    }
}
