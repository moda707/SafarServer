using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SafarObjects.ExpenseClasses
{
    public class Expense
    {
        public string ExpenseId { get; set; }
        public string TripId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<IndividualExpense> Payer { get; set; }
        public List<IndividualExpense> PaidFor { get; set; }
        public double Amount { get; set; }
        public DateTime PayDateTime { get; set; }
        public ExpenseItemStatus Status { get; set; }

        public Expense()
        {
            
        }

        public ExpenseInDb GetExpenseInDb()
        {
            return new ExpenseInDb()
            {
                ExpenseId = this.ExpenseId,
                TripId = this.TripId,
                Title = this.Title,
                Description = this.Description,
                Amount = this.Amount,
                Payer = this.Payer,
                PaidFor = this.PaidFor,
                PayDateTime = this.PayDateTime,
                Status = this.Status
            };
        }
    }

    public class ExpenseInDb:Expense
    {
        [BsonId]
        public ObjectId _id { get; set; }
        

        public ExpenseInDb()

        {

        }

        public Expense GetExpense()
        {
            return new Expense()
            {
                ExpenseId = this.ExpenseId,
                TripId = this.TripId,
                Title = this.Title,
                Description = this.Description,
                Amount = this.Amount,
                Payer = this.Payer,
                PaidFor = this.PaidFor,
                PayDateTime = this.PayDateTime,
                Status = this.Status
            };
        }
    }

    public enum ExpenseItemStatus   
    {
        Added = 1,
        Deleted = 2,
        Edited = 3
    }

    public class IndividualExpense
    {
        public string UserId { get; set; }
        public double Weight { get; set; }
        public ExpenseWeightType WeightType { get; set; }

        public IndividualExpense()
        {
            
        }

        public IndividualExpense(string userId, double weight, ExpenseWeightType weightType)
        {
            UserId = userId;
            Weight = weight;
            WeightType = weightType;
        }
    }

    public enum ExpenseWeightType
    {
        Percentage = 1,
        Share = 2
    }

    public class IndividualBalance
    {
        public string UserId { get; set; }
        public double Amount { get; set; }
    }
}
