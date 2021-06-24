using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStartInit : MonoBehaviour
{
    private Grid grid = null;
    private AStartPathfinding aStar = null;
    private PathRequestManager pathMan = null;
    private void Awake(){
        GetComponents();
        InitComponentsVars();
    }
    private void GetComponents(){
        grid = GetComponent<Grid>();
        aStar = GetComponent<AStartPathfinding>();
        pathMan = GetComponent<PathRequestManager>();
    }
    private void InitComponentsVars(){
        grid.Init();
        pathMan.Init(aStar);
        aStar.Init(grid, pathMan);
    }
}
