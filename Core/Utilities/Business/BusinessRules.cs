using Core.Utilities.Results;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        //logic iş kuralı demek
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.IsSuccess)
                {
                    return logic;
                }
            }
            return null;
        }
    }
}