using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class Trace
{
    public float time;

    Force horizontal;
    Force vertical;
    Force gravity;

    public Vector3 Position;
    public Vector3 Velocity;

    public Trace()
    {
        time = 0.0f;

        horizontal = new Force();
        vertical = new Force();
        gravity = new Force();
    }

    public Trace(Vector3 _from, Vector3 _to): this()
    {
        Vector3 tDirection = Vector3.Normalize(_to - _from);
        float tMagnitude = 1.0f;

        horizontal.initialVelocity = tDirection * tMagnitude;

        vertical.acceleration = -9.8f;
        vertical.initialVelocity = Vector3.up * tMagnitude;

        gravity.acceleration = -9.8f;
    }

    public void update(Vector3 _from, Vector3 _to)
    {
        time = 0.0f;

        Vector3 tDirection = Vector3.Normalize(_to - _from);
        float tMagnitude = 3.0f;

        horizontal.initialVelocity = tDirection * tMagnitude;
        horizontal.instantVelocity = (horizontal.acceleration * Vector3.right) * time + horizontal.initialVelocity;
        horizontal.currentPosition = (0.5f) * (horizontal.acceleration * Vector3.right) * time * time + horizontal.instantVelocity * time + horizontal.initialPosition;

        vertical.acceleration = -9.8f;
        vertical.initialVelocity = Vector3.up * tMagnitude;
        vertical.instantVelocity = (vertical.acceleration * Vector3.up) * time + vertical.initialVelocity;
        vertical.currentPosition = (0.5f) * (vertical.acceleration * Vector3.up) * time * time + vertical.instantVelocity * time + vertical.initialPosition;

        gravity.acceleration = -9.8f;
        gravity.initialVelocity = Vector3.zero;
        gravity.instantVelocity = (gravity.acceleration * Vector3.up) * time + gravity.initialVelocity;
        gravity.currentPosition = (0.5f) * (gravity.acceleration * Vector3.up) * time * time + gravity.instantVelocity * time + gravity.initialPosition;

        Position = horizontal.currentPosition + vertical.currentPosition + gravity.currentPosition;
        Velocity = horizontal.instantVelocity + vertical.instantVelocity + gravity.instantVelocity;

    }
}
