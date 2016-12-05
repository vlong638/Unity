using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.VL.Scripts
{
    public class BoardManager : MonoBehaviour
    {
        #region Init Details
        [Serializable]
        public class Count
        {
            public int Minimum;
            public int Maximum;

            public Count(int min, int max)
            {
                Minimum = min;
                Maximum = max;
            }
        }

        public int columns = 8;
        public int rows = 8;
        public GameObject exit;
        public GameObject[] floorTiles;
        public GameObject[] outerWallTiles;
        public Count wallCount = new Count(5, 9);
        public GameObject[] wallTiles;
        public Count foodCount = new Count(1, 5);
        public GameObject[] foodTiles;
        public GameObject[] enemyTiles;

        private Transform boardHolder;
        private List<Vector3> gridPositions = new List<Vector3>();

        void SetupBoard()
        {
            boardHolder = new GameObject("Board").transform;
            for (int i = -1; i < columns + 1; i++)
            {
                for (int j = -1; j < rows + 1; j++)
                {
                    GameObject floor;
                    if (i == -1 || i == columns || j == -1 || j == columns)
                        floor = GetRandomObject(outerWallTiles);
                    else
                        floor = GetRandomObject(floorTiles);
                    GameObject floorInstance = Instantiate(floor, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    floorInstance.transform.SetParent(boardHolder);
                }
            }
        }
        void InitializePositions()
        {
            gridPositions.Clear();
            //可移动格子
            for (int i = 1; i < columns - 1; i++)
            {
                for (int j = 1; j < rows - 1; j++)
                {
                    gridPositions.Add(new Vector3(i, j, 0f));
                }
            }
        }
        void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
        {
            int objectCount = Random.Range(minimum, maximum + 1);
            for (int i = 0; i < objectCount; i++)
            {
                Vector3 randomPosition = GetRandomVector();
                GameObject tileChoice = GetRandomObject(tileArray);
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
        }
        #endregion

        #region Supports
        GameObject GetRandomObject(GameObject[] objects)
        {
            return objects[Random.Range(0, objects.Length)];
        }
        object randomLocker = new object();
        Vector3 GetRandomVector()
        {
            //lock (randomLocker)
            //{
            //    int randomIndex = Random.Range(0, gridPositions.Count);
            //    Vector3 randomPosition = gridPositions[randomIndex];
            //    gridPositions.RemoveAt(randomIndex);
            //    return randomPosition;
            //}
            //TODO 生成内容重叠
            Vector3 randomPosition = gridPositions[Random.Range(0, gridPositions.Count)];
            gridPositions.Remove(randomPosition);
            return randomPosition;
        }
        #endregion

        public void SetupScene(int level)
        {
            SetupBoard();
            InitializePositions();
            LayoutObjectAtRandom(wallTiles, wallCount.Minimum, wallCount.Maximum);
            LayoutObjectAtRandom(foodTiles, foodCount.Minimum, foodCount.Maximum);
            int enemyCount = (int)Mathf.Log(level, 2f);
            LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount + 1);
            Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
