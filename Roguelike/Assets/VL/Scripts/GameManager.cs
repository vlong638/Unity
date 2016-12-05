using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.VL.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public float levelStartDelay = 1f;
        public float turnDelay = 0.01f;
        public static GameManager instance = null;
        public BoardManager boardScript;
        public int playerFoodPoints = 100;
        [HideInInspector]
        public bool PlayerTurn = true;

        private Text levelText;
        private GameObject levelImage;
        private int level = 0;
        private List<Enemy> enemies;
        private bool enemiesMoving;
        private bool doingSetup = true;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            enemies = new List<Enemy>();
            boardScript = GetComponent<BoardManager>();
            //InitGame();
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }
        void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            level++;
            InitGame();
        }

        private void InitGame()
        {
            doingSetup = true;
            levelText = GameObject.Find(Constraints.Text_Level).GetComponent<Text>();
            levelText.text = "Day " + level;
            levelImage = GameObject.Find(Constraints.Image_Level);
            levelImage.SetActive(true);
            Invoke(Constraints.GetFunctionName(HideLevelImage), levelStartDelay);
            enemies.Clear();
            boardScript.SetupScene(level);
        }
        private void HideLevelImage()
        {
            levelImage.SetActive(false);
            doingSetup = false;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (PlayerTurn || enemiesMoving|| doingSetup)
                return;

            StartCoroutine(MoveEnemie());
        }

        IEnumerator MoveEnemie()
        {
            enemiesMoving = true;
            yield return new WaitForSeconds(turnDelay);
            if (enemies.Count==0)
            {
                yield return new WaitForSeconds(turnDelay);
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].MoveEnemy();
                yield return new WaitForSeconds(enemies[i].moveTime);
            }
            PlayerTurn = true;
            enemiesMoving = false;
        }
        public void GameOver()
        {
            levelText.text = "After" + level + "days, you staved.";
            levelImage.SetActive(true);
            enabled = false;
        }
        public void AddEnemyToList(Enemy script)
        {
            enemies.Add(script);
        }

    }
}
