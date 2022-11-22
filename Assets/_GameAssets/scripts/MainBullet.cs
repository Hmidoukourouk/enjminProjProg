using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBullet : MonoBehaviour
{
    public static System.Action<PlayerControler> onPlayerHit;
    public float speed = 2f;
    public bool alive;
    public PlayerShooting owner;

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
                //Debug.Log("mur");
                alive = false;
                owner.UnregisterBullet(this);

            }
            if (other.CompareTag("Player"))
            {
                other.gameObject.TryGetComponent(out PlayerControler shootingRef);
                if (shootingRef != null)
                {
                    if (shootingRef.playerNumber != owner.playerNumber)
                    {
                        //Debug.Log("player");
                        onPlayerHit?.Invoke(shootingRef);
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
        if (owner.authority)
        {
            owner.UnregisterBullet(this);
        }

    }

    public void Init(PlayerShooting playerShootingRef)
    {
        owner = playerShootingRef;
        gameObject.SetActive(false);
    }

}
