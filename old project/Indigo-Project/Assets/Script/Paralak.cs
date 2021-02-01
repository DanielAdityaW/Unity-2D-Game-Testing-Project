using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralak : MonoBehaviour
{
    private float durasi, posisiMulai;
    public GameObject cam;
    public float efekParalak;
    void Start()
    {
        posisiMulai = transform.position.x;
        durasi = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float tempo = (cam.transform.position.x * (1- efekParalak));
        float jarak = (cam.transform.position.x * efekParalak);

        transform.position = new Vector3(posisiMulai + jarak, transform.position.y, transform.position.z);

        if(tempo>posisiMulai + durasi) posisiMulai += durasi;
        else if(tempo<posisiMulai - durasi) posisiMulai -= durasi;
    }
}
