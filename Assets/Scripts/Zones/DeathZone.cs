using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
/*    private Vector3 respawn;*/
    public new GameObject camera;
    public int damage;

    private void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
            int hp = PlayerMove.instance.hp;
            if (hp != 0)
            {
                StartCoroutine(PauseAndTeleport(collision));
            }
        }
    }

    private IEnumerator PauseAndTeleport(Collider2D collision)
    {
        yield return new WaitForSeconds(0.25f);
        Transform respawnTransform = new GameObject().transform;
        respawnTransform.position = CurrentSceneManager.instance.respawnPoint;
        collision.transform.position = respawnTransform.position;
        camera.transform.position = new Vector3(respawnTransform.position.x, respawnTransform.position.y, camera.transform.position.z);
        Destroy(respawnTransform.gameObject);
    }

}
