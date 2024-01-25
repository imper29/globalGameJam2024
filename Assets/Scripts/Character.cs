using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private GameObject[] organHolder;
    [SerializeField] private float pickingRange;
    [SerializeField] private LayerMask organLayer;
    [SerializeField] private GameObject testObj;
    private int _MessedUp;
    private bool _hasAnOrgan;

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
        var ray = Physics2D.BoxCastAll(new Vector2(charPos.x, charPos.y /* - 2.9f*/), new Vector2(2, pickingRange), 0,
            Vector2.down);
        foreach (var hit in ray)
        {
            if (hit.transform.gameObject.GetComponent<Organ>() != null)
            {
                if (Vector2.Distance(transform.position, hit.transform.position) <= pickingRange)
                {
                    if (hit.transform.gameObject.GetComponent<Organ>().isInPlayerHands)
                        continue;
                    
                    if (_hasAnOrgan)
                    {
                        //Swap Organs
                        organHolder[_MessedUp].transform.GetChild(0).transform.position = hit.transform.position;
                        organHolder[_MessedUp].transform.GetChild(0).GetComponent<Organ>().isInPlayerHands = false;
                        // organHolder[_MessedUp].GetComponentInChildren<Organ>().transform.position =
                        //     hit.transform.position;
                        organHolder[_MessedUp].GetComponentInChildren<Organ>().transform.parent = null;
                    }
                    // Pick an Organ
                    print(hit.transform.gameObject.name);
                    hit.transform.parent = organHolder[_MessedUp].transform;
                    hit.transform.localPosition = new Vector3();
                    hit.transform.gameObject.GetComponent<Organ>().isInPlayerHands = true;
                    _hasAnOrgan = true;
                    return;
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