using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    bool IsMove = false;
    [SerializeField]
    GameObject wall = null;
    float timer = 0;
    [SerializeField]
    bool deleteWall = false;
    [SerializeField]
    LayerMask layerMask;
    public bool IsDice=false ;
    bool lastAc = false;

    public bool diceEnd=false ;

    public void DiceSet()
    {
        diceEnd = false;
        IsDice = true;
        transform.localPosition = Vector3.zero;
        wall.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            DiceSet();
        }


        if (IsDice)
        {


            if (Input.GetMouseButtonDown(0))
            {

                clickedGameObject = null;

                Ray ray = diceCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit))
                {
                    clickedGameObject = hit.collider.gameObject;
                }

                if (this.gameObject == clickedGameObject)
                {


                    Catch = true;
                    vector = Input.mousePosition;
                }
            }



            if (Input.GetMouseButtonUp(0) && Catch)
            {

                Vector2 nevec = Input.mousePosition;
                lastAc = false;
                Catch = false;
                Push((vector - nevec).normalized);
                IsMove = true;
                timer = 0;
            }

            if (IsMove)
            {
                timer += Time.deltaTime;


                if (rigidbody.velocity.magnitude < 0.01f && timer > 0.5f)
                {
                   
                    
                    int ret = Check();
                    if (ret == 0)
                    {
                        if (!lastAc)
                        {
                            lastAc = true;
                            wall.SetActive(!deleteWall);
                            rigidbody.AddForce(Vector3.up);
                        }
                    }
                    else
                    {
                        IsMove = false;
                        UnityEngine.Debug.Log(ret);
                        diceEnd = true;
                    }

                    
                }
            }


        }

    }

    public void Push(Vector3 vector)
    {
        rigidbody.AddForce(vector * pow);

    }
    private int Check()
    {
        RaycastHit hit;
        Ray ray;

        for(int i = 0; i < 6; i++)
        {
            switch(i){
                case 0:
                    ray = new Ray(transform.position, transform.up);
                    if (Physics.Raycast(ray, out hit, 0.51f, layerMask))
                    {
                        return 3;
                    }
                    break;
                case 1:
                    ray = new Ray(transform.position, -transform.up);
                    if (Physics.Raycast(ray, out hit, 0.51f, layerMask))
                    {
                        return 4;
                    }
                    break;
                case 2:
                    ray = new Ray(transform.position, transform.right  );
                    if (Physics.Raycast(ray, out hit, 0.51f, layerMask))
                    {
                        return 6;
                    }
                    break;
                case 3:
                    ray = new Ray(transform.position, -transform.right);
                    if (Physics.Raycast(ray, out hit, 0.51f, layerMask))
                    {
                        return 1;
                    }
                    break;
                case 4:
                    ray = new Ray(transform.position, transform.forward );
                    if (Physics.Raycast(ray, out hit, 0.51f, layerMask))
                    {
                        return 2;
                    }
                    break;
                default:
                    ray = new Ray(transform.position, -transform.forward);
                    if (Physics.Raycast(ray, out hit, 0.51f, layerMask))
                    {
                        return 5;
                    }
                    break;
            }




        }

        return 0;
    }




}
