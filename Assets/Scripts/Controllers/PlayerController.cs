using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigid;
    Animator _anim;
    public Vector3 MoveVec { get; set; }

    Stat _stat;
    public Stat Stat { get { return _stat; } set { _stat = value; } }

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Player;

    Define.State _state = Define.State.Idle;

    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            _anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Die:
                    break;
                case Define.State.Idle:
                    _anim.Play("Idle");
                    break;
                case Define.State.Walk:
                    _anim.Play("Walk");
                    break;
                case Define.State.Jump:
                    _anim.Play("Jump");
                    break;
            }
        }
    }

    void Awake()
    {
        _stat = GetComponent<Stat>();
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        switch(State)
        {
            case Define.State.Die:
                break;
            case Define.State.Idle:
                break;
            case Define.State.Walk:
                UpdateWalk();
                break;
            case Define.State.Jump:

                break;
        }

        _rigid.velocity = Vector3.zero;
    }

    void UpdateDie()
    {

    }

    void UpdateWalk()
    {
        Vector2 nextVec = MoveVec * Stat.MoveSpeed * Time.fixedDeltaTime;
        _rigid.MovePosition(_rigid.position + nextVec);
    }
}
