using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct Point : IEquatable<Point>
{
    //  이 구조체에서의 X값과 Y값은 Vector2의 X,Y를 의미하지 않는다.
    public int x;
    public int y;
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    //  operator overloading 을 통하여 타일을 연산자로 계산할 수 있도록 함.
    public static bool operator ==(Point a, Point b)
    {
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(Point a, Point b)
    {
        return !(a == b);
    }

    public static Point operator +(Point a, Point b)
    {
        return new Point(a.x + b.x, a.y + b.y);
    }

    public static Point operator -(Point p1, Point p2)
    {
        return new Point(p1.x - p2.x, p1.y - p2.y);
    }


     
    public override bool Equals(object obj)
    {
        if (obj is Point)
        {
            Point p = (Point)obj;
            return x == p.x && y == p.y;
        }
        return false;
    }

    public bool Equals(Point p)
    {
        return x == p.x && y == p.y;
    }

    public override int GetHashCode()
    {
        return x ^ y;
    }
    //  위치정보 반환을 위한 포멧
    public override string ToString()
    {
        return string.Format("({0},{1})", x, y);
    }

    //  벡터 변환
    public static implicit operator Vector2(Point p)
    {
        return new Vector2(p.x, p.y);
    }
}
