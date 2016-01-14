using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class MayugeManager : MonoBehaviour
{
    static MayugeManager _instance;
    public static MayugeManager instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = GameObject.FindObjectOfType<MayugeManager>();
            return _instance;
        }
    }

    public enum MayugeType
    {
        Default,
        ZOffset,
        SortingOrder,
        Stencil,
        DstAlpha,
        Max
    }
    public MayugeType mayugeType;
    MayugeType _oldMayugeType;

    public Material defaultMaterial;
    public Material zoffsetMaterial;
    public Material sortingOrderMaterial;
    public Material stencilMaterial;
    public Material dstAlphaMaterial;

    UnityEngine.UI.Text _text;
    MayugeCharacter[] _objects;
    void Start()
    {
        _text = GameObject.Find("MayuText").GetComponent<UnityEngine.UI.Text>();
        _text.text = mayugeType.ToString();
        _objects = GameObject.FindObjectsOfType<MayugeCharacter>();
    }

    void Update()
    {
        if (_oldMayugeType != mayugeType)
        {
            foreach (var o in _objects)
            {
                ExecuteEvents.Execute<IMayugeChanger>( o.gameObject, null, (reciveTarget, arg) => reciveTarget.OnMayugeChange(mayugeType) );
            }
        }
        _oldMayugeType = mayugeType;
        _text.text = mayugeType.ToString();
    }

    public void OnChangeMayugeMaterial()
    {
        mayugeType = (MayugeType)(((int)mayugeType + 1) % (int)MayugeType.Max);
    }
}
