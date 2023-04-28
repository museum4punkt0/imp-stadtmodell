using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenDrying : MonoBehaviour
{
    public List<Animator> plants;
    public List<Animator> plantsToDry;
    public void GoDry()
    {
        plantsToDry = new List<Animator>();
        foreach (var plant in plants)
        {
            plantsToDry.Add(plant);
            Invoke("DryPlant", Random.Range(0, 3f));
        }
    }

    void DryPlant()
    {
        var randomIndex = Random.Range(0, plantsToDry.Count);

        plantsToDry[randomIndex].SetTrigger("Go");
        plantsToDry.RemoveAt(randomIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
