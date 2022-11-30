using ApprovalTests.Reporters.Windows;
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
            var grid = new HexGrid(5, 5);
            var neighbors = grid.GetNeighbors(2, 2);
            
            var n = string.Join(" ", neighbors);
            Assert.AreEqual("{X=1,Y=1} {X=1,Y=2} {X=2,Y=3} {X=3,Y=2} {X=3,Y=1} {X=2,Y=1}", n);
        }

        [TestMethod]
        public void HexagonalGrid_Cell3_2_HasCorrectNeighbors()
        {
            var grid = new HexGrid(5, 5);
            var neighbors = grid.GetNeighbors(3, 2);

            var n = string.Join(" ", neighbors);
            Assert.AreEqual("{X=2,Y=2} {X=2,Y=3} {X=3,Y=3} {X=4,Y=3} {X=4,Y=2} {X=3,Y=1}", n);
        }
    }
}
