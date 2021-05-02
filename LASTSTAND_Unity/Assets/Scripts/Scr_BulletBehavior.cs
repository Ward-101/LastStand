using UnityEngine;

public class Scr_BulletBehavior : MonoBehaviour
{
    public int bulletDamage = 0;
    [SerializeField] private float bulletSpeed = 0.2f;

    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed, Space.Self);
    }
}
