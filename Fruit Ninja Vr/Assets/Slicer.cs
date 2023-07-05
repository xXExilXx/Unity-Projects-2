using UnityEngine;
using EzySlice;

public class Slicer : MonoBehaviour
{
    public Material materialAfterSlice;
    public LayerMask sliceLayerMask;
    public bool isTouched;
    public float minForce;
    public float maxForce;
    public float destroyDelay = 2f;

    private void Update()
    {
        if (isTouched)
        {
            isTouched = false;

            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1f, 0.1f, 0.1f), transform.rotation, sliceLayerMask);

            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
                SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, materialAfterSlice);

                GameObject upperHullGameObject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, materialAfterSlice);
                GameObject lowerHullGameObject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, materialAfterSlice);

                upperHullGameObject.transform.position = objectToBeSliced.transform.position;
                lowerHullGameObject.transform.position = objectToBeSliced.transform.position;

                MakeItPhysical(upperHullGameObject);
                MakeItPhysical(lowerHullGameObject);

                float force = RandomForce(minForce, maxForce);

                // Apply force to sliced objects
                Rigidbody upperRigidbody = upperHullGameObject.GetComponent<Rigidbody>();
                upperRigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
                upperRigidbody.velocity = transform.up * force;

                Rigidbody lowerRigidbody = lowerHullGameObject.GetComponent<Rigidbody>();
                lowerRigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
                lowerRigidbody.velocity = -transform.up * force;

                Destroy(objectToBeSliced.gameObject);

                // Destroy the sliced objects after the specified delay
                Destroy(upperHullGameObject, destroyDelay);
                Destroy(lowerHullGameObject, destroyDelay);
            }
        }
    }

    float RandomForce(float maxValue, float minValue)
    {
        return Random.Range(minValue, maxValue);
    }

    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
        obj.layer = 6;
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}
