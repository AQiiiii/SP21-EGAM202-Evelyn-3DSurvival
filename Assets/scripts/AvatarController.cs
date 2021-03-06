using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public GameObject MouseBox;
    public Animator thisAnimator;
    public UnityEngine.AI.NavMeshAgent thisNavMeshAgent;
    public enum MovementStateT { StandStill, Walking}
    public MovementStateT currentState;
    public Item ItemInHands, ItemOnHead;
    public GameObject thedoorswitch;
    public GameObject door;

    private void Start()
    {
        thisAnimator = GetComponent<Animator>();
        thisNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentState = MovementStateT.StandStill;
    }

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

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (ItemInHands != null)
                Debug.Log("you can't get anything new, becasue your hands are full.");
            else
            {
                Collider[] overlappingItems;
                overlappingItems = Physics.OverlapBox(transform.position + 2 * Vector3.forward, 3 * Vector3.one, Quaternion.identity, LayerMask.GetMask("Item"));

                if (overlappingItems.Length == 0)
                    Debug.Log("There are no items in front of you");
                else
                {
                    if (ItemInHands != null)
                    {
                        ItemInHands.transform.SetParent(null);
                        ItemInHands = null;
                    }
                    ItemInHands = overlappingItems[0].GetComponent<Item>();
                    ItemInHands.transform.SetParent(gameObject.transform);
                    ItemInHands.transform.localPosition = new Vector3(0, 1, 1);
                    if (ItemInHands.name == "thedoorswitch")
                    {
                        door.SetActive(false);
                        Debug.Log("door open");
                    }
                    Debug.Log("You picked up a" + ItemInHands.name);
                    
                }
                
                
               
            }

           
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (ItemInHands != null)
            {
                ItemInHands.transform.SetParent(null);
                if (ItemInHands.name == "thedoorswitch")
                {
                    door.SetActive(true);
                    Debug.Log("door close");
                }
                Debug.Log("You dropped the" + ItemInHands.name);
                ItemInHands = null;
            }
            else
                Debug.Log("You can't drop an item, because you're not holding");

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ItemInHands != null)
                ItemInHands.GetComponent<Item>().Use();
            else
                Debug.Log("You can't use an item, because you're not holding anything.");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (ItemInHands != null)
                ItemInHands.GetComponent<Item>().Wear();
            else
                Debug.Log("You can't wear an itm, because you're not holding anything.");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ItemInHands != null)
                ItemInHands.GetComponent<Item>().Eat();
            else
                Debug.Log("You can't eat am item, because you're not holding anything");
        }

       

    }

    
}
