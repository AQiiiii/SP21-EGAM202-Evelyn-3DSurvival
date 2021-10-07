using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : Item
{
   public override void Use()
    {
        GetComponent<Animator>().SetTrigger("Swing");
    }

    private void OnTriggerEnter(Collider other)
    {

    }

}
