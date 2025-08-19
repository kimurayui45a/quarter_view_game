using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// カメラと（エディタの）SceneViewカメラの Transparency Sort を
/// 「Custom Axis（任意軸）」に設定するヘルパー。
/// 2D/アイソメ系で「画面下ほど手前」に見せたいときに使用。
/// </summary>
/// <remarks>
/// - transparencySortAxis = (0, 1, -0.49)
///   → 主に Y（上が奥/下が手前）で並べ、Z も少しだけ考慮してタイブレークする設定。
/// - [ExecuteInEditMode] により、再生していなくても Scene 上で適用される。
///   もし再生中/編集時の両方で確実に動かしたいなら [ExecuteAlways] でもOK。
/// </remarks>
[ExecuteInEditMode] // エディタ編集中も実行
[RequireComponent(typeof(Camera))] // 同じオブジェクトに Camera を必須化
public class AxisDistanceSortCameraHelper : MonoBehaviour
{
    private static readonly Vector3 kSortAxis = new Vector3(0f, 1f, -0.49f);

    void Start()
    {
        // このGameObjectの Camera を取得し、カスタム軸でのソートを有効化
        var cam = GetComponent<Camera>();
        if (cam != null)
        {
            cam.transparencySortMode = TransparencySortMode.CustomAxis;
            cam.transparencySortAxis = kSortAxis;
        }

#if UNITY_EDITOR
        // エディタの SceneView も同じ設定にして、編集時の見た目と実行時の見た目を揃える
        foreach (SceneView sv in SceneView.sceneViews)
        {
            if (sv != null && sv.camera != null)
            {
                sv.camera.transparencySortMode = TransparencySortMode.CustomAxis;
                sv.camera.transparencySortAxis = kSortAxis;
            }
        }
#endif
    }

    // 必要に応じて、OnEnable でも同じ処理を呼ぶと有効/無効切替時に再適用できる。
    // void OnEnable() => Start();
}
