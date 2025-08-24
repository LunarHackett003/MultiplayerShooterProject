using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    /// <summary>
    /// Basically just a spring.
    /// </summary>
    public class GroundSpring
    {
        float springOffset;
        float lastSpringOffset;
        float springSpeed;
        float maxSpringLength;
        float currentSpringLength;

        public float springRestLength;
        public float springTravel;
        public float springStrength;
        public float springDamper;

        public Vector3 springPos;
        public Vector3 originPos;

        GroundChecker gc;

        Transform transform;

        public void Init(GroundChecker gc, int index)
        {
            this.gc = gc;
            transform = gc.transform;
            originPos = gc.rayRadialOffset * (Quaternion.Euler(0, index * gc.anglePerRay, 0) * Vector3.forward);
            springRestLength = gc.springRestLength;
            springTravel = gc.springTravel;
            springStrength = gc.springStrength;
            springDamper = gc.springDamper;
        }


        public void UpdateSpring(out bool groundHit, out Vector3 normal, out float force)
        {
            maxSpringLength = springRestLength + springTravel;
            currentSpringLength = 0;
            Vector3 origin = transform.TransformPoint(originPos + gc.localRayOrigin);
            groundHit = Physics.Raycast(origin, -transform.up, out RaycastHit hit, maxSpringLength + gc.hoverDistanceOffset, 
                gc.rayMask, QueryTriggerInteraction.Ignore) 
                && CheckWalkable(transform, gc.walkableGroundThreshold, hit);
            if(hit.collider != null)
            {
                currentSpringLength = hit.distance;
                normal = hit.normal;
                Debug.DrawLine(origin, hit.point, Color.green, Time.fixedDeltaTime);
            }
            else
            {
                Debug.DrawRay(origin, -transform.up * maxSpringLength, Color.green, Time.fixedDeltaTime);
                normal = transform.up;
            }


            if (groundHit)
            {
                currentSpringLength -= gc.hoverDistanceOffset;
                springOffset = (springRestLength - currentSpringLength) / springTravel;

                springSpeed = Vector3.Dot(transform.up, gc.cm.rb.GetPointVelocity(springPos));

                float yForce = (springStrength * springOffset) - (springDamper * springSpeed);

                springPos = Vector3.up * Mathf.Clamp(springRestLength - currentSpringLength, -springTravel, springTravel);
                force = yForce;
            }
            else
            {
                force = 0;
            }
        }
    }

    public GroundSpring[] groundSprings;


    public float springRestLength;
    public float springTravel;
    public float springStrength;
    public float springDamper;

    public float maxDownwardForce;

    public bool onGround;
    public Vector3 normal;

    public float rayRadialOffset;
    public float rayDistance;
    public LayerMask rayMask;
    public Vector3 localRayOrigin;
    [SerializeField] float anglePerRay;
    public float walkableGroundThreshold;

    public float hoverDistanceOffset;
    public float targetHeight = 0.14f;
    public CharacterMotor cm;

    public int groundSpringCount;

    private void Start()
    {
        if(groundSpringCount <= 0)
        {
            groundSpringCount = 1;
        }
        anglePerRay = 360 / groundSpringCount;
        if(cm == null)
            cm = GetComponent<CharacterMotor>();

        SetUpSprings();
    }
    void SetUpSprings()
    {
        if(groundSprings == null || groundSprings.Length != groundSpringCount)
        {
            groundSprings = new GroundSpring[groundSpringCount];
        }
        for (int i = 0; i < groundSpringCount; i++)
        {
            if (groundSprings[i] == null)
            {
                groundSprings[i] = new GroundSpring();
            }
            groundSprings[i].Init(this, i);
        }
    }
    private void OnValidate()
    {
        groundSpringCount = Mathf.Clamp(groundSpringCount, 1, 16);
        anglePerRay = 360 / groundSpringCount;
        SetUpSprings();
    }
    public void CheckGround()
    {
        CastRays();
    }
    void CastRays()
    {
        /*  normal = Vector3.zero;
        *   hoverSpringLength = 0;
        *   hits = 0;
        *   for (int i = 0; i < rayCount; i++)
        *   {
        *       if (Physics.Raycast(transform.TransformPoint((Quaternion.Euler(0, anglePerRay * i, 0) * Vector3.forward * rayRadialOffset) + localRayOrigin), -transform.up, out RaycastHit hit, rayDistance, rayMask, QueryTriggerInteraction.Ignore) && CheckWalkable(hit))
        *       {
        *           hits++;
        *           normal += hit.normal;
        *           hoverSpringLength += hit.distance;
        *       }
        *   }
        *   if(hits > 0)
        *   {
        *       normal /= hits;
        *       onGround = true;
        *   }
        *   else
        *   {
        *       onGround = false;
        *   }
        */
        normal = Vector3.zero;
        onGround = false;
        int hits = 0;
        for (int i = 0; i < groundSprings.Length; i++)
        {
            GroundSpring gs = groundSprings[i];
            gs.UpdateSpring(out bool groundHit, out Vector3 normal, out float force);
            if (groundHit)
            {
                hits++;
                this.normal += normal;
                //This should prevent the force from pulling us back down too harshly
                force = Mathf.Max(force, maxDownwardForce);
                cm.rb.AddForce(transform.up * force);
            }
            onGround |= groundHit;
        }
        if(hits > 0)
        {
            normal /= hits;
        }



    }
    public static bool CheckWalkable(Transform transform, float threshold, RaycastHit hit)
    {
        if (Vector3.Dot(transform.up, hit.normal) >= threshold)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if(groundSpringCount == 1)
        {

        }
        else
        {
            //for (int i = 0; i < groundSpringCount; i++)
            //{
            //    Gizmos.DrawRay(transform.TransformPoint((Quaternion.Euler(0, anglePerRay * i, 0) * Vector3.forward * rayRadialOffset) + localRayOrigin), -transform.up * rayDistance);
            //}
        }
    }
}
