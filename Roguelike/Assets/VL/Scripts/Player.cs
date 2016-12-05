using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.VL.Scripts
{
    public class Player : MovingObject
    {
        public int wallDamage = 1;
        public int pointsPerFood = 10;
        public int pointsPerSoda = 20;
        public float restartLevelDelay = 0.5f;
        public Text foodText;
        public AudioClip moveSound1;
        public AudioClip moveSound2;
        public AudioClip eatSound1;
        public AudioClip eatSound2;
        public AudioClip drinkSound1;
        public AudioClip drinkSound2;
        public AudioClip gameOverSound;

        private Animator animator;
        private int food;
        private Vector2 touchOrigin = -Vector2.one;//-1.-1是一个屏幕外的点,用于检测是否有输入


        protected override void Start()
        {
            animator = GetComponent<Animator>();
            food = GameManager.instance.playerFoodPoints;
            UpdateFoodText(food);
            base.Start();
        }

        private void UpdateFoodText(int endFood, string change = "")
        {
            foodText.text = change + "Food:" + endFood;
        }

        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            food--;
            UpdateFoodText(food);
            base.AttemptMove<T>(xDir, yDir);
            RaycastHit2D hit;
            if (Move(xDir,yDir,out hit))
            {
                SoundManager.instance.Play(moveSound1, moveSound2);
            }
            CheckIfGameOver();
            GameManager.instance.PlayerTurn = false;
        }
        private void Update()
        {
            if (!GameManager.instance.PlayerTurn)
                return;

            int horizontal = 0;
            int vertical = 0;
#if UNITY_STANDALONE || UNITY_WEBPLAYER
            horizontal = (int)Input.GetAxisRaw(Constraints.Axis_Horizontal);
            vertical = (int)Input.GetAxisRaw(Constraints.Axis_Vertical);
            //防止斜向移动
            if (horizontal != 0)
                vertical = 0;
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
            if (Input.touchCount>0)
            {
                Touch myTouch = Input.touches[0];
                if (myTouch.phase==TouchPhase.Began)
                {
                    touchOrigin = myTouch.position;
                }
                else if (myTouch.phase==TouchPhase.Ended&&touchOrigin.x>=0)
                {
                    Vector2 touchEnd = myTouch.position;
                    float x = touchEnd.x -touchOrigin.x;
                    float y = touchEnd.y- touchOrigin.y;
                    touchOrigin.x = -1;
                    if (Mathf.Abs(x) > Mathf.Abs(y))
                        horizontal = x > 0 ? 1 : -1;
                    else
                        vertical = y > 0 ? 1 : -1;
                }
            }
#endif
            if (horizontal != 0 || vertical != 0)
                AttemptMove<Wall>(horizontal, vertical);
        }
        private void OnDisable()
        {
            GameManager.instance.playerFoodPoints = food;
        }
        private void CheckIfGameOver()
        {
            if (food <= 0)
            {
                SoundManager.instance.Play(gameOverSound);
                GameManager.instance.GameOver();
            }
        }
        protected override void OnCanAttack<T>(T component)
        {
            Wall hitWall = component as Wall;
            hitWall.DamageWall(wallDamage);
            animator.SetTrigger(Constraints.Player_Action_Chop);
        }
        private void Restart()
        {
            //TODO Application.LoadLevel()过时
            SceneManager.LoadScene("Scene1");
            //Application.LoadLevel(Application.loadedLevel);
        }
        public void LoseFood(int attack)
        {
            animator.SetTrigger(Constraints.Player_Action_Hit);
            food -= attack;
            UpdateFoodText(food, "-" + attack + " ");
            CheckIfGameOver();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == Constraints.Tag_Exit)
            {
                Invoke(Constraints.GetFunctionName(Restart), restartLevelDelay);
				enabled = false;
            }
            else if (collision.tag == Constraints.Tag_Food)
            {
                SoundManager.instance.Play(eatSound1, eatSound2);
                food += pointsPerFood;
                UpdateFoodText(food);
                collision.gameObject.SetActive(false);
            }
            else if (collision.tag == Constraints.Tag_Soda)
            {
                SoundManager.instance.Play(drinkSound1, drinkSound2);
                food += pointsPerSoda;
                UpdateFoodText(food);
                collision.gameObject.SetActive(false);
            }
        }
    }
}
