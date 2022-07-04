using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AxisLock : CinemachineExtension
{
    [Tooltip("Lock the camera's Y position to this value")]
    public float m_YPosition = 13;
 
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            var rot = state.RawOrientation;
            pos.y = m_YPosition;
            rot.y = 0;
            state.RawPosition = pos;
            state.RawOrientation = rot;
        }
    }
}