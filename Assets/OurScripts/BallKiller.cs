using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallKiller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Killer());
    }
    
    // make it so that it uses the store? pattern. So when a ball hits it doesn't get destroyed it just gets moved. 
    // Some way of checking if the ball is no longer in the box.

    private IEnumerator Killer()
    {
        int maxThrowables = 15;
        int maxFireBalls = 5;

        while (true)
        {
            Throwable[] throwables = FindObjectsOfType<Throwable>();
            FireBall[] fireBalls = FindObjectsOfType<FireBall>();

            if (throwables.Length > maxThrowables)
            {
                foreach (Throwable throwable in throwables)
                {
                    if (throwable.name != "OriginalBall")
                    {
                        Destroy(throwable.gameObject);
                        break;
                    }
                }
            }

            if (fireBalls.Length > maxFireBalls)
            {
                foreach (FireBall fireBall in fireBalls)
                {
                    if (fireBall.name != "OriginalFire")
                    {
                        Destroy(fireBall.transform.parent.gameObject);
                        break;
                    }
                }
            }
            
            int yieldTime = Mathf.Abs((maxThrowables - throwables.Length > maxFireBalls - fireBalls.Length ? throwables.Length * 5 : fireBalls.Length * 10));

            yield return new WaitForSeconds(yieldTime);
        }
    }
}
