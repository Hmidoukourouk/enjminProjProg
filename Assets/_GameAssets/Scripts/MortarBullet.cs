using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBullet : MainBullet
{

    // Start is called before the first frame update

    // Update is called once per frame
    [SerializeField] float angle = 3f;

    private void Awake()
    {
        isNotBaseBullet = true;
    }

    private void Update()
    {
        if (alive)
        {
            if (t<1)
            {
                t += Time.deltaTime * speed;

                Vector3 center = (basePos + clickedArea) * 0.5F;

                center -= new Vector3(0, angle, 0);

                // Interpolate over the arc relative to center
                Vector3 riseRelCenter = basePos - center;
                Vector3 setRelCenter = clickedArea - center;


                transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, t);
                transform.position += center;
            }
            else
            {
                Unregister();
            }
            
        }
    }
}
