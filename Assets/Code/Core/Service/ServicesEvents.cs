using System;
using UnityEngine;

public static class ServicesEvents
{
    public static class Constructor
    {
        public static event Action<Vector3> OnCursorCell;
        public static event Action<GridObject> OnDrag;
        public static event Action<GridObject> OnDrop;
        public static event Action<GridObject> OnPointIn;
        public static event Action<GridObject> OnPointOut;
        public static event Action OnFloorUp;
        public static event Action OnFloorDown;

        public static void CursorCell(Vector3 worldPosition)
        {
            OnCursorCell?.Invoke(worldPosition.ToCell());
        }

        public static void Drag(GridObject Object)
        {
            OnDrag?.Invoke(Object);
        }
        
        public static void Drop(GridObject Object)
        {
            OnDrop?.Invoke(Object);
        }

        public static void PointIn(GridObject Object)
        {
            OnPointIn?.Invoke(Object);
        }
        
        public static void PointOut(GridObject Object)
        {
            OnPointOut?.Invoke(Object);
        }

        public static void FloorUp()
        {
            OnFloorUp?.Invoke();
        }
        
        public static void FloorDown()
        {
            OnFloorDown?.Invoke();
        }
    }
    
    public static class Game
    {
        public static event Action OnStartGame;

        public static void StartGame()
        {
            OnStartGame?.Invoke();
        }
    }
}