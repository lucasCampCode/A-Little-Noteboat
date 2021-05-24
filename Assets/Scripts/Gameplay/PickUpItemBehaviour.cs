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
        //if powerup doesn't exists
        if (!_powerUp)
            return;//skip update
        //increment time
        _time += Time.deltaTime;
        //if time is greater than its the item lifetime
        if (_time > _itemLifeTime)
        {
            //end the current upgrade on the ship
            _powerUp.GetComponent<PowerUp>().EndUpgrade();
            Destroy(_powerUp);//destroy the current power up
            _powerUp = null;//just incase powerup is not set to null
            _time = 0;//reset time;
        }
        else
        {
            //start the power up if ther time is less that item lifetime
            _powerUp.GetComponent<PowerUp>().StartUpgrade();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))//if the collision tag is item
        {
            if (_powerUp)//if the current power up exist
            {
                //end the current upgrade on the ship
                _powerUp.GetComponent<PowerUp>().EndUpgrade();
                Destroy(_powerUp);//destroy the current power up
                _powerUp = null;//just incase powerup is not set to null
                _time = 0;//reset time;
            }
            //set the current power up to the powerup this object collided with
            _powerUp = collision.gameObject;
            collision.gameObject.SetActive(false);
        }
    }
}
