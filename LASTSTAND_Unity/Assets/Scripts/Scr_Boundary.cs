using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Boundary : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            other.gameObject.SetActive(false);
        }
    }
}
