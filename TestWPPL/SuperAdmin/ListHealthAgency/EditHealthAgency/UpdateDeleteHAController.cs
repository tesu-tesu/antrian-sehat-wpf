using Velacro.Basic;

namespace TestWPPL.SuperAdmin.ListHealthAgency.EditHealthAgency
{
    public class UpdateDeleteHAController : MyController
    {
        public UpdateDeleteHAController CreateInstance(IMyView _myView)
        {
            return new UpdateDeleteHAController(_myView);
        }

        public UpdateDeleteHAController(IMyView _myView) : base(_myView)
        {
        }
        
        
    }
}