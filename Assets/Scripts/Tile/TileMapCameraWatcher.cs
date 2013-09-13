using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TileMapCameraWatcher : MonoBehaviour {
    private Camera _camera;
    private Plane _plane;
    public TileMap TileMap { get; set; }

    public void Start() {
        _camera = GetComponent<Camera>();
        _plane = new Plane(Vector3.up, Vector3.zero);
    }

    public void Update() {
        var intersectionPoints = gatherPoints2();
        int minX, minZ, maxX, maxZ;
        minX = minZ = maxX = maxZ = 0;
        var initialized = false;
        foreach (var intersectionPoint in intersectionPoints) {
            int x = (int)intersectionPoint.x;
            int z = (int)intersectionPoint.z;
            if (initialized == false) {
                minX = maxX = x;
                minZ = maxZ = z;
                initialized = true;
            } else {
                if (x < minX)
                    minX = x;
                if (x > maxX)
                    maxX = x;
                if (z < minZ)
                    minZ = z;
                if (z > maxZ)
                    maxZ = z;
            }
        }
        //TODO: this is taking way too long
        for (var i = minX; i < maxX; i++) {
            for (var j = minZ; j < maxZ; j++) {
                if(TileMap.AreValidCoordinates(i,j))
                    TileMap.GetTile(i, j);
            }
        }
    }

//    public void OnDrawGizmos() {
//        foreach (var point in gatherPoints2()) {
//            Gizmos.color = Color.yellow;
//            Gizmos.DrawSphere(point, 5f);
//        }
//    }

    private List<Vector3> gatherPoints2() {
        //Creates a square from where the camera is looking
        //The middle of the square is the middle of the screen projected onto the plane
        //The orientation of the square is according to the rotation of the camera
        //The size of the square is the distance the camera can see
        var points = new List<Vector3>();
        var centerRay = _camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        var distance = _camera.farClipPlane / 2f - 5;
        float middleIntersectionDistance;
        if (_plane.Raycast(centerRay, out middleIntersectionDistance)) {
            var middlePoint = centerRay.GetPoint(middleIntersectionDistance);
            var cameraProjectedPoint = new Vector3(_camera.transform.position.x, 0, _camera.transform.position.z);
            var vectorA = middlePoint - cameraProjectedPoint;
            vectorA.Normalize();
            vectorA *= distance;
            var vectorB = new Vector3(-vectorA.z, 0, vectorA.x);
            points.Add(middlePoint + vectorA + vectorB);
            points.Add(middlePoint + vectorA - vectorB);
            points.Add(middlePoint - vectorA + vectorB);
            points.Add(middlePoint - vectorA - vectorB);
        }
        return points;
    }

    private List<Vector3> gatherPoints() {
        var rays = new List<Ray> {
            _camera.ScreenPointToRay(new Vector3(0, 0, 0)),
            _camera.ScreenPointToRay(new Vector3(Screen.width, 0, 0)),
            _camera.ScreenPointToRay(new Vector3(0, Screen.height, 0)),
            _camera.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0))
        };
        var intersectionPoints = new List<Vector3>();
        foreach (var ray in rays) {
            float intersectionDistance;
            if (_plane.Raycast(ray, out intersectionDistance))
                intersectionPoints.Add(ray.GetPoint(intersectionDistance));
        }
        return intersectionPoints;
    }
}


