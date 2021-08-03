using System;

namespace IdSort.Restructure
{
    public class PlanException : Exception
    {
        public Plan RevertPlan;
        public Operation Operation;

        public PlanException(Plan revertPlan, Operation operation, string? message, Exception? innerException) : base(message, innerException)
        {
            RevertPlan = revertPlan;
            Operation = operation;
        }
    }
}
