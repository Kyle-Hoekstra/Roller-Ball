using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float startY;

    private float movementX;
    private float movementY;
    public float speed = 0;
    public float jumpForce = 200;
    public int jumpCount = 2;
    int currentJump = 1;
    bool canJump = true;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        rb = GetComponent <Rigidbody>();
        startY = rb.position.y;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void Update(){
        //Reset my jumps when I hit the ground
        jumpReset();
        if (currentJump == jumpCount){
            canJump = false;
        }

        if(Input.GetKeyDown("space")){
            //while I have jumps available
            if(canJump){
                currentJump++;
                Jump();
            }
        }

        //Debug.Log(rb.velocity.y);
    }



    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void Jump(){
        // Will apply force up regarless of sphere rotation
        rb.AddForce(Vector3.up * jumpForce);
    }

    void jumpReset(){
        if(rb.position.y == startY){
            currentJump = 1;
            canJump = true;
        }
    }

    void SetCountText(){
        countText.text = "Count: " + count.ToString();
        if (count >= 7){
            winTextObject.SetActive(true);
        }
    }

    private void FixedUpdate(){
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("PickUp")){
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

}
