using UnityEngine;
using UnityEditor;
//tutorial: https://www.youtube.com/watch?v=xkcCWqifT9M&list=RDCMUCmtyQOKKmrMVaKuRXz02jbQ&index=2
[CustomEditor (typeof(FieldOfView)) ]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI() {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);

        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle/2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle/2, false);
       
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        // Handles.color = Color.red;
        // foreach ( Transform _target in fow.visibleTargets){
        //     Handles.DrawLine (fow.transform.position, _target.position);
        // }
    
    }
}
