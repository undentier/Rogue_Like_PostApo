using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_ALX : MonoBehaviour
{
    public AnimationCurve dashSpeed;
    public float duration;
    public float maxSpeed;
   
    private bool isDashing;

    public float moveSpeed;

    public Rigidbody2D rb;
    public Animator animator;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    private float verticalDelta;
    private float horizontalDelta;

    public float direction;
    private bool isMoving;

    public GameObject gun; // face right
    public GameObject gun1; // face left

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isDashing) StartCoroutine(DashRoutine());

        playerSpeed(); //vitesse du joueur

        //Creation des imput direction et position de la cam

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Creation de la position de la souris par rapport au perso

        verticalDelta = mousePos.x - transform.position.x;
        horizontalDelta = mousePos.y - transform.position.y;

        //End

        if (movement != new Vector2(0, 0))
        {
            isMoving = true;
        } //Le joueur est il en mouvement ?

        gunDirection(); //Direction du regard

        //Gestion des animations
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("direction", direction);
        animator.SetBool("moving", isMoving);
        //End

    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    void playerSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift)) //Sprint pour le moment
        {
            moveSpeed = 15f;
        }
        else
        {
            moveSpeed = 8f;//Bonne vite
        }
    } //Fonction qui règle la vitesse du joueur.

    void gunDirection()
    {
        if (verticalDelta >= 0 && horizontalDelta >= 0)
        {
            if ((verticalDelta - horizontalDelta) >= 0)
            {
                direction = 0; //regarde en haut
                gun.SetActive(true);
                gun1.SetActive(false);
            }
            else
            {
                direction = 1; // regarde a droite
                gun.SetActive(false);
                gun1.SetActive(true);
            }
        }
        else if (verticalDelta >= 0 && horizontalDelta < 0)
        {
            horizontalDelta = -horizontalDelta;
            if ((verticalDelta - horizontalDelta) >= 0)
            {
                direction = 0; //regarde en haut
                gun.SetActive(true);
                gun1.SetActive(false);

            }
            else
            {
                direction = 3; // regarde a gauche
                gun.SetActive(true);
                gun1.SetActive(false);
            }
        }

        if (verticalDelta < 0 && horizontalDelta >= 0)
        {
            verticalDelta = -verticalDelta;
            if ((verticalDelta - horizontalDelta) >= 0)
            {
                direction = 2; //regarde en bas
                gun.SetActive(false);
                gun1.SetActive(true);
            }
            else
            {
                direction = 1; // regarde a droite
                gun.SetActive(false);
                gun1.SetActive(true);
            }
        }
        else if (verticalDelta < 0 && horizontalDelta < 0)
        {
            horizontalDelta = -horizontalDelta;
            verticalDelta = -verticalDelta;
            if ((verticalDelta - horizontalDelta) >= 0)
            {
                direction = 2; //regarde en bas
                gun.SetActive(false);
                gun1.SetActive(true);
            }
            else
            {
                direction = 3; // regarde a gauche
                gun.SetActive(true);
                gun1.SetActive(false);
            }
        }
    } 

    // Fonction pour le Dash
    private IEnumerator DashRoutine()
    {
        isDashing = true;
        float timer = 0.0f;

        while (timer < duration)
        {
            transform.position += (Vector3) movement * maxSpeed * dashSpeed.Evaluate(timer / duration);

            timer += Time.deltaTime;
            yield return null;
        }

        isDashing = false;

    }
}
