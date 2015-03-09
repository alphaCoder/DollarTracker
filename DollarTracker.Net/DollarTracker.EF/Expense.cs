//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DollarTracker.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Expense
    {
        public string ExpenseId { get; set; }
        public string ExpenseStoryId { get; set; }
        public System.Guid CollaboratorId { get; set; }
        public double Amount { get; set; }
        public byte[] Receipt { get; set; }
        public Nullable<System.DateTime> CreatedUtcDt { get; set; }
        public string Comments { get; set; }
        public string ExpenseCategoryId { get; set; }
        public string CustomExpenseCategoryId { get; set; }
    
        public virtual Collaborator Collaborator { get; set; }
        public virtual CustomExpenseCategory CustomExpenseCategory { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; }
        public virtual ExpenseStory ExpenseStory { get; set; }
    }
}