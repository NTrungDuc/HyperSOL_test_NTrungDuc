using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static EnemyController instance;
    public static EnemyController Instance { get { return instance; } }
    private int hp = 3;
    public GameObject bulletEnemyPrefab;
    public Transform shootPoint;
    public float bulletSpeed;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(ShootBullets());
    }
    public IEnumerator ShootBullets()
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletEnemyPrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.down * bulletSpeed;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
            float waitTime = Random.Range(2f, 5f);
            yield return new WaitForSeconds(waitTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            hp--;
            Destroy(collision.gameObject);
            if (hp <= 0)
            {
                UIManager.Instance.updateScore(1);
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            //gameOver
            UIManager.Instance.GameOver();
        }
    }
}
