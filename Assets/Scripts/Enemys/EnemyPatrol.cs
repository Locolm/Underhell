using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    public int damageOnCollision = 20;

    public AudioSource GrrMonster;
    public AudioSource MortMonster;

    public GameObject objectToDestroy;

    public SpriteRenderer graphics;
    private Transform target;
    private int destPoint=0;
    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized*speed*Time.deltaTime,Space.World);

        if(Vector3.Distance(transform.position, target.position)< 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;
        }
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(damageOnCollision);
            GrrMonster.Play();
        }
        if (collision.CompareTag("Sword"))
        {
            Destroy(objectToDestroy);
            MortMonster.Play();
        }


    }

}
