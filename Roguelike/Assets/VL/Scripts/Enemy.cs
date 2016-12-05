using UnityEngine;
using System.Collections;
using System;
using Assets.VL.Scripts;

/// <summary>
/// 脚本的调用顺序
/// 
/// Awake()
/// Start()
/// 线程1
/// loop()
/// {
/// Update()
/// LateUpdate()
/// }
/// 线程2
/// loop()
/// {
/// FixedUpdate()
/// }
/// 
/// 
/// </summary>
namespace Assets.VL.Scripts
{
    public class Enemy : MovingObject
    {
        public int playerDamage;
        public AudioClip enemyAttack1;
        public AudioClip enemyAttack2;

        private Animator animator;
        private Transform target;
        private bool skipMove;

        /// <summary>
        /// Update函数第一次运行之前调用
        /// </summary>
        protected override void Start()
        {
            GameManager.instance.AddEnemyToList(this);
            animator = GetComponent<Animator>();
            target = GameObject.FindGameObjectWithTag(Constraints.Player_PlayerName).transform;
            base.Start();
        }

        /// <summary>
        /// 每帧调用
        /// </summary>
        void Update()
        {

        }

        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            if (skipMove)
            {
                skipMove = false;
                return;
            }

            base.AttemptMove<T>(xDir, yDir);
            skipMove = true;
        }
        public void MoveEnemy()
        {
            int xDir = 0;
            int yDir = 0;
            if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
                yDir = target.position.y > transform.position.y ? 1 : -1;
            else
                xDir = target.position.x > transform.position.x ? 1 : -1;
            //Move and Try Encounter
            AttemptMove<Player>(xDir, yDir);
        }
        protected override void OnCanAttack<T>(T component)
        {
            Player player = component as Player;
            player.LoseFood(playerDamage);
            animator.SetTrigger(Constraints.Enemy_Action_Attack);
            SoundManager.instance.Play(enemyAttack1, enemyAttack2);
        }
    }
}