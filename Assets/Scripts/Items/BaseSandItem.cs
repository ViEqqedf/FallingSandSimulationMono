using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class SandPosition {
    public int X { get; set; }
    public int Y { get; set; }
}

public abstract class BaseSandItem : MonoBehaviour {
    public SandTypeEnum sandTypeEnum;
    public SandPosition position;
    public byte updateMask = Byte.MaxValue;
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
