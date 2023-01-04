using ApprovalTests.Reporters.Windows;
using ApprovalTests.Reporters;
using Dungeon.Environment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            _ = grid.GetNeighbors(5, 0);
        }

        [TestMethod]
        public void HexagonalGrid_Cells_CanContainValue()
        {
            var grid = new HexGrid<int>(5, 5);
            grid[1, 3] = 7;

            Assert.AreEqual(grid[1, 3], 7);
        }

        [TestMethod]
        public void HexagonalGrid_SimplePath_CanBeFound()
        {
            var grid = new HexGrid<int>(5, 5);
            for (int i = 0; i < grid.Width; i++)
            {
                for (int j = 0; j < grid.Height; j++)
                {
                    grid[i, j] = 0;
                }
            }

            var path = grid.GetPath(0, 2, 3, 2);
            var p = string.Join("->", path);

            Assert.AreEqual("{X=0,Y=2}->{X=1,Y=1}->{X=2,Y=2}->{X=3,Y=2}", p);
        }

        [TestMethod]
        public void HexagonalGrid_PathFromClosedRoom_CannotBeFound()
        {
            var grid = new HexGrid<int>(10, 10);
            for (int i = 0; i < grid.Width; i++)
            {
                for (int j = 0; j < grid.Height; j++)
                {
                    grid[i, j] = 0;
                }
            }

            for(int i = 0; i < 5; i++)
            {
                grid[i, 0] = grid[i, 4] = grid[0, i] = grid[4, i] = 1;
            }

            var path = grid.GetPath(2, 2, 7, 2);

            Assert.IsNull(path);
        }
    }
}
