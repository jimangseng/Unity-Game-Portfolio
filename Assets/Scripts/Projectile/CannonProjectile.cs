
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static Weapon;

public class CannonProjectile : ProjectileBase
{
    public class Trace
    {
        public static float Time { get; set; }

        Force horizontal;
        Force vertical;
        Force gravity;

        Vector3 forceDirection;

        public Vector3 Position
        {
            get { return horizontal.currentPosition + vertical.currentPosition + gravity.currentPosition; }
        }

        public Vector3 Velocity
        {
            get { return horizontal.instantVelocity + vertical.instantVelocity + gravity.instantVelocity; }
        }

        public Trace()
        {
            horizontal = new Force();
            vertical = new Force();
            gravity = new Force();

            forceDirection = Vector3.zero;
        }

        public Trace(Vector3 _from, Vector3 _to) : this()
        {
            float tMagnitude = 10.0f;

            forceDirection = Vector3.Normalize(_to - _from);

            horizontal.initialVelocity = forceDirection * tMagnitude;

            vertical.acceleration = -9.8f;
            vertical.initialVelocity = Vector3.up * tMagnitude;

            gravity.acceleration = -9.8f;
        }

        public void update()
        {
            horizontal.instantVelocity = (horizontal.acceleration * Vector3.up) * Time + horizontal.initialVelocity;
            horizontal.currentPosition = (0.5f) * (horizontal.acceleration * Vector3.up) * Time * Time + horizontal.instantVelocity * Time + horizontal.initialPosition;

            vertical.instantVelocity = (vertical.acceleration * Vector3.up) * Time + vertical.initialVelocity;
            vertical.currentPosition = (0.5f) * (vertical.acceleration * Vector3.up) * Time * Time + vertical.instantVelocity * Time + vertical.initialPosition;

            gravity.instantVelocity = (gravity.acceleration * Vector3.up) * Time + gravity.initialVelocity;
            gravity.currentPosition = (0.5f) * (gravity.acceleration * Vector3.up) * Time * Time + gravity.instantVelocity * Time + gravity.initialPosition;

        }
    }

    public class Force
    {
        public float acceleration;
        public Vector3 initialVelocity;
        public Vector3 initialPosition;
        public Vector3 instantVelocity;
        public Vector3 currentPosition;
    }

    public Trace trace;

    protected override void Start()
    {
        base.Start();

        trace = new Trace();
    }

    private void Update()
    {
        Trace.Time += Time.deltaTime;
        trace.update(); 
    }

}

