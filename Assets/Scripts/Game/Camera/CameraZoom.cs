using Cinemachine;
using Game.Camera.Config;
using UnityEngine;
using Utility;
namespace Game.Camera
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private CameraConfig cameraConfig;
        private CinemachineFramingTransposer _framingTranspose;
        private CinemachineInputProvider _inputProvider;
        private float _currentTargetDistance;
        
        private void Awake()
        {
            InitializeCamera();
            _framingTranspose = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
            _inputProvider = GetComponent<CinemachineInputProvider>();
            _currentTargetDistance = cameraConfig.DefaultDistance;
        }

        private void Update()
        {
            ZoomCamera();
        }

        private static void InitializeCamera()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void ZoomCamera()
        {
            var zoomValue = CalculateZoomValue();
            UpdateTargetDistance(zoomValue);
            UpdateCameraDistance();
        }

        private float CalculateZoomValue()
        {
            return _inputProvider.GetAxisValue(2) * cameraConfig.ZoomSensitivity;
        }

        private void UpdateTargetDistance(float zoomValue)
        {
            _currentTargetDistance = Mathf.Clamp(_currentTargetDistance + zoomValue, cameraConfig.MinimumDistance, cameraConfig.MaximumDistance);
        }

        private void UpdateCameraDistance()
        {
            var currentDistance = _framingTranspose.m_CameraDistance;
            if (MathUtility.IsClose(currentDistance, _currentTargetDistance)) return;
            _framingTranspose.m_CameraDistance =  Mathf.Lerp(currentDistance, _currentTargetDistance, cameraConfig.Smoothing * Time.deltaTime);;
        }
    }
}