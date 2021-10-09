using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apple : Item
{
    public override void Eat()
    {
        transform.parent.transform.localScale = new Vector3(5, 5, 5);
        Debug.Log("You eat the apple");
        transform.parent.GetComponent<AvatarController>().ItemInHands = null;
        Destroy(this.gameObject);
    }
}
