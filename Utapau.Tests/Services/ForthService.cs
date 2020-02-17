namespace Utapau.Tests.Services
{
    public class ForthService
    {
        public IService FirstService { get; }
        
        public IService SecondService { get; }

        public ForthService(IService firstService, IService secondService)
        {
            FirstService = firstService;
            SecondService = secondService;
        }
    }
}