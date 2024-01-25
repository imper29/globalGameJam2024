using System;
using System.Collections;
using System.Collections.Generic;
using Marten;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private Transform[] organHolder;
    [SerializeField] private float pickingRange;
    [SerializeField] private LayerMask organLayer;
    private int _MessedUp;


    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            obj.transform.rotation = new Quaternion();
            gameObject.transform.position += Vector3.up * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            obj.transform.rotation = new Quaternion(0, 0, 0, 0);
            gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            obj.transform.rotation = new Quaternion(0, 180, 0, 0);
            gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            obj.transform.rotation = new Quaternion();
            gameObject.transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PickUpOrgan();
        }

        //Debug
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y - 2.9f, 0),
            new Vector2(transform.position.x, transform.position.y - 2.9f + pickingRange));
    }
    
    private void PickUpOrgan()
    {
        var charPos = transform.position;
        var ray = Physics2D.BoxCastAll(new Vector2(charPos.x, charPos.y/* - 2.9f*/), new Vector2(2, pickingRange), 0,
            Vector2.down);
        foreach (var hit in ray)
        {
            if (hit.transform.gameObject.GetComponent<Organ>() != null)
            {
                if (Vector2.Distance(transform.position,hit.transform.position) <= pickingRange)
                {
                    print(hit.transform.gameObject.name);
                    hit.transform.parent = organHolder[_MessedUp];
                    print("parent" + hit.transform.parent);    
                }
                
            }
        }
    }

    private void PlayerMessedUp()
    {
        _MessedUp++;
        if (_MessedUp >= 3)
        {
            //GameOver
        }
    }
}