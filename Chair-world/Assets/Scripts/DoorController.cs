using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator _animator;

    private void Start() => _animator = GetComponent<Animator>();

    public void Open() => _animator.SetTrigger("Open");
}
