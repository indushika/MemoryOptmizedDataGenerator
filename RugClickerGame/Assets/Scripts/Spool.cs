using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputCombinationSystem;
using System;

public class Spool : MonoBehaviour
{
    public Transform gridTransform;
    public bool isOverSlot;
    public bool isMoving; 
    public Transform slotTransform;
    public Vector3 gridPosition;

    public Color color;
    public int gridSlotID = -1;

    public enum Color
    {
        Pink,
        Blue,
        Green
    }

    void Start()
    {
        
        gridPosition = transform.localPosition; 
        //InputController.OnLeftHoldDown += OnLeftHoldDown;
        //InputController.OnLeftUp += OnLeftUp; 
    }

    //private void OnLeftUp()
    //{
    //    RaycastHit hit = new RaycastHit();

    //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
    //    {
    //        if (hit.transform == transform)
    //        {

    //                hit.transform.localPosition = slotTransform.localPosition;
    //                isOverSlot = false;
                
    //            //else
    //            //{
    //            //    hit.transform.localPosition = gridPosition;
    //            //}
    //        }
    //    }

    //}

    //private void OnLeftHoldDown()
    //{
    //    RaycastHit hit = new RaycastHit();

    //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
    //    {
    //        Debug.Log("Object Hit is " + hit.collider.gameObject.name);
    //        if (hit.transform == transform)
    //        {
    //            Vector3 localPosition = gridTransform.InverseTransformPoint(hit.point);
    //            localPosition.z = transform.localPosition.z;
               
    //            hit.transform.localPosition = localPosition;
    //        }

    //    }
    //}


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Slot")
    //    {
    //        if (!other.GetComponent<MergeSlot>().isOccupied)
    //        {
    //            isOverSlot = true;
    //            slotTransform = other.transform;
    //            other.GetComponent<MergeSlot>().isOccupied = true; 
    //        }

    //    }
    //    else if (other.tag == "GridSlot")
    //    {
    //        if (other.GetComponent<GridSlot>())
    //        {
    //            if (!other.GetComponent<GridSlot>().isOccupied)
    //            {
    //                isOverSlot = true;
    //                gridSlotID = other.GetComponent<GridSlot>().slotID;
    //                slotTransform = other.transform;
    //                other.GetComponent<GridSlot>().isOccupied = true; 
    //            }


    //        }
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Slot")
        {
            if (!other.GetComponent<MergeSlot>().isOccupied)
            {
                isOverSlot = true;
                slotTransform = other.transform;
                other.GetComponent<MergeSlot>().isOccupied = true;
            }
        }
        else if (other.tag == "GridSlot")
        {
            if (other.GetComponent<GridSlot>())
            {
                if (!other.GetComponent<GridSlot>().isOccupied)
                {
                    isOverSlot = true;
                    gridSlotID = other.GetComponent<GridSlot>().slotID;
                    slotTransform = other.transform;
                    other.GetComponent<GridSlot>().isOccupied = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Slot")
        {
            if (other.GetComponent<MergeSlot>())
            {
                other.GetComponent<MergeSlot>().isOccupied = false;
                isOverSlot = false;
            }
        }
        else if (other.tag == "GridSlot")
        {
            if (other.GetComponent<GridSlot>())
            {
                other.GetComponent<GridSlot>().isOccupied = false;
                isOverSlot = false;
            }
        }

    }
}
