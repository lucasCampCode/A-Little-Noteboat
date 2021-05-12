using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    public void ToggleCube()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
