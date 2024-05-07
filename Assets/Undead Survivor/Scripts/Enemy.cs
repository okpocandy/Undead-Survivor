using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public RuntimeAnimatorController[] animatorCon;
    public float monsterCurHp;
    public float montserMaxHp;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        spriter.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        monsterCurHp = montserMaxHp;
    }

    public void Init(SpawnData spawnData)
    {
        anim.runtimeAnimatorController = animatorCon[spawnData.spriteType];
        speed = spawnData.monsterSpeed;
        montserMaxHp = spawnData.monsterHp;
        monsterCurHp = montserMaxHp;
    }
}
