using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts.Collections;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
//    private TileMap _tileMap;

    public void Start()
    {
//        _tileMap = new TileMap(100);
//        var tileFactory = this.AddComponent<TileFactory>();
//        tileFactory.CreateGameObjects(_tileMap);
//        Camera.main.gameObject.AddComponent<TileMapCameraWatcher>().TileMap = _tileMap;
//        CreatePlane("CustomPlane", 250);
        //var plane = CreatePlane("CustomPlane", 100);
        var plane = CreateAlternatePlane("CustomAlternatePlane", 100, 100);
        var tileObserver = Camera.main.gameObject.AddComponent<CameraTileObserver>();
        tileObserver.TargetTransform = plane.transform;
    }

    private GameObject CreateAlternatePlane(string goName, int width, int height)
    {
        var go = new GameObject(goName);
        var meshFilter = go.AddComponent<MeshFilter>();
        var mesh = meshFilter.mesh;
        mesh.Clear();

        var totalCount = width*height*4;
        if (totalCount > 65000)
            throw new Exception("Cannot create a plane mesh with 65000+ vertices. Reduce size.");

        #region Colors
        var predefinedColors = new List<Color32>
        {
            Color.blue.ToColor32(),
            Color.red.ToColor32(),
            Color.green.ToColor32()
        };
        var colorEnumerator = new RotatingEnumerator<Color32>(predefinedColors);
        #endregion

        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        var colors = new List<Color32>();
        var uvs = new List<Vector2>();
        var triangles = new List<int>();
        for (var i = 0; i < width; i ++)
        {
            for (var j = 0; j < height; j++)
            {
                var baseIndex = vertices.Count;
                //4 Vertices
                vertices.Add(new Vector3(i, 0, j));
                vertices.Add(new Vector3(i, 0, j + 1));
                vertices.Add(new Vector3(i + 1, 0, j));
                vertices.Add(new Vector3(i + 1, 0, j + 1));
                //4 Normals
                normals.Add(Vector3.up);
                normals.Add(Vector3.up);
                normals.Add(Vector3.up);
                normals.Add(Vector3.up);
                //2 Triangles of 3 vertices
                //Triangle 1
                triangles.Add(baseIndex);
                triangles.Add(baseIndex + 1);
                triangles.Add(baseIndex + 3);
                //Triangle 2
                triangles.Add(baseIndex + 3);
                triangles.Add(baseIndex + 2);
                triangles.Add(baseIndex);
                //Copy the color 4 times since each vertex needs it
                var nextColor = colorEnumerator.Next();
                colors.Add(nextColor);
                colors.Add(nextColor);
                colors.Add(nextColor);
                colors.Add(nextColor);
                //UV should be (0,0) for bottom left, (1,1) for top right, etc.
                uvs.Add(new Vector2(0, 0));
                uvs.Add(new Vector2(0, 1));
                uvs.Add(new Vector2(1, 0));
                uvs.Add(new Vector2(1, 1));
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors32 = colors.ToArray();
        mesh.RecalculateBounds();
        mesh.Optimize();
        var mr = go.AddComponent<MeshRenderer>();
        //mr.material = new Material(Shader.Find("Diffuse"));
        mr.material = Resources.Load("VertexColorUnlitMaterial") as Material;

        return go;
    }

    private GameObject CreatePlane(string goName, int size)
    {
        var resolution = size + 1;
        var count = resolution*resolution;
        if (count >= 65000)
            throw new Exception("Cannot create a plane mesh with 65000+ vertices. Reduce size.");
        var go = new GameObject(goName);
        var filter = go.AddComponent<MeshFilter>();
        var mesh = filter.mesh;
        mesh.Clear();

        #region Vertices

        var colors = new Color32[count];
        var vertices = new Vector3[count];
        for (var z = 0; z < resolution; z++)
        {
            // [ -length / 2, length / 2 ]
            var zPos = ((float) z/(resolution - 1) - .5f)*size;
            for (var x = 0; x < resolution; x++)
            {
                // [ -width / 2, width / 2 ]
                var xPos = ((float) x/(resolution - 1) - .5f)*size;
                vertices[x + z*resolution] = new Vector3(xPos, 0f, zPos);
                
            }
        }

        #endregion

        #region Normals

        var normals = new Vector3[vertices.Length];
        for (var n = 0; n < normals.Length; n++)
            normals[n] = Vector3.up;

        #endregion

        #region UVs

        var uvs = new Vector2[vertices.Length];
        for (var v = 0; v < resolution; v++)
        {
            for (var u = 0; u < resolution; u++)
                uvs[u + v*resolution] = new Vector2((float) u/(resolution - 1), (float) v/(resolution - 1));
        }

        #endregion

        #region Triangles

        var predefinedColors = new List<Color32>
        {
            Color.blue.ToColor32(),
            Color.red.ToColor32(),
            Color.green.ToColor32()
        };
        var enumerator = new RotatingEnumerator<Color32>(predefinedColors);


        var nbFaces = (resolution - 1)*(resolution - 1);
        var triangles = new int[nbFaces*6];
        var t = 0;
        for (var face = 0; face < nbFaces; face++)
        {

            // Retrieve lower left corner from face ind
            var i = face%(resolution - 1) + (face/(resolution - 1)*resolution);
            triangles[t++] = i + resolution;
            triangles[t++] = i + 1;
            triangles[t++] = i;
            triangles[t++] = i + resolution;
            triangles[t++] = i + resolution + 1;
            triangles[t++] = i + 1;

            colors[i] = colors[i + 1] = colors[i + resolution] = colors[i + resolution] = colors[i + resolution + 1] = enumerator.Next();
        }

        #endregion

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.colors32 = colors;
        mesh.RecalculateBounds();
        mesh.Optimize();
        var mr = go.AddComponent<MeshRenderer>();
        //mr.material = new Material(Shader.Find("Diffuse"));
        mr.material = Resources.Load("VertexColorUnlitMaterial") as Material;

        return go;
    }
}