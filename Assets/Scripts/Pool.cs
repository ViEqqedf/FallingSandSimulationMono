using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace {
    public class Pool {
        public readonly int poolSize = 20;
        public BaseSandItem [,] container;

        public Pool() {
            container = new BaseSandItem[poolSize, poolSize];

            GameManager manager = GameManager.instance;
            Empty emptyPrefab = manager.emptyPrefab;
            for (int i = 0, count1 = poolSize; i < count1; i++) {
                for (int j = 0, count2 = poolSize; j < count2; j++) {
                    container[i, j] = GameObject.Instantiate(
                        emptyPrefab, new Vector3(i, j, 0),
                        Quaternion.identity, manager.sandPoolParent.transform);
                    container[i, j].Ctor(new SandPosition(){X = i, Y = j});
                }
            }
        }
    }
}