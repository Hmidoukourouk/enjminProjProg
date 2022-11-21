using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBullet : MonoBehaviour
{

    public float speed = 2f;
    public bool alive;
    public PlayerShooting owner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (alive)
        {
            if (other.CompareTag("Wall"))
            {
                Debug.Log("mur");
                alive = false;
                owner.UnregisterBullet(this);

            }
            if (other.CompareTag("Player"))
            {
                other.gameObject.TryGetComponent<PlayerControler>(out PlayerControler shootingRef);
                if (shootingRef != null)
                {
                    if (shootingRef.playerNumber != owner.playerNumber)
                    {
                        Debug.Log("player");
                        alive = false;
                        owner.UnregisterBullet(this);
                    }
                }
            }
        }

    }

    public void Respawn(Vector3 pos, Quaternion rota)
    {
        transform.position = pos;
        transform.rotation = rota;
        alive = true;
        StopCoroutine(UnregisterOnDelay());
        StartCoroutine(UnregisterOnDelay());
    }

    IEnumerator UnregisterOnDelay()
    {
        yield return new WaitForSeconds(7f);
        owner.UnregisterBullet(this);
    }

    public void Init(PlayerShooting playerShootingRef)
    {
        owner = playerShootingRef;
        gameObject.SetActive(false);
    }

}
