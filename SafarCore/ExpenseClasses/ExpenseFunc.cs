using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SafarCore.DbClasses;
using SafarObjects.ExpenseClasses;

namespace SafarCore.ExpenseClasses
{
    public class ExpenseFunc:Expense
    {
        public static Task<FuncResult> AddUpdateExpense(Expense expense)
        {
            return DbConnection.FastAddorUpdate(expense, CollectionNames.Expenses);
        }
        
        public static Task<FuncResult> DeleteExpense(string expenseId)
        {
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("ExpenseId", expenseId, FieldType.String, CompareType.Equal)
            };

            return DbConnection.DeleteMany(filter, CollectionNames.Expenses);
        }

        public static Task<List<Expense>> GetAllExpensesByTrip(string tripId, int startPoint = 0, int count = 30)
        {
            var dbConnection = new DbConnection();

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", tripId, FieldType.String, CompareType.Equal)
            };

            var sort = new SortFilter("PayDateTime", SortType.Descending);

            return dbConnection.GetFilteredListAsync<Expense>(CollectionNames.Expenses, filter, sort, count);
        }

        public static Task<List<Expense>> GetAllExpensesByUserByTrip(string userId, string tripId, int startPoint = 0, int count = 30)
        {
            var dbConnection = new DbConnection();

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", tripId, FieldType.String, CompareType.Equal),
                new FieldFilter("Payer.UserId", userId, FieldType.String, CompareType.Equal)
            };

            var sort = new SortFilter("PayDateTime", SortType.Descending);

            return dbConnection.GetFilteredListAsync<Expense>(CollectionNames.Expenses, filter, sort, count);
        }

        public static Task<List<IndividualBalance>> GetBalanceForUser(string userId)
        {
            //indicates the balance of a user in relation to the other users: should pay or get?
            return new Task<List<IndividualBalance>>(() => new List<IndividualBalance>());
        }

        public static Task<List<IndividualBalance>> GetBalanceByTrip(string tripId)
        {
            //indicates the balance of each user totally for a trip
            return new Task<List<IndividualBalance>>(() => new List<IndividualBalance>());
        }
    }
}
