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
        if (other.gameObject.layer == LayerMask.NameToLayer("CornPlant"))
            Destroy(other.gameObject);
    }

}
