using UnityEngine;


namespace TopDown.CameraController
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float displacementMultiplier = 0.15f;
        private float zPosition = -10;
        private void Update()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cameraDisplacement = (mousePosition - playerTransform.position) * displacementMultiplier;


            Vector3 finalCameraPosition = playerTransform.position + cameraDisplacement;
            finalCameraPosition.z = zPosition;           
            transform.position = finalCameraPosition;
        }
    }
}