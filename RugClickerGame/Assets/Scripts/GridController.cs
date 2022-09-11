using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputCombinationSystem; 

public class GridController : MonoBehaviour
{
    public static GridController gridController;

    public Spool.Color color; 


    public List<GameObject> pinkSpoolPrefabs; 
    public List<GameObject> blueSpoolPrefabs; 
    public List<GameObject> greenSpoolPrefabs;

    public List<GameObject> spools; 
    public List<MergeSlot> mergeSlots;
    private Transform slotTransform;

    public static event Action OnSpoolSpawned; 

    private void Awake()
    {
        gridController = this; 
    }
    void Start()
    {
        InputController.OnLeftHoldDown += OnLeftHoldDown;
        InputController.OnLeftUp += OnLeftUp;

        UIManager.OnBuySpoolAction += OnBuySpool;
    }

    private void OnLeftUp()
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.transform.tag == "Spool")
            {

                hit.transform.localPosition = slotTransform.localPosition;
            }
        }
    }

    private void OnLeftHoldDown()
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            Debug.Log("Object Hit is " + hit.collider.gameObject.name);
            if (hit.transform.tag == "Spool")
            {
                Vector3 localPosition = transform.InverseTransformPoint(hit.point);
                localPosition.z = hit.transform.localPosition.z;

                hit.transform.localPosition = localPosition;
            }

        }
    }

    private void OnBuySpool()
    {
        foreach (var slot in mergeSlots)
        {
            if (!slot.isOccupied)
            {
                slot.isOccupied = true; 
                slotTransform = slot.transform;
                SpawnSpool();
                OnSpoolSpawned?.Invoke(); 
                break; 
            }
        }
    }

    private void SpawnSpool()
    {
        color = (Spool.Color)UnityEngine.Random.Range(0, 3);
        switch (color)
        {
            case Spool.Color.Pink:
                InstantiateSpool(color, pinkSpoolPrefabs[0]);  
                break;
            case Spool.Color.Blue:
                InstantiateSpool(color, blueSpoolPrefabs[0]); 
                break;
            case Spool.Color.Green:
                InstantiateSpool(color, greenSpoolPrefabs[0]); 
                break;
            default:
                break;
        }
    }

    private void InstantiateSpool(Spool.Color color, GameObject prefab)
    {
        GameObject spoolObj = Instantiate(prefab, transform); 
        Spool spool = spoolObj.GetComponent<Spool>();
        spool.transform.localPosition = slotTransform.localPosition;
        spool.gridTransform = transform; 
        spool.color = color;
        spools.Add(spoolObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
