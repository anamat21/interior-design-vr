using UnityEngine;
using Oculus.Interaction;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using System;
using System.Collections.Generic;

namespace Meta.XR.MRUtilityKitSamples.BouncingBall
{
    public class ObjectHandler : MonoBehaviour
    {

        [SerializeField] private Transform trackingSpace;
        [SerializeField] private Transform rightControllerPivot;
        [SerializeField] private GameObject chair1;
        [SerializeField] private GameObject chair2;
        [SerializeField] private GameObject chair3;
        [SerializeField] private GameObject chair4;
        [SerializeField] private GameObject chair5;
        [SerializeField] private GameObject sofa1;
        [SerializeField] private GameObject sofa2;
        [SerializeField] private GameObject sofa3;
        [SerializeField] private GameObject sofa4;
        [SerializeField] private GameObject sofa5;

        public Vector3 pos;
        public LayerMask SceneLayer;
        // Get the controller transform
        public static OVRInput.Controller activeController;
        public Vector3 controllerPosition;
        private BouncingBallLogic currentBall;

        // public RayInteractor rayInteractor;
        private List<GameObject> memory; 
    
        public float maxRayDistance = 20f; // Maximum distance to raycast

        void Start()
        {   
            // Optional: Visualize the ray (for debugging)
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;

            activeController = OVRInput.Controller.RTouch; // or LTouch
            controllerPosition = OVRInput.GetLocalControllerPosition(activeController);

            memory = new List<GameObject>();
        }

        void Update()
        {
            var controllerTransform = GameObject.Find("RightControllerAnchor").transform; // or "LeftControllerAnchor"

            // Optional: Visualize the ray (for debugging)
            if (GetComponent<LineRenderer>() != null)
            {
                LineRenderer lineRenderer = GetComponent<LineRenderer>();
                lineRenderer.SetPosition(0, controllerPosition);
                lineRenderer.SetPosition(1, controllerPosition + controllerTransform.forward * maxRayDistance);
            }
        }

        public void PlaceObject(string[] values)
        {
            var objectShape = values[0];

            var controllerTransform = GameObject.Find("RightControllerAnchor").transform; // or "LeftControllerAnchor"

            RaycastHit hit;
            if (Physics.Raycast(controllerTransform.position, controllerTransform.forward, out hit, maxRayDistance))
            {
                // Instantiate a cube at the hit point
                pos = hit.point;
            }
            else
            {
                // If the ray doesn't hit anything, place the cube at a distance in the ray direction
                Vector3 position = controllerTransform.position + controllerTransform.forward * 2f;
                pos = position;
            }

            if (objectShape == "chair 1"){
                //GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameObject instance = Instantiate(chair1);
                var obj = instance.GetComponent<BouncingBallLogic>();

                //obj.AddComponent<Light>();
                //obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                obj.Rigidbody.position = pos;
                
                memory.Add(instance);
                // const float shiftToPreventCollisionWithGrabbedBall = 0.1f;
                // var pos = rightControllerPivot.position +
                // rightControllerPivot.forward * shiftToPreventCollisionWithGrabbedBall;
                // newBall.Release(pos, rightControllerPivot.forward * speed, Vector3.zero);
            } 

            if (objectShape == "chair 2"){
                // GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                // obj.AddComponent<Light>();
                // obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                // obj.Rigidbody.position = pos;
                GameObject instance = Instantiate(chair2);
                var obj = instance.GetComponent<BouncingBallLogic>();

                //obj.AddComponent<Light>();
                //obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                obj.Rigidbody.position = pos;
                memory.Add(instance);
            } 

            if (objectShape == "chair 3"){
                GameObject instance = Instantiate(chair3);
                var obj = instance.GetComponent<BouncingBallLogic>();

                obj.Rigidbody.position = pos;
                memory.Add(instance);
            }

            if (objectShape == "chair 4"){
                GameObject instance = Instantiate(chair4);
                var obj = instance.GetComponent<BouncingBallLogic>();

                obj.Rigidbody.position = pos;
                memory.Add(instance);
            }

            if (objectShape == "chair 5"){
                GameObject instance = Instantiate(chair5);
                var obj = instance.GetComponent<BouncingBallLogic>();

                obj.Rigidbody.position = pos;
                memory.Add(instance);
            }

            if (objectShape == "sofa 1"){
                //GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameObject instance = Instantiate(sofa1);
                var obj = instance.GetComponent<BouncingBallLogic>();

                obj.Rigidbody.position = pos;
                memory.Add(instance);

                // const float shiftToPreventCollisionWithGrabbedBall = 0.1f;
                // var pos = rightControllerPivot.position +
                // rightControllerPivot.forward * shiftToPreventCollisionWithGrabbedBall;
                // newBall.Release(pos, rightControllerPivot.forward * speed, Vector3.zero);
            } 

            if (objectShape == "sofa 2"){
                // GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                // obj.AddComponent<Light>();
                // obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                // obj.Rigidbody.position = pos;
                GameObject instance = Instantiate(sofa2);
                var obj = instance.GetComponent<BouncingBallLogic>();

                obj.Rigidbody.position = pos;
                memory.Add(instance);
            } 

            if (objectShape == "sofa 3"){
                GameObject instance = Instantiate(sofa3);
                var obj = instance.GetComponent<BouncingBallLogic>();

                obj.Rigidbody.position = pos;
                memory.Add(instance);
            }

            if (objectShape == "sofa 4"){
                GameObject instance = Instantiate(sofa4);
                var obj = instance.GetComponent<BouncingBallLogic>();

                obj.Rigidbody.position = pos;
                memory.Add(instance);
            }

            if (objectShape == "sofa 5"){
                GameObject instance = Instantiate(sofa5);
                var obj = instance.GetComponent<BouncingBallLogic>();

                obj.Rigidbody.position = pos;
                memory.Add(instance);
            }

        }

        public void Undo(){
            if (memory.Count > 0) {
                var obj = memory[memory.Count-1];
                Destroy(obj);
                memory.RemoveAt(memory.Count-1);
            }
        }
    }

}
