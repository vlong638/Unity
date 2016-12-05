using UnityEngine;
using System.Collections;

namespace Assets.VL.Scripts
{
    public abstract class MovingObject : MonoBehaviour
    {
        public float moveTime = 0.01f;
        public LayerMask blockingLayer;

        private BoxCollider2D boxCollider;
        private Rigidbody2D rb2D;
        private float inverseMoveTime;

        protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(xDir, yDir);
            boxCollider.enabled = false;//当我们投射射线的时候 避免跟自身碰撞
            hit = Physics2D.Linecast(start, end, blockingLayer);
            boxCollider.enabled = true;
            if (hit.transform == null)
            {
                StartCoroutine(SmoothMovement(end));
                return true;
            }
            return false;
        }

        protected IEnumerator SmoothMovement(Vector3 end)
        {
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            while (sqrRemainingDistance > float.Epsilon)
            {
                Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);//TODO 此处含义
                rb2D.MovePosition(newPostion);//TODO 使用刚体表现对象的位置
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                yield return null;//Wait for a frame before reevaluation the condition of the loop
            }
        }

        protected virtual void AttemptMove<T>(int xDir, int yDir) where T : Component
        {
            RaycastHit2D hit;
            bool canMove = Move(xDir, yDir, out hit);
            //TODO 多余?
            if (hit.transform == null)
                return;
            T hitComponent = hit.transform.GetComponent<T>();
            if (!canMove && hitComponent != null)
                OnCanAttack(hitComponent);
        }

        protected abstract void OnCanAttack<T>(T component) where T : Component;


        // Use this for initialization
        protected virtual void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            rb2D = GetComponent<Rigidbody2D>();
            inverseMoveTime = 1f / moveTime;
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
