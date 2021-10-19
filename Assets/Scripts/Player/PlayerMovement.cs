﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    private void Awake() {
        // Mendapat nilai mask dari layer Floor
        floorMask = LayerMask.GetMask("Floor");

        // Mendapatkan komponen Animator
        anim = GetComponent<Animator>();

        // Mendapat komponen rigidBody
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        // Mendapat nilai input horizontal (-1,0,1)
        float h = Input.GetAxisRaw("Horizontal");

        // Mendapat nilai input vertical (-1,0,1)
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    // Method (function) agar player bisa jalan
    void Move(float h, float v){
        // Set nilai x & y
        movement.Set(h, 0f, v);

        // Normalisasi nilai vector agar total panjang vector = 1
        movement = movement.normalized*speed*Time.deltaTime;

        // Move to position
        playerRigidbody.MovePosition(transform.position+movement);
    }

    void Turning(){
        // Buat Ray dari posisi mouse di layar
        Ray camRay= Camera.main.ScreenPointToRay(Input.mousePosition);

        // Buat raycast untuk floorHit
        RaycastHit floorHit;

        // Lakukan raycast
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Mendapat vector dari posisi player dan floorHit
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            // Mendapat look rotation baru ke hit position
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Rotasi player
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v){
        bool walking = h!=0f || v!=0f;
        anim.SetBool("IsWalking", walking);
    }
}
