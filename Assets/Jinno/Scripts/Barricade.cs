using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms;

public class Barricade : MonoBehaviour
{
    public enum ColliderShape { Cube, Sphere, Capsule, Cylinder }
    public ColliderShape colliderShape;
    public float colliderSize = 30.0f;
    public float interval = 5.0f;
    public float impactSize = 20.0f;
    public enum ColliderEffect { FireDistort, Cube_JailFence, Cube_DistortAndJail }
    public ColliderEffect colliderEffect;
    public bool isActivated = false;
    public GameObject colliderObject;
    public GameObject colliderSubObject;
    // private string collisionObjectName;
    // private string collisionObjectType;

    // 足場に乗ったプレイヤーを、足場オブジェクトの子要素にする
    // プレイヤーにアタッチするスクリプト
    // void OnCollisionEnter(Collision col) {
    //     if (col.gameObject.tag == "MoveStage") {
    //         transform.SetParent(col.transform);
    //     }
    // }
    
    // void OnCollisionExit(Collision col) {
    //     if (col.gameObject.tag == "MoveStage") {
    //         transform.SetParent(null);
    //     }
    // }

    void Start()
    {
        SelectShape();
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        // バリケード範囲サイズ(colliderSize)を数分後に段階的縮小
        // StartCoroutine(ReduceBarricadeRange());
    }

    // バリケード外に存続させたくないオブジェクトには"Inbound"タグを要追加
    // Inboundタグ以外のオブジェクトはバリケード範囲外の物理的な進行を制御
    // ※ 無名およびPlayerタグのオブジェクトなどはバリケード範囲外への進行不可(範囲内に留置)
    void OnTriggerEnter(Collider other)
    {
        // バリケードに接触したアイテムの排除
        // Inboundタグのオブジェクトを数秒後に非アクティブ化
        if (other.gameObject.CompareTag("Inbound"))
        {
            StartCoroutine(ObjectInactivate(other));
        }
    }

    IEnumerator ObjectInactivate(Collider other)
    {
        // 指定した秒数だけ処理を待つ(1.0秒)
        yield return new WaitForSeconds(2.5f);
        // オブジェクトを非アクティブ化
        other.gameObject.SetActive(false);
    }

    void SelectShape()
    {
        (colliderObject, colliderSubObject) = colliderShape switch
        {
            ColliderShape.Cube => (
                GameObject.CreatePrimitive(PrimitiveType.Cube),
                GameObject.CreatePrimitive(PrimitiveType.Cube)
            ),
            ColliderShape.Sphere => (
                GameObject.CreatePrimitive(PrimitiveType.Sphere),
                GameObject.CreatePrimitive(PrimitiveType.Sphere)
            ),
            ColliderShape.Cylinder => (
                GameObject.CreatePrimitive(PrimitiveType.Cylinder),
                GameObject.CreatePrimitive(PrimitiveType.Cylinder)
            ),
            ColliderShape.Capsule => (
                GameObject.CreatePrimitive(PrimitiveType.Capsule),
                GameObject.CreatePrimitive(PrimitiveType.Capsule)
            ),
            _ => (
                GameObject.CreatePrimitive(PrimitiveType.Cube),
                GameObject.CreatePrimitive(PrimitiveType.Cube)
            )
        };

        colliderObject.transform.position = transform.position;
        colliderObject.transform.SetParent(transform);

        float modifier = 0.0f;
        switch (colliderShape)
        {
            case ColliderShape.Cube:
                colliderObject.transform.localScale = new Vector3(colliderSize, colliderSize / 2, colliderSize);
                modifier = colliderSize / 4;
                break;
            case ColliderShape.Cylinder:
                colliderObject.transform.localScale = new Vector3(colliderSize, colliderSize / 4, colliderSize);
                modifier = colliderSize / 4;
                break;
            case ColliderShape.Capsule:
                colliderObject.transform.localScale = new Vector3(colliderSize / 2, colliderSize / 2, colliderSize / 2);
                colliderObject.transform.Rotate(0f, 90.0f, 90.0f);
                modifier = colliderSize / 6;
                break;
            case ColliderShape.Sphere:
                colliderObject.transform.localScale = new Vector3(colliderSize, colliderSize, colliderSize);
                modifier = colliderSize / 4;
                break;
            default:
                break;
        }
        Vector3 newPos = colliderObject.transform.position;
        newPos.y = colliderObject.transform.position.y + modifier;
        colliderObject.transform.position = newPos;   
        SelectEffect();
    }

    void SelectEffect()
    {
        InverseMesh();

        switch (colliderEffect)
        {
            case ColliderEffect.FireDistort:
                FireDistort();
                break;
            case ColliderEffect.Cube_JailFence:
                Cube_JailFence();
                break;
            case ColliderEffect.Cube_DistortAndJail:
                FireDistort();
                Cube_JailFence();
                break;
            default:
                Debug.Log("Invalid Collider Effect.");
                break;
        }
    }

    void InverseMesh()
    {
        Destroy(colliderObject.GetComponent<Renderer>());

        Collider collider = colliderObject.GetComponent<Collider>();
        collider.isTrigger = true;

        Mesh mesh = colliderObject.GetComponent<MeshFilter>().mesh;

        int[] triangles = mesh.triangles;
        System.Array.Reverse(triangles);

        mesh.triangles = triangles;
        colliderObject.AddComponent<MeshCollider>();
    }

    void FireDistort()
    {
        // Add FireDistort shader to colliderSubObject
        Renderer renderer = colliderSubObject.GetComponent<Renderer>();
        Material material = new Material(Shader.Find("Custom/FireDistort")); // Replace with the actual name of your FireDistort shader
        renderer.material = material;
        // Set the same position and scale as colliderObject
        colliderSubObject.transform.position = colliderObject.transform.position;
        colliderSubObject.transform.rotation = colliderObject.transform.rotation;
        colliderSubObject.transform.localScale = colliderObject.transform.localScale;
        // マテリアルにシェーダーパラメータを設定する
        // material.SetColor("_MainColor", new Color(0, 0, 0.5f, 0.3f));
        // マテリアルにシェーダーパラメータを設定する
        material.SetColor("_ColorA", new Color(0.35f, 0.35f, 0.60f, 0.13f)); // 色を設定
        material.SetColor("_ColorB", new Color(0.45f, 0.45f, 0.85f, 0.1f)); // 色を設定
        material.SetColor("_TintA", new Color(0.25f, 0.25f, 0.45f, 0.15f)); // 色を設定
        material.SetColor("_TintB", new Color(0.25f, 0.25f, 0.60f, 0.1f)); // 色を設定
        // material.SetFloat("_ScrollX", 1.0f); // スクロール値を1.0に設定
        // material.SetFloat("_ScrollY", 1.0f); // スクロール値を1.0に設定
        // material.SetFloat("_Offset", 1.0f); // オフセット値を1.0に設定
        // material.SetFloat("_Hard", 30.0f); // ハードカットオフ値を30.0に設定
        // material.SetFloat("_Height", 1.0f); // 高さ値を1.0に設定
        // material.SetFloat("_Edge", 1.0f); // エッジ値を1.0に設定
        // material.SetFloat("_Distort", 0.2f); // 歪み値を0.2に設定
        // // material.SetTexture("_NoiseTex", カスタムNoiseTexture); // ノイズテクスチャを設定
        // // material.SetTexture("_DistortTex", カスタムyourDistortTexture); // 歪みテクスチャを設定
        // material.SetFloat("_Shape", 1.0f); // マスクテクスチャを使用するように設定
        // // material.SetTexture("_ShapeTex", カスタムShapeTexture); // マスクテクスチャを設定
        material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);  // Cullステートを設定
    }    

    void Cube_JailFence()
    {
        Vector3 center = colliderObject.transform.position;
        // center.y = colliderObject.transform.position.y - colliderSize/8;

        Vector3 halfSize = Vector3.one * colliderSize / 2;
        // for (float i = -colliderSize / 2 + interval; i < colliderSize / 2; i += interval)
        for (float i = -colliderSize / 2; i < colliderSize / 2 + interval; i += interval)
        {
            //  前面(縦・横)
            GenerateCylinder(center + new Vector3(halfSize.x, (-i/2), halfSize.z), center + new Vector3(-halfSize.x, (-i/2), halfSize.z));
            GenerateCylinder(center + new Vector3(-i, (halfSize.y/2), halfSize.z), center + new Vector3(-i, (-halfSize.y/2), halfSize.z));
            //  後面(縦・横)
            GenerateCylinder(center + new Vector3(halfSize.x, (i/2), -halfSize.z), center + new Vector3(-halfSize.x, (i/2), -halfSize.z));
            GenerateCylinder(center + new Vector3(i, (-halfSize.y/2), -halfSize.z), center + new Vector3(i, (halfSize.y/2), -halfSize.z));
            //  右面(縦・横)
            GenerateCylinder(center + new Vector3(-halfSize.x, (halfSize.y/2), i), center + new Vector3(-halfSize.x, (-halfSize.y/2), i));
            GenerateCylinder(center + new Vector3(-halfSize.x, (i/2), halfSize.z), center + new Vector3(-halfSize.x, (i/2), -halfSize.z));
            //  左面(縦・横)
            GenerateCylinder(center + new Vector3(halfSize.x, (halfSize.y/2), -i), center + new Vector3(halfSize.x, (-halfSize.y/2), -i));
            GenerateCylinder(center + new Vector3(halfSize.x, (-i/2), halfSize.z), center + new Vector3(halfSize.x, (-i/2), -halfSize.z));
            //  上面(縦・横)
            GenerateCylinder(center + new Vector3(i, (halfSize.y/2), halfSize.z), center + new Vector3(i, (halfSize.y/2), -halfSize.z));
            GenerateCylinder(center + new Vector3(i, (-halfSize.y/2), halfSize.z), center + new Vector3(i, (-halfSize.y/2), -halfSize.z));
            //  下面(縦・横)
            GenerateCylinder(center + new Vector3(halfSize.x, (halfSize.y/2), i), center + new Vector3(-halfSize.x, (halfSize.y/2), i));
            GenerateCylinder(center + new Vector3(halfSize.x, (-halfSize.y/2), i), center + new Vector3(-halfSize.x, (-halfSize.y/2), i));
        }
    }    

    void GenerateCylinder(Vector3 startPoint, Vector3 endPoint)
    {
        // 開始位置と終了位置から長さを計算
        float height = Vector3.Distance(startPoint, endPoint);
        // cubeの1辺に当たる部分のオブジェクト生成
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = (startPoint + endPoint) / 2; // 中点を設定
        cylinder.transform.up = (endPoint - startPoint).normalized;
        cylinder.transform.localScale = new Vector3(0.25f, height / 2, 0.25f); // 半径を 0.5 に設定
        // マテリアル設定
        Renderer rend = cylinder.GetComponent<Renderer>();
        rend.material = new Material(Shader.Find("Transparent/Diffuse"));
        // rend.material = new Material(Shader.Find("Custom/FireDistort"));
        // rend.material = new Material(Shader.Find("Custom/FlameShader"));
        rend.material.color = new Color(1.0f, 0.2f, 0.2f, 0.35f); // 半透明の赤色に設定
        // AttachFlameShader(cylinder);
    }

    void DisableMesh() 
    {
        if (colliderObject.gameObject != null)
        {
            Destroy(colliderObject.gameObject);
        }
    }

    IEnumerator ReduceBarricadeRange()
    {
        // 指定した秒数だけ処理を待つ
        yield return new WaitForSeconds(5.0f);
        // yield return new WaitForMinutes(3);
        while (colliderSize > impactSize)
        {
            Debug.Log("colliderSize");
            // DisableMesh();
            colliderSize -= 0.25f;
            InverseMesh();
        }
    }

    // void OnGUI()
    // {
    //     if (hasCollided)
    //     {
    //         GUILayout.Label("Collision Object: " + collisionObjectName);
    //         GUILayout.Label("Collision Object Type: " + collisionObjectType);
    //     }
    // }
}
