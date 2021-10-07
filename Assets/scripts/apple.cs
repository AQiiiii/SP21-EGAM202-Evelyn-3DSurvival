using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apple : Item
{
    public override void Eat()
    {
        transform.parent.transform.localScale = new Vector3(3, 3, 3);
    }
}
