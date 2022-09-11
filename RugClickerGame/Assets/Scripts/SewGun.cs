using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks; 
public class SewGun : MonoBehaviour
{
    public float speed;
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float offset;
    public bool isMoving;
    public bool isActive;
    public bool isSewing;
    public bool isWaiting; 

    public int delay; 

    public static event Action Sew; 

    // Start is called before the first frame update
    void Start()
    {
        SewGunAnimController.SewAnimationEnded += OnSewEnded; 
        startPoint = transform.localPosition;
        endPoint = startPoint;
        endPoint.x -= offset;

    }

    private void OnSewEnded()
    {
        WaitForDelay(); 

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (!isMoving && !isSewing)
            {
                Sew?.Invoke();
                isSewing = true;
            }
            else if (isMoving && !isSewing)
            {
                LerpGun();
            }
        }

        
    } 

    public void LerpGun()
    {

        transform.localPosition = Vector3.Lerp(transform.localPosition, endPoint, Time.deltaTime * speed);
        ResetEndPoint(); 
    } 

    private async void WaitForDelay()
    {
        await Task.Delay(delay);
        isSewing = false;
        isMoving = true;
    }

    public void ResetEndPoint()
    {
        if (Vector3.Distance(transform.localPosition,endPoint)<0.01)
        {
            //saw square
            endPoint.x -= offset;
            isMoving = false; 
        }
    }
}
