using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmitterBehaviour : MonoBehaviour
{
    [Tooltip("A reference to the game object that the emitter will spawn. Must have BulletBehaviour attached to it.")]
    [SerializeField]
    private GameObject _bullet;
    public GameObject Bullet { get { return _bullet; } }

    /// <summary>
    /// Spawns a bullet and applies the given force.
    /// </summary>
    /// <param name="force"></param>
    public void Fire(Vector3 force, float Scale = 1)
    {
        if (gameObject.activeSelf)
        {
            //Spawn a new bullet
            GameObject firedBullet = Instantiate(_bullet, transform.position, transform.rotation);
            //Get a reference to the attached bullet script
            BulletBehaviour bulletScript = firedBullet.GetComponent<BulletBehaviour>();

            //If the script isn't null, apply a force to its rigidbody
            if (bulletScript)
            {
                //applies the host tag
                bulletScript.Host = gameObject.tag;
                bulletScript.Rigidbody.AddForce(force, ForceMode.Impulse);
                firedBullet.transform.localScale = new Vector3(1,1,1) * Scale;
            }
        }
    }
}
