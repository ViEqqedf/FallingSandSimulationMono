using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace {
    public class GameManager : MonoBehaviour {
        #region 单例相关

        public static GameManager instance { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        public Empty emptyPrefab;
        public Water waterPrefab;
        public GameObject sandPoolParent;
        private const float tickInterval = 0.5f;
        private float countDownTimer = 0;
        private int simulationCount = 1;

        public Pool Pool {
            get { return pool ??= new Pool(); }
        }
        private Pool pool;

        private void Start() {
            pool = new Pool();
        }

        private void Update() {
            countDownTimer += Time.deltaTime;
            if (countDownTimer >= tickInterval) {
                for (int i = pool.poolSize - 1, countX = -1; i > countX; i--) {
                    for (int j = pool.poolSize - 1, countY = -1; j > countY; j--) {
                        pool.container[i, j].SandUpdate(simulationCount);
                    }
                }

                countDownTimer = 0;
                simulationCount++;
            }

            #region 交互相关

            // 点击生成沙粒
            if (Input.GetMouseButtonUp(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    Vector3 hitPoint = hit.point;
                    Debug.Log($"本次点击的区域是{hitPoint}");
                    if (Mathf.Abs(hitPoint.x) < pool.poolSize + 0.5f &&
                        Mathf.Abs(hitPoint.y) < pool.poolSize + 0.5f) {
                        int newX = Mathf.RoundToInt(hitPoint.x);
                        int newY = Mathf.RoundToInt(hitPoint.y);
                        Vector3 createPos = new Vector3(newX, newY, 0);
                        BaseSandItem newSand = Instantiate(waterPrefab, createPos,
                            Quaternion.identity, sandPoolParent.transform);
                        newSand.Ctor(new SandPosition(){X = newX, Y = newY});
                        Destroy(pool.container[newX, newY].gameObject);
                        pool.container[newX, newY] = newSand;
                    }
                }
            }
            //空格重置沙池
            if (Input.GetKeyDown(KeyCode.Space)) {
                foreach (var item in pool.container) {
                    Destroy(item.gameObject);
                }

                pool = new Pool();
            }

            #endregion
        }
    }
}