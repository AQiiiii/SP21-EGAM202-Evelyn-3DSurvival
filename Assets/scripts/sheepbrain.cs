using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class sheepbrain : MonoBehaviour
{
    public enum SheepStateT
    {
        DecidingWhatToDoNext,
        SeekingWater,MovingToWater,DrinkingTillFull,Resting,
    }

    public float Water = 50, WaterLostPerSecond = 1, DrinkingSpeed = 10;
    public float Food = 50, FoodLostPerSecond = 1, EatingSpeed = 10;

    public SheepStateT currentState;

    NavMeshAgent thisNavMeshAgent;

    void Start()
    {
        Water = Random.Range(40f, 60f);
        Food = Random.Range(40f, 60f);
        thisNavMeshAgent = GetComponent<NavMeshAgent>();
        currentState = SheepStateT.DecidingWhatToDoNext;

    }

    void Update()
    {
        Water -= WaterLostPerSecond * Time.deltaTime;
        Food -= FoodLostPerSecond * Time.deltaTime;

        if (Food < 0 || Water < 0)
        {
            Destroy(this.gameObject);
        }
        switch(currentState)
        {
            case SheepStateT.DecidingWhatToDoNext:
                DecideWhatToDoNext();
                break;
            case SheepStateT.SeekingWater:
                SeekWater();
                break;
            case SheepStateT.MovingToWater:
                break;
            case SheepStateT.DrinkingTillFull:
                DrinkFromWater();
                break;
            default:
                throw new System.NotImplementedException("Whoops ran off the end of the case statement");

        }
    }

    public void DecideWhatToDoNext()
    {
        if(Water < 50)
        {
            currentState = SheepStateT.SeekingWater;
            return;
        }
    }

    public void SeekWater()
    {
   
        GameObject targetWateryObject = FindClosetObjectWithTag("Water");
        Debug.Log("Sheep is going to" + targetWateryObject.name);

        thisNavMeshAgent.SetDestination(targetWateryObject.transform.position);
        currentState = SheepStateT.MovingToWater;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentState == SheepStateT.MovingToWater && other.tag == "Water")
        {
            currentState = SheepStateT.DrinkingTillFull;
            Debug.Log("water");
        }
            
    }

    public void DrinkFromWater()
    {
        Water += DrinkingSpeed * Time.deltaTime;
        if (Water > 100)
            currentState = SheepStateT.DecidingWhatToDoNext;
    }

    public GameObject FindClosetObjectWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        if(objectsWithTag.Length == 0)
        {
            return null;
        }

        GameObject closetObject = objectsWithTag[0];
        float distanteToClosetObject = 1000000;
        float distanceToCurrentObject;
        Vector3 vectorToCurrentObject;
        GameObject currentObject;

        for(int i = 0; i<objectsWithTag.Length; i++)
        {
            currentObject = objectsWithTag[i];
            vectorToCurrentObject = currentObject.transform.position - transform.position;
            distanceToCurrentObject = vectorToCurrentObject.magnitude;

            if(distanceToCurrentObject <distanteToClosetObject)
            {
                closetObject = currentObject;
                distanteToClosetObject = distanceToCurrentObject;
            }
        }

        return closetObject;
    }



}
