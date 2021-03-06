﻿using Lawsome.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawsome.Logic
{
    class EvaluationResult
    {
        public bool IsGood { get; }

        public string Reason { get; }

        public EvaluationResult(bool isGood, string reason = "")
        {
            IsGood = isGood;
            Reason = reason;
        }

        public override string ToString()
        {
            return IsGood ? "Good" : $"Warning: {Reason}";
        }
    }

    //can be set up
    class Evaluator
    {
        //List of evaluators per clause type
        public Dictionary<Clause, EvaluationResult> Evaluate(Contract contract, Party whoAmI)
        {
            var toReturn = new Dictionary<Clause, EvaluationResult>();
            foreach (var clause in contract.Clauses)
            {
                toReturn.Add(clause, Evaluate(clause, whoAmI));
            }
            return toReturn;
        }

        EvaluationResult Evaluate(Clause clause, Party whoAmI)
        {
            switch (clause)
            {
                case ArbitrationClause arbitrationClause:
                    return EvaluateArbitrationClause(arbitrationClause);
                case NonDisclosureClause nonDisclosureClause:
                    return EvaluateNonDiscloseureClause(nonDisclosureClause, whoAmI);
                default:
                    return new EvaluationResult(false, "Unknown clause");
            }
        }

        private EvaluationResult EvaluateNonDiscloseureClause(NonDisclosureClause nonDisclosureClause, Party whoAmI)
        {
            if (nonDisclosureClause.WhoIsBound.Count == 1 && nonDisclosureClause.WhoIsBound[0].Equals(whoAmI))
            {
                return new EvaluationResult(false, "unilaterally binding to own disadvandtage");
            }

            if (nonDisclosureClause.OnlyMarked)
            {
                return new EvaluationResult(false, "Only marked informations are labeld confidential.");
            }
            return new EvaluationResult(true);

        }

        private EvaluationResult EvaluateArbitrationClause(ArbitrationClause arbitrationClause)
        {
            List<string> badCountries = new List<string>() { "United Kingdom", "GB" };
            List<GoverningLaw> badGoverningLaw = new List<GoverningLaw>() { GoverningLaw.British };

            //hardcoded:
            var index = badCountries.IndexOf(arbitrationClause.PlaceOfArbirtration.Country);
            if (index > -1)
            {
                return new EvaluationResult(false, $"{badCountries[index]} is bad as Place of arbitration");
            }

            index = badGoverningLaw.IndexOf(arbitrationClause.GoverningLaw);
            if (index > -1)
            {
                return new EvaluationResult(false, $"{badGoverningLaw[index]} is bad as Choice for governing law.");
            }

            return new EvaluationResult(true);
        }
    }
}
