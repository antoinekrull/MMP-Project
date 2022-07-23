using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject player;

  
    void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPositon = currentPosition + velocity * Time.deltaTime;

        Debug.DrawLine(currentPosition, newPositon, Color.red);

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPositon);
        foreach(RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;
            Debug.Log(hit.collider);
            if(other != player)
            {
                if(hit.collider.Equals(other.GetComponent<BoxCollider2D>()))
                {
                    Destroy(gameObject);
                    EnemyAI enemy = other.GetComponent<EnemyAI>();                  
                    enemy.TakeDamage(1);                                       
                    break;
                }

                if (other.CompareTag("Walls"))
                {
                    Destroy(gameObject);
                    break;
                }

            }
            
        }

        transform.position = newPositon;
    }
}