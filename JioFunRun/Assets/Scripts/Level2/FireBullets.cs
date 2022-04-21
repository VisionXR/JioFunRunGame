using System.Collections;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
    public GameObject Bullet, BulletPos;
    public float FiringRate, Force;
    public float DestroyTime = 6;
    void Start()
    {
        StartCoroutine(Fire());
    }
    public IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / FiringRate);
            GameObject tmpBullet = Instantiate(Bullet, BulletPos.transform.position, BulletPos.transform.rotation);
            tmpBullet.SetActive(true);
            tmpBullet.tag = "Collider";
            tmpBullet.GetComponent<Rigidbody>().AddForce(BulletPos.transform.up * Force);
            Destroy(tmpBullet, DestroyTime);
        }
    }
}
