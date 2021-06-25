using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//tutorial: https://www.youtube.com/watch?v=xkcCWqifT9M&list=RDCMUCmtyQOKKmrMVaKuRXz02jbQ&index=2

public class FieldOfView : MonoBehaviour
{
    [Range (0,  10)] public float viewRadius = 7;
    [Range (0, 360)] public float viewAngle = 75;
    [SerializeField] MeshFilter viewMeshFilter = null; 
    Mesh viewMesh;

    [SerializeField] LayerMask targetMask = 0; 
    [SerializeField] LayerMask obstacleMask = 0;
    [SerializeField] float edgesDstThreshold = 0.5f;
    [Range (0.1f, 5.0f)][SerializeField] float meshResolution = 0.4f;
    
    [Range (1, 20)][SerializeField] int edgeResolveIterations = 10;
    
    List<Transform> visibleTargets = new List<Transform>();
    [HideInInspector]public Transform ClosestTarget = null;

    
    public void FieldOfViewInit(){
        viewMesh = new Mesh();
        viewMesh.name = "malha_da_vista";
        viewMeshFilter.mesh = viewMesh;
    }

    public void FindVisibleTargets(){
       //here, we clean the list of visibleTargets and the ClosestTareget so we update it everytime this func is called
        visibleTargets.Clear();
        ClosestTarget = null;

        //here we get all objects around the viewRadius through their colliders
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        
        //for performance proposes, I added this "if", so the code continues only if there is any obj in targetsInViewRadius 
        if (targetsInViewRadius.Length > 0)
        {   
            //so, for any obj in targetsInViewRadius
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform _target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (_target.position - transform.position).normalized;
                
                //and if the target is not the drone itself
                if (_target != this.transform)
                {
                    //there is a 'if' checking the viewAngle or something (didnt get this enough to explain lol);
                    if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                    {
                        float dstToTarget = Vector3.Distance(transform.position, _target.position);
                        //and if there is not any obstacle between drone and target (obstacleMask)...
                        if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                        {
                            //the '_target' is added to the list
                            visibleTargets.Add(_target);
                            //so we can set ClosestTarget as any one of the objs in visibleTargets, to handle ClosestTarget in the other scripts.
                            ClosestTarget = visibleTargets[0]; //here is setted as the first of the array list for testing, but we can implement the code to get the real closest one in the list later.
                        }
                    }
                } 
            }
        }
    }

    /// --------------------------------------------------------------------------------------------------------//////
    /// ------------  ALL METHODS UNDER THIS LINE ARE MADE FOR RENDERING THE MESH OF THE VIEW 'LIGHT' OF THE DRONE -------- //////
    /// --------------------------------------------------------------------------------------------------------//////

    public Vector3 DirFromAngle(float _angleInDegrees, bool _angleIsGlobal){
        if(!_angleIsGlobal)
            _angleInDegrees += transform.eulerAngles.y;
        float finalDeg = _angleInDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(finalDeg), 0, Mathf.Cos(finalDeg));
    }
    public void DrawFieldOfView(){
        //this method draws the field-of-view mesh ('light') effect, and is called inside LateUpdate() witch is called after normal Update() unity method 
        int stepCount = Mathf.RoundToInt( viewAngle * meshResolution );
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++){
            float angle = transform.eulerAngles.y - viewAngle/2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            
            if (i > 0){
                bool edgesDstThresholdExceeded = (Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgesDstThreshold);
                
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgesDstThresholdExceeded)){
                    EdgeInfo edge = FindEdge (oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero){
                        viewPoints.Add(edge.pointA);
                    }
                     if (edge.pointB != Vector3.zero){
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        //drawing the visible triangles of the mesh
        int vectexCount = viewPoints.Count + 1;
        Vector3[] vectices = new Vector3[vectexCount];
        int[] triangles = new int[ (vectexCount - 2) * 3 ];
        vectices[0] = Vector3.zero;
        for (int i = 0; i < vectexCount-1; i++)
        {
            vectices [i+1] = transform.InverseTransformPoint(viewPoints[i]);

            if( i < vectexCount-2){
                triangles[i*3]     =   0;
                triangles[i *3+1]  =   (i + 1);
                triangles[i *3+2]  =   (i + 2);
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vectices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }
    private EdgeInfo FindEdge(ViewCastInfo _minViewCast, ViewCastInfo _maxViewCast){
        float minAngle = _minViewCast.angle;
        float maxAngle = _maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++){
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast (angle);

            bool edgesDstThresholdExceeded = (Mathf.Abs(_minViewCast.dst - newViewCast.dst) > edgesDstThreshold);

            if (newViewCast.hit == _minViewCast.hit  && !edgesDstThresholdExceeded ){
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else{
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }
        return new EdgeInfo(minPoint, maxPoint);
    }
    private ViewCastInfo ViewCast(float globalAngle){
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        bool Hited = Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask);
        if (Hited)
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        else
            return new ViewCastInfo(false, transform.position + dir*viewRadius, viewRadius, globalAngle);
    }
    public struct ViewCastInfo {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle){
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
    public struct EdgeInfo{
        public Vector3 pointA;
        public Vector3 pointB;
        public EdgeInfo (Vector3 _pointA, Vector3 _pointB){
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
