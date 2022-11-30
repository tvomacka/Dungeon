﻿using ApprovalTests.Reporters.Windows;
using ApprovalTests.Reporters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dungeon.Environment;

namespace DungeonTests
{
    [UseReporter(typeof(VisualStudioReporter))]
    [TestClass]
    public class HexGridTests
    {
        [TestMethod]
        public void HexagonalGrid_Cell2_2_HasCorrectNeighbors()
        {
            var grid = new HexGrid<int>(5, 5);
            var neighbors = grid.GetNeighbors(2, 2);
            
            var n = string.Join(" ", neighbors);
            Assert.AreEqual("{X=1,Y=1} {X=1,Y=2} {X=2,Y=3} {X=3,Y=2} {X=3,Y=1} {X=2,Y=1}", n);
        }

        [TestMethod]
        public void HexagonalGrid_Cell3_2_HasCorrectNeighbors()
        {
            var grid = new HexGrid<int>(5, 5);
            var neighbors = grid.GetNeighbors(3, 2);

            var n = string.Join(" ", neighbors);
            Assert.AreEqual("{X=2,Y=2} {X=2,Y=3} {X=3,Y=3} {X=4,Y=3} {X=4,Y=2} {X=3,Y=1}", n);
        }

        [TestMethod]
        public void HexagonalGrid_Cell_0_0_HasTwoNeighbors()
        {
            var grid = new HexGrid<int>(5, 5);
            var neighbors = grid.GetNeighbors(0, 0);

            var n = string.Join(" ", neighbors);
            Assert.AreEqual("{X=0,Y=1} {X=1,Y=0}", n);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HexagonalGrid_CellOutOfBounds_Explodes()
        {
            var grid = new HexGrid<int>(3, 3);
            var neighbors = grid.GetNeighbors(5, 0);
        }

        [TestMethod]
        public void HexagonalGrid_Cells_CanContainValue()
        {
            var grid = new HexGrid<int>(5, 5);
            grid[1, 3] = 7;

            Assert.AreEqual(grid[1, 3], 7);
        }
    }
}
