
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static Weapon;

public class CannonProjectile : ProjectileBase
{
    public class Trace
    {
        static Vector3 velocity = Vector3.zero;


        static float time;

        public Force horizontal;
        public Force vertical;
        public Force gravity;

        Vector3 forceDirection;

        public Vector3 Velocity
        {
            get { return horizontal.InstVelocity + vertical.InstVelocity + gravity.InstVelocity; }
            set { }
        }

        public Vector3 Position
        {
            get { return horizontal.CurrentPosition + vertical.CurrentPosition + gravity.CurrentPosition; }
            set { }
        }


        public Trace(Vector3 _from, Vector3 _to) : this()
        {

            forceDirection = Vector3.Normalize(_to - _from);

            horizontal.InstVelocity = forceDirection * 10.0f;

            vertical.Acceleration = -10.0f;
            vertical.InitVelocity = Vector3.up * 10.0f;

            gravity.Acceleration = -10.0f;
        }

        public Trace()
        {
            horizontal = new Force();
            vertical = new Force();
            gravity = new Force();

            forceDirection = Vector3.zero;
        }




        public void UpdateTime(float _time)
        {
            time = _time;
        }


        public class Force
        {
            public float Acceleration
            {
                get;
                set;
            }
            public Vector3 InitPosition
            {
                get;
                set;
            }
            public Vector3 InitVelocity
            {
                get;
                set;
            }

            public Vector3 InstVelocity
            {
                get { return InitVelocity + new Vector3(0.0f, Acceleration * time, 0.0f); }
                set { }
            }

            public Vector3 CurrentPosition
            {
                get { return InitPosition + (InitVelocity * time) + (0.5f * time * time * Vector3.up * Acceleration); }
                set { }
            }
        }
    }

    public Weapon weapon;
    Vector3 velocity;

    public Trace trace;

    float elapsedTime;

    protected override void Start()
    {
        base.Start();

        trace = new Trace();
    }

    void Update()
    {
        trace.UpdateTime(elapsedTime);

        GetComponent<Rigidbody>().velocity = velocity;
        elapsedTime = elapsedTime + Time.deltaTime;
    }

    public override void Fire(Vector3 _from, Vector3 _to)
    {
        base.Fire(_from, _to);

        trace = new Trace(_from, _to);
        velocity = trace.Velocity;
    }
}

