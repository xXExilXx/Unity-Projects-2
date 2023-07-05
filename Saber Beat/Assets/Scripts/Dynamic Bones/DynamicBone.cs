using UnityEngine;
[AddComponentMenu("Dynamic Bone/Dynamic Bone Component")]
public class DynamicBone : MonoBehaviour
{
    public Transform Root;
    public float UpdateRate = 60f;
    public float Damping = 0.1f;
    public float Elasticity = 0.1f;
    public float Stiffness = 0.1f;
    public float Inert = 0.1f;
    public float Radius = 0.2f;
    public float ObjectScale = 1f;
    public Vector3 Gravity = new Vector3(0f, -0.1f, 0f);
    public Vector3 Force = Vector3.zero;
    public Vector3 Wind = Vector3.zero;
    public float WindScale = 0.5f;
    public Vector3 DeltaPosition = Vector3.zero;
    private float updateTime;
    private float deltaTime;
    private float fixedDeltaTime;
    private struct Particle
    {
        public int ParentIndex;
        public float DistanceToParent;
        public Vector3 Position;
        public Vector3 PrevPosition;
        public float Radius;
    }
    private Particle[] particles;
    private Transform[] transforms;
    private void Start()
    {
        if (Root != null)
            InitDynamicBones();
        else
            Debug.LogError("Root Transform is not assigned in DynamicBoneController.");
    }
    private void Update()
    {
        UpdateDynamicBones(Time.deltaTime);
    }
    private void LateUpdate()
    {
        if (updateTime < deltaTime)
        {
            UpdateDynamicBones(updateTime);
            updateTime = fixedDeltaTime;
        }
        else
        {
            updateTime -= deltaTime;
            UpdateDynamicBones(deltaTime);
        }
    }
    private void InitDynamicBones()
    {
        int boneCount = CountBones();
        if (boneCount > 0)
        {
            particles = new Particle[boneCount];
            transforms = new Transform[boneCount];
            deltaTime = 1f / UpdateRate;
            fixedDeltaTime = Time.fixedDeltaTime;
            updateTime = fixedDeltaTime;
            int index = 0;
            TraverseHierarchy(Root, ref index);
        }
    }
    private int CountBones()
    {
        int count = 0;
        void CountTransform(Transform transform)
        {
            count++;
            foreach (Transform child in transform)
                CountTransform(child);
        }
        CountTransform(Root);
        return count;
    }
    private void TraverseHierarchy(Transform transform, ref int index)
    {
        DynamicBone dynamicBone = transform.GetComponent<DynamicBone>();
        if (dynamicBone != null)
        {
            transforms[index] = transform;
            dynamicBone.UpdateRate = UpdateRate;
            dynamicBone.Damping = Damping;
            dynamicBone.Elasticity = Elasticity;
            dynamicBone.Stiffness = Stiffness;
            dynamicBone.Inert = Inert;
            dynamicBone.Radius = Radius * ObjectScale;
            dynamicBone.Force = Force;
            dynamicBone.Gravity = Gravity * ObjectScale;
            dynamicBone.Wind = Wind * WindScale * ObjectScale;
            dynamicBone.DeltaPosition = DeltaPosition;
            Particle particle = new Particle();
            particle.ParentIndex = -1;
            particle.Position = transform.position;
            particle.PrevPosition = particle.Position;
            particle.Radius = dynamicBone.Radius;
            particles[index] = particle;
            index++;
        }
        for (int i = 0; i < transform.childCount; i++)
            TraverseHierarchy(transform.GetChild(i), ref index);
    }
    private void UpdateDynamicBones(float t)
    {
        int count = particles.Length;
        if (count > 0)
        {
            Vector3 gravity = Gravity * ObjectScale;
            Vector3 force = gravity * t * t;

            for (int i = 0; i < count; i++)
            {
                Particle particle = particles[i];

                if (particle.ParentIndex >= 0)
                {
                    Vector3 position = particle.Position;
                    Vector3 prevPosition = particle.PrevPosition;
                    Vector3 displacement = position - prevPosition;
                    displacement += force;
                    displacement *= 1f - Damping;
                    position = prevPosition + displacement + gravity;
                    Vector3 parentOffset = position - particles[particle.ParentIndex].Position;
                    position -= parentOffset * (1f - Stiffness);
                    Vector3 targetPosition = position + parentOffset;
                    position = Vector3.Lerp(position, targetPosition, Elasticity);
                    Vector3 inertia = position - particle.Position;
                    particle.Position = position + inertia * (1f - Inert);
                    particle.PrevPosition = particle.Position - inertia * t;
                }
                else
                {
                    particle.Position += force;
                    Vector3 position = particle.Position;
                    if (Wind != Vector3.zero)
                    {
                        Vector3 wind = Wind * WindScale * ObjectScale;
                        position += wind * t;
                    }
                    Vector3 dir = position - particle.PrevPosition;
                    particle.PrevPosition = particle.Position;
                    particle.Position += dir * (1f - Damping);
                    particle.Position += Force * t;
                }
                particle.Radius = Radius * ObjectScale;
                particle.Position += DeltaPosition;
                particles[i] = particle;
            }
        }
    }
}
