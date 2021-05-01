using TrackerLibrary.Models;

namespace TrackerUI
{
    public interface IPrizeRequester
    {
        public void PrizeComplete(PrizeModel model);
    }
}