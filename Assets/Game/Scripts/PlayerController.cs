using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int hpPlayer = 3;
    public GameObject bulletPrefab;
    public Transform spawnPos;
    public float spawnTime = 1f;
    public float shoot2Delay = 5f;
    public float speed;
    public float bulletSpeed;
    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToTouch(Input.mousePosition);
        }
    }
    void MoveToTouch(Vector3 screenPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
        transform.position = Vector3.Lerp(transform.position, worldPosition, speed * Time.deltaTime);
    }
    IEnumerator SpawnObjects()
    {
        float startTime = Time.time;
        while (true)
        {
            ShootBullet(Vector2.up);
            yield return new WaitForSeconds(spawnTime);
            if (Time.time - startTime >= shoot2Delay)
            {
                ShootBullet(Quaternion.Euler(0, 0, 30) * Vector2.up);
                ShootBullet(Quaternion.Euler(0, 0, -30) * Vector2.up);
            }
            
        }
    }
    void ShootBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPos.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bulletEnemy")) {
            hpPlayer--;
            Destroy(collision.gameObject);
            if (hpPlayer <= 0)
            {                
                //gameOver
                UIManager.Instance.GameOver();
            }
        }
    }
}
