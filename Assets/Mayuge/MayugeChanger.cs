using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface IMayugeChanger : IEventSystemHandler
{
    void OnMayugeChange(MayugeManager.MayugeType mayugeType);
}
