using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class AnimationManager : MonoBehaviour
{
    Rigidbody2D m_rb;
    Animator m_animator;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        if (!(m_rb && m_animator)) 
        {
            if (!m_rb)
                Debug.LogWarning("No Rigidbody2D found!");

            if (!m_animator)
                Debug.LogWarning("No Animator found!");

            this.enabled = false;
        }
    }

    void Update()
    {
        var speed = m_rb.velocity.magnitude;
        m_animator.SetFloat("Speed",speed);

        if(speed > .1f)
        UpdateRotation();
    }

    private void UpdateRotation() 
    {
        transform.rotation = Quaternion.LookRotation(m_rb.velocity,Vector3.back);
    }
}
