using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeLocation : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        int x = Random.Range(0, 9);
        int y = Random.Range(0, 9);

        Vector3 pos = new Vector3(x + .5f, y + .5f, -0.2f);

        transform.position = pos;
    }
}
