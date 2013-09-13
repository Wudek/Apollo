using System;
using UnityEngine;

public class MainScreen : MonoBehaviour {
    private TileMap _tileMap;

    public void Start() {
//        _tileMap = new TileMap(100);
//        var tileFactory = this.AddComponent<TileFactory>();
//        tileFactory.CreateGameObjects(_tileMap);
//        Camera.main.gameObject.AddComponent<TileMapCameraWatcher>().TileMap = _tileMap;
        CreatePlane("CustomPlane", 250);
    }

    private void CreatePlane(string name, int size) {
        int resolution = size + 1;
        int count = resolution * resolution;
        if (count >= 65000)
            throw new Exception("Cannot create a plane mesh with 65000+ vertices. Reduce size.");
        var go = new GameObject(name);
        var filter = go.AddComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        mesh.Clear();

        #region Vertices

        var vertices = new Vector3[count];
        for (int z = 0; z < resolution; z++) {
            // [ -length / 2, length / 2 ]
            float zPos = ((float)z / (resolution - 1) - .5f) * size;
            for (int x = 0; x < resolution; x++) {
                // [ -width / 2, width / 2 ]
                float xPos = ((float)x / (resolution - 1) - .5f) * size;
                vertices[x + z * resolution] = new Vector3(xPos, 0f, zPos);
            }
        }

        #endregion

        #region Normales

        var normales = new Vector3[vertices.Length];
        for (int n = 0; n < normales.Length; n++)
            normales[n] = Vector3.up;

        #endregion

        #region UVs

        var uvs = new Vector2[vertices.Length];
        for (int v = 0; v < resolution; v++) {
            for (int u = 0; u < resolution; u++)
                uvs[u + v * resolution] = new Vector2((float)u / (resolution - 1), (float)v / (resolution - 1));
        }

        #endregion

        #region Triangles

        int nbFaces = (resolution - 1) * (resolution - 1);
        var triangles = new int[nbFaces * 6];
        int t = 0;
        for (int face = 0; face < nbFaces; face++) {
            // Retrieve lower left corner from face ind
            int i = face % (resolution - 1) + (face / (resolution - 1) * resolution);
            triangles[t++] = i + resolution;
            triangles[t++] = i + 1;
            triangles[t++] = i;
            triangles[t++] = i + resolution;
            triangles[t++] = i + resolution + 1;
            triangles[t++] = i + 1;
        }

        #endregion

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.Optimize();
        var mr = go.AddComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Diffuse"));
    }
}