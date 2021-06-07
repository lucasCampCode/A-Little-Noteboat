using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    /// <summary>
    /// what the upgrade should do at the start
    /// </summary>
    public abstract void StartUpgrade();
    /// <summary>
    /// what the upgrade should do when it ends
    /// </summary>
    public abstract void EndUpgrade();
}
