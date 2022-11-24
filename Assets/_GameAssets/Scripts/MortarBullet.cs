using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBullet : MainBullet
{
    // Start is called before the first frame update

    // Update is called once per frame
    [SerializeField] float angle = 3f;
    [SerializeField] AnimationCurve curve;
    bool isUp;

    private void Awake()
    {
        isNotBaseBullet = true;
    }

    private void Update()
    {
        if (alive)
        {
            if (t<1 && !isUp)
            {
                t += Time.deltaTime * speed;

                transform.position = Vector3.Slerp(basePos, basePos + transform.forward * 20, curve.Evaluate(t));
            }
            if (t<1 && isUp)
            {
                t += Time.deltaTime * speed;

                Vector3 center = (basePos + clickedArea) * 0.5F;

                center -= new Vector3(0, angle + speedOffset / 10, 0);

                Debug.Log(angle + speedOffset / 10);

                // Interpolate over the arc relative to center
                Vector3 riseRelCenter = basePos - center;
                Vector3 setRelCenter = clickedArea - center;


                transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, curve.Evaluate(t));
                transform.position += center;
                transform.LookAt(clickedArea);
            }
            else if (!isUp)
            {
                t = 0;
                basePos = transform.position;
                isUp = true;
            }else
            {
                Unregister();
            }
            
        }
    }
}
