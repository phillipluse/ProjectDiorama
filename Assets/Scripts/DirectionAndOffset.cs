using System;
using UnityEngine;

namespace ProjectDiorama
{
    public static class DirectionAndOffset
    {
        // public static Vector2Int DirectionsPerRotation(RotationDirection dir)
        // {
        //     return dir switch
        //     {
        //         RotationDirection.Up    => new Vector2Int( 1,  1),
        //         RotationDirection.Right => new Vector2Int( -1, 1),
        //         RotationDirection.Down  => new Vector2Int(-1, -1),
        //         RotationDirection.Left  => new Vector2Int(1,  -1),
        //         _   => new Vector2Int(1, 1)
        //     };
        //     
        //     // return info.InteractionCategory switch
        //     // {
        //     //     GridInteractionCategory.SingleSquare    => SingleSquareDirections(info.ObjectRotation),
        //     //     GridInteractionCategory.SingleRectangle => SingleRectangleGridDirections(info.ObjectRotation),
        //     //     GridInteractionCategory.MultiGrid       => MultiGridObjectDirections(info.ObjectRotation),
        //     //     _ => throw new ArgumentOutOfRangeException()
        //     // };
        // }
        //
        // public static Vector3 ObjectOffset(DirectionOffsetInfo info)
        // {
        //     int cellSize = info.CellSize;
        //     float height = info.ObjectHeight;
        //     float halfX = (float)info.FootPrintGridSize.x / 2;
        //     float halfY = (float)info.FootPrintGridSize.y / 2;
        //     int dirX = info.Directions.x;
        //     int dirZ = info.Directions.y;
        //     int posDir = 1;
        //     int negDir = -1;
        //     
        //     
        //     return info.ObjectRotation switch
        //     {
        //         RotationDirection.Up    => new Vector3(halfX * posDir, height, halfY * posDir),
        //         RotationDirection.Right => new Vector3(halfX * negDir, height, halfY * posDir),
        //         RotationDirection.Down  => new Vector3(halfX * negDir, height, halfY * negDir),
        //         RotationDirection.Left  => new Vector3(halfX * posDir, height, halfY * negDir),
        //         _   => new Vector3(0.0f, 0.0f)
        //     };
        //     
        //     
        //     
        //     // return info.InteractionCategory switch
        //     // {
        //     //     GridInteractionCategory.SingleSquare    => SingleRectangleObjectOffset(info),
        //     //     GridInteractionCategory.SingleRectangle => SingleRectangleObjectOffset(info),
        //     //     GridInteractionCategory.MultiGrid       => MultiGridObjectOffset(info),
        //     //     _ => throw new ArgumentOutOfRangeException()
        //     // };
        // }
        //
        // static Vector2Int SingleSquareDirections(RotationDirection dir)
        // {
        //     return new Vector2Int(1, 1);
        // }
        //
        // /// <summary>
        // /// Use this when at least of the footprint dimensions has a size of 1
        // /// </summary>
        // /// <param name="dir"></param>
        // /// <returns></returns>
        // static Vector2Int SingleRectangleGridDirections(RotationDirection dir)
        // {
        //     return dir switch
        //     {
        //         RotationDirection.Up    => new Vector2Int( 1,  1),
        //         RotationDirection.Right => new Vector2Int( 1, -1),
        //         RotationDirection.Down  => new Vector2Int(-1,  1),
        //         RotationDirection.Left  => new Vector2Int( 1,  1),
        //         _ => new Vector2Int(1, 1)
        //     };
        // }
        //
        // /// <summary>
        // /// Use this when both footprint directions are greater than 1
        // /// </summary>
        // /// <param name="dir"></param>
        // /// <returns></returns>
        // static Vector2Int MultiGridObjectDirections(RotationDirection dir)
        // {
        //     return dir switch
        //     {
        //         RotationDirection.Up    => new Vector2Int( 1,  1),
        //         RotationDirection.Right => new Vector2Int( 1, -1),
        //         RotationDirection.Down  => new Vector2Int(-1, -1),
        //         RotationDirection.Left  => new Vector2Int(-1,  1),
        //         _   => new Vector2Int(1, 1)
        //     };
        // }
        //
        // static Vector3 SingleSquareObjectOffset(DirectionOffsetInfo info)
        // {
        //     float halfX = (float)info.FootPrintGridSize.x / 2;
        //     float halfY = (float)info.FootPrintGridSize.y / 2;
        //     return new Vector3(halfX, info.ObjectHeight, halfY);
        // }
        //
        // static Vector3 SingleRectangleObjectOffset(DirectionOffsetInfo info)
        // {
        //     int cellSize = info.CellSize;
        //     float height = info.ObjectHeight;
        //     float halfX = (float)info.FootPrintGridSize.x / 2;
        //     float halfY = (float)info.FootPrintGridSize.y / 2;
        //     
        //     return info.ObjectRotation switch
        //     {
        //         RotationDirection.Up    => new Vector3(  halfX,            height, halfY),
        //         RotationDirection.Right => new Vector3(halfX - cellSize, height, halfY),
        //         RotationDirection.Down  => new Vector3(halfX - cellSize, height, halfY - cellSize),
        //         RotationDirection.Left  => new Vector3(  halfX,            height, halfY - cellSize),
        //         _   => new Vector3(0.0f, 0.0f)
        //     };
        // }
        //
        // static Vector3 MultiGridObjectOffset(DirectionOffsetInfo info)
        // {
        //     int cellSize = info.CellSize;
        //     float height = info.ObjectHeight;
        //     float halfX = (float)info.FootPrintGridSize.x / 2;
        //     float halfY = (float)info.FootPrintGridSize.y / 2;
        //     
        //     return info.ObjectRotation switch
        //     {
        //         RotationDirection.Up    => new Vector3(  halfX,            height, halfY),
        //         RotationDirection.Right => new Vector3(halfX - cellSize, height, halfY),
        //         RotationDirection.Down  => new Vector3(halfX - cellSize, height, halfY - cellSize),
        //         RotationDirection.Left  => new Vector3(  halfX,            height, halfY - cellSize),
        //         _   => new Vector3(0.0f, 0.0f)
        //     };
        // }
    }
}
