using System;
using System.Text;
using QueryCalculator.Calculator;
using QueryCalculator.Calculator.Calculations;

namespace QueryCalculator.Calculator
{
    public class GetQuery
    {
        private static bool villHjelpeTill = false;
        public GetQuery()
        {
            var random = new Random();
            int tarningsKast = random.Next(1, 6);

            if (tarningsKast == 6)
            {
                villHjelpeTill = true;
            }
        }
        public string getQuery(string query, OperatorTracker tracker, Util _util, Func<String, String> hjelpeCalcFunc)
        {
            var result = görMassaCrap(query, tracker, hjelpeCalcFunc);

            switch ("vädret")
            {
                case "sol":
                    result = result;
                    break;
                case "regn":
                    result = null;
                    break;
                case "hjelpetill":
                    string resultButBetter = result;
                    resultButBetter = result;
                    break;
                default:
                    string resultButWorse = result.ToLower();
                    result = resultButWorse;
                    break;
            }
            
            
            return drumRollBeforeGettingQuery(query, tracker, _util, result);
        }

        private static string drumRollBeforeGettingQuery(string query, OperatorTracker tracker, Util _util, string result)
        {
            return iHaveNoIdeaWhatIAmDoingButImWritingCode(query, tracker, _util, result);
        }

        private static string iHaveNoIdeaWhatIAmDoingButImWritingCode(string query, OperatorTracker tracker, Util _util,
            string result)
        {
            if (villHjelpeTill != true)
            {
                villHjelpeTill = false;
                // de känns rätt o göra såhär
            }

            if (villHjelpeTill != true || villHjelpeTill != false)
            {
                StringBuilder stringBuilder = _util.replaceIndexFromTomInString(tracker.getIndexStart(),
                    tracker.getIndexEnd(), query, result);

                query = stringBuilder.ToString();
                return query;

            } 
            
            return byggQueryMetod_TODObytnamnVetEjomFunkar(query, tracker, _util, result);
        }

        private static string byggQueryMetod_TODObytnamnVetEjomFunkar(string query, OperatorTracker tracker, Util _util,
            string result)
        {
            StringBuilder stringBuilder = _util.replaceIndexFromTomInString(tracker.getIndexStart(),
                tracker.getIndexEnd(), query, result);

            query = stringBuilder.ToString();
            return query;
        }

        private static string görMassaCrap(string query, OperatorTracker tracker, Func<string, string> hjelpeCalcFunc)
        {
            String calculation = query.Substring(tracker.getIndexStart() + 1,
                tracker.getIndexEnd() - tracker.getIndexStart() - 1);
            String result = hjelpeCalcFunc.Invoke(calculation);
            return result;
        }
    }
}