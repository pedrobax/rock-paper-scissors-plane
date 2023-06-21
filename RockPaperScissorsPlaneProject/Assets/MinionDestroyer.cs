using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionDestroyer : MonoBehaviour
{
    public GameObject minion01, minion02;

    private void OnDestroy() {
        Destroy(minion01);
        Destroy(minion02);
    }
}
