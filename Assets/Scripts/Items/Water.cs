using System;
using System.Collections;
using System.Collections.Generic;
using FallingSandSimultion;
using UnityEngine;

namespace FallingSandSimulation.Items {
    public class Water : BaseSandItem {
        public Water(SandPosition createPos) : base(SandTypeEnum.Water) {
        }

        protected override void Start() {
            base.Start();
        }

        protected override void Update() {
            base.Update();
        }

        public override void SandUpdate(int simulationCount) {
            bool maskCheck = updateMask != SandTool.GetUpdateMask(simulationCount);
            base.SandUpdate(simulationCount);

            if (isCtor && maskCheck) {
                var pool = GameManager.instance.Pool;
                var container = GameManager.instance.Pool.container;
                SandPosition oldPosition = new SandPosition(){X = position.X, Y = position.Y};
                int downY = position.Y - 1;
                downY = downY < 0 ? 0 : downY;
                int leftX = position.X - 1;
                leftX = leftX < 0 ? 0 : leftX;
                int rightX = position.X + 1;
                rightX = rightX > pool.poolSize - 1 ? pool.poolSize - 1 : rightX;

                // 水的规则
                if (position.Y == 0) {
                    return;
                } else {
                    if (SandTool.CompareWeight(container[position.X, downY], this) < 0) {
                        // 可向下
                        this.position.Y = downY;
                    } else {
                        bool canGoLeftDown = SandTool.CompareWeight(
                            container[leftX, downY], this) < 0;
                        bool canGoRightDown = SandTool.CompareWeight(
                            container[rightX, downY], this) < 0;
                        if (canGoLeftDown || canGoRightDown) {
                            // 可向左下或右下
                            if (canGoLeftDown && canGoRightDown) {
                                // 如果左右检测都通过，用当前更新检测的值奇偶简单做一次随机;
                                canGoLeftDown = updateMask == 0;
                                canGoRightDown = updateMask == 1;
                            }
                            if (canGoLeftDown) {
                                this.position.X = leftX;
                                this.position.Y = downY;
                            } else if (canGoRightDown) {
                                this.position.X = rightX;
                                this.position.Y = downY;
                            }
                        } else {
                            // 可向左或右
                            bool canGoLeft = SandTool.CompareWeight(
                                container[leftX, position.Y], this) < 0;
                            bool canGoRight = SandTool.CompareWeight(
                                container[rightX, position.Y], this) < 0;
                            if (canGoLeft && canGoRight) {
                                // 如果左右检测都通过，用当前更新检测的值奇偶简单做一次随机;
                                canGoLeft = updateMask == 1;
                                canGoRight = updateMask == 0;
                            }
                            if (canGoLeft) {
                                this.position.X = leftX;
                            } else if (canGoRight) {
                                this.position.X = rightX;
                            }
                        }
                    }
                }

                BaseSandItem oldItem = container[position.X, position.Y];
                if (this.position.Equals(oldPosition)) {
                    return;
                }
                container[position.X, position.Y] = this;
                container[oldPosition.X, oldPosition.Y] = oldItem;
                oldItem.Ctor(oldPosition);
                oldItem.transform.position =
                    new Vector3(oldItem.position.X, oldItem.position.Y);
                this.transform.position =
                    new Vector3(this.position.X, this.position.Y);
            }
        }
    }
}
