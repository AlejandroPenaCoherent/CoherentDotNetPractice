using System;
using System.Collections.Generic;

namespace DotNetPractice.Backend.Models
{
    public class OperationResult
    {
        public bool Succeded { get; set; } = false;
        public List<string> Errors { get; set; } = new List<string>();
        public string ContatenatedErrors
        {
            get
            {
                return string.Join(", ", Errors);
            }
        }

        public void AddError(string message)
        {
            Errors.Add(message);
        }
    }

    public class OperationResultWithData<TData> : OperationResult
    {
        public TData Data { get; set; }
    }
}
