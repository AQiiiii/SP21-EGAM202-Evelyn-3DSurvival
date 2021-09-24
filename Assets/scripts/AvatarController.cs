using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public GameObject MouseBox;

    void Update()
    {
        Ray rayFromCameraToMouse;
        RaycastHit closetClickableGroundOnRay;

        rayFromCameraToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(rayFromCameraToMouse, out closetClickableGroundOnRay, Mathf.Infinity, LayerMask.GetMask("ClickableGround"));
        MouseBox.transform.position = closetClickableGroundOnRay.point;

        if (Input.GetMouseButton(0))
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(MouseBox.transform.position);
        }
        GetComponent<Animator>().SetFloat("WalkSpeed", .5f * GetComponent<UnityEngine.AI.NavMeshAgent>().velocity.magnitude);



    }
}
