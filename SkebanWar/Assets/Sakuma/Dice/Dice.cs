using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidbody;
    [SerializeField]
    float pow;
    [SerializeField]
    Camera diceCamera;
    GameObject clickedGameObject;
    bool Catch = false;
    Vector2 vector;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input .GetKeyDown (KeyCode.H))
        {
           
        }


        if (Input.GetMouseButtonDown(0))
        {
            
            clickedGameObject = null;

            Ray ray = diceCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                clickedGameObject = hit.collider.gameObject;
            }

            if(this.gameObject == clickedGameObject)
            {
                Debug.Log("触りますた");
                Catch = true;
                vector= Input.mousePosition;
            }
        }


        if (Input.GetMouseButtonUp(0)&& Catch)
        {
            
            Vector2 nevec= Input.mousePosition;
            Debug.Log(nevec);
            Catch = false ;
            Push(( vector- nevec).normalized);
        }



    }

    public void Push(Vector3 vector)
    {
        rigidbody.AddForce(vector * pow);
        
    }





}
