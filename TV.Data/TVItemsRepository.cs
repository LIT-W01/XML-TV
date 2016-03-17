using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV.Data
{
    public class TVItemsRepository
    {
        private readonly string _connectionString;

        public TVItemsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddItem(TVItem item)
        {
            using (var context = new TVItemsDataContext(_connectionString))
            {
                context.TVItems.InsertOnSubmit(item);
                context.SubmitChanges();
            }
        }

        public IEnumerable<TVItem> GetAll()
        {
            using (var context = new TVItemsDataContext(_connectionString))
            {
                return context.TVItems.ToList();
            }
        }

        public void AddNewItemChangeLog(int itemNumber)
        {
            using (var context = new TVItemsDataContext(_connectionString))
            {
                var changeLog = new ChangeLog
                {
                    ItemNumber = itemNumber,
                    ChangeType = "Add"
                };
                context.ChangeLogs.InsertOnSubmit(changeLog);
                context.SubmitChanges();
            }
        }

        public void RemoveItemChangeLog(int itemNumber)
        {
            using (var context = new TVItemsDataContext(_connectionString))
            {
                var changeLog = new ChangeLog
                {
                    ItemNumber = itemNumber,
                    ChangeType = "Remove"
                };
                context.ChangeLogs.InsertOnSubmit(changeLog);
                context.SubmitChanges();
            }
        }

        public void UpdateItem(TVItem item)
        {
            using (var context = new TVItemsDataContext(_connectionString))
            {
                context.TVItems.Attach(item);
                context.Refresh(RefreshMode.KeepCurrentValues, item);
                context.SubmitChanges();
            }
        }

        public void ChangeItemChangeLog(ChangeLog changeLog)
        {
            using (var context = new TVItemsDataContext(_connectionString))
            {
                context.ChangeLogs.InsertOnSubmit(changeLog);
                context.SubmitChanges();
            }
        }

        public void DeleteItem(int itemNumber)
        {
            using (var context = new TVItemsDataContext(_connectionString))
            {
                context.ExecuteCommand("DELETE FROM TvItems WHERE ItemNumber = {0}", itemNumber);
            }
        }

        public void ClearChangeLogs()
        {
            using (var context = new TVItemsDataContext(_connectionString))
            {
                context.ExecuteCommand("TRUNCATE Table ChangeLogs");
            }
        }
    }
}
