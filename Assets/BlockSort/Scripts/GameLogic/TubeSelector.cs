namespace BlockSort.GameLogic
{
    public class TubeSelector
    {
        private const int noneTube = -101;

        private const int chooseFirstTubeSuccess = 1;
        private const int chooseSecondTubeSuccess = 2;
        private const int chooseFirstTubeFail = -1;
        private const int chooseSecondTubeFail = -2;
        private const int deleteFirstTubeSuccess = 3;

        private int firstTube;
        private int secondTube;

        public TubeSelector()
        {
            firstTube = noneTube;
            secondTube = noneTube;
        }

        public Config.Config ChooseTube(int indexTube)
        {
            if (firstTube != noneTube)
            {
                if (firstTube == indexTube)
                {
                    firstTube = noneTube;
                    return Config.Config.DELETE_FIRST_TUBE_SUCCESS;
                }

                secondTube = indexTube;

                if (GameLogic.GetInstance().GetGame().MoveBlock(firstTube, secondTube))
                {
                    firstTube = noneTube;
                    secondTube = noneTube;
                    return Config.Config.CHOOSE_SECOND_TUBE_SUCCESS;
                }

                firstTube = noneTube;
                secondTube = noneTube;
                return Config.Config.CHOOSE_SECOND_TUBE_FAIL;
            }

            if (GameLogic.GetInstance().GetGame().GetCurGameStatus().GetTubeByIndex(indexTube)
                .IsEmptyOrFullATypeBlock())
            {
                return Config.Config.CHOOSE_FIRST_TUBE_FAIL;
            }

            firstTube = indexTube;
            return Config.Config.CHOOSE_FIRST_TUBE_SUCCESS;
        }

        public void ResetChoose()
        {
            firstTube = noneTube;
            secondTube = noneTube;
        }
    }
}
