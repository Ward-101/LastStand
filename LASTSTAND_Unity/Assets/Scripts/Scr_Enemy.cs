using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Enemy : MonoBehaviour
{
    [SerializeField] private int maxLife;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int collisionDamage;

    public int currentLife;
    private GameObject player;

    private void Awake()
    {
        player = Scr_CharacterController.instance.gameObject;
        currentLife = maxLife;
    }

    private void Update()
    {
        Move();
    }

    

    private void Move()
    {
        Vector3 directionVec = (player.transform.position - transform.position).normalized;

        transform.Translate(directionVec * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Scr_CharacterHealthManager>().TakeDamage(collisionDamage);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "PlayerBullet")
        {
            currentLife -= other.GetComponent<Scr_BulletBehavior>().bulletDamage;
            other.gameObject.SetActive(false);

            if (currentLife <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
