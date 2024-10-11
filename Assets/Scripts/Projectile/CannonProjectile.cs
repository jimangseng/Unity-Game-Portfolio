
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static Weapon;

public class CannonProjectile : ProjectileBase
{
    public class Trace
    {
        static float time;

        public Force horizontal;
        public Force vertical;
        public Force gravity;

        public Trace()
        {
            horizontal = new Force(0.0f, Vector3.zero, Vector3.zero);
            vertical = new Force(0.0f, Vector3.zero, Vector3.zero);
            gravity = new Force(0.0f, Vector3.zero, Vector3.zero);
        }

        public Vector3 currentPosition
        {
            get { return gravity.currentPosition + horizontal.currentPosition + vertical.currentPosition; }
        }

        public Vector3 instVelocity
        {
            get { return gravity.instVelocity + horizontal.instVelocity + vertical.instVelocity; }
        }

        public Vector3 initVelocity
        {
            get; private set;
        }

        public void UpdateTime(float _time)
        {
            time = _time;
        }


        public class Force
        {
            public float acceleration;
            public Vector3 initialVelocity;
            public Vector3 initialPosition;

            public Force(float _acceleration, Vector3 _initialVelocity, Vector3 _initialPosition)
            {
                acceleration = _acceleration;
                initialVelocity = _initialVelocity;
                initialPosition = _initialPosition;
                time = 0.0f;
            }

            public Force(float _acceleration, Vector3 _initialVelocity, Vector3 _initialPosition, float _time) : this(_acceleration, _initialVelocity, _initialPosition)
            {
                acceleration = _acceleration;
                initialVelocity = _initialVelocity;
                initialPosition = _initialPosition;
                time = _time;
            }

            public Vector3 currentPosition
            {
                get { return initialPosition + initialVelocity * time + new Vector3(0.0f, (0.5f) * acceleration * time * time, 0.0f); }
            }

            public Vector3 instVelocity
            {
                get{ return initialVelocity + new Vector3(0.0f, acceleration * time, 0.0f); }
            }

            public Vector3 initVelocity
            {
                get { return initialVelocity; }
            }
        }
    }

    public Weapon weapon;
    Vector3 velocity;

    public Trace trace;

    float elapsedTime;

    public CannonProjectile ()
    {
        trace = new Trace();

        elapsedTime = 0.0f;


        float tSpeed = 10.0f; // todo: 정확한 예측 지점에 착지하도록
        float tAcc = -10.0f;

        trace.horizontal.initialVelocity = forceDirection * tSpeed;

        trace.vertical.acceleration = tAcc;
        trace.vertical.initialVelocity = Vector3.up * tSpeed;

        trace.gravity.acceleration = tAcc;
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        trace.UpdateTime(elapsedTime);

        GetComponent<Rigidbody>().velocity = trace.instVelocity;
        elapsedTime = elapsedTime + Time.deltaTime;
    }

    public override void Fire(Vector3 _from, Vector3 _to)
    {
        base.Fire(_from, _to);

    }
}

