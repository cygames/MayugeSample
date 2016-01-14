using UnityEngine;
using System.Collections;

public class MayugeCharacter : MonoBehaviour, IMayugeChanger
{
    [SerializeField]
    MeshRenderer _mayugeRight = null;
    [SerializeField]
    MeshRenderer _mayugeLeft = null;

    MeshRenderer[] _meshs;
    SkinnedMeshRenderer[] _skinMeshs;
    [SerializeField]
    int _characterOrder;

    void Start()
    {
        _meshs = GetComponentsInChildren<MeshRenderer>();
        _skinMeshs = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        _characterOrder = 100000 + (int)(Camera.main.worldToCameraMatrix.MultiplyPoint(transform.position).z * 10000);
        int sortingOrder = 0;
        int sortingOrderOffset = 1;
        //meshに対し、Typeに合わせたsortingOrder値を設定する
        switch (MayugeManager.instance.mayugeType)
        {
            case MayugeManager.MayugeType.Default:
            case MayugeManager.MayugeType.ZOffset:
                break;
            case MayugeManager.MayugeType.SortingOrder:
            case MayugeManager.MayugeType.Stencil:
                sortingOrder = _characterOrder;
                break;
            case MayugeManager.MayugeType.DstAlpha:
                sortingOrder = _characterOrder * -1;
                sortingOrderOffset = -1;
                break;
        }
        foreach (var m in _meshs)
        {
            m.sortingOrder = sortingOrder;
        }
        foreach (var m in _skinMeshs)
        {
            m.sortingOrder = sortingOrder;
        }
        //まゆげのみ、固有の値設定が必要となる
        _mayugeLeft.sortingOrder = sortingOrder + sortingOrderOffset;
        _mayugeRight.sortingOrder = sortingOrder + sortingOrderOffset;
    }

    public void OnMayugeChange(MayugeManager.MayugeType mayugeType)
    {
        switch (mayugeType)
        {
            case MayugeManager.MayugeType.Default:
                _mayugeLeft.material = MayugeManager.instance.defaultMaterial;
                _mayugeRight.material = MayugeManager.instance.defaultMaterial;
                break;
            case MayugeManager.MayugeType.ZOffset:
                _mayugeLeft.material = MayugeManager.instance.zoffsetMaterial;
                _mayugeRight.material = MayugeManager.instance.zoffsetMaterial;
                break;
            case MayugeManager.MayugeType.SortingOrder:
                _mayugeLeft.material = MayugeManager.instance.sortingOrderMaterial;
                _mayugeRight.material = MayugeManager.instance.sortingOrderMaterial;
                break;
            case MayugeManager.MayugeType.Stencil:
                _mayugeLeft.material = MayugeManager.instance.stencilMaterial;
                _mayugeRight.material = MayugeManager.instance.stencilMaterial;
                break;
            case MayugeManager.MayugeType.DstAlpha:
                _mayugeLeft.material = MayugeManager.instance.dstAlphaMaterial;
                _mayugeRight.material = MayugeManager.instance.dstAlphaMaterial;
                break;
        }
    }
}
