using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// �J�����Ɓi�G�f�B�^�́jSceneView�J������ Transparency Sort ��
/// �uCustom Axis�i�C�ӎ��j�v�ɐݒ肷��w���p�[�B
/// 2D/�A�C�\���n�Łu��ʉ��قǎ�O�v�Ɍ��������Ƃ��Ɏg�p�B
/// </summary>
/// <remarks>
/// - transparencySortAxis = (0, 1, -0.49)
///   �� ��� Y�i�オ��/������O�j�ŕ��ׁAZ �����������l�����ă^�C�u���[�N����ݒ�B
/// - [ExecuteInEditMode] �ɂ��A�Đ����Ă��Ȃ��Ă� Scene ��œK�p�����B
///   �����Đ���/�ҏW���̗����Ŋm���ɓ����������Ȃ� [ExecuteAlways] �ł�OK�B
/// </remarks>
[ExecuteInEditMode] // �G�f�B�^�ҏW�������s
[RequireComponent(typeof(Camera))] // �����I�u�W�F�N�g�� Camera ��K�{��
public class AxisDistanceSortCameraHelper : MonoBehaviour
{
    private static readonly Vector3 kSortAxis = new Vector3(0f, 1f, -0.49f);

    void Start()
    {
        // ����GameObject�� Camera ���擾���A�J�X�^�����ł̃\�[�g��L����
        var cam = GetComponent<Camera>();
        if (cam != null)
        {
            cam.transparencySortMode = TransparencySortMode.CustomAxis;
            cam.transparencySortAxis = kSortAxis;
        }

#if UNITY_EDITOR
        // �G�f�B�^�� SceneView �������ݒ�ɂ��āA�ҏW���̌����ڂƎ��s���̌����ڂ𑵂���
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

    // �K�v�ɉ����āAOnEnable �ł������������ĂԂƗL��/�����ؑ֎��ɍēK�p�ł���B
    // void OnEnable() => Start();
}
