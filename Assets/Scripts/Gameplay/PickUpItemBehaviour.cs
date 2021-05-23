using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _powerUp;
    [SerializeField]
    private float _itemLifeTime = 2;
    private float _time;

    // Update is called once per frame
    void Update()
    {
        if (_powerUp == null)
            return;

        _time += Time.deltaTime;

        if (_time > _itemLifeTime)
        {
            Debug.Log("time");
            _powerUp.GetComponent<PowerUp>().EndUpgrade();
            Destroy(_powerUp);
            _powerUp = null;
            _time = 0;
        }
        else
        {
            _powerUp.GetComponent<PowerUp>().StartUpgrade();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            if (_powerUp != null)
            {
                Debug.Log("replace");
                _powerUp.GetComponent<PowerUp>().EndUpgrade();
                Destroy(_powerUp);
                _powerUp = null;
                _time = 0;
            }
            _powerUp = collision.gameObject;
            collision.gameObject.SetActive(false);
        }
    }
}
