using UnityEngine;

[RequireComponent(typeof (Camera))]
public class CameraTileObserver : MonoBehaviour
{
    /// <summary>
    ///     Distance to follow target
    /// </summary>
    public float Distance = 5;

    /// <summary>
    ///     Make this around 0.1f for a small zoom out or 0.5f for a large zoom (depending on the speed of your rigidbody)
    /// </summary>
    public float DistanceMultiplier = 0f;

    /// <summary>
    ///     The time it takes to snap back to the original distance or the zoomed distance (depending on speed of
    ///     parentRigidyBody)
    /// </summary>
    public float DistanceSnapTime = 0f;

    /// <summary>
    ///     The Height of the camera
    /// </summary>
    public float Height = 5f;

    /// <summary>
    ///     Smooth out the height position
    /// </summary>
    public float HeightDamping = 2.0f;

    /// <summary>
    ///     An offset of the target
    /// </summary>
    public float LookAtHeight = 0.0f;

    /// <summary>
    ///     The time it takes to snap back to original rotation
    /// </summary>
    public float RotationSnapTime = 0.3F;

    /// <summary>
    ///     TargetTransform to look at
    /// </summary>
    public Transform TargetTransform;

    private Vector3 _lookAtOffset;
    private float _usedDistance;
    private float _yVelocity;
    private float _zVelocity;

    private void Start()
    {
        _lookAtOffset = new Vector3(0, LookAtHeight, 0);
    }

    private void Update()
    {
        var scrollValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollValue.IsNotZero())
        {
            Distance -= scrollValue;
            Height -= scrollValue;
        }
    }

    private void LateUpdate()
    {
        var goalHeight = TargetTransform.position.y + Height;
        var currentHeight = transform.position.y;

        var goalRotationAngle = TargetTransform.eulerAngles.y;
        var currentRotationAngle = transform.eulerAngles.y;

        currentRotationAngle = Mathf.SmoothDampAngle(currentRotationAngle, goalRotationAngle, ref _yVelocity,
            RotationSnapTime);

        currentHeight = Mathf.Lerp(currentHeight, goalHeight, HeightDamping*Time.deltaTime);

        var wantedPosition = TargetTransform.position;
        wantedPosition.y = currentHeight;

        _usedDistance = Mathf.SmoothDampAngle(_usedDistance, Distance, ref _zVelocity, DistanceSnapTime);

        wantedPosition += Quaternion.Euler(0, currentRotationAngle, 0)*new Vector3(0, 0, -_usedDistance);

        transform.position = wantedPosition;

        transform.LookAt(TargetTransform.position + _lookAtOffset);
    }
}