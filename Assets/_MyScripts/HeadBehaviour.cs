using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBehaviour : MonoBehaviour
{
    private Collider collidedWith;
    private OVRScreenFade oVRScreenFade;

    private void Awake()
    {
        oVRScreenFade = GetComponentInParent<OVRScreenFade>();
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        SphereCollider sc = gameObject.AddComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.radius = 0.2f;
        FadingStuff();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            collidedWith = other;
            StartCoroutine(PainFade());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (collidedWith == other)
        {
            //TODO: stop pain
            inPain = false;
            collidedWith = null;
        }
    }

    private void GameOver()
    {
        oVRScreenFade.FadeOut();
        StartCoroutine(LoadLevel.Begin(0));
    }


    /// <summary>
    /// The render queue used by the fade mesh. Reduce this if you need to render on top of it.
    /// </summary>
    private int renderQueue = 4000;

    private MeshRenderer fadeRenderer;
    private MeshFilter fadeMesh;
    private Material fadeMaterial = null;
    private float fadeTime = 0.5f;
    private float currentAlpha;

    private bool inPain;
    IEnumerator PainFade()
    {
        inPain = true;
        while (inPain)
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                currentAlpha = Mathf.Lerp(0, 1, Mathf.Clamp01(elapsedTime / fadeTime));
                SetMaterialAlpha();
                yield return new WaitForEndOfFrame();
            }
            elapsedTime = 0.0f;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                currentAlpha = Mathf.Lerp(1, 0, Mathf.Clamp01(elapsedTime / fadeTime));
                SetMaterialAlpha();
                yield return new WaitForEndOfFrame();
            }
        }

    }

    /// <summary>
    /// Update material alpha. UI fade and the current fade due to fade in/out animations (or explicit control)
    /// both affect the fade. (The max is taken)
    /// </summary>
    private void SetMaterialAlpha()
    {
        Color color = new Color(0.75f, 0f, 0f, 1.0f);
        color.a = Mathf.Max(currentAlpha, 0);
        if (fadeMaterial != null)
        {
            fadeMaterial.color = color;
            fadeMaterial.renderQueue = renderQueue;
            fadeRenderer.material = fadeMaterial;
            fadeRenderer.enabled = color.a > 0; ;
        }
    }
    private void FadingStuff()
    {
        // create the fade material
        fadeMaterial = new Material(Shader.Find("Oculus/Unlit Transparent Color"));
        fadeMesh = gameObject.AddComponent<MeshFilter>();
        fadeRenderer = gameObject.AddComponent<MeshRenderer>();

        float width = 2f;
        float height = 2f;
        float depth = 1f;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(-width, -height, depth);
        vertices[1] = new Vector3(width, -height, depth);
        vertices[2] = new Vector3(-width, height, depth);
        vertices[3] = new Vector3(width, height, depth);

        var mesh = new Mesh();
        fadeMesh.mesh = mesh;
        mesh.vertices = vertices;

        int[] tri = new int[6];
        tri[0] = 0;
        tri[1] = 2;
        tri[2] = 1;
        tri[3] = 2;
        tri[4] = 3;
        tri[5] = 1;
        mesh.triangles = tri;

        Vector3[] normals = new Vector3[4];
        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;
        normals[3] = -Vector3.forward;
        mesh.normals = normals;

        Vector2[] uv = new Vector2[4];
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);
        mesh.uv = uv;

        currentAlpha = 0;
        SetMaterialAlpha();

    }
}
