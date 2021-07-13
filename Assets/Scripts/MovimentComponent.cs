using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IGetInput))]

public class MovimentComponent : MonoBehaviour
{
    [SerializeField] private float speed;
    private IGetInput input;

    private void Start()
    {
        input = GetComponent<IGetInput>();
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = input.GetAxis() * speed;
    }
}
