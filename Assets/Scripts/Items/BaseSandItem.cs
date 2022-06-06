using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FallingSandSimulation.Items {
    public class SandPosition {
        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object other) {
            if (other is SandPosition otherSand) {
                return this.X == otherSand.X && this.Y == otherSand.Y;
            } else {
                throw new Exception($"SandPosition不支持与{other?.GetType()}进行Equal");
            }
        }
    }

    public abstract class BaseSandItem : MonoBehaviour {
        [HideInInspector]
        public SandTypeEnum sandTypeEnum;
        [HideInInspector]
        public byte updateMask = Byte.MaxValue;
        public SandPosition position;
        protected bool isCtor;

        protected BaseSandItem(SandTypeEnum sandType) {
            sandTypeEnum = sandType;
        }

        public void Ctor(SandPosition createPos) {
            position ??= new SandPosition();
            position.X = createPos.X;
            position.Y = createPos.Y;

            string typeName = string.Empty;
            switch (sandTypeEnum) {
                case SandTypeEnum.Empty:
                    typeName = "Empty";
                    break;
                case SandTypeEnum.Water:
                    typeName = "Water";
                    break;
                case SandTypeEnum.Sand:
                    typeName = "Sand";
                    break;
            }
            transform.name = $"{typeName}{createPos.X}-{createPos.Y}";

            isCtor = true;
        }

        protected virtual void Start() {

        }

        protected virtual void Update() {
        }

        public virtual void SandUpdate(int simulationCount) {
            updateMask = (byte) (simulationCount & (long) 1);
        }
    }
}
