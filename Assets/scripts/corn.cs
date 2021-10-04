using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corn : MonoBehaviour
{ 
    public enum CornfieldStateT { Seed,Seeding,Adult,Grow}
    public CornfieldStateT currentState;
    public int CornfieldID;
    public float Food, SeedFood, FoodGainedPerSecond, MaxFood;
    public float CrowdingDistance, MaxDispersalDistance;
    public float BirthTime, Age;
    public GameObject CornfieldPrefab;
    public Collider[] cornfieldHits;


    // Start is called before the first frame update
    void Start()
    {
        float terrainHeight = GameObject.Find("Terrain").GetComponent<Terrain>().SampleHeight(transform.position);
        transform.position = new Vector3(transform.position.x, terrainHeight, transform.position.z);

        cornfieldHits = Physics.OverlapSphere(transform.position, CrowdingDistance, LayerMask.GetMask("CornPlant"));
        if (cornfieldHits.Length > 1)
        {
            gameObject.SetActive(false);
            Object.Destroy(this.gameObject);
            return;
        }

        BirthTime = Time.time;
        Age = 0;
        currentState = CornfieldStateT.Seed;
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = new Vector3(transform.position.x, terrainHeight, transform.position.z);
        Food = SeedFood;


    }
   

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case CornfieldStateT.Seed:
                SeedUpdate();
                break;
            case CornfieldStateT.Seeding:
                SeedingUpdate();
                break;
            case CornfieldStateT.Adult:
                AdultUpdate();
                break;

        }

    }

    public void SeedUpdate()
    {
        Food += FoodGainedPerSecond * Time.deltaTime;
        Age = Time.time - BirthTime;
        if(Age > 5)
        {
            transform.localScale = new Vector3(2, 2, 2);
            currentState = CornfieldStateT.Seeding;
        }
    }

    public void SeedingUpdate()
    {
        Food += FoodGainedPerSecond * Time.deltaTime;
        Age = Time.time - BirthTime;
        if(Age>10)
        {
            transform.localScale = new Vector3(2, 4, 2);
            currentState = CornfieldStateT.Adult;
        }
    }
    public void AdultUpdate()
    {
        Food += FoodGainedPerSecond * Time.deltaTime;
        Age = Time.time - BirthTime;
        if(Food>MaxFood)
        {
            Vector3 randomNearbyPosition;
            randomNearbyPosition = transform.position + MaxDispersalDistance * Random.insideUnitSphere;
            Instantiate(CornfieldPrefab, randomNearbyPosition, Quaternion.identity, transform.parent);
            Food -= 2f * SeedFood;
        }
    }
}
