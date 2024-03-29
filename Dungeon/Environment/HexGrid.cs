﻿using System.Drawing;

namespace Dungeon.Environment;

public class HexGrid<T> where T : ILocation
{
    private readonly IEnumerable<Point> xEvenNeighborModifiers = new List<Point>() {
        new(-1, -1),
        new(-1, 0),
        new(0, 1),
        new(1, 0),
        new(1, -1),
        new(0, -1)
    };

    private readonly IEnumerable<Point> xOddNeighborModifiers = new List<Point>() {
        new(-1, 0),
        new(-1, 1),
        new(0, 1),
        new(1, 1),
        new(1, 0),
        new(0, -1)
    };

    private readonly T[,] cells;

    public int Width => cells.GetLength(0);

    public int Height => cells.GetLength(1);

    public T this[int x, int y]
    {
        get => cells[x, y];
        set => cells[x, y] = value;
    }

    public HexGrid(int width, int height)
    {
        cells = new T[width, height];
    }

    public IEnumerable<Point> GetNeighbors(int x, int y)
    {
        if (!IsInsideGrid(x, y))
        {
            throw new ArgumentException($"The provided coordinates {nameof(x)}, {nameof(y)} are outside this grid.\n" +
                $"\t{nameof(x)}-coordinate must be between 0 and {Width - 1}.\n" +
                $"\t{nameof(y)}-coordinate must be between 0 and {Height - 1}.");
        }

        List<Point> neighbors = new();

        foreach (var m in GetNeighborIndexModifiers(x))
        {
            if (IsInsideGrid(x + m.X, y + m.Y))
            {
                neighbors.Add(new Point(x + m.X, y + m.Y));
            }
        }

        return neighbors;
    }

    public IEnumerable<Point> GetTraversableNeighbors(int x, int y)
    {
        if (!IsInsideGrid(x, y))
        {
            throw new ArgumentException($"The provided coordinates {nameof(x)}, {nameof(y)} are outside this grid.\n" +
                $"\t{nameof(x)}-coordinate must be between 0 and {Width - 1}.\n" +
                $"\t{nameof(y)}-coordinate must be between 0 and {Height - 1}.");
        }

        List<Point> neighbors = new();

        foreach (var m in GetNeighborIndexModifiers(x))
        {
            if (IsInsideGrid(x + m.X, y + m.Y) && this[x + m.X, y + m.Y].IsTraversable())
            {
                neighbors.Add(new Point(x + m.X, y + m.Y));
            }
        }

        return neighbors;
    }

    private bool IsInsideGrid(int x, int y)
    {
        return 0 <= x && x < Width && 0 <= y && y < Height;
    }

    private IEnumerable<Point> GetNeighborIndexModifiers(int x)
    {
        return x % 2 == 0 ? xEvenNeighborModifiers : xOddNeighborModifiers;
    }

    public List<Point> GetPath(int startX, int startY, int destX, int destY)
    {
        var distance = InitializeDistanceMatrix();
        distance[startX, startY] = 0;
        var q = new Queue<Point>();
        q.Enqueue(new Point(startX, startY));

        while (q.Count > 0)
        {
            var p = q.Dequeue();
            foreach (var n in GetTraversableNeighbors(p.X, p.Y))
            {
                if (distance[p.X, p.Y] < distance[n.X, n.Y])
                {
                    distance[n.X, n.Y] = distance[p.X, p.Y] + 1;
                    q.Enqueue(n);
                }
                if (p.X == destX && p.Y == destY)
                {
                    return ConstructPath(distance, destX, destY);
                }
            }
        }

        return null;
    }

    private List<Point> ConstructPath(int[,] distance, int destX, int destY)
    {
        var path = new List<Point>();
        var current = new Point(destX, destY);
        var distanceToOrigin = distance[destX, destY];
        do
        {
            path.Add(current);
            foreach (var neighbor in GetNeighbors(current.X, current.Y))
            {
                if (distance[neighbor.X, neighbor.Y] == distanceToOrigin - 1)
                {
                    current = neighbor;
                    distanceToOrigin--;
                    break;
                }
            }
        } while (0 < distanceToOrigin);

        path.Add(current);
        path.Reverse();

        return path;
    }

    private int[,] InitializeDistanceMatrix()
    {
        int[,] distance = new int[Width, Height];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                distance[i, j] = int.MaxValue;
            }
        }

        return distance;
    }
}
