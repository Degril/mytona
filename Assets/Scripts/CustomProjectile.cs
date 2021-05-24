using UnityEngine;

public class CustomProjectile : Projectile
{
    private const float rightMultiplier = 10;
    protected override void Update()
    {
        if (!destroyed)
        {
            transform.position += (transform.forward + transform.right * (Mathf.Sin(timer*rightMultiplier) * timer)) * (speed * Time.deltaTime);
                
        }

        timer += Time.deltaTime;
        if (timer > timeToLive)
        {
            Destroy(gameObject);
        }
    }
}
