using System.Collections.Generic;

namespace BlockSort.GameLogic
{
    public class Solution
    {
        private HashSet<string> visited;
        private List<int[]> ans;

        public Solution()
        {
            ans = new List<int[]>();
            visited = new HashSet<string>();
        }

        public List<int[]> SolveGameStatus(GameStatus gameStatus)
        {
            ans = new List<int[]>();
            var curGameStatus = gameStatus.Clone();
            if (gameStatus.GetNumTube() > 9 || !Solve(curGameStatus))
            {
                return null;
            }

            ans.Reverse();
            return ans;
        }

        private bool Solve(GameStatus gameStatus)
        {
            if (gameStatus.IsComplete())
            {
                return true;
            }

            var s = gameStatus.ConvertTubesToString();
            if (visited.Contains(s))
            {
                return false;
            }

            visited.Add(s);
            var numTube = gameStatus.GetNumTube();

            for (var i = 0; i < numTube; i++)
            for (var j = 0; j < numTube; j++)
            {
                if (i != j)
                {
                    var copyGameStatus = gameStatus.Clone();
                    var checkMove = copyGameStatus.MoveBlock(i, j);
                    if (checkMove)
                    {
                        if (Solve(copyGameStatus))
                        {
                            ans.Add(new[] { i, j });
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
