using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public float speed = 3f;
    public CharacterController controller;
    public Animator anim;
    private bool doubleClick = false;
    private Vector3 position;

	// Use this for initialization
	void Start () {
        position = transform.position;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
    // TODO: detect double click
	void Update () {
        if (Input.GetMouseButton(0))
        {
            getPosition();
        }
        moveToPosition(doubleClick);
	}

    // get mouse's position
    void getPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if( Physics.Raycast(ray, out hit, 500) ) {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
    }

    // move player to the mouse's position
    void moveToPosition(bool doubleClick)
    {
        if (Vector3.Distance(transform.position, position) > 1.1)
        {
            // change player's lookat direction
            Quaternion newRotation = Quaternion.LookRotation(position - transform.position);
            newRotation.x = 0f;
            newRotation.z = 0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.time);

            // move player
            //transform.Translate(position - transform.position);
            if (doubleClick)
            {
                controller.SimpleMove(transform.forward * speed * 2);
                anim.Play("RUN00_F");
            }
            else
            {
                controller.SimpleMove(transform.forward * speed);
                anim.Play("WALK00_F");
            }
        }
        else
        {
            anim.Play("WAIT00");
        }    
    }
}
