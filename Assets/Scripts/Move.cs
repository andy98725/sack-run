using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{

    private Rigidbody2D physics;
    public Game game;
    public AudioSource jumpSound;

    public static float deathY = -15;
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);
    }


    void Update()
    {
        if (groundTime > 0) groundTime -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
            MouseDown();
        if (Input.GetMouseButtonUp(0))
            MouseUp();


        if (transform.position.y < deathY) game.Die();
    }

    private float mouseDownTime = 0;
    private Vector2 mouseDownPosition;
    void MouseDown()
    {
        mouseDownTime = Time.time;
        mouseDownPosition = Input.mousePosition;

    }
    void MouseUp()
    {
        float timeDelta = (Mathf.Clamp(Time.time - mouseDownTime, 0.1f, 0.25f) + 2f) / 2.25f;
        bool isGrounded = groundTime > 0;

        Vector2 force = ((Vector2)Input.mousePosition - mouseDownPosition) / timeDelta;
        force *= physics.mass * 0.0115f;

        if (!isGrounded)
        {
            force = new Vector2(force.x * 0.5f, force.y * 0.2f);
        }
        else
        {
            force = new Vector2(force.x * 1f, force.y * 1.1f);
        }

        physics.AddForce(new Vector2(force.x, force.y), ForceMode2D.Impulse);
        jumpSound.volume = Mathf.Clamp(force.magnitude / 10f, 0.25f, 1f);
        jumpSound.Play();
    }


    private float groundTime = 0;
    private ContactPoint2D[] contactPoints = new ContactPoint2D[10];

    void OnCollisionStay2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "ground":
                int contactCount = collision.GetContacts(contactPoints);
                for (int i = 0; i < contactCount; i++)
                {
                    if (Vector2.Dot(contactPoints[i].normal, Vector2.up) > 0.5f)
                    {
                        groundTime = 0.2f;
                        return;
                    }
                }
                break;

            case "bad":
                game.Die();
                break;
            case "good":
                game.Win();
                break;
        }

    }

}
