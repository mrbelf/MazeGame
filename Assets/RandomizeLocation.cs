using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeLocation : MonoBehaviour
{
    public float zOffSet;
    // Start is called before the first frame update
    MazeGenerator maze;
    private void Start()
    {
        maze = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>();

        Vector3 pos = maze.RandomPositionInMaze();
        pos.z = zOffSet;

        transform.position = pos;
    }
}
